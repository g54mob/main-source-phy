using Landfall.TABS;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileDodgeMove : ProjectileSurfaceEffect
{
	public DodgeMove dodge;

	public UnityEvent unityEvent;

	private DataHandler data;

	public override bool DoEffect(HitData hit, GameObject projectile)
	{
		if (!data)
		{
			Unit component = dodge.transform.root.GetComponent<Unit>();
			if ((bool)component)
			{
				data = component.data;
			}
		}
		if (!data)
		{
			return true;
		}
		TeamHolder component2 = projectile.GetComponent<TeamHolder>();
		if ((bool)component2 && (bool)data && data.unit.Team == component2.team)
		{
			return true;
		}
		dodge.targetObject = projectile.transform;
		unityEvent.Invoke();
		return true;
	}
}
