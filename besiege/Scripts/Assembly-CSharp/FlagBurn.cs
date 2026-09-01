using UnityEngine;

public class FlagBurn : MonoBehaviour
{
	public FireController fireController;

	public MeshRenderer renderer;

	public Material originalMaterial;

	public Material BurnMaterial;

	private float dissolveStep;

	private bool materialWasSwitched;

	private float fireDuration;

	private float currentLerpTime;

	private void Start()
	{
		renderer = GetComponent<MeshRenderer>();
		renderer.material = originalMaterial;
		BurnMaterial.SetFloat("_Progress", 1f);
		fireDuration = fireController.lateBurnDuration;
		currentLerpTime = 0f;
	}

	private void Update()
	{
		if (fireController.onFire)
		{
			if (!materialWasSwitched)
			{
				renderer.material = BurnMaterial;
				materialWasSwitched = true;
			}
			currentLerpTime += Time.deltaTime;
			if (currentLerpTime > fireDuration)
			{
				currentLerpTime = fireDuration;
			}
			dissolveStep = currentLerpTime / fireDuration;
			renderer.material.SetFloat("_Progress", Mathf.Lerp(1f, 0f, dissolveStep));
		}
	}
}
