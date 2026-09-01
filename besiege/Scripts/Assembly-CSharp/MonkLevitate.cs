using System.Collections;
using UnityEngine;

public class MonkLevitate : MonoBehaviour
{
	public float levitatePower = 10f;

	public float levitateDuration;

	public Transform windZone;

	private IEnumerator Start()
	{
		yield return new WaitForSeconds(1f);
		if (StatMaster.levelSimulating)
		{
			Levitate();
		}
	}

	private void Levitate()
	{
	}
}
