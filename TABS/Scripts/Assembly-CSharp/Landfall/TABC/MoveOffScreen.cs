using UnityEngine;

namespace Landfall.TABC
{
	public class MoveOffScreen : MonoBehaviour
	{
		private void Awake()
		{
			base.transform.GetComponent<RectTransform>().position -= GetComponent<CodeAnimation>().animations[0].animDirection * GetComponent<CodeAnimation>().animations[0].multiplier * GetComponentInParent<Canvas>().transform.localScale.x;
		}
	}
}
