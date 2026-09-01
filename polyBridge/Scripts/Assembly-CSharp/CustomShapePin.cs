using UnityEngine;

public class CustomShapePin : MonoBehaviour
{
	public SpriteRenderer m_SpriteRenderer;

	public MeshRenderer m_MeshRenderer;

	public void SetColor(Color color)
	{
		m_SpriteRenderer.color = color;
	}

	public void InverseScale(Vector3 scale)
	{
		base.transform.localScale = new Vector3(1f / scale.x, 1f / scale.y, 1f / scale.z);
	}

	public void ShowMesh(bool show)
	{
		m_MeshRenderer.gameObject.SetActive(show);
		m_SpriteRenderer.gameObject.SetActive(!show);
	}

	public void ShowSprite(bool show)
	{
		m_SpriteRenderer.gameObject.SetActive(show);
	}
}
