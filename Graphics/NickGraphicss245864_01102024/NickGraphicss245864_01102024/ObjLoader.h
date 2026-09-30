#pragma once
#ifndef OBJLOADER_H
#define OBJLOADER_H

#include<string>
#include<fstream>
#include<iostream>
#include<vector>
#include"glm.hpp"
#include "Vertex.h"
#include <map>
using namespace std;

class OBJLoader
{
public:
	static vector<Vertex> LoadOBJ(const string& FolderLoc,
		const string& FileLoc, string& AmbiantLoc, string& DiffLoc,
		string& SpecLoc, string& NormalLoc, vector<uint32_t>& indicies);

	static void LoadMaterial(const string& MatLibLoc, string& AmbiantLoc,
		string& DiffLoc, string& SpecLoc, string& NormalLoc);
};


#endif // !OBJLOADER_H
