#pragma 
#include <string>
#include "SDL_ttf.h"

struct SDL_Surface;
struct SDL_Texture;
struct SDL_Renderer;
//useing namespace std

class Bitmap
{
private:
	SDL_Surface* m_pbitmapSurface;
	SDL_Texture* m_pbitmapTexture;
	SDL_Renderer* m_pbitmapRenderer;
	

public:
	
	~Bitmap();
	Bitmap(SDL_Renderer* renderer, std::string fileName, int xpos, int ypos, bool useTransparancy = false);
	
	
	void draw(int m_x, int  m_y);
	/*int GetX();
	int GetY();
	void SetX(int temppos);
	void SetY(int temppos);
	int m_x, m_y , */
	
	std::string FileName;
	

	SDL_Texture* GetTextureRef()
	{
		return m_pbitmapTexture;
	}

	int GetWidth();
	int GetHeight();
	
};

