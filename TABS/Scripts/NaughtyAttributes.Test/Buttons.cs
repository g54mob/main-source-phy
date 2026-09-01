using NaughtyAttributes;
using UnityEngine;

public class Buttons : MonoBehaviour
{
	[Button(null)]
	public void MethodOne()
	{
		Debug.Log("MethodOne()");
	}

	[Button("Button Text")]
	private void MethodTwo()
	{
		Debug.Log("MethodTwo()");
	}
}
