using UnityEngine;

public class AssignBlurToCam : MonoBehaviour
{
	public Transform target;

	public BlurCamTest cam;

	private void Awake()
	{
		cam.target = target;
	}
}
