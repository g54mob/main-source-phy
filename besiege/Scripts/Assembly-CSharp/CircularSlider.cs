using System;
using System.Collections.Generic;
using UnityEngine;

public class CircularSlider : ClickBehaviour
{
	private float _value;

	private float _max;

	[SerializeField]
	private Material material;

	[SerializeField]
	private int edgeCount = 30;

	[SerializeField]
	private float radius = 0.5f;

	[SerializeField]
	private bool invert;

	[SerializeField]
	private int mask = -1;

	private MeshFilter meshFilter;

	private MeshRenderer meshRenderer;

	private Camera hudCamera;

	private bool isFullMeshGenerated;

	private Vector3[] fullVertices;

	private int[] fullTriangles;

	private Vector2[] fullUVs;

	private float startAngle;

	private float addedAngle;

	private float lastAngle;

	private bool mouseDown;

	public int EdgeCount { get; set; }

	public float Radius { get; set; }

	public bool Invert { get; set; }

	public float Value
	{
		get
		{
			return _value;
		}
		set
		{
			float num = Mathf.Clamp(value, Min, MaxValue);
			if (!(Mathf.Abs(_value - num) < 0.001f))
			{
				_value = num;
				if (isFullMeshGenerated)
				{
					UpdateMesh();
				}
				InvokeValueChanged(num);
			}
		}
	}

	public float Min { get; set; }

	public float Max
	{
		get
		{
			return _max;
		}
		set
		{
			float max = (MaxValue = value);
			_max = max;
		}
	}

	public float MaxValue { get; set; }

	public event ValueChangeHandler ValueChanged;

	public event DoneEditingHandler DoneEditing;

	private void Awake()
	{
		hudCamera = GameObject.Find("HUD Cam").GetComponent<Camera>();
		meshFilter = GetComponent<MeshFilter>();
		if (!meshFilter)
		{
			meshFilter = base.gameObject.AddComponent<MeshFilter>();
		}
		meshRenderer = GetComponent<MeshRenderer>();
		if (!meshRenderer)
		{
			meshRenderer = base.gameObject.AddComponent<MeshRenderer>();
		}
		SetDefaultValues();
	}

	private void Start()
	{
		GenerateFullMeshReference();
		meshRenderer.material = material;
		Mesh mesh = new Mesh();
		mesh.MarkDynamic();
		meshFilter.mesh = mesh;
		UpdateMesh();
	}

	private void Update()
	{
		if (!UIMask.InsideMask(mask, base.transform.position))
		{
			OnClickReleased();
		}
		else if (mouseDown)
		{
			float mouseAngle = GetMouseAngle();
			float num = MathUtil.DifferenceBetweenAngles(lastAngle, mouseAngle);
			lastAngle = mouseAngle;
			addedAngle += num;
			Value = Mathf.Lerp(Min, Max, ((!Invert) ? (360f - addedAngle) : addedAngle) / 360f);
		}
	}

	public override void OnClicked()
	{
		startAngle = GetMouseAngle();
		addedAngle = startAngle;
		lastAngle = startAngle;
		mouseDown = true;
	}

	public override void OnClickReleased()
	{
		if (mouseDown)
		{
			mouseDown = false;
			InvokeDoneEditing();
		}
	}

	private float GetMouseAngle()
	{
		Vector3 position = InputManager.CursorPosition();
		position.z = Mathf.Abs(base.transform.position.z - hudCamera.transform.position.z);
		Vector3 normalized = (hudCamera.ScreenToWorldPoint(position) - base.transform.position).normalized;
		return (Mathf.Atan2(normalized.y, normalized.x) * 57.29578f + 180f + 90f) % 360f;
	}

	private void UpdateMesh()
	{
		int num = (int)Mathf.Ceil((Value - Min) / (Max - Min) * (float)EdgeCount);
		Mesh mesh = meshFilter.mesh;
		mesh.Clear();
		if (num > 0)
		{
			List<Vector3> list = new List<Vector3>(fullVertices.Slice(0, num + 2));
			List<Vector2> list2 = new List<Vector2>(fullUVs.Slice(0, num + 2));
			List<int> list3 = new List<int>(fullTriangles.Slice(0, num * 3 - 3));
			float num2 = Mathf.Lerp(Min, Max, (float)(num - 1) / (float)EdgeCount);
			float num3 = Mathf.Lerp(Min, Max, (float)num / (float)EdgeCount);
			float t = (Value - num2) / (num3 - num2);
			Vector3 item = Vector3.Lerp(fullVertices[num], fullVertices[num + 1], t);
			list.Add(item);
			list2.Add(new Vector2(1f, 1f));
			list3.Add(0);
			if (Invert)
			{
				list3.Add(list.Count - 1);
				list3.Add(list.Count - 3);
			}
			else
			{
				list3.Add(list.Count - 3);
				list3.Add(list.Count - 1);
			}
			mesh.vertices = list.ToArray();
			mesh.triangles = list3.ToArray();
			mesh.uv = list2.ToArray();
		}
		meshFilter.mesh = mesh;
	}

	private void GenerateFullMeshReference()
	{
		List<Vector3> list = new List<Vector3>();
		List<int> list2 = new List<int>();
		List<Vector2> list3 = new List<Vector2>();
		list.Add(Vector3.zero);
		list3.Add(new Vector2(0f, 0f));
		for (int i = 0; i < EdgeCount + 1; i++)
		{
			float f = (float)Math.PI / 180f * ((float)i / (float)EdgeCount * 360f);
			Vector3 item = new Vector3(Mathf.Sin(f), Mathf.Cos(f)) * Radius;
			if (Invert)
			{
				item.x = 0f - item.x;
			}
			list.Add(item);
			list3.Add(new Vector2(1f, 1f));
			if (i < EdgeCount)
			{
				list2.Add(0);
				if (Invert)
				{
					list2.Add(i + 2);
					list2.Add(i + 1);
				}
				else
				{
					list2.Add(i + 1);
					list2.Add(i + 2);
				}
			}
		}
		fullVertices = list.ToArray();
		fullTriangles = list2.ToArray();
		fullUVs = list3.ToArray();
		isFullMeshGenerated = true;
	}

	private void SetDefaultValues()
	{
		Min = 0f;
		Max = 1f;
		Value = 1f;
		EdgeCount = edgeCount;
		Radius = radius;
		Invert = invert;
	}

	public static CircularSlider Create(Vector3 position, Material material)
	{
		CircularSlider circularSlider = new GameObject("CircularSlider").AddComponent<CircularSlider>();
		circularSlider.transform.position = position;
		circularSlider.material = material;
		return circularSlider;
	}

	protected virtual void InvokeDoneEditing()
	{
		DoneEditingHandler doneEditing = this.DoneEditing;
		if (doneEditing != null)
		{
			doneEditing();
		}
	}

	protected virtual void InvokeValueChanged(float value)
	{
		ValueChangeHandler valueChanged = this.ValueChanged;
		if (valueChanged != null)
		{
			valueChanged(value);
		}
	}
}
