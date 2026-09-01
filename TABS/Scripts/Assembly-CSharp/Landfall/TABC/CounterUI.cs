using TMPro;
using UnityEngine;

namespace Landfall.TABC
{
	public class CounterUI : MonoBehaviour
	{
		public TextMeshProUGUI counterText;

		public TextMeshProUGUI descriptionText;

		public void Update()
		{
			if (TimeCounter.instance.isCounting)
			{
				counterText.text = Mathf.Ceil(Mathf.Clamp(TimeCounter.instance.timeLeft, 0.01f, float.PositiveInfinity)).ToString("F0");
			}
			else
			{
				counterText.text = "X";
			}
			if (RoundHandler.instance.roundState == RoundHandler.RoundState.Planning)
			{
				descriptionText.text = "PLANNING";
			}
			if (RoundHandler.instance.roundState == RoundHandler.RoundState.Battle)
			{
				descriptionText.text = "BATTLING";
			}
			if (RoundHandler.instance.roundState == RoundHandler.RoundState.WaitingForOtherBattles)
			{
				descriptionText.text = "WAITING FOR OTHERS";
			}
			if (RoundHandler.instance.roundState == RoundHandler.RoundState.PostRound)
			{
				descriptionText.text = "GET MONEY IN";
			}
			if (RoundHandler.instance.roundState == RoundHandler.RoundState.PickingChallange)
			{
				descriptionText.text = "PICKING CHALLANGE";
			}
		}
	}
}
