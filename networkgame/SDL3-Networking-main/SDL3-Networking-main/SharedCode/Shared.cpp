#include "Shared.h"

bool CheckCollision(const GameObject& mover,
    const clientList& clients,
    float proposedX,
    float proposedY,
    CollisionHit& outHit)
{
    for (const auto& other : clients)
    {

        if (!other)
            continue;

        // Skip self
        if (other->id == mover.id)
            continue;


        if (Intersects(proposedX, proposedY, PLAYER_SIZE, PLAYER_SIZE,
            other->x, other->y, PLAYER_SIZE, PLAYER_SIZE))
        {
            outHit.id = other->id;
            outHit.type = other->type;
            return true;
        }
    }
    return false;
}

bool Intersects(float ax, float ay, float aw, float ah,
    float bx, float by, float bw, float bh)
{
    bool temp = false;

    if (ax < bx + bw && ax + aw > bx && ay < by + bh && ay + ah > by)
    {
        temp = true;
    }
    return temp;
}