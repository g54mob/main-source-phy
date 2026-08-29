using System;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Renderer))]
public class PlatformSpecificMaterialPropertySetter : MonoBehaviour
{
	[Serializable]
	private class PropertyChange<T>
	{
		[Tooltip("On which platform should this change be applied?")]
		public TargetPlatform targetPlatform;

		[Tooltip("Which specific material should this changed be applied to?")]
		public int materialIndex;

		[Tooltip("Which specific property should be modified?")]
		public string propertyId;

		[Tooltip("What value should the property be set to?")]
		public T propertyValue;

		[Tooltip("By default, material property blocks are used. If you want to directly override material instead, set this to true.")]
		public bool directlyOverrideMaterial;
	}

	private enum TargetPlatform
	{
		[Tooltip("Change will apply on all of the platforms listed below.")]
		AllListedBelow = 0,
		[Tooltip("Change will apply only on the Nintendo Switch builds.")]
		Switch = 32,
		[Tooltip("Change will apply only on the Android builds.")]
		Android = 11
	}

	[SerializeField]
	private List<PropertyChange<float>> floatPropertyChanges = new List<PropertyChange<float>>();

	[SerializeField]
	private List<PropertyChange<Color>> colorPropertyChanges = new List<PropertyChange<Color>>();

	public void init()
	{
		applyPropertyChanges(floatPropertyChanges, delegate(MaterialPropertyBlock materialPropertyBlock, PropertyChange<float> change)
		{
			materialPropertyBlock.SetFloat(change.propertyId, change.propertyValue);
		}, delegate(Material material, PropertyChange<float> change)
		{
			material.SetFloat(change.propertyId, change.propertyValue);
		});
		applyPropertyChanges(colorPropertyChanges, delegate(MaterialPropertyBlock materialPropertyBlock, PropertyChange<Color> change)
		{
			materialPropertyBlock.SetColor(change.propertyId, change.propertyValue);
		}, delegate(Material material, PropertyChange<Color> change)
		{
			material.SetColor(change.propertyId, change.propertyValue);
		});
	}

	private void applyPropertyChanges<T>(List<PropertyChange<T>> propertyChanges, Action<MaterialPropertyBlock, PropertyChange<T>> propertyBlockSetter, Action<Material, PropertyChange<T>> materialSetter)
	{
		if (propertyChanges == null || propertyChanges.Count == 0)
		{
			return;
		}
		Renderer component = GetComponent<Renderer>();
		if (component == null)
		{
			return;
		}
		RuntimePlatform runtimePlatform = getRuntimePlatform();
		bool flag = Array.Exists((TargetPlatform[])Enum.GetValues(typeof(TargetPlatform)), (TargetPlatform platform) => platform == (TargetPlatform)runtimePlatform);
		foreach (PropertyChange<T> propertyChange in propertyChanges)
		{
			bool num = propertyChange.targetPlatform == TargetPlatform.AllListedBelow && flag;
			bool flag2 = propertyChange.targetPlatform == (TargetPlatform)runtimePlatform;
			if (!(num || flag2))
			{
				continue;
			}
			if (propertyChange.directlyOverrideMaterial)
			{
				materialSetter(component.materials[propertyChange.materialIndex], propertyChange);
			}
			else
			{
				MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
				if (component.HasPropertyBlock())
				{
					component.GetPropertyBlock(materialPropertyBlock, propertyChange.materialIndex);
				}
				propertyBlockSetter(materialPropertyBlock, propertyChange);
				component.SetPropertyBlock(materialPropertyBlock, propertyChange.materialIndex);
			}
			string arg = $"{base.transform.GetScenePath()}: {component.GetType().Name}.materials[{propertyChange.materialIndex}][{propertyChange.propertyId}] set to '{propertyChange.propertyValue}'.";
			Debug.Log(string.Format("[{0}] [{1}] {2}", "PlatformSpecificMaterialPropertySetter", runtimePlatform, arg), base.transform);
		}
	}

	private RuntimePlatform getRuntimePlatform()
	{
		return Application.platform;
	}
}
