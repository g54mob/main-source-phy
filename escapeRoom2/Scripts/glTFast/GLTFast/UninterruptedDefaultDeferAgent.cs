using UnityEngine;

namespace GLTFast
{
	[DefaultExecutionOrder(-1)]
	internal class UninterruptedDefaultDeferAgent : MonoBehaviour
	{
		private UninterruptedDeferAgent m_DeferAgent;

		private void OnEnable()
		{
			m_DeferAgent = new UninterruptedDeferAgent();
			GltfImportBase.SetDefaultDeferAgent(m_DeferAgent);
		}

		private void OnDisable()
		{
			GltfImportBase.UnsetDefaultDeferAgent(m_DeferAgent);
			m_DeferAgent = null;
		}
	}
}
