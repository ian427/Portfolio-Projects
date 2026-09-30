#pragma once
#define GLM_ENABLE_EXPERIMENTAL
#include "Transform.hpp"
#include <glm.hpp>
#include <gtx\transform.hpp>
#include <SDL.h>
using namespace glm;
class Camera
{
public:
	bool use_target;
	Camera(vec3 pos, float fov, float aspec, float znear, float zfar);
	Transform m_CamTransform;
	//void SetCamDirection(vec3 NewForward, vec3 NewUp, vec3 NewRight);
	mat4 CalcPespective(float fov, float aspec, float znear, float zfar);
	mat4 GetPerspective();
	mat4 ViewMatrix();
	
	mat4 PerspetiveProjection;
	void mouseMoveTarget(SDL_Event* e);
	void ToggleTarget();
	~Camera();

	vec3 GetForward()
	{
		return Forward;
	}
	vec3 GetRight()
	{
		return Right;
	}
	vec3 GetUp()
	{
		return Up;
	}

private:
	
	vec3 Forward , Up, Right,currenttarget;
	int lastX , lastY;
	bool FirstMove;
	//bool use_target;
    float xRot, yRot,FOV,Aspect;



};

