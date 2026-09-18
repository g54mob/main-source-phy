using UnityEngine;
using UnityEngine.UI;

namespace Synty.Interface.FantasyMenus.Samples
{
	public class SampleToggleAnimatorEvents : MonoBehaviour
	{
		private Toggle toggle;

		private Animator animator;

		private void Awake()
		{
			toggle = GetComponent<Toggle>();
			animator = GetComponent<Animator>();
			if (toggle != null)
			{
				toggle.onValueChanged.AddListener(OnValueChanged);
				OnValueChanged(toggle.isOn);
			}
		}

		private void OnEnable()
		{
			if (toggle != null)
			{
				OnValueChanged(toggle.isOn);
			}
		}

		private void OnDestroy()
		{
			if (toggle != null)
			{
				toggle.onValueChanged.RemoveListener(OnValueChanged);
			}
		}

		private void OnValueChanged(bool value)
		{
			if (animator != null)
			{
				animator.SetBool("ToggleIsOn", value);
			}
		}
	}
}
