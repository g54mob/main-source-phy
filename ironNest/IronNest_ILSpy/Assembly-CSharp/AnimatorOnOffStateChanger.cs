using Cpp2ILInjected;
using UnityEngine;

public class AnimatorOnOffStateChanger : MonoBehaviour
{
	private Animator _animator;

	private string _onStateName;

	private string _offStateName;

	private float _crossfadeDuration;

	private bool _setValueOnStart;

	private bool _valueOnStart;

	private bool _isOn;

	private void Start()
	{
		if (_setValueOnStart)
		{
			bool isOn = !_valueOnStart;
			_isOn = isOn;
			SetState(_valueOnStart);
		}
	}

	public void SetState(bool isOn)
	{
		//IL_0050: Expected O, but got I4
		//IL_0068: Invalid comparison between I4 and F4
		//IL_00bb: Expected F4, but got I4
		//IL_013b: Invalid comparison between I4 and F4
		//IL_010b: Expected O, but got I
		if (isOn == _isOn)
		{
			return;
		}
		_isOn = isOn;
		AnimatorStateInfo currentAnimatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
		object obj = (isOn ? 1 : 0) ^ 1;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D4F790");
		float num = default(float);
		float num2;
		if (!(0f > num))
		{
			bool flag = !(num > 1f);
			num2 = num;
			if (!flag)
			{
				num2 = 1f;
			}
		}
		else
		{
			num2 = 0f;
		}
		float num3 = 1f - num2;
		float num4 = num3 - _crossfadeDuration;
		if (0f > num4 || num4 > 1f)
		{
		}
		Animator animator = _animator;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (AnimatorOnOffStateChanger)+28+v126 @ rax_v8*8]");
		float normalizedTimeOffset = default(float);
		animator.CrossFade((string)0, _crossfadeDuration, 0, normalizedTimeOffset);
	}

	public void ToggleState()
	{
		bool state = !_isOn;
		SetState(state);
	}

	public void ForceState(bool isOn)
	{
		bool isOn2 = (byte)((isOn ? 1u : 0u) ^ 1u) != 0;
		_isOn = isOn2;
		SetState(isOn);
	}

	private void Reset()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		Animator animator = default(Animator);
		_animator = animator;
	}
}
