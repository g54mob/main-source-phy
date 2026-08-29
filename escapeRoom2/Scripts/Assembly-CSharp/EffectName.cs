using UnityEngine;
using UnityEngine.UI;

public class EffectName : MonoBehaviour
{
	public Text nameLable;

	private void Update()
	{
		Vector3 position = Camera.main.WorldToScreenPoint(base.transform.position);
		nameLable.transform.position = position;
	}
}
