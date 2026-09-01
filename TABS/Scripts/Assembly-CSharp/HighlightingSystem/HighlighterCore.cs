using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Internal;
using UnityEngine.Rendering;

namespace HighlightingSystem
{
	[DisallowMultipleComponent]
	public class HighlighterCore : MonoBehaviour
	{
		[ExcludeFromDocs]
		private class RendererData
		{
			public Renderer renderer;

			public List<int> submeshIndices = new List<int>();
		}

		public delegate bool RendererFilter(Renderer renderer, List<int> submeshIndices);

		[ExcludeFromDocs]
		public const string keywordOverlay = "HIGHLIGHTING_OVERLAY";

		private readonly Color occluderColor = new Color(0f, 0f, 0f, 0f);

		private static readonly HighlighterMode[] renderingOrder = new HighlighterMode[3]
		{
			HighlighterMode.Default,
			HighlighterMode.Overlay,
			HighlighterMode.Occluder
		};

		private const int poolChunkSize = 4;

		private static readonly List<Renderer> sRenderers = new List<Renderer>(4);

		private static readonly Stack<RendererData> sRendererDataPool = new Stack<RendererData>();

		private static readonly List<RendererData> sRendererData = new List<RendererData>(4);

		private static readonly List<int> sSubmeshIndices = new List<int>(4);

		private static readonly List<HighlighterCore> sHighlighters = new List<HighlighterCore>();

		private static ReadOnlyCollection<HighlighterCore> sHighlightersReadonly;

		public HighlighterMode mode;

		public bool forceRender;

		public Color color = Color.white;

		private Transform tr;

		private List<HighlighterRenderer> highlightableRenderers = new List<HighlighterRenderer>();

		private bool isDirty = true;

		private bool cachedOverlay;

		private Color cachedColor = Color.clear;

		private static Shader _opaqueShader;

		private static Shader _transparentShader;

		private Material _opaqueMaterial;

		private static RendererFilter _globalRendererFilter = null;

		private RendererFilter _rendererFilter;

		public static ReadOnlyCollection<HighlighterCore> highlighters
		{
			get
			{
				if (sHighlightersReadonly == null)
				{
					sHighlightersReadonly = sHighlighters.AsReadOnly();
				}
				return sHighlightersReadonly;
			}
		}

		[ExcludeFromDocs]
		public static Shader opaqueShader
		{
			get
			{
				if (_opaqueShader == null)
				{
					_opaqueShader = Shader.Find("Hidden/Highlighted/Opaque");
				}
				return _opaqueShader;
			}
		}

		[ExcludeFromDocs]
		public static Shader transparentShader
		{
			get
			{
				if (_transparentShader == null)
				{
					_transparentShader = Shader.Find("Hidden/Highlighted/Transparent");
				}
				return _transparentShader;
			}
		}

		private Material opaqueMaterial
		{
			get
			{
				if (_opaqueMaterial == null)
				{
					_opaqueMaterial = new Material(opaqueShader);
					_opaqueMaterial.SetKeyword("HIGHLIGHTING_OVERLAY", cachedOverlay);
					_opaqueMaterial.SetColor(ShaderPropertyID._HighlightingColor, cachedColor);
				}
				return _opaqueMaterial;
			}
		}

		public static RendererFilter globalRendererFilter
		{
			get
			{
				return _globalRendererFilter;
			}
			set
			{
				if (!(_globalRendererFilter != value))
				{
					return;
				}
				_globalRendererFilter = value;
				for (int num = sHighlighters.Count - 1; num >= 0; num--)
				{
					HighlighterCore highlighterCore = sHighlighters[num];
					if (highlighterCore == null)
					{
						sHighlighters.RemoveAt(num);
					}
					else if (highlighterCore.rendererFilter == null)
					{
						highlighterCore.SetDirty();
					}
				}
			}
		}

		public RendererFilter rendererFilter
		{
			get
			{
				return _rendererFilter;
			}
			set
			{
				if (_rendererFilter != value)
				{
					_rendererFilter = value;
					SetDirty();
				}
			}
		}

		protected virtual RendererFilter rendererFilterToUse
		{
			get
			{
				if (_rendererFilter != null)
				{
					return _rendererFilter;
				}
				if (_globalRendererFilter != null)
				{
					return _globalRendererFilter;
				}
				return DefaultRendererFilter;
			}
		}

		public static bool DefaultRendererFilter(Renderer renderer, List<int> submeshIndices)
		{
			if (renderer.GetComponentInParent<HighlighterBlocker>() != null)
			{
				return false;
			}
			bool flag = false;
			if (renderer is MeshRenderer)
			{
				flag = true;
			}
			else if (renderer is SkinnedMeshRenderer)
			{
				flag = true;
			}
			else if (renderer is SpriteRenderer)
			{
				flag = true;
			}
			else if (renderer is ParticleSystemRenderer)
			{
				flag = true;
			}
			if (flag)
			{
				submeshIndices.Add(-1);
			}
			return flag;
		}

		private void Awake()
		{
			tr = GetComponent<Transform>();
			AwakeSafe();
		}

		private void OnEnable()
		{
			if (!sHighlighters.Contains(this))
			{
				sHighlighters.Add(this);
			}
			OnEnableSafe();
		}

		private void OnDisable()
		{
			sHighlighters.Remove(this);
			OnDisableSafe();
		}

		private void OnDestroy()
		{
			if (_opaqueMaterial != null)
			{
				Object.Destroy(_opaqueMaterial);
			}
			OnDestroySafe();
		}

		protected virtual void AwakeSafe()
		{
		}

		protected virtual void OnEnableSafe()
		{
		}

		protected virtual void OnDisableSafe()
		{
		}

		protected virtual void OnDestroySafe()
		{
		}

		public void SetDirty()
		{
			isDirty = true;
		}

		protected virtual void UpdateHighlighting()
		{
		}

		private void ClearRenderers()
		{
			for (int num = highlightableRenderers.Count - 1; num >= 0; num--)
			{
				highlightableRenderers[num].isAlive = false;
			}
			highlightableRenderers.Clear();
		}

		private void UpdateRenderers()
		{
			if (!isDirty)
			{
				return;
			}
			isDirty = false;
			ClearRenderers();
			GrabRenderers(tr);
			int i = 0;
			for (int count = sRendererData.Count; i < count; i++)
			{
				RendererData rendererData = sRendererData[i];
				GameObject gameObject = rendererData.renderer.gameObject;
				HighlighterRenderer highlighterRenderer = gameObject.GetComponent<HighlighterRenderer>();
				if (highlighterRenderer == null)
				{
					highlighterRenderer = gameObject.AddComponent<HighlighterRenderer>();
				}
				highlighterRenderer.isAlive = true;
				highlighterRenderer.Initialize(opaqueMaterial, transparentShader, rendererData.submeshIndices);
				highlighterRenderer.SetOverlay(cachedOverlay);
				highlighterRenderer.SetColor(cachedColor);
				highlightableRenderers.Add(highlighterRenderer);
			}
			for (int j = 0; j < sRendererData.Count; j++)
			{
				ReleaseRendererDataInstance(sRendererData[j]);
			}
			sRendererData.Clear();
		}

		private void GrabRenderers(Transform t)
		{
			t.gameObject.GetComponents(sRenderers);
			int i = 0;
			for (int count = sRenderers.Count; i < count; i++)
			{
				Renderer renderer = sRenderers[i];
				if (renderer.enabled)
				{
					if (rendererFilterToUse(renderer, sSubmeshIndices))
					{
						RendererData rendererDataInstance = GetRendererDataInstance();
						rendererDataInstance.renderer = renderer;
						List<int> submeshIndices = rendererDataInstance.submeshIndices;
						submeshIndices.Clear();
						submeshIndices.AddRange(sSubmeshIndices);
						sRendererData.Add(rendererDataInstance);
					}
					sSubmeshIndices.Clear();
				}
			}
			sRenderers.Clear();
			int childCount = t.childCount;
			if (childCount == 0)
			{
				return;
			}
			for (int j = 0; j < childCount; j++)
			{
				Transform child = t.GetChild(j);
				if (!(child.GetComponent<HighlighterCore>() != null))
				{
					GrabRenderers(child);
				}
			}
		}

		private void FillBufferInternal(CommandBuffer buffer)
		{
			bool flag = mode == HighlighterMode.Overlay || mode == HighlighterMode.Occluder;
			if (cachedOverlay != flag)
			{
				cachedOverlay = flag;
				opaqueMaterial.SetKeyword("HIGHLIGHTING_OVERLAY", cachedOverlay);
				for (int i = 0; i < highlightableRenderers.Count; i++)
				{
					highlightableRenderers[i].SetOverlay(cachedOverlay);
				}
			}
			Color color = ((mode != HighlighterMode.Occluder) ? this.color : occluderColor);
			if (cachedColor != color)
			{
				cachedColor = color;
				opaqueMaterial.SetColor(ShaderPropertyID._HighlightingColor, cachedColor);
				for (int j = 0; j < highlightableRenderers.Count; j++)
				{
					highlightableRenderers[j].SetColor(cachedColor);
				}
			}
			for (int num = highlightableRenderers.Count - 1; num >= 0; num--)
			{
				HighlighterRenderer highlighterRenderer = highlightableRenderers[num];
				if (highlighterRenderer == null)
				{
					highlightableRenderers.RemoveAt(num);
				}
				else if (!highlighterRenderer.IsValid())
				{
					highlightableRenderers.RemoveAt(num);
					highlighterRenderer.isAlive = false;
				}
				else if (HighlightingBase.GetVisible(highlighterRenderer) || forceRender)
				{
					highlighterRenderer.FillBuffer(buffer);
				}
			}
		}

		private static void ExpandRendererDataPool(int count)
		{
			for (int i = 0; i < count; i++)
			{
				RendererData item = new RendererData();
				sRendererDataPool.Push(item);
			}
		}

		private static RendererData GetRendererDataInstance()
		{
			if (sRendererDataPool.Count == 0)
			{
				ExpandRendererDataPool(4);
			}
			return sRendererDataPool.Pop();
		}

		private static void ReleaseRendererDataInstance(RendererData instance)
		{
			if (instance != null && !sRendererDataPool.Contains(instance))
			{
				instance.renderer = null;
				instance.submeshIndices.Clear();
				sRendererDataPool.Push(instance);
			}
		}

		[ExcludeFromDocs]
		public static void FillBuffer(CommandBuffer buffer)
		{
			for (int num = sHighlighters.Count - 1; num >= 0; num--)
			{
				HighlighterCore highlighterCore = sHighlighters[num];
				if (highlighterCore == null)
				{
					sHighlighters.RemoveAt(num);
				}
				else
				{
					highlighterCore.UpdateHighlighting();
					if (highlighterCore == null)
					{
						sHighlighters.RemoveAt(num);
					}
					else
					{
						highlighterCore.UpdateRenderers();
					}
				}
			}
			for (int i = 0; i < renderingOrder.Length; i++)
			{
				HighlighterMode highlighterMode = renderingOrder[i];
				for (int num2 = sHighlighters.Count - 1; num2 >= 0; num2--)
				{
					HighlighterCore highlighterCore2 = sHighlighters[num2];
					if (highlighterCore2.mode == highlighterMode)
					{
						highlighterCore2.FillBufferInternal(buffer);
					}
				}
			}
		}
	}
}
