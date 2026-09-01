using UnityEngine;

public class EnableCam : MonoBehaviour
{
	public CamToggleControl cam;

	public string findCam;

	private bool _enabled;

	public Camera target
	{
		get
		{
			if (!_enabled)
			{
				OnEnable();
			}
			return cam.Cam;
		}
	}

	private void OnEnable()
	{
		if (_enabled)
		{
			return;
		}
		_enabled = true;
		if (cam == null)
		{
			if (findCam != null && findCam != string.Empty)
			{
				cam = GameObject.Find(findCam).GetComponent<CamToggleControl>();
			}
			else
			{
				cam = Object.FindObjectOfType<CamToggleControl>();
			}
		}
		if (cam != null)
		{
			cam.AddObject(base.gameObject);
		}
	}

	public void OnDisable()
	{
		if (_enabled)
		{
			_enabled = false;
			if (cam != null)
			{
				cam.RemoveObject(base.gameObject);
			}
		}
	}
}
