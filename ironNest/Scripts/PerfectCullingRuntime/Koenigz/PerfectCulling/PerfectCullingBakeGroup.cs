using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Koenigz.PerfectCulling
{
	[Serializable]
	public class PerfectCullingBakeGroup
	{
		public readonly struct RuntimeGroupContent
		{
			public readonly Renderer Renderer;

			public readonly ShadowCastingMode ShadowCastingMode;

			public RuntimeGroupContent(Renderer renderer, ShadowCastingMode shadowCastingMode)
			{
				Renderer = null;
				ShadowCastingMode = default(ShadowCastingMode);
			}
		}

		public enum GroupType
		{
			Other = 0,
			LOD = 1,
			User = 2
		}

		public GroupType groupType;

		public Renderer[] renderers;

		public Behaviour[] unityBehaviours;

		public int vertexCount;

		[NonSerialized]
		private int m_runtimeGroupDataSize;

		[NonSerialized]
		private RuntimeGroupContent[] m_runtimeGroupData;

		[NonSerialized]
		private bool m_initialized;

		public ArraySegment<RuntimeGroupContent> GetRuntimeGroupContent()
		{
			return default(ArraySegment<RuntimeGroupContent>);
		}

		public void Init()
		{
		}

		public bool ContainsRuntimeRenderer(Renderer r)
		{
			return false;
		}

		public void PushRuntimeRenderer(Renderer renderer)
		{
		}

		public bool PopRuntimeRenderer(Renderer renderer)
		{
			return false;
		}

		private void PushRuntimeGroupContent(RuntimeGroupContent groupContent)
		{
		}

		private RuntimeGroupContent PopRuntimeGroupContent()
		{
			return default(RuntimeGroupContent);
		}

		public void ClearRuntimeRenderers()
		{
		}

		public void Toggle(bool isVisible)
		{
		}

		public void ForeachRenderer(Action<Renderer> actionForRenderer)
		{
		}

		public bool CollectMeshStats()
		{
			return false;
		}

		public int GetRuntimeRendererCount()
		{
			return 0;
		}

		public void RemoveInvalidRenderers()
		{
		}
	}
}
