using System;
using UnityEngine;

namespace ModIO.UI
{
	[Obsolete("No longer supported.")]
	[RequireComponent(typeof(UserView))]
	[RequireComponent(typeof(SlideToggle))]
	public class UserLoginSlideToggle : MonoBehaviour
	{
		private UserView view => base.gameObject.GetComponent<UserView>();

		private SlideToggle slider => base.gameObject.GetComponent<SlideToggle>();

		public void OnUserClicked()
		{
			if (!slider.isAnimating)
			{
				if (view.profile.id != -1)
				{
					slider.isOn = true;
				}
				else
				{
					view.NotifyClicked();
				}
			}
		}

		public void OnLogoutClicked()
		{
			if (!slider.isAnimating)
			{
				view.NotifyClicked();
				slider.isOn = false;
			}
		}
	}
}
