using UnityEngine;

public class ShootPosition : MonoBehaviour
{
	public bool useAltShootPos;

	public bool useAltShootRot;

	private AlternatingShootPosition[] altShootPos;

	private int currentAltShootPos;

	private void Start()
	{
		if (useAltShootPos)
		{
			altShootPos = base.transform.root.GetComponentsInChildren<AlternatingShootPosition>();
		}
	}

	public void Shoot()
	{
		if (altShootPos == null || altShootPos.Length == 0)
		{
			return;
		}
		if (useAltShootRot)
		{
			base.transform.rotation = altShootPos[currentAltShootPos].transform.rotation;
		}
		if (useAltShootPos)
		{
			base.transform.position = altShootPos[currentAltShootPos].transform.position;
			altShootPos[currentAltShootPos].Shoot();
			currentAltShootPos++;
			if (currentAltShootPos >= altShootPos.Length)
			{
				currentAltShootPos = 0;
			}
		}
	}
}
