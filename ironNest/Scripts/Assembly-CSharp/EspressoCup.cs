using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
[RequireComponent(typeof(DraggableItem))]
[AddComponentMenu("Espresso/Espresso Cup")]
public class EspressoCup : MonoBehaviour
{
	[Header("Cup State")]
	[Tooltip("Whether this cup currently contains espresso.\n\nSet to FALSE in the Inspector for the empty cup prefab.\nSet to TRUE automatically by EspressoBrewingController.InitialiseResult().\n\nSafe default: false (empty cup).")]
	[SerializeField]
	private bool isFull;

	[Header("Quality Bands")]
	[Tooltip("Quality % at or above which this cup is graded 'Perfect'.\nSafe default: 90.0")]
	[SerializeField]
	private float gradePerfectThreshold;

	[Tooltip("Quality % threshold for the 'Good' grade.\nSafe default: 70.0")]
	[SerializeField]
	private float gradeGoodThreshold;

	[Tooltip("Quality % threshold for the 'Acceptable' grade.\nSafe default: 50.0")]
	[SerializeField]
	private float gradeAcceptableThreshold;

	[Tooltip("Quality % threshold for the 'Poor' grade.\nSafe default: 30.0")]
	[SerializeField]
	private float gradePoorThreshold;

	[Header("Events")]
	[Tooltip("Fired when this cup is picked up by the player AND the cup is full.\n\nOnly fires after InitialiseResult() has been called — picking up an\nempty cup does NOT fire this event.\n\nAuto-wired by EspressoBrewingController on brew complete.")]
	public UnityEvent OnCupPickedUp;

	[Tooltip("Fired immediately after this cup is filled (InitialiseResult is called).\nParameter: quality as a percentage (0.00–100.00).")]
	public UnityEvent<float> OnResultInitialised;

	[Tooltip("Fired immediately after this cup is filled (InitialiseResult is called),\nafter OnResultInitialised.\nUse this to react to the cup becoming full without needing the quality value.")]
	public UnityEvent OnCupFilled;

	[Tooltip("Fired immediately after this cup is emptied (MarkEmpty is called).\nUse this to react to the cup returning to the empty state.")]
	public UnityEvent OnCupEmptied;

	[Header("Result — Runtime (Read Only)")]
	[Tooltip("Final brew quality as a percentage (0.00–100.00).\nOnly meaningful when IsFull = true. Read-only.")]
	[SerializeField]
	private float quality;

	[Tooltip("Pressure system score as a percentage (0.00–100.00).\nRunning mean of per-frame pressure accuracy across the full brew.\nOnly meaningful when IsFull = true. Read-only.")]
	[SerializeField]
	private float pressureScore;

	[Tooltip("Temperature system score as a percentage (0.00–100.00).\nRunning mean of per-frame temperature accuracy across the full brew.\nOnly meaningful when IsFull = true. Read-only.")]
	[SerializeField]
	private float temperatureScore;

	[Tooltip("Timing score as a percentage (0.00–100.00).\nHow close the brew stop time was to the ideal brew duration.\nOnly meaningful when IsFull = true. Read-only.")]
	[SerializeField]
	private float timingScore;

	[Tooltip("Label of the coffee grounds used in this brew.\nOnly meaningful when IsFull = true. Read-only.")]
	[SerializeField]
	private string coffeeLabel;

	[Tooltip("True once InitialiseResult() has been called. Read-only.")]
	[SerializeField]
	private bool isInitialised;

	[Tooltip("True once the player has picked this filled cup up at least once. Read-only.")]
	[SerializeField]
	private bool hasBeenPickedUp;

	private DraggableItem _draggable;

	public bool IsFull => false;

	public bool IsEmpty => false;

	public float Quality => 0f;

	public float PressureScore => 0f;

	public float TemperatureScore => 0f;

	public float TimingScore => 0f;

	public string CoffeeLabel => null;

	public bool IsInitialised => false;

	public bool HasBeenPickedUp => false;

	public string QualityGrade => null;

	private void Awake()
	{
	}

	private void OnDestroy()
	{
	}

	public void InitialiseResult(float qualityPct, float pressurePct, float temperaturePct, float timingPct, string label)
	{
	}

	public void MarkEmpty()
	{
	}

	private void HandlePickedUp()
	{
	}
}
