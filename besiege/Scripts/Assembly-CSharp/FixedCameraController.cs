using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCameraController : SingleInstance<FixedCameraController>
{
	public List<FixedCameraBlock> cameras = new List<FixedCameraBlock>();

	[HideInInspector]
	public FixedCameraBlock activeCamera;

	protected List<FixedCameraBlock> relevant = new List<FixedCameraBlock>();

	private KeyCode lastKey;

	public override string Name
	{
		get
		{
			return "FixedCameraController";
		}
	}

	public void Register(FixedCameraBlock camera)
	{
		cameras.Add(camera);
	}

	public void Unregister(FixedCameraBlock camera)
	{
		cameras.Remove(camera);
	}

	protected void UpdateCameraList()
	{
		relevant.Clear();
		foreach (FixedCameraBlock camera in cameras)
		{
			if (camera.ParentMachine.isLocalMachine && camera.KeyCode == lastKey)
			{
				relevant.Add(camera);
			}
		}
	}

	public void OnKeyPressed(KeyCode key)
	{
		StopAllCoroutines();
		lastKey = key;
		StartCoroutine(UpdateCam());
	}

	public IEnumerator UpdateCam()
	{
		yield return new WaitForEndOfFrame();
		FixedCameraBlock lastActive = activeCamera;
		UpdateCameraList();
		if (lastActive != null)
		{
			int index = relevant.IndexOf(lastActive);
			if (index == -1)
			{
				if (relevant.Count > 0)
				{
					SetActiveCam(relevant[0]);
				}
			}
			else if (index < relevant.Count - 1)
			{
				SetActiveCam(relevant[index + 1]);
			}
			else
			{
				ResetCam();
			}
		}
		else
		{
			FixedCameraBlock block = FindFirstAppropriateCam(lastKey);
			if ((bool)block)
			{
				SetActiveCam(block);
			}
		}
	}

	public void Activate(FixedCameraBlock block)
	{
		SetActiveCam(block);
	}

	protected void Update()
	{
		if (SingleInstanceFindOnly<MouseOrbit>.Instance == null)
		{
			return;
		}
		float verticalFOV = OptionsMaster.GetVerticalFOV();
		Camera cam = SingleInstanceFindOnly<MouseOrbit>.Instance.cam;
		if (cam.fieldOfView != verticalFOV)
		{
			Machine machine = Machine.Active();
			if (!machine || !machine.isSimulating)
			{
				SetCameraFovs(verticalFOV);
			}
		}
	}

	private void SetActiveCam(FixedCameraBlock block)
	{
		if (!(block == activeCamera))
		{
			if ((bool)activeCamera)
			{
				activeCamera.isActive = false;
			}
			activeCamera = block;
			activeCamera.isActive = true;
			MouseOrbit mouseOrbit = SingleInstanceFindOnly<MouseOrbit>.Instance;
			if (mouseOrbit != null)
			{
				mouseOrbit.isActive = false;
			}
			SetFOV(block);
		}
	}

	private void ResetCam()
	{
		if ((bool)activeCamera)
		{
			activeCamera.isActive = false;
			activeCamera = null;
		}
		SingleInstanceFindOnly<MouseOrbit>.Instance.isActive = true;
		SetFOV();
	}

	private FixedCameraBlock FindFirstAppropriateCam(KeyCode key)
	{
		foreach (FixedCameraBlock camera in cameras)
		{
			if (!camera.ParentMachine.isLocalMachine || camera.KeyCode != key)
			{
				continue;
			}
			return camera;
		}
		return null;
	}

	private void SetFOV()
	{
		SetCameraFovs(OptionsMaster.GetVerticalFOV());
	}

	private void SetFOV(FixedCameraBlock camera)
	{
		if ((camera != null && camera.CamMode == FixedCameraBlock.Mode.FirstPerson) || camera.CamMode == FixedCameraBlock.Mode.Custom)
		{
			SetCameraFovs(camera.fovSlider.Value);
		}
		else
		{
			SetCameraFovs(OptionsMaster.GetVerticalFOV());
		}
	}

	private void SetCameraFovs(float fov)
	{
		SingleInstanceFindOnly<MouseOrbit>.Instance.cam.fieldOfView = fov;
		SingleInstanceFindOnly<MouseOrbit>.Instance.hud3Dcam.fieldOfView = fov;
	}
}
