using System;
using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/GenericDraggedBlock")]
public class GenericDraggedBlock : BlockBehaviour
{
	public bool isSet;

	public int version = 1;

	public Transform startPoint;

	public Transform endPoint;

	public Transform startInterpolater;

	public Transform endInterpolater;

	public Transform cylinder;

	public float radius = 0.5f;

	public float rayLength = 1f;

	public LayerMask layerMasky;

	public GameObject[] occluders;

	public bool parentVis;

	public MeshRenderer startVis;

	public MeshRenderer endVis;

	public Rigidbody startBody;

	public Rigidbody endBody;

	[NonSerialized]
	public Transform symEndPos;

	[NonSerialized]
	public Vector3 savedPosA;

	[NonSerialized]
	public Vector3 savedPosB;

	[NonSerialized]
	public Vector3 savedEulerA;

	[NonSerialized]
	public Vector3 savedEulerB;

	private bool hasBothPoints;

	private bool looksLikeGhost;

	public override Vector3 GetCenter()
	{
		if (!hasOffset)
		{
			center = base.transform.InverseTransformPoint(cylinder.position);
			hasOffset = true;
		}
		return base.transform.TransformPoint(center);
	}

	protected override void Start()
	{
		base.Start();
		GameObject[] array = occluders;
		foreach (GameObject gameObject in array)
		{
			gameObject.SetActive(false);
		}
		if (isSimulating)
		{
			UnityEngine.Object.Destroy(cylinder.GetComponent<Collider>());
			if (!SimPhysics)
			{
				return;
			}
		}
		SetCenterOfMass();
	}

	public override void SetRotation(Quaternion rot)
	{
		base.SetRotation(rot);
		SaveEulerAngles(base.LastState);
	}

	protected virtual void CreateCylinderBetweenPoints(Vector3 start, Vector3 end)
	{
		Vector3 localScale = base.transform.localScale;
		float num;
		float num2;
		float num3;
		if (localScale != Vector3.one)
		{
			if (abs(localScale.x) < 0.001f || abs(localScale.y) < 0.001f || abs(localScale.z) < 0.001f)
			{
				cylinder.localScale = new Vector3(radius, radius, radius);
				return;
			}
			start = base.transform.InverseTransformPoint(start);
			end = base.transform.InverseTransformPoint(end);
			num = end.x - start.x;
			num2 = end.y - start.y;
			num3 = end.z - start.z;
			float num4 = num * num + num2 * num2 + num3 * num3;
			if (num4 < 1.4E-44f)
			{
				cylinder.localScale = Vector3.zero;
				return;
			}
			cylinder.localPosition = new Vector3(start.x + num * 0.5f, start.y + num2 * 0.5f, start.z + num3 * 0.5f);
			if (abs(num) + abs(num2) + abs(num3) > float.Epsilon)
			{
				cylinder.localRotation = Quaternion.LookRotation(new Vector3(num, num2, num3), base.transform.InverseTransformDirection(Vector3.up));
			}
		}
		else
		{
			num = end.x - start.x;
			num2 = end.y - start.y;
			num3 = end.z - start.z;
			cylinder.position = new Vector3(start.x + num * 0.5f, start.y + num2 * 0.5f, start.z + num3 * 0.5f);
			if (abs(num) + abs(num2) + abs(num3) > float.Epsilon)
			{
				cylinder.rotation = Quaternion.LookRotation(new Vector3(num, num2, num3), Vector3.up);
			}
		}
		cylinder.localScale = new Vector3(radius, radius, Mathf.Sqrt(num * num + num2 * num2 + num3 * num3));
	}

	protected float abs(float val)
	{
		return (!(val < 0f)) ? val : (0f - val);
	}

	public virtual void UpdateDragged()
	{
		if (endPoint.gameObject == null)
		{
			Debug.LogError("endPoints is null in UpdateDragged!");
		}
		else
		{
			SetDragged(Quaternion.LookRotation(AddPiece.mouseHitNormal));
		}
	}

	protected void SetDragged(Quaternion rotation)
	{
		AddPiece instance = SingleInstanceFindOnly<AddPiece>.Instance;
		if (instance.mouseHasHit && instance.validHitThisFrame)
		{
			if (looksLikeGhost)
			{
				VisualController.SetNormal();
				looksLikeGhost = false;
			}
			endPoint.position = ((!symEndPos) ? AddPiece.mouseHitPos : symEndPos.position);
			hasBothPoints = true;
		}
		else
		{
			if ((bool)symEndPos)
			{
				endPoint.position = startPoint.position;
			}
			else
			{
				Vector3 vector = startPoint.position - instance.ray.origin;
				vector = Vector3.Project(vector, instance.ray.direction.normalized);
				endPoint.position = instance.ray.origin + vector;
			}
			if (!looksLikeGhost)
			{
				VisualController.MimicGhost();
				looksLikeGhost = true;
			}
			hasBothPoints = false;
		}
		if (!looksLikeGhost && Vector3.SqrMagnitude(startPoint.position - endPoint.position) < 1E-05f)
		{
			VisualController.MimicGhost();
			looksLikeGhost = true;
			hasBothPoints = false;
		}
		endPoint.rotation = rotation;
		CreateCylinderBetweenPoints(startPoint.position, endPoint.position);
	}

	public virtual bool Set(bool forceFail = false)
	{
		isSet = true;
		endPoint.position = ((!symEndPos) ? AddPiece.mouseHitPos : symEndPos.position);
		endPoint.rotation = ((!symEndPos) ? AddPiece.blockPlacedRotation : symEndPos.rotation);
		if (!base.HasParentMachine)
		{
			UpdateParentMachine();
		}
		_parentMachine.UnregisterUpdate(this, true);
		AddPiece instance = SingleInstanceFindOnly<AddPiece>.Instance;
		if (forceFail || !hasBothPoints || instance.symmetryController.CheckForBraceDouble(startPoint.position, endPoint.position))
		{
			_parentMachine.RemoveBlock(this);
			StatMaster.Mode.placingBlock = false;
			return false;
		}
		GameObject[] array = occluders;
		foreach (GameObject gameObject in array)
		{
			gameObject.SetActive(true);
		}
		SetCenterOfMass();
		return true;
	}

	public virtual void SetPositionsGlobal(Vector3 starty, Vector3 endy)
	{
		startPoint.position = starty;
		endPoint.position = endy;
		GameObject[] array = occluders;
		foreach (GameObject gameObject in array)
		{
			gameObject.SetActive(true);
		}
		if ((bool)startInterpolater && (bool)endInterpolater)
		{
			CreateCylinderBetweenPoints(startInterpolater.position, endInterpolater.position);
		}
		else
		{
			CreateCylinderBetweenPoints(startPoint.position, endPoint.position);
		}
		SetCenterOfMass();
	}

	public void SaveBraceState()
	{
		savedPosA = startPoint.position;
		savedPosB = endPoint.position;
		savedEulerA = startPoint.eulerAngles;
		savedEulerB = endPoint.eulerAngles;
	}

	public void SetPositionsGlobal(Vector3 starty, Vector3 startR, Vector3 endy, Vector3 endR, bool sync)
	{
		startPoint.eulerAngles = base.ParentMachine.boundingBoxController.transform.eulerAngles + startR;
		endPoint.eulerAngles = base.ParentMachine.boundingBoxController.transform.eulerAngles + endR;
		SetPositionsGlobal(starty, endy);
		if (sync && StatMaster.isMP && _parentMachine.isLocalMachine)
		{
			XDataHolder xDataHolder = new XDataHolder();
			OnSave(xDataHolder);
			byte[] outData;
			xDataHolder.Encode(out outData);
			byte[] array = new byte[64 + outData.Length];
			int num = 0;
			NetworkCompression.WriteUInt((uint)BuildIndex, false, array, num);
			num += 4;
			NetworkCompression.PackVector(Position, array, num);
			num += 12;
			NetworkCompression.PackVector(base.transform.position, array, num);
			num += 12;
			NetworkCompression.PackQuaternion(Rotation, array, num);
			num += 16;
			NetworkCompression.PackQuaternion(base.transform.rotation, array, num);
			num += 16;
			NetworkCompression.WriteUInt((ushort)outData.Length, false, array, num);
			num += 4;
			Buffer.BlockCopy(outData, 0, array, num, outData.Length);
			NetworkAuxAddPiece instance = NetworkAuxAddPiece.Instance;
			if (StatMaster.cachingTransformActions)
			{
				(_parentMachine as ServerMachine).CacheBlockTransformAction(RPCMessageType.MirrorDragged, array);
			}
			else
			{
				instance.SendNetworkMessage(RPCMessageType.MirrorDragged, array);
			}
		}
		OnSave(new XDataHolder());
	}

	public virtual void SetCenterOfMass()
	{
		if (!noRigidbody)
		{
			Rigidbody.centerOfMass = base.transform.InverseTransformPoint((startPoint.position + endPoint.position) / 2f);
		}
	}

	public void SaveEulerAngles(XDataHolder data)
	{
		Vector3 vector = startPoint.eulerAngles - base.ParentMachine.boundingBoxController.transform.eulerAngles;
		Vector3 vector2 = endPoint.eulerAngles - base.ParentMachine.boundingBoxController.transform.eulerAngles;
		data.Write("start-rotation", vector);
		data.Write("end-rotation", vector2);
	}

	public override void OnSave(XDataHolder data)
	{
		base.OnSave(data);
		data.Write("start-position", base.transform.InverseTransformPoint(startPoint.position));
		data.Write("end-position", base.transform.InverseTransformPoint(endPoint.position));
		SaveEulerAngles(data);
	}

	public override void OnLoad(XDataHolder data)
	{
		base.OnLoad(data);
		if (!isSimulating && data.HasKey("start-position") && data.HasKey("end-position"))
		{
			if (data.HasKey("start-rotation") && data.HasKey("end-rotation"))
			{
				SetPositionsGlobal(base.transform.TransformPoint(data.ReadVector3("start-position")), data.ReadVector3("start-rotation"), base.transform.TransformPoint(data.ReadVector3("end-position")), data.ReadVector3("end-rotation"), false);
			}
			else
			{
				SetPositionsGlobal(base.transform.TransformPoint(data.ReadVector3("start-position")), base.transform.TransformPoint(data.ReadVector3("end-position")));
			}
			if (!base.HasParentMachine)
			{
				UpdateParentMachine();
			}
			_parentMachine.UnregisterUpdate(this, true);
			isSet = true;
		}
	}
}
