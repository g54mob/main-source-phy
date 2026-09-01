using UnityEngine;
using UnityEngine.UI;

namespace LevelCreator
{
	public class CurrentToolDataViewer : MonoBehaviour
	{
		public Image imageInitial;

		public Sprite defaultSeedSpriteInitial;

		public Sprite defaultBrushSpriteInitial;

		private Image image;

		private Sprite defaultSeedSprite;

		private Sprite defaultBrushSprite;

		private static CurrentToolDataViewer currentToolDataViewer;

		private Sprite seedSprite;

		private Sprite brushSprite;

		public static Sprite SeedSprite
		{
			get
			{
				return currentToolDataViewer.seedSprite;
			}
			set
			{
				currentToolDataViewer.seedSprite = value;
				UpdateImage(currentToolDataViewer.seedSprite);
			}
		}

		public static Sprite BrushSprite
		{
			get
			{
				return currentToolDataViewer.brushSprite;
			}
			set
			{
				currentToolDataViewer.brushSprite = value;
				UpdateImage(currentToolDataViewer.brushSprite);
			}
		}

		public static void UpdateImage(Sprite sprite)
		{
			if (sprite == null)
			{
				currentToolDataViewer.gameObject.SetActive(value: false);
				return;
			}
			currentToolDataViewer.gameObject.SetActive(value: true);
			currentToolDataViewer.image.sprite = sprite;
		}

		private void Awake()
		{
			image = imageInitial;
			defaultSeedSprite = defaultSeedSpriteInitial;
			defaultBrushSprite = defaultBrushSpriteInitial;
			currentToolDataViewer = this;
			SeedSprite = defaultSeedSprite;
			BrushSprite = defaultBrushSprite;
			UpdateImage(null);
		}
	}
}
