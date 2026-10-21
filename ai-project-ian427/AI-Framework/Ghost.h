#pragma once
#include"BaseEntity.h"
#include "Cell.h"
#include "Chunk.h"
#include<vector>

class Grid;


enum class GhostState {
	Idle,      // Waiting at start
	Escaping,  // Moving away from Pac-Man
	Trapped    // No escape routes available
};

class Ghost : public BaseEntity
{

	
	void Initialize();
	
	public:
	virtual void Think();//overide think
	int ChunkPositionX, ChunkPositionY, CellPosX, CellPosY,PosInPath=0;//current pos
	Cell* Position;
	bool NeedPath = true;
	GhostState state = GhostState::Idle;
	void Move();
	void UpdatePosition(Cell* Location);
	std::vector<Cell*> Path;
	Ghost(Cell* Location)
	{
		//SetTextureFile("../Build/Assets/pacman.png");
		//set world tobe world grid
		SetTextureFile("../Build/Assets/Ghost.png");
		//set world tobe world grid
		CellPosX = Location->cellX;
		CellPosY = Location->cellY;
		ChunkPositionX = Location->parent->_x;
		ChunkPositionY = Location->parent->_y;
		Position = Location;
		Initialize();

	}
	~Ghost()
	{

	}

private:
	
	
};