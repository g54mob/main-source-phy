using UnityEngine;

public class LightmapInterpolatorTextureSwapper : MonoBehaviour
{
	public Material m_material;

	private Texture m_texture01;

	private Texture m_texture02;

	private void Start()
	{
		m_texture01 = null;
		m_texture02 = null;
	}

	public void SetTextures(Texture texture01, Texture texture02)
	{
		if (m_texture01 != texture01)
		{
			m_material.SetTexture("_DarkTex", texture01);
			m_texture01 = texture01;
		}
		if (m_texture02 != texture02)
		{
			m_material.SetTexture("_LightTex", texture02);
			m_texture02 = texture02;
		}
	}
}
