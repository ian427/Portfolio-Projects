#pragma once
/*
#include "Components.h"
#include "BitMaps.h"


#include "physics.h"
#include "imgui.h"
#include "SDL.h"
*/
//base class in vector with virtual methods each child of base overloads that method
//struct SDL_Renderer;
#include <string>
#include "MyEventTypes.hpp"
#include "EventHandler.h"
struct SDL_Renderer;
class Bitmap;
#include "physics.h"
class Collider;
class Transform;
class Event;

class ITreeNode
	
{
public:
	ITreeNode* Parent;
	std::vector<ITreeNode*>Children;

	void addToChildren(ITreeNode* newChild);
	
};




class BaseEntity : public ITreeNode
{
public:

	SDL_Renderer* m_Renderer;
	Physics phi;//copy
	float Radius;
	Bitmap* Map;
	Collider* BoxCollider;
	Transform transform;//26,27 getting this transforms data not flappys
	Transform& GetTransform() { return transform; }
	int ID;
	std::string ObjectName;
	//loading
	BaseEntity(SDL_Renderer* rendererin, float inRadius, std::string fileName, int xpos, int ypos, int inID, std::string& instri);

		//constructing
		BaseEntity(SDL_Renderer* renderer, std::string fileName, int xpos, int ypos, bool useTransparancy, std::string Me);
	
		void DrawUIChildren();


		void UIDraw(); //editor ui
	

		bool CheckBounds(int x, int y);
	virtual void Draw();//genral class ?

	Collider* GetCollider();
	
	float GetRadius();
	

	virtual void ApplyContinuousMoment();
	~BaseEntity();
};



class Bird : public I_EventHandeler, public virtual BaseEntity
{
public:

	Bird(SDL_Renderer* renderer, int X, int Y);


	~Bird();


	void ApplyContinuousMoment();
	void OnEvent(MyEventTypes eventType, Event* data);
	bool CollisionCheck(BaseEntity* CollidingAgainst);
};
class Pipe : public virtual BaseEntity
{

	bool inuse = false; //spawning
public:
	bool topPipe = true;
	
	Pipe(bool value, SDL_Renderer* renderer, int X, int Y);
	
	~Pipe();
	void ApplyContinuousMoment();
};



class Ground : public virtual BaseEntity
{
public:
	
	Ground(SDL_Renderer* renderer, int X, int Y);
	~Ground();
	void ApplyContinuousMoment();
	
};
class UI : public virtual BaseEntity
{
public:
	UI(SDL_Renderer* renderer, int X, int Y);
	
	~UI();
	void ApplyContinuousMoment();
};
class Trigger :public virtual BaseEntity
{

public:
	bool isTrigger = true;
	
	Trigger(SDL_Renderer* renderer, int X, int Y);
	
	~Trigger();
	
	void ApplyContinuousMoment();
	

};

class Seed :public virtual BaseEntity
{

public:
	Seed(SDL_Renderer* renderer, int X, int Y);
	~Seed();
	void ApplyContinuousMoment();

};

