#ifndef TRANSFORM_H
#define TRANSFORM_H
#define GLM_ENABLE_EXPERIMENTAL
#include<glm.hpp>
#include<gtx\transform.hpp>

using namespace glm;

class Transform
{
public:
	Transform(const vec3& pos = vec3(0), const vec3& rot = vec3(0), const vec3& scale = vec3(1)) :
		m_position(pos),
		m_rotation(rot),
		m_scale(scale)
	{}
	~Transform()
	{}
	inline mat4 GetModel()const
	{
		mat4 posMatrix = translate(m_position);
		mat4 rotXMatrix = rotate(m_rotation.x, vec3(1, 0, 0));
		mat4 rotYMatrix = rotate(m_rotation.y, vec3(0, 1, 0));
		mat4 rotZMatrix = rotate(m_rotation.z, vec3(0, 0, 1));
		mat4 scaleXMatrix = scale(m_scale);
		mat4 rotMatrix = rotZMatrix * rotYMatrix * rotXMatrix;//MUST BE BACKWARDS

		return posMatrix * rotMatrix * scaleXMatrix;
	}



	vec3 GetPosition() const
	{
		return  m_position;

	}
	void SetPosition(vec3 newpos)
	{

		m_position = newpos;

	}
	vec3 GetRotation() const
	{
		return  m_rotation;

	}
	void SetRotation(vec3 newrotation)
	{
		m_rotation = newrotation;


	}
	vec3 GetScale() const
	{
		return  m_scale;

	}
	void SetScale(vec3 newscale)
	{
		m_scale = newscale;


	}

private:
	vec3 m_position;
	vec3 m_rotation;
	vec3 m_scale;


};



#endif // !TRANSFORM_H
