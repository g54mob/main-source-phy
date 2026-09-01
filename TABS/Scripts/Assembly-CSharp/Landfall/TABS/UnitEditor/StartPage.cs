using Landfall.TABS.Workshop;
using UnityEngine;

namespace Landfall.TABS.UnitEditor
{
	public class StartPage : UIComponentMainMenu
	{
		public GameObject quickLoadCell;

		private GameObject[] quickLoadUnits;

		protected override void Start()
		{
			base.Start();
			Setup();
		}

		public void Setup()
		{
			if (quickLoadUnits != null)
			{
				for (int i = 0; i < quickLoadUnits.Length; i++)
				{
					Object.Destroy(quickLoadUnits[i]);
				}
			}
			UnitBlueprint[] lastLoadedUnits = CustomUnitHandler.GetLastLoadedUnits();
			quickLoadUnits = new GameObject[lastLoadedUnits.Length];
			Debug.Log(lastLoadedUnits);
			for (int j = 0; j < lastLoadedUnits.Length; j++)
			{
				GameObject gameObject = Object.Instantiate(quickLoadCell, quickLoadCell.transform.parent);
				gameObject.SetActive(value: true);
				gameObject.GetComponent<QuickLoadCell>().Setup(lastLoadedUnits[j]);
				quickLoadUnits[j] = gameObject;
			}
		}
	}
}
