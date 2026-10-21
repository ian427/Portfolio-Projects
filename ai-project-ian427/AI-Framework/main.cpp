#include <iostream>
#include <SFML/Graphics.hpp>

#include "BaseEntity.h"
#include "UtilRandom.h"
#include "Constants.h"
#include "World.h"
int main()
{
	// construct a window
	sf::RenderWindow window(sf::VideoMode(ScreenWidth, ScreenHeight), "AI Framework");

	// because we're running as a console application, we still get access to the console, so can use std::cout to print to it
	std::cout << "Constructed SFML Window" << std::endl;

	std::vector<BaseEntity*> entities;

	Grid* world = new Grid(20);
	int r = UtilRandom::instance()->GetRange(0, world->ValidCells.size() - 1);
	Cell* Target = world->ValidCells[r];
	Ghost* ghost = new Ghost(Target);
	
		r = UtilRandom::instance()->GetRange(0, world->ValidCells.size() - 1);
	Target = world->ValidCells[r];
	PacMan* pacman = new PacMan(Target);
	Target = world->CalculateGhostsTarget(pacman->Position, world->ValidCells);
	ghost->Path = world->CalculateAstarPath(ghost->Position, Target);
	pacman->Path = world->CalculateAstarPath(pacman->Position, Target);
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
					world->testPathing();
				}
			}
		}
		//GAMELOOP//
		if (ghost->NeedPath || pacman->NeedPath)
		{
			Target = world->CalculateGhostsTarget(pacman->Position, world->ValidCells);
			pacman->Path.clear();
			ghost->Path.clear();

			//disable to teleport
			pacman->PosInPath = 0;
			ghost->PosInPath = 0;
			pacman->UpdatePosition(pacman->Position);
			ghost->UpdatePosition(ghost->Position);
			//to here
			ghost->Path = world->CalculateAstarPath(ghost->Position, Target);
			pacman->Path = world->CalculateAstarPath(pacman->Position, Target);
			pacman->NeedPath = false;
			ghost->NeedPath = false;
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
			window.draw(entity->GetSprite(), entity->getTransform());
		}
		ghost->Think();
		pacman->Think();
		window.draw(ghost->GetSprite(), ghost->getTransform());
		window.draw(pacman->GetSprite(), pacman->getTransform());
		// swap the frame buffers
		window.display();
	}
	
	return 0;
}
