#pragma once
#include "Transform.hpp"
//#include "physics.h"

#include "SDL.h"
//struct SDL_Rect;
//class SDL_Renderer;

//give new box collider .... this is a factory method, look it up
class Commponent //should this be an interface
{
	virtual void Update() =0;
};


 class Collider : public Commponent
{
private:

public:
	SDL_Rect CollisionRect;
	
	Collider(const Transform& t, int width, int height);
	

	void Update(){}
	

	void Update(const Transform& t)
	{
			UpdateColliderPosition(t);
			
	}
	void UpdateColliderPosition(const Transform& t);
	

	void DrawDebug(const Transform& transform, SDL_Renderer* m_Renderer);
};
