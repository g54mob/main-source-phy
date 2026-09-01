using System.Collections;
using UnityEngine;

namespace ModIO.UI
{
	[RequireComponent(typeof(RectTransform))]
	public class MoveOnEnable : MonoBehaviour
	{
		public Vector2 anchoredPosition = Vector2.zero;

		public bool lateMove;

		private void OnEnable()
		{
			StartCoroutine(DoMove());
		}

		private IEnumerator DoMove()
		{
			if (lateMove)
			{
				yield return null;
			}
			GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
		}
	}
}
