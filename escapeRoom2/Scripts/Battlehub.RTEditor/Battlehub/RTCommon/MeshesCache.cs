using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battlehub.RTCommon
{
	public class MeshesCache : MonoBehaviour, IMeshesCache
	{
		public class RenderTransformedMeshesBatch : RenderMeshesBatch
		{
			public readonly List<Transform> Transforms = new List<Transform>();

			public RenderTransformedMeshesBatch(Mesh mesh, Material material)
				: base(mesh, material, new Matrix4x4[0])
			{
			}

			public override void Refresh()
			{
				for (int num = Transforms.Count - 1; num >= 0; num--)
				{
					if (Transforms[num] == null)
					{
						Transforms.RemoveAt(num);
					}
				}
				if (Transforms.Count != m_matrices.Length)
				{
					m_matrices = new Matrix4x4[Transforms.Count];
				}
				for (int num2 = Transforms.Count - 1; num2 >= 0; num2--)
				{
					Transform transform = Transforms[num2];
					m_matrices[num2] = transform.localToWorldMatrix;
				}
			}
		}

		private class PRS
		{
			public Vector3 Position;

			public Quaternion Rotation;

			public Vector3 LocalScale;
		}

		private CacheRefreshMode m_refreshMode;

		private readonly List<RenderMeshesBatch> m_batches = new List<RenderMeshesBatch>();

		private List<PRS> m_prs = new List<PRS>();

		private List<Transform> m_transforms = new List<Transform>();

		private Dictionary<Mesh, Tuple<Material, List<Transform>>> m_meshToData = new Dictionary<Mesh, Tuple<Material, List<Transform>>>();

		private readonly Dictionary<Mesh, RenderMeshesBatch> m_meshToBatch = new Dictionary<Mesh, RenderMeshesBatch>();

		public CacheRefreshMode RefreshMode
		{
			get
			{
				return m_refreshMode;
			}
			set
			{
				m_refreshMode = value;
				base.enabled = m_refreshMode != CacheRefreshMode.Manual;
			}
		}

		public bool IsEmpty => m_batches.Count == 0;

		public IList<RenderMeshesBatch> Batches => m_batches;

		public event Action Refreshing;

		private void Awake()
		{
			base.enabled = m_refreshMode != CacheRefreshMode.Manual;
		}

		private void Update()
		{
			if (m_refreshMode == CacheRefreshMode.Always)
			{
				Refresh(batchesOnly: true);
				return;
			}
			for (int i = 0; i < m_transforms.Count; i++)
			{
				Transform transform = m_transforms[i];
				if (transform != null)
				{
					PRS pRS = m_prs[i];
					if (pRS.Position != transform.position || pRS.Rotation != transform.rotation || pRS.LocalScale != transform.localScale)
					{
						pRS.Position = transform.position;
						pRS.Rotation = transform.rotation;
						pRS.LocalScale = transform.localScale;
						Refresh(batchesOnly: true);
						break;
					}
				}
			}
		}

		public void AddBatch(Mesh mesh, Material material, Matrix4x4[] matrices)
		{
			m_meshToBatch.Add(mesh, new RenderMeshesBatch(mesh, material, matrices));
		}

		public void RemoveBatch(Mesh mesh)
		{
			m_meshToBatch.Remove(mesh);
		}

		public void Add(Mesh mesh, Transform transform)
		{
			if (!m_meshToData.TryGetValue(mesh, out var value))
			{
				value = new Tuple<Material, List<Transform>>(null, new List<Transform>());
				m_meshToData.Add(mesh, value);
			}
			m_transforms.Add(transform);
			m_prs.Add(new PRS
			{
				Position = Vector3.one * float.NaN
			});
			value.Item2.Add(transform);
		}

		public void SetMaterial(Mesh mesh, Material material)
		{
			if (m_meshToData.ContainsKey(mesh))
			{
				m_meshToData[mesh] = new Tuple<Material, List<Transform>>(material, m_meshToData[mesh].Item2);
			}
			else
			{
				m_meshToData.Add(mesh, new Tuple<Material, List<Transform>>(material, new List<Transform>()));
			}
		}

		public void Remove(Mesh mesh, Transform transform)
		{
			if (m_meshToData.TryGetValue(mesh, out var value))
			{
				value.Item2.Remove(transform);
				int num = m_transforms.IndexOf(transform);
				if (num >= 0)
				{
					m_transforms.RemoveAt(num);
					m_prs.RemoveAt(num);
				}
				if (value.Item2.Count == 0)
				{
					m_meshToData.Remove(mesh);
				}
			}
		}

		public void Clear()
		{
			m_meshToData.Clear();
			m_batches.Clear();
		}

		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}

		public void Refresh(bool batchesOnly = false, int maxBatchSize = 128)
		{
			if (batchesOnly)
			{
				RefreshBatches();
				return;
			}
			m_batches.Clear();
			foreach (KeyValuePair<Mesh, Tuple<Material, List<Transform>>> meshToDatum in m_meshToData)
			{
				if (meshToDatum.Key == null)
				{
					continue;
				}
				Tuple<Material, List<Transform>> value = meshToDatum.Value;
				RenderTransformedMeshesBatch renderTransformedMeshesBatch = new RenderTransformedMeshesBatch(meshToDatum.Key, value.Item1);
				m_batches.Add(renderTransformedMeshesBatch);
				int num = 0;
				for (int i = 0; i < value.Item2.Count; i++)
				{
					if (num == maxBatchSize)
					{
						renderTransformedMeshesBatch = new RenderTransformedMeshesBatch(meshToDatum.Key, value.Item1);
						m_batches.Add(renderTransformedMeshesBatch);
						num = 0;
					}
					renderTransformedMeshesBatch.Transforms.Add(value.Item2[i]);
					num++;
				}
			}
			foreach (KeyValuePair<Mesh, RenderMeshesBatch> item in m_meshToBatch)
			{
				if (!(item.Key == null))
				{
					m_batches.Add(item.Value);
				}
			}
			RefreshBatches();
		}

		private void RefreshBatches()
		{
			for (int i = 0; i < m_batches.Count; i++)
			{
				m_batches[i].Refresh();
			}
			if (this.Refreshing != null)
			{
				this.Refreshing();
			}
		}
	}
}
