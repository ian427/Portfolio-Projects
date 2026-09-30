#include "Lights.h"

LightBase::LightBase()
{
}

LightBase::~LightBase()
{
}

void LightBase::DrawLight(Camera* Cam)
{
	//glUseProgram(0);


	//glMatrixMode(GL_PROJECTION);

	//mat4 temp = Cam->GetPerspective();//do not change 

	//glLoadMatrixf((const GLfloat*) &temp);
	//glMatrixMode(GL_MODELVIEW);
	//glm::mat4 MV = Cam->ViewMatrix() * m_Transform.GetModel();
	//glLoadMatrixf((const GLfloat*)&MV[0]);
	//


	//glBegin(GL_LINES);
	//glm::vec3 p1 = this->m_Transform.GetPosition();
	//glm::vec3 p2 = p1;

	//glColor3f(1, 0, 0);
	//glVertex3fv(&p1.x);
	//p2 = p1 + glm::vec3(1, 0, 0) * 0.1f;
	//glColor3f(1, 0, 0);
	//glVertex3fv(&p2.x);

	//glColor3f(0, 1, 0);
	//glVertex3fv(&p1.x);
	//p2 = p1 + glm::vec3(0, 1, 0) * 0.1f;
	//glColor3f(0, 1, 0);
	//glVertex3fv(&p2.x);

	//glColor3f(0, 0, 1);
	//glVertex3fv(&p1.x);
	//p2 = p1 + glm::vec3(0, 0, 1) * 0.1f;
	//glColor3f(0, 0, 1);
	//glVertex3fv(&p2.x);

	//glEnd();
}
