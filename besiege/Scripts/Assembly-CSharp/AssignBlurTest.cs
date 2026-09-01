using UnityEngine;

public class AssignBlurTest : MonoBehaviour
{
	public Transform target;

	public string camName;

	protected BlurCamTest blur;

	public Camera camera
	{
		get
		{
			if (blur == null)
			{
				Awake();
			}
			return blur.BlurCam;
		}
	}

	private void Awake()
	{
		GameObject gameObject = GameObject.Find(camName);
		if (gameObject != null)
		{
			blur = gameObject.GetComponent<BlurCamTest>();
			if (blur != null)
			{
				blur.target = target;
				return;
			}
		}
		blur = Object.FindObjectOfType<BlurCamTest>();
		blur.target = target;
	}

	private void OnDisable()
	{
		if (blur != null && blur.target == target)
		{
			blur.target = null;
		}
	}
}
