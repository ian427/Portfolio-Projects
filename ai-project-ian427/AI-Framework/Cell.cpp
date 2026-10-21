#include "Cell.h"
#include "Chunk.h"

void Cell::Initialize()
{
	// load the texture into memory
	texture.loadFromFile(filename);
	texture.setSmooth(true);
	// and provide the sprite with a pointer to the texture object
	// if the texture object is destroyed (goes out of scope etc) then the sprite will display weirdly
	sprite.setTexture(texture);
	// set up our colour tint
	sprite.setColor(colourTint);

	sprite.setOrigin(texture.getSize().x / 2, texture.getSize().y / 2);

	Renderables.push_back(this);
}

Cell* Cell::GetNeighbour(Neighbourdirection directrion)
{
	Cell* temp = Neighbours[directrion];
	return (temp);
}

void Cell::CalculateHvalue(Cell* End)
{	
	H = (End->parent->_x * End->parent->_x) + (parent->_y * parent->_y);
}
