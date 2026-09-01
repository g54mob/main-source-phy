using UnityEngine;

public class Translator : MonoBehaviour
{
	public float Speed;

	public float Amplitude;

	public Vector3 Direction;

	public bool AlongOwnAxis;

	public float SinOffset;

	public bool ResetOnDisable;

	protected float _angleInRad;

	protected Vector3 _startPos;

	private void Awake()
	{
	}

	private void Update()
	{
	}

	private void OnDisable()
	{
	}

	public void Toggle()
	{
	}
}
