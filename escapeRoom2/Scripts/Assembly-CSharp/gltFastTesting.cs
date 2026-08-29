using GLTFast;
using UnityEngine;

public class gltFastTesting : MonoBehaviour
{
	private bool loaded;

	private void Update()
	{
		if (Time.time > 2f && !loaded)
		{
			loaded = true;
			loadGltf("https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/master/2.0/Duck/glTF/Duck.gltf");
		}
	}

	private bool loadGltf(string url)
	{
		new GameObject("glTF Model").AddComponent<GltfAsset>().Url = url;
		return false;
	}
}
