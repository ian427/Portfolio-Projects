#include "Physics.h"
#include "Debug.h" //|6|

#include "SDL.h"


void Physics::ApplyForces(Transform& transform)
{
    vec3 currentForce = vec3(0);

    currentForce = AccumulateForces(forces);

    //auto it;//purpose

    for (auto it = impulses.begin(); it != impulses.end(); it++)//modern for loop
    {
        currentForce += *it;
    }
    impulses.clear();
    
    //apply currentFocre to transform
    vec3 Pos = transform.GetPosition();
    transform.SetPosition(Pos + currentForce);
}

 void Physics::ContinuousMoment(Transform transform, vec3 Move)
{
    vec3 Pos = transform.GetPosition();
    Pos + Move;
    ApplyForces(transform);
     
}

vec3 Physics::AccumulateForces(const std::vector<vec3> foreces)
{
    vec3 Force = vec3(0);

    for (int i = 0; i < foreces.size(); i++)
    {
        Force += forces[i];
    }

    

	return Force;
}



Collider::Collider(const Transform& t, int width, int height)
{
    CollisionRect = SDL_Rect{ (int)t.GetPosition().x, (int)t.GetPosition().y, width, height };
}

void Collider::UpdateColliderPosition(const Transform& t)
{
    CollisionRect.x = t.GetPosition().x;
    CollisionRect.y = t.GetPosition().y;
    //vec3 Pos = t.GetPosition();
    //vec3 Scale = t.GetScale();
}

void Collider::DrawDebug(const Transform& transform, SDL_Renderer* m_Renderer)
{

    SDL_SetRenderDrawColor(m_Renderer, 255, 0, 0, 0);
    SDL_RenderDrawRect(m_Renderer, &CollisionRect);

    SDL_SetRenderDrawColor(m_Renderer, 0, 0, 255, 0);
}

bool Physics::AABBIntersection(Collider* Collider1 , Collider* Collider2)
{
   
    SDL_Rect resuslt;
    return SDL_IntersectRect(&Collider1->CollisionRect, &Collider2->CollisionRect, &resuslt); 

}

void Physics::UpdatePhysics()
{

}

void  Physics::Move(Transform& transform, vec3 movement)
{
    //set impulse apply movment
    
    impulses.push_back(movement);
    ApplyForces(transform);
}
/*
void Physics::CollisionCheck(BaseEntity Object, BaseEntity CollidingAgainst)
{


}*/




