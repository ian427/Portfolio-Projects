#pragma once

#include <chrono>
#include <SDL3/SDL.h>
#include <SDL3/SDL_net.h>

#include "../SharedCode/Shared.h"

int RandomInt(int minVal, int maxVal);

class Server {
public:
    Server(Uint16 port);
    ~Server();

    void Update();
    bool isRunning();

private:

    void doHearingCheck();
    void updateClientStates();
    void processPackets();

    GameObject* findClientByID(int clientId);
    void respawnClient(int clientId);
    void removeClient(int idToRemove);

    void eatPlayer(ServerClient* eater, int eatenId);
    void spawnWalls();

    bool checkWallCollision(const GameObject& mover, float x, float y);

    void handleClientJoin(NET_Address* addr, uint16_t port);
    void handleClientLeave(NET_Datagram* msg);
    void handleClientInput(NET_Datagram* msg);
    void handlePing(NET_Datagram* msg);


    std::chrono::system_clock::time_point TimeToRespawnBerry = std::chrono::system_clock::now();
    bool RespawnBerry = true;
    NET_DatagramSocket* socket{ nullptr };
    clientList clients;
    std::vector<GameObject> walls;
    std::vector<GameObject*> clientsThatUpdated;
    int nextId = 1;
    int currentlyPoweredId = -1;
};