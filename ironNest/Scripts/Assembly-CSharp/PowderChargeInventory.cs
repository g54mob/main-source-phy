using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class PowderChargeInventory : MonoBehaviour
{
	[Header("Inventory Settings")]
	[Tooltip("The number of powder charges the player starts the game with.")]
	[SerializeField]
	private int startingCharges;

	[Tooltip("The maximum number of powder charges the player can hold at one time.")]
	[SerializeField]
	private int maxCapacity;

	[Header("Debugging")]
	[Tooltip("DEBUG VIEW: The current number of charges in the inventory. This value is read-only and updated by the script.")]
	[SerializeField]
	private int currentChargesForInspector;

	[Header("Unity Events")]
	[Tooltip("Invoked when the remaining charges are exactly 0.")]
	[SerializeField]
	private UnityEvent onInventoryEmpty;

	[Tooltip("Invoked when the remaining charges are between 1 and 6 (inclusive).")]
	[SerializeField]
	private UnityEvent onSixOrLessRemaining;

	[Tooltip("Invoked when the remaining charges are 7 or more.")]
	[SerializeField]
	private UnityEvent onMoreThanSixRemaining;

	[Tooltip("If true, threshold events (empty, 1..6, 7+) will also be invoked based on the starting value on Start().")]
	[SerializeField]
	private bool invokeThresholdEventsOnStart;

	private int _currentCharges;

	public static PowderChargeInventory Instance { get; private set; }

	public int CurrentCharges
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public float CurrentChargesAsFloat => 0f;

	public float CurrentChargesAsPercent => 0f;

	public event Action<int> OnChargesChanged
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public bool TryUseCharge()
	{
		return false;
	}

	public void AddCharges(int amount)
	{
	}

	private void InvokeStateEventsForCount(int count)
	{
	}
}
