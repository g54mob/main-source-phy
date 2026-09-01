using System.Collections;
using UnityEngine;

public class TrailerSmoothZoomPan : MonoBehaviour
{
	public Camera cam;

	public MouseOrbit camoRBITcODE;

	public float zoomLerpDuration = 10f;

	public float endFov = 20f;

	public float orbitAmount = 5f;

	private IEnumerator lerpZoomCoroutine;

	private IEnumerator lerpOrbitCoroutine;

	private void Update()
	{
		if (Input.GetKeyDown("j"))
		{
			lerpZoomCoroutine = LerpZoom();
			StartCoroutine(lerpZoomCoroutine);
		}
		if (Input.GetKeyDown("h"))
		{
			lerpOrbitCoroutine = LerpOrbit();
			StartCoroutine(lerpOrbitCoroutine);
		}
		if (Input.GetKeyDown("k"))
		{
			StopCoroutine(lerpZoomCoroutine);
			StopCoroutine(lerpOrbitCoroutine);
			ResetZoom();
		}
	}

	private IEnumerator LerpZoom()
	{
		float cTime = 0f;
		float rate = 1f / zoomLerpDuration;
		float currentZoom = camoRBITcODE.distance;
		while (cTime < 1f)
		{
			cTime += TimeSlider.Instance.deltaTime * rate;
			camoRBITcODE.distance = Mathf.Lerp(currentZoom, endFov, cTime);
			yield return null;
		}
	}

	private IEnumerator LerpOrbit()
	{
		float cTime = 0f;
		float rate = 1f / zoomLerpDuration;
		while (cTime < 1f)
		{
			camoRBITcODE.x += TimeSlider.Instance.deltaTime * rate * orbitAmount;
			yield return null;
		}
	}

	private void ResetZoom()
	{
		cam.fieldOfView = 41f;
	}
}
