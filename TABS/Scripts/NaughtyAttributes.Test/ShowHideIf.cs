using NaughtyAttributes;
using UnityEngine;

public class ShowHideIf : MonoBehaviour
{
	public bool show1;

	public bool show2;

	public bool hide1;

	public bool hide2;

	[ShowIf("False")]
	public int showIf;

	[ShowIf(ConditionOperator.And, new string[] { "show1", "show2" })]
	public int showIfAll = 1;

	[ShowIf(ConditionOperator.Or, new string[] { "show1", "show2" })]
	public int showIfAny = 2;

	[HideIf("True")]
	public int hideIf;

	[HideIf(ConditionOperator.And, new string[] { "hide1", "hide2" })]
	public int hideIfAll = 1;

	[HideIf(ConditionOperator.Or, new string[] { "hide1", "hide2" })]
	public int hideIfAny = 2;

	private bool True()
	{
		return true;
	}

	private bool False()
	{
		return false;
	}
}
