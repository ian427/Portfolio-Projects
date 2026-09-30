#define GLEW_STATIC
#define GLM_ENABLE_EXPERIMENTAL
#define STB_IMAGE_IMPLEMENTATION

#include <glew.h>//must be first

#include <SDL_opengl.h>
#include <glm.hpp>
#include <gtc/type_ptr.hpp>
#include <SDL.h>
#include <iostream>
#include <cmath>
#include "Mesh.h"
#include "Camera.h"
#include "math.h"
#include "Shader.h"
#include "Vertex.h"
#include <vector>
#include"stb_image.h"
#include "Lights.h"
#include"ObjLoader.h"
using namespace std;


enum TextureType
{
	texture_diffuse
};
TextureType texturetype;


GLuint LoadTexture(string TextureLocation)
{
	GLuint textureID;
	//should be its own class good for one texture
	int width, height, numComponents;
	unsigned char* ImageData = stbi_load(TextureLocation.c_str(), &width, &height, &numComponents, STBI_rgb_alpha);
	if (ImageData == NULL)
	{

		cerr << "texture loading failed for texture" << TextureLocation << endl;

	}
	GLenum format{};
	if (numComponents == 1)
		format = GL_RED;
	if (numComponents == 3)
		format = GL_RGB;
	if (numComponents == 4)
		format = GL_RGBA;
	glGenTextures(1, &textureID);
	glBindTexture(GL_TEXTURE_2D, textureID);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
	glTexImage2D(GL_TEXTURE_2D, 0, format, width, height, 0, GL_RGBA, GL_UNSIGNED_BYTE, ImageData);
	glBindTexture(GL_TEXTURE_2D, 0);
    
	
	stbi_image_free(ImageData);

	return textureID ;

}



int main(int argc, char* argv[])
{
	SDL_Event e;
	string AmbiantLoc, DiffuseLoc, SpecLoc, NormalLoc;
	vector<uint>Indicies;
	
	
	 /*float SquareVerticies[]
	{
		-0.5f, 0.5f ,0.0f,
		0.5f, 0.5f ,0.0f,
		-0.5f, -0.5f ,0.0f,
		0.5f, -0.5f ,0.0f,
	};*/
vector<Vertex>SquareVertices;
{
	SquareVertices.push_back(Vertex(vec3(-0.5f, 0.5f, 0.0f),vec2(0,0)));
	SquareVertices.push_back(Vertex(vec3(0.5f, 0.5f, 0.0f),vec2(1, 0)));
	SquareVertices.push_back(Vertex(vec3(-0.5f, -0.5f, 0.0f),vec2(1, 1)));
	SquareVertices.push_back(Vertex(vec3(0.5f, -0.5f, 0.0f),vec2(0, 1)));
}
unsigned int  SquareIndecies[]
{
	0,1,2,1,3,2
};
	//setup
	SDL_Init(SDL_INIT_EVERYTHING); 
	SDL_GL_SetAttribute(SDL_GL_RED_SIZE,8);//AMOUNT OF BITSFOR RED
	SDL_GL_SetAttribute(SDL_GL_GREEN_SIZE,8);
	SDL_GL_SetAttribute(SDL_GL_BLUE_SIZE,8);
	SDL_GL_SetAttribute(SDL_GL_ALPHA_SIZE,8);
	SDL_GL_SetAttribute(SDL_GL_BUFFER_SIZE,32);//total of rgba
	SDL_GL_SetAttribute(SDL_GL_DOUBLEBUFFER,1);
	SDL_GL_SetAttribute(SDL_GL_CONTEXT_PROFILE_MASK, SDL_GL_CONTEXT_PROFILE_CORE);
	SDL_GL_SetAttribute(SDL_GL_DEPTH_SIZE, 16);//DEPTH IS NOT 32 Bit /to do with shadows
	int WindowWidth=1600;
	int WindowHeight;
	WindowHeight = WindowWidth / 1.333f;
	//creating window
	SDL_Window* window = SDL_CreateWindow("window1", SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED, WindowWidth, WindowHeight,SDL_WINDOW_OPENGL| SDL_WINDOW_RESIZABLE);//0,0 is in the top left
	SDL_GLContext GLContext = SDL_GL_CreateContext(window);
	glewExperimental = GL_TRUE;
	GLenum status = glewInit();
	glEnable(GL_DEPTH_TEST);
	glDepthFunc(GL_LESS);
	//put in mesh func

    Mesh Tri1(&SquareVertices[0], SquareVertices.size(), &SquareIndecies[0], 6);//
	Camera MainCam(vec3(1, 1, 1),  (90 * (3.1415 / 180.0f)), (WindowWidth / WindowHeight), 0.1, 100);//degress turn into radian|scren width&hieght|distacetocam|distacefromCAM
	LightBase* light = new LightBase();
	light->m_Transform.SetPosition(vec3(0, 1, -1));
	Mesh Floor(&SquareVertices[0], SquareVertices.size(), &SquareIndecies[0], 6);
	Floor.m_Transform.SetScale(vec3(50,50,50));
	Floor.m_Transform.SetRotation(vec3(radians(90.0f),0.0f , 0.0f));
	Floor.m_Transform.SetPosition(vec3(0, 0, 0));

	////////////////////////////
	// Get the position of the triangle
	vec3 trianglePosition = Tri1.m_Transform.GetPosition();  // This gives you the position of Tri1

	// Get the forward direction from the camera's view matrix (we assume MainCam is defined)
	mat4 viewMatrix = MainCam.ViewMatrix();
	vec3 forwardDirection = normalize(vec3(viewMatrix[0][2], viewMatrix[1][2], viewMatrix[2][2]));

	// Calculate the light's position, offset in front of Tri1
	//vec3 lightPosition = trianglePosition + forwardDirection * 2.0f;  // Move 2 units in front of Tri1

	// Update the light's position using the setter
	//light->SetLightPosition(lightPosition);  // This will update the m_Transform position of the light

	// Optionally, draw the light or apply it in shaders
	//light->DrawLight(&MainCam);
	//////////////////////////
    /*const char* VertexShaderCode =
    "#version 450\n"
    "in vec3 vp;"
    "uniform mat4 model;"
    "uniform mat4 view;"
    "uniform mat4 perspective;"
    "void main (){"
    " gl_Position = model * vec4(vp,1.0);"
    "}";
    const char* FragmentShaderCode =
    "#version 450\n"
    "out vec4 frag_colour ;"
    "void main (){"
    " frag_colour = vec4(0.0,0.0,0.5,1.0);"
    "}";*/
	
	Shader* BasicShader = new Shader ("Basic", MainCam);
	Shader* DepthShader = new Shader("DepthShader", MainCam);
	GLuint  DiffuseTextureID = LoadTexture("Textures/brickwall.jpg");
	GLuint  NormalTextureID = LoadTexture("Textures/brickwall_normal.jpg");
	
	
	vector<Vertex>LoaderVerts = OBJLoader::LoadOBJ("Resource/Block", "blocks_01.obj",
		AmbiantLoc, DiffuseLoc, SpecLoc, NormalLoc, Indicies);
	GLuint AmbiantTextureID = LoadTexture("Resource/Block/" + AmbiantLoc);
	GLuint DiffiuseTextureID = LoadTexture("Resource/Block/" + DiffuseLoc);
	GLuint SpeculareTextureID = LoadTexture("Resource/Block/" + SpecLoc);
	GLuint BNormalTextureID = LoadTexture("Resource/Block/" + NormalLoc);

	Mesh* cube = new Mesh(LoaderVerts.data(), LoaderVerts.size(), Indicies.data(), Indicies.size());
	cube->m_Transform.SetScale(vec3(0.02));

	//drawloop
	float r = 1;
	float g = 1;
	float b = 1;
	float DeltaTime = 0;

	bool isRedIncreasing = true;
	bool isBlueIncreasing = true;
	bool isGreenIncreasing = true;
	bool GoRainbow = true;//switch to disable/enable
	bool CinSpin = false;
	vec3 MovementOffset;
	float MovmentSpeed = 0.1;
	float Rotation;

	GLuint depthMapFBO;
	glGenFramebuffers(1, &depthMapFBO);
	glBindFramebuffer(GL_FRAMEBUFFER, depthMapFBO);

	GLuint ShadowMapID;
	int ShadowWidth = 2048;
	int ShaowHeight = 2048;

	glGenTextures(1, &ShadowMapID);
	glBindTexture(GL_TEXTURE_2D, ShadowMapID);
	glTexImage2D(GL_TEXTURE_2D, 0, GL_DEPTH_COMPONENT, ShadowWidth, ShaowHeight, 0, GL_DEPTH_COMPONENT, GL_FLOAT, NULL);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_BORDER);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_BORDER);
	GLfloat BorderColor[] = { 1.0 ,1.0, 1.0, 1.0 };
	glTexParameterfv(GL_TEXTURE_2D, GL_TEXTURE_BORDER_COLOR, BorderColor);

	glFramebufferTexture2D(GL_FRAMEBUFFER, GL_DEPTH_ATTACHMENT, GL_TEXTURE_2D, ShadowMapID, 0);
	glDrawBuffer(GL_NONE);
	glReadBuffer(GL_NONE);

	if (glCheckFramebufferStatus(GL_FRAMEBUFFER) != GL_FRAMEBUFFER_COMPLETE)
		cerr << "ERROR fram buffer incomplete" << endl;

	glBindFramebuffer(GL_FRAMEBUFFER, 0);
	glBindTexture(GL_TEXTURE_2D, 0);


	glEnable(GL_CULL_FACE);


	while (true)
	{
		SDL_PollEvent(&e);

		if (e.type == SDL_MOUSEMOTION)
		{

			MainCam.mouseMoveTarget(&e);
		}
		switch (e.key.keysym.sym)
		{
		case(SDLK_w):
			MovementOffset = MainCam.GetForward();
			MainCam.m_CamTransform.SetPosition(vec3(MainCam.m_CamTransform.GetPosition() + MovementOffset * MovmentSpeed));
				break;
		case(SDLK_a):
			MovementOffset = MainCam.GetRight();
			MainCam.m_CamTransform.SetPosition(vec3(MainCam.m_CamTransform.GetPosition() +  MovementOffset * MovmentSpeed));
				break;
		case(SDLK_s):
			MovementOffset = MainCam.GetForward();
			MainCam.m_CamTransform.SetPosition(vec3(MainCam.m_CamTransform.GetPosition() - MovementOffset* MovmentSpeed));
				break;
		case(SDLK_d):
			MovementOffset = MainCam.GetRight();
			MainCam.m_CamTransform.SetPosition(vec3(MainCam.m_CamTransform.GetPosition() - MovementOffset * MovmentSpeed));
				break;
		case(SDLK_f):
			if (!CinSpin) 
			{
				MainCam.ToggleTarget();	
			}
			break;
		case(SDLK_RIGHT):
			vec3 tempRot = cube->m_Transform.GetRotation();
			cube->m_Transform.SetRotation(tempRot + vec3(0, 0.1, 0));
			break;
		case(SDLK_r):
			if (GoRainbow) 
			{
				GoRainbow = false;
			}
			else
			{
				GoRainbow = true;
			}
			break;
		case(SDLK_e):
			MovementOffset = MainCam.GetUp();
			MainCam.m_CamTransform.SetPosition(vec3(MainCam.m_CamTransform.GetPosition() + MovementOffset * MovmentSpeed));
			break;
		case(SDLK_c):
			MovementOffset = MainCam.GetUp();
			MainCam.m_CamTransform.SetPosition(vec3(MainCam.m_CamTransform.GetPosition() - MovementOffset * MovmentSpeed));
			break;
		case(SDLK_g):
			if (CinSpin)
			{
				CinSpin = false;
				MovmentSpeed = 0.1;
				if (MainCam.use_target == true)
				{
					MainCam.ToggleTarget();
				}
				
				
			}
			else
			{
				CinSpin = true;
				MovmentSpeed = 0.03;
				MainCam.ToggleTarget();
			}
			break;
		
		}
		if (CinSpin)
		{
			MovementOffset = MainCam.GetRight();
				MainCam.m_CamTransform.SetPosition(vec3(MainCam.m_CamTransform.GetPosition() + MovementOffset * MovmentSpeed));

		}
		//drawnewframe
		//changes background colour
		if (GoRainbow == true)
		{
			r = sin(DeltaTime);
			g = sin(DeltaTime + 2);
			b = sin(DeltaTime + 4);

		}
		

		glViewport(0, 0, ShadowWidth, ShaowHeight);
		glBindFramebuffer(GL_FRAMEBUFFER, depthMapFBO);
		glClear(GL_DEPTH_BUFFER_BIT);

		GLfloat near_plane = 1.0f, far_plane = 100.0f;
		mat4 LightProjection = ortho(-20.0f, 20.0f, -20.0f, 20.0f, near_plane, far_plane);
		mat4 lightview = lookAt(light->m_Transform.GetPosition(), vec3(0), vec3(0, 1, 0));
		mat4 lightSpaceMatrix = LightProjection * lightview;

		
		
		
		//cout << "r" << r<< ", g" << g<< ", b" << b<<endl;
		glCullFace(GL_FRONT);
		DepthShader->Bind();
		//DepthShader->UpdateShadows(Floor->m_Transform, lightSpaceMatrix);
		//Floor.Draw();
		DepthShader->UpdateShadows(cube->m_Transform, lightSpaceMatrix);
		cube->Draw();
		glCullFace(GL_BACK);



		glBindFramebuffer(GL_FRAMEBUFFER, 0);
		glClearColor(r, g, b, 0.1f);//leave
		glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
		glViewport(0, 0, WindowWidth, WindowHeight);//leave#

		BasicShader->Bind();
		//seting up textures
		glActiveTexture(GL_TEXTURE0);
		GLuint TextureLoc = glGetUniformLocation(BasicShader->GetProgram(), "texture_diffuse");
		glUniform1i(TextureLoc, 0);
		glBindTexture(GL_TEXTURE_2D, DiffuseTextureID);

		glActiveTexture(GL_TEXTURE1);
		TextureLoc = glGetUniformLocation(BasicShader->GetProgram(), "texture_normal");
		glUniform1i(TextureLoc, 1);
		glBindTexture(GL_TEXTURE_2D, NormalTextureID);

		glActiveTexture(GL_TEXTURE2);
		TextureLoc = glGetUniformLocation(BasicShader->GetProgram(), "texture_spec");
		glUniform1i(TextureLoc, 2);
		glBindTexture(GL_TEXTURE_2D, SpeculareTextureID);

		glActiveTexture(GL_TEXTURE3);
		TextureLoc = glGetUniformLocation(BasicShader->GetProgram(), "texture_Shadow");
		glUniform1i(TextureLoc, 3);
		glBindTexture(GL_TEXTURE_2D, ShadowMapID);

		//BasicShader->Update(Tri1.m_Transform, *light,lightSpaceMatrix);
		//Tri1.Draw();
		BasicShader->Update(Floor.m_Transform, *light, lightSpaceMatrix);
		Floor.Draw();
		//load texture for cude
		BasicShader->Update(cube->m_Transform, *light, lightSpaceMatrix);
		cube->Draw();
		
		


		light->DrawLight(&MainCam);
		DeltaTime += 0.016;
		SDL_Delay(16);//ms
		//executeframe
		SDL_GL_SwapWindow(window);


		//BasicShader->Update(Tri1.m_Transform);

		//rotates triangle
		//vec3 rot = Tri1.m_Transform.GetRotation();
		//rot += sin(DeltaTime/1000);
		////rot += 0.01
		//Tri1.m_Transform.SetRotation(rot);

	}
	SDL_GL_DeleteContext(GLContext);
	SDL_DestroyWindow(window);
	window = NULL;
	SDL_Quit();
	return 0 ;
	while (true)
	{
		

	}
	
}



