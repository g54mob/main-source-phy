using UnityEngine;

public class BaloonExplosion : MonoBehaviour
{
	[Header("Dissolve")]
	public float baloonDissolveTime = 0.8f;

	private float baloonDissolveStep = 0.1f;

	private float currentBaloonDissolveLerp;

	[Header("Scaling")]
	public float baloonScaleTime = 0.8f;

	private float baloonScaleStep = 0.1f;

	private float currentBaloonScaleLerp;

	public float baloonScaleMultiplier = 1.5f;

	private MeshRenderer baloonRenderer;

	private Vector3 targetScale;

	private Vector3 initialScale;

	private void Start()
	{
		baloonRenderer = GetComponent<MeshRenderer>();
		targetScale = new Vector3(base.transform.localScale.x * baloonScaleMultiplier, base.transform.localScale.y * baloonScaleMultiplier, base.transform.localScale.z * baloonScaleMultiplier);
		initialScale = base.transform.localScale;
	}

	private void Update()
	{
		if (baloonRenderer.material.GetFloat("_Progress") > 0f)
		{
			currentBaloonDissolveLerp += Time.deltaTime;
			currentBaloonScaleLerp += Time.deltaTime;
			if (currentBaloonDissolveLerp > baloonDissolveTime)
			{
				currentBaloonDissolveLerp = baloonDissolveTime;
			}
			if (currentBaloonScaleLerp > baloonScaleTime)
			{
				currentBaloonScaleLerp = baloonScaleTime;
			}
			baloonDissolveStep = currentBaloonDissolveLerp / baloonDissolveTime;
			baloonScaleStep = currentBaloonScaleLerp / baloonScaleTime;
			baloonRenderer.material.SetFloat("_Progress", Mathf.Lerp(1f, 0f, baloonDissolveStep));
			base.transform.localScale = Vector3.Lerp(initialScale, targetScale, baloonScaleStep);
		}
	}
}
