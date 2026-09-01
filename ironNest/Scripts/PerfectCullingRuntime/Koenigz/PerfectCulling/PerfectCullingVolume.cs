using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Koenigz.PerfectCulling
{
	public class PerfectCullingVolume : PerfectCullingBakingBehaviour, CustomHandle.IResizableByHandle
	{
		[FormerlySerializedAs("VolumeSize")]
		[SerializeField]
		public Vector3 volumeSize;

		public static readonly List<PerfectCullingVolume> AllVolumes;

		[FormerlySerializedAs("VolumeBakeData")]
		public PerfectCullingVolumeBakeData volumeBakeData;

		[FormerlySerializedAs("BakeCellSize")]
		[Tooltip("The size of a single cell. This needs to be a divisor of the scale of the volume.")]
		[SerializeField]
		public Vector3 bakeCellSize;

		[FormerlySerializedAs("MergeDownsampleIterations")]
		[Tooltip("After the bake completed for each cell all neighbor cells are merged into a single cell. This will reduce the number of cells without introducing culling issues. This is useful to reduce memory usage.")]
		[Range(0f, 8f)]
		public int mergeDownsampleIterations;

		public Vector3Int[] mergeDownsampleAxes;

		public PerfectCullingVisibilityLayer visibilityLayer;

		private List<PerfectCullingPortalCell> portalCells;

		public Bounds volumeBakeBounds
		{
			get
			{
				return default(Bounds);
			}
			set
			{
			}
		}

		public int CellCount => 0;

		public bool VisualizeProbes { get; set; }

		public bool VisualizeGridCells { get; set; }

		public bool VisualizeHitLines { get; set; }

		public int RenderersCount => 0;

		public override PerfectCullingBakeData BakeData => null;

		public Vector3 HandleSized
		{
			get
			{
				return default(Vector3);
			}
			set
			{
			}
		}

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		private void SortVolumes()
		{
		}

		private void OnDestroy()
		{
		}

		public override void GetIndicesForWorldPos(Vector3 worldPos, List<ushort> indices)
		{
		}

		public override List<Vector3> GetSamplingPositions(Space space = Space.Self)
		{
			return null;
		}

		public override bool PreBake()
		{
			return false;
		}

		public override void CullAdditionalOccluders(ref HashSet<Renderer> additionalOccluders)
		{
		}

		public override void PostBake()
		{
		}

		public Vector3 AlignToGrid(Vector3 pos)
		{
			return default(Vector3);
		}

		public override int GetIndexForWorldPos(Vector3 pos, out bool isOutOfBounds)
		{
			isOutOfBounds = default(bool);
			return 0;
		}

		private int GetIndexForWorldPos(Vector3 pos, Vector3 cellSize, out bool isOutOfBounds, bool emptyCellScanAllowed = true)
		{
			isOutOfBounds = default(bool);
			return 0;
		}

		public void AddPortalCell(PerfectCullingPortalCell portalCell)
		{
		}

		public void RemovePortalCell(PerfectCullingPortalCell portalCell)
		{
		}

		private int GetIndexForWorldPos(Vector3 pos, Vector3 cellCount, Vector3 cellSize, out bool isOutOfBounds, bool emptyCellScanAllowed = true)
		{
			isOutOfBounds = default(bool);
			return 0;
		}

		public override void SetBakeData(PerfectCullingBakeData bakeData)
		{
		}

		private Vector3 GetSamplingPositionAt(int index, Vector3 cellSize, Space space = Space.Self)
		{
			return default(Vector3);
		}

		private Vector3 GetSamplingPositionAt(int index, Vector3 gridSize, Vector3 cellSize, Space space = Space.Self)
		{
			return default(Vector3);
		}

		public override int GetBakeHash()
		{
			return 0;
		}
	}
}
