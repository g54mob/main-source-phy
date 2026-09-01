using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/Surface/BuildEdgeBlock")]
public class BuildEdgeBlock : BlockBehaviour
{
	public BuildNodeBlock startNode;

	public BuildNodeBlock endNode;

	public float Radius = 0.18f;

	public bool needsSort;

	public bool isValid;

	private Vector3[] pointPath;

	private Vector3 start;

	private Vector3 pos;

	private Vector3 end;

	private Vector3 delta;

	private static Vector3 a;

	private static Vector3 b;

	private static Vector3 c;

	private static Vector3 d;

	private static Vector3 result;

	private static float u2;

	private static float u3;

	private static float ax;

	private static float ay;

	private static float az;

	private static float bx;

	private static float by;

	private static float bz;

	private static float cx;

	private static float cy;

	private static float cz;

	private static float dx;

	private static float dy;

	private static float dz;

	public bool isStraight { get; private set; }

	public float Length { get; private set; }

	public float Angle { get; private set; }

	public Vector3 Direction { get; private set; }

	public override void StartPhysics(bool isKinematic)
	{
		base.gameObject.SetActive(false);
	}

	protected override void Awake()
	{
		VisualController.lockVisibility = true;
		if (isSimulating)
		{
			BuildEdgeBlock buildEdgeBlock = (BuildEdgeBlock)BuildingBlock;
			Length = buildEdgeBlock.Length;
			Direction = buildEdgeBlock.Direction;
		}
		else
		{
			Direction = base.transform.forward;
		}
		base.Awake();
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (!isSimulating)
		{
			Machine parentMachine = base.ParentMachine;
			parentMachine.UnregisterSurfaceBlock(this);
		}
	}

	public override void OnMapperOpen()
	{
		if (StatMaster.KeyMapper.allowSelectingNodes)
		{
			base.OnMapperOpen();
			return;
		}
		List<BuildSurface> surfaces = base.ParentMachine.nodeController.GetSurfaces(this);
		if (surfaces.Count > 0)
		{
			BuildSurface buildSurface = surfaces.FirstOrDefault((BuildSurface x) => x.IsSelected);
			if (!buildSurface)
			{
				buildSurface = surfaces[0];
			}
			buildSurface.SetOutlineForMapper(true);
			BlockMapper.Open(buildSurface);
		}
		else
		{
			base.OnMapperOpen();
		}
	}

	public override void SetPosition(Vector3 pos)
	{
		Vector3 position = Position;
		base.SetPosition(pos);
		Machine parentMachine = base.ParentMachine;
		if (position != Position || !parentMachine.isLocalMachine)
		{
			NodeController nodeController = parentMachine.nodeController;
			nodeController.Refresh(this);
			List<BuildSurface> surfaces = nodeController.GetSurfaces(this);
			for (int i = 0; i < surfaces.Count; i++)
			{
				surfaces[i].SurfaceChanged(this);
			}
		}
	}

	protected void OnBecameVisible()
	{
		base.ParentMachine.RegisterSurfaceBlock(this);
	}

	protected void OnBecameInvisible()
	{
		base.ParentMachine.UnregisterSurfaceBlock(this);
	}

	public bool RayHit(Ray ray, out float dist)
	{
		return NodeController.RaySphereIntersection(ray, base.transform.position, Radius * VisualController.MeshFilter.transform.localScale.x, out dist);
	}

	private bool EdgeChanged()
	{
		return isValid && (start != startNode.Position || pos != Position || end != endNode.Position);
	}

	public bool UpdatePlanar()
	{
		if (!isValid || !EdgeChanged())
		{
			return false;
		}
		start = startNode.Position;
		pos = Position;
		end = endNode.Position;
		delta = end - start;
		SetRotation(Quaternion.identity);
		float num = 0.0001f;
		isStraight = (Position - (start + delta * 0.5f)).sqrMagnitude < num;
		return true;
	}

	public override void SetRotation(Quaternion rot)
	{
		UpdateEdgeDir();
		base.SetRotation(base.transform.rotation);
	}

	public void UpdateEdgeDir()
	{
		if (delta.sqrMagnitude > 0.01f && isValid)
		{
			base.transform.forward = endNode.transform.position - startNode.transform.position;
			Direction = base.transform.forward;
		}
	}

	public bool UpdateEdge()
	{
		if (!isValid || !UpdatePlanar())
		{
			return false;
		}
		if (isStraight)
		{
			Length = delta.magnitude;
			Angle = 0f;
			return true;
		}
		Vector3 vector = start - Position;
		Vector3 vector2 = end - Position;
		float magnitude = vector.magnitude;
		float magnitude2 = vector2.magnitude;
		Length = magnitude + magnitude2;
		Vector3 rhs = vector / magnitude;
		Vector3 vector3 = vector2 / magnitude2;
		float num = 45f;
		float num2 = Vector3.Dot(-vector3, rhs);
		Angle = 57.29578f * Mathf.Clamp(Mathf.Acos(num2), -1f, 1f);
		if (Mathf.Abs(Angle) > num)
		{
			num2 = Mathf.Cos(num * Mathf.Sign(Angle) * ((float)Math.PI / 180f));
		}
		Direction = (end - start).normalized;
		Vector3 vector4 = Vector3.Cross(-vector3, rhs);
		Vector4 normalized = new Vector4(vector4.x, vector4.y, vector4.z, 1f + num2).normalized;
		Vector3 vector5 = new Quaternion(normalized.x, normalized.y, normalized.z, normalized.w) * vector;
		Vector3 vector6 = new Quaternion(0f - normalized.x, 0f - normalized.y, 0f - normalized.z, normalized.w) * vector2;
		pointPath = new Vector3[5]
		{
			start + vector5,
			start,
			Position,
			end,
			end + vector6
		};
		return true;
	}

	public Vector3 Interp(float t)
	{
		if (isStraight)
		{
			ax = start.x;
			ay = start.y;
			az = start.z;
			bx = delta.x;
			by = delta.y;
			bz = delta.z;
			result = new Vector3(ax + bx * t, ay + by * t, az + bz * t);
			return result;
		}
		if (pointPath.Length < 4)
		{
			UpdateEdge();
		}
		int num = pointPath.Length - 3;
		int num2 = Mathf.Min(Mathf.FloorToInt(t * (float)num), num - 1);
		float num3 = t * (float)num - (float)num2;
		a = pointPath[num2];
		b = pointPath[num2 + 1];
		c = pointPath[num2 + 2];
		d = pointPath[num2 + 3];
		u2 = num3 * num3;
		u3 = u2 * num3;
		ax = a.x;
		ay = a.y;
		az = a.z;
		cx = c.x;
		cy = c.y;
		cz = c.z;
		bx = b.x;
		by = b.y;
		bz = b.z;
		dx = d.x;
		dy = d.y;
		dz = d.z;
		result = new Vector3(0.5f * ((0f - ax + 3f * bx - 3f * cx + dx) * u3 + (2f * ax - 5f * bx + 4f * cx - dx) * u2 + (0f - ax + cx) * num3 + 2f * bx), 0.5f * ((0f - ay + 3f * by - 3f * cy + dy) * u3 + (2f * ay - 5f * by + 4f * cy - dy) * u2 + (0f - ay + cy) * num3 + 2f * by), 0.5f * ((0f - az + 3f * bz - 3f * cz + dz) * u3 + (2f * az - 5f * bz + 4f * cz - dz) * u2 + (0f - az + cz) * num3 + 2f * bz));
		return result;
	}

	public override void OnLoad(XDataHolder data)
	{
		if (isSimulating)
		{
			return;
		}
		isValid = true;
		if (isBMAction)
		{
			if (startNode != null && endNode != null)
			{
				WriteData(data, startNode, endNode);
			}
			else
			{
				Debug.Log(string.Concat("Edge ", Guid, " is settng valid to false, but why, it might become valid as the rest of this method finishes..."));
				isValid = false;
			}
		}
		else if (!data.HasKey("start") || !data.HasKey("end"))
		{
			Debug.LogError(string.Concat("Edge ", Guid, " doesn't contain start and/or end node!"));
			isValid = false;
			return;
		}
		Guid guid = new Guid(data.ReadString("start"));
		Guid guid2 = new Guid(data.ReadString("end"));
		Machine parentMachine = base.ParentMachine;
		BlockBehaviour block;
		BlockBehaviour block2;
		if (!parentMachine.GetBlock(guid, out block))
		{
			Debug.LogError(string.Concat("Edge ", Guid, " couldn't find start block ", guid, "!"));
			isValid = false;
		}
		else if (!parentMachine.GetBlock(guid2, out block2))
		{
			Debug.LogError(string.Concat("Edge ", Guid, " couldn't find end block ", guid, "!"));
			isValid = false;
		}
		else
		{
			startNode = block as BuildNodeBlock;
			endNode = block2 as BuildNodeBlock;
			parentMachine.nodeController.Refresh(this);
		}
	}

	public override void OnPostEdit()
	{
		base.OnPostEdit();
		WriteData(InitialState, startNode, endNode);
		needsSort = true;
		NodeController nodeController = base.ParentMachine.nodeController;
		nodeController.Refresh(this);
		nodeController.GetSurfaces(this).ForEach(delegate(BuildSurface x)
		{
			x.needsSort = true;
			nodeController.Refresh(x);
		});
	}

	public static void WriteData(XDataHolder data, BuildNodeBlock start, BuildNodeBlock end)
	{
		if (!(start == null) && !(end == null))
		{
			data.Write("start", start.Guid.ToString());
			data.Write("end", end.Guid.ToString());
		}
	}

	public override void OnSave(XDataHolder data)
	{
		base.OnSave(data);
		WriteData(data, startNode, endNode);
	}
}
