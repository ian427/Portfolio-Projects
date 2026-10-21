#include "Pacman.h"

void PacMan::Think()
{
    switch (state) {
    case PacState::Idle:
        // If Pac-Man is close, start escaping
        if (PosInPath == Path.size())
        {
            NeedPath = true;
  
        }
        else
        {
            state = PacState::CHASING;
        }
       
        break;

    case PacState::CHASING:
        PosInPath++;
        if (PosInPath+1 > Path.size())
        {
            NeedPath = true;
            PosInPath = 0;
            Path.clear();
            state = PacState::Idle;
        }
        else
        {
            NeedPath = false;
            Move();
        }
        break;

    }
    

}

void PacMan::Move()
{
	
	UpdatePosition(Path[PosInPath]);
}

void PacMan::UpdatePosition(Cell* Location)
{
	//set world tobe world grid
	CellPosX = Location->cellX;
	CellPosY = Location->cellY;
	ChunkPositionX = Location->parent->_x;
	ChunkPositionY = Location->parent->_y;
	Position = Location;
    Initialize();
}

void PacMan::Initialize()
{
	// load the texture into memory
	texture.loadFromFile(filename);
	texture.setSmooth(true);
	// and provide the sprite with a pointer to the texture object
	// if the texture object is destroyed (goes out of scope etc) then the sprite will display weirdly
	sprite.setTexture(texture);
	// set up our colour tint
	sprite.setColor(colourTint);

    sprite.setOrigin(texture.getSize().x / 2.0f, texture.getSize().y / 2.0f);//not working
     int x = ((8 * CellPosX) + (24 * ChunkPositionX));
    int y = ((8 * CellPosY) + (24 * ChunkPositionY));
    // setPosition(((8 * CellPosX) + (24 * ChunkPositionX)), ((8 * CellPosY) + (24 * ChunkPositionY)));
    setPosition(x, y);
	
	
}

