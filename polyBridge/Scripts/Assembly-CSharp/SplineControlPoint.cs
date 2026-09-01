using UnityEngine;

public class SplineControlPoint : MonoBehaviour
{
	public SpriteRenderer m_SpriteRenderer;

	public void Select()
	{
		m_SpriteRenderer.color = Color.yellow;
		base.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
	}

	public void DeSelect()
	{
		m_SpriteRenderer.color = Color.white;
		base.transform.localScale = Vector3.one;
	}
}
