#include "ObjLoader.h"

vector<Vertex> OBJLoader::LoadOBJ(const string& FolderLoc , const string& FileName, string& AmbiantLoc, string& DiffLoc, string& SpecLoc, string& NormalLoc, vector<uint32_t>& indicies)
{
	string line, MatLibName, MaterialOfMesh, MeshName;
	vector<glm::vec3>VertPositions;
	vector<glm::vec3>VertNormals;
	vector<glm::vec3>VertTextureCoords;
	vector<Vertex>FinalVerts;

	std::ifstream file;
	string FileLoc = FolderLoc + "/" + FileName;
	const char* fileNameChar = FileLoc.c_str();
	file.open(fileNameChar, ifstream::in);
	if (!file.is_open())
	{
		cerr << "unable to open file" << FolderLoc + "/" + FileName << endl;
	}
	else
	{
		while (file.good())
		{

			getline(file, line);
			if (line[0] != '#')
			{
				string FirstWord = line.substr(0, line.find(' '));
				if (FirstWord == "mtllib")
				{
					MatLibName = line.substr(line.find(' ') + 1, line.find('\n'));

				}
				else if (FirstWord == "o")
				{
					MeshName = line.substr(line.find(' '), line.find('\n'));
				}
				else if (FirstWord == "v")
				{
					string VertValues = line.substr(line.find(' ') , line.find('\n'));
					float x, y, z;
					sscanf_s(VertValues.c_str(), "%f %f %f", &x, &y, &z);
					VertPositions.push_back(glm::vec3(x, y, z));
				}
				else if (FirstWord == "vn")
				{
					string VertNormalValues = line.substr(line.find(' '), line.find('\n'));
					float x, y, z;
					sscanf_s(VertNormalValues.c_str(), "%f %f %f", &x, &y, &z);
					VertNormals.push_back(glm::vec3(x, y, z));
				}
				else if (FirstWord == "vt")// vert texture coords
				{
					string VertTexValues = line.substr(line.find(' '), line.find('\n'));
					float x, y, z;
					sscanf_s(VertTexValues.c_str(), "%f %f %f", &x, &y, &z);
					VertTextureCoords.push_back(glm::vec3(x, y, z));
				}
				if (FirstWord == "usemtl")
				{
					MaterialOfMesh = line.substr(line.find(' ') + 1, line.find('\n'));
					LoadMaterial(FolderLoc +"/" + MatLibName, AmbiantLoc, DiffLoc, SpecLoc, NormalLoc);

				}
				else if (FirstWord == "f")//geometry face list
				{
					string FaceValues = line.substr(line.find(' '), line.find('\n'));
					Vertex vertsInFace[3];
					unsigned int TmpPosition[3], TmpTexCords[3], TmpNormals[3];
					sscanf_s(FaceValues.c_str(), "%d/%d/%d %d/%d/%d %d/%d/%d",
						&TmpPosition[0], &TmpTexCords[0], &TmpNormals[0],
						&TmpPosition[1], &TmpTexCords[1], &TmpNormals[1],
						&TmpPosition[2], &TmpTexCords[2], &TmpNormals[2]
						);

					vertsInFace[0].Position = VertPositions[TmpPosition[0] - 1];
					vertsInFace[0].TextureCords = VertTextureCoords[TmpTexCords[0] - 1];
					vertsInFace[0].normal = VertNormals[TmpNormals[0] - 1];

					vertsInFace[1].Position = VertPositions[TmpPosition[1] - 1];
					vertsInFace[1].TextureCords = VertTextureCoords[TmpTexCords[1] - 1];
					vertsInFace[1].normal = VertNormals[TmpNormals[1] - 1];

					vertsInFace[2].Position = VertPositions[TmpPosition[2] - 1];
					vertsInFace[2].TextureCords = VertTextureCoords[TmpTexCords[2] - 1];
					vertsInFace[2].normal = VertNormals[TmpNormals[2] - 1];

					FinalVerts.push_back(vertsInFace[0]);
					FinalVerts.push_back(vertsInFace[1]);
					FinalVerts.push_back(vertsInFace[2]);

				}
				
				
			}
			
		}

	}
	for (int i = 0; i < FinalVerts.size(); i++)
	{
		indicies.push_back(i);
	}
	return FinalVerts;
}

void OBJLoader::LoadMaterial(const string& MatLibLoc, string& AmbiantLoc, string& DiffLoc, string& SpecLoc, string& NormalLoc)
{
	std::ifstream file;
	const char* FileNameChar = MatLibLoc.c_str();
	file.open(FileNameChar, ifstream::in);
	string line;
	string MatName;
	if (file.is_open())
	{
		while (file.good())
		{
			getline(file, line);
			if (line[0] != '#')
			{
				string FirstWord = line.substr(0, line.find(' '));
				if (strstr(FirstWord.c_str(), "newmtl"))
				{
					MatName = line.substr(line.find(' ') + 1, line.find('\n'));

				}
				else if (strstr(FirstWord.c_str(), "map_Ka"))
				{
					AmbiantLoc = line.substr(line.find(' ') + 1, line.find('\n'));

				}
				else if (strstr(FirstWord.c_str(), "map_Kd"))
				{
					DiffLoc = line.substr(line.find(' ') + 1, line.find('\n'));

				}
				else if (strstr(FirstWord.c_str(), "map_Ks"))
				{
					SpecLoc = line.substr(line.find(' ') + 1, line.find('\n'));

				}
				else if (strstr(FirstWord.c_str(), "map_bump"))
				{
					NormalLoc = line.substr(line.find(' ') + 1, line.find('\n'));

				}

			}
			
		
		}
		file.close();

	}
	else
	{
		cerr << "unable to load text file" << MatLibLoc << endl;
	}
}
