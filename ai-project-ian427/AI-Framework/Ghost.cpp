#include <cmath>

#include "Ghost.h"


void Ghost::Think()
{
    
    switch (state) {
    case GhostState::Idle:
       
        if (PosInPath == Path.size())//if reached end of path
        {
            NeedPath = true;
        }
        else
        {
            state = GhostState::Escaping;
        }
       
        break;

    case GhostState::Escaping:
        PosInPath++;
        if (PosInPath+1 > Path.size())
        {
            NeedPath = true;
            PosInPath = 0;
            Path.clear();
            state = GhostState::Idle;
        }
        else//moveing
        {
            NeedPath = false;
            Move();
        }
      
        break;

    case GhostState::Trapped:
        // Stay in place if no valid escape route
        break;
    }
    
}


void Ghost::Move()
{
   
    UpdatePosition(Path[PosInPath]);
}

void Ghost::UpdatePosition(Cell* Location)
{
    //set world tobe world grid
    CellPosX = Location->cellX;
    CellPosY = Location->cellY;
    ChunkPositionX = Location->parent->_x;
    ChunkPositionY = Location->parent->_y;
    Position = Location;
    Initialize();
  
}
void Ghost::Initialize()
{
    // load the texture into memory
    texture.loadFromFile(filename);
    texture.setSmooth(true);
    // and provide the sprite with a pointer to the texture object
    // if the texture object is destroyed (goes out of scope etc) then the sprite will display weirdly
    sprite.setTexture(texture);
    // set up our colour tint
    sprite.setColor(colourTint);

    sprite.setOrigin(texture.getSize().x /2.0f, texture.getSize().y / 2.0f);//not working
   int x = ((8 * CellPosX) + (24 * ChunkPositionX));
    int y = ((8 * CellPosY) + (24 * ChunkPositionY));
    // setPosition(((8 * CellPosX) + (24 * ChunkPositionX)), ((8 * CellPosY) + (24 * ChunkPositionY)));
    setPosition(x, y);
 
}


