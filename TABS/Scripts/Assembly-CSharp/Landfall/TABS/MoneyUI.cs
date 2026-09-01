using TMPro;
using UnityEngine;

namespace Landfall.TABS
{
	public class MoneyUI : MonoBehaviour
	{
		public TextMeshProUGUI moneyText;

		public TextMeshProUGUI unitCountText;

		private SimpleStateAnimation moneyAnimation;

		private void Awake()
		{
			moneyAnimation = GetComponent<SimpleStateAnimation>();
		}

		public void SetMoney(int money)
		{
			moneyText.text = money.ToString();
		}

		public void SetCount(int count, int? maxCount)
		{
			unitCountText.text = ((!maxCount.HasValue) ? count.ToString() : $"{count}/{maxCount.Value}");
		}

		public void Show()
		{
			if (moneyAnimation != null)
			{
				moneyAnimation.SetState(SimpleStateAnimation.State.State01);
			}
		}

		public void Hide()
		{
			if (moneyAnimation != null)
			{
				moneyAnimation.SetState(SimpleStateAnimation.State.State02);
			}
		}
	}
}
