#version 450

in vec3 VertexPosition ;
in vec2 TextureCords ;
in vec3 Normal;
in vec3 Tangent ;
in vec3 BiTangent;
uniform mat4 model ;
uniform mat4 view ;
uniform mat4 projection ;
uniform mat4 lightSpaceMatrix;

uniform vec3 LightColor;
uniform vec3 LightPos;
uniform vec3 ViewPos;



out vec2 FragTextureCords;
out vec3 FragNormal;
out vec3 FragPos;
out mat3 TBN ;
out vec4 FragPosLightSpace;
out vec3 VsNormal ;


out vec3 TangentLightColor;
out vec3 TangentLightPos;
out vec3 TangentViewPos;
out vec3 TangentFragPos;


void main()
{
	FragTextureCords = TextureCords;

    FragPos = vec3(model * vec4(VertexPosition, 1.0));
	mat3 normalMAtrix = transpose(inverse(mat3(model)));

	FragPosLightSpace = lightSpaceMatrix * vec4(FragPos,1.0);
	VsNormal = Normal;
	vec3 T = normalize(normalMAtrix * Tangent);
	vec3 N = normalize(normalMAtrix * Normal);
	T = normalize(T -dot(T,N)*N);
	vec3 B = cross(N,T);
	TBN = transpose(mat3(T,B,N));

	TangentLightPos =TBN * LightPos;
	TangentViewPos = TBN * ViewPos;
	TangentFragPos = TBN * FragPos;

	
	 gl_Position = projection * view * model *vec4 (VertexPosition,1.0);
	 //FragPos = vec3(model* vec4(VertexPosition,1.0f));
	 //FragNormal = mat3(transpose(inverse(model))) *Normal;
	//vec3 T = normalize(vec3(model* vec4(Tangent,0.0)));
	//vec3 B = normalize(vec3(model* vec4(BiTangent,0.0)));
	//vec3 N = normalize(vec3(model* vec4(Normal,0.0)));

	
}









//#version 450
//
//in vec3 VertexPosition ;
//in vec2 TextureCords ;
//in vec3 Normal;
//in vec3 Tangent ;
//in vec3 BiTangent;
//uniform mat4 model ;
//uniform mat4 view ;
//uniform mat4 projection ;
//
//out vec2 FragTextureCords;
//out vec3 FragNormal;
//out vec3 FragPos;
//out mat3 TBN ;
//
//
//
//void main()
//{
//	FragTextureCords = TextureCords ;
//	 gl_Position = projection * view * model *vec4 (VertexPosition,1.0);
//	 FragPos = vec3(model* vec4(VertexPosition,1.0f));
//	 FragNormal = mat3(transpose(inverse(model))) *Normal;
//	vec3 T = normalize(vec3(model* vec4(Tangent,0.0)));
//	vec3 B = normalize(vec3(model* vec4(BiTangent,0.0)));
//	vec3 N = normalize(vec3(model* vec4(Normal,0.0)));
//
//	TBN = mat3(T,B,N);
//}