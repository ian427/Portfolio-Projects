#include <iostream>
#include <SFML/Graphics.hpp>

#include "../headers/BaseEntity.h"
#include "../headers/UtilRandom.h"
#include "../headers/Constants.h"

#include "../headers/Boid.h"
#include "../headers/Grid.h"

int main()
{
	// construct a window
	sf::RenderWindow window(sf::VideoMode(ScreenWidth, ScreenHeight), "AI Framework");

	// because we're running as a console application, we still get access to the console, so can use std::cout to print to it
	std::cout << "Constructed SFML Window" << std::endl;

	std::vector<BaseEntity*> entities;

	Grid grid;

	grid.generate_dungeon();

	sf::Sprite openCell;
	sf::Sprite closedCell;

	sf::Texture openCellTex;
	sf::Texture closedCellTex;

	openCellTex.loadFromFile("assets\\groundTile.png");
	closedCellTex.loadFromFile("assets\\solidTile.png");

	openCell.setTexture(openCellTex);
	closedCell.setTexture(closedCellTex);

	for (int i = 0; i < MaxBoids; ++i)
	{
		entities.push_back(new Boid());
		entities[i]->Initialize();
	}

	// our game loop
	while (window.isOpen())
	{
		// poll for events
		sf::Event event;
		while (window.pollEvent(event))
		{
			if (event.type == sf::Event::Closed)
				window.close();

			if (event.type == sf::Event::KeyReleased)
			{
				if (event.key.code == sf::Keyboard::Space)
				{
					for (BaseEntity* agent : BaseEntity::Renderables)
					{
						agent->RandomisePosition();
					}

					grid.generate_dungeon();
				}
			}
		}

		// clear the window before drawing anything to it
		window.clear();
		
		// update all our entities
		for ( auto entity : BaseEntity::Renderables )
		{
			entity->Think();
		}

		for (auto entity : BaseEntity::Renderables)
		{
			entity->Update();
		}

		// and then draw them
		for (auto entity : BaseEntity::Renderables)
		{			
			window.draw(*entity);
		}

		grid.draw_grid(window, openCell, closedCell);

		// swap the frame buffers
		window.display();
	}

	return 0;
}
