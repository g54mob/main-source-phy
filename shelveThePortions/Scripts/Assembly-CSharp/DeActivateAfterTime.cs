using System;
using UnityEngine;

public class DeActivateAfterTime : MonoBehaviour
{
	[SerializeField]
	private PrefabTypes prefabType;

	[SerializeField]
	public float duration = 3f;

	public Action OnDisableAction;

	private void OnEnable()
	{
		Invoke("DeActivate", duration);
	}

	public void SetDuration(float newDuration)
	{
		CancelInvoke();
		Invoke("DeActivate", newDuration);
	}

	public void DeActivate()
	{
		CancelInvoke();
		if (prefabType == PrefabTypes.None)
		{
			base.gameObject.SetActive(value: false);
		}
		else
		{
			MasterPool.ReturnToPoolTransform(base.gameObject, prefabType);
		}
		if (OnDisableAction != null)
		{
			OnDisableAction();
		}
	}
}
