#include <SDL3/SDL.h>
#include <SDL3/SDL_net.h>
#include <iostream>
#include <cstring>
#include <vector>
//#include "Shared.h"
#include "../SharedCode/Shared.h"

int main(int argc, char** argv) 
{
    int Score = 0;
    int Lastservertime = 0;
    if (SDL_Init(SDL_INIT_VIDEO) < 0) 
    {
        std::cout << "SDL init failed: " << SDL_GetError() << "\n";
        return 1;
    }


    SDL_Window* window = SDL_CreateWindow("Client",Windowx , Windowy, 0);
    SDL_Renderer* renderer = SDL_CreateRenderer(window, NULL);


    if (NET_Init() < 0) 
    {
        std::cout << "SDLNet init failed: " << SDL_GetError() << "\n";
        return 1;
    }

    // Create UDP socket (ephemeral port)
    NET_DatagramSocket* socket = NET_CreateDatagramSocket(NULL, 0);
    if (!socket) 
    {
        std::cout << "Socket failed: " << SDL_GetError() << "\n";
        return 1;
    }

    // Resolve server
    NET_Address* serverAddr = NET_ResolveHostname("127.0.0.1");
    Uint16 serverPort = 1234;

    if (!serverAddr) 
    {
        std::cout << "Resolve failed: " << SDL_GetError() << "\n";
        return 1;
    }

    std::cout << "Client started\n";

    std::vector<std::unique_ptr<GameObject>> Clients;

    // -------------------------
    // SEND JOIN PACKET
    // -------------------------
    PacketType join = PACKET_JOIN;

    NET_SendDatagram(
        socket,
        serverAddr,
        serverPort,
        &join,
        sizeof(join)
    );

    int myId = -1;


    bool running = true;
 
    GameObject* localClient = nullptr;

    while (running) 
    {

        SDL_Event e;
        while (SDL_PollEvent(&e)) 
        {
            if (e.type == SDL_EVENT_QUIT)
                running = false;
        }

        float dx = 0, dy = 0;

        const bool* keys = SDL_GetKeyboardState(NULL);

        if (keys[SDL_SCANCODE_W]) 
            dy = -2;
        if (keys[SDL_SCANCODE_S])
            dy = 2;
        if (keys[SDL_SCANCODE_A])
            dx = -2;
        if (keys[SDL_SCANCODE_D])
            dx = 2;
        if(localClient)
        {
            float proposedX = localClient->x + dx;
            float proposedY = localClient->y + dy;
            CollisionHit hit;
            bool hasHit = CheckCollision(*localClient, Clients, proposedX, proposedY, hit);

            if (!hasHit) {
              localClient->x += dx;
              localClient->y += dy;
            }
        }
        if (myId != -1 && (dx != 0 || dy != 0))
        {
            InputPacket input = { PACKET_INPUT,myId, dx, dy };

            NET_SendDatagram(
                socket,
                serverAddr,
                serverPort,
                &input,
                sizeof(input)
            );
        }

        // -------------------------
        // RECEIVE SERVER DATA
        // -------------------------
        NET_Datagram* dgram = nullptr;

        while (NET_ReceiveDatagram(socket, &dgram) > 0 && dgram) 
        {

            PacketType type = *(PacketType*)dgram->buf;

            if (type == PACKET_STATE) {
                StatePacket* state = (StatePacket*)dgram->buf;

                bool found = false;
               
                for (auto& c : Clients) 
                {
                    if (c->id == state->id) 
                    {
                        c->x = state->x;
                        c->y = state->y;

                        if (c->type == ObjectType::Player) {
                            auto client = static_cast<Client*>(c.get());
                            client->Powered = state->isPowered;
                        }

                        found = true;
                        break;
                    }
                }

                if (!found) // for when new player appears
                {
                    if (state->objtype == ObjectType::Player) {
                        Clients.push_back(std::make_unique<Client>(state->id, state->x, state->y));//change for berry

                        if (state->id == myId) {
                            localClient = Clients.back().get();
                        }

                        SDL_Log("new client detected %d", state->id);
                    }
                    else if (state->objtype == ObjectType::Wall)
                    {
                        Clients.push_back(std::make_unique<GameObject>(state->id, state->x, state->y));
                        auto c = Clients.back().get();
                        c->type = ObjectType::Wall;
                    }
                    else 
                    {
                        Clients.push_back(std::make_unique<GameObject>(state->id, state->x, state->y));
                        SDL_Log("new berry detected %d", state->id);
                    }
                    
                }
            }
            if (type == PACKET_ASSIGN_ID) 
            {

                AssignIdPacket* msg = (AssignIdPacket*)dgram->buf;

                myId = msg->id;

                std::cout << "Got ID: " << myId << "\n";
            }
            if (type == PACKET_UPDATE_SCORE)
            {

                ScorePacket* msg = (ScorePacket*)dgram->buf;
                int tempscore = 0;
                tempscore = msg->score;
                Score = tempscore;
                std::cout << "Your Score: " << Score << "\n";
            }
            if (type == PACKET_LEAVING)
            {
                LeavingPacket* msg = (LeavingPacket*)dgram->buf;
               
                int tempid = msg->id;
                std::erase_if(Clients, [tempid](std::unique_ptr<GameObject>& client) {return client->id == tempid; });
            }
            if (type == PACKET_SWITCHSTATE)
            {
                SDL_Log("SwitchState Packet received");
                SwitchPacket* msg = (SwitchPacket*)dgram->buf;
                int tempid = msg->id;
                int temp = FindIndexByID(Clients, tempid);
               
                auto client = dynamic_cast<Client*>(Clients[temp].get());

                client->Powered = !client->Powered;
            }
            if (type == PACKET_PING)
            {
                Ping input = { PACKET_PING,myId };
                NET_SendDatagram(
                    socket,
                    serverAddr,
                    serverPort,
                    &input,
                    sizeof(input)
                );
            }

            NET_DestroyDatagram(dgram);
            dgram = nullptr;
        }

        // -------------------------
        // RENDER (sprites = rectangles)
        // -------------------------
        SDL_SetRenderDrawColor(renderer, 20, 20, 20, 255);
        SDL_RenderClear(renderer);

        for (auto& c : Clients) 
        {
            SDL_FRect rect = { c->x, c->y, c->size, c->size };//drawing square
            if (c->type == ObjectType::Player)
            {
                Client* client = dynamic_cast<Client*>(c.get());
                if(client->Powered)
                    SDL_SetRenderDrawColor(renderer, 0, 0, 255, 255);
                else
                    SDL_SetRenderDrawColor(renderer, 155, 255, 0, 255);
            }
            else if (c->type == ObjectType::Berry)
            {
                SDL_SetRenderDrawColor(renderer, 255, 0, 0, 255);
            }
            else if (c->type == ObjectType::Wall)
            {
                SDL_SetRenderDrawColor(renderer, 125, 0, 125, 255);
            }
            SDL_RenderFillRect(renderer, &rect);
        }

        SDL_RenderPresent(renderer);



        SDL_Delay(16); // ~60 FPS
    }
    LeavingPacket input = { PACKET_LEAVING,myId};

    NET_SendDatagram(
        socket,
        serverAddr,
        serverPort,
        &input,
        sizeof(input)
    );
    NET_UnrefAddress(serverAddr);
    NET_DestroyDatagramSocket(socket);
    NET_Quit();
    SDL_Quit();

    return 0;
}