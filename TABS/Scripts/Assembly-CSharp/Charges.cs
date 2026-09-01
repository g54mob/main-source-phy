using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Charges : MonoBehaviour
{
	public UnityEvent failUse;

	public UnityEvent succUse;

	public ChargeInstance[] charges;

	public float rechargeTime;

	public bool ChargeEnabled()
	{
		for (int i = 0; i < charges.Length; i++)
		{
			if (charges[i].isOn)
			{
				return true;
			}
		}
		return false;
	}

	public bool UseCharge()
	{
		for (int i = 0; i < charges.Length; i++)
		{
			if (charges[i].isOn)
			{
				DisableTop();
				return true;
			}
		}
		return false;
	}

	private void EnableTop()
	{
		for (int num = charges.Length - 1; num >= 0; num--)
		{
			if (!charges[num].isOn)
			{
				charges[num].turnOnEvent.Invoke();
				charges[num].isOn = true;
				break;
			}
		}
	}

	private void DisableTop()
	{
		for (int i = 0; i < charges.Length; i++)
		{
			if (charges[i].isOn)
			{
				charges[i].turnOffEvent.Invoke();
				charges[i].isOn = false;
				StartCoroutine(CountRecharge());
				break;
			}
		}
	}

	private IEnumerator CountRecharge()
	{
		yield return new WaitForSeconds(rechargeTime);
		EnableTop();
	}
}
