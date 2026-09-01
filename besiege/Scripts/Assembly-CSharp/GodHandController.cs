using System.Collections;
using UnityEngine;

public class GodHandController : MonoBehaviour
{
	public Camera hudCam;

	public DragRigidbody dragRigidbody;

	public Transform squareIcon1;

	public Vector3 screenPos;

	public Transform lineObj;

	public Transform visHolder;

	public AudioClip[] sfx;

	private AudioSource audioSource;

	private float startZpos;

	private bool isActive = true;

	private Coroutine activeRoutine;

	private Camera mainCam;

	protected void Start()
	{
		audioSource = GetComponent<AudioSource>();
		startZpos = squareIcon1.position.z;
		Toggle(false);
		mainCam = Camera.main;
		if (hudCam == null)
		{
			hudCam = SingleInstanceFindOnly<AddPiece>.Instance.hudCam;
			if (hudCam == null)
			{
				Debug.Log("Could not find hud camera in GodHandController");
			}
		}
	}

	protected void LateUpdate()
	{
		if (dragRigidbody.hasJoint())
		{
			if (isActive)
			{
				UpdateVis();
			}
			else
			{
				Toggle(true);
			}
		}
		else if (isActive)
		{
			Toggle(false);
		}
	}

	private void UpdateVis()
	{
		screenPos = mainCam.WorldToScreenPoint(dragRigidbody.springJoint.connectedBody.transform.TransformPoint(dragRigidbody.springJoint.connectedAnchor));
		Vector3 vector = hudCam.ScreenToWorldPoint(screenPos);
		squareIcon1.position = new Vector3(vector.x, vector.y, startZpos);
		Vector3 vector2 = hudCam.ScreenToWorldPoint(InputManager.CursorPosition());
		lineObj.position = new Vector3(vector2.x, vector2.y, startZpos);
	}

	private void Toggle(bool t, bool playSound = true)
	{
		if (isActive != t)
		{
			isActive = t;
			if (playSound)
			{
				audioSource.clip = ((!t) ? sfx[1] : sfx[0]);
				audioSource.Play();
			}
			if (activeRoutine != null)
			{
				StopCoroutine(activeRoutine);
			}
			if (t)
			{
				UpdateVis();
				activeRoutine = StartCoroutine(IEActivate());
			}
			else
			{
				ToggleVis(false);
			}
		}
	}

	private void ToggleVis(bool t)
	{
		visHolder.gameObject.SetActive(t);
	}

	private IEnumerator IEActivate()
	{
		yield return new WaitForEndOfFrame();
		ToggleVis(true);
	}
}
