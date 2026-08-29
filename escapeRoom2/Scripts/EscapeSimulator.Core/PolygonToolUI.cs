using UnityEngine;
using UnityEngine.UI;

public class PolygonToolUI : PineUIComponent
{
	public Canvas root;

	public Button SaveChanges;

	public Text SaveChanges_Text;

	public Button AddHole;

	public Text AddHole_Text;

	public Button GenerateWalls;

	public Text GenerateWalls_Text;

	public Button GenerateCeiling;

	public Text GenerateCeiling_Text;

	public Button DiscardChanges;

	public Text DiscardChanges_Text;

	public object data;

	private bool isInitialized;

	protected override void Awake()
	{
		init();
	}

	public void init()
	{
		if (!isInitialized)
		{
			isInitialized = true;
			PineUI.addButtonListeners(SaveChanges);
			PineUI.addButtonListeners(AddHole);
			PineUI.addButtonListeners(GenerateWalls);
			PineUI.addButtonListeners(GenerateCeiling);
			PineUI.addButtonListeners(DiscardChanges);
		}
	}
}
