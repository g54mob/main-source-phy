using Landfall.TABS_Input;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LevelCreator
{
	public class ColorByDistance : MonoBehaviour
	{
		public TextMeshProUGUI textTMP;

		public DMEditorColors.ColorState nearColor;

		public DMEditorColors.ColorState farColor;

		public float distance;

		public Vector3 positionOffset;

		private Color nearCol;

		private Color farCol;

		private void Start()
		{
			nearCol = DMEditorColors.GetColor(nearColor);
			farCol = DMEditorColors.GetColor(farColor);
		}

		private void Update()
		{
			float num = 0f;
			if (PlayerActions.Instance.InputType == InputType.Keyboard)
			{
				num = Vector2.Distance(base.transform.position + positionOffset, Input.mousePosition);
			}
			else if (PlayerActions.Instance.InputType == InputType.Controller && EventSystem.current.currentSelectedGameObject != null)
			{
				num = Vector2.Distance(base.transform.position + positionOffset, EventSystem.current.currentSelectedGameObject.transform.position);
			}
			num *= distance * 0.01f;
			textTMP.color = Color.Lerp(nearCol, farCol, num);
		}
	}
}
