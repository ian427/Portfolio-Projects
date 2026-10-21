#include"Entity3.h"
#include "Components.h"
#include "BitMaps.h"
#include "MyEventTypes.hpp"
#include "EventHandler.h"
#include "physics.h"
#include "imgui.h"
#include "SDL.h"
//base class in vector with virtual methods each child of base overloads that method
//struct SDL_Renderer;

void ITreeNode::addToChildren(ITreeNode* newChild)
{
	Children.push_back(newChild);
}





//loading
BaseEntity::BaseEntity(SDL_Renderer* rendererin, float inRadius, std::string fileName, int xpos, int ypos, int inID, std::string& instri)
{

		Radius = inRadius;

		transform.SetPosition(vec3(xpos, ypos, 0));
		ID = inID;
		m_Renderer = rendererin;
		Map = new Bitmap(m_Renderer, fileName, transform.GetPosition().x, transform.GetPosition().y, true);
		BoxCollider = new Collider(transform, Map->GetWidth(), Map->GetHeight());

}
	//constructing
BaseEntity::BaseEntity(SDL_Renderer* renderer, std::string fileName, int xpos, int ypos, bool useTransparancy, std::string Me)
	{
		m_Renderer = renderer;
		ObjectName = Me;

		transform.SetPosition(vec3(xpos, ypos, 0));

		Map = new Bitmap(m_Renderer, fileName, xpos, ypos, useTransparancy);
		BoxCollider = new Collider(transform, Map->GetWidth(), Map->GetHeight());

	}
	void BaseEntity::DrawUIChildren()
	{
		ImGuiTreeNodeFlags nodeFlags = ImGuiTreeNodeFlags_FramePadding | ImGuiTreeNodeFlags_DefaultOpen;
		bool isNodeOpen = ImGui::TreeNodeEx((this->ObjectName.c_str(), nodeFlags, this->ObjectName.c_str()));
		if (isNodeOpen)
		{


			ImGui::TreePop();
		}
		for (int i = 0; i < Children.size(); i++)
		{
			DrawUIChildren();
		}
	}

	void BaseEntity::UIDraw() //editor ui
	{

		//std::cout << "clicked";


		//all sprites use the samf window ID to stop the window "pop" problem
		ImGui::Begin("Selection");
		ImGui::Text(this->ObjectName.c_str());

		float TempPosAddressArry[2] = { transform.GetPosition().x, transform.GetPosition().y };//todo convert to int 
		ImGui::InputFloat2("Position:", TempPosAddressArry);
		transform.SetPosition(vec3(TempPosAddressArry[0], TempPosAddressArry[1], 0));

		float TempScaleAddressArry[2] = { transform.GetScale().x , transform.GetScale().y };
		ImGui::InputFloat2("Scale:", TempScaleAddressArry);
		transform.SetScale(vec3(TempScaleAddressArry[0], TempScaleAddressArry[1], 0));

		ImGui::Image(this->Map->GetTextureRef(), ImVec2(this->Map->GetWidth(), Map->GetHeight()));
		//ImGui::SliderFloat2("x", transform.GetScale().x, 0, 10);
		ImGui::Separator();


		ImGui::End();
	}

	bool BaseEntity::CheckBounds(int x, int y)
	{
		SDL_Rect ImageBound = { transform.GetPosition().x , transform.GetPosition().y , Map->GetWidth(), Map->GetHeight() };



		SDL_Point MousePosition = { x, y };

		bool b = SDL_PointInRect(&MousePosition, &ImageBound);
		return b;
	}



	void BaseEntity::Draw()//genral class ?
	{

		BoxCollider->Update(transform);
		Map->draw(transform.GetPosition().x, transform.GetPosition().y);
		BoxCollider->DrawDebug(transform, m_Renderer);
	}



	Collider* BaseEntity::GetCollider()
	{
		return BoxCollider;
	}
	float BaseEntity::GetRadius()
	{
		return Radius;
	}

	 void BaseEntity::ApplyContinuousMoment() {};
	BaseEntity::~BaseEntity()
	{

		Bitmap* Map = nullptr;
		Collider* BoxCollider = nullptr;
		SDL_Renderer* m_Renderer = nullptr;

	}




	Bird::Bird(SDL_Renderer* renderer, int X, int Y) : BaseEntity(renderer, "assets/Flappy.bmp", X, Y, true, "Bird")
	{


	}

	Bird::~Bird()
	{
		delete BoxCollider;
		//delete Map;
	}

	void Bird::ApplyContinuousMoment()
	{
		phi.Move(transform, vec3(0, 2, 0));
	}


	void Bird::OnEvent(MyEventTypes eventType, Event* data)
	{
		switch (eventType)
		{
		case SPACE_PRESSED:
			phi.Move(transform, vec3(0, -20, 0));
			break;
		default:
			break;
		}
	}
	bool Bird::CollisionCheck(BaseEntity* CollidingAgainst)// needs to the the derived class
	{//calculates the position and hoe far apart so we can find where to check radii from 
		//float Dx = transform.GetPosition().x - CollidingAgainst->GetTransform().GetPosition().x;
		//float Dy = transform.GetPosition().y - CollidingAgainst->GetTransform().GetPosition().y;
		//float D = (sqrt((Dx * Dx) + (Dy * Dy)));

		//if ((D == Radius + CollidingAgainst->GetRadius()) || (D == Radius - CollidingAgainst->GetRadius()))//circle collision check

		//if(D <= Radius + CollidingAgainst->GetRadius())
		//{
		bool Result = false;
		if (phi.AABBIntersection(BoxCollider, CollidingAgainst->GetCollider()))
		{

			Result = true;
		}

		//}
		return Result;

	}





	Pipe::Pipe(bool value, SDL_Renderer* renderer, int X, int Y) : BaseEntity(renderer, "assets/Pipes3Bottom.bmp", X, Y, true, "pipe")
	{
		delete Map;
		Map = nullptr;
		topPipe = value;

		if (topPipe)
		{
			ObjectName = "TopPipe";
			//Map = new Bitmap(m_Renderer, "assets/Pipes3Top.bmp", transform.GetPosition().x, transform.GetPosition().y);// loads top pipe png
			Map = new Bitmap(m_Renderer, "assets/Pipes3Bottom.bmp", transform.GetPosition().x, transform.GetPosition().y);// loads top pipe png
		}
		else
		{
			ObjectName = "BottomPipe";
			Map = new Bitmap(m_Renderer, "assets/Pipes3Top.bmp", transform.GetPosition().x, transform.GetPosition().y);//loads bottom pipe png
		}

	}
	Pipe::~Pipe()
	{

		SDL_Renderer* m_Renderer = nullptr;
	}
	void Pipe::ApplyContinuousMoment()
	{
		phi.Move(transform, vec3(-3, 0, 0));
	}





	Ground::Ground(SDL_Renderer* renderer, int X, int Y) : BaseEntity(renderer, "assets/Ground.bmp", X, Y, true, "Ground")
	{

		Map = new Bitmap(m_Renderer, "assets/Ground.bmp", transform.GetPosition().x, transform.GetPosition().y);
	}
	Ground::~Ground()
	{

		SDL_Renderer* m_Renderer = nullptr;
	}

	void Ground::ApplyContinuousMoment()
	{
	}

	UI::UI(SDL_Renderer* renderer, int X, int Y) : BaseEntity(renderer, "assets/UI.bmp", X, Y, true, "UI")
	{
	}
	UI::~UI()
	{
		SDL_Renderer* m_Renderer = nullptr;

	}
	void UI::ApplyContinuousMoment()
	{
	}



	Trigger::Trigger(SDL_Renderer* renderer, int X, int Y) : BaseEntity(renderer, "assets/Trigger.bmp", X, Y, true, "trigger")
	{

	}
	Trigger::~Trigger()
	{

		SDL_Renderer* m_Renderer = nullptr;
	}
	void Trigger::ApplyContinuousMoment()
	{
		phi.Move(transform, vec3(-3, 0, 0));
	}


	Seed::Seed(SDL_Renderer* renderer, int X, int Y) : BaseEntity(renderer, "assets/Seed.bmp", X, Y, true, "seed")
	{
	}
	Seed::~Seed()
	{
	}
	void Seed::ApplyContinuousMoment()
	{
		phi.Move(transform, vec3(-3, 0, 0));
	}


