using System;
using System.Collections.Generic;
using UnityEngine;

public class Lookable : MonoBehaviour, ISaveable
{
	public bool wasTriggeredOnce;

	[DontSave]
	public bool localPlayerActivating;

	[DontSave]
	public List<GameObject> onActivated = new List<GameObject>();

	[DontSave]
	public List<GameObject> onDeactivated = new List<GameObject>();

	[DontSave]
	public bool activateOnlyOnce;

	[DontSave]
	public float targetVisibilityPercent = 0.5f;

	[DontSave]
	public float targetScreenPercent = 0.5f;

	[DontSave]
	public int accuracy = 10;

	[DontSave]
	private Bounds bounds;

	[DontSave]
	private Collider[] colliders;

	[DontSave]
	private List<MeshRenderer> meshRenderers = new List<MeshRenderer>();

	[DontSave]
	private Camera mainCamera;

	[DontSave]
	private Transform cameraTransform;

	[DontSave]
	private float currentVisibilityPercent;

	[DontSave]
	private float currentScreenPercent;

	[DontSave]
	private float currentCoveredPercentOnScreen;

	public bool isActivated
	{
		get
		{
			if (currentVisibilityPercent > targetVisibilityPercent)
			{
				return currentScreenPercent > targetScreenPercent;
			}
			return false;
		}
	}

	private bool isValid
	{
		get
		{
			if (colliders != null)
			{
				return meshRenderers.Find((MeshRenderer x) => x.isVisible) != null;
			}
			return false;
		}
	}

	public void init(Bounds bounds, MeshRenderer[] meshRenderers, Collider[] colliders, Camera mainCamera, Transform cameraTransform)
	{
		this.bounds = bounds;
		this.colliders = colliders;
		this.mainCamera = mainCamera;
		this.cameraTransform = cameraTransform;
		this.meshRenderers = new List<MeshRenderer>(meshRenderers);
	}

	public void estimateVisibility()
	{
		if (!isValid)
		{
			currentVisibilityPercent = 0f;
			currentCoveredPercentOnScreen = 0f;
			return;
		}
		Camera camera = mainCamera;
		int num = accuracy;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		Vector3 min = bounds.min;
		Vector3 max = bounds.max;
		if (!GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(mainCamera), bounds))
		{
			currentVisibilityPercent = 0f;
			currentCoveredPercentOnScreen = 0f;
			return;
		}
		for (int i = 0; i < num; i++)
		{
			float t = (float)i / (float)(num - 1);
			for (int j = 0; j < num; j++)
			{
				float t2 = (float)j / (float)(num - 1);
				for (int k = 0; k < num; k++)
				{
					float t3 = (float)k / (float)(num - 1);
					Vector3 vector = new Vector3(Mathf.Lerp(min.x, max.x, t), Mathf.Lerp(min.y, max.y, t2), Mathf.Lerp(min.z, max.z, t3));
					Vector3 vector2 = camera.WorldToViewportPoint(vector);
					num2++;
					if (!(vector2.z < 0f) && !(vector2.x < 0f) && !(vector2.x > 1f) && !(vector2.y < 0f) && !(vector2.y > 1f))
					{
						num3++;
						Vector3 vector3 = vector - cameraTransform.position;
						if (Physics.Raycast(new Ray(cameraTransform.position, vector3.normalized), out var hit, vector3.magnitude * 1.1f) && Array.Exists(colliders, (Collider r) => r != null && r == hit.collider))
						{
							num4++;
						}
					}
				}
			}
		}
		currentVisibilityPercent = ((num2 > 0) ? ((float)num4 / (float)num2) : 0f);
		currentCoveredPercentOnScreen = ((num3 > 0) ? ((float)num4 / (float)num3) : 0f);
	}

	public void estimateScreenCoverage()
	{
		if (!isValid)
		{
			currentScreenPercent = 0f;
			return;
		}
		Span<Vector3> span = stackalloc Vector3[8];
		Vector3 min = bounds.min;
		Vector3 max = bounds.max;
		span[0] = new Vector3(min.x, min.y, min.z);
		span[1] = new Vector3(max.x, min.y, min.z);
		span[2] = new Vector3(min.x, max.y, min.z);
		span[3] = new Vector3(max.x, max.y, min.z);
		span[4] = new Vector3(min.x, min.y, max.z);
		span[5] = new Vector3(max.x, min.y, max.z);
		span[6] = new Vector3(min.x, max.y, max.z);
		span[7] = new Vector3(max.x, max.y, max.z);
		Vector2 lhs = new Vector2(float.MaxValue, float.MaxValue);
		Vector2 lhs2 = new Vector2(float.MinValue, float.MinValue);
		bool flag = false;
		Span<Vector3> span2 = span;
		for (int i = 0; i < span2.Length; i++)
		{
			Vector3 position = span2[i];
			Vector3 vector = mainCamera.WorldToScreenPoint(position);
			if (vector.z > 0f)
			{
				flag = true;
				lhs = Vector2.Min(lhs, vector);
				lhs2 = Vector2.Max(lhs2, vector);
			}
		}
		if (!flag)
		{
			currentScreenPercent = 0f;
			return;
		}
		lhs = Vector2.Max(lhs, Vector2.zero);
		lhs2 = Vector2.Min(lhs2, new Vector2(Screen.width, Screen.height));
		float num = Mathf.Max(0f, lhs2.x - lhs.x) * Mathf.Max(0f, lhs2.y - lhs.y);
		int num2 = Screen.width * Screen.height;
		float num3 = num / (float)num2;
		currentScreenPercent = currentCoveredPercentOnScreen * num3;
	}

	public virtual void save(FastBinaryWriter writer)
	{
		writer.Write(in wasTriggeredOnce, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void load(FastBinaryReader reader)
	{
		wasTriggeredOnce = reader.ReadBoolean();
	}

	public virtual void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		int position = reader.Position;
		bool flag = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "wasTriggeredOnce",
			fieldValue = $"{flag}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}
}
