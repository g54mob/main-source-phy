using Landfall.TABS.GameState;
using UnityEngine;

public class CanDoUnitEvents : MonoBehaviour
{
	[HideInInspector]
	public DataHandler data;

	private GameStateManager man;

	public bool canDoStuff;

	private void Start()
	{
		data = base.transform.root.GetComponentInChildren<DataHandler>();
		man = ServiceLocator.GetService<GameStateManager>();
	}

	private void Update()
	{
		canDoStuff = false;
		if ((bool)data && man.GameState == GameState.BattleState && !data.input.hasControl && !data.Dead)
		{
			canDoStuff = true;
		}
	}
}
