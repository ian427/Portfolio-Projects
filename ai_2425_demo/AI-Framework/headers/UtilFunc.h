#pragma once

#include <SFML/Graphics.hpp>

float distance(const sf::Vector2f& a, const sf::Vector2f& b)
{
	sf::Vector2f diff = a - b;
	return sqrt(diff.x * diff.x + diff.y * diff.y);
}

float magnitude(const sf::Vector2f& v)
{
	return sqrt(v.x * v.x + v.y * v.y);
}

sf::Vector2f normalize(const sf::Vector2f& v)
{
	float mag = magnitude(v);
	return v / mag;
}