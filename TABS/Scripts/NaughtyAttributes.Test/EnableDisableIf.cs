using NaughtyAttributes;
using UnityEngine;

public class EnableDisableIf : MonoBehaviour
{
	public bool enable1;

	public bool enable2;

	public bool disable1;

	public bool disable2;

	[EnableIf(ConditionOperator.And, new string[] { "enable1", "enable2" })]
	public int enableIfAll = 1;

	[EnableIf(ConditionOperator.Or, new string[] { "enable1", "enable2" })]
	public int enableIfAny = 2;

	[DisableIf(ConditionOperator.And, new string[] { "disable1", "disable2" })]
	public int disableIfAll = 1;

	[DisableIf(ConditionOperator.Or, new string[] { "disable1", "disable2" })]
	public int disableIfAny = 2;
}
