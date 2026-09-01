using UnityEngine;

public class ArcReactor_EmitterDestructor : MonoBehaviour
{
	public ParticleSystem partSystem;

	public bool onlyDisable;

	private void Update()
	{
		if (!partSystem.IsAlive())
		{
			if (onlyDisable)
			{
				base.gameObject.SetActive(false);
				base.enabled = false;
			}
			else
			{
				Object.Destroy(base.gameObject);
			}
		}
	}
}
