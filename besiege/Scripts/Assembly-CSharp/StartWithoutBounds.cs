using System.Collections;
using UnityEngine;

public class StartWithoutBounds : MonoBehaviour
{
	private IEnumerator Start()
	{
		yield return null;
		DisableBoundsButton.SetBounds(false);
	}
}
