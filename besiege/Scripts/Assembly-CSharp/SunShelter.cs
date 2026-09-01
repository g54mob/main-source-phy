using UnityEngine;

public class SunShelter : MonoBehaviour
{
	public BurningSun sunScript;

	private void OnTriggerEnter(Collider other)
	{
		if ((bool)other.attachedRigidbody.GetComponent<FireTag>())
		{
			FireTag component = other.attachedRigidbody.GetComponent<FireTag>();
			if (sunScript.fireTagsList.Contains(component))
			{
				sunScript.fireTagsList.Remove(component);
			}
		}
		if (sunScript.fireTagsList.Count == 0)
		{
			sunScript.timer = sunScript.storedTimer;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if ((bool)other.attachedRigidbody.GetComponent<FireTag>())
		{
			FireTag component = other.attachedRigidbody.GetComponent<FireTag>();
			if (!sunScript.fireTagsList.Contains(component))
			{
				sunScript.fireTagsList.Add(component);
			}
		}
	}
}
