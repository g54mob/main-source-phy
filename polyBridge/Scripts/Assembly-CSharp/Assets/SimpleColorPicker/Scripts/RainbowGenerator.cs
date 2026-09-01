using UnityEngine;
using UnityEngine.UI;

namespace Assets.SimpleColorPicker.Scripts
{
	public class RainbowGenerator : MonoBehaviour
	{
		public void Start()
		{
			Texture2D texture2D = new Texture2D(1, 128);
			for (int i = 0; i < texture2D.height; i++)
			{
				texture2D.SetPixel(0, i, Color.HSVToRGB((float)i / (float)(texture2D.height - 1), 1f, 1f));
			}
			texture2D.Apply();
			GetComponent<Image>().sprite = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), 100f);
		}
	}
}
