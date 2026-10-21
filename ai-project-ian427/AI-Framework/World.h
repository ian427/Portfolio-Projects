#pragma once

#include "BaseEntity.h"
#include<vector>
#include "Pacman.h"
#include "Ghost.h"
#include "Chunk.h"
//how to tie paman to world
struct Grid
{
	int worldSize;

	std::vector<Chunk*> chuckArray;
	std::vector<Cell*> ValidCells;//not visited

	void testPathing();

	Cell* tempStart{ nullptr };
	Cell* tempEnd{ nullptr };

	void SetNeighbours(Cell& cell);
	Cell* CalculateGhostsTarget(Cell* Pacmanpos, std::vector<Cell*> ValidCells);
	void Movelement(Cell* toMove, std::vector<Cell*> &From, std::vector<Cell*> &To);
	bool Findelement(Cell* tofind, std::vector<Cell*> &in);
	std::vector<Cell*> CalculateAstarPath(Cell* Start, Cell* Target);
	//Chunk World[7][7];
	Grid(int size)
	{
		worldSize = size;
		
		chuckArray = std::vector<Chunk*>(worldSize * worldSize);
		Chunk* temp = chuckArray[0 + 0 * worldSize];

		int cellSize = 8;
		int chunkSize = cellSize * Chunk::chunksize;

		for (int x = 0; x < worldSize; x++)//i++pass in the increment//++i increment then pass
		{
			for (int y = 0; y < worldSize; y++)
			{
				// deciding what chunk to load
				if (y==0||x==0||y==worldSize-1||x==worldSize-1)
				{
					//edge
					chuckArray[x + y * worldSize] = new EdgeChunk(x, y, cellSize, this);

				}
				else if (y == 1 || x == 1 || y == worldSize-2 || x == worldSize-2)
				{
					//clear
					chuckArray[x + y * worldSize] = new ClearChunk(x, y, cellSize, this);
					temp = chuckArray[x + y * worldSize];
					for (int x = 0; x < 3; x++)//i++pass in the increment//++i increment then pass
					{
						for (int y = 0; y < 3; y++)
						{
							if (temp->chunk[x][y].isSolid == false)
							{
								ValidCells.push_back(&temp->chunk[x][y]);

							}

						}
					}
				}
				else
				{
					//middel
					chuckArray[x + y * worldSize] = new BasicChunk(x, y , cellSize, this);
					//chuckArray[x + y * worldSize] = new ClearChunk(x, y, cellSize, this);
					temp = chuckArray[x + y * worldSize];
					for (int x = 0; x < 3; x++)//i++pass in the increment//++i increment then pass
					{
						for (int y = 0; y < 3; y++)
						{
							if (temp->chunk[x][y].isSolid == false)
							{
								ValidCells.push_back(&temp->chunk[x][y]);

							}

						}
					}

				}

			}
		}

		// findNeighbours();
		for (int x = 0; x < worldSize; x++)//for all chunks 
		{
			for (int y = 0; y < worldSize; y++)
			{
				temp = chuckArray[x + y * worldSize];//tempory chunk
				//if(temp!= EdgeChunk)
				for (int ax = 0; ax < Chunk::chunksize; ax++)
				{
					for (int ay = 0; ay < Chunk::chunksize; ay++)
					{
						if (temp->chunk[ax][ay].isSolid == false)//is cell valid
						{//pushbackneighbours
							SetNeighbours(temp->chunk[ax][ay]);
						}

					}

				}

			}
		}

		
		//r = rand() % (ValidCells.size() + 1);
		// tempcell = ValidCells[r];
		//setpacman pos
		//Ghost* ghost = new Ghost(tempcell);
		
		
		std::cout << "end of build world\n";
	}
};

