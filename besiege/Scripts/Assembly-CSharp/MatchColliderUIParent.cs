using System.Collections;
using UnityEngine;

[AddComponentMenu("UI/Match Collider With UI parent")]
[ExecuteInEditMode]
public class MatchColliderUIParent : MonoBehaviour
{
	public Vector3 offset = Vector2.zero;

	public void OnEnable()
	{
		StartCoroutine(Enable());
	}

	private IEnumerator Enable()
	{
		yield return null;
		if (base.transform.parent is RectTransform)
		{
			RectTransform t = base.transform.parent as RectTransform;
			base.transform.localScale = new Vector3(t.rect.width + offset.x, t.rect.height + offset.y, base.transform.localScale.z);
		}
	}
}
