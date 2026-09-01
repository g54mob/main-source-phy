using System;
using UnityEngine;

namespace ModIO.UI
{
	[RequireComponent(typeof(UserView))]
	[Obsolete("No longer supported.")]
	[RequireComponent(typeof(SlideToggle))]
	public class UserLoginSlideToggle : MonoBehaviour
	{
		private UserView view
		{
			get
			{
				return base.gameObject.GetComponent<UserView>();
			}
		}

		private SlideToggle slider
		{
			get
			{
				return base.gameObject.GetComponent<SlideToggle>();
			}
		}

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
