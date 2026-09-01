using System.Collections.Generic;
using InControl;
using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DMInvokeCyclic : MonoBehaviour
{
	[SerializeField]
	private string cycleLeftAction;

	[SerializeField]
	private string cycleRightAction;

	[SerializeField]
	private CanvasGroup menuCanvasGroup;

	private PlayerAction leftAction;

	private PlayerAction rightAction;

	public Selectable[] m_selectables;

	private List<ISubmitHandler> submitHandlers = new List<ISubmitHandler>();

	private int cycleIndex;

	private void Start()
	{
		leftAction = PlayerActions.Instance.GetPlayerActionByName(cycleLeftAction);
		rightAction = PlayerActions.Instance.GetPlayerActionByName(cycleRightAction);
		FetchSelectables();
	}

	public void FetchSelectables()
	{
		cycleIndex = 0;
		submitHandlers.Clear();
		Selectable[] selectables = m_selectables;
		foreach (Selectable selectable in selectables)
		{
			if (selectable != null)
			{
				ISubmitHandler component = selectable.GetComponent<ISubmitHandler>();
				if (component != null)
				{
					submitHandlers.Add(component);
				}
			}
		}
	}

	public void SetIndex(int index)
	{
		cycleIndex = index;
	}

	private void Update()
	{
		if (leftAction.WasPressed)
		{
			Cycle(-1);
		}
		else if (rightAction.WasPressed)
		{
			Cycle(1);
		}
	}

	private void Cycle(int direction)
	{
		if (m_selectables != null && m_selectables.Length != 0 && (!(menuCanvasGroup != null) || menuCanvasGroup.interactable))
		{
			cycleIndex = Mod(cycleIndex + direction, m_selectables.Length);
			submitHandlers[cycleIndex]?.OnSubmit(null);
		}
	}

	private int Mod(int x, int m)
	{
		return (x % m + m) % m;
	}
}
