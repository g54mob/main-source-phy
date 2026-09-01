using UnityEngine;

[AddComponentMenu("Water/Objects/Wave Splash Detector")]
public class WaveSplashDetector : MonoBehaviour
{
	public float repeatRate = 0.5f;

	private Transform t;

	public Rigidbody body;

	public Transform parent;

	public ParticleSystem[] particles;

	private bool wasUnder = true;

	private float timer;

	private bool hasBody;

	private void Awake()
	{
		t = base.transform;
		hasBody = body != null;
	}

	private void Start()
	{
		if (hasBody)
		{
			hasBody = body != null;
		}
	}

	private bool EnteredWater(float y)
	{
		return !wasUnder && y > 2.2f + WaterController.waterTransformHeight;
	}

	private bool HitByBigWave(float y)
	{
		return y > 4f + WaterController.waterTransformHeight;
	}

	private bool MovingQuicklyDownwards()
	{
		return hasBody && body.velocity.y < -1.5f;
	}

	private void FixedUpdate()
	{
		if (timer > repeatRate)
		{
			if (hasBody && StatMaster.isMP && StatMaster.isClient && !StatMaster.isLocalSim && body == null)
			{
				hasBody = false;
			}
			if (hasBody)
			{
				Vector3 velocity = body.velocity;
				velocity.y = 0f;
				if (velocity.sqrMagnitude < 2f)
				{
					return;
				}
			}
			float num = WaterController.CheckHeightMap(t.position.x, t.position.z);
			if (t.position.y < num)
			{
				if (EnteredWater(num) || HitByBigWave(num) || MovingQuicklyDownwards())
				{
					Set(num);
				}
				wasUnder = true;
				timer = 0f;
			}
			else
			{
				wasUnder = false;
			}
		}
		else
		{
			timer += Time.fixedDeltaTime;
		}
	}

	private void Set(float y)
	{
		Vector3 position = t.position;
		position.y = y;
		y = parent.parent.InverseTransformPoint(position).y;
		position = parent.localPosition;
		position.y = y;
		parent.localPosition = position;
		for (int i = 0; i < particles.Length; i++)
		{
			particles[i].Stop();
			particles[i].Play();
		}
	}
}
