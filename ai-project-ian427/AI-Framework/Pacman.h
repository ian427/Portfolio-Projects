#pragma once
#include "BaseEntity.h"
#include "Cell.h"
#include "Chunk.h"
enum class PacState {
	Idle,      // Waiting at start
	CHASING //following path
};
class PacMan : public BaseEntity
{
	//virtual void Think();//overide think
	void Initialize();
public:
	int ChunkPositionX, ChunkPositionY, CellPosX, CellPosY, PosInPath = 0;//current pos
	Cell* Position;
	PacState state = PacState::Idle;
	bool NeedPath = false;
	void Think();
	void Move();
	void UpdatePosition(Cell* Location);
	std::vector<Cell*> Path;
	PacMan(Cell*Location)
	{
		SetTextureFile("../Build/Assets/pacman.png");
		//set world tobe world grid
		CellPosX = Location->cellX;
		CellPosY = Location->cellY;
		ChunkPositionX = Location->parent->_x;
		ChunkPositionY = Location->parent->_y;
		Position = Location;
		Initialize();


	}
	~PacMan()
	{

	}


};
