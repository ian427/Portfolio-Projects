//#pragma once
//
//#include "Components.h"
//#include "BitMaps.h"
//#include "MyEventTypes.hpp"
//#include "EventHandler.h"
//#include "physics.h"
//#include "imgui.h"
//#include "SDL.h"
////base class in vector with virtual methods each child of base overloads that method
////struct SDL_Renderer;
//class ITreeNode
//{
//public:
//	ITreeNode* Parent;
//	std::vector<ITreeNode*>Children;
//	
//
//	void addToChildren(ITreeNode* newChild)
//	{
//		Children.push_back(newChild);
//	}
//	
//};
//
//
//
//
//class BaseEntity: public ITreeNode
//{
//public:
//	 
//	SDL_Renderer* m_Renderer;
//
//
//	
//
//	Physics phi;//copy
//	
//	 float Radius;
//	 Bitmap* Map;
//	 Collider*  BoxCollider;
//	 Transform transform;//26,27 getting this transforms data not flappys
//	 Transform& GetTransform() { return transform; }
//
//
//
//	 
//
//	 int ID;
//	 std::string ObjectName;
//
//	 
//
//	 //loading
//	 BaseEntity(SDL_Renderer* rendererin,  float inRadius, std::string fileName, int xpos, int ypos, int inID,std::string& instri)
//	 {
//		 
//		 Radius = inRadius;
//
//		 transform.SetPosition(vec3(xpos, ypos, 0));
//		 ID = inID;
//		 m_Renderer = rendererin;
//		 Map = new Bitmap(m_Renderer, fileName, transform.GetPosition().x , transform.GetPosition().y, false);
//		 BoxCollider = new Collider(transform, Map->GetWidth(), Map->GetHeight());
//
//	 }
//	 //constructing
//	 BaseEntity(SDL_Renderer* renderer, std::string fileName, int xpos, int ypos, bool useTransparancy, std::string Me)
//	 {
//		 m_Renderer = renderer;
//		 ObjectName = Me;
//
//		 transform.SetPosition(vec3(xpos, ypos, 0));
//
//		 Map = new Bitmap(m_Renderer, fileName, xpos, ypos, useTransparancy);
//		 BoxCollider = new Collider(transform, Map->GetWidth(), Map->GetHeight() );
//
//	 }
//	 void DrawUIChildren()
//	 {
//		ImGuiTreeNodeFlags nodeFlags = ImGuiTreeNodeFlags_FramePadding | ImGuiTreeNodeFlags_DefaultOpen;
//		bool isNodeOpen = ImGui::TreeNodeEx((this->ObjectName.c_str(), nodeFlags, this->ObjectName.c_str()));
//		if (isNodeOpen)
//		{
//			
//			
//			ImGui::TreePop();
//		}
//		for (int i = 0; i < Children.size(); i++)
//		{
//			DrawUIChildren();
//		}
//	 }
//	 
//	 void UIDraw() //editor ui
//	 {
//		 
//		 //std::cout << "clicked";
//		
//		 
//		 //all sprites use the samf window ID to stop the window "pop" problem
//		 ImGui::Begin("Selection");
//		 ImGui::Text(this->ObjectName.c_str());
//
//		 float TempPosAddressArry[2] = { transform.GetPosition().x, transform.GetPosition().y};//todo convert to int 
//		 ImGui::InputFloat2("Position:", TempPosAddressArry);
//		 transform.SetPosition( vec3( TempPosAddressArry[0], TempPosAddressArry[1], 0));
//
//		 float TempScaleAddressArry[2] = {transform.GetScale().x , transform.GetScale().y };
//		 ImGui::InputFloat2("Scale:", TempScaleAddressArry);
//		 transform.SetScale(vec3(TempScaleAddressArry[0], TempScaleAddressArry[1], 0));
//
//		 ImGui::Image(this->Map->GetTextureRef(), ImVec2(this->Map->GetWidth(), Map->GetHeight()));
//		 //ImGui::SliderFloat2("x", transform.GetScale().x, 0, 10);
//		 ImGui::Separator();
//		 
//
//		 ImGui::End();
//	 }
//	
//	 bool CheckBounds(int x, int y)
//	 {
//		 SDL_Rect ImageBound = { transform.GetPosition().x , transform.GetPosition().y , Map->GetWidth(), Map->GetHeight()};
//		 
//		 
//		 
//		 SDL_Point MousePosition = { x, y };
//
//		 bool b =SDL_PointInRect(&MousePosition, &ImageBound);
//		 return b;
//	 }
//
//	 
//
//	 virtual void Draw()//genral class ?
//	{
//		
//		BoxCollider->Update(transform);
//		Map->draw(transform.GetPosition().x, transform.GetPosition().y);
//		BoxCollider->DrawDebug(transform, m_Renderer);
//	}
//
//	
//	 
//	 Collider* GetCollider()
//	 {
//		return BoxCollider;
//	 }
//	 float GetRadius()
//	 {
//		return Radius;
//	 }
//
//	 virtual void ApplyContinuousMoment() {};
//	 ~BaseEntity()
//	 {
//		
//		 Bitmap* Map = nullptr;
//		 Collider* BoxCollider = nullptr;
//		 SDL_Renderer* m_Renderer = nullptr;
//		 
//	 }
//	
// protected:
//	 //BaseEntity()
//	 //{
//		// ObjectName = "Object name to be changed";
//
//		// //Map->m_x = transform.GetPosition().x;
//		// //Map->m_y = transform.GetPosition().y;
//	 //}
//	
//};
//
//
//
//class Bird : public I_EventHandeler , public virtual BaseEntity
//{
//public:
//	//collider 
//	//	gravity
//	//Hit Object
//	//bitmap
//	//phiysics check
//
//	
//	//anything that needs to move
//	
//	//Transform transform;
//	//Transform Transform{ vec3{320.0f, 240.0f, 0.0f} };
//	
//	
//
//	Bird(SDL_Renderer* renderer, int X, int Y) : BaseEntity(renderer, "assets/Flappy.bmp", X, Y, true,"Bird")
//	{
//
//		
//	}
//
//	~Bird()
//	{
//		delete BoxCollider;  
//		//delete Map;
//	}
//	
//	void ApplyContinuousMoment()
//	{
//		phi.Move(transform, vec3(0, 2, 0));
//	}
//	
//
//	void OnEvent(MyEventTypes eventType, Event* data)
//	{
//		switch (eventType)
//		{
//		case SPACE_PRESSED:
//			phi.Move(transform, vec3(0, -20, 0));
//			break;
//		default:
//			break;
//		}
//	}
//	bool CollisionCheck( BaseEntity* CollidingAgainst)// needs to the the derived class
//	{//calculates the position and hoe far apart so we can find where to check radii from 
//		//float Dx = transform.GetPosition().x - CollidingAgainst->GetTransform().GetPosition().x;
//		//float Dy = transform.GetPosition().y - CollidingAgainst->GetTransform().GetPosition().y;
//		//float D = (sqrt((Dx * Dx) + (Dy * Dy)));
//
//		//if ((D == Radius + CollidingAgainst->GetRadius()) || (D == Radius - CollidingAgainst->GetRadius()))//circle collision check
//	
//		//if(D <= Radius + CollidingAgainst->GetRadius())
//		//{
//				bool Result = false;
//			if (phi.AABBIntersection(BoxCollider, CollidingAgainst->GetCollider()))
//			{
//				
//				Result = true;
//			}
//
//		//}
//		return Result;
//
//	}
//};
////?setBitmapWith input string implement if time
//class Pipe : public virtual BaseEntity
//{
//	
//	bool inuse = false; //spawning
//public:
//	bool topPipe = true;
//	//bitmap
//	//collider 
//
//	//Collider* BoxCollider;
//	
//	
//	Pipe(bool value,SDL_Renderer* renderer, int X, int Y) : BaseEntity(renderer, "assets/Pipes3Bottom.bmp", X, Y, true,"pipe")
//	{
//		delete Map;
//		Map = nullptr;
//		topPipe = value;
//
//		if (topPipe)
//		{
//			ObjectName = "TopPipe";
//			//Map = new Bitmap(m_Renderer, "assets/Pipes3Top.bmp", transform.GetPosition().x, transform.GetPosition().y);// loads top pipe png
//			Map = new Bitmap(m_Renderer, "assets/Pipes3Bottom.bmp", transform.GetPosition().x, transform.GetPosition().y);// loads top pipe png
//		}
//		else
//		{
//			ObjectName = "BottomPipe";
//			Map = new Bitmap(m_Renderer, "assets/Pipes3Top.bmp", transform.GetPosition().x, transform.GetPosition().y);//loads bottom pipe png
//		}
//
//	}
//	~Pipe()
//	{
//
//		SDL_Renderer* m_Renderer = nullptr;
//	}
//	void ApplyContinuousMoment()
//	{
//		phi.Move(transform, vec3(-3, 0, 0));
//	}
//};
//	
//
//
//class Ground : public virtual BaseEntity
//{
//	public:
//	//bitmap
//	//collider 
//	
//	//Collider* BoxCollider;
//	
//	
//	Ground(SDL_Renderer* renderer, int X, int Y) : BaseEntity(renderer, "assets/Ground.bmp", X, Y, true,"Ground")
//	{
//		
//		Map = new Bitmap(m_Renderer, "assets/Ground.bmp", transform.GetPosition().x, transform.GetPosition().y);
//	}
//	~Ground()
//	{
//
//		SDL_Renderer* m_Renderer = nullptr;
//    }
//
//	void ApplyContinuousMoment()
//	{
//	}
//};
//class UI : public virtual BaseEntity
//{
//public:
//	//bitmap
//	//collider 
//	
//	//Collider* BoxCollider;
//	
//
//	UI(SDL_Renderer* renderer, int X, int Y) : BaseEntity(renderer, "assets/UI.bmp", X, Y, true,"UI")
//	{
//	}
//	~UI()
//	{
//		SDL_Renderer* m_Renderer = nullptr;
//		
//	}
//	void ApplyContinuousMoment()
//	{
//	}
//};
//class Trigger :public virtual BaseEntity
//{
//	
//public:
//	bool isTrigger = true;
//	//bitmap
//	//collider 
//
//	//Collider* BoxCollider;
//	
//	
//	Trigger( SDL_Renderer* renderer, int X, int Y) : BaseEntity(renderer, "assets/Trigger.bmp", X, Y, true,"trigger")
//	{
//		
//	}
//	~Trigger()
//	{
//
//		SDL_Renderer* m_Renderer = nullptr;
//	}
//	void ApplyContinuousMoment()
//	{
//		phi.Move(transform, vec3(-3, 0, 0));
//	}
//
//};
//
//class Seed :public virtual BaseEntity
//{
//
//public:
//	
//	Seed(SDL_Renderer* renderer, int X, int Y) : BaseEntity(renderer, "assets/Seed.bmp", X, Y, true,"seed")
//	{
//	}
//	~Seed()
//	{
//	}
//	void ApplyContinuousMoment()
//	{
//		phi.Move(transform, vec3(-3, 0, 0));
//	}
//
//};
