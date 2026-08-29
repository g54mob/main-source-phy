using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

public class ImpostorClone : MonoBehaviour
{
	public class ImpostorCloneContext
	{
		public Dictionary<Transform, CloneFlags> flags = new Dictionary<Transform, CloneFlags>();

		public Dictionary<Transform, float> explicitSync = new Dictionary<Transform, float>();

		public Dictionary<Transform, bool> onChangeSync = new Dictionary<Transform, bool>();
	}

	[Flags]
	public enum StrippingFlags
	{
		None = 0,
		AllowParticles = 1,
		AllowLights = 2,
		AllowAll = -1
	}

	public float scaleModifier = 1f;

	private float scaleModifierLastFrame;

	private int layerOverride;

	private GameObject clonedObject;

	private StrippingFlags strippingFlags;

	private string specialStrippingTag;

	private string specialEnableGOTag;

	private bool strip = true;

	private bool stripMotionVectors;

	[NonSerialized]
	public bool invalidated;

	[NonSerialized]
	public bool changedFlag;

	[NonSerialized]
	public Transform targetRoot;

	[NonSerialized]
	public Action<ImpostorClone> onChange;

	[NonSerialized]
	public List<RendererCommandParams> renderersOpaque = new List<RendererCommandParams>();

	[NonSerialized]
	public List<RendererCommandParams> renderersTransparent = new List<RendererCommandParams>();

	private ImpostorCloneContext context;

	public static GameObject create(ImpostorCloneContext context, GameObject toClone, int layerOverride = -1, bool strip = true, Action<ImpostorClone> onChange = null, string specialStrippingTag = "", StrippingFlags strippingFlags = StrippingFlags.AllowParticles, bool automaticSync = true, string specialEnableGOTag = "", bool stripMotionVectors = true)
	{
		return create(context, toClone, toClone.transform.position, toClone.transform.rotation, toClone.transform.localScale, layerOverride, strip, onChange, specialStrippingTag, strippingFlags, automaticSync, specialEnableGOTag, stripMotionVectors);
	}

	public static GameObject create(ImpostorCloneContext context, GameObject toClone, Vector3 position, Quaternion rotation, Vector3 scale, int layerOverride = -1, bool strip = true, Action<ImpostorClone> onChange = null, string specialStrippingTag = "", StrippingFlags strippingFlags = StrippingFlags.AllowParticles, bool automaticSync = true, string specialEnableGOTag = "", bool stripMotionVectors = true)
	{
		GameObject gameObject = new GameObject();
		gameObject.name = toClone.name + "(Impostor)";
		gameObject.transform.position = position;
		gameObject.transform.rotation = rotation;
		gameObject.transform.localScale = scale;
		GameObject gameObject2 = copyOther(toClone, gameObject.transform, layerOverride, strippingFlags, specialStrippingTag, strip, specialEnableGOTag, stripMotionVectors);
		if (automaticSync)
		{
			ImpostorClone impostorClone = gameObject.AddComponent<ImpostorClone>();
			impostorClone.context = context;
			impostorClone.layerOverride = layerOverride;
			impostorClone.stripMotionVectors = stripMotionVectors;
			impostorClone.targetRoot = toClone.transform;
			impostorClone.onChange = onChange;
			impostorClone.specialStrippingTag = specialStrippingTag;
			impostorClone.specialEnableGOTag = specialEnableGOTag;
			impostorClone.strip = strip;
			impostorClone.strippingFlags = strippingFlags;
			impostorClone.clonedObject = gameObject2;
		}
		return gameObject;
	}

	public static void replaceLightLayers(GameObject clone, uint layers)
	{
		Renderer[] componentsInChildren = clone.GetComponentsInChildren<Renderer>(includeInactive: true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].renderingLayerMask = layers;
		}
	}

	public static void replaceMaterials(GameObject clone, Material material)
	{
		Renderer[] componentsInChildren = clone.GetComponentsInChildren<Renderer>(includeInactive: true);
		foreach (Renderer renderer in componentsInChildren)
		{
			int num = renderer.sharedMaterials.Length;
			Material[] array = new Material[num];
			for (int j = 0; j < num; j++)
			{
				Material material2 = material;
				if (renderer.sharedMaterials[j].HasTexture("_BaseColorMap"))
				{
					material2 = new Material(material);
					material2.SetTexture("_BaseColorMap", renderer.sharedMaterials[j].GetTexture("_BaseColorMap"));
				}
				array[j] = material2;
			}
			renderer.materials = array;
		}
	}

	public static void replaceMaterialsWithUnlit(GameObject clone, Material material, Material transparent)
	{
		Renderer[] componentsInChildren = clone.GetComponentsInChildren<Renderer>(includeInactive: true);
		foreach (Renderer renderer in componentsInChildren)
		{
			int num = renderer.sharedMaterials.Length;
			Material[] array = new Material[num];
			for (int j = 0; j < num; j++)
			{
				Material material2 = (array[j] = renderer.sharedMaterials[j]);
				if (material2 == null || material2.shader.name == "Pine Studio/Item In UI" || material2.shader.name == "Pine Studio/Item In UI Transparent" || !material2.HasProperty("_SurfaceType"))
				{
					continue;
				}
				Material material3 = new Material((material2.GetInt("_SurfaceType") == 1) ? transparent : material);
				Texture texture = (material2.HasTexture("_BaseColorMap") ? material2.GetTexture("_BaseColorMap") : null);
				Texture texture2 = (material2.HasTexture("_UnlitColorMap") ? material2.GetTexture("_UnlitColorMap") : null);
				Texture texture3 = (material2.HasTexture("_Diffuse") ? material2.GetTexture("_Diffuse") : null);
				Texture texture4 = ((texture != null) ? texture : ((texture2 != null) ? texture2 : texture3));
				material3.SetTexture("_BaseColorMap", (texture4 != null) ? texture4 : Texture2D.whiteTexture);
				if (texture4 != null && material2.HasVector("_BaseColorMap_ST"))
				{
					material3.SetVector("_BaseColorMap_ST", material2.GetVector("_BaseColorMap_ST"));
				}
				if (material2.HasTexture("_NormalMap"))
				{
					material3.SetTexture("_NormalMap", material2.GetTexture("_NormalMap"));
				}
				if (material2.HasVector("_BaseColor"))
				{
					material3.SetVector("_BaseColor", material2.GetVector("_BaseColor"));
				}
				if (material2.HasFloat("_Metallic"))
				{
					material3.SetFloat("_Metallic", material2.GetFloat("_Metallic"));
				}
				if (material2.HasFloat("_Smoothness"))
				{
					material3.SetFloat("_Smoothness", material2.GetFloat("_Smoothness"));
				}
				if (material2.HasTexture("_MaskMap"))
				{
					Texture texture5 = material2.GetTexture("_MaskMap");
					material3.SetTexture("_MaskMap", texture5);
					if (texture5 != null)
					{
						material3.SetFloat("_Metallic", 1f);
						material3.SetFloat("_Smoothness", 1f);
					}
				}
				if (material2.HasTexture("_EmissiveColorMap"))
				{
					material3.SetTexture("_EmissiveColorMap", material2.GetTexture("_EmissiveColorMap"));
				}
				if (material2.HasColor("_EmissiveColor"))
				{
					material3.SetColor("_EmissiveColor", material2.GetColor("_EmissiveColor"));
				}
				array[j] = material3;
			}
			renderer.materials = array;
		}
	}

	public static void replaceMaterialsKinds(GameObject clone, Material opaqueMaterial, Material opaqueAlphaTestMaterial, Material transparentMaterial)
	{
		Renderer[] componentsInChildren = clone.GetComponentsInChildren<Renderer>(includeInactive: true);
		foreach (Renderer renderer in componentsInChildren)
		{
			int num = renderer.sharedMaterials.Length;
			Material[] array = new Material[num];
			for (int j = 0; j < num; j++)
			{
				Material material = renderer.sharedMaterials[j];
				bool flag = material != null && material.HasProperty("_SurfaceType") && material.GetInt("_SurfaceType") == 1;
				bool num2 = material != null && !material.HasFloat("_TransparentSortPriority");
				bool flag2 = material != null && material.HasFloat("_AlphaCutoffEnable") && material.GetFloat("_AlphaCutoffEnable") > 0f;
				if (num2)
				{
					array[j] = new Material(transparentMaterial);
				}
				else if (flag2)
				{
					Texture texture = (material.HasTexture("_BaseColorMap") ? material.GetTexture("_BaseColorMap") : null);
					Texture texture2 = (material.HasTexture("_UnlitColorMap") ? material.GetTexture("_UnlitColorMap") : null);
					Texture texture3 = (material.HasTexture("_Diffuse") ? material.GetTexture("_Diffuse") : null);
					float value = (material.HasFloat("_Cutoff") ? material.GetFloat("_Cutoff") : 1f);
					array[j] = new Material(opaqueAlphaTestMaterial);
					array[j].SetTexture("_BaseTexture", (texture != null) ? texture : ((texture2 != null) ? texture2 : texture3));
					array[j].SetFloat("_AlphaClipTreshold", value);
				}
				else if (flag)
				{
					array[j] = new Material(transparentMaterial);
				}
				else
				{
					array[j] = new Material(opaqueMaterial);
				}
			}
			renderer.materials = array;
		}
	}

	private static GameObject copyOther(GameObject toClone, Transform container, int layerOverride, StrippingFlags strippingFlags, string specialStrippingTag, bool strip, string goEnableTag, bool stripMotionVectors)
	{
		GameObject clonedObject = UnityEngine.Object.Instantiate(toClone.gameObject, container.position, container.rotation, container);
		clonedObject.transform.localScale = Vector3.one;
		if (strip)
		{
			stripNonVisualComponents(clonedObject, strippingFlags, specialStrippingTag);
		}
		if (stripMotionVectors)
		{
			Renderer[] componentsInChildren = clonedObject.GetComponentsInChildren<Renderer>(includeInactive: true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].motionVectorGenerationMode = MotionVectorGenerationMode.ForceNoMotion;
			}
		}
		Transform[] transforms = null;
		if (layerOverride != -1)
		{
			ensureObjectsValid();
			Transform[] array = transforms;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].gameObject.layer = layerOverride;
			}
		}
		if (!string.IsNullOrEmpty(goEnableTag))
		{
			ensureObjectsValid();
			Transform[] array = transforms;
			foreach (Transform transform in array)
			{
				if (transform.gameObject.CompareTag(goEnableTag))
				{
					transform.gameObject.SetActive(value: true);
				}
			}
		}
		clonedObject.SetActive(value: true);
		return clonedObject;
		void ensureObjectsValid()
		{
			if (transforms == null)
			{
				transforms = clonedObject.GetComponentsInChildren<Transform>(includeInactive: true);
			}
		}
	}

	public static bool isComponentForStripping(Component component, StrippingFlags strippingFlags = StrippingFlags.AllowParticles, string specialStrippingTag = "")
	{
		if (component == null)
		{
			return false;
		}
		bool flag = component is Transform || component is MeshFilter || component is MeshRenderer || component is SkinnedMeshRenderer || component is PersistantInImpostor || component is Canvas || component is CanvasRenderer || component is CanvasGroup || component is CanvasScaler || component is GraphicRaycaster || component is Text || component is TextMeshProUGUI || component is Image || component is RawImage || component is Mask || component is SpriteRenderer || component is Floor || component is Wall || component is LODGroup || component is EyeController || component is DebugLogState;
		if (strippingFlags.HasFlag(StrippingFlags.AllowParticles))
		{
			flag = flag || component is ParticleSystem || component is ParticleSystemRenderer;
		}
		if (strippingFlags.HasFlag(StrippingFlags.AllowLights))
		{
			flag = flag || component is Light || component is HDAdditionalLightData;
		}
		if (flag)
		{
			if (!string.IsNullOrEmpty(specialStrippingTag))
			{
				return component.gameObject.CompareTag(specialStrippingTag);
			}
			return false;
		}
		return true;
	}

	public static void stripNonVisualComponents(GameObject gameObject, StrippingFlags strippingFlags = StrippingFlags.None, string specialStrippingTag = "")
	{
		Component[] componentsInChildren = gameObject.GetComponentsInChildren<Component>(includeInactive: true);
		for (int num = componentsInChildren.Length - 1; num >= 0; num--)
		{
			Component component = componentsInChildren[num];
			if (isComponentForStripping(component, strippingFlags, specialStrippingTag) && !(component is Transform))
			{
				UnityEngine.Object.DestroyImmediate(component);
			}
		}
	}

	private bool syncRecursive(Transform cloneTransform, Transform syncTransform)
	{
		if (syncTransform.childCount != cloneTransform.childCount)
		{
			return false;
		}
		for (int i = 0; i < syncTransform.childCount; i++)
		{
			Transform child = cloneTransform.GetChild(i);
			Transform child2 = syncTransform.GetChild(i);
			child.localPosition = child2.localPosition;
			child.localRotation = child2.localRotation;
			child.localScale = child2.localScale;
			bool activeSelf = child2.gameObject.activeSelf;
			if (child.gameObject.activeSelf != activeSelf)
			{
				child.gameObject.SetActive(activeSelf);
				changedFlag = true;
			}
			if (child2.TryGetComponent<SkinnedMeshRenderer>(out var component))
			{
				if (!child.TryGetComponent<SkinnedMeshRenderer>(out var component2))
				{
					return false;
				}
				if (component.sharedMesh.blendShapeCount != component2.sharedMesh.blendShapeCount)
				{
					return false;
				}
				for (int j = 0; j < component.sharedMesh.blendShapeCount; j++)
				{
					component2.SetBlendShapeWeight(j, component.GetBlendShapeWeight(j));
				}
			}
			if (child2.TryGetComponent<Renderer>(out var component3) && !child.TryGetComponent<Renderer>(out var _) && !(component3 is LineRenderer) && !(component3 is ParticleSystemRenderer))
			{
				return false;
			}
			if (!syncRecursive(child, child2))
			{
				return false;
			}
		}
		return true;
	}

	private void Update()
	{
		changedFlag = false;
		bool flag = true;
		if (context != null && context.flags.ContainsKey(targetRoot) && context.flags[targetRoot] == CloneFlags.ExplicitSync)
		{
			flag = context.explicitSync.ContainsKey(targetRoot);
			if (flag && context.explicitSync[targetRoot] <= Time.time)
			{
				context.explicitSync.Remove(targetRoot);
			}
		}
		bool value = default(bool);
		if (context != null && context.onChangeSync.TryGetValue(targetRoot, out value) && value)
		{
			flag = true;
			context.onChangeSync[targetRoot] = false;
		}
		if ((flag || invalidated) && (invalidated || !syncRecursive(base.transform.GetChild(0), targetRoot)))
		{
			changedFlag = true;
			UnityEngine.Object.Destroy(base.transform.GetChild(0).gameObject);
			clonedObject = copyOther(targetRoot.gameObject, base.transform, layerOverride, strippingFlags, specialStrippingTag, strip, specialEnableGOTag, stripMotionVectors);
		}
		if (changedFlag && onChange != null)
		{
			onChange(this);
		}
		if (scaleModifier != scaleModifierLastFrame)
		{
			clonedObject.transform.localScale = Vector3.one * scaleModifier;
			scaleModifierLastFrame = scaleModifier;
		}
		invalidated = false;
	}
}
