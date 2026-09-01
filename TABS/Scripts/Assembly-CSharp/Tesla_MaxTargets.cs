using UnityEngine;

public class Tesla_MaxTargets : MonoBehaviour
{
	public int maxAllowedTargets = 1;

	private int targetsHit;

	public bool destoryWhenDone = true;

	public bool CheckIfAllowedToHit()
	{
		if (targetsHit < maxAllowedTargets)
		{
			targetsHit++;
			return true;
		}
		if (destoryWhenDone)
		{
			base.gameObject.AddComponent<RemoveAfterSeconds>().seconds = 3f;
		}
		return false;
	}
}
