using System;
using UnityEngine;

namespace cakeslice
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(Renderer))]
	public class Outline : MonoBehaviour
	{
		public int color;

		public bool eraseRenderer;

		public bool ignoreAlpha;

		public bool useFill = true;

		public bool useBasicInfo = true;

		[NonSerialized]
		public Renderer Renderer;

		[NonSerialized]
		public MeshFilter Filter;

		[NonSerialized]
		public bool hasFilter;

		[NonSerialized]
		public Texture Tex;

		[NonSerialized]
		public int materialAmount;

		[NonSerialized]
		public Material[] materialArray;

		[NonSerialized]
		public int originalColor;

		[NonSerialized]
		public bool isSkinned;

		private bool isInitialized;

		private bool isAdded;

		protected void OnEnable()
		{
			if (useBasicInfo)
			{
				BasicInfo basicInfo = GetComponent<BasicInfo>();
				if (basicInfo == null)
				{
					basicInfo = GetComponentInParent<BasicInfo>();
				}
				if (basicInfo == null)
				{
					if (base.transform.parent != null && Application.isPlaying)
					{
						Debug.LogError("Invalid outline on " + Machine.GetObjectPath(base.gameObject) + "!", base.gameObject);
					}
					OnDisable();
					base.enabled = false;
					return;
				}
				if (basicInfo.isSimulating)
				{
					Debug.LogWarning("Outline enabled in sim: " + base.transform.name + ", " + basicInfo.name);
					OnDisable();
					base.enabled = false;
					return;
				}
			}
			if (!isInitialized)
			{
				Init();
			}
			if (Renderer.enabled)
			{
				OutlineEffect instance = OutlineEffect.Instance;
				if (instance != null)
				{
					instance.AddOutline(this);
					isAdded = true;
				}
			}
		}

		protected void OnDisable()
		{
			if (isAdded)
			{
				OutlineEffect instance = OutlineEffect.Instance;
				if (instance != null)
				{
					instance.RemoveOutline(this);
				}
			}
		}

		protected virtual void Init()
		{
			Filter = GetComponent<MeshFilter>();
			hasFilter = Filter != null;
			Renderer = GetComponent<Renderer>();
			isSkinned = Renderer is SkinnedMeshRenderer;
			Tex = ((!Renderer.sharedMaterial.HasProperty("_MainTex")) ? null : Renderer.sharedMaterial.mainTexture);
			materialAmount = Renderer.sharedMaterials.Length;
			materialArray = Renderer.sharedMaterials;
			originalColor = color;
			isInitialized = true;
		}

		public void SetFromBlock(BlockBehaviour block)
		{
			if (!isInitialized)
			{
				Init();
			}
			if (isSkinned)
			{
				BlockSkinLoader.SkinPack.Skin defaultSkin = block.Prefab.DefaultSkin;
				Tex = defaultSkin.material.mainTexture;
				materialAmount = defaultSkin.materials.Length;
				materialArray = defaultSkin.materials;
				return;
			}
			BlockSkinLoader.SkinPack.Skin defaultSkin2 = block.Prefab.DefaultSkin;
			for (int i = 0; i < block.VisualController.renderers.Length; i++)
			{
				if (Renderer == block.VisualController.renderers[i])
				{
					if (block.VisualController.SplitMats(defaultSkin2.materials))
					{
						Tex = defaultSkin2.materials[i].mainTexture;
					}
					else
					{
						Tex = defaultSkin2.material.mainTexture;
					}
					materialAmount = defaultSkin2.materials.Length;
					materialArray = defaultSkin2.materials;
					break;
				}
			}
		}
	}
}
