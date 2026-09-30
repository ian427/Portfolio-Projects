#version 430 core

in vec3 position;
uniform mat4 model;
uniform mat4 lighspacematrix;

void main()
{
	gl_Position = lighspacematrix *model* vec4(position,1.0f);
}
