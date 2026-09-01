using UnityEngine;

namespace Landfall.TABC
{
	public class AllianceHandlerUI : MonoBehaviour
	{
		public static AllianceHandlerUI instance;

		public Populate populate;

		private AllianceButton[] buttons;

		public void Populate(AllianceProgress[] aliances)
		{
			populate.times = aliances.Length;
			if (buttons != null)
			{
				for (int i = 0; i < buttons.Length; i++)
				{
					Object.Destroy(buttons[i].gameObject);
				}
			}
			buttons = populate.DoPopulate<AllianceButton>().ToArray();
			for (int j = 0; j < buttons.Length; j++)
			{
				buttons[j].Init(aliances[j].alliance);
				buttons[j].UpdateAlliance(aliances[j].unlockedUnits);
			}
		}

		private void Awake()
		{
			instance = this;
		}
	}
}
