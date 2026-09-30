#ifndef MESH_H
#define MESH_H
#include <glew.h>
#include <glm.hpp>
#include "Transform.hpp"
#include"Vertex.h"
using namespace std;
class Mesh
{
public:
enum
	
	{
		POSITION_VB,
		TEXCOORD_VB,
		NORMAL_VB,
		TANGENT_VB,
		BITANGENT_VB,
		INDEX_VB,
		NUM_BUFFERS
	};

	Mesh(Vertex* verts, unsigned int VertCount , unsigned int* indicies, unsigned int numIndices);
	void Draw( );
	~Mesh();
	int m_DrawCount;
	
	void CalculateTangentsBiTangents(Vertex* Verticies, unsigned int VertCount, unsigned int* Indecies, unsigned int NumIndicies);
	Transform m_Transform;
	GLuint m_vertexBufferObjects[NUM_BUFFERS];
	GLuint m_vertexArrayObject = 0;


	
	

};

#endif // !MESH_H