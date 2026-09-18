using UnityEngine;
using UnityEngine.UI;

namespace Synty.Interface.FantasyMenus.Samples
{
	public class SamplePopup : MonoBehaviour
	{
		[Header("References")]
		public SamplePopup acceptObject;

		public SamplePopup cancelObject;

		public Selectable inputBlocker;

		public Animator animator;

		[Header("Parameters")]
		public bool selfDismiss;

		public float dismissTime;

		private bool dismissed = true;

		private void Update()
		{
			if (dismissed)
			{
				return;
			}
			if (Input.GetKeyDown(KeyCode.Return))
			{
				if (acceptObject != null)
				{
					acceptObject.ShowMe();
				}
				DismissMe();
			}
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				if (cancelObject != null)
				{
					cancelObject.ShowMe();
				}
				DismissMe();
			}
		}

		public void ShowMe()
		{
			CancelInvoke();
			dismissed = false;
			if (inputBlocker != null)
			{
				inputBlocker.gameObject.SetActive(value: true);
				inputBlocker.Select();
			}
			base.gameObject.SetActive(value: false);
			base.gameObject.SetActive(value: true);
			animator.SetBool("Active", value: true);
			if (selfDismiss)
			{
				Invoke("DisableMe", dismissTime);
			}
		}

		public void DismissMe()
		{
			animator.SetBool("Active", value: false);
			Invoke("DisableMe", dismissTime);
			if (inputBlocker != null)
			{
				inputBlocker.gameObject.SetActive(value: false);
			}
			dismissed = true;
		}

		public void DisableMe()
		{
			base.gameObject.SetActive(value: false);
		}
	}
}
