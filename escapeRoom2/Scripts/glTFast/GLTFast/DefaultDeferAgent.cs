using UnityEngine;

namespace GLTFast
{
	[RequireComponent(typeof(IDeferAgent))]
	[DefaultExecutionOrder(-1)]
	internal class DefaultDeferAgent : MonoBehaviour
	{
		private void OnEnable()
		{
			IDeferAgent component = GetComponent<IDeferAgent>();
			if (component != null)
			{
				GltfImportBase.SetDefaultDeferAgent(component);
			}
		}

		private void OnDisable()
		{
			IDeferAgent component = GetComponent<IDeferAgent>();
			if (component != null)
			{
				GltfImportBase.UnsetDefaultDeferAgent(component);
			}
		}
	}
}
