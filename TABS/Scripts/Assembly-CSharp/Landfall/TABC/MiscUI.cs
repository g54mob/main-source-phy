using System;
using TMPro;
using UnityEngine;

namespace Landfall.TABC
{
	public class MiscUI : MonoBehaviour
	{
		public CodeAnimation unitCountAnim;

		public TextMeshProUGUI unitCountText;

		public CodeAnimation lineAnim;

		public Color whiteColor;

		public Color redColor;

		private int unitsLastUpdate;

		private void Start()
		{
			RoundHandler instance = RoundHandler.instance;
			instance.NewStateAction = (Action<RoundHandler.RoundState>)Delegate.Combine(instance.NewStateAction, new Action<RoundHandler.RoundState>(NewState));
		}

		public void NewState(RoundHandler.RoundState state)
		{
			if (state == RoundHandler.RoundState.Battle)
			{
				lineAnim.PlayOut();
			}
			else if (state != RoundHandler.RoundState.Battle)
			{
				lineAnim.PlayIn();
			}
		}

		private void Update()
		{
			if (GameFlowHandlerServer.isDebug)
			{
				return;
			}
			int level = XPHandlerClient.instance.level;
			int num = BoardManager.instance.UnitsOnTheBoard();
			if (num != level)
			{
				unitCountText.text = num + " / " + level;
				if (level > num)
				{
					unitCountText.color = whiteColor;
					if (unitCountAnim.currentState == CodeAnimationUse.Out)
					{
						unitCountAnim.PlayIn();
					}
				}
				else
				{
					unitCountText.color = redColor;
					if (unitCountAnim.currentState == CodeAnimationUse.Out)
					{
						unitCountAnim.PlayIn();
					}
					else if (unitsLastUpdate != num)
					{
						unitCountAnim.PlayBoop();
					}
				}
			}
			else if (unitCountAnim.currentState != CodeAnimationUse.Out)
			{
				unitCountAnim.PlayOut();
			}
			unitsLastUpdate = num;
		}
	}
}
