using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Synty.Interface.FantasyMenus.Samples
{
	public class SampleToggleHelper : MonoBehaviour
	{
		private Toggle toggle;

		private Animator animator;

		public void Awake()
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

		private void LateUpdate()
		{
			bool flag = EventSystem.current.currentSelectedGameObject == toggle.gameObject;
			if (flag != toggle.isOn)
			{
				SetToggle(flag);
			}
		}

		public void SetToggle(bool value)
		{
			if (toggle != null)
			{
				toggle.isOn = value;
			}
		}

		public void Toggle()
		{
			if (toggle != null)
			{
				SetToggle(!toggle.isOn);
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
