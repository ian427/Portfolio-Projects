#pragma once
#include<glew.h>
#include<string>
#include<fstream>
#include "Camera.h"
#include "Transform.hpp"


class LightBase;

using namespace std;
enum UnifromNames
{
	MODEL_U ,
	PROJECTION_U,
	VIEW_U,
	FRAG_LIGHTCOLOR_U , //<<<
	FRAG_LIGHTPOS_U,
	FRAG_CAMPOS_U,//<<<
	NUM_UNIFORMS

};
enum ShaderTypes
{
	VERTEX,
	FRAGMENT,
	NUM_SHADER_TYPES

};
class Shader
{
public:
	Shader(const string FileLocation, Camera& camera);
	~Shader();

	void Update(Transform& transfrom, LightBase& light,mat4& LightSpaceMatrix);
	void UpdateShadows(Transform& transfrom, mat4& LightSpaceMatrix);
	void Bind();
	
	GLuint GetProgram()
	{
		return m_Program;
	}

private :
	string Name;
	GLuint m_Program;
	Camera* m_Camera;
	GLuint m_Shaders[NUM_SHADER_TYPES];
	GLuint m_Uniforms[NUM_UNIFORMS];
	
};
