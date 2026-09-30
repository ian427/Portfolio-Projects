#pragma once
#ifndef LIGHTBASE_H
#define LIGHTBASE_H

#include "Camera.h"
#include "Mesh.h"
class LightBase {
public:
    Transform m_Transform;
    LightBase();
    ~LightBase();
    

    
    vec3 M_Color = vec3(0.5f, 0.5f, 0.5f);
    // method to update the light's position
    void SetLightPosition(const vec3& newPosition) {
        m_Transform.SetPosition(newPosition);  // Ensure you're using SetPosition() from Transform
    }

    void DrawLight(Camera* Cam);
};


#endif // !LIGHTBASE_H

