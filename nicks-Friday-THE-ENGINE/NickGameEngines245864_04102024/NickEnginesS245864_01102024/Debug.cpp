#include <string> 
#include "Debug.h" 
#include <cstdio>  
#include <cstdarg>
#include <string>
///NOTE/////////////////////////////////////////////////////the logger has been done this way to preserve the errors that happened in the event of a crash
#define _CRT_SECURE_NO_WARNINGS
DeBug::DeBug()
{    
    
    // Open 
    fopen_s(&DebugFile ,"DebugLogger.txt", "a");

    //error check
    if (DebugFile == nullptr)
    {
      
        printf("Error: Unable to open DebugLogger.txt\n");
        return;
    }
  fprintf(DebugFile, "New  Log \n");

}
DeBug* DeBug::_Log = NULL;

DeBug* DeBug::Log()
{
    
    if (!_Log)
    {
        _Log = new DeBug();
        //_Log->Init();
    }

    return _Log;
}


 void DeBug::printDebug( int Verbosity , std::string ERRORMSG, ...)//called to write log entry
{
     

     std::string VerbosityType[] = { "|none|","|error_red|","|error_yellow|","|error_green|","|warning|","|note|"}
      ;//|0| not classed // |1|system|game breaking/ could cause crash// |2|error visiable to player//|3|error not visiable to player//  |4|happened recovered from|5| thing has happened
     
     
    
     //format
     va_list args_list;
     va_start(args_list, ERRORMSG.c_str());
     
     char MessageBuffer[256];
     vsnprintf(MessageBuffer, 256, ERRORMSG.c_str(), args_list); 
    va_end(args_list);
     


    ERRORNUM++;
    fprintf(DebugFile, "Error %d: %s%s \n", ERRORNUM, VerbosityType[Verbosity].c_str(), MessageBuffer);
    printf("Error %d: %s %s \n", ERRORNUM, VerbosityType[Verbosity].c_str(), MessageBuffer);

}


 DeBug::~DeBug()
 {
     // Close the file
     fclose(DebugFile);
 }