using UnityEngine;

[RequireComponent(typeof(DraggableItem))]
public class DraggableItemSpeedProvider : MonoBehaviour, IFloatValueProvider
{
	[SerializeField]
	private DraggableItem _draggable;

	[SerializeField]
	private bool _useUnscaledTime;

	[SerializeField]
	[Min(0.01f)]
	private float _speedNormalizationRange;

	private bool _hasPreviousPosition;

	private Vector3 _previousPosition;

	[Header("Debug")]
	[field: SerializeField]
	public float Speed { get; private set; }

	[field: SerializeField]
	public float NormalizedSpeed { get; private set; }

	float IFloatValueProvider.GetFloatValue()
	{
		return 0f;
	}

	private void Start()
	{
	}

	private void Reset()
	{
	}

	private void Update()
	{
	}
}
