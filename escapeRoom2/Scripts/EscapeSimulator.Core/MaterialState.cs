using System;
using System.Collections.Generic;
using UnityEngine;

public class MaterialState : MonoBehaviour
{
	[Serializable]
	public class ObjectState
	{
		public GameObject gameObject;

		public string path;

		public MaterialChange[] materials;
	}

	[Serializable]
	public class MaterialChange
	{
		public Material material;

		public Material changedMaterial;

		public int materialIndex;

		public int flags;

		public Texture baseMap;

		public Dictionary<string, object> properties;

		public Texture emissiveMap;
	}

	[Serializable]
	public class MaterialStateRecord
	{
		public string name;

		public List<ObjectState> state = new List<ObjectState>();

		public float weight;

		public float targetWeight;

		public float speed = 1f;
	}

	private const int Flag_Material = 2;

	private const int Flag_Base_Map = 4;

	private const int Flag_Emmisive_Map = 8;

	public const string DefaultRecord = "Default";

	private static readonly int[] lerpableFloats = new int[25]
	{
		Shader.PropertyToID("_Metallic"),
		Shader.PropertyToID("_Smoothness"),
		Shader.PropertyToID("_Cutoff"),
		Shader.PropertyToID("_EmissiveIntensity"),
		Shader.PropertyToID("_NormalScale"),
		Shader.PropertyToID("_UseEmissiveIntensity"),
		Shader.PropertyToID("_Dissolve"),
		Shader.PropertyToID("_Wave_Distance_From_Center"),
		Shader.PropertyToID("_Speed"),
		Shader.PropertyToID("_SailWind"),
		Shader.PropertyToID("_FullRipple"),
		Shader.PropertyToID("_Furl"),
		Shader.PropertyToID("_Alpha"),
		Shader.PropertyToID("_PosY"),
		Shader.PropertyToID("_AlphaRemapMin"),
		Shader.PropertyToID("_AlphaRemapMax"),
		Shader.PropertyToID("Vector1_10EB46E7"),
		Shader.PropertyToID("Vector1_9A6CDA63"),
		Shader.PropertyToID("Vector1_A01E7616"),
		Shader.PropertyToID("Vector1_8163804F"),
		Shader.PropertyToID("Vector1_5E63D775"),
		Shader.PropertyToID("Vector1_276CFA84"),
		Shader.PropertyToID("Vector1_C09E8EE0"),
		Shader.PropertyToID("Vector1_AA127297"),
		Shader.PropertyToID("Vector1_A6116528")
	};

	private static readonly int[] lerpableColors = new int[16]
	{
		Shader.PropertyToID("_BaseColor"),
		Shader.PropertyToID("_Color"),
		Shader.PropertyToID("_EmmisionColor"),
		Shader.PropertyToID("_EmissionColor"),
		Shader.PropertyToID("_EmissiveColor"),
		Shader.PropertyToID("_EmissiveColorLDR"),
		Shader.PropertyToID("_Emmisive_Color"),
		Shader.PropertyToID("_RimColor"),
		Shader.PropertyToID("_MainColor"),
		Shader.PropertyToID("_FresnelColor"),
		Shader.PropertyToID("_BaseEmissionColor"),
		Shader.PropertyToID("_tint"),
		Shader.PropertyToID("_Vortex_Color"),
		Shader.PropertyToID("_TintColor"),
		Shader.PropertyToID("Color_3311DE9B"),
		Shader.PropertyToID("Color_65F0F92")
	};

	private static readonly int lerpableTextureBaseColorMap = Shader.PropertyToID("_BaseColorMap");

	private static readonly int lerpableTextureEmissiveColorMap = Shader.PropertyToID("_EmissiveColorMap");

	[HideInInspector]
	public MaterialStateData materialStateData;

	public MaterialStateData overrideMaterialStateData;

	private MaterialStateRecord defaultRecord;

	public Interpolation interpolation = Interpolation.SmootherStep;

	public void init()
	{
		try
		{
			initOverrideMatState();
			List<ObjectState> list = new List<ObjectState>();
			captureStateRecursive(base.transform, base.transform, list, null);
			defaultRecord = new MaterialStateRecord
			{
				name = "Default",
				state = list,
				weight = 1f
			};
			foreach (ObjectState item in defaultRecord.state)
			{
				MaterialChange[] materials = item.materials;
				for (int i = 0; i < materials.Length; i++)
				{
					materials[i].flags = 0;
				}
			}
			foreach (MaterialStateRecord materialState in materialStateData.materialStates)
			{
				foreach (ObjectState item2 in materialState.state)
				{
					foreach (ObjectState item3 in defaultRecord.state)
					{
						if (!(item3.gameObject == item2.gameObject))
						{
							continue;
						}
						for (int j = 0; j < item3.materials.Length; j++)
						{
							if (item2.materials.Length > j)
							{
								item3.materials[j].flags = item3.materials[j].flags | item2.materials[j].flags;
							}
						}
					}
				}
			}
		}
		catch
		{
			Debug.LogError("Failed in material state: " + base.gameObject.name);
		}
	}

	public void relinkStates()
	{
		if (!(materialStateData != null))
		{
			return;
		}
		bool flag = false;
		foreach (MaterialStateRecord materialState in materialStateData.materialStates)
		{
			foreach (ObjectState item in materialState.state)
			{
				if (item.gameObject == null || !item.gameObject.transform.IsChildOf(base.transform))
				{
					Debug.Log(item.path + " GO link is missing, trying to recreate link " + base.gameObject);
					flag = true;
					break;
				}
			}
		}
		if (!flag)
		{
			return;
		}
		materialStateData = UnityEngine.Object.Instantiate(materialStateData);
		foreach (MaterialStateRecord materialState2 in materialStateData.materialStates)
		{
			foreach (ObjectState item2 in materialState2.state)
			{
				if (item2.gameObject == null || !item2.gameObject.transform.IsChildOf(base.transform))
				{
					Transform transform = base.transform.Find(item2.path);
					item2.gameObject = ((transform == null) ? null : base.transform.gameObject);
				}
			}
		}
	}

	public void initOverrideMatState()
	{
		if (!(overrideMaterialStateData != null))
		{
			return;
		}
		materialStateData = UnityEngine.Object.Instantiate(overrideMaterialStateData);
		foreach (MaterialStateRecord materialState in materialStateData.materialStates)
		{
			materialState.targetWeight = 0f;
			materialState.weight = 0f;
			foreach (ObjectState item in materialState.state)
			{
				Transform transform = base.transform.Find(item.path);
				item.gameObject = ((transform == null) ? null : transform.gameObject);
			}
		}
	}

	public MaterialStateRecord findStateByName(string name)
	{
		return materialStateData.materialStates.Find((MaterialStateRecord x) => x.name == name);
	}

	private string getTransformPath(Transform current, Transform root)
	{
		string text = "";
		while (current != root && current != null)
		{
			text = ((text == "") ? current.name : (current.name + "/" + text));
			current = current.parent;
		}
		if (!(current == null))
		{
			return text;
		}
		return null;
	}

	public void setWeight(string record, float weight = 1f)
	{
		setState(findStateByName(record), weight);
	}

	public void setWeight(MaterialStateRecord record, float weight)
	{
		record.weight = weight;
		record.targetWeight = weight;
		syncVisuals();
	}

	public float getWeight(string record)
	{
		return findStateByName(record).weight;
	}

	public float getTargetWeight(string record)
	{
		return findStateByName(record).targetWeight;
	}

	public void setState(string record, float weight = 1f)
	{
		setState(findStateByName(record), weight);
	}

	public void setState(MaterialStateRecord record, float weight = 1f)
	{
		foreach (MaterialStateRecord materialState in materialStateData.materialStates)
		{
			materialState.weight = ((record == materialState) ? weight : 0f);
			materialState.targetWeight = materialState.weight;
		}
		syncVisuals();
	}

	public void transitionToDuration(string record, float duration = 1f, float weight = 1f)
	{
		transitionToDuration(findStateByName(record), duration, weight);
	}

	public void transitionToDuration(MaterialStateRecord record, float duration = 1f, float weight = 1f)
	{
		transitionTo(record, 1f / duration, weight);
	}

	public void transitionTo(string record, float speed = 1f, float weight = 1f)
	{
		transitionTo(findStateByName(record), speed, weight);
	}

	public void transitionTo(MaterialStateRecord record, float speed = 1f, float weight = 1f)
	{
		foreach (MaterialStateRecord materialState in materialStateData.materialStates)
		{
			updateRecord(materialState);
		}
		syncVisuals();
		void updateRecord(MaterialStateRecord current)
		{
			current.targetWeight = ((record == current) ? weight : 0f);
			current.speed = speed;
		}
	}

	public void transitionToStateAdditive(string record, float speed = 1f, float weight = 1f)
	{
		transitionToStateAdditive(findStateByName(record), speed, weight);
	}

	public void transitionToStateAdditive(MaterialStateRecord record, float speed = 1f, float weight = 1f)
	{
		record.targetWeight = weight;
		record.speed = speed;
		syncVisuals();
	}

	public void update(float dt, List<string> transitionCompleteEvents)
	{
		bool changed = false;
		foreach (MaterialStateRecord materialState in materialStateData.materialStates)
		{
			updateWeights(materialState);
		}
		if (changed)
		{
			syncVisuals();
		}
		void updateWeights(MaterialStateRecord record)
		{
			float num = Mathf.MoveTowards(record.weight, record.targetWeight, record.speed * dt);
			if (num != record.weight)
			{
				if (num == record.targetWeight)
				{
					transitionCompleteEvents.Add(record.name);
				}
				record.weight = num;
				changed = true;
			}
		}
	}

	public void syncVisuals()
	{
		syncRecord(defaultRecord, 1f);
		foreach (MaterialStateRecord materialState in materialStateData.materialStates)
		{
			syncRecord(materialState, materialState.weight);
		}
	}

	private void syncRecord(MaterialStateRecord record, float weight = 0f)
	{
		weight = interpolation.evaluate(weight);
		foreach (ObjectState item in record.state)
		{
			Renderer component = item.gameObject.GetComponent<Renderer>();
			if (checkRendererErrors(component))
			{
				break;
			}
			Material[] array = (Application.isPlaying ? component.materials : component.sharedMaterials);
			if (array.Length != item.materials.Length)
			{
				Debug.Log("Material count changed");
				continue;
			}
			for (int i = 0; i < item.materials.Length; i++)
			{
				MaterialChange materialChange = item.materials[i];
				materialChange.material = array[i];
				if (Application.isPlaying && materialChange.material == null)
				{
					array[i] = new Material(array[i]);
					materialChange.material = component.materials[i];
				}
				if (materialChange.changedMaterial != null && (materialChange.flags & 2) != 0)
				{
					int[] array2 = lerpableFloats;
					foreach (int nameID in array2)
					{
						if (materialChange.material.HasFloat(nameID) && materialChange.changedMaterial.HasFloat(nameID))
						{
							float value = Mathf.Lerp(materialChange.material.GetFloat(nameID), materialChange.changedMaterial.GetFloat(nameID), weight);
							materialChange.material.SetFloat(nameID, value);
						}
					}
					array2 = lerpableColors;
					foreach (int nameID2 in array2)
					{
						if (materialChange.material.HasColor(nameID2) && materialChange.changedMaterial.HasColor(nameID2))
						{
							Color value2 = Color.Lerp(materialChange.material.GetColor(nameID2), materialChange.changedMaterial.GetColor(nameID2), weight);
							materialChange.material.SetColor(nameID2, value2);
						}
					}
				}
				if ((materialChange.flags & 4) != 0 && weight > 0.5f && materialChange.material.HasTexture(lerpableTextureBaseColorMap))
				{
					materialChange.material.SetTexture(lerpableTextureBaseColorMap, materialChange.baseMap);
				}
				if ((materialChange.flags & 8) != 0 && weight > 0.5f && materialChange.material.HasTexture(lerpableTextureEmissiveColorMap))
				{
					materialChange.material.SetTexture(lerpableTextureEmissiveColorMap, materialChange.emissiveMap);
				}
			}
		}
	}

	private void captureMaterialProperties(Material material, Dictionary<string, object> properties)
	{
		properties.Clear();
		string[] propertyNames = material.GetPropertyNames(MaterialPropertyType.Float);
		foreach (string key in propertyNames)
		{
			properties[key] = material.GetFloat(key);
		}
		propertyNames = material.GetPropertyNames(MaterialPropertyType.Int);
		foreach (string key2 in propertyNames)
		{
			properties[key2] = material.GetInt(key2);
		}
		propertyNames = material.GetPropertyNames(MaterialPropertyType.Matrix);
		foreach (string key3 in propertyNames)
		{
			properties[key3] = material.GetMatrix(key3);
		}
		propertyNames = material.GetPropertyNames(MaterialPropertyType.Vector);
		foreach (string key4 in propertyNames)
		{
			properties[key4] = material.GetVector(key4);
		}
	}

	private bool CheckMaterialPropertiesChanged(Material material, Dictionary<string, object> originalProperties)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		captureMaterialProperties(material, dictionary);
		foreach (KeyValuePair<string, object> item in dictionary)
		{
			if (!originalProperties.ContainsKey(item.Key) || !isPropertyEqual(originalProperties[item.Key], item.Value))
			{
				return true;
			}
		}
		return false;
		static bool isPropertyEqual(object object1, object object2)
		{
			if (object1 is int num && object2 is int num2)
			{
				return num == num2;
			}
			if (object1 is float num3 && object2 is float num4)
			{
				return num3 == num4;
			}
			if (object1 is Matrix4x4 matrix4x && object2 is Matrix4x4 matrix4x2)
			{
				return matrix4x == matrix4x2;
			}
			if (object1 is Vector4 vector && object2 is Vector4 vector2)
			{
				return vector == vector2;
			}
			return false;
		}
	}

	private void captureStateRecursive(Transform current, Transform root, List<ObjectState> states, MaterialStateRecord diffRecord)
	{
		ObjectState objectState = new ObjectState();
		objectState.gameObject = current.gameObject;
		objectState.path = getTransformPath(current, root);
		ObjectState objectState2 = null;
		if (diffRecord != null)
		{
			foreach (ObjectState item in diffRecord.state)
			{
				if (item.gameObject == current.gameObject)
				{
					objectState2 = item;
				}
			}
		}
		Renderer component = objectState.gameObject.GetComponent<Renderer>();
		if (checkRendererErrors(component))
		{
			return;
		}
		objectState.materials = new MaterialChange[component.sharedMaterials.Length];
		for (int i = 0; i < component.sharedMaterials.Length; i++)
		{
			Material material = component.sharedMaterials[i];
			MaterialChange materialChange = new MaterialChange();
			MaterialChange materialChange2 = ((objectState2 != null) ? objectState2.materials[i] : null);
			if (diffRecord == null)
			{
				Dictionary<string, object> properties = new Dictionary<string, object>();
				captureMaterialProperties(material, properties);
				materialChange.properties = properties;
			}
			if (materialChange2 != null)
			{
				materialChange.changedMaterial = new Material(material);
				if (CheckMaterialPropertiesChanged(material, materialChange2.properties))
				{
					materialChange.flags |= 2;
				}
			}
			if (material.HasTexture("_BaseColorMap"))
			{
				materialChange.baseMap = material.GetTexture("_BaseColorMap");
			}
			if (materialChange2?.baseMap?.name != materialChange.baseMap?.name)
			{
				materialChange.flags |= 4;
			}
			else
			{
				materialChange.baseMap = null;
			}
			if (material.HasTexture("_EmissiveColorMap"))
			{
				materialChange.emissiveMap = material.GetTexture("_EmissiveColorMap");
			}
			if (materialChange2?.emissiveMap?.name != materialChange.emissiveMap?.name)
			{
				materialChange.flags |= 8;
			}
			else
			{
				materialChange.emissiveMap = null;
			}
			objectState.materials[i] = materialChange;
			if (diffRecord == null)
			{
				materialChange.flags = 14;
				if (materialChange.flags != 0)
				{
					materialChange.changedMaterial = new Material(material);
					materialChange.changedMaterial.CopyPropertiesFromMaterial(material);
				}
			}
		}
		states.Add(objectState);
	}

	public bool checkRendererErrors(Renderer renderer)
	{
		if (renderer == null)
		{
			Debug.Log("Renderer is missig");
			return true;
		}
		if (renderer.sharedMaterials.Length == 0)
		{
			Debug.Log("No materials on renderer");
			return true;
		}
		return false;
	}
}
