using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class EspressoCup : MonoBehaviour
{
	private bool isFull;

	private float gradePerfectThreshold = 90f;

	private float gradeGoodThreshold = 70f;

	private float gradeAcceptableThreshold = 50f;

	private float gradePoorThreshold = 30f;

	public UnityEvent OnCupPickedUp;

	public UnityEvent<float> OnResultInitialised;

	public UnityEvent OnCupFilled;

	public UnityEvent OnCupEmptied;

	private float quality;

	private float pressureScore;

	private float temperatureScore;

	private float timingScore;

	private string coffeeLabel;

	private bool isInitialised;

	private bool hasBeenPickedUp;

	private DraggableItem _draggable;

	public bool IsFull => isFull;

	public bool IsEmpty => !isFull;

	public float Quality => quality;

	public float PressureScore => pressureScore;

	public float TemperatureScore => temperatureScore;

	public float TimingScore => timingScore;

	public string CoffeeLabel => coffeeLabel;

	public bool IsInitialised => isInitialised;

	public bool HasBeenPickedUp => hasBeenPickedUp;

	public string QualityGrade
	{
		get
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AA6F]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			if (isFull)
			{
				if (quality < gradePerfectThreshold)
				{
					if (quality < gradeGoodThreshold)
					{
						if (quality < gradeAcceptableThreshold)
						{
							bool flag = !(quality < gradePoorThreshold);
							string result = "Poor";
							if (!flag)
							{
								result = "Undrinkable";
							}
							return result;
						}
						return "Acceptable";
					}
					return "Good";
				}
				return "Perfect";
			}
			return "Empty";
		}
	}

	private void Awake()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		DraggableItem draggable = default(DraggableItem);
		_draggable = draggable;
		DraggableItem draggable2 = _draggable;
		UnityAction call = HandlePickedUp;
		draggable2.OnPickedUpByPlayer.AddListener(call);
	}

	private void OnDestroy()
	{
		if (_draggable != null)
		{
			DraggableItem draggable = _draggable;
			UnityAction call = HandlePickedUp;
			draggable.OnPickedUpByPlayer.RemoveListener(call);
		}
	}

	public unsafe void InitialiseResult(float qualityPct, float pressurePct, float temperaturePct, float timingPct, string label)
	{
		//IL_00a6: Expected F4, but got Ref
		float num = qualityPct * 100f;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
		float num2 = num / 100f;
		float num3 = pressurePct * 100f;
		quality = num2;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
		float num4 = num3 / 100f;
		float num5 = temperaturePct * 100f;
		pressureScore = num4;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
		float num6 = num5 / 100f;
		temperatureScore = num6;
		object obj = default(object);
		float num7 = (float)obj * 100f;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
		float num8 = num7 / 100f;
		string text = default(string);
		coffeeLabel = text;
		timingScore = num8;
		isFull = true;
		isInitialised = true;
		if (OnResultInitialised != null)
		{
			OnResultInitialised.Invoke((nint)(&obj));
		}
		if (OnCupFilled != null)
		{
			OnCupFilled.Invoke();
		}
	}

	public void MarkEmpty()
	{
		//IL_003c: Expected O, but got I
		//IL_004c: Expected O, but got I
		isFull = false;
		quality = 0f;
		temperatureScore = 0f;
		isInitialised = false;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rax_v2+B8]");
		object obj2 = 0;
		coffeeLabel = (string)obj2;
		if (OnCupEmptied != null)
		{
			OnCupEmptied.Invoke();
		}
	}

	private void HandlePickedUp()
	{
		if (isFull)
		{
			hasBeenPickedUp = true;
			if (OnCupPickedUp != null)
			{
				OnCupPickedUp.Invoke();
			}
		}
	}

	public EspressoCup()
	{
		UnityEvent onCupPickedUp = new UnityEvent();
		OnCupPickedUp = onCupPickedUp;
		OnResultInitialised = new UnityEvent<float>();
		OnCupFilled = new UnityEvent();
		OnCupEmptied = new UnityEvent();
		base._002Ector();
	}
}
