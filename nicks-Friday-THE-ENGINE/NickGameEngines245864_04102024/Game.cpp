#include <SDL.h>
#include <iostream>
#include <Game.h>


int game ()
{
 m_window = nullptr;
  m_renderer = nullptr;

  //start up
  SDL_Init(SDL_INIT_Video);

  //CreateWindow
  m_Window = SDL_CreateWindow(
  "Main Window",
   250, // XPosition
   50, // YPosition
   640, //Width in pixels
   480,  //height pixels
    0 //behaviour flags
   );
  if (!m_Window)
  {
   printf ("WINDOW initialsation FAILED\n"),SDL_GetErrot();
   printf ("PRESS ANY KEY\n");
   getchar();
   return ;
   
  } 
  
   m_renderer =SDL_CreateRenderer(
   m_window, //links to window
   -1 ,      // indexes render driver
    0         //behaviour flags
   );
   if (!m_renderer)
    {
        printf ("RENDERER initialsation FAILED\n"),SDL_GetErrot();
   printf ("PRESS ANY KEY\n");
   getchar();
   return ;
       
    }
   

   // destroy in reverse creation order
   if(m_renderer)
    {
      SDL_DestroyRenderer(m_renderer);
    }
   if(m_window)
    {
      SDL_DestroyWindow(m_window );

    }
   void Game::SetDisplayColour(void)
   {
    if(m_renderer)
{
    int result = SDL_SetRendeDrawColor(
        m_renderer //target renderer
        255, 0,0,255, //r,g,b,alpha
        );
     //wipe display
    SDL_RederClear(m_renderer);
     //present whats drawn
      SDL_RenderPresent(m_renderer);
//pause 5 seconds
    SDL_Delay (5000);//milli seconds
 

   }
  

}
