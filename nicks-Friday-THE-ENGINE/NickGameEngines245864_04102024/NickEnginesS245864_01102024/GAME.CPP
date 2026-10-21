#include <SDL.h>
#include <iostream>
#include "Game.h"
#include "Entity3.h" 

#include "Saveing.h"
#include "Debug.h" //|4|
#include "imgui.h"
#include "backends/imgui_impl_sdl.h"
#include "imgui_sdl.h"
#include "imgui_internal.h"
#include <filesystem>

#include"BitMaps.h"
 Game::Game(EventSystem* eventSytem)
 {  
     srand(time(NULL));
    this->eventSystem = eventSytem;
    m_Window = nullptr;
   // m_Renderer = nullptr;

    //start up
    SDL_Init(SDL_INIT_VIDEO);
    TTF_Init();//04-02
   
    //CreateWindow//make configurable
    m_Window = SDL_CreateWindow(
        "Main Window",
        250, // XPosition
        50, // YPosition
        screenWidth, //Width in pixels
        720,  //height pixels
        0 //behaviour flags
    );
    if (!m_Window)
    {
        DeBug::Log()->printDebug(DeBug::Verbosity::red, "WINDOW initialsation FAILED %s, %d", 3);
        printf("PRESS ANY KEY\n");
        
        getchar();
        return;

    }
   


        m_Renderer = SDL_CreateRenderer(
        m_Window, //links to window
        -1,      // indexes render driver
        0         //behaviour flags
    );
    if (!m_Renderer)
    {
        printf("\n"), SDL_GetError();
        DeBug::Log()->printDebug(DeBug::Verbosity::yellow, "RENDERER initialsation FAILED %s, %d", 3);
        printf("PRESS ANY KEY\n");
        getchar();
        return;

    }	
    IMGUI_CHECKVERSION();
    ImGui::CreateContext();
    SDL_DisplayMode DisplayMode;
    SDL_GetCurrentDisplayMode(0, &DisplayMode);
    ImGuiSDL::Initialize(m_Renderer, DisplayMode.w, DisplayMode.h); 
    io = new ImGuiIO();
    *io = ImGui::GetIO();
    (void)io;
    io->ConfigFlags |= ImGuiConfigFlags_NavEnableKeyboard;
    io->ConfigFlags |= ImGuiConfigFlags_DockingEnable;
    io->ConfigFlags |= ImGuiConfigFlags_ViewportsEnable;
    ImGui::StyleColorsDark();

    ImGui_ImplSDL2_InitForOpenGL(m_Window, 0);///behaviour flag 0 ask nick
    /////////////////////////////////////////////////////////////////////// migrate to cpp
    //populates imGui with assets
    std::string path = "./assets";
    
    for (const auto& entry : std::filesystem::directory_iterator(path)) //directory_iterator(path) //recursive_
    {
        if (entry.path().extension() == ".bmp" || entry.path().extension() == ".jpg" || entry.path().extension() == ".png")
        {
            Bitmap* Asset = new Bitmap(m_Renderer, entry.path().string(), 0, 0, true);
            content.push_back(Asset);

        }
        else if (entry.is_directory())
        {
            std::cout << "dir " << entry << std::endl;
        }
        //debug
        std::cout << entry.path() << std::endl;
    }

    
    phi = new Physics();
    Framtimstack = new FrameTimeStack();

    int tempx = 10;
    
   
    //////// create all objects
     
   for (int i =0; i < 4; i++)
   { 
          int y;
     
          y = rand() % (600 - 300 + 1) + 300;//between 300,600

          BottomPipe1 = new Pipe( false, m_Renderer, tempx, y);
     Entitys.push_back(BottomPipe1);
     Pipes.push_back(BottomPipe1);    // pushing the pointer into the vector
    
     TopPipe1 = new Pipe(true, m_Renderer, tempx, (y - 800));
      Entitys.push_back(TopPipe1);
      Pipes.push_back(TopPipe1);

      seed = new Seed( m_Renderer, tempx, (y - 700));
      Collectables.push_back(seed);

     tempx += screenWidth/4+1;

    }
   

   Flappy = new Bird(m_Renderer, 200,250);
   eventSystem->AddListener(Flappy);
   Entitys.push_back(Flappy);
     //read in the font
    m_pSmallFont = TTF_OpenFont("assets/DejaVuSans.ttf",15); //04-02
    m_pBigFont = TTF_OpenFont("assets/DejaVuSans.ttf",50); //04-02   

    Floor = new Ground(m_Renderer, 0, 620);
 
    Entitys.push_back(Floor);
    Celling = new UI(m_Renderer,0,0);

    Entitys.push_back(Celling);
 
    
    //calculates raduius that we sould do a collision check in 

   
    /*
    for (int i = 0; i < Entitys.size(); i++)
    {
        float A = Entitys[i]->transform.GetPosition().x;
        float B = Entitys[i]->transform.GetPosition().y;
        //Entitys[i]->Radius = (sqrt((A * A) + (B * B))) / 2;
        Entitys[i]->Radius = 100;//TODO save bitmap size to plug in
    }
   */
    /*
    std::ifstream file("Save.txt");
    if(file.good())
    {
        std::vector<BaseEntity>load;
        load = Save::loadObjects("Save.txt", m_Renderer);
        for (int i = 0; i < load.size(); i++)
        {
            SavedObjects.push_back(new BaseEntity(load[i]));

        }
    }
    */
     sceneRoot = new BaseEntity(m_Renderer, "assets/Trigger.bmp", 0, 620, true,"sceenroot");
    for (int i = 0; i < Entitys.size(); i++)
    {
        sceneRoot->addToChildren(Entitys[i]);
    }
    ProfilerSystem::Instance().storeProfiledata("Start", 0.0f);

 }


 Game::~Game()
 {
     delete phi;
     Physics* phi = nullptr;
     delete Floor;
     Ground* Floor = nullptr;
     delete Celling;
  
     UI* Celling = nullptr;
     delete seed;
     Seed* seed = nullptr;
     delete BottomPipe1;
     Pipe* BottomPipe1 = nullptr;
     delete TopPipe1;
     Pipe* TopPipe1 = nullptr;
     delete Flappy;
     Bird* Flappy = nullptr;
     delete AssetMousDrag;
     Bitmap* AssetMousDrag = nullptr;
     delete m_monsterTransKeyed;
     Bitmap* m_monsterTransKeyed = nullptr;
     delete m_monsterTrans;
     Bitmap* m_monsterTrans = nullptr;
     delete m_monster;
     Bitmap* m_monster = nullptr;
     


    
     TTF_CloseFont(m_pBigFont);
     TTF_CloseFont(m_pSmallFont);
     // destroy in reverse creation order
    if (m_Renderer)
    {
        SDL_DestroyRenderer(m_Renderer);
    }
    if (m_Window)
    {
        SDL_DestroyWindow(m_Window);

    }

 }
 


 void Game::SetDisplayColour(int r, int g, int b, int a)
 {
     if (m_Renderer)
        
     { 
         
         int result = SDL_SetRenderDrawColor(
             m_Renderer, //target renderer
             r, g, b, a  //r,g,b,alpha
             );
         //wipe display
         SDL_RenderClear(m_Renderer);
         //present whats drawn
       
        // SDL_Delay(16);//milli seconds //may be needed back
       


     }


 }
 void Game::UpdateText(std::string msg, int x, int y, TTF_Font* font, SDL_Colour colour)
 {
     FrameTimeStack frameTimes;
     SDL_Surface* surface = nullptr;
     SDL_Texture* texture = nullptr;

     int texW = 0;
     int texH = 0;
    

     surface = TTF_RenderText_Solid(font, msg.c_str(), colour);
     if (!surface)
     {
         //error check
         printf("Surface font  not loaded ,file\n" );
         DeBug::Log()->printDebug(DeBug::Verbosity::note, "WINDOW initialsation FAILED %s, %d", 3);
         printf("%\n", SDL_GetError());

     }
     else
     {
         texture = SDL_CreateTextureFromSurface(m_Renderer, surface);
             if (!texture)
             {
                 //error check
                 
                 DeBug::Log()->printDebug(DeBug::Verbosity::yellow, "TEXTURE font not loaded %s, %d", msg.c_str(), 3);
                 printf("%\n", SDL_GetError());

             }
             else
             {
                 SDL_QueryTexture(texture, NULL, NULL, &texW, &texH);
                 SDL_Rect textRect = { x,y,texW,texH };

                 SDL_RenderCopy(m_Renderer, texture, NULL, &textRect);

             }

     }
     if (texture)
     {
         
         SDL_DestroyTexture(texture);
     }
     if (surface)
     {
  
         SDL_FreeSurface(surface);
         
      }


 }
 void Game::Update(void)
 {
     ProfilerSystem::Instance().startframe();

     //CheckEvents();
     SDL_RenderClear(m_Renderer);
     if (currentScene == TESTSCENE)
     {
         
         ImGui::NewFrame();
         ImGui_ImplSDL2_NewFrame(m_Window);
         bool show = true;
         ImGui::ShowDemoWindow(nullptr);
         //ShowExampleAppleDockSpace(&show);
         ImGui::Begin(".");
         bool clicked = ImGui::Button("ToGame", ImVec2(0, 0));
         if (clicked)
         {
             currentScene = GAME;

         }
         ImGui::End();
         
             PROFILE("Draw Editor");
             {

                 ImGui::Begin("Editor");
                 ImGui::BeginChild("Content Window", ImVec2(), true);
                 ImGui::BeginTable("Content browser", 3);

                 for (int i = 0; i < content.size(); i++)
                 {
                     ImGui::PushID(i);

                     ImGui::ImageButton((ImTextureID)content[i]->GetTextureRef(), { 100,100 });


                     //for dragging

                     if (ImGui::BeginDragDropSource())
                     {
                         AssetMousDrag = content[i];
                         ImGui::Image((ImTextureID)content[i]->GetTextureRef(), { 100,100 });
                         ImGui::EndDragDropSource();
                     }

                     ImGui::PopID();
                     ImGui::SameLine();
                 }

                 ImGui::EndTable();

                 ImGui::EndChild();
                 ImGui::End();
             }
         

             {
                 PROFILE("Mouse");
                 if (ImGui::IsMouseReleased(ImGuiMouseButton_Left) && AssetMousDrag != nullptr)
                 {

                     int x, y;
                     SDL_GetMouseState(&x, &y);
                     BaseEntity* b = new BaseEntity(m_Renderer, AssetMousDrag->FileName, x, y, true, "Default");
                     //Bitmap* s = new Bitmap(m_Renderer, AssetMousDrag->FileName, x, y, true);
                     //s->Transfrom.ParentSet(GameWindow::Instance().GetHirarcy());
                     SceanHierarchy.push_back(b);
                     sceneRoot->addToChildren(b);
                     //Entitys.push_back(b);
                     SavedObjects.push_back(b);//DEMO use above for launch 
                     //save
                     Save::saveObject(*b, "Save.txt");


                     AssetMousDrag = nullptr;
                 }
             }

             {

                 PROFILE("Content");
                 {

                     ImGui::Begin("Editor");
                     ImGui::BeginChild("Content Window", ImVec2(), true);

                     //for contents of assets file creat a dragable
                     int CountInRow = 0;
                     for (int i = 0; i < content.size(); i++)
                     {
                         ImGui::PushID(i);

                         ImGui::ImageButton((ImTextureID)content[i]->GetTextureRef(), { 100,100 });//bit in window


                         /////////////////////////////////For Draging
                         if (ImGui::BeginDragDropSource())
                         {
                             AssetMousDrag = content[i];
                             ImGui::Image((ImTextureID)content[i]->GetTextureRef(), { 100,100 }); // thumbnail

                             ImGui::EndDragDropSource();

                         }
                         //add place in scene

                         /////////////////////////////////For Draging
                         ImGui::PopID();
                         if (CountInRow % 3 < 3)
                         {
                             ImGui::SameLine();
                             CountInRow++;//todo fix
                         }

                     }

                     // ImGui::EndTabItem();

                     ImGui::EndChild();
                     ImGui::End();

                 }
             }
             {

                 PROFILE("Draw Hirarcy");
                 {
                     int selectedFrame = -1;

                     ImGui::Begin("Scene Hirarcy", 0, ImGuiWindowFlags_NoMove);
                     ImGui::SetWindowPos({ 0,200 });
                     ImGui::SetWindowSize({ 150, 720 - 200 });




                     ImGuiTreeNodeFlags nodeFlags = ImGuiTreeNodeFlags_FramePadding | ImGuiTreeNodeFlags_DefaultOpen;
                     bool isNodeOpen = ImGui::TreeNodeEx("Scene", nodeFlags, "Scene");
                     if (isNodeOpen)
                     {
                         //cout << bitmap->ObjectName.c_str() << endl;
                         //I_GUIWindow::SelectedObject = .ObjectName;
                         for (int i = 0; i < sceneRoot->Children.size(); i++)
                         {
                             BaseEntity* child = static_cast<BaseEntity*>(sceneRoot->Children[i]);
                             child->DrawUIChildren();

                         }

                         ImGui::TreePop();
                     }

                     ImGui::End();
                 }

                 int x, y;
                 uint32 Buttons = SDL_GetMouseState(&x, &y);
                 for (BaseEntity* gameObject : Entitys)
                 {


                     if (static_cast<BaseEntity*>(gameObject)->CheckBounds(x, y) && SDL_BUTTON(Buttons) == 1 ||
                         static_cast<BaseEntity*>(gameObject)->CheckBounds(x, y) && SDL_BUTTON(Buttons) == 3)//1 left 2 middel 3 right|mousebutton//click tolorance
                     {

                         SelectedObject = gameObject;
                     }
                     if (SelectedObject == gameObject)
                     {
                         gameObject->UIDraw();

                     }
                 }




                 for (int i = 0; i < sceneRoot->Children.size(); i++)
                 {
                     BaseEntity* child = static_cast<BaseEntity*>(sceneRoot->Children[i]);
                     child->Draw();

                 }
             }
             {

                 PROFILE("Profiler");
                 {
                     ImGui::Begin("Frametime");
                     Framtimstack->push(io->DeltaTime * 1000);//fram time stack
                     //ImGui::PlotLines("frame", FrameTime.FramTimeQ.data(),FrameTime.Capacity);//no data

                     char Buffer[64];
                     //printf(Buffer, sizeof Buffer, "%f ms",  FrameTime.AverageTime());    
                     ImGui::Text(Buffer);
                     ImGui::End();
                     //checkinf selectedclean up//




                     ImGui::Begin("Profiler");
                     static vector<float>* FrameTimes;
                     static FrameMap* Snapshot;
                     ImGui::SameLine();
                     static bool LiveFlameGraph = true;
                     ImGui::Checkbox("Live Flame Graph", &LiveFlameGraph);
                     if (LiveFlameGraph)
                     {
                         selectedFrame = -1;
                     }
                     static int range[2] = { 0, 100 };

                     if (FrameTimes && FrameTimes->size() > 100)
                     {
                         ImGui::SliderInt2("Frame Range", range, 0, FrameTimes->size());
                         if (range[0] >= range[1])
                             range[0] = range[1] - 1;
                         /*ImGui::SliderInt("Frame Range", &range[0], 0, FrameTimes->size());
                         if (range[0] <10)
                             range[0] =10;*/
                         vector<float> subData(FrameTimes->cbegin() + range[0], FrameTimes->cbegin() + range[1]);
                         //vector<float> subData(FrameTimes->cbegin() + range[0]-10, FrameTimes->cbegin() + range[0]);

                         int tempHistSelection = ImGui::MyPlotHistogram("Frame data", subData.data(), subData.size());
                         if (tempHistSelection != -1)
                         {
                             LiveFlameGraph = false;
                             selectedFrame = tempHistSelection;
                         }
                         ImRect rect = { ImGui::GetItemRectMin(), ImGui::GetItemRectMax() };
                         if (rect.Contains(io->MousePos))
                         {
                             if (ImGui::IsMouseClicked(ImGuiMouseButton_Left))
                             {

                             }
                         }
                     }
                     FrameMap& previousFrame = ProfilerSystem::Instance().GetLastFrameData();//empty
                     if (!LiveFlameGraph && selectedFrame != -1)
                     {
                         previousFrame.clear();
                         for (auto const [SampleName, samples] : *Snapshot)
                         {
                             previousFrame[SampleName].push_back(samples[range[0] + selectedFrame]);
                         }
                     }
                     else
                     {
                         LiveFlameGraph = false;
                     }
                     //ImGui::PlotHistogram("FrameTimes")


                     ImGui::LabelText("Frame Date Count", std::to_string(previousFrame.size()).c_str());



                     //ImGui::LabelText("Graph Window Width", std::to_string(GraphWindowWidth).c_str());
                     //ImGui::RenderFrame({ 10,20 }, { 100,50 }, ImGui::GetColorU32(ImGuiCol_FrameBg), true, GImGui->Style.FrameRounding);
                     ImDrawList* drawlist = ImGui::GetCurrentWindow()->DrawList;
                     ImVec2 canvas_p0 = ImGui::GetCursorScreenPos();//pos of screen top left
                     ImVec2 canvas_sz = ImGui::GetContentRegionAvail();   // Resize canvas to what's available
                     if (canvas_sz.x < 50.0f) canvas_sz.x = 50.0f;
                     if (canvas_sz.y < 50.0f) canvas_sz.y = 50.0f;
                     ImVec2 canvas_p1 = ImVec2(canvas_p0.x + canvas_sz.x, canvas_p0.y + canvas_sz.y);
                     drawlist->PushClipRect(canvas_p0, canvas_p1, true);


                     uint64_t totalframeTime = 0;
                     vector<uint64_t> SampleTimes;
                     vector<float> SampleWidths;
                     vector<string> SampleNames;
                     for (auto const& [SampleName, samples] : previousFrame)
                     {
                         totalframeTime += samples[0]->milliSecondsTaken + 1;
                         SampleTimes.push_back(samples[0]->milliSecondsTaken + 1);
                         SampleNames.push_back(SampleName);
                     }

                     float MinBlockWith = canvas_sz.x / totalframeTime;// GraphWindowWidth / totalframeTime;
                     for (int i = 0; i < SampleTimes.size(); i++)
                     {
                         SampleWidths.push_back(SampleTimes[i] * MinBlockWith);
                     }

                     //ImGui::LabelText("Total window width", std::to_string(GraphWindowWidth).c_str());
                     ImGui::LabelText("Total Frame Time", std::to_string(totalframeTime).c_str());
                     ImGui::LabelText("Window Width / total frame Time", std::to_string(MinBlockWith).c_str());
                     float TotalBlockWidthSoFar = 0;

                     int sampleCount = previousFrame.size();
                     //ImGui::SliderInt("Iterations", &sampleCount, 1, 10);
                     //sampleCount = 2;
                     //MinBlockWith = GraphWindowWidth / sampleCount;

                     const ImU32 col_outline_base = ImGui::GetColorU32(ImGuiCol_PlotHistogram) & 0x7FFFFFFF;
                     const ImU32 col_base = ImGui::GetColorU32(ImGuiCol_PlotHistogram) & 0x77FFFFFF;

                     float f = 5.0f + 5.0f;

                     for (int i = 0; i < sampleCount; i++)
                     {
                         float ThisBlockWidth = SampleWidths[i];
                         //if (ThisBlockWidth == 0)
                             //ThisBlockWidth = MinBlockWith;

                         const ImVec2 minPos = ImVec2(canvas_p0.x + TotalBlockWidthSoFar, canvas_p0.y + 100);
                         //float a = canvas_sz.x - (canvas_p1.x - (canvas_p1.x*i))
                         const ImVec2 maxPos = ImVec2(canvas_p0.x + TotalBlockWidthSoFar + ThisBlockWidth, canvas_p0.y + 200);
                         drawlist->AddRectFilled(
                             minPos,
                             maxPos,
                             col_base,
                             GImGui->Style.FrameRounding);

                         drawlist->AddRect(minPos, maxPos, col_outline_base);

                         ImGui::RenderText(ImVec2(minPos.x + 10, minPos.y + 10), SampleNames[i].c_str());
                         ImGui::RenderText(ImVec2(minPos.x + 10, minPos.y + 20), std::to_string(SampleTimes[i] - 1).c_str());


                         TotalBlockWidthSoFar += ThisBlockWidth;
                     }

                     drawlist->PopClipRect();
                     ImGui::End();

                     //checkinf selectedclean up//
                     if (SelectedObject != nullptr)
                     {
                         BaseEntity* SelectedObject = nullptr;
                     }


                     ImGui::Render();
                     ImGuiSDL::Render(ImGui::GetDrawData());
                     if (SelectedObject != nullptr)
                     {
                         BaseEntity* SelectedObject = nullptr;
                     }

                 }
             }
             for (int i = 0; SavedObjects.size() < i; i++)
             {
                 SavedObjects[i]->Draw();
             }
     }
     else if (currentScene == GAMEOVER)
     {
         ImGui::NewFrame();
         ImGui_ImplSDL2_NewFrame(m_Window);
         ImGui::Begin(".");
         bool clicked = ImGui::Button("Restart", ImVec2(0, 0));
         if (clicked)
         {
             isInPlay = false;
             currentScene = GAME;

         }
         ImGui::End();
         ImGui::Begin(".");
         bool clicked2 = ImGui::Button("ToEditor", ImVec2(0, 0));
         if (clicked2)
         {
             currentScene = TESTSCENE;

         }
         ImGui::End();
        
         ImGui::Render();
         ImGuiSDL::Render(ImGui::GetDrawData());
         
     }
     else if (currentScene == GAME)
     {
       

         std::cout << "frame \n";
         
         if (isInPlay ==false)// resets positions flappy and pipes on restart
         {
             {
                // PROFILE("Reset");
                 Flappy->transform.SetPosition(vec3(300, 250, 0));
                 int  y;
                 int tempx = 1;
                 for (int i = 1; i < Pipes.size(); i = i + 2)
                 {


                     if (Pipes[i]->topPipe == true)//if top pipe
                     {
                         y = rand() % (600 - 300 + 1) + 300;
                         Pipes[i]->transform.SetPosition(vec3(tempx, (y - 800), 0.0f));//buged
                         if (Pipes[i - 1]->topPipe == false) { Pipes[i - 1]->transform.SetPosition(vec3(tempx, y, 0.0f)); }

                     }
                     tempx += screenWidth / 4 + 1;
                 }
                 isInPlay = true;
             }

         }
         else
         {
             //////////////////////////////////////////GAMELOOP//////////////////////////////////////////////////
             eventSystem->HandelEvents();
             int  y=720;
             {
                 PROFILE("positionChecks");
                 for (int i = 0; i < Pipes.size(); i++)
                 {

                     if (Pipes[i]->transform.GetPosition().x <= 0)
                     {    //top pipe
                         if (Pipes[i]->topPipe == true)
                         {
                             Pipes[i]->transform = vec3{ screenWidth,(y - 800), 0.0f };
                         }
                         else
                         {
                             y = rand() % (600 - 300 + 1) + 300;
                             Pipes[i]->transform = vec3{ screenWidth, y, 0.0f };
                         }
                         seed = new Seed(m_Renderer, screenWidth, (y - 700));
                         Collectables.push_back(seed);

                     }

                 }
             }
             {
                 PROFILE("CollisionCheck");
                 for (int i = 0; i < Collectables.size(); i++)
                 {
                     if (Collectables[i]->transform.GetPosition().x <= 0)
                     {

                         Collectables.erase(Collectables.begin() + i);
                         Collectables[i] = nullptr;

                     }

                 }


                 score = score+1;
                 scoretxt = std::to_string(score);

                 //collision check/////////////////////////////////////////////////////////////
                 for (int i = 0; i < Entitys.size(); i++)
                 {
                     if (!(Entitys[i] == Flappy))
                     {
                         if (Flappy->CollisionCheck(Entitys[i]))
                         {
                             currentScene = GAMEOVER;
                             isInPlay = false;
                             //endGame
                         }

                     }

                 }
             }
             for (int i = 0; i < Collectables.size(); i++)
             {
                 
                     if (Flappy->CollisionCheck(Collectables[i]))
                     {
                         score++;
                         //endGame
                     }

             }

             {
                 PROFILE("ApplyMovment");
                 for (int i = 0; i < Entitys.size(); i++)
                 {
                     Entitys[i]->ApplyContinuousMoment();

                 }
             }
             
             {
                 PROFILE("Drawing");
                 for (int i = 0; i < Entitys.size(); i++)
                 {
                     Entitys[i]->Draw();
                 }
                 ////draw text
                 UpdateText(scoretxt, 50, 640, m_pSmallFont, { 0,0,0 });
             }

             //UpdateText("Small Red", 50, 10, m_pSmallFont, { 255,0,0 });
             //UpdateText("Small Blue", 50, 40, m_pSmallFont, { 0,0,255 });

             //char array[] = "Big White";
             //UpdateText(array, 50, 140, m_pBigFont, { 0,255,0 });

             //std::string myString = "Big Green";
             //UpdateText(myString, 50, 70, m_pBigFont, { 0,255,0 });

             //int testNumber = 1234;
             //std::string testString = "Test Number:  ";
             //testString += std::to_string(testNumber);
             //UpdateText(testString, 50, 210, m_pBigFont, { 255,255,255 });
             //  //phi->Move(Flappy->transform, vec3(1, -1, 0));

         }
     }

        
     SDL_RenderPresent(m_Renderer);

     SDL_Delay(16);//milliseconds
     ProfilerSystem::Instance().endFrame();
 }
 

 


     