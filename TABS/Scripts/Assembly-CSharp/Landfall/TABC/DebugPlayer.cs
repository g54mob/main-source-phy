using UnityEngine;

namespace Landfall.TABC
{
	public class DebugPlayer : MonoBehaviour
	{
		private void Start()
		{
		}

		private void Update()
		{
			if ((double)Random.value < 0.01 && RoundHandler.instance.roundState == RoundHandler.RoundState.Battle)
			{
				Object.FindObjectOfType<GameFlowHandlerServer>().ClientBattleOver(0);
			}
		}
	}
}
