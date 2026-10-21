#include "World.h"
#include "UtilRandom.h"
#include <queue>

void Grid::testPathing()
{
	if (tempStart)
	{
		tempStart->SetColour(sf::Color::Blue);
		tempStart->Initialize();
	}

	if (tempEnd)
	{
		tempEnd->SetColour(sf::Color::Blue);
		tempEnd->Initialize();
	}

	//picks random cell
	int r = UtilRandom::instance()->GetRange(0, ValidCells.size() - 1);
	tempStart = ValidCells[r];
	std::cout << "start cell for bfs test - index: " << r << "\n";
	//setpacman pos
	tempEnd = CalculateGhostsTarget(tempStart, ValidCells);
	
	tempStart->SetColour(sf::Color::Red);
	tempStart->Initialize();
	tempEnd->SetColour(sf::Color::Green);
	tempEnd->Initialize();

	CalculateAstarPath(tempStart, tempEnd);
}

void Grid::SetNeighbours(Cell& cell)
{
	Cell* Neighbour{ nullptr };
	int x, y;

	for (int i = 0; i < 4; i++)
		cell.Neighbours[i] = nullptr;

	// check for neighbours outside current chunk
	if ((cell.cellY == 0) && ((cell.parent->_y) > 0))
	{
		x = cell.parent->_x;
		y = cell.parent->_y-1;
		Chunk* temp = chuckArray[x + y * worldSize];	//getschunk
		Cell* tempcell = &temp->chunk[cell.cellX][Chunk::chunksize - 1];
		if (tempcell->isSolid == false)
		cell.Neighbours[Neighbourdirection::UP] = &temp->chunk[cell.cellX][Chunk::chunksize - 1];
	}
	if ((cell.cellX == Chunk::chunksize - 1) && ((cell.parent->_x) < worldSize - 1))
	{
		x = cell.parent->_x+1;
		y = cell.parent->_y;
		Chunk* temp = chuckArray[x + y * worldSize];	//getschunk	 
		Cell* tempcell = &temp->chunk[0][cell.cellY];
		if (tempcell->isSolid == false)
		cell.Neighbours[Neighbourdirection::RIGHT] = &temp->chunk[0][cell.cellY];
	}
	if ((cell.cellY == Chunk::chunksize - 1) && ((cell.parent->_y) < worldSize - 1))
	{
		x = cell.parent->_x;
		y = cell.parent->_y + 1;
		Chunk* temp = chuckArray[x + y * worldSize];	//getschunk
		Cell* tempcell = &temp->chunk[cell.cellX][0];
		if (tempcell->isSolid == false)
		cell.Neighbours[Neighbourdirection::DOWN] = &temp->chunk[cell.cellX][0];
	}
	if ((cell.cellX == 0) && ((cell.parent->_x) > 0))
	{
		x = cell.parent->_x - 1;
		y = cell.parent->_y;
		Chunk* temp = chuckArray[x + y * worldSize];	//getschunk	 
		Cell* tempcell = &temp->chunk[Chunk::chunksize - 1][cell.cellY];
		if (tempcell->isSolid == false)
		cell.Neighbours[Neighbourdirection::LEFT] = &temp->chunk[Chunk::chunksize-1][cell.cellY];
	}
	
	// check for neighbours internal to chunk
	if ((cell.cellY > 0))
	{
		x = cell.cellX;
		y = cell.cellY -1;
		Cell* tempcell = &cell.parent->chunk[x][y];
		if (tempcell->isSolid == false)
		cell.Neighbours[Neighbourdirection::UP] = &cell.parent->chunk[x][y];
	}
	if ((cell.cellX < Chunk::chunksize - 1))
	{
		x = cell.cellX + 1;
		y = cell.cellY;
		Cell* tempcell = &cell.parent->chunk[x][y];
		if (tempcell->isSolid == false)
		cell.Neighbours[Neighbourdirection::RIGHT] = &cell.parent->chunk[x][y];
	}
	if ((cell.cellY < Chunk::chunksize - 1))
	{
		x = cell.cellX;
		y = cell.cellY + 1;
		Cell* tempcell = &cell.parent->chunk[x][y];
		if (tempcell->isSolid == false)
		cell.Neighbours[Neighbourdirection::DOWN] = &cell.parent->chunk[x][y];
	}
	if ((cell.cellX > 0))
	{
		x = cell.cellX-1;
		y = cell.cellY;
		Cell* tempcell = &cell.parent->chunk[x][y];
		if (tempcell->isSolid == false)
		cell.Neighbours[Neighbourdirection::LEFT] = &cell.parent->chunk[x][y];
	}
}

Cell* Grid::CalculateGhostsTarget(Cell* Pacmanpos, std::vector<Cell*> ValidCells)//take in valid cells//flood search
{
	for (auto cell : ValidCells) {
		cell->SetColour(sf::Color::Blue);
		cell->Initialize();
		cell->G = INT_MAX;
		cell->isVisited = false;
	}

	// BFS

	// queue of cells to look at
	// enqueue start cell
	std::queue<Cell*> frontier;
	frontier.push(Pacmanpos);

	Pacmanpos->G = 0;
	Pacmanpos->isVisited = true;

	Cell* furthest = Pacmanpos;
	
	// while queue is not empty
	while (!frontier.empty())
	{
		//   current cell = dequeue
		Cell* current = frontier.front();
		frontier.pop();

		//    iterate current neighbours and add to queue
		for (int i = 0; i < 4; i++)
		{
			Cell* neighbour = current->Neighbours[i];

			if (neighbour == nullptr) continue;

			if (neighbour->isVisited) continue;

			neighbour->G = current->G + 1;
			neighbour->isVisited = true;
			frontier.push(neighbour);

			if (neighbour->G > furthest->G)
				furthest = neighbour;
		}
	}
	
	return furthest;	
}

void Grid::Movelement(Cell* toMove, std::vector<Cell*> &From, std::vector<Cell*> &To)
{
	for(int i = 0 ;i < From.size();i++)
	{ 
		if ((toMove->cellX == From[i]->cellX) && (toMove->cellY == From[i]->cellY) && (toMove->parent == From[i]->parent))
		{
			To.push_back(toMove);
			From.erase(From.begin() + i);
		}
	}
}

bool Grid::Findelement(Cell* tofind, std::vector<Cell*> &in)
{
	bool result = false;
		
			for (int i = 0; i < in.size(); i++)
			{
				if ((tofind = in[i]) && (result == false)) { result = true; }
			}

	return result;
}

std::vector<Cell*> Grid::CalculateAstarPath(Cell* Start, Cell* Target)
{
	
	/*
	* A*
	* 
	* BFS but guided by a heuristic
	* - uses a priority queue to find lowest cost cell
	* - cost calculated as G + H (G - cumulative cost, H is heuristic cost)
	*/

	for (auto cell : ValidCells) {
		cell->SetColour(sf::Color::Blue);
		cell->Initialize();
		cell->G = INT_MAX;
		cell->H = 0;
		cell->isVisited = false;
	}

	auto cmp = [](const Cell* a, const Cell* b) {return a->F() > b->F(); };
	std::priority_queue<Cell*, std::vector<Cell*>, decltype(cmp)> frontier(cmp);

	Start->G = 0;
	Start->H = 0;
	Start->cellParent = nullptr;
	Start->isVisited = true;

	frontier.push(Start);

	bool found{ false };

	// while queue is not empty
	while (!frontier.empty())
	{
		//   current cell = dequeue
		Cell* current = frontier.top();
		frontier.pop();

		if (current == Target)
		{
			found = true;
			break;
		}

		//    iterate current neighbours and add to queue
		for (int i = 0; i < 4; i++)
		{
			Cell* neighbour = current->Neighbours[i];

			int G = current->G + 1;

			if (neighbour == nullptr || G > neighbour->G) continue;

			neighbour->G = G;
			neighbour->cellParent = current;
			neighbour->CalculateHvalue(Target);
			
			if (!neighbour->isVisited)
			{
				neighbour->isVisited = true;
				frontier.push(neighbour);
			}
		}
	}


	if (!found) return std::vector<Cell*>();

	std::vector<Cell*> Path;

	Path.push_back(Target);

	Cell* current = Target->cellParent;

	while (current)
	{
		current->SetColour(sf::Color::Magenta);
		current->Initialize();
		Path.push_back(current);
		current = current->cellParent;
	}

	std::reverse(Path.begin(), Path.end());

	return Path;
}
