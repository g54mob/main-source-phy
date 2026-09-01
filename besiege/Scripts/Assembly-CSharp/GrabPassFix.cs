using System.Collections;
using UnityEngine;

public class GrabPassFix : MonoBehaviour
{
	private IEnumerator Start()
	{
		yield return null;
		base.gameObject.SetActive(false);
	}
}
