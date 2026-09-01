using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Particle
{
	public bool IsStickySide;

	public FracturePiece.Side Side;

	public GameObject Object;

	public float Mass;

	public bool HasRigidbody;

	public Rigidbody Body;

	public BuildSurface StickyAttachment;

	public void CreateRigidbody()
	{
		if (!HasRigidbody)
		{
			Body = Object.AddComponent<Rigidbody>();
			Body.mass = Mass;
			Body.drag = 0f;
			Body.solverIterations = 5;
			Body.interpolation = RigidbodyInterpolation.Interpolate;
			HasRigidbody = true;
		}
	}

	public void StartAnimation(MonoBehaviour b)
	{
		b.StartCoroutine(ScaleAndDestroy());
	}

	private IEnumerator ScaleAndDestroy()
	{
		yield return new WaitForSeconds(5f);
		float time = 0f;
		while (time < 1f)
		{
			time += Time.deltaTime;
			Vector3 scale = Vector3.Lerp(Vector3.one, Vector3.zero, time / 1f);
			Object.transform.localScale = scale;
			yield return null;
		}
		UnityEngine.Object.Destroy(Object);
	}
}
