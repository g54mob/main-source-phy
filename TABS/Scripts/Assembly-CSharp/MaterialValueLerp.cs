using UnityEngine;

public class MaterialValueLerp : MonoBehaviour
{
	public Material mat;

	public string propertyName;

	public float speed;

	public float startDelay;

	private float lerpValue;

	private float delayTimer;

	private void Awake()
	{
		if (mat == null)
		{
			int num = GetComponents<MaterialValueLerp>().Length;
			mat = GetComponent<Renderer>().materials[num - 1];
		}
	}

	private void Update()
	{
		delayTimer += Time.deltaTime;
		if (delayTimer >= startDelay)
		{
			mat.SetFloat(propertyName, lerpValue);
			lerpValue += Time.deltaTime * speed;
			if (lerpValue > 1f)
			{
				Object.Destroy(this);
			}
		}
	}
}
