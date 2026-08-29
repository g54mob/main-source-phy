using System;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Renderer))]
public class PlatformSpecificMaterialSwitcher : MonoBehaviour
{
	[Serializable]
	public class MaterialData
	{
		public Material material;

		public int index;

		public MaterialPlatform platform;
	}

	public enum MaterialPlatform
	{
		AllListedBelow = 0,
		Switch = 32,
		Android = 11
	}

	public List<MaterialData> materials = new List<MaterialData>();

	public void init()
	{
		Renderer component = GetComponent<Renderer>();
		Material[] array = component.materials;
		RuntimePlatform runtimePlatform = Application.platform;
		bool flag = Array.Exists((MaterialPlatform[])Enum.GetValues(typeof(MaterialPlatform)), (MaterialPlatform platform) => platform == (MaterialPlatform)runtimePlatform);
		foreach (MaterialData material in materials)
		{
			bool num = material.platform == MaterialPlatform.AllListedBelow && flag;
			bool flag2 = material.platform == (MaterialPlatform)runtimePlatform;
			if (num || flag2)
			{
				array[material.index] = material.material;
				string text = $"Material[{material.index}] set to '{material.material}' on '{base.transform.GetScenePath()}' for platform '{runtimePlatform}'.";
				Debug.Log("[PlatformSpecificMaterialSwitcher] " + text);
			}
		}
		component.materials = array;
	}
}
