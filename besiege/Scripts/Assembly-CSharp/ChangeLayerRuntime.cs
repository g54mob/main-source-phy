using UnityEngine;

public class ChangeLayerRuntime : MonoBehaviour
{
	public int _TargetLayer;

	public float _TimeBeforeSwitch = 1f;

	private float _Timer;

	private void Start()
	{
	}

	private void Update()
	{
		if (_Timer < _TimeBeforeSwitch || base.gameObject.layer == _TargetLayer)
		{
			return;
		}
		base.gameObject.layer = _TargetLayer;
		foreach (Transform item in base.gameObject.transform)
		{
			item.gameObject.layer = _TargetLayer;
		}
	}
}
