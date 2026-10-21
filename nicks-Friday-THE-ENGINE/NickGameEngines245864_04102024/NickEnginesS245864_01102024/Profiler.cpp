#include "Profiler.h"
#include <chrono>

void Profile::timeprocess()
{
    milliSecondsTaken = end - start;
}

void Profile::SetStart()
{
    start = std::clock();
}

void Profile::SetEnd()
{
    end = std::clock();

}

float Profile::Gettime()
{
    return milliSecondsTaken;
}



ProfilerSystem::ProfilerSystem()
{
    frameData = new FrameMap();
    thisFramesFrameData = new FrameMap();
    lastFramesFrameData = new FrameMap();
}

ProfilerSystem::~ProfilerSystem()
{
}

void ProfilerSystem::startframe()
{
    currentFrame++;
    thisFramesTotalTime = 0;
}

void ProfilerSystem::endFrame()
{
    totalFrameTimes.push_back(thisFramesTotalTime);
    lastFramesFrameData = thisFramesFrameData;
    thisFramesFrameData = new FrameMap();
}

void ProfilerSystem::storeProfiledata(const char* name, float timetaken)
{
    ProfileData* data = new ProfileData();
    data->milliSecondsTaken = timetaken;
    data->Profilename = name;
    thisFramesTotalTime += timetaken;

    (*frameData)[name].push_back(data);
    (*thisFramesFrameData)[name].push_back(data);
}




void Frame::Calctotaltime()
{
    for (int i = 0; i < Thisframe.size(); i++)
    {
        frameTime = frameTime + Thisframe[i].milliSecondsTaken;
    }
   
}
