#pragma once

#include <vector>
#include <string>
#include <ctime>
#include <SDL.h>
#include <map>
#define PROFILE(name) Profile p(name)
struct ProfileData
{
	const char* Profilename;
	float milliSecondsTaken;

};
class Frame
{
	int FrameNum = 0;
	float frameTime;
	std::vector<ProfileData> Thisframe;
	void Calctotaltime();
};
typedef std::map<const char*, std::vector<struct ProfileData*>> FrameMap;
class ProfilerSystem
{

public:
	static ProfilerSystem& Instance()
	{
		static ProfilerSystem INSTANCE;
		return INSTANCE;
	}
	ProfilerSystem();
	~ProfilerSystem();
	std::vector<Frame> Profiler;
	void startframe();
	void endFrame();

	void storeProfiledata(const char* name, float timetaken);

	FrameMap& GetLastFrameData() const
	{
		return *lastFramesFrameData;
	}
	FrameMap& GetFrameDate() const
	{
		return *frameData;
	}


	int GetCurrentFrame()
	{
		return currentFrame;
	}
	
		std::vector<float>&GetFrameTimes();
	

private:
	int currentFrame;
	FrameMap* frameData;
	std::vector<float> totalFrameTimes;
	float thisFramesTotalTime = 0;
	FrameMap* thisFramesFrameData;
	FrameMap* lastFramesFrameData;

};
struct Profile
{
	const char* Profilename;
	float milliSecondsTaken;
	float start, end;
	void timeprocess();
	void SetStart();
	void SetEnd();
	float Gettime();
	Profile(const char* name )
	{
		SetStart();
		Profilename = name;
	}
	~Profile()
	{
		
		SetEnd();
		timeprocess();

		ProfilerSystem::Instance().storeProfiledata(Profilename, (1000 * Gettime()) / CLOCKS_PER_SEC);

	}

};

struct FrameTimeStack
{
public:
	std::vector<float> FrameTimeQueue;
	int Capacity = 240;
	void push(float FrameTime)
	{
		int diff = FrameTimeQueue.size() - Capacity;
		if (diff > 0)
		{
			for (int i = 0; i <= diff; i++)
			{
				FrameTimeQueue.erase(FrameTimeQueue.begin());
			}
		}

		FrameTimeQueue.push_back(FrameTime);
	}

	float AverageTime()
	{
		float average = 0;
		for (int i = 0; i < FrameTimeQueue.size(); i++)
			average += FrameTimeQueue[i];

		return average / FrameTimeQueue.size();
	}

};
