using System;
using UnityEngine.Events;

[Serializable]
public class ChargeInstance
{
	public bool isOn = true;

	public UnityEvent turnOnEvent;

	public UnityEvent turnOffEvent;
}
