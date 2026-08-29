using UnityEngine;

namespace GLTFast.Materials
{
	public static class MaterialProperty
	{
		public static readonly int AlphaCutoff = Shader.PropertyToID("alphaCutoff");

		public static readonly int BaseColor = Shader.PropertyToID("baseColorFactor");

		public static readonly int BaseColorTexture = Shader.PropertyToID("baseColorTexture");

		public static readonly int BaseColorTextureRotation = Shader.PropertyToID("baseColorTexture_Rotation");

		public static readonly int BaseColorTextureScaleTransform = Shader.PropertyToID("baseColorTexture_ST");

		public static readonly int BaseColorTextureTexCoord = Shader.PropertyToID("baseColorTexture_texCoord");

		public static readonly int Cull = Shader.PropertyToID("_Cull");

		public static readonly int CullMode = Shader.PropertyToID("_CullMode");

		public static readonly int DstBlend = Shader.PropertyToID("_DstBlend");

		public static readonly int DiffuseFactor = Shader.PropertyToID("diffuseFactor");

		public static readonly int DiffuseTexture = Shader.PropertyToID("diffuseTexture");

		public static readonly int DiffuseTextureScaleTransform = Shader.PropertyToID("diffuseTexture_ST");

		public static readonly int DiffuseTextureRotation = Shader.PropertyToID("diffuseTexture_Rotation");

		public static readonly int DiffuseTextureTexCoord = Shader.PropertyToID("diffuseTexture_texCoord");

		public static readonly int EmissiveFactor = Shader.PropertyToID("emissiveFactor");

		public static readonly int EmissiveTexture = Shader.PropertyToID("emissiveTexture");

		public static readonly int EmissiveTextureRotation = Shader.PropertyToID("emissiveTexture_Rotation");

		public static readonly int EmissiveTextureScaleTransform = Shader.PropertyToID("emissiveTexture_ST");

		public static readonly int EmissiveTextureTexCoord = Shader.PropertyToID("emissiveTexture_texCoord");

		public static readonly int GlossinessFactor = Shader.PropertyToID("glossinessFactor");

		public static readonly int NormalTexture = Shader.PropertyToID("normalTexture");

		public static readonly int NormalTextureRotation = Shader.PropertyToID("normalTexture_Rotation");

		public static readonly int NormalTextureScaleTransform = Shader.PropertyToID("normalTexture_ST");

		public static readonly int NormalTextureTexCoord = Shader.PropertyToID("normalTexture_texCoord");

		public static readonly int NormalTextureScale = Shader.PropertyToID("normalTexture_scale");

		public static readonly int Metallic = Shader.PropertyToID("metallicFactor");

		public static readonly int MetallicRoughnessMap = Shader.PropertyToID("metallicRoughnessTexture");

		public static readonly int MetallicRoughnessMapScaleTransform = Shader.PropertyToID("metallicRoughnessTexture_ST");

		public static readonly int MetallicRoughnessMapRotation = Shader.PropertyToID("metallicRoughnessTexture_Rotation");

		public static readonly int MetallicRoughnessMapTexCoord = Shader.PropertyToID("metallicRoughnessTexture_texCoord");

		public static readonly int Mode = Shader.PropertyToID("_Mode");

		public static readonly int OcclusionTexture = Shader.PropertyToID("occlusionTexture");

		public static readonly int OcclusionTextureStrength = Shader.PropertyToID("occlusionTexture_strength");

		public static readonly int OcclusionTextureRotation = Shader.PropertyToID("occlusionTexture_Rotation");

		public static readonly int OcclusionTextureScaleTransform = Shader.PropertyToID("occlusionTexture_ST");

		public static readonly int OcclusionTextureTexCoord = Shader.PropertyToID("occlusionTexture_texCoord");

		public static readonly int RoughnessFactor = Shader.PropertyToID("roughnessFactor");

		public static readonly int SpecularFactor = Shader.PropertyToID("specularFactor");

		public static readonly int SpecularGlossinessTexture = Shader.PropertyToID("specularGlossinessTexture");

		public static readonly int SpecularGlossinessTextureScaleTransform = Shader.PropertyToID("specularGlossinessTexture_ST");

		public static readonly int SpecularGlossinessTextureRotation = Shader.PropertyToID("specularGlossinessTexture_Rotation");

		public static readonly int SpecularGlossinessTextureTexCoord = Shader.PropertyToID("specularGlossinessTexture_texCoord");

		public static readonly int SrcBlend = Shader.PropertyToID("_SrcBlend");

		public static readonly int ZWrite = Shader.PropertyToID("_ZWrite");

		public static readonly int AlphaClip = Shader.PropertyToID("_AlphaClip");

		public static readonly int Surface = Shader.PropertyToID("_Surface");

		public static readonly int AlphaCutoffEnable = Shader.PropertyToID("_AlphaCutoffEnable");

		public static readonly int DoubleSidedEnable = Shader.PropertyToID("_DoubleSidedEnable");

		public static readonly int EnableBlendModePreserveSpecularLighting = Shader.PropertyToID("_EnableBlendModePreserveSpecularLighting");

		public static readonly int SurfaceType = Shader.PropertyToID("_SurfaceType");
	}
}
