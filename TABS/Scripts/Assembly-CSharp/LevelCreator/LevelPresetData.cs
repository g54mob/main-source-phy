using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Serialization;

namespace LevelCreator
{
	[CreateAssetMenu(fileName = "LevelPreset", menuName = "LevelMenu/LevelPreset", order = 0)]
	public class LevelPresetData : ScriptableObject
	{
		public string PresetName;

		[Tooltip("The custom map templates (.tld) files associated with this preset.")]
		public Object[] MapFiles;

		[HideInInspector]
		[SerializeField]
		private string[] mapFilesNames;

		public string LocalizedName;

		public Sprite PresetIcon;

		public PostProcessProfile PostProcessProfile;

		[Space]
		public Material Skybox;

		[Space]
		public Material WaterMaterial;

		[Space]
		public string Music;

		[Space]
		public Material ScreenSpaceMaterial;

		public string SeedCollectionKey;

		[Header("Base Material")]
		[FormerlySerializedAs("GrassColor")]
		public Color TopColor = Color.white;

		public Color DirtColor = Color.white;

		public Color RockColor = Color.white;

		[Range(0f, 1f)]
		public float BaseMetallic;

		[Range(0f, 1f)]
		public float BaseSmoothness;

		[Header("Second Material")]
		public Color SecondColor = Color.white;

		[Range(0f, 1f)]
		public float SecondMetallic;

		[Range(0f, 1f)]
		public float SecondSmoothness;

		[Header("Third Material")]
		public Color ThirdColor = Color.white;

		[Range(0f, 1f)]
		public float ThirdMetallic;

		[Range(0f, 1f)]
		public float ThirdSmoothness;

		public string[] MapFilesNames => mapFilesNames;

		private void OnValidate()
		{
			mapFilesNames = new string[MapFiles.Length];
			for (int i = 0; i < MapFiles.Length; i++)
			{
				if (MapFiles[i] != null)
				{
					mapFilesNames[i] = MapFiles[i].name;
				}
			}
		}

		public static LevelPresetData[] GetAllPresets()
		{
			return Resources.LoadAll<LevelPresetData>("LevelMenuData");
		}
	}
}
