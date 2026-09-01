using UnityEngine;

public class ProximityActivate : MonoBehaviour
{
	public Transform distanceActivator;

	public Transform lookAtActivator;

	public float distance;

	public Transform activator;

	public bool activeState;

	public CanvasGroup target;

	public bool lookAtCamera;

	public bool enableInfoPanel;

	public GameObject infoIcon;

	private float alpha;

	public CanvasGroup infoPanel;

	private Quaternion originRotation;

	private Quaternion targetRotation;

	private void Start()
	{
	}

	private bool IsTargetNear()
	{
		return false;
	}

	private void Update()
	{
	}
}
