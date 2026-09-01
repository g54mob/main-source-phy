using System;
using UnityEngine;

[AddComponentMenu("Water/Objects/Water Dependent Display")]
public class WaterDependentDisplay : MonoBehaviour
{
	private void Awake()
	{
		WaterFogController.UnderwaterToggled = (Action<bool>)Delegate.Combine(WaterFogController.UnderwaterToggled, new Action<bool>(ToggleDisplay));
	}

	private void OnDestroy()
	{
		WaterFogController.UnderwaterToggled = (Action<bool>)Delegate.Remove(WaterFogController.UnderwaterToggled, new Action<bool>(ToggleDisplay));
	}

	private void ToggleDisplay(bool underwater)
	{
		float waterTransformHeight = WaterController.waterTransformHeight;
		bool flag = base.transform.position.y < waterTransformHeight;
		base.gameObject.SetActive(flag == underwater);
	}
}
