using UnityEngine;

public class AnimatorOnOffStateChanger : MonoBehaviour
{
	[SerializeField]
	private Animator _animator;

	[SerializeField]
	private string _onStateName;

	[SerializeField]
	private string _offStateName;

	[SerializeField]
	[Range(0f, 1f)]
	private float _crossfadeDuration;

	[SerializeField]
	private bool _setValueOnStart;

	[SerializeField]
	private bool _valueOnStart;

	private bool _isOn;

	private void Start()
	{
	}

	public void SetState(bool isOn)
	{
	}

	public void ToggleState()
	{
	}

	public void ForceState(bool isOn)
	{
	}

	private void Reset()
	{
	}
}
