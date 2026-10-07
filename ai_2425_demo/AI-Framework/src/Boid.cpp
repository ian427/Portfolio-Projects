#include "../headers/Boid.h"

#include "../headers/UtilFunc.h"

void Boid::Think()
{
	// calculate neighbourhood
	std::vector<Boid*> neighbours;

	for (auto entity : BaseEntity::Renderables)
	{
		if (entity == this)
			continue;

		float dist = distance(entity->getPosition(), getPosition());

		if (dist <= _neighbourhoodSize)
		{
			neighbours.push_back(static_cast<Boid*>(entity));
		}
	}

	sf::Vector2f sep = separation() * separationWeight;
	sf::Vector2f ali = alignment() * alignmentWeight;
	sf::Vector2f coh = cohesion() * cohesionWeight;

	applyForce(sep);
	applyForce(ali);
	applyForce(coh);
}

sf::Vector2f Boid::separation()
{
	return sf::Vector2f();
}

sf::Vector2f Boid::alignment()
{
	return sf::Vector2f();
}

sf::Vector2f Boid::cohesion()
{
	return sf::Vector2f();
}

sf::Vector2f Boid::seek(const sf::Vector2f& target_position, float max_speed)
{
	sf::Vector2f desired = normalize(target_position - getPosition()) * max_speed;

	return desired - velocity;
}
