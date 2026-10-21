#pragma once

#include "BaseEntity.h"

class Chunk;
enum  Neighbourdirection
{
		 UP,
         RIGHT,
		 DOWN,
		 LEFT
};
class Cell : public BaseEntity
{
public:
	bool isSolid = true;
	int cellX, cellY , G, H;//cells position in chunk array
	float scale;

	bool isVisited{ false };

	int F() const { return G + H; }

	Cell* Neighbours[4]{nullptr};//|up|right|downZleft
	Cell* cellParent{ nullptr };
	Chunk* parent = nullptr;

	void Initialize() override;
	Cell* GetNeighbour(Neighbourdirection directrion);
	void CalculateHvalue(Cell* End);
	Cell()
	{
		cellX = 0;
		cellY = 0;
		scale = 0;
	}
	~Cell()
	{

	}
};