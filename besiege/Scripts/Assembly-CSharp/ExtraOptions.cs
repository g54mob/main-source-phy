using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExtraOptions : MonoBehaviour
{
	public static Dictionary<string, string> SavedOptions = new Dictionary<string, string>();

	public static List<ToggleExtraOption> toggleExtraOptionList;

	public static List<FloatExtraOption> floatExtraOptionList;

	public static List<ExtraOption> extraOptionList;

	public CreateLayout createLayout;

	public bool Started;

	public bool HasSetUpLayout;

	public GameObject mainCamera;

	public bool isInMainMenu;

	public bool setBoolWhenApplied = true;

	public DepthOfFieldScatter depthOfFieldScatter;

	public Vignetting vignetting;

	public SSAOPro ssaoPro;

	public AntialiasingAsPostEffect antialiasingAsPostEffect;

	public BloomAndLensFlares bloomScript;

	private void FindCorrectCameraIfMainCameraVariableIsNull()
	{
		if (!(mainCamera != null))
		{
			if (isInMainMenu)
			{
				mainCamera = GameObject.Find("ExtraOptionsMenu").transform.GetComponentsInChildren<Camera>(true)[0].gameObject;
			}
			else
			{
				mainCamera = Camera.main.gameObject;
			}
		}
	}

	private void SetShadowsFunction(object myValue)
	{
		bool flag = (bool)myValue;
		GameObject gameObject = ((!isInMainMenu) ? GameObject.Find("Directional light") : GameObject.Find("MagicLight"));
		if (gameObject != null)
		{
			gameObject.GetComponent<Light>().shadows = (flag ? LightShadows.Hard : LightShadows.None);
		}
	}

	private void FOVFunction(object myValue)
	{
		float fieldOfView = (float)myValue;
		FindCorrectCameraIfMainCameraVariableIsNull();
		if (!isInMainMenu)
		{
			Transform transform = mainCamera.transform.FindChild("3D Hud Cam");
			if (transform != null)
			{
				transform.GetComponent<Camera>().fieldOfView = fieldOfView;
			}
		}
	}

	private void anisotropicFunction(object myValue)
	{
		AnisotropicFiltering anisotropicFiltering = (AnisotropicFiltering)(int)myValue;
		QualitySettings.anisotropicFiltering = anisotropicFiltering;
	}

	private void shadowCascadesFunction(object myValue)
	{
		int shadowCascades = (int)myValue;
		QualitySettings.shadowCascades = shadowCascades;
	}

	private void shadowDistanceFunction(object myValue)
	{
		float num = (float)myValue;
		if (Started && setBoolWhenApplied)
		{
			toggleExtraOptionList[0].SetValue(num != 0f);
		}
		QualitySettings.shadowDistance = num;
	}

	private void DOFFunction(object myValue)
	{
		float num = (float)myValue;
		if (Started && setBoolWhenApplied)
		{
			toggleExtraOptionList[4].SetValue(num != 0f);
		}
		if (depthOfFieldScatter == null)
		{
			FindCorrectCameraIfMainCameraVariableIsNull();
			depthOfFieldScatter = mainCamera.GetComponent<DepthOfFieldScatter>();
		}
		depthOfFieldScatter.aperture = num;
	}

	private void vignetteFunction(object myValue)
	{
		float num = (float)myValue;
		if (Started && setBoolWhenApplied)
		{
			toggleExtraOptionList[3].SetValue(num != 0f);
		}
		if (vignetting == null)
		{
			FindCorrectCameraIfMainCameraVariableIsNull();
			vignetting = mainCamera.GetComponent<Vignetting>();
		}
		vignetting.intensity = num;
	}

	private void SetSSAOFunction(object myValue)
	{
		bool flag = (bool)myValue;
		if (ssaoPro == null)
		{
			FindCorrectCameraIfMainCameraVariableIsNull();
			ssaoPro = mainCamera.GetComponent<SSAOPro>();
		}
		ssaoPro.enabled = flag;
	}

	private void SSAOIntensity(object myValue)
	{
		float num = (float)myValue;
		if (Started && setBoolWhenApplied)
		{
			toggleExtraOptionList[1].SetValue(num != 0f);
		}
		if (ssaoPro == null)
		{
			FindCorrectCameraIfMainCameraVariableIsNull();
			ssaoPro = mainCamera.GetComponent<SSAOPro>();
		}
		ssaoPro.Intensity = num;
	}

	private void SetFXAAFunction(object myValue)
	{
		bool flag = (bool)myValue;
		if (antialiasingAsPostEffect == null)
		{
			FindCorrectCameraIfMainCameraVariableIsNull();
			antialiasingAsPostEffect = mainCamera.GetComponent<AntialiasingAsPostEffect>();
		}
		antialiasingAsPostEffect.enabled = flag;
	}

	private void FXAATypeFunction(object myValue)
	{
		AAMode mode = (AAMode)(int)myValue;
		if (antialiasingAsPostEffect == null)
		{
			FindCorrectCameraIfMainCameraVariableIsNull();
			antialiasingAsPostEffect = mainCamera.GetComponent<AntialiasingAsPostEffect>();
		}
		antialiasingAsPostEffect.SetMode(mode);
	}

	private void SetDLAASharpFunction(object myValue)
	{
		bool dlaaSharp = (bool)myValue;
		antialiasingAsPostEffect.dlaaSharp = dlaaSharp;
	}

	private void SetNFAAShowNormalFunction(object myValue)
	{
		bool showGeneratedNormals = (bool)myValue;
		antialiasingAsPostEffect.showGeneratedNormals = showGeneratedNormals;
	}

	private void SetDOFFunction(object myValue)
	{
		bool flag = (bool)myValue;
		if (depthOfFieldScatter == null)
		{
			FindCorrectCameraIfMainCameraVariableIsNull();
			depthOfFieldScatter = mainCamera.GetComponent<DepthOfFieldScatter>();
		}
		depthOfFieldScatter.enabled = flag;
	}

	private void SetVignetteFunction(object myValue)
	{
		if (!isInMainMenu)
		{
			bool flag = (bool)myValue;
			if (vignetting == null)
			{
				FindCorrectCameraIfMainCameraVariableIsNull();
				vignetting = mainCamera.GetComponent<Vignetting>();
			}
			vignetting.enabled = flag;
		}
	}

	private void SetBloomFunction(object myValue)
	{
		bool flag = (bool)myValue;
		if (bloomScript == null)
		{
			FindCorrectCameraIfMainCameraVariableIsNull();
			bloomScript = mainCamera.GetComponent<BloomAndLensFlares>();
		}
		bloomScript.enabled = flag;
	}

	private void BloomIntensityFunction(object myValue)
	{
		float num = (float)myValue;
		if (Started && setBoolWhenApplied)
		{
			toggleExtraOptionList[5].SetValue(num != 0f);
		}
		if (bloomScript == null)
		{
			FindCorrectCameraIfMainCameraVariableIsNull();
			bloomScript = mainCamera.GetComponent<BloomAndLensFlares>();
		}
	}

	private IEnumerator Start()
	{
		setBoolWhenApplied = false;
		Load();
		yield return new WaitForSeconds(0.3f);
		SetUp();
		Started = true;
		setBoolWhenApplied = true;
		if (!SceneManager.GetActiveScene().name.Contains("INITIALISER"))
		{
			SetUpLayout();
		}
		SceneManager.sceneLoaded += OnSceneLoad;
	}

	private void SetUp()
	{
		if (createLayout == null)
		{
			createLayout = Object.FindObjectOfType<CreateLayout>();
		}
		isInMainMenu = createLayout != null;
		toggleExtraOptionList = new List<ToggleExtraOption>();
		floatExtraOptionList = new List<FloatExtraOption>();
		extraOptionList = new List<ExtraOption>();
		toggleExtraOptionList.Add(new ToggleExtraOption("SHADOWS TOGGLE", true, "SHADOWS", SetShadowsFunction));
		extraOptionList.Add(new ExtraOption("DYNAMIC RESOLUTION", new object[3] { 0, 2, 4 }, new string[3] { "QUALITY (Low)", "Quality (Medium)", "Quality (High)" }, shadowCascadesFunction));
		floatExtraOptionList.Add(new FloatExtraOption("RENDER DISTANCE", 350f, "DISTANCE", shadowDistanceFunction));
		toggleExtraOptionList.Add(new ToggleExtraOption("SSAO TOGGLE", true, "SSAO", SetSSAOFunction));
		floatExtraOptionList.Add(new FloatExtraOption("SSAO INTENSITY", 15.1f, "INTENSITY", SSAOIntensity));
		toggleExtraOptionList.Add(new ToggleExtraOption("FXAA TOGGLE", true, "FXAA", SetFXAAFunction));
		new ToggleExtraOption("DLAA sharp", true, "SHARP", SetDLAASharpFunction);
		new ToggleExtraOption("NFAA Show Normals", false, "SHOW NORMALS", SetNFAAShowNormalFunction);
		extraOptionList.Add(new ExtraOption("FXAA MODE", new object[4]
		{
			AAMode.FXAA1PresetB,
			AAMode.NFAA,
			AAMode.SSAA,
			AAMode.DLAA
		}, new string[4] { "TYPE (FXAA)", "TYPE (NFAA)", "TYPE (SSAA)", "TYPE (DLAA)" }, FXAATypeFunction));
		new ExtraOption("AnisotropicFiltering", new object[3]
		{
			AnisotropicFiltering.Enable,
			AnisotropicFiltering.Disable,
			AnisotropicFiltering.ForceEnable
		}, new string[3] { "ALT (Per Texture)", "ALT (Disabled)", "ALT (Force On)" }, anisotropicFunction);
		toggleExtraOptionList.Add(new ToggleExtraOption("TOGGLE VIGNETTE", true, "VIGNETTE", SetVignetteFunction));
		floatExtraOptionList.Add(new FloatExtraOption("VIGNETTE INTENISTY", 1.12f, "INTENSITY", vignetteFunction));
		toggleExtraOptionList.Add(new ToggleExtraOption("TOGGLE DOF", true, "DOF", SetDOFFunction));
		floatExtraOptionList.Add(new FloatExtraOption("DEPTH OF FIELD APERTURE", 16f, "INTENSITY", DOFFunction));
		toggleExtraOptionList.Add(new ToggleExtraOption("TOGGLE BLOOM", true, "BLOOM", SetBloomFunction));
		floatExtraOptionList.Add(new FloatExtraOption("BLOOM INTENSITY", 0.6f, "INTENSITY", BloomIntensityFunction));
	}

	private void SetUpLayout()
	{
		if (createLayout == null)
		{
			createLayout = Object.FindObjectOfType<CreateLayout>();
		}
		isInMainMenu = createLayout != null;
		if (isInMainMenu)
		{
			if (!HasSetUpLayout)
			{
				HasSetUpLayout = true;
				createLayout.layout.Add(new SimpleMenuToggleContainer(1, toggleExtraOptionList[0], new ExtraOption[1] { extraOptionList[0] }, new object[1] { new object[3]
				{
					floatExtraOptionList[0],
					0,
					700
				} }));
				createLayout.layout.Add(new SimpleMenuToggleContainer(1, toggleExtraOptionList[1], new ExtraOption[0], new object[1] { new object[3]
				{
					floatExtraOptionList[1],
					0,
					46
				} }));
				createLayout.layout.Add(new SimpleMenuToggleContainer(1, toggleExtraOptionList[2], new ExtraOption[1] { extraOptionList[1] }, new object[0]));
				createLayout.layout.Add(new SimpleMenuToggleContainer(1, toggleExtraOptionList[3], new ExtraOption[0], new object[1] { new object[3]
				{
					floatExtraOptionList[2],
					0,
					5.5f
				} }));
				createLayout.layout.Add(new SimpleMenuToggleContainer(1, toggleExtraOptionList[4], new ExtraOption[0], new object[1] { new object[3]
				{
					floatExtraOptionList[3],
					0,
					32
				} }));
				createLayout.layout.Add(new SimpleMenuToggleContainer(1, toggleExtraOptionList[5], new ExtraOption[0], new object[1] { new object[3]
				{
					floatExtraOptionList[4],
					0,
					100
				} }));
			}
			createLayout.SetUpMenu();
		}
	}

	private void OnSceneLoad(Scene scene, LoadSceneMode m)
	{
		if (createLayout == null)
		{
			createLayout = Object.FindObjectOfType<CreateLayout>();
		}
		isInMainMenu = createLayout != null;
		if (scene.name.Contains("INITIALISER"))
		{
			return;
		}
		setBoolWhenApplied = false;
		mainCamera = null;
		SetUpLayout();
		if (Started)
		{
			Save();
			if (scene.name.Contains("LevelSelect"))
			{
				return;
			}
			for (int i = 0; i < toggleExtraOptionList.Count; i++)
			{
				toggleExtraOptionList[i].Apply();
			}
			for (int i = 0; i < extraOptionList.Count; i++)
			{
				extraOptionList[i].Apply();
			}
			for (int i = 0; i < floatExtraOptionList.Count; i++)
			{
				floatExtraOptionList[i].Apply();
			}
		}
		setBoolWhenApplied = true;
	}

	private void OnApplicationQuit()
	{
		if (Started)
		{
			Save();
		}
	}

	private void Save()
	{
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, string> savedOption in SavedOptions)
		{
			list.Add(savedOption.Key + ">>>" + savedOption.Value);
		}
		File.WriteAllLines(StaticSettings.DataPath + "/ExtraOptions.txt", list.ToArray());
	}

	private void Load()
	{
		if (!File.Exists(StaticSettings.DataPath + "/ExtraOptions.txt"))
		{
			File.Create(StaticSettings.DataPath + "/ExtraOptions.txt");
			return;
		}
		string[] array = File.ReadAllLines(StaticSettings.DataPath + "/ExtraOptions.txt");
		for (int i = 0; i < array.Length; i++)
		{
			int num = array[i].IndexOf(">>>");
			if (num != -1)
			{
				string key = array[i].Substring(0, num);
				string value = array[i].Substring(num + 3);
				if (SavedOptions.ContainsKey(key))
				{
					SavedOptions[key] = value;
				}
				else
				{
					SavedOptions.Add(key, value);
				}
			}
		}
	}
}
