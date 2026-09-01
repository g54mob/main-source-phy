using System;
using Localisation;
using UnityEngine;
using UnityEngine.UI;

namespace Interface.QuickSelect
{
	public class QuickSelectMenu : MonoBehaviour
	{
		public enum MenuType
		{
			Generic = 0,
			BuildingBlocks = 1
		}

		public Action<MenuType, int> onSelect;

		[SerializeField]
		protected QuickMenu[] menus;

		[SerializeField]
		protected Image selector;

		[SerializeField]
		protected Text titleText;

		[SerializeField]
		protected float height = 38f;

		[SerializeField]
		protected float iconSize = 15f;

		protected float halfSelectorAngle;

		private Image[] menuSprites;

		private QuickMenu currentMenu;

		private void Start()
		{
			Toggle(false);
		}

		private void Clear()
		{
			if (menuSprites != null)
			{
				for (int i = 0; i < menuSprites.Length; i++)
				{
					UnityEngine.Object.Destroy(menuSprites[i].gameObject);
				}
			}
		}

		public void Open(MenuType menu)
		{
			currentMenu = menus[(int)menu];
			Open(currentMenu);
		}

		public void Open(QuickMenu menu)
		{
			Clear();
			QuickMenu.MenuOption[] options = menu.Options;
			int num = options.Length;
			float num2 = (float)Math.PI * 2f / (float)num;
			float num3 = 0f;
			float num4 = 1f / (float)num;
			selector.fillAmount = num4;
			halfSelectorAngle = num4 * 180f;
			menuSprites = new Image[num];
			for (int i = 0; i < options.Length; i++)
			{
				QuickMenu.MenuOption menuOption = options[i];
				GameObject gameObject = new GameObject("option" + i, typeof(Image));
				RectTransform rectTransform = gameObject.transform as RectTransform;
				rectTransform.SetParent(base.transform, false);
				rectTransform.sizeDelta = new Vector2(iconSize, iconSize);
				rectTransform.anchoredPosition = new Vector2(Mathf.Sin(num3), Mathf.Cos(num3)) * height;
				(menuSprites[i] = gameObject.GetComponent<Image>()).sprite = menuOption.Icon;
				num3 += num2;
			}
		}

		protected void Update()
		{
			RectTransform rectTransform = selector.rectTransform;
			float axis = Input.GetAxis("SelectHorizontal");
			float axis2 = Input.GetAxis("SelectVertical");
			bool flag = axis != 0f || axis2 != 0f;
			selector.enabled = flag;
			for (int i = 0; i < menuSprites.Length; i++)
			{
				menuSprites[i].rectTransform.localScale = Vector3.one;
			}
			bool buttonUp = Input.GetButtonUp("SelectToggle");
			if (flag)
			{
				float num = Angle(axis, axis2);
				int num2 = Mathf.RoundToInt(num / 360f * (float)currentMenu.Options.Length);
				if (num2 == currentMenu.Options.Length)
				{
					num2 = 0;
				}
				titleText.text = LocalisationManager.GetTranslation(currentMenu.Options[num2].LocalisationId);
				menuSprites[num2].rectTransform.localScale = Vector3.one * 1.1f;
				rectTransform.localEulerAngles = new Vector3(0f, 0f, 0f - num + halfSelectorAngle);
				if (buttonUp)
				{
					currentMenu.OnSelect(num2);
				}
			}
			if (buttonUp)
			{
				Toggle(false);
			}
		}

		private float Angle(float x, float y)
		{
			if (x < 0f)
			{
				return 360f - Mathf.Atan2(x, y) * 57.29578f * -1f;
			}
			return Mathf.Atan2(x, y) * 57.29578f;
		}

		public void Toggle(bool toggle)
		{
			base.gameObject.SetActive(toggle);
		}
	}
}
