#include "../headers/Grid.h"

void Grid::print_grid() {
	for (auto row : _grid)
	{
		for (auto cell : row)
		{
			std::cout << (cell.isSolid ? '#' : ' ');
		}
		std::cout << '\n';
	}
}

void Grid::generate_dungeon()
{
	int x = rand() % Grid::GridSize;
	int y = rand() % Grid::GridSize;

	if (x == 0) x++;
	if (x == Grid::GridSize - 1) x--;
	if (y == 0) y++;
	if (y == Grid::GridSize - 1) y--;

	for (int i = 0; i < Grid::GridSize; ++i)
	{
		for (int j = 0; j < Grid::GridSize; ++j)
		{
			_grid[i][j].isSolid = true;
		}
	}

	_grid[x][y].isSolid = false;
	int cellsConverted{ 0 };
	constexpr int MaxWalkableCells = (Grid::GridSize * Grid::GridSize) / 2;

	while (cellsConverted < MaxWalkableCells)
	{
		int direction = rand() % 4;
		switch (direction)
		{
		case 0: // up
			if (y > 1) 
				y--;
			break;
		case 1: // left
			if (x > 1)
				x--;
			break;
		case 2: // down
			if (y < Grid::GridSize - 2)
				y++;
			break;
		case 3: // right 
			if (x < Grid::GridSize - 2)
				x++;
			break;
		}

		if (_grid[x][y].isSolid)
		{
			_grid[x][y].isSolid = false;
			cellsConverted++;
		}
	}
}

void Grid::draw_grid(sf::RenderWindow& window, sf::Sprite& open, sf::Sprite& closed)
{
	int x = 0;
	int y = 0;

	for (auto row : _grid)
	{
		for (auto cell : row)
		{
			closed.setPosition(sf::Vector2f(x, y));
			open.setPosition(sf::Vector2f(x, y));
			window.draw(cell.isSolid ? closed : open);
			x += 16;
		}
		y += 16;
		x = 0;
	}
}
