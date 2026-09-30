#version 450



uniform sampler2D texture_diffuse;
uniform sampler2D texture_normal;
uniform sampler2D texture_spec;
uniform sampler2D texture_Shadow;
//uniform mat4 lightSpaceMatrix; <<<<<<<<<<<<<

in vec2  FragTextureCords ;
in vec3 FragNormal;
in vec3 FragPos;
in mat3 TBN;

in vec3 TangentLightColor;
in vec3 TangentLightPos;
in vec3 TangentViewPos;
in vec3 TangentFragPos;

in vec4 FragPosLightSpace;

out vec4 frag_color;
//"uniform vec3 color;"


float CalculateShadowValue(vec4 fragposLightSpace)
{
	vec3 projCords =fragposLightSpace.xyz/fragposLightSpace.w ;
	projCords = projCords * 0.5 + 0.5 ;

	float CloseestDepth = texture2D(texture_Shadow ,projCords.xy).r;
	float CurrentDepth = projCords.z;
	float bias = 0.00005;

	float shadow = 0.0 ;
	vec2 texelSize = 1.0 / textureSize(texture_Shadow,0);
	for(int x = -1;x <= 1;++x)
	{
		for ( int y =-1 ; y<= 1 ; ++y )
		{
			float pcfDepth =texture2D(texture_Shadow,projCords.xy + vec2(x,y) * texelSize).r;
			shadow += CurrentDepth - bias >pcfDepth ? 1.0 : 0.0 ;
		}
	}
	shadow /= 9.0;

	if(projCords.z > 1.0)
	{
		shadow = 0 ;
	}
	return shadow;

}






void main()
{
	vec3 normal = texture2D(texture_normal, FragTextureCords).rgb;

	normal = normalize(normal *2.0 -1.0);

	vec3 color = texture2D(texture_diffuse, FragTextureCords).rgb;

	vec3 ambiant = 0.2* color;

	vec3 lightDir = normalize( TangentLightPos - TangentFragPos);

	float diff = max(dot(lightDir, normal), 0.0);

	vec3 diffuse = diff * color;

	vec3 viewDir = normalize(TangentViewPos - TangentFragPos);
	vec3 reflictDir = reflect(-lightDir, normal);
	vec3 halfwayDir = normalize(lightDir + viewDir);
	float spec = pow(max(dot( normal, reflictDir), 0.0), 32.0);

	vec3 specular = vec3(0.2)*spec;

	frag_color = vec4(ambiant+ diffuse+ specular, 1.0);
	///frag_color = vec4(vec3(1), 1.0);
	//frag_color = vec4(texture2D(texture_Shadow,FragTextureCords).rgb,0);


	float shadow = CalculateShadowValue(FragPosLightSpace);
	frag_color = vec4((ambiant + (1.0 -shadow) ) * (diffuse +specular),0);
	//frag_color = vec4(ambiant +(1-shadow),0);
}























//
//#version 450
//
//uniform vec3 FragLightColor;
//uniform vec3 FragLightPos;
//uniform vec3 FragCamPos;
//
//uniform sampler2D texture_diffuse;
//uniform sampler2D texture_normal;
//in vec2  FragTextureCords ;
//in vec3 FragNormal;
//in vec3 FragPos;
//in mat3 TBN;
//out vec4 frag_color;
////"uniform vec3 color;"
//void main()
//{
////frag_colour = vec4 (vec3(FragTextureCords.x,FragTextureCords.y,0),1.0f);
////frag_colour = vec4(vec3(1,0,0),1.0f);
//  frag_color=vec4(texture2D (texture_diffuse, FragTextureCords).rgb,1);
//
////diffuse
////vec3 normal = normalize(FragNormal);
//
//vec3 normal = normalize(texture2D(texture_normal,FragTextureCords).rgb);
//normal = normalize (normal * 2.0 - 1.0);
////normal = normalize (TBN*normal);
//
//
//vec3 lightDir = normalize(FragLightPos - FragPos);
//
//
//float diff = max (dot(normal,lightDir),0.0);
//vec3 diffuse =diff * FragLightColor ;
//
////speculare
//float specularStrength = 10f;
//
//
//vec3 viewDir =  normalize (FragCamPos-FragPos);
//
//
//vec3 reflectDir = reflect (-lightDir,normal);
//float spec = pow(max(dot(normal,reflectDir),0.0),32.0);
//vec3 specular = vec3 (specularStrength * spec);
////ambiant
//float ambiantStrength = 0.5;
//vec3 ambiant = ambiantStrength * FragLightColor;
//
//vec4 result =vec4(texture2D(texture_diffuse,FragTextureCords).rgb *(ambiant+diffuse + specular),1);            
//frag_color = result;
////frag_color = vec4(FragPos,0);
//}