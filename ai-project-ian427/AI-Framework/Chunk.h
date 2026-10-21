#pragma once
#include <SFML/Graphics.hpp>

#include "BaseEntity.h"
#include "Cell.h"

class Grid;

class Chunk
{
public:
	static const int chunksize = 3;
	Cell chunk[chunksize][chunksize];
	int _x, _y;//place in world
	void Draw()
	{


	}

	Chunk() : _x{ 0 }, _y{ 0 } {}

	Chunk(int _x, int _y, int cellSize, bool solid, Grid* world) : _x{_x}, _y{_y}
	{

		int chunkSize = cellSize * chunksize;
		for (int x = 0; x < 3; x++)//i++pas in the increment//++i increment then pass
		{
			for (int y = 0; y < 3; y++)
			{
				chunk[x][y].SetTextureFile("../Build/Assets/static.png");
				chunk[x][y].cellX = x;//set cell poition
				chunk[x][y].cellY = y;
				chunk[x][y].parent = this;
				chunk[x][y].setPosition(_x * chunkSize + x * cellSize, _y * chunkSize + y * cellSize);
				chunk[x][y].isSolid = solid;
				if (!solid) chunk[x][y].SetColour(sf::Color::Blue);
				chunk[x][y].Initialize();
			}
		}
	}
	~Chunk()
	{

	}

};
class EdgeChunk :public Chunk
{
public:

	EdgeChunk(int x, int y, int cellSize, Grid* world) : Chunk(x, y, cellSize, true, world) {}
	~EdgeChunk()
	{

	}

};
class ClearChunk :public Chunk
{
public:

	ClearChunk(int x, int y, int cellSize, Grid* world) : Chunk(x, y, cellSize, false, world) {}
	~ClearChunk()
	{

	}

};
class BasicChunk :public Chunk
{
public:
	int direction, cellsConverted, maxWalkCells, walkX, walkY;
	BasicChunk(int x, int y, int cellSize, Grid* world) : Chunk(x, y, cellSize, true, world)
	{
		maxWalkCells = 10;
		walkX = 1;//default to center
		walkY = 1;
		//case and switch is solid

		for (int i = 0; i < maxWalkCells; i++)
		{
			direction = rand() % 4;
			//std::cout << direction << ", ";
			switch (direction)
			{
			case 0://up
				if ((walkY > 0) && (walkY != 0))
				{
					walkY--;

					if (this->chunk[walkX][walkY].isSolid == true)
					{
						chunk[walkX][walkY].isSolid = false;
					}

				}
				break;
			case 1://righ
				if ((walkX > 2) && (walkX != 2))
				{
					walkX++;
					if (chunk[walkX][walkY].isSolid == true)
					{
						chunk[walkX][walkY].isSolid = false;
					}

				}
				break;
			case 2://down
				if ((walkY < 2) && (walkY != 2))
				{
					walkY++;
					if (this->chunk[walkX][walkY].isSolid == true)
					{
						chunk[walkX][walkY].isSolid = false;
					}

				}

				break;
			case 3://left
				if ((walkX > 0) && (walkX != 0))
					walkX--;
				if (chunk[walkX][walkY].isSolid == true)
				{
					chunk[walkX][walkY].isSolid = false;
				}

				break;


			}


		}

		std::cout << "\n";
		for (int x = 0; x < 3; x++)//i++pas in the increment//++i increment then pass
		{
			for (int y = 0; y < 3; y++)
			{
				chunk[x][y].isSolid = !chunk[x][y].isSolid;
				chunk[x][y].SetTextureFile("../Build/Assets/static.png");

				if (chunk[x][y].isSolid == false) chunk[x][y].SetColour(sf::Color::Blue);
				chunk[x][y].Initialize();
			}
		}



	}
	~BasicChunk()
	{

	}

};