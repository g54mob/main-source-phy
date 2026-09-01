using UnityEngine;
using UnityEngine.UI;

namespace TFBGames
{
	[RequireComponent(typeof(Image))]
	public class HighlightColorChange : MonoBehaviour
	{
		[SerializeField]
		protected Color enabledColor = Color.white;

		[SerializeField]
		protected Color disabledColor = Color.white;

		private Button button;

		private Image image;

		private void Awake()
		{
			image = GetComponent<Image>();
		}

		public void SetInteractable(bool isInteractable)
		{
			if (isInteractable)
			{
				SetEnabled();
			}
			else
			{
				SetDisabled();
			}
		}

		private void SetEnabled()
		{
			if (image != null)
			{
				image.color = enabledColor;
			}
		}

		private void SetDisabled()
		{
			if (image != null)
			{
				image.color = disabledColor;
			}
		}
	}
}
