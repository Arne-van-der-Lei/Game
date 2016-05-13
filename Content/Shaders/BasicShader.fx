#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

matrix WorldViewProjection;
sampler TextureSampler : register(s0);
float4 sunNormal;

struct VertexShaderInput
{
	float4 Position : SV_POSITION;
	float2 Texture : TEXCOORD0;
	float4 Normal : NORMAL0;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float2 Texture : TEXCOORD0;
	float4 Normal : COLOR0;
};

VertexShaderOutput MainVS(in VertexShaderInput input)
{
	VertexShaderOutput output = (VertexShaderOutput)0;

	output.Position = mul(input.Position, WorldViewProjection);
	output.Texture = input.Texture;
	output.Normal = input.Normal;
	return output;
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
	float4 Color = tex2D(TextureSampler, input.Texture.xy);
	float alpha = Color.w;
	Color = Color * saturate(dot(input.Normal, sunNormal * -1));
	Color.w = alpha;
	Color.x /= 1.3;
	Color.y /= 1.1;
	Color.z /= 1;
	return Color;
}

technique gameShaderBase
{
	pass P0
	{
		AlphaBlendEnable = TRUE;
		DestBlend = INVSRCALPHA;
		SrcBlend = SRCALPHA;
		VertexShader = compile VS_SHADERMODEL MainVS();
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};