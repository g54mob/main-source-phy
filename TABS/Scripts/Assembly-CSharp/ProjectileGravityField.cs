using System.Collections.Generic;
using UnityEngine;

public class ProjectileGravityField : ProjectileSurfaceEffect
{
	private List<GameObject> projectiles;

	private List<float> projectileGravities;

	private List<float> projectileDrags;

	public float dragToAdd = 3f;

	private float addedDrag;

	public float rotAmount = 150f;

	private void Start()
	{
		projectiles = new List<GameObject>();
		projectileGravities = new List<float>();
		projectileDrags = new List<float>();
	}

	public override bool DoEffect(HitData hit, GameObject projectile)
	{
		if (!projectiles.Contains(projectile))
		{
			MoveTransform component = projectile.GetComponent<MoveTransform>();
			RemoveAfterSeconds component2 = projectile.GetComponent<RemoveAfterSeconds>();
			addedDrag = Random.Range(dragToAdd * 0.5f, dragToAdd * 1.5f);
			projectiles.Add(projectile);
			projectileGravities.Add(component.gravity);
			projectileDrags.Add(addedDrag);
			component.gravity = 0f;
			component.velocity *= 0.5f;
			component.drag += addedDrag;
			component.rotationFollowVelocity = false;
			component.useYDrag = true;
			if (component2 != null)
			{
				component2.seconds = 5f;
			}
			ProjectileRotate componentInChildren = projectile.transform.GetComponentInChildren<ProjectileRotate>();
			if ((bool)componentInChildren)
			{
				componentInChildren.rotation *= 0.3f;
			}
			else
			{
				componentInChildren = projectile.transform.GetChild(0).gameObject.AddComponent<ProjectileRotate>();
				Vector3 rotation = new Vector3(Random.Range(0f - rotAmount, rotAmount), Random.Range(0f - rotAmount, rotAmount), Random.Range(0f - rotAmount, rotAmount));
				componentInChildren.rotation = rotation;
				componentInChildren.self = false;
			}
		}
		return true;
	}

	public void ResetProjectiles()
	{
		for (int i = 0; i < projectiles.Count; i++)
		{
			if (projectiles[i] != null)
			{
				MoveTransform component = projectiles[i].GetComponent<MoveTransform>();
				component.gravity = Random.Range(projectileGravities[i] * 0.8f, projectileGravities[i] * 1.2f);
				component.useYDrag = false;
				component.rotationFollowVelocity = true;
				component.drag -= projectileDrags[i];
			}
		}
	}
}
