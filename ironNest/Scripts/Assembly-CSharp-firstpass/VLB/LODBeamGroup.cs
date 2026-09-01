using UnityEngine;

namespace VLB
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(LODGroup))]
	[HelpURL("http://saladgamer.com/vlb-doc/comp-lodbeamgroup/")]
	public class LODBeamGroup : MonoBehaviour
	{
		[SerializeField]
		private VolumetricLightBeamAbstractBase[] m_LODBeams;

		[SerializeField]
		private bool m_ResetAllLODsLocalTransform;

		[SerializeField]
		private BeamProps m_LOD0PropsToCopy;

		[SerializeField]
		private bool m_CopyLOD0PropsEachFrame;

		[SerializeField]
		private bool m_CullVolumetricDustParticles;

		private LODGroup m_LODGroup;

		private void Awake()
		{
		}

		private void Start()
		{
		}

		public LOD[] GetLODsFromLODGroup()
		{
			return null;
		}

		private void SetLODRenderer(int lodIdx, Renderer renderer)
		{
		}

		private void SetLODRenderers(int lodIdx, Renderer[] renderers)
		{
		}

		private void SetLOD(int lodIdx)
		{
		}

		private void OnBeamGeometryGenerated(VolumetricLightBeamAbstractBase beam)
		{
		}

		private void SetupLodGroupData()
		{
		}

		private void UnifyBeamsProperties()
		{
		}

		private void Update()
		{
		}
	}
}
