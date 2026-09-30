#include "Camera.h"
#include <SDL.h>
#include <iostream>



Camera::Camera(vec3 pos, float fov, float aspec, float znear, float zfar)
{
	PerspetiveProjection = CalcPespective(fov, aspec, znear,  zfar);
	m_CamTransform.SetPosition(pos);

	FOV = fov;
	use_target = false;
	
	FirstMove = true;
	//this->znear = znear;
}

//void Camera:: SetCamDirection(vec3 NewForward, vec3 NewUp, vec3 NewRight)
//{
//
//	Forward = normalize(vec3(0)-NewForward);
//	Right = normalize(cross(vec3(0,1,0), Forward ));
//	Up = cross(Forward, Right);
//
//
//}
mat4 Camera::CalcPespective (float fov, float aspec, float znear, float zfar)
{
	return glm::perspective(fov, aspec, znear, zfar);
}

mat4 Camera::GetPerspective()
{
	return PerspetiveProjection;
}

mat4 Camera::ViewMatrix()
{

	vec3 target = m_CamTransform.GetPosition() + Forward;
	if (use_target)
	{
		Forward = normalize(vec3(0) - m_CamTransform.GetPosition());
		target = vec3(0);
	}
	Right = normalize(cross(vec3(0, 1, 0), Forward));
	Up = cross(Forward, Right);

	return glm::lookAt(m_CamTransform.GetPosition(), target, Up);

	
}

void Camera::mouseMoveTarget(SDL_Event* e)
{
	if (!use_target)
	{
		int mouseX, mouseY;
		SDL_GetMouseState(&mouseX, &mouseY);
		if (FirstMove)
		{
			lastX = mouseX;
			lastY = mouseY;
			FirstMove = false;
		}

		float xOffset = mouseX - lastX;
		float yOffset = lastY - mouseY;
		lastX = mouseX;
		lastY = mouseY;

		float sensitivity = 0.5;
		xOffset *= sensitivity;
		yOffset *= sensitivity;

		xRot = m_CamTransform.GetRotation().x;
		yRot = m_CamTransform.GetRotation().y;
		//Mouse X/Y is the inverse of screen X and Y so we add y offset to x and vice versa
		xRot += yOffset; //Pitch
		yRot += xOffset; //Yaw
		m_CamTransform.SetRotation(vec3(xRot, yRot, m_CamTransform.GetRotation().z));

		//std::cout << "xRot: " << xRot << " yRot: " << yRot << std::endl;//Debug

		//Stop gimble lock
		if (m_CamTransform.GetRotation().x > 89.0f)
		{
			m_CamTransform.SetRotation(vec3(89.0f, m_CamTransform.GetRotation().y, m_CamTransform.GetRotation().z));
		}
		if (m_CamTransform.GetRotation().x < -89.0f)
		{
			m_CamTransform.SetRotation(vec3(-89.0f, m_CamTransform.GetRotation().y, m_CamTransform.GetRotation().z));
		}

		vec3 front;
		front.x = cos(radians(m_CamTransform.GetRotation().y)) * cos(radians(m_CamTransform.GetRotation().x));
		front.y = sin(radians(m_CamTransform.GetRotation().x));
		front.z = sin(radians(m_CamTransform.GetRotation().y)) * cos(radians(m_CamTransform.GetRotation().x));
		Forward = normalize(front);
		//std::cout << "Updated Forward: " << Forward.x << ", " << Forward.y << ", " << Forward.z << std::endl;
	}

}
void Camera::ToggleTarget()
{
	use_target = !use_target;
}

Camera::~Camera()
{
}
