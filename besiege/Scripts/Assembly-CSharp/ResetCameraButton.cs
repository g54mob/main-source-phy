using UnityEngine;

public class ResetCameraButton : ClickBehaviour
{
	public Renderer bgRend;

	public Material clickedMaterial;

	private AudioSource aSource;

	private void Start()
	{
		aSource = GetComponent<AudioSource>();
		releaseOnlyOver = true;
	}

	private void Update()
	{
		if (InputManager.ResetCameraButton())
		{
			if (SingleInstanceFindOnly<MouseOrbit>.hasInstance())
			{
				SingleInstanceFindOnly<MouseOrbit>.Instance.ResetCam();
			}
			aSource.Play();
		}
	}

	public override void OnClicked()
	{
		bgRend.enabled = true;
	}

	public override void OnClickReleased()
	{
		if (SingleInstanceFindOnly<MouseOrbit>.hasInstance())
		{
			SingleInstanceFindOnly<MouseOrbit>.Instance.ResetCam();
		}
		aSource.Play();
		bgRend.enabled = false;
	}

	public void OnMouseExit()
	{
		_wasClicked = false;
		bgRend.enabled = false;
	}
}
