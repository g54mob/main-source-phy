using System.Collections.Generic;
using Landfall.TABS;
using UnityEngine;

public class AimForRandomTarget : MonoBehaviour
{
	public LayerMask mainRigMask;

	public float radius = 5f;

	private void Start()
	{
		TeamHolder component = GetComponent<TeamHolder>();
		if (!component || !component.target)
		{
			return;
		}
		Collider[] array = Physics.OverlapSphere(component.target.position, radius, mainRigMask);
		List<Rigidbody> list = new List<Rigidbody>();
		for (int i = 0; i < array.Length; i++)
		{
			if (!array[i])
			{
				continue;
			}
			Rigidbody attachedRigidbody = array[i].attachedRigidbody;
			if ((bool)attachedRigidbody && attachedRigidbody != component.target)
			{
				Unit component2 = attachedRigidbody.transform.root.GetComponent<Unit>();
				if (component.team != component2.Team && !component2.dead)
				{
					list.Add(attachedRigidbody);
				}
			}
		}
		if (Random.value > 0.4f && list.Count > 0)
		{
			ReAim(list[Random.Range(0, list.Count - 1)]);
		}
	}

	private void ReAim(Rigidbody newTarget)
	{
		if ((bool)newTarget)
		{
			Compensation component = GetComponent<Compensation>();
			MoveTransform component2 = GetComponent<MoveTransform>();
			if ((bool)component2 && (bool)component)
			{
				base.transform.rotation = Quaternion.LookRotation(component.GetCompensation(newTarget.position, newTarget.velocity, 0f));
				component2.Initialize();
			}
		}
	}
}
