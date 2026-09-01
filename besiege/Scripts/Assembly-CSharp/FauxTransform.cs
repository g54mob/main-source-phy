using UnityEngine;

public class FauxTransform
{
	public Vector3 localPosition;

	public Quaternion localRotation;

	public Vector3 localScale;

	public FauxTransform(Vector3 localPosition, Quaternion localRotation, Vector3 localScale)
	{
		this.localPosition = localPosition;
		this.localRotation = localRotation;
		this.localScale = localScale;
	}
}
