using UnityEngine;

public class PlayUnitBowAnimation : MonoBehaviour
{
	private UnitBowAnimation bowAnim;

	public bool callEvents = true;

	private void Start()
	{
		bowAnim = base.transform.root.GetComponentInChildren<UnitBowAnimation>();
	}

	public void CallDrawAnim()
	{
		if ((bool)bowAnim)
		{
			bowAnim.GoToHand(callEvents);
		}
	}

	public void CallShootAnim()
	{
		if ((bool)bowAnim)
		{
			bowAnim.GoToStartPos();
		}
	}
}
