#pragma once
#include <iostream>
#include <fstream>
//#include "Entity2.hpp"
#include <vector>



struct SDL_Renderer;
class BaseEntity;
class Bitmap;
class Physics;
class Collider;
class Transform;

class Save
{
public:
	static void saveObject(const BaseEntity& obj, const std::string& filename);
	static std::vector<BaseEntity> loadObjects(const std::string& filename, SDL_Renderer* renderer);
};