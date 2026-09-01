using System;
using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	public class MessageDisplay : MonoBehaviour
	{
		[Header("UI Components")]
		public Text content;

		public event Action<MessageDisplay> onClick;

		public void NotifyClicked()
		{
			if (this.onClick != null)
			{
				this.onClick(this);
			}
		}
	}
}
