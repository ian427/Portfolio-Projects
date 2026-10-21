#include "Server.h"
#include <random>

int RandomInt(int minVal, int maxVal)
{
    static std::mt19937 rng(std::random_device{}());
    std::uniform_int_distribution<int> dist(minVal, maxVal);
    return dist(rng);
}

Server::Server(Uint16 port)
{
	SDL_Init(0);
	NET_Init();

    socket = NET_CreateDatagramSocket(NULL, port);
    if (!socket) {
        SDL_Log("Socket failed: %s", SDL_GetError());
        throw new std::runtime_error("failed to open socket");
    }

    SDL_Log("Server started");

    spawnWalls();
}

Server::~Server()
{
    for (auto& obj : clients)
    {
        if (!obj)
            continue;

        ServerClient* c = dynamic_cast<ServerClient*>(obj.get());
        if (!c)
            continue;

        NET_UnrefAddress(c->addr);
    }


    NET_DestroyDatagramSocket(socket);
    NET_Quit();
    SDL_Quit();
}

void Server::doHearingCheck()
{
    for (auto& obj : clients)
    {
        if (!obj)
            continue;

        ServerClient* client = dynamic_cast<ServerClient*>(obj.get());
        if (!client)
            continue;
        auto now = std::chrono::steady_clock::now();
        if (now >= client->LastHeardFrom + std::chrono::seconds(5))
        {
            //send ping
            Ping state;
            state.type = PACKET_PING;
            state.id = client->id;
            //std::cout << "Server Ping" <<"\n";
            NET_SendDatagram(socket,
                client->addr,
                client->port,
                &state,
                sizeof(state));

        }
        if (now >= client->LastHeardFrom + std::chrono::seconds(10))
        {
            removeClient(client->id);
        }

    }
}

void Server::updateClientStates()
{
    for (auto& obj : clients)
    {
        if (!obj)
            continue;

        ServerClient* receiver = dynamic_cast<ServerClient*>(obj.get());
        if (!receiver)
            continue;  // not a ServerClient -> skip

        for (auto& obj : clientsThatUpdated)// clients ->swap smoother preformance
        {
            if (!obj)
                continue;

            StatePacket state;
            state.type = PACKET_STATE;
            state.objtype = obj->type;
            state.id = obj->id;
            state.x = obj->x;
            state.y = obj->y;

            //auto client = static_cast<ServerClient*>(obj.get());//if  for (auto& obj : clients)
            auto client = static_cast<ServerClient*>(obj);

            if (client)
            {
                state.isPowered = client->Powered;
            }

            NET_SendDatagram(socket,
                receiver->addr,
                receiver->port,
                &state,
                sizeof(state));
        }
    }
}

void Server::processPackets()
{
    NET_Datagram* dgram = nullptr;

    while (NET_ReceiveDatagram(socket, &dgram) > 0 && dgram) {
        auto clientPort = dgram->port;
        auto clientAddr = dgram->addr;

        PacketType type = *(PacketType*)dgram->buf;

        switch (type) {
        case PacketType::PACKET_INPUT:
            handleClientInput(dgram);
            break;
        case PacketType::PACKET_JOIN:
            handleClientJoin(clientAddr, clientPort);
            break;
        case PacketType::PACKET_LEAVING:
            handleClientLeave(dgram);
            break;
        case PacketType::PACKET_PING:
            handlePing(dgram);
            break;
        }

        NET_DestroyDatagram(dgram);
        dgram = nullptr;
    }
}

void Server::Update()
{
    processPackets();

    if (RespawnBerry)
    {
        if (auto now = std::chrono::system_clock::now() >= TimeToRespawnBerry)
        {
            RespawnBerry = false;
            float x = RandomInt(0, Windowx);
            float y = RandomInt(0, Windowy);
            clients.push_back(std::make_unique<GameObject>(nextId++, x, y));
            clientsThatUpdated.push_back(clients.back().get());
            if (currentlyPoweredId > -1)
            {
                auto client = dynamic_cast<ServerClient*>(findClientByID(currentlyPoweredId));
                client->Powered = false;
                currentlyPoweredId = -1;
                clientsThatUpdated.push_back(client);
            }
        }
    }

    updateClientStates();
    doHearingCheck();

    clientsThatUpdated.clear();

    SDL_Delay(16);
}

bool Server::isRunning()
{
    return true;
}

GameObject* Server::findClientByID(int clientId)
{
    auto it = std::find_if(clients.begin(), clients.end(), [clientId](const auto& c) {return c->id == clientId; });
    if (it != clients.end()) {
        return it->get();
    }

    // If this ever happens, your server state is inconsistent
    throw std::runtime_error("FindByID: invalid id");
}

void Server::respawnClient(int clientId)
{
    auto client = findClientByID(clientId);
    do {
        client->x = RandomInt(0, Windowx);
        client->y = RandomInt(0, Windowy);
    } while (checkWallCollision(*client, client->x, client->y));

    ServerClient* serverClient = dynamic_cast<ServerClient*>(client);

    serverClient->spawnTime = std::chrono::steady_clock::now();
}

void Server::removeClient(int idToRemove)
{
    std::erase_if(clients, [idToRemove](std::unique_ptr<GameObject>& client) {return client->id == idToRemove; });

    for (auto& obj : clients) {
        if (!obj) continue;

        ServerClient* receiver = dynamic_cast<ServerClient*>(obj.get());
        if (!receiver)
            continue;

        LeavingPacket state;
        state.type = PACKET_LEAVING;
        state.id = idToRemove;

        NET_SendDatagram(socket,
            receiver->addr,
            receiver->port,
            &state,
            sizeof(state));
    }
}

void Server::eatPlayer(ServerClient* eater, int eatenId)
{
    ScorePacket state;
    state.score = ++eater->score;
    state.type = PacketType::PACKET_UPDATE_SCORE;
    NET_SendDatagram(socket, eater->addr, eater->port, &state, sizeof(state));
    /// DO EATEN PLAYERS RESPAWN RNG
    respawnClient(eatenId);
}

void Server::spawnWalls()
{
    for (int i = 0; i < 50; i++)
    {
        float x = RandomInt(0, Windowx);
        float y = RandomInt(0, Windowy);

        walls.push_back({ nextId++, x, y });
        walls.back().type = ObjectType::Wall;
    }
}

bool Server::checkWallCollision(const GameObject& mover, float x, float y)
{
    for (const auto& wall : walls)
    {
        // Skip self
        if (wall.id == mover.id)
            continue;

        if (Intersects(x, y, PLAYER_SIZE, PLAYER_SIZE,
            wall.x, wall.y, PLAYER_SIZE, PLAYER_SIZE))
        {
            return true;
        }
    }
    return false;
}

void Server::handleClientJoin(NET_Address* addr, uint16_t port)
{
    clients.push_back(
        std::make_unique<ServerClient>(
            NET_RefAddress(addr), port, nextId++, 100.0f, 100.0f
        )
    );

    ServerClient* client = dynamic_cast<ServerClient*>(clients.back().get());
    clientsThatUpdated.push_back(client);
    client->LastHeardFrom = std::chrono::steady_clock::now();
    SDL_Log("Client joined: %d", client->id);

    //  SEND ID BACK TO CLIENT
    AssignIdPacket msg;
    msg.type = PACKET_ASSIGN_ID;
    msg.id = client->id;

    respawnClient(client->id);

    NET_SendDatagram(
        socket,
        client->addr,
        client->port,
        &msg,
        sizeof(msg)
    );

    for (auto& obj : clients)
    {
        if (!obj)
            continue;

        StatePacket state;
        state.type = PACKET_STATE;
        state.objtype = obj->type;
        state.id = obj->id;
        state.x = obj->x;
        state.y = obj->y;

        //auto client = static_cast<ServerClient*>(obj.get());//if  for (auto& obj : clients)
        auto serverClient = static_cast<ServerClient*>(obj.get());

        if (serverClient)
        {
            state.isPowered = serverClient->Powered;
        }

        NET_SendDatagram(socket,
            client->addr,
            client->port,
            &state,
            sizeof(state));
    }

    for (auto& wall : walls) {
        StatePacket state;
        state.type = PACKET_STATE;
        state.objtype = ObjectType::Wall;
        state.id = wall.id;
        state.x = wall.x;
        state.y = wall.y;

        NET_SendDatagram(socket,
            client->addr,
            client->port,
            &state,
            sizeof(state));
    }

    SDL_Log("Assigned ID %d", client->id);
}

void Server::handleClientLeave(NET_Datagram* msg)
{
    AssignIdPacket* packet = (AssignIdPacket*)msg->buf;

    if (packet->id == currentlyPoweredId)
        currentlyPoweredId = -1;

    removeClient(packet->id);
}

void Server::handleClientInput(NET_Datagram* msg)
{
    InputPacket* input = (InputPacket*)msg->buf;

    for (auto& obj : clients)
    {
        if (!obj)
            continue;

        ServerClient* c = dynamic_cast<ServerClient*>(obj.get());
        if (!c)
            continue;  // not a ServerClient, skip
        // c->LastHeardFrom = std::chrono::steady_clock::now();
        if (c->id == input->id)
        {
            // collision check

          //  int verifyx = std::abs(input->dx);
           // int verifyy = std::abs(input->dy);
            if (!(input->dy == 2) || !(input->dy == -2) || !(input->dx == 2) || !(input->dx == -2))
            {
                CollisionHit clientHit;

                float proposedX = c->x + input->dx;
                float proposedY = c->y + input->dy;

                bool hasHit = CheckCollision(*c, clients, proposedX, proposedY, clientHit) ||
                    checkWallCollision(*c, proposedX, proposedY);

                if (!hasHit)// faking the movement first to check collison
                {
                    //movement applied
                    c->x += input->dx;
                    c->y += input->dy;
                }
                else
                {
                    if (clientHit.type == ObjectType::Berry)
                    {
                        c->Powered = true;
                        RespawnBerry = true;
                        //start timer
                        int delay = 15; //seconds/15seconds
                        auto now = std::chrono::system_clock::now();
                        TimeToRespawnBerry = (now + std::chrono::seconds(delay));
                        currentlyPoweredId = c->id;
                        //once timer over power down and berry respawn
                        // send leaving packet for berry
                        removeClient(clientHit.id);
                    }
                    else if (clientHit.type == ObjectType::Player)
                    {
                        auto hitClient = static_cast<ServerClient*>(findClientByID(clientHit.id));

                        bool thisClientProtected = std::chrono::steady_clock::now() <= c->spawnTime + std::chrono::microseconds(500);
                        bool otherClientProtected = std::chrono::steady_clock::now() <= hitClient->spawnTime + std::chrono::microseconds(500);

                        int respawnId = thisClientProtected ? c->id : otherClientProtected ? hitClient->id : -1;

                        if (respawnId > -1)
                        {
                            respawnClient(respawnId);
                        }
                        else if (c->Powered)
                        {
                            eatPlayer(c, clientHit.id);
                            clientsThatUpdated.push_back(hitClient);
                        }
                        else if (hitClient->Powered)
                        {
                            eatPlayer(hitClient, c->id);
                        }
                    }
                }

                clientsThatUpdated.push_back(c);
            }

            break;
        }
    }
}

void Server::handlePing(NET_Datagram* msg)
{
    Ping* input = (Ping*)msg->buf;

    auto& obj = clients[FindIndexByID(clients, input->id)];

    ServerClient* client = dynamic_cast<ServerClient*>(obj.get());

    if (client)
    {
        client->LastHeardFrom = std::chrono::steady_clock::now();
    }
}
