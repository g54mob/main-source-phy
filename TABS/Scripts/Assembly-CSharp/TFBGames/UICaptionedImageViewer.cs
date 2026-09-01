using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TFBGames
{
	public class UICaptionedImageViewer : MonoBehaviour
	{
		[SerializeField]
		private CaptionedImage[] spritesToShow;

		[SerializeField]
		private Image imageContainer;

		[SerializeField]
		private TMP_Text captionText;

		private int index = -1;

		private void Start()
		{
			index = 0;
			ChangeDisplay(index);
		}

		public void Open()
		{
			base.gameObject.SetActive(value: true);
			if (spritesToShow != null && spritesToShow.Length != 0)
			{
				if (index > spritesToShow.Length)
				{
					index = 0;
				}
				ChangeDisplay(index);
			}
		}

		public void Close()
		{
			base.gameObject.SetActive(value: false);
		}

		public void NextImage()
		{
			index++;
			index %= spritesToShow.Length;
			ChangeDisplay(index);
		}

		private void ChangeDisplay(int i)
		{
			if (i < spritesToShow.Length)
			{
				CaptionedImage captionedImage = spritesToShow[i];
				if (captionedImage != null)
				{
					imageContainer.sprite = captionedImage.sprite;
					captionText.text = captionedImage.caption;
				}
			}
		}
	}
}
