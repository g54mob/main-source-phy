using UnityEngine;

public class CustomShapeVert : MonoBehaviour
{
	public SpriteRenderer m_SpriteRenderer;

	public void SetColor(Color color)
	{
		m_SpriteRenderer.color = color;
	}

	public void InverseScale(Vector3 scale)
	{
		base.transform.localScale = new Vector3(1f / scale.x, 1f / scale.y, 1f / scale.z);
	}
}
