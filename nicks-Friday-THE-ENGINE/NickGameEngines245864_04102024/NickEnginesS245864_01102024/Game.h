#ifndef GAME_H
#define GAME_H
#include "SDL_ttf.h"
#include <vector>
#include "physics.h"
#include "EventHandler.h"
#include "imgui.h"
#include <queue>
#include "Profiler.h" 

class Bitmap;
struct SDL_Window;
struct SDL_Renderer;

class Bird;
class Pipe;
class UI;
class Ground;
class BaseEntity;

class Seed;

class Game
{
private:
	SDL_Renderer* m_Renderer;
	SDL_Window *m_Window;
	//SDL_Renderer* m_Renderer;
		bool m_running;
		int selectedFrame;
		//bitmaps
		Bitmap* m_monster;
		Bitmap* m_monsterTrans;
		Bitmap* m_monsterTransKeyed;

		Bitmap* AssetMousDrag;
		
		Bird* Flappy;
		Pipe* TopPipe1;
		Pipe* BottomPipe1;
		
		Seed* seed;
		BaseEntity* sceneRoot;///root scene object
		std::vector<Seed*>Collectables;
		std::vector<Pipe*>Pipes;
		std::vector<BaseEntity*>Entitys;
		std::vector<BaseEntity*>SavedObjects;/// to show in editor test and demo only
		std::vector<Bitmap*>content;///to be shown in to drag in
		vector<BaseEntity*> SceanHierarchy;///added obj
		UI* Celling;
		ImGuiIO* io;
		Ground* Floor;
		EventSystem* eventSystem;
		ProfilerSystem* profilersystem;
		Physics* phi;
		
		int score = 0;
		bool isInPlay = false;
		std::string scoretxt = "Test Number: ";
		FrameTimeStack* Framtimstack;

		TTF_Font* m_pSmallFont;
		TTF_Font* m_pBigFont;
		//list of eventcalls
		
		int currentScene = 0;///what scene the editior/game is on
		///list of Scenes
		enum Scenes
		{
			TESTSCENE,
			GAME,
			GAMEOVER

		};
		
		
public:
	BaseEntity* SelectedObject;
	int screenWidth = 2000;///sets screen width
	
	Game(EventSystem* eventSytem);///game object that creates and runs the game
	~Game();
	void SetDisplayColour(int r, int g, int b, int a);///sets ups display colour

	void UpdateText(std::string msg, int x, int y, TTF_Font* font, SDL_Colour colour);///updates the text

	void Update(void);///game Update Loop

};
#endif

