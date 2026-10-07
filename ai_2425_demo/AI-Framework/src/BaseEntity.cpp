#include "../headers/BaseEntity.h"
#include "../headers/UtilRandom.h"

std::vector<BaseEntity*> BaseEntity::Renderables;

BaseEntity::BaseEntity()
{
	// with no further information, we make some assumptions and set default values.
	filename = "assets\\circle.png";
	colourTint = sf::Color::White;
}

BaseEntity::BaseEntity(std::string file) : filename(file)
{
	// with just a texture file, we default to a white tint (so no tint at all).
	colourTint = sf::Color::White;
}

BaseEntity::BaseEntity(std::string file, sf::Color colour) : filename(file), colourTint(colour)
{
	
}

BaseEntity::~BaseEntity()
{
	
}

void BaseEntity::Think()
{
	// set acceleration, somehow
}

void BaseEntity::Update()
{
	velocity += acceleration;

	// build a new position vector by adding a scaled version of the velocity vector
	sf::Vector2f newPosition = getPosition() + (velocity * 0.1f);

	// wrap!
	if (newPosition.x > ScreenWidth)
		newPosition.x = 0.001f;
	if (newPosition.x < 0)
		newPosition.x = ScreenWidth;

	if (newPosition.y > ScreenHeight)
		newPosition.y = 0.001f;
	if (newPosition.y < 0)
		newPosition.y = ScreenHeight;

	// update our position
	setPosition(newPosition);

	acceleration *= 0.0f;
}

void BaseEntity::RandomisePosition()
{
	// set a random starting position.
	float startX = UtilRandom::instance()->GetRange(0, ScreenWidth);
	float startY = UtilRandom::instance()->GetRange(0, ScreenHeight);
	setPosition(startX, startY);
}

void BaseEntity::ResetVelocity()
{
		// select a random angle
	float angle = UtilRandom::instance()->GetRange(0, 361);
	// set our rotation value
	setRotation(angle);
	// and assign a velocity, we need to convert angle to radians so it plays nicely with cos and sin.
	velocity = sf::Vector2f((float)cos(angle * M_PI / 180), (float)sin(angle * M_PI / 180));
}

sf::Vector2f BaseEntity::getVelocity()
{
	return velocity;
}

void BaseEntity::applyForce(const sf::Vector2f& force)
{
	acceleration += force;
}

void BaseEntity::Initialize()
{
	RandomisePosition();

	// load the texture into memory
	texture.loadFromFile(filename);
	texture.setSmooth(true);
	// and provide the sprite with a pointer to the texture object
	// if the texture object is destroyed (goes out of scope etc) then the sprite will display weirdly
	setTexture(texture);
	// set up our colour tint
	setColor(colourTint);
	
	setOrigin(texture.getSize().x / 2, texture.getSize().y / 2);

	ResetVelocity();

	// add the entity to the list of renderables.
	Renderables.push_back(this);
}