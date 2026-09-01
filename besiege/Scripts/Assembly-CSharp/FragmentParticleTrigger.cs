using System;
using UnityEngine;

public class FragmentParticleTrigger : MonoBehaviour
{
	public Fragment Fragment;

	private bool hasActivated;

	public event Action OnActivateParticles;

	private void OnCollisionEnter(Collision collision)
	{
		if (hasActivated || (collision.rigidbody != null && collision.rigidbody.CompareTag("PreventSurfaceFracture")))
		{
			return;
		}
		BuildSurface.SurfaceMaterialType currentType = Fragment.OriginalSurface.currentType;
		if (currentType.breakImpactSettings == BuildSurface.BreakImpactSettings.Disabled)
		{
			return;
		}
		if (currentType.breakImpactSettings == BuildSurface.BreakImpactSettings.ProjectilesOnly)
		{
			if (collision.rigidbody == null)
			{
				return;
			}
			BasicInfo component = collision.rigidbody.GetComponent<BasicInfo>();
			if (component == null || component.infoType != BasicInfo.BasicInfoType.Projectile)
			{
				return;
			}
		}
		Rigidbody rigidbody = collision.rigidbody;
		float magnitude = collision.relativeVelocity.magnitude;
		float num = ((!(rigidbody != null)) ? (magnitude / Time.fixedDeltaTime) : (rigidbody.mass * magnitude / Time.fixedDeltaTime));
		if (num > currentType.breakImpactThreshold)
		{
			TriggerParticles();
		}
	}

	public void TriggerParticles()
	{
		if (!hasActivated)
		{
			hasActivated = true;
			if (this.OnActivateParticles != null)
			{
				this.OnActivateParticles();
			}
		}
	}
}
