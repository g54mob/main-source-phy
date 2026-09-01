using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Rope : MonoBehaviour, GameObjectPooling.IPoolable
{
	[Range(0f, 1f)]
	public float stiffnes;

	public float dragMultiplier = 1f;

	public int segments = 10;

	public bool checkCollision = true;

	public float noise;

	public float noiseScale = 0.5f;

	public AnimationCurve lerpOverLineAmount;

	public AnimationCurve noiseOverLineAmount;

	public float lerpSpeed = 10f;

	public Transform positionobject1;

	public Transform positionobject2;

	public Transform bezierPos;

	[HideInInspector]
	public Vector3 position1;

	[HideInInspector]
	public Vector3 middleVelocity;

	[HideInInspector]
	public bool done;

	private Vector3 position2;

	private Vector3 doneVelocity;

	private LineRenderer line;

	private Vector3 middlePos;

	private Vector3[] positions;

	private float clampedTime;

	private Vector3 originalPosition1;

	private Vector3 originalPosition2;

	private Vector3 originalMiddleVelocity;

	private Vector3 originalDoneVelocity;

	private bool shouldUpdate;

	[HideInInspector]
	public Vector3 Position2
	{
		get
		{
			return position2;
		}
		set
		{
			if (!done)
			{
				position2 = value;
			}
		}
	}

	public bool IsManagedByPool { get; set; }

	public Action ReleaseSelf { get; set; }

	private void Awake()
	{
		line = GetComponent<LineRenderer>();
	}

	private void Start()
	{
		originalPosition1 = position1;
		originalPosition2 = position2;
		originalMiddleVelocity = middleVelocity;
		originalDoneVelocity = doneVelocity;
		if (!IsManagedByPool)
		{
			InitializeOnSpawn();
		}
	}

	private void Update()
	{
		if (!shouldUpdate)
		{
			return;
		}
		clampedTime = Mathf.Clamp(Time.deltaTime, 0f, 0.02f);
		if (done)
		{
			Done();
		}
		if ((bool)positionobject1)
		{
			position1 = positionobject1.transform.position;
		}
		if ((bool)positionobject1)
		{
			position2 = positionobject2.transform.position;
		}
		Vector3 vector = (position1 + position2) * 0.5f;
		if ((bool)bezierPos)
		{
			vector = bezierPos.position;
		}
		if (position1 != Vector3.zero && middlePos == Vector3.zero)
		{
			middlePos = vector;
		}
		middleVelocity += clampedTime * (500f * (0.1f + stiffnes * 3f)) * (vector - middlePos);
		middleVelocity += clampedTime * 20f * Vector3.down;
		middleVelocity += clampedTime * -10f * (0.1f + stiffnes * 2f) * dragMultiplier * middleVelocity;
		if (checkCollision)
		{
			Collider[] array = Physics.OverlapSphere(middlePos, 0.5f);
			for (int i = 0; i < array.Length; i++)
			{
				Vector3 vector2 = array[i].ClosestPoint(middlePos);
				middleVelocity += (middlePos - vector2).normalized * (clampedTime * 400f * ((0.5f - Vector3.Distance(vector2, middlePos)) * 2f));
			}
		}
		middlePos += middleVelocity * clampedTime;
		for (int j = 0; j < positions.Length; j++)
		{
			float num = (float)j / ((float)positions.Length - 1f);
			Vector3 vector3 = BezierCurve.QuadraticBezier(position1, middlePos, position2, num);
			if (noise != 0f)
			{
				float num2 = 1f;
				if (noiseOverLineAmount.length > 0)
				{
					num2 = noiseOverLineAmount.Evaluate(num);
				}
				vector3 += GetPerlinPos((float)j / (float)positions.Length, positions[j]) * noise * num2;
			}
			if (lerpOverLineAmount.length > 0)
			{
				float t = lerpOverLineAmount.Evaluate(num);
				Vector3 b = Vector3.Lerp(positions[j], vector3, clampedTime * lerpSpeed);
				positions[j] = Vector3.Lerp(vector3, b, t);
			}
			else
			{
				positions[j] = vector3;
			}
		}
		line.SetPositions(positions);
	}

	public void Initialize()
	{
		InitializeOnSpawn();
	}

	public void Reset()
	{
		line.enabled = true;
		done = false;
		position1 = originalPosition1;
		position2 = originalPosition2;
		middleVelocity = originalMiddleVelocity;
		doneVelocity = originalDoneVelocity;
	}

	public void Release()
	{
	}

	private void InitializeOnSpawn()
	{
		shouldUpdate = true;
		positions = new Vector3[segments];
		line.positionCount = segments;
		for (int i = 0; i < positions.Length; i++)
		{
			positions[i] = base.transform.position;
		}
		line.SetPositions(positions);
	}

	private void Done()
	{
		doneVelocity += (position1 - position2) * clampedTime * 50f;
		middleVelocity += (position1 - middlePos) * clampedTime * 40f;
		doneVelocity += doneVelocity * clampedTime * -10f;
		position2 += doneVelocity * clampedTime;
		if (Vector3.Distance(position1, position2) < 0.3f)
		{
			if (IsManagedByPool)
			{
				line.enabled = false;
				shouldUpdate = false;
			}
			else
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	private Vector3 GetPerlinPos(float input, Vector3 position)
	{
		input *= noiseScale;
		Vector3 zero = Vector3.zero;
		zero.x += Mathf.PerlinNoise(position.x * noiseScale, position.z * noiseScale);
		zero.y += Mathf.PerlinNoise(position.y * noiseScale, position.x * noiseScale);
		zero.z += Mathf.PerlinNoise(position.z * noiseScale, position.x * noiseScale);
		zero.x -= 0.5f;
		zero.y -= 0.5f;
		zero.z -= 0.5f;
		return zero;
	}
}
