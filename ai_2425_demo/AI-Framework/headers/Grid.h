#pragma once

#include <iostream>
#include <array>

#include <random>

#include <SFML/Graphics.hpp>

struct Cell {
	int x{ 0 }, y{ 0 };
	bool isSolid{ true };
};

class Grid {
public:
	const static int GridSize = 15;

	Grid() {
		for (int y = 0; y < Grid::GridSize; ++y)
		{
			for (int x = 0; x < Grid::GridSize; ++x)
			{
				_grid[y][x] = Cell{ y, x };
			}
		}

		srand(time(0));
	}

	void print_grid();

	void generate_dungeon();

	void draw_grid(sf::RenderWindow& window, sf::Sprite& open, sf::Sprite& closed);

private:
	std::array<std::array<Cell, Grid::GridSize>, Grid::GridSize> _grid;
};