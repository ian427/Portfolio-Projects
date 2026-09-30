#pragma once

#ifndef VERTEX_H
#define VERTEX_H
#include <glm.hpp>

using namespace glm;
struct Vertex
{
public :
	glm::vec3 Position;
	glm::vec2 TextureCords;
	glm::vec3 Tangent;
	glm::vec3 BiTangent;
	glm::vec3 normal;
	Vertex(const float X, const float Y, const float Z)
	{
		this->Position.x = X;
		this->Position.y = Y;
		this->Position.z = Z;

		TextureCords = { 0,0 };
		Tangent = vec3(0);
		BiTangent = vec3(0);
	}
	Vertex(const vec3 position) :Vertex(position.x, position.y, position.z)
	{

	}
	Vertex(const vec3 position, const vec2 texcords)
	{
		this->Position = position;
		this->TextureCords = texcords;

	}
	Vertex(const Vertex& vert)
	{
		this->Position.x = vert.Position.x;
		this->Position.y = vert.Position.y;
		this->Position.z = vert.Position.z;

		TextureCords = { vert.TextureCords.x,vert.TextureCords.y };
		Tangent = glm::vec3(0);
		BiTangent = glm::vec3(0);
	}

	Vertex()
	{
		this->Position.x = 0;
		this->Position.y = 0;
		this->Position.z = 0;

		TextureCords = { 0,0 };
		Tangent = vec3(0);
		BiTangent = vec3(0);
	}
	
};

#endif


