using System;
using System.Collections.Generic;
using UnityEngine;

namespace Koenigz.PerfectCulling
{
	public abstract class PerfectCullingBakingBehaviour : MonoBehaviour
	{
		public enum EOutOfBoundsBehaviour
		{
			ClampToNearestCell = 0,
			Cull = 1,
			IgnoreDoNothing = 2
		}

		[Tooltip("Try to find a non-empty cell if we hit an empty one?")]
		public bool searchForNonEmptyCells;

		[Tooltip("What should happen if we encounter an empty cell? Cull everything or make everything visible?")]
		public EEmptyCellCullBehaviour emptyCellCullBehaviour;

		[Tooltip("Should this volume be culled if the camera is not inside it or should the camera position be clamped to the nearest cell?")]
		public EOutOfBoundsBehaviour outOfBoundsBehaviour;

		private IActiveSamplingProvider[] m_activeSamplingProviders;

		[SerializeField]
		public PerfectCullingBakeGroup[] bakeGroups;

		[SerializeField]
		public List<GameObject> ignoredParents;

		[SerializeField]
		public List<string> ignoredObjectNames;

		[SerializeField]
		public List<Renderer> additionalOccluders;

		[NonSerialized]
		public int TotalVertexCount;

		private bool[] m_renderersState;

		public virtual PerfectCullingBakeData BakeData { get; }

		public IActiveSamplingProvider[] QuerySamplingProviders()
		{
			return null;
		}

		public void InitializeAllSamplingProviders()
		{
		}

		public bool SamplingProvidersIsPositionActive(Vector3 pos)
		{
			return false;
		}

		public virtual void Start()
		{
		}

		public void QueueToggleAllRenderers(bool state)
		{
		}

		public void QueueToggleRenderer(int index, bool state, out PerfectCullingBakeGroup modifiedBakeGroup)
		{
			modifiedBakeGroup = null;
		}

		public void ExecuteQueue(bool suppressException = false)
		{
		}

		public virtual void SetBakeData(PerfectCullingBakeData bakeData)
		{
		}

		public virtual List<Vector3> GetSamplingPositions(Space space = Space.Self)
		{
			return null;
		}

		public virtual void GetIndicesForWorldPos(Vector3 worldPos, List<ushort> indices)
		{
		}

		public virtual int GetIndexForWorldPos(Vector3 worldPos, out bool isOutOfBounds)
		{
			isOutOfBounds = default(bool);
			return 0;
		}

		public virtual void GetIndicesForIndex(int index, List<ushort> indices)
		{
		}

		public virtual bool PreBake()
		{
			return false;
		}

		public virtual void PostBake()
		{
		}

		public virtual int GetBakeHash()
		{
			return 0;
		}

		public virtual void CullAdditionalOccluders(ref HashSet<Renderer> additionalOccluders)
		{
		}
	}
}
