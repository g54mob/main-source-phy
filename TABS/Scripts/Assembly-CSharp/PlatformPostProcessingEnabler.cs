using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(PostProcessLayer))]
public class PlatformPostProcessingEnabler : MonoBehaviour
{
	private PostProcessLayer layer;

	private void Start()
	{
		layer = GetComponent<PostProcessLayer>();
		if (layer != null)
		{
			SetPostProcessLayerStateForPlatform();
		}
	}

	private void SetPostProcessLayerStateForPlatform()
	{
		layer.enabled = true;
	}
}
