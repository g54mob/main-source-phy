using GLTFast;
using GLTFast.Logging;
using GLTFast.Materials;
using GLTFast.Schema;
using UnityEngine;

public class GltfToHdrpMaterialGenerator : IMaterialGenerator
{
	private static readonly int baseMapId = Shader.PropertyToID("_BaseColorMap");

	private static readonly int baseColorId = Shader.PropertyToID("_BaseColor");

	private static readonly int maskMapId = Shader.PropertyToID("_MaskMap");

	private static readonly int metallicId = Shader.PropertyToID("_Metallic");

	private static readonly int smoothnessId = Shader.PropertyToID("_Smoothness");

	private static readonly int normalMapId = Shader.PropertyToID("_NormalMap");

	private static readonly int normalScaleId = Shader.PropertyToID("_NormalScale");

	private static readonly int enableNormalMapId = Shader.PropertyToID("_EnableNormalMap");

	private static readonly int emissiveMapId = Shader.PropertyToID("_EmissiveColorMap");

	private static readonly int emissiveColorId = Shader.PropertyToID("_EmissiveColor");

	private static readonly int emissiveExposureWeightId = Shader.PropertyToID("_EmissiveExposureWeight");

	private static readonly int enableEmissiveId = Shader.PropertyToID("_EnableEmissive");

	private static readonly int alphaCutoffEnableId = Shader.PropertyToID("_AlphaCutoffEnable");

	private static readonly int alphaCutoffId = Shader.PropertyToID("_AlphaCutoff");

	private static readonly int surfaceTypeId = Shader.PropertyToID("_SurfaceType");

	private static readonly int blendModeId = Shader.PropertyToID("_BlendMode");

	private static readonly int doubleSidedId = Shader.PropertyToID("_DoubleSidedEnable");

	private readonly UnityEngine.Material defaultMaterial = new UnityEngine.Material(Shader.Find("HDRP/Lit"));

	public UnityEngine.Material GetDefaultMaterial(bool isOpaque = true)
	{
		return defaultMaterial;
	}

	public UnityEngine.Material GenerateMaterial(MaterialBase gltfMaterialBase, IGltfReadable gltf, bool report = true)
	{
		if (!(gltfMaterialBase is GLTFast.Schema.Material material))
		{
			Debug.LogWarning("Material is not a valid glTF material. Returning default material.");
			return defaultMaterial;
		}
		UnityEngine.Material material2 = new UnityEngine.Material(defaultMaterial)
		{
			name = (material.name ?? "GLTF_Material")
		};
		if (material.pbrMetallicRoughness.baseColorTexture != null)
		{
			Texture2D texture = gltf.GetTexture(material.pbrMetallicRoughness.baseColorTexture.index);
			if (texture != null)
			{
				material2.SetTexture(baseMapId, texture);
			}
		}
		material2.SetColor(value: new Color(material.pbrMetallicRoughness.baseColorFactor[0], material.pbrMetallicRoughness.baseColorFactor[1], material.pbrMetallicRoughness.baseColorFactor[2], material.pbrMetallicRoughness.baseColorFactor[3]), nameID: baseColorId);
		material2.SetFloat(metallicId, material.pbrMetallicRoughness.metallicFactor);
		material2.SetFloat(smoothnessId, 1f - material.pbrMetallicRoughness.roughnessFactor);
		if (material.normalTexture != null)
		{
			Texture2D texture2 = gltf.GetTexture(material.normalTexture.index);
			if (texture2 != null)
			{
				material2.SetTexture(normalMapId, texture2);
				material2.SetFloat(normalScaleId, material.normalTexture.scale);
				material2.SetFloat(enableNormalMapId, 1f);
			}
		}
		bool flag = false;
		if (material.emissiveTexture != null)
		{
			Texture2D texture3 = gltf.GetTexture(material.emissiveTexture.index);
			if (texture3 != null)
			{
				material2.SetTexture(emissiveMapId, texture3);
				flag = true;
			}
		}
		if (material.Emissive != Color.black)
		{
			material2.SetColor(emissiveColorId, material.Emissive);
			material2.SetFloat(emissiveExposureWeightId, 1f);
			flag = true;
		}
		if (flag)
		{
			material2.SetFloat(enableEmissiveId, 1f);
		}
		Texture2D texture2D = null;
		if (material.pbrMetallicRoughness.metallicRoughnessTexture != null)
		{
			texture2D = gltf.GetTexture(material.pbrMetallicRoughness.metallicRoughnessTexture.index);
		}
		Texture2D texture2D2 = null;
		if (material.occlusionTexture != null)
		{
			texture2D2 = gltf.GetTexture(material.occlusionTexture.index);
		}
		if (texture2D != null || texture2D2 != null)
		{
			Texture2D texture2D3 = CreateHdrpMaskMap(texture2D, texture2D2);
			if (texture2D3 != null)
			{
				material2.SetTexture(maskMapId, texture2D3);
			}
		}
		switch (material.GetAlphaMode())
		{
		case MaterialBase.AlphaMode.Mask:
			material2.SetFloat(alphaCutoffEnableId, 1f);
			material2.SetFloat(alphaCutoffId, material.alphaCutoff);
			material2.SetFloat(surfaceTypeId, 0f);
			break;
		case MaterialBase.AlphaMode.Blend:
			material2.SetFloat(surfaceTypeId, 1f);
			material2.SetFloat(blendModeId, 0f);
			break;
		default:
			material2.SetFloat(surfaceTypeId, 0f);
			break;
		}
		if (material.doubleSided)
		{
			material2.SetFloat(doubleSidedId, 1f);
		}
		return material2;
	}

	private Texture2D CreateHdrpMaskMap(Texture2D mrTexture, Texture2D occTexture)
	{
		Texture2D texture2D = ((mrTexture != null) ? mrTexture : occTexture);
		if (texture2D == null)
		{
			return null;
		}
		if (!texture2D.isReadable)
		{
			Debug.LogWarning("Texture " + texture2D.name + " is not readable. MaskMap generation skipped.");
			return null;
		}
		int width = texture2D.width;
		int height = texture2D.height;
		Color[] array = ((mrTexture != null) ? mrTexture.GetPixels() : null);
		Color[] array2 = ((occTexture != null) ? occTexture.GetPixels() : null);
		Color[] array3 = new Color[width * height];
		for (int i = 0; i < array3.Length; i++)
		{
			float r = array?[i].b ?? 0f;
			float g = array2?[i].r ?? 1f;
			float b = 1f;
			float a = ((array != null) ? (1f - array[i].g) : 1f);
			array3[i] = new Color(r, g, b, a);
		}
		Texture2D texture2D2 = new Texture2D(width, height, TextureFormat.RGBA32, mipChain: true, linear: true);
		texture2D2.name = "HDRP_MaskMap";
		texture2D2.SetPixels(array3);
		texture2D2.Apply(updateMipmaps: true, makeNoLongerReadable: false);
		return texture2D2;
	}

	public void SetLogger(ICodeLogger logger)
	{
	}
}
