
#ifndef DEBUGLOGGER_H
#define DEBUGLOGGER_H
#include <iostream>
// Declare the DeBug function in the header
class DeBug
{
	static DeBug * _Log;
	FILE* DebugFile;
	std::string ERRORMSG = "NULL";
	std::string ERRORFILE = "NULL";
    
    DeBug();
	~DeBug();
public:
	int ERRORNUM = 0;
	static DeBug* Log();
	void printDebug( int Verbosity  ,std::string ERRORMSG, ...);
	enum Verbosity
	{
		none, //|0| not classed 
		red, // |1|system|game breaking/ could cause crash
		yellow,// |2|error visiable to player
		error_green,//  |3|error not visiable to player
		warning,//  |4|happened recovered from
		note// |5| thing has happened


	};
};
	

#endif