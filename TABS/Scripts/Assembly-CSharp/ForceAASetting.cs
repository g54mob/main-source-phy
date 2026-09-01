using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ForceAASetting : MonoBehaviour
{
	private PostProcessLayer layer;

	private void Start()
	{
		layer = GetComponent<PostProcessLayer>();
	}

	private void Update()
	{
		layer.antialiasingMode = PostProcessLayer.Antialiasing.SubpixelMorphologicalAntialiasing;
	}
}
