using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Koenigz.PerfectCulling
{
	[RequireComponent(typeof(Camera))]
	public class PerfectCullingCamera : MonoBehaviour
	{
		public static List<PerfectCullingCamera> AllCameras;

		[Tooltip("Allows to take into account neighbor cells to prevent popping issues. It's a great way to compensate for a too sparse bake. This comes with a minor performance impact.\n\nYou can achieve even better results without performance implications by baking this in by using the Merge-Downsample feature for your bakes.")]
		[Range(0f, 2f)]
		public int NeighborCellIncludeRadius;

		public PerfectCullingVisibilityLayer visibilityLayer;

		private bool m_invertCulling;

		private static readonly bool[] m_visibleRenderers;

		private static int m_lastFrame;

		private int m_lastFrameHash;

		private Camera m_camera;

		private static readonly Vector3[] m_offsets;

		private bool m_cullingPreview;

		private readonly string m_CamPreCull_SampleName;

		public bool ShowInGameStats { get; set; }

		public bool VisualizeFrustumCulling { get; set; }

		public int LastTotalVertices { get; private set; }

		public int LastVisibleVertices { get; private set; }

		public int LastCulledVertices => 0;

		public int LastVisible { get; private set; }

		public int LastTotal { get; private set; }

		public int LastCulled => 0;

		public int LastFrameHash => 0;

		public bool InvertCulling
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		private void Awake()
		{
		}

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		private void OnBeginCameraRendering(ScriptableRenderContext context, Camera camera)
		{
		}

		private void OnPreCull()
		{
		}

		private void CamPreCull(Camera camera)
		{
		}

		private void PerformCameraCulling(Camera camera)
		{
		}

		private void SetDirty()
		{
		}
	}
}
