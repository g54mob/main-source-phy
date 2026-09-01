using UnityEngine;

[AddComponentMenu("UI/Translate Tool (rotator)")]
public class TranslateTool : MonoBehaviour
{
	public Transform cam;

	private void Update()
	{
		base.transform.rotation = Quaternion.Inverse(cam.rotation);
	}
}
