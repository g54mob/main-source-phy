using System.Collections.Generic;
using BitCode;
using UnityEngine;

namespace TFBGames
{
	[DisallowMultipleComponent]
	public class FoliageManager : Singleton<FoliageManager>
	{
		private struct MaterialInfo
		{
			public Material Material;

			public float OriginalFadeDistance;

			public float OriginalFadeRange;

			public float OriginalWorldFadeOffset;
		}

		public static readonly int FadeOutRange = Shader.PropertyToID("_FadeOutRange");

		public static readonly int InvFadeOutRange = Shader.PropertyToID("_InvFadeOutRange");

		public static readonly int FadeOutDistance = Shader.PropertyToID("_FadeOutDistance");

		public static readonly int WorldFadeOffset = Shader.PropertyToID("_WorldNoiseFadeOffset");

		public static readonly int StippleNoise = Shader.PropertyToID("_StippleNoise");

		public static readonly int ScreenToNoiseRatio = Shader.PropertyToID("_ScreenToNoiseRatio");

		[SerializeField]
		protected float globalAmplitude = 0.5f;

		[SerializeField]
		protected float globalSpeed = 4f;

		[SerializeField]
		protected float globalScale = 0.5f;

		[Space(20f)]
		[SerializeField]
		protected Texture2D noiseTexture;

		[SerializeField]
		protected float noiseSpeed = 1f;

		[SerializeField]
		protected float noiseScale = 1f;

		private Dictionary<Material, MaterialInfo> changedMaterials = new Dictionary<Material, MaterialInfo>();

		private List<FadingFoliage> foliageInstances = new List<FadingFoliage>();

		private SettingsProfileManager profileManager;

		protected override void Awake()
		{
			base.Awake();
			profileManager = ServiceLocator.GetService<SettingsProfileManager>();
			profileManager.SettingsProfileChanged += UpdateMaterialDistances;
			SetGlobalShaderProperties();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			profileManager.SettingsProfileChanged -= UpdateMaterialDistances;
			ResetAllMaterials();
		}

		public void RegisterFoliageMaterial(FadingFoliage foliageInstance, Material material)
		{
			foliageInstances.Add(foliageInstance);
			SettingsProfile currentSettingsProfile = profileManager.CurrentSettingsProfile;
			float multiplier = ((currentSettingsProfile != null) ? currentSettingsProfile.FoliageFadeDistanceMultiplier : 1f);
			if (!changedMaterials.TryGetValue(material, out var value))
			{
				value = new MaterialInfo
				{
					Material = material,
					OriginalFadeDistance = material.GetFloat(FadeOutDistance),
					OriginalFadeRange = material.GetFloat(FadeOutRange),
					OriginalWorldFadeOffset = material.GetFloat(WorldFadeOffset)
				};
				changedMaterials.Add(material, value);
			}
			UpdateMaterial(value, multiplier);
		}

		private void UpdateMaterialDistances(SettingsProfile newProfile)
		{
			float multiplier = ((newProfile != null) ? newProfile.FoliageFadeDistanceMultiplier : 1f);
			foreach (MaterialInfo value in changedMaterials.Values)
			{
				UpdateMaterial(value, multiplier);
			}
			foreach (FadingFoliage foliageInstance in foliageInstances)
			{
				foliageInstance.RecalculateFadeDistance();
			}
		}

		private void ResetAllMaterials()
		{
			foreach (MaterialInfo value in changedMaterials.Values)
			{
				UpdateMaterial(value, 1f);
			}
		}

		private void UpdateMaterial(MaterialInfo matInfo, float multiplier)
		{
			Material material = matInfo.Material;
			material.SetFloat(FadeOutDistance, matInfo.OriginalFadeDistance * multiplier);
			material.SetFloat(FadeOutRange, matInfo.OriginalFadeRange * multiplier);
			material.SetFloat(WorldFadeOffset, matInfo.OriginalWorldFadeOffset * multiplier);
		}

		private void SetGlobalShaderProperties()
		{
			Shader.SetGlobalVector("Foliage_GlobalDirection", base.gameObject.transform.forward);
			Shader.SetGlobalFloat("Foliage_GlobalAmplitude", globalAmplitude);
			Shader.SetGlobalFloat("Foliage_GlobalSpeed", globalSpeed);
			Shader.SetGlobalFloat("Foliage_GlobalScale", globalScale);
			Shader.SetGlobalTexture("Foliage_NoiseTex", noiseTexture);
			Shader.SetGlobalFloat("Foliage_NoiseSpeed", noiseSpeed * 0.1f);
			Shader.SetGlobalFloat("Foliage_NoiseScale", noiseScale * 0.1f);
		}
	}
}
