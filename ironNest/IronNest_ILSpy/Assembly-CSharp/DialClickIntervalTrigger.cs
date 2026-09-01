using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class DialClickIntervalTrigger : MonoBehaviour
{
	private DialInteractable dial;

	private bool useClickInterval = true;

	private float clickIntervalAmount = 360f;

	private bool onlyTrackInUnlimitedMode = true;

	public UnityEvent<int> OnClickInterval;

	private int totalTriggeredClicks;

	private bool _subscribed;

	private float _lastObservedAccumulated;

	private bool _hasLastObserved;

	private float _cumulativeTravel;

	public int TotalTriggeredClicks => totalTriggeredClicks;

	private void Awake()
	{
		if (dial == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			DialInteractable dialInteractable = default(DialInteractable);
			dial = dialInteractable;
		}
		if (dial == null)
		{
			Debug.LogError("[DialClickIntervalTrigger] No DialInteractable assigned or found on this GameObject.", this);
		}
	}

	private void OnEnable()
	{
		if (!_subscribed && dial != null)
		{
			DialInteractable dialInteractable = dial;
			UnityAction<float> call = HandleDialValueChanged;
			dialInteractable.OnValueChanged.AddListener(call);
			_subscribed = true;
		}
		_cumulativeTravel = 0f;
		_hasLastObserved = false;
		_lastObservedAccumulated = 0f;
	}

	private void OnDisable()
	{
		if (_subscribed && dial != null)
		{
			DialInteractable dialInteractable = dial;
			UnityAction<float> call = HandleDialValueChanged;
			dialInteractable.OnValueChanged.RemoveListener(call);
			_subscribed = false;
		}
		_cumulativeTravel = 0f;
		_hasLastObserved = false;
		_lastObservedAccumulated = 0f;
	}

	private void Update()
	{
		bool flag = dial == null;
		if (flag || useClickInterval == flag)
		{
			return;
		}
		if (onlyTrackInUnlimitedMode != flag)
		{
			DialInteractable dialInteractable = dial;
			if (dialInteractable.dialMode != DialInteractable.DialMode.Unlimited)
			{
				return;
			}
		}
		DialInteractable dialInteractable2 = dial;
		if (_hasLastObserved)
		{
			ProcessAccumulatedChange(dialInteractable2.accumulatedValue);
			return;
		}
		_lastObservedAccumulated = dialInteractable2.accumulatedValue;
		_hasLastObserved = true;
	}

	private void TrySubscribe()
	{
		if (!_subscribed && dial != null)
		{
			DialInteractable dialInteractable = dial;
			UnityAction<float> call = HandleDialValueChanged;
			dialInteractable.OnValueChanged.AddListener(call);
			_subscribed = true;
		}
	}

	private void Unsubscribe()
	{
		if (_subscribed && dial != null)
		{
			DialInteractable dialInteractable = dial;
			UnityAction<float> call = HandleDialValueChanged;
			dialInteractable.OnValueChanged.RemoveListener(call);
			_subscribed = false;
		}
	}

	private void HandleDialValueChanged(float newValue)
	{
		if (!useClickInterval)
		{
			return;
		}
		bool flag = dial == null;
		if (flag)
		{
			return;
		}
		if (onlyTrackInUnlimitedMode != flag)
		{
			DialInteractable dialInteractable = dial;
			if (dialInteractable.dialMode != DialInteractable.DialMode.Unlimited)
			{
				return;
			}
		}
		ProcessAccumulatedChange(newValue);
	}

	private unsafe void ProcessAccumulatedChange(float newValue)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		//IL_0071: Invalid comparison between F4 and I4
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Expected I4, but got Unknown
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Expected O, but got Unknown
		if (_hasLastObserved)
		{
			float num = newValue - _lastObservedAccumulated;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj = num & 0;
			if ((nint)obj > 0)
			{
				float num2 = (_cumulativeTravel = (float)obj + _cumulativeTravel);
				if (clickIntervalAmount > 0f)
				{
					float num3 = num2 / clickIntervalAmount;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803DB600");
					object obj2 = default(object);
					if ((nint)obj2 > 0)
					{
						int num4 = totalTriggeredClicks + obj2;
						totalTriggeredClicks = num4;
						if (OnClickInterval != null)
						{
							object obj3 = default(object);
							OnClickInterval.Invoke((int)(&obj3));
						}
						object obj4 = obj2 * clickIntervalAmount;
						float cumulativeTravel = _cumulativeTravel - (float)obj4;
						_cumulativeTravel = cumulativeTravel;
					}
					_lastObservedAccumulated = newValue;
					return;
				}
			}
		}
		else
		{
			_hasLastObserved = true;
		}
		_lastObservedAccumulated = newValue;
	}

	private void ResetCounters(bool keepTotal)
	{
		_cumulativeTravel = 0f;
		_hasLastObserved = false;
		_lastObservedAccumulated = 0f;
		if (!keepTotal)
		{
			totalTriggeredClicks = 0;
		}
	}
}
