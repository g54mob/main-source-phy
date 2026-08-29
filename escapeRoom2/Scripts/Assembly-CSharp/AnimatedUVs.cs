using UnityEngine;

public class AnimatedUVs : MonoBehaviour
{
	public float speedx;

	public float speedY = 0.5f;

	public bool useSineX;

	public bool useSineY;

	private float offsety;

	private float offsetx;

	private Renderer rend;

	private void Start()
	{
		rend = GetComponent<Renderer>();
	}

	private void Update()
	{
		float num = speedY;
		float num2 = speedx;
		if (useSineX)
		{
			num2 = UnityUtils.map(-1f, 1f, num2, num2 * -1f, Mathf.Sin(Time.time));
		}
		if (useSineY)
		{
			num = UnityUtils.map(-1f, 1f, num, num * -1f, Mathf.Sin(Time.time));
		}
		offsety += Time.deltaTime * num;
		offsetx += Time.deltaTime * num2;
		rend.material.SetTextureOffset("_MainTex", new Vector2(offsetx, offsety));
	}
}
