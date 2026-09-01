using System.Collections;
using UnityEngine;

public class BarrierVanish : MonoBehaviour
{
	private void Start()
	{
		StartCoroutine(disabler());
	}

	private IEnumerator disabler()
	{
		yield return new WaitForSeconds(2f);
		base.gameObject.SetActive(false);
	}
}
