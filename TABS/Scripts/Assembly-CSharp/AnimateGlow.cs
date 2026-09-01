using UnityEngine;

public class AnimateGlow : MonoBehaviour
{
	private Renderer m_renderer;

	private MaterialPropertyBlock m_propertyBlock;

	[SerializeField]
	private Gradient m_baseGradient;

	[SerializeField]
	private float m_colorAnimationSpeed = 1f;

	[SerializeField]
	private float m_intensityMin;

	[SerializeField]
	private float m_intensityMax = 1f;

	[SerializeField]
	private float m_glowAnimationSpeed = 1f;

	private void Start()
	{
		m_propertyBlock = new MaterialPropertyBlock();
		m_renderer = GetComponent<Renderer>();
	}

	private void Update()
	{
		Color color = m_baseGradient.Evaluate(Mathf.PingPong(Time.time * m_colorAnimationSpeed, 1f));
		float num = (Mathf.Sin(Time.time * m_glowAnimationSpeed) + 1f) * (m_intensityMax - m_intensityMin) * 0.5f + m_intensityMin;
		Color value = color * num * num;
		m_propertyBlock.SetColor("_EmissionColor", value);
		m_renderer.SetPropertyBlock(m_propertyBlock);
	}
}
