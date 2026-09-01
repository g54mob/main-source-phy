using System;
using System.Collections;
using UnityEngine;

public class PlayButton : ClickBehaviour
{
	public Renderer playMesh;

	public Renderer stopMesh;

	public Material redMaterial;

	public Material darkMaterial;

	private Vector3 startPlayScale;

	private Vector3 startStopScale;

	public Renderer myRenderer;

	private void Start()
	{
		ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Combine(ReferenceMaster.onLevelSimulation, new Action<bool>(OnSimulationToggle));
		startPlayScale = playMesh.transform.localScale;
		startStopScale = stopMesh.transform.localScale;
		if (myRenderer == null)
		{
			myRenderer = GetComponent<Renderer>();
		}
		releaseOnlyOver = true;
	}

	private void OnSimulationToggle(bool isSim)
	{
		if (isSim)
		{
			Play();
		}
		else
		{
			Stop();
		}
	}

	private void OnDestroy()
	{
		ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Remove(ReferenceMaster.onLevelSimulation, new Action<bool>(OnSimulationToggle));
	}

	public override void OnClicked()
	{
		if (OnActivation != null)
		{
			OnActivation();
		}
		ScaleIconDown();
	}

	public override void OnClickReleased()
	{
		StartCoroutine(ReleaseClick());
	}

	protected IEnumerator ReleaseClick()
	{
		ScaleIconUp();
		yield return null;
		SingleInstanceFindOnly<AddPiece>.Instance.ToggleSimulate();
	}

	public void Play()
	{
		if (!StatMaster.isMP)
		{
			playMesh.enabled = false;
			stopMesh.enabled = true;
		}
		myRenderer.material = redMaterial;
	}

	public void Stop()
	{
		if (!StatMaster.isMP)
		{
			playMesh.enabled = true;
			stopMesh.enabled = false;
		}
		myRenderer.material = darkMaterial;
	}

	private void ScaleIconDown()
	{
		Machine machine = Machine.Active();
		if (machine != null && machine.isSimulating)
		{
			stopMesh.transform.localScale = startStopScale * 0.6f;
		}
		else
		{
			playMesh.transform.localScale = startPlayScale * 0.6f;
		}
	}

	private void ScaleIconUp()
	{
		Machine machine = Machine.Active();
		if (machine != null && machine.isSimulating)
		{
			stopMesh.transform.localScale = startStopScale;
		}
		else
		{
			playMesh.transform.localScale = startPlayScale;
		}
	}
}
