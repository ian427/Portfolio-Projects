#include "Saveing.h"
#include "Entity3.h"
//#include <SDL.h>
#include "BitMaps.h"
#include "Transact.h"
#include "Components.h"
//#include <string>
#include <sstream>

 void Save::saveObject(const BaseEntity& obj, const std::string& filename)
{
    std::ofstream file(filename, std::ios::app); // Append mode
    if (file.is_open()) 
    {
        //convert object ot string 
        //serialise to binary
        float x = obj.transform.GetPosition().x;
        float y = obj.transform.GetPosition().y;
        float z = obj.transform.GetPosition().z;


        file  << obj.Radius << ", "  << obj.Map->FileName <<", " << obj.ID << ", " << obj.ObjectName << " " <<x << ", " <<y << ", " <<z << ", " << std::endl;
        
        file.close();
        std::cout << "Object saved to " << filename << std::endl;
    }
    else 
    {
        std::cerr << "Error: Could not open file for writing!\n";
    }
}


std::vector<BaseEntity> Save::loadObjects(const std::string& filename,SDL_Renderer* renderer)
{
    std::vector<BaseEntity> object;
    std::ifstream file(filename);

    std::string str;

    while (std::getline(file, str))
    {
        float Radius;
        Bitmap* Map;

        Transform* transform;//26,27 getting this transforms data not flappys
        int ID;
        std::string ObjectName;
        int x, y ;


        std::stringstream ss(str);
        vector<string> result;

        while (ss.good())
        {
            string substr;
            getline(ss, substr, ',');
            result.push_back(substr);
        }
        //split result into correct data type
        //serilised as//|radius,0|bitmapfilename,1|ID,2|objName,3|x,4|y,5|z,6|
        if(result.size() == 7 )
        { 
        Radius = std::stof( result[0]);
        x = std::stoi(result[4]);
        y = std::stoi(result[5]);
        //z = std::stoi(result[6]); 
        ID = std::stoi(result[2]);
        BaseEntity basic(renderer, Radius, result[1], x,y, ID, result[4]);
        object.push_back(basic);
        }
        //new object
    }
    file.close();
    return object;
}
