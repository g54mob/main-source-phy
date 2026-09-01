using UnityEngine;

public class FadeObjectAlphaWithCamDistance : MonoBehaviour
{
	public Renderer rend;

	public float distance = 3f;

	private MouseOrbit camscript;

	private void Start()
	{
		camscript = SingleInstanceFindOnly<MouseOrbit>.Instance;
	}

	private void LateUpdate()
	{
		float magnitude = (camscript.transform.position - base.transform.position).magnitude;
		float a = Mathf.Clamp(magnitude / distance, 0f, 1f);
		Color color = rend.material.GetColor("_TintColor");
		rend.material.SetColor("_TintColor", new Color(color.r, color.g, color.b, a));
	}
}
