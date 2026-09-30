#include "Shader.h"
#include "Lights.h"
#include <iostream>
static void CheckShaderError(GLuint shader, GLuint flag, bool isProgram, const string &errorMessage)
{
	GLint success = 0;
	GLchar error[1024] = { 0 };
	if (isProgram)
		glGetProgramiv(shader, flag, &success );
	else
		glGetShaderiv(shader, flag, &success );

	if (success == GL_FALSE)
	{
		if (isProgram)
			glGetProgramInfoLog(shader, sizeof(error), NULL, error);
		else
			glGetShaderInfoLog(shader, sizeof(error), NULL, error);
		cerr << errorMessage << ":'" << error << "'" << endl;


	}
}
static GLuint CreateShader(const string& ShaderSource, GLenum shaderType)
{

	GLuint shader =  glCreateShader(shaderType);
	if (shader == 0)
	{

		cerr << "Error : shader creation failed" << endl;
	}
	//need to convert from string to char for shader
	const char* TempSourceCode = ShaderSource.c_str();

	glShaderSource(shader, 1, &TempSourceCode, NULL);
	glCompileShader(shader);

	//CheckShaderError(shader, GL_LINK_STATUS, false, "ERROR Shader Compilation failed");
	return shader;

}
static string LoadShader(const string& fileName)
{
	std::ifstream file;
	const char* FileNameChar = fileName.c_str();
	file.open(FileNameChar, ifstream::in);

	string output;
	string line;

	if (file.is_open())
	{

		while (file.good())
		{
			getline(file, line);
			output.append(line + "\n");


		}
	}
	else
	{
		cerr << "Error : unable to load shader"<< fileName << endl;

	}
	return output;


}
Shader::Shader(const string FileLocation, Camera & camera)
{
		m_Camera = &camera;
		Name = FileLocation;
		m_Program = glCreateProgram();//debug shader refrence

		m_Shaders[0] = CreateShader(LoadShader(FileLocation+".vert"), GL_VERTEX_SHADER);
		m_Shaders[1] = CreateShader(LoadShader(FileLocation+".frag"), GL_FRAGMENT_SHADER);

		for (GLuint i = 0; i < NUM_SHADER_TYPES; i++)
		{
			glAttachShader(m_Program, m_Shaders[i]);

		}
		glLinkProgram(m_Program);
		CheckShaderError(m_Program, GL_LINK_STATUS, true, "ERROR :Program Linking failed");
		glLinkProgram(m_Program);
		CheckShaderError(m_Program, GL_VALIDATE_STATUS, true, "ERROR : Program is invalid");

		//Shader(FileLocation + "frag"), GL_FRAGMENT_SHADER);

		 m_Uniforms[MODEL_U] = glGetUniformLocation(m_Program, "model");

		 m_Uniforms[PROJECTION_U] = glGetUniformLocation(m_Program, "projection");

		 m_Uniforms[VIEW_U] = glGetUniformLocation(m_Program, "view");

		 m_Uniforms[FRAG_CAMPOS_U] = glGetUniformLocation(m_Program, "ViewPos");

		 m_Uniforms[FRAG_LIGHTCOLOR_U] = glGetUniformLocation(m_Program, "LightColor");

		 m_Uniforms[FRAG_LIGHTPOS_U] = glGetUniformLocation(m_Program, "LightPos");

		for (GLuint i = 0; i < NUM_UNIFORMS; i++)
		{
			if (m_Uniforms[i] == GL_INVALID_INDEX)
			{
				cout << "Shader" << Name << "Uniform invalid index" << static_cast<UnifromNames>(i) << "(might be optimised out if not used)" << endl;


			}

		}


}
Shader::~Shader()
{
	for (unsigned int i = 0; i < NUM_SHADER_TYPES; i++)
	{
		glDetachShader(m_Program, m_Shaders[i]);
		glDeleteShader(m_Shaders[i]);

	}
	glDeleteProgram(m_Program);
}
void Shader::Bind()
{
		glUseProgram(m_Program);
}

void Shader::Update(Transform& transform,LightBase& light ,mat4& LightSpaceMatrix)
{

	mat4 projection  = m_Camera->PerspetiveProjection;
	mat4 view = m_Camera->ViewMatrix();
	mat4 model = transform.GetModel();
	glUniformMatrix4fv(glGetUniformLocation(m_Program, "lightSpaceMatrix"), 1, GL_FALSE, &LightSpaceMatrix[0][0]);

	glUniformMatrix4fv(m_Uniforms[MODEL_U], 1, GL_FALSE, &model[0][0]);
	glUniformMatrix4fv(m_Uniforms[PROJECTION_U], 1, GL_FALSE, &projection[0][0]);
	glUniformMatrix4fv(m_Uniforms[VIEW_U], 1, GL_FALSE, &view[0][0]);

	glUniform3f(m_Uniforms[FRAG_CAMPOS_U], m_Camera->m_CamTransform.GetPosition().x,
		m_Camera->m_CamTransform.GetPosition().y,
		m_Camera->m_CamTransform.GetPosition().z);

	glUniform3f(m_Uniforms[FRAG_LIGHTPOS_U], light.m_Transform.GetPosition().x,
		light.m_Transform.GetPosition().y,
		light.m_Transform.GetPosition().z);

	glUniform3f(m_Uniforms[FRAG_LIGHTCOLOR_U], light.M_Color.x,
		light.M_Color.y,
		light.M_Color.z);

	//glUseProgram(m_Program);
	//GLint modelLoc = glGetUniformLocation(ShaderPrograme, "model");//send to this shader
	//glUniformMatrix4fv(modelLoc, 1, GL_FALSE, &Tri1.m_Transform.GetModel()[0][0]); //get this model

	//GLint viewLoc = glGetUniformLocation(ShaderPrograme, "view");
	//glUniformMatrix4fv(viewLoc, 1, GL_FALSE, &MainCam.ViewMatrix()[0][0]);//camera locatione| location box

	//GLint PerspectiveLoc = glGetUniformLocation(ShaderPrograme, "perspective");
	//glUniformMatrix4fv(PerspectiveLoc, 1, GL_FALSE, &MainCam.PerspetiveProjection[0][0]);//camera locatione| location box
	////find view mat4 in the shader
	////find view mat4 in the shader
	////sned view mat from cam to shader

}

void Shader::UpdateShadows(Transform& transfrom, mat4& LightSpaceMatrix)
{
	glUniformMatrix4fv(glGetUniformLocation(m_Program, "model"), 1, GL_FALSE, &transfrom.GetModel()[0][0]);
	glUniformMatrix4fv(glGetUniformLocation(m_Program, "lighspacematrix"), 1, GL_FALSE, &LightSpaceMatrix[0][0]);
}
