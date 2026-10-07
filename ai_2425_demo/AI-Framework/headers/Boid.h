#pragma once

#include "BaseEntity.h"
#include <vector>

class Boid : public BaseEntity {
public:
	Boid() : BaseEntity("assets\\boid.png") {}

	void Think() override;

private:

	sf::Vector2f separation();
	sf::Vector2f alignment();
	sf::Vector2f cohesion();

	sf::Vector2f seek(const sf::Vector2f& target_position, float max_speed);

	float separationWeight{ 1.0f };
	float alignmentWeight{ 1.0f };
	float cohesionWeight{ 1.0f };

	float _neighbourhoodSize{ 75.0f };
};