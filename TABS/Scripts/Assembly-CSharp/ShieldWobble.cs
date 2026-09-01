using UnityEngine;

public class ShieldWobble : MonoBehaviour
{
	public float DurationLerpFactor = 4f;

	private Renderer shield;

	private MaterialPropertyBlock propertyBlock01;

	private float distortionAmount;

	private Vector3 point;

	private void Start()
	{
		shield = GetComponent<Renderer>();
		propertyBlock01 = new MaterialPropertyBlock();
	}

	public void OnHit(Vector3 p)
	{
		Debug.Log("On hit effect! Setting wobble!");
		distortionAmount = 1f;
		point = p;
	}

	private void Update()
	{
		distortionAmount = Mathf.Lerp(distortionAmount, 0f, Time.deltaTime * DurationLerpFactor);
		shield.GetPropertyBlock(propertyBlock01);
		propertyBlock01.SetVector("_VertexWobblePos", new Vector4(point.x, point.y, point.z, distortionAmount));
		shield.SetPropertyBlock(propertyBlock01);
	}
}
