using NaughtyAttributes;
using UnityEngine;

public class OnValueChanged : MonoBehaviour
{
	[OnValueChanged("OnValueChangedMethod")]
	public int onValueChanged;

	private void OnValueChangedMethod()
	{
		Debug.Log(onValueChanged);
	}
}
