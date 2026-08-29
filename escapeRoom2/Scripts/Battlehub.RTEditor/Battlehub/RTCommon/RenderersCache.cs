using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battlehub.RTCommon
{
	public class RenderersCache : MonoBehaviour, IRenderersCache
	{
		private readonly List<Tuple<bool?, bool?>> m_settingsBackup = new List<Tuple<bool?, bool?>>();

		private readonly List<Renderer> m_renderers = new List<Renderer>();

		public bool IsEmpty => m_renderers.Count == 0;

		public Material MaterialOverride { get; set; }

		public IList<Renderer> Renderers => m_renderers;

		public event Action Refreshed;

		public void Add(Renderer[] renderers, bool forceRender = true, bool forceMatrixRecalculationPerRender = false)
		{
			for (int i = 0; i < renderers.Length; i++)
			{
				Add(renderers[i], forceRender, forceMatrixRecalculationPerRender);
			}
		}

		public void Remove(Renderer[] renderers)
		{
			for (int i = 0; i < renderers.Length; i++)
			{
				Remove(renderers[i]);
			}
		}

		public void Add(Renderer renderer, bool forceRender = true, bool forceMatrixRecalcuationPerRender = false)
		{
			bool value = renderer.enabled;
			bool value2 = false;
			if (renderer is SkinnedMeshRenderer)
			{
				SkinnedMeshRenderer skinnedMeshRenderer = (SkinnedMeshRenderer)renderer;
				value2 = skinnedMeshRenderer.forceMatrixRecalculationPerRender;
				if (forceMatrixRecalcuationPerRender)
				{
					skinnedMeshRenderer.forceMatrixRecalculationPerRender = true;
				}
			}
			if (forceRender)
			{
				m_renderers.Add(renderer);
				m_settingsBackup.Add(new Tuple<bool?, bool?>(null, null));
			}
			else if (!renderer.forceRenderingOff)
			{
				renderer.enabled = false;
				m_renderers.Add(renderer);
				m_settingsBackup.Add(new Tuple<bool?, bool?>(value, value2));
			}
		}

		public void Remove(Renderer renderer)
		{
			int num = m_renderers.IndexOf(renderer);
			if (num >= 0)
			{
				Tuple<bool?, bool?> tuple = m_settingsBackup[num];
				if (tuple.Item2.HasValue && renderer is SkinnedMeshRenderer)
				{
					((SkinnedMeshRenderer)renderer).forceMatrixRecalculationPerRender = tuple.Item2.Value;
				}
				if (tuple.Item1.HasValue)
				{
					renderer.enabled = tuple.Item1.Value;
				}
				m_renderers.RemoveAt(num);
				m_settingsBackup.RemoveAt(num);
			}
		}

		public void Refresh()
		{
			if (this.Refreshed != null)
			{
				this.Refreshed();
			}
		}

		public void Clear()
		{
			m_renderers.Clear();
			m_settingsBackup.Clear();
		}

		public void Destroy()
		{
			Clear();
			UnityEngine.Object.Destroy(this);
		}
	}
}
