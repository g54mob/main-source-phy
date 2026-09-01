using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class TurretLocationIcon : MonoBehaviour
{
	[Header("References")]
	public GameObject VisualRoot;

	[Header("Reveal")]
	public bool UpdateVisualOnMove;

	public string RevealAreaTag;

	public float RectanglePadding;

	public bool StartWithVisualRootHidden;

	public bool IgnoreParentRotation;

	public float ScanWindowDurationSeconds;

	public float ScanIntervalSeconds;

	[Header("Events")]
	public UnityEvent<TurretLocationIcon> OnMove;

	public UnityEvent<TurretLocationIcon> OnRevealed;

	private static bool warnedMissingRevealTag;

	private Vector3 visualRootWorldPosition;

	private Quaternion visualRootWorldRotation;

	private bool hasVisualRootWorldPosition;

	private bool hasVisualRootWorldRotation;

	private bool scanActive;

	private float scanWindowEndTime;

	private float nextScanTime;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void LateUpdate()
	{
	}

	public void OnLocationMoved()
	{
	}

	public void StartScanWindow()
	{
	}

	public void StopScanWindow()
	{
	}

	private void OnImpact(Vector2 impactLocation, float impactRadius)
	{
	}

	private bool EvaluateRevealArea()
	{
		return false;
	}

	private bool CheckTaggedRectangles(Vector3 worldPos)
	{
		return false;
	}

	private void RevealVisualRoot()
	{
	}

	private void HideVisualRoot()
	{
	}

	private void KeepVisualRootLocked()
	{
	}

	private void CacheVisualRootWorldRotation()
	{
	}

	private void ApplyVisualRootWorldRotation()
	{
	}

	private static bool IsWorldPointInsideRectTransform(RectTransform rect, Vector3 worldPoint, float padding)
	{
		return false;
	}
}
