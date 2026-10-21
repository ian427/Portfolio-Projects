#pragma once
//squrecolliders
//gravity
// //apply movement
//collision event
//
#include <glew.h>
#include<glm.hpp>
#include "Components.h"
#include <vector>

using namespace glm;
class Physics
{
public :
	vec3 CurrentForces ;
	vec3 Forces;
	float Mass;
	
	vec3 AccumulateForces(const std::vector<vec3> forces);
	void ApplyForces(Transform& transform);//to move
	
	void ContinuousMoment(Transform transform, vec3 Move);


	////this is an example ONLY
	//virtual void TestMethod()// default
	//{
	//	int i = 0;
	//	//do stuff
	//}
	//virtual void TestMethodPure() =0;

	bool AABBIntersection (Collider* Collider1, Collider* Collider2);

	void UpdatePhysics();
	void Move(Transform& transform, vec3 movement);
	
private:
	std::vector<vec3> forces;
	std::vector<vec3> impulses;
    
};

