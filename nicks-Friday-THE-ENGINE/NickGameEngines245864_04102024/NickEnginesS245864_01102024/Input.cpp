#include "input.h"
#include "Debug.h "//|3|
#include "backends/imgui_impl_sdl.h"

void Input::Update(void)
{
	class EventSystem;
	while (SDL_PollEvent(&m_event) != NULL)
	{
		//ImGui_ImplSDL2_ProcessEvent(&e);
		//check keydown
		ImGui_ImplSDL2_ProcessEvent(&m_event);

		if (m_event.type == SDL_KEYDOWN)
		{

			
			//cache code for keypress
			SDL_Keycode keyPressed = m_event.key.keysym.sym;
			
			//update all keys extend
			switch (keyPressed)
			{

			case SDLK_ESCAPE:
				m_keysPressed[KEY_ESCAPE] = true;
				DeBug::Log()->printDebug(DeBug::Verbosity::note, "Shuting Down %d", 3);
				break;
			case SDLK_r:
				m_keysPressed[KEY_R] = true;
				DeBug::Log()->printDebug(DeBug::Verbosity::note, "R key pressed %d", 3);
				break;
			case SDLK_g:
				m_keysPressed[KEY_G] = true;
				break;
			case SDLK_b:
				m_keysPressed[KEY_B] = true;
				break;
			case SDLK_SPACE:
				m_keysPressed[KEY_SPACE] = true;
				//AddEvent(MyEventTypes eventType, Event * data);
				break;
			}
		}
		// check for key up
		else if (m_event.type == SDL_KEYUP)
		{
			
			SDL_Keycode keyPressed = m_event.key.keysym.sym;

			switch (keyPressed)
			{

			case SDLK_ESCAPE:
				m_keysPressed[KEY_ESCAPE] = false;
				break;
			case SDLK_r:
				m_keysPressed[KEY_R] = false;
				break;
			case SDLK_g:
				m_keysPressed[KEY_G] = false;
				break;
			case SDLK_b:
				m_keysPressed[KEY_B] = false;
				
				break;
				break;
			case SDLK_SPACE:
				m_keysPressed[KEY_SPACE] = false;
				eventSystem->AddEvent(MyEventTypes::SPACE_PRESSED, new Event("test spoace pressed"));
				break;
			}

		}
    }
}
bool Input::KeyIsPressed(KEYS_PRESSED_LIST key)
{
	return m_keysPressed[key];
}
Input::Input(EventSystem* eventSystem)//CONSTRUCTOR
{
	this->eventSystem = eventSystem;
	for (int i = 0; i < SIZE_OF_KEYS_PRESSED_ENUM;i++)
	{
		m_keysPressed[i] = NULL;
	}
}
Input::~Input()//DESTRUCTOR
{
	eventSystem = nullptr;
}

