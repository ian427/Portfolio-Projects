#pragma once
#include <vector>
#include <memory>
#include <chrono>
#include <SDL3/SDL_net.h>

const int MAX_PLAYERS = 32;
const int Windowx = 800;
const int Windowy = 600;
constexpr float PLAYER_SIZE = 20.0f;

enum PacketType
{
    PACKET_JOIN,
    PACKET_INPUT,
    PACKET_STATE,//current position//and if eaten
    PACKET_ASSIGN_ID,
    PACKET_UPDATE_SCORE,
    PACKET_SWITCHSTATE,//FOR switching powered
    PACKET_LEAVING,
    PACKET_PING
};

enum class ObjectType 
{
    Player,
    Berry,
    Wall
};

struct Vector2
{
    int x;
    int y;
};
struct PlayerState
{
    int id;
    float x, y;
};
struct GameObject
{
    GameObject() : id{ -1 }, x{ 0.0f }, y{ 0.0f } {}
    GameObject(int id, float x, float y) : id{ id }, x{ x }, y{ y } {}
    int id;
    float x, y;
    int size = 20;  
    ObjectType type = ObjectType::Berry;
    virtual ~GameObject() = default;
    
};
struct  Client : GameObject
{
    Client()   // default constructor
    {
        type = ObjectType::Player;
    }

    Client(int id, float x, float y) : GameObject(id, x, y)
    {
        type = ObjectType::Player;
    }

    virtual ~Client() = default;
   
    int score = 0;
    bool Powered = false;
};

struct ServerClient : Client
{

    ServerClient(NET_Address* address, Uint16 p, int objId, float xPos, float yPos)
        : Client(objId, xPos, yPos),
        addr{ address },
        port{ p },
        LastHeardFrom(std::chrono::steady_clock::now())
    {
    }


    virtual ~ServerClient() = default;

    NET_Address* addr;
    Uint16 port;
    std::chrono::time_point<std::chrono::steady_clock> LastHeardFrom;
    std::chrono::time_point<std::chrono::steady_clock> spawnTime;
};

template<typename T>
int FindIndexByID(const std::vector<std::unique_ptr<T>>& clients, int id)
{
    for (size_t i = 0; i < clients.size(); ++i)
    {
        if (clients[i]->id == id)
            return static_cast<int>(i);
    }

    return -1; // not found
}

struct Packet {
    PacketType type;
    int id;
};

struct InputPacket : public Packet
{
    float dx, dy;
};

struct StatePacket : public Packet
{
    ObjectType objtype;
    bool isPowered{ false };
    float x, y;
};

struct LeavingPacket : public Packet
{
        
};

struct AssignIdPacket : public Packet
{
    
};

struct ScorePacket : public Packet
{
    int score;
};

struct SwitchPacket : public Packet
{
    
}; struct Ping : public Packet
{
  
};

struct CollisionHit
{
    int id;
    ObjectType type{ ObjectType::Wall };
};

typedef std::vector<std::unique_ptr<GameObject>> clientList;

bool Intersects(float ax, float ay, float aw, float ah,
    float bx, float by, float bw, float bh);

bool CheckCollision(const GameObject& mover,
    const clientList& clients,
    float proposedX,
    float proposedY,
    CollisionHit& outHit);