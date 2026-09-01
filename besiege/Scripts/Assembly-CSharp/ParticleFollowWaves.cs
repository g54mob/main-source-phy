using UnityEngine;

[AddComponentMenu("Water/Objects/Follow Waves")]
public class ParticleFollowWaves : MonoBehaviour
{
	public float repeatRate = 0.0333333f;

	private Transform t;

	private float timer;

	private void Awake()
	{
		t = base.transform;
	}

	private void LateUpdate()
	{
		if (timer > repeatRate)
		{
			float y = WaterController.CheckHeightMap(t.position.x, t.position.z);
			Set(y);
			timer = 0f;
		}
		else
		{
			timer += Time.deltaTime;
		}
	}

	private void Set(float y)
	{
		Vector3 position = t.position;
		position.y = y;
		y = t.parent.InverseTransformPoint(position).y;
		position = t.localPosition;
		position.y = y;
		t.localPosition = position;
	}
}
