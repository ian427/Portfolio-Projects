#include <glew.h>
#include <glm.hpp>
#include <vector>
#include "Mesh.h"
#include"Vertex.h"
#include <../glm/gtx/normal.hpp>

Mesh::Mesh(Vertex* verts, unsigned int VertCount, unsigned int* indicies, unsigned int numIndices)
{
    CalculateTangentsBiTangents(verts, VertCount, indicies, numIndices);
    m_DrawCount = numIndices;
    vector<vec3> positions;
    vector<vec2> texcords;
    vector<vec3> Tangents;
    vector<vec3> BiTangents;

    for (unsigned int i = 0; i < VertCount; i++)
    {
        positions.push_back(verts[i].Position);
        texcords.push_back(verts[i].TextureCords);
        Tangents.push_back(verts[i].Tangent);
        BiTangents.push_back(verts[i].BiTangent);
    }

    vector<vec3> Normals;
    Normals.resize(VertCount);
    for (int i = 0; i < numIndices; i += 3)
    {
        vec3 Vert1 = positions[indicies[i]];
        vec3 Vert2 = positions[indicies[i + 1]];
        vec3 Vert3 = positions[indicies[i + 2]];

        vec3 normal = triangleNormal(Vert1, Vert2, Vert3);
        Normals[indicies[i]] += normal;
        Normals[indicies[i + 1]] += normal;
        Normals[indicies[i + 2]] += normal; // Fixed index here
    }

    Tangents[0] = vec3(2, 3, 4);

    glGenVertexArrays(1, &m_vertexArrayObject);
    glBindVertexArray(m_vertexArrayObject);

    glGenBuffers(NUM_BUFFERS, m_vertexBufferObjects);

    // Position
    glBindBuffer(GL_ARRAY_BUFFER, m_vertexBufferObjects[POSITION_VB]);
    glBufferData(GL_ARRAY_BUFFER, VertCount * sizeof(positions[0]), &positions[0], GL_STATIC_DRAW);
    glVertexAttribPointer(POSITION_VB, 3, GL_FLOAT, GL_FALSE, 0, NULL);
    glEnableVertexAttribArray(POSITION_VB);

    // Texture coordinates
    glBindBuffer(GL_ARRAY_BUFFER, m_vertexBufferObjects[TEXCOORD_VB]);
    glBufferData(GL_ARRAY_BUFFER, VertCount * sizeof(texcords[0]), &texcords[0], GL_STATIC_DRAW);
    glVertexAttribPointer(TEXCOORD_VB, 2, GL_FLOAT, GL_FALSE, 0, NULL);
    glEnableVertexAttribArray(TEXCOORD_VB);

    // Normals
    glBindBuffer(GL_ARRAY_BUFFER, m_vertexBufferObjects[NORMAL_VB]);
    glBufferData(GL_ARRAY_BUFFER, VertCount * sizeof(Normals[0]), &Normals[0], GL_STATIC_DRAW);
    glVertexAttribPointer(NORMAL_VB, 3, GL_FLOAT, GL_FALSE, 0, 0);
    glEnableVertexAttribArray(NORMAL_VB);

    

    //TANGENT
    glBindBuffer(GL_ARRAY_BUFFER, m_vertexBufferObjects[TANGENT_VB]);
    glBufferData(GL_ARRAY_BUFFER, VertCount * sizeof(Tangents[0]), &Tangents[0], GL_STATIC_DRAW);
    glVertexAttribPointer(TANGENT_VB, 3, GL_FLOAT, GL_FALSE, 0, 0);
    glEnableVertexAttribArray(TANGENT_VB);
    //BITANGENT
    glBindBuffer(GL_ARRAY_BUFFER, m_vertexBufferObjects[BITANGENT_VB]);
    glBufferData(GL_ARRAY_BUFFER, VertCount * sizeof(BiTangents[0]), &BiTangents[0], GL_STATIC_DRAW);
    glVertexAttribPointer(BITANGENT_VB, 3, GL_FLOAT, GL_FALSE, 0, 0);
    glEnableVertexAttribArray(BITANGENT_VB);

    // Indices
    glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, m_vertexBufferObjects[INDEX_VB]);
    glBufferData(GL_ELEMENT_ARRAY_BUFFER, numIndices * sizeof(unsigned int), indicies, GL_STATIC_DRAW);

    glBindVertexArray(0);

    m_Transform = Transform();
}

Mesh :: ~Mesh()
{
	glDeleteVertexArrays(NUM_BUFFERS, &m_vertexArrayObject);

}
void Mesh::Draw()
{
	glBindVertexArray(m_vertexArrayObject);
	glDrawElements(GL_TRIANGLES, m_DrawCount,GL_UNSIGNED_INT,0);
	glBindVertexArray(0);

}
void Mesh::CalculateTangentsBiTangents(Vertex* Verticies, unsigned int VertCount, unsigned int* Indecies, unsigned int NumIndicies)
{
    for (unsigned int i = 0; i < NumIndicies; i += 3)
    {
        Vertex V0 = Verticies[Indecies[i]];
        Vertex V1 = Verticies[Indecies[i + 1]];
        Vertex V2 = Verticies[Indecies[i + 2]];

        vec3 Edge1 = V1.Position - V0.Position;
        vec3 Edge2 = V2.Position - V0.Position;

        GLfloat DeltaU1 = V1.TextureCords.x - V0.TextureCords.x;
        GLfloat DeltaV1 = V1.TextureCords.y - V0.TextureCords.y;

        GLfloat DeltaU2 = V2.TextureCords.x - V0.TextureCords.x;
        GLfloat DeltaV2 = V2.TextureCords.y - V0.TextureCords.y;

        GLfloat f = 1.0f / (DeltaU1 * DeltaV2 - DeltaU2 * DeltaV1);

        vec3 Tangent;
        vec3 BiTangent;

        Tangent.x = f * (DeltaV2 * Edge1.x - DeltaV1 * Edge2.x);
        Tangent.y = f * (DeltaV2 * Edge1.y - DeltaV1 * Edge2.y);
        Tangent.z = f * (DeltaV2 * Edge1.z - DeltaV1 * Edge2.z);

        BiTangent.x = f * (DeltaU2 * Edge1.x - DeltaU1 * Edge2.x);
        BiTangent.y = f * (DeltaU2 * Edge1.y - DeltaU1 * Edge2.y);
        BiTangent.z = f * (DeltaU2 * Edge1.z - DeltaU1 * Edge2.z);

        V0.Tangent += Tangent;
        V1.Tangent += Tangent;
        V2.Tangent += Tangent;

        V0.BiTangent += BiTangent;
        V1.BiTangent += BiTangent;
        V2.BiTangent += BiTangent;

        Verticies[Indecies[i]] = V0;
        Verticies[Indecies[i + 1]] = V1;
        Verticies[Indecies[i + 2]] = V2;


    }
    for (unsigned int i = 0; i < VertCount; i++)
    {

        Verticies[i].Tangent = normalize(Verticies[i].Tangent);
        Verticies[i].BiTangent = normalize(Verticies[i].BiTangent);

    }

}
