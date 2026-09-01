using UnityEngine;

namespace Landfall.TABC
{
	public class BoardManagerUI : MonoBehaviour
	{
		public Populate populate;

		public static BoardManagerUI instance;

		public UnitButton currentUnitButton;

		public UnitButton[] buttons;

		private void Awake()
		{
			instance = this;
		}

		private void Start()
		{
			buttons = populate.DoPopulate<UnitButton>().ToArray();
			for (int i = 0; i < buttons.Length; i++)
			{
				buttons[i].SetUnit(null, isOWned: true);
			}
		}

		private void Update()
		{
		}

		public void EnterUnitButton(UnitButton unitButton)
		{
			currentUnitButton = unitButton;
		}

		public void ExitUnitButton()
		{
			currentUnitButton = null;
		}
	}
}
