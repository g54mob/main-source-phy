using UnityEngine;

public class ForcePoint : MonoBehaviour
{
	public void InvertLocalPosition()
	{
		base.transform.localPosition *= -1f;
	}
}
