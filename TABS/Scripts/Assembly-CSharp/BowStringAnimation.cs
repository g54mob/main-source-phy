using UnityEngine;

public class BowStringAnimation : MonoBehaviour
{
	public float pullBackLength = 0.5f;

	public PositionShake stringObject;

	private Vector3 defaultPos;

	private void Start()
	{
		if ((bool)stringObject)
		{
			defaultPos = stringObject.transform.localPosition;
		}
	}

	public void ChargeUp(float chargeUp)
	{
		if ((bool)stringObject)
		{
			stringObject.startLocal = defaultPos + Vector3.back * chargeUp * pullBackLength;
		}
	}
}
