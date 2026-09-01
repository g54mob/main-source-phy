using UnityEngine;
using UnityEngine.Events;

namespace Zagreekie.Tools;

public sealed class ArmedFireRelayOneShot : MonoBehaviour
{
	private UnityEvent _fireLeft;

	private UnityEvent _fireRight;

	private UnityEvent _leftArmedEvent;

	private UnityEvent _leftDisarmedEvent;

	private UnityEvent _rightArmedEvent;

	private UnityEvent _rightDisarmedEvent;

	private UnityEvent _anyArmedEvent;

	private UnityEvent _allDisarmedEvent;

	private bool _leftArmed;

	private bool _rightArmed;

	private bool _disarmBeforeInvoke;

	private bool _clearOnEnable;

	private void OnEnable()
	{
		//IL_01d8: Expected O, but got I4
		if (!_clearOnEnable)
		{
			return;
		}
		if (_leftArmed)
		{
			_leftArmed = false;
			_leftDisarmedEvent.Invoke();
			bool flag = _leftArmed;
			bool flag2 = true;
			if (!flag)
			{
				flag2 = _rightArmed;
			}
			if (!flag2)
			{
				_allDisarmedEvent.Invoke();
			}
		}
		bool flag3 = _leftArmed;
		bool flag4 = true;
		if (!flag3)
		{
			flag4 = _rightArmed;
		}
		if (_rightArmed)
		{
			_rightArmed = false;
			_rightDisarmedEvent.Invoke();
			bool flag5 = _leftArmed;
			bool flag6 = true;
			if (!flag5)
			{
				flag6 = _rightArmed;
			}
			bool flag7 = !flag4;
			object obj = flag6 & flag7;
			if (obj != null)
			{
				_anyArmedEvent.Invoke();
			}
			if (flag4 && !flag6)
			{
				_allDisarmedEvent.Invoke();
			}
		}
	}

	public void ArmLeft()
	{
		//IL_0112: Expected O, but got I4
		bool flag;
		if (!_leftArmed)
		{
			flag = _rightArmed;
		}
		else
		{
			bool flag2 = _leftArmed;
			flag = true;
			if (flag2)
			{
				return;
			}
		}
		_leftArmed = true;
		_leftArmedEvent.Invoke();
		bool flag3 = _leftArmed;
		bool flag4 = true;
		if (!flag3)
		{
			flag4 = _rightArmed;
		}
		bool flag5 = !flag;
		object obj = flag4 & flag5;
		if (obj != null)
		{
			_anyArmedEvent.Invoke();
		}
		if (flag && !flag4)
		{
			_allDisarmedEvent.Invoke();
		}
	}

	public void ArmRight()
	{
		bool flag = _leftArmed;
		bool wasAnyArmed = true;
		if (!flag)
		{
			wasAnyArmed = _rightArmed;
		}
		if (!_rightArmed)
		{
			_rightArmed = true;
			_rightArmedEvent.Invoke();
			EmitAggregateStateChangeEvents(wasAnyArmed);
		}
	}

	public void ArmBoth()
	{
		//IL_01e8: Expected O, but got I4
		//IL_0240: Expected O, but got I4
		bool flag;
		if (!_leftArmed)
		{
			flag = _rightArmed;
		}
		else
		{
			bool flag2 = _leftArmed;
			flag = true;
			if (flag2)
			{
				goto IL_00f5;
			}
		}
		_leftArmed = true;
		_leftArmedEvent.Invoke();
		bool flag3 = _leftArmed;
		bool flag4 = true;
		if (!flag3)
		{
			flag4 = _rightArmed;
		}
		bool flag5 = !flag;
		object obj = flag4 & flag5;
		if (obj != null)
		{
			_anyArmedEvent.Invoke();
		}
		if (flag && !flag4)
		{
			_allDisarmedEvent.Invoke();
		}
		goto IL_00f5;
		IL_00f5:
		bool flag6 = _leftArmed;
		bool flag7 = true;
		if (!flag6)
		{
			flag7 = _rightArmed;
		}
		if (!_rightArmed)
		{
			_rightArmed = true;
			_rightArmedEvent.Invoke();
			bool flag8 = _leftArmed;
			bool flag9 = true;
			if (!flag8)
			{
				flag9 = _rightArmed;
			}
			bool flag10 = !flag7;
			object obj2 = flag9 & flag10;
			if (obj2 != null)
			{
				_anyArmedEvent.Invoke();
			}
			if (flag7 && !flag9)
			{
				_allDisarmedEvent.Invoke();
			}
		}
	}

	public void DisarmLeft()
	{
		if (_leftArmed)
		{
			_leftArmed = false;
			_leftDisarmedEvent.Invoke();
			bool flag = _leftArmed;
			bool flag2 = true;
			if (!flag)
			{
				flag2 = _rightArmed;
			}
			if (!flag2)
			{
				_allDisarmedEvent.Invoke();
			}
		}
	}

	public void DisarmRight()
	{
		bool flag = _leftArmed;
		bool wasAnyArmed = true;
		if (!flag)
		{
			wasAnyArmed = _rightArmed;
		}
		if (_rightArmed)
		{
			_rightArmed = false;
			_rightDisarmedEvent.Invoke();
			EmitAggregateStateChangeEvents(wasAnyArmed);
		}
	}

	public void DisarmAll()
	{
		//IL_01b9: Expected O, but got I4
		if (_leftArmed)
		{
			_leftArmed = false;
			_leftDisarmedEvent.Invoke();
			bool flag = _leftArmed;
			bool flag2 = true;
			if (!flag)
			{
				flag2 = _rightArmed;
			}
			if (!flag2)
			{
				_allDisarmedEvent.Invoke();
			}
		}
		bool flag3 = _leftArmed;
		bool flag4 = true;
		if (!flag3)
		{
			flag4 = _rightArmed;
		}
		if (_rightArmed)
		{
			_rightArmed = false;
			_rightDisarmedEvent.Invoke();
			bool flag5 = _leftArmed;
			bool flag6 = true;
			if (!flag5)
			{
				flag6 = _rightArmed;
			}
			bool flag7 = !flag4;
			object obj = flag6 & flag7;
			if (obj != null)
			{
				_anyArmedEvent.Invoke();
			}
			if (flag4 && !flag6)
			{
				_allDisarmedEvent.Invoke();
			}
		}
	}

	public void ToggleLeft()
	{
		bool flag = !_leftArmed;
		bool wasAnyArmed = true;
		if (!_leftArmed)
		{
			wasAnyArmed = _rightArmed;
		}
		if (_leftArmed != flag)
		{
			_leftArmed = flag;
			UnityEvent unityEvent = ((~(_leftArmed ? 1u : 0u) != 0) ? _leftArmedEvent : _leftDisarmedEvent);
			unityEvent.Invoke();
			EmitAggregateStateChangeEvents(wasAnyArmed);
		}
	}

	public void ToggleRight()
	{
		bool flag = !_rightArmed;
		bool flag2 = _leftArmed;
		bool wasAnyArmed = true;
		if (!flag2)
		{
			wasAnyArmed = _rightArmed;
		}
		if (_rightArmed != flag)
		{
			_rightArmed = flag;
			UnityEvent unityEvent = ((~(_rightArmed ? 1u : 0u) != 0) ? _rightArmedEvent : _rightDisarmedEvent);
			unityEvent.Invoke();
			EmitAggregateStateChangeEvents(wasAnyArmed);
		}
	}

	public void TriggerFire()
	{
		//IL_0432: Expected O, but got I4
		//IL_048f: Expected O, but got I4
		if (!_leftArmed && ~(_rightArmed ? 1u : 0u) != 0)
		{
			return;
		}
		if (_disarmBeforeInvoke)
		{
			if (~(_leftArmed ? 1u : 0u) == 0)
			{
				_leftArmed = false;
				_leftDisarmedEvent.Invoke();
				bool flag = _leftArmed;
				bool flag2 = true;
				if (!flag)
				{
					flag2 = _rightArmed;
				}
				if (!flag2)
				{
					_allDisarmedEvent.Invoke();
				}
			}
			if (~(_rightArmed ? 1u : 0u) == 0)
			{
				bool flag3 = _leftArmed;
				bool flag4 = true;
				if (!flag3)
				{
					flag4 = _rightArmed;
				}
				if (_rightArmed)
				{
					_rightArmed = false;
					_rightDisarmedEvent.Invoke();
					bool flag5 = _leftArmed;
					bool flag6 = true;
					if (!flag5)
					{
						flag6 = _rightArmed;
					}
					bool flag7 = !flag4;
					object obj = flag6 & flag7;
					if (obj != null)
					{
						_anyArmedEvent.Invoke();
					}
					if (flag4 && !flag6)
					{
						_allDisarmedEvent.Invoke();
					}
				}
			}
		}
		if (~(_leftArmed ? 1u : 0u) == 0)
		{
			_fireLeft.Invoke();
		}
		if (~(_rightArmed ? 1u : 0u) == 0)
		{
			_fireRight.Invoke();
		}
		if (_disarmBeforeInvoke)
		{
			return;
		}
		if (~(_leftArmed ? 1u : 0u) == 0 && _leftArmed)
		{
			_leftArmed = false;
			_leftDisarmedEvent.Invoke();
			bool flag8 = _leftArmed;
			bool flag9 = true;
			if (!flag8)
			{
				flag9 = _rightArmed;
			}
			if (!flag9)
			{
				_allDisarmedEvent.Invoke();
			}
		}
		if (~(_rightArmed ? 1u : 0u) != 0)
		{
			return;
		}
		bool flag10 = _leftArmed;
		bool flag11 = true;
		if (!flag10)
		{
			flag11 = _rightArmed;
		}
		if (_rightArmed)
		{
			_rightArmed = false;
			_rightDisarmedEvent.Invoke();
			bool flag12 = _leftArmed;
			bool flag13 = true;
			if (!flag12)
			{
				flag13 = _rightArmed;
			}
			bool flag14 = !flag11;
			object obj2 = flag13 & flag14;
			if (obj2 != null)
			{
				_anyArmedEvent.Invoke();
			}
			if (flag11 && !flag13)
			{
				_allDisarmedEvent.Invoke();
			}
		}
	}

	public bool IsLeftArmed()
	{
		return _leftArmed;
	}

	public bool IsRightArmed()
	{
		return _rightArmed;
	}

	public bool IsAnyArmed()
	{
		bool flag = _leftArmed;
		bool result = true;
		if (!flag)
		{
			result = _rightArmed;
		}
		return result;
	}

	private void SetLeftArmed(bool armed)
	{
		bool flag = _leftArmed;
		bool wasAnyArmed = true;
		if (!flag)
		{
			wasAnyArmed = _rightArmed;
		}
		if (_leftArmed != armed)
		{
			_leftArmed = armed;
			UnityEvent unityEvent = (armed ? _leftArmedEvent : _leftDisarmedEvent);
			unityEvent.Invoke();
			EmitAggregateStateChangeEvents(wasAnyArmed);
		}
	}

	private void SetRightArmed(bool armed)
	{
		bool flag = _leftArmed;
		bool wasAnyArmed = true;
		if (!flag)
		{
			wasAnyArmed = _rightArmed;
		}
		if (_rightArmed != armed)
		{
			_rightArmed = armed;
			UnityEvent unityEvent = (armed ? _rightArmedEvent : _rightDisarmedEvent);
			unityEvent.Invoke();
			EmitAggregateStateChangeEvents(wasAnyArmed);
		}
	}

	private void EmitAggregateStateChangeEvents(bool wasAnyArmed)
	{
		//IL_0090: Expected O, but got I4
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Expected O, but got Unknown
		bool flag = _leftArmed;
		bool flag2 = true;
		if (!flag)
		{
			flag2 = _rightArmed;
		}
		object obj = (wasAnyArmed ? 1 : 0) ^ 1;
		object obj2 = flag2 & obj;
		if (obj2 != null)
		{
			_anyArmedEvent.Invoke();
		}
		if (wasAnyArmed && !flag2)
		{
			_allDisarmedEvent.Invoke();
		}
	}

	public ArmedFireRelayOneShot()
	{
		UnityEvent fireLeft = new UnityEvent();
		_fireLeft = fireLeft;
		_fireRight = new UnityEvent();
		_leftArmedEvent = new UnityEvent();
		_leftDisarmedEvent = new UnityEvent();
		_rightArmedEvent = new UnityEvent();
		_rightDisarmedEvent = new UnityEvent();
		_anyArmedEvent = new UnityEvent();
		_allDisarmedEvent = new UnityEvent();
		_disarmBeforeInvoke = true;
		base._002Ector();
	}
}
