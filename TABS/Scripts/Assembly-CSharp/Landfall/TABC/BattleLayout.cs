using System;

namespace Landfall.TABC
{
	[Serializable]
	public class BattleLayout
	{
		public BattleLayoutUnit[] units;

		public BattleLayout BoardToLayout(Board board)
		{
			BattleLayout battleLayout = new BattleLayout();
			battleLayout.units = new BattleLayoutUnit[board.Units.Count];
			for (int i = 0; i < board.Units.Count; i++)
			{
				battleLayout.units[i] = new BattleLayoutUnit();
				battleLayout.units[i].unit = board.Units[i].unitDataInstance.unit.unitBlueprint;
				battleLayout.units[i].pos = board.Units[i].pos;
			}
			return battleLayout;
		}
	}
}
