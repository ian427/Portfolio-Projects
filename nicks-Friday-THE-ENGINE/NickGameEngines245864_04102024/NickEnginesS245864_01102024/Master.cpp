
#include <SDL.h>
#include <iostream>
#include <cmath>
#include "Game.h"
#include "Input.h"
#include "Debug.h"//|1|



#include "EventHandler.h"
#include "Entity2.hpp"

#undef main 

using namespace std;


int main(int argc, char* argv[])
{	
	//DeBug::Log()->printDebug(DeBug::Verbosity::note, "entered Main , %d",  1);

	//Bird TestBird;//
	
	EventSystem* eventSystem = new EventSystem();
	
	Game* game = new Game(eventSystem); // creates new game
	Input* input = new Input(eventSystem);
	
	if (game && input)
	{
		Uint8 r = 5, g = 0 ,b = 140, a = 255;// background colour
		
		while (!input->KeyIsPressed(KEY_ESCAPE))
			
		{
			input->Update();
			//DeBug::Log()->printDebug(DeBug::Verbosity::red, "looping %s, %d", 0);
			if (input->KeyIsPressed(KEY_R))
			{
				r++;
					if (r++ > 255) r = 0;
					//DeBug::Log()->printDebug(DeBug::Verbosity::note, "Red pressed %s, %d",  1);
			}
			if (input->KeyIsPressed(KEY_G))
			{
				g++;
				if (g++ > 255)g = 0;

			}
			if (input->KeyIsPressed(KEY_B))
			{
				b++;
				if (b++ > 255) b = 0;

			}

			game->SetDisplayColour(r, g, b, a);//show display
			game->Update();
		}
		//destructors

	}
	

	delete input;
	input = nullptr;

    delete game;
    game = nullptr;

	delete eventSystem;
	eventSystem = nullptr;

	return 0 ;
	
}
