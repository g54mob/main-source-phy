using System.Collections.Generic;
using Kamgam.SettingsGenerator;
using UnityEngine;

public class CatCustomizationController : MonoBehaviour
{
	[SerializeField]
	private GameObject catPrefab;

	[SerializeField]
	private GameObject kittenPrefab;

	[SerializeField]
	private Transform defaultTransform;

	[SerializeField]
	private List<Material> FurMaterials;

	[SerializeField]
	private List<Material> BodyMaterials;

	[SerializeField]
	private List<Material> EyesMaterials;

	[SerializeField]
	private SettingsProvider settingsProvider;

	[SerializeField]
	private PickUpZoomTarget clipBoard;

	[SerializeField]
	private GameObject catCameraZone;

	public bool isKitten;

	public int hatType;

	public string catName;

	public int bodyColor;

	public int eyesColor;

	public bool catEnabled;

	private CatCustomization currentCatCustomization;

	private CatController currentCatController;

	private Dictionary<string, BlendShapeKey> blendShapeKeys;

	public void ChangeToKitten()
	{
	}

	public void ChangeToAdultCat()
	{
	}

	public void ChangeBodyColor(int index)
	{
	}

	public void ChangeEyesColor(int index)
	{
	}

	public void ChangeHatState(int type)
	{
	}

	public void SetBlendShapeValue(int value, bool eyes, bool body, bool fur, bool whiskers, int blendShapeIndex)
	{
	}

	public int GetBlendShapeValue(bool eyes, bool body, bool fur, bool whiskers, int blendShapeIndex)
	{
		return 0;
	}

	public void StartCustomization()
	{
	}

	public void StopCustomization()
	{
	}

	public void ChangeCatState(bool state)
	{
	}

	private void ChangeModel(GameObject prefab)
	{
	}
}
