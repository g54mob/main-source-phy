using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;
using UnityEngine.Rendering;

namespace HighlightingSystem
{
	[DisallowMultipleComponent]
	[AddComponentMenu("")]
	[ExcludeFromDocs]
	public class HighlighterRenderer : MonoBehaviour
	{
		[ExcludeFromDocs]
		private struct Data
		{
			public Material material;

			public int submeshIndex;

			public bool transparent;
		}

		private static float transparentCutoff = 0.5f;

		private const HideFlags flags = HideFlags.HideInInspector | HideFlags.DontSaveInEditor | HideFlags.NotEditable | HideFlags.DontSaveInBuild;

		private const int cullOff = 0;

		private static readonly string sRenderType = "RenderType";

		private static readonly string sOpaque = "Opaque";

		private static readonly string sTransparent = "Transparent";

		private static readonly string sTransparentCutout = "TransparentCutout";

		private static readonly string sMainTex = "_MainTex";

		public bool isAlive;

		private Renderer r;

		private List<Data> data = new List<Data>();

		private void Awake()
		{
			base.hideFlags = HideFlags.HideInInspector | HideFlags.DontSaveInEditor | HideFlags.NotEditable | HideFlags.DontSaveInBuild;
		}

		private void OnEnable()
		{
			EndOfFrame.AddListener(OnEndOfFrame);
		}

		private void OnDisable()
		{
			EndOfFrame.RemoveListener(OnEndOfFrame);
		}

		private void OnWillRenderObject()
		{
			HighlightingBase.SetVisible(this);
		}

		private void OnDestroy()
		{
			if (this.data == null)
			{
				return;
			}
			int i = 0;
			for (int count = this.data.Count; i < count; i++)
			{
				Data data = this.data[i];
				if (data.transparent)
				{
					Object.Destroy(data.material);
				}
			}
		}

		public void Initialize(Material sharedOpaqueMaterial, Shader transparentShader, List<int> submeshIndices)
		{
			data.Clear();
			r = GetComponent<Renderer>();
			Material[] sharedMaterials = r.sharedMaterials;
			int num = sharedMaterials.Length;
			if (sharedMaterials == null || num == 0)
			{
				return;
			}
			if (submeshIndices.Count == 1 && submeshIndices[0] == -1)
			{
				submeshIndices.Clear();
				for (int i = 0; i < num; i++)
				{
					submeshIndices.Add(i);
				}
			}
			int j = 0;
			for (int count = submeshIndices.Count; j < count; j++)
			{
				int num2 = submeshIndices[j];
				if (num2 >= num)
				{
					continue;
				}
				Material material = sharedMaterials[num2];
				if (material == null)
				{
					continue;
				}
				Data item = default(Data);
				string text = material.GetTag(sRenderType, searchFallbacks: true, sOpaque);
				if (text == sTransparent || text == sTransparentCutout)
				{
					Material material2 = new Material(transparentShader);
					if (r is SpriteRenderer)
					{
						material2.SetInt(ShaderPropertyID._HighlightingCull, 0);
					}
					if (material.HasProperty(ShaderPropertyID._MainTex))
					{
						material2.SetTexture(ShaderPropertyID._MainTex, material.mainTexture);
						material2.SetTextureOffset(sMainTex, material.mainTextureOffset);
						material2.SetTextureScale(sMainTex, material.mainTextureScale);
					}
					int cutoff = ShaderPropertyID._Cutoff;
					material2.SetFloat(cutoff, material.HasProperty(cutoff) ? material.GetFloat(cutoff) : transparentCutoff);
					item.material = material2;
					item.transparent = true;
				}
				else
				{
					item.material = sharedOpaqueMaterial;
					item.transparent = false;
				}
				item.submeshIndex = num2;
				data.Add(item);
			}
		}

		public void SetOverlay(bool overlay)
		{
			int i = 0;
			for (int count = this.data.Count; i < count; i++)
			{
				Data data = this.data[i];
				if (data.transparent)
				{
					data.material.SetKeyword("HIGHLIGHTING_OVERLAY", overlay);
				}
			}
		}

		public void SetColor(Color clr)
		{
			int i = 0;
			for (int count = this.data.Count; i < count; i++)
			{
				Data data = this.data[i];
				if (data.transparent)
				{
					data.material.SetColor(ShaderPropertyID._HighlightingColor, clr);
				}
			}
		}

		public void FillBuffer(CommandBuffer buffer)
		{
			int i = 0;
			for (int count = this.data.Count; i < count; i++)
			{
				Data data = this.data[i];
				buffer.DrawRenderer(r, data.material, data.submeshIndex);
			}
		}

		public bool IsValid()
		{
			return r != null;
		}

		private void OnEndOfFrame()
		{
			if (!isAlive)
			{
				Object.Destroy(this);
			}
		}
	}
}
