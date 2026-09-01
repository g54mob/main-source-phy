using System.Collections;
using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/ShorteningBlock")]
public class ShorteningBlock : BlockBehaviour
{
	[SerializeField]
	private int version = 1;

	public Transform endPiece;

	public Renderer halfVis;

	public Transform endTrigger;

	public int startingLength = 2;

	protected bool hasChecked;

	protected int length = 2;

	public MeshFilter shadow;

	public Mesh halfShadow;

	private Mesh orgShadow;

	public bool useShadow;

	public float shortMass;

	public float shortDensity;

	private float baseMass = 1f;

	private float baseDensity = 1f;

	public int Length
	{
		get
		{
			return length;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		if (!isSimulating)
		{
			length = startingLength;
			if (!noRigidbody)
			{
				baseMass = Rigidbody.mass;
			}
			baseDensity = density;
		}
		else
		{
			length = (BuildingBlock as ShorteningBlock).length;
			if (version == 0 && BlockHealth != null)
			{
				BlockHealth.weakenSecondaryJoints = false;
			}
		}
	}

	public override Vector3 GetCenter()
	{
		return base.transform.TransformPoint(new Vector3(0f, 0f, (float)length * 0.5f));
	}

	private void RayCheck()
	{
		if (length > startingLength - 1)
		{
			LayerMask layerMask = AddPiece.CreateLayerMask(new int[3] { 12, 14, 25 });
			RaycastHit[] array = Physics.RaycastAll(base.transform.TransformPoint(Prefab.rayPosition), base.transform.forward, 1f, layerMask);
			for (int i = 0; i < array.Length; i++)
			{
				RaycastHit raycastHit = array[i];
				BlockBehaviour componentInParent = raycastHit.collider.GetComponentInParent<BlockBehaviour>();
				if (!AddPiece.SelectedBlocks.Contains(componentInParent) && raycastHit.collider != null && raycastHit.collider.transform.parent != base.transform)
				{
					length = startingLength - 1;
					break;
				}
			}
		}
		UpdateLength(length, true, true);
	}

	public void UpdateLength(int newLength, bool save, bool syncLength = false)
	{
		length = newLength;
		bool flag = newLength == startingLength - 1;
		if (version > 0 && !noRigidbody)
		{
			if (shortMass == 0f)
			{
				float num = (float)newLength / (1f * (float)startingLength);
				float mass = baseMass * num;
				Rigidbody.mass = mass;
				density = baseDensity / Mathf.Sqrt(num);
			}
			else if (flag)
			{
				Rigidbody.mass = shortMass;
				density = shortDensity;
			}
			else
			{
				Rigidbody.mass = baseMass;
				density = baseDensity;
			}
			originalMass = Rigidbody.mass;
		}
		if (useShadow && orgShadow == null)
		{
			if ((bool)shadow)
			{
				orgShadow = shadow.sharedMesh;
			}
			else
			{
				useShadow = false;
			}
		}
		if (flag)
		{
			if (!stripped)
			{
				endTrigger.localPosition = new Vector3(endTrigger.localPosition.x, endTrigger.localPosition.y, 1f * (float)startingLength - 1f);
			}
			MeshRenderer.enabled = false;
			if (useShadow && (bool)halfShadow)
			{
				shadow.sharedMesh = halfShadow;
			}
			halfVis.enabled = true;
		}
		else
		{
			MeshRenderer.enabled = true;
			if (useShadow)
			{
				shadow.sharedMesh = orgShadow;
			}
			halfVis.enabled = false;
		}
		if (!stripped && (bool)endPiece)
		{
			endPiece.gameObject.SetActive(!flag);
		}
		if (Prefab.hasBVC)
		{
			BlockVisualController visualController = VisualController;
			if (Prefab.hasFragment)
			{
				FragmentVisualController fragmentVisualController = visualController as FragmentVisualController;
				fragmentVisualController.breakIntoPieces = !flag;
				fragmentVisualController.sjOffset = Vector3.forward * length;
			}
			visualController.ResetIsVisible();
			visualController.SetNormal();
		}
		StartCoroutine(SyncLength());
		if (save)
		{
			OnSave(new XDataHolder());
		}
	}

	private IEnumerator SyncLength()
	{
		yield return new WaitForFixedUpdate();
		if (StatMaster.isMP && _parentMachine.isLocalMachine && length != startingLength)
		{
			byte[] shortenData = new byte[5];
			int offset = 0;
			NetworkCompression.WriteUInt((uint)BuildIndex, false, shortenData, offset);
			offset += 4;
			shortenData[offset] = (byte)length;
			NetworkAuxAddPiece.Instance.SendNetworkMessage(RPCMessageType.ShortenBlock, shortenData);
		}
	}

	public override void OnSave(XDataHolder data)
	{
		data.Write("bmt-version", version);
		base.OnSave(data);
		data.Write("length", length);
	}

	public override void OnLoad(XDataHolder data)
	{
		if (!isSimulating)
		{
			if (!data.HasKey("bmt-version"))
			{
				if (data.WasLoadedFromFile)
				{
					version = 0;
					data.Write("bmt-version", version);
				}
			}
			else if (data.WasLoadedFromFile)
			{
				version = data.ReadInt("bmt-version");
			}
		}
		base.OnLoad(data);
		if (!isSimulating)
		{
			bool flag = data.HasKey("length");
			length = ((!flag) ? length : data.ReadInt("length"));
			if (!flag && _parentMachine.isLocalMachine && !hasChecked && !BlockSelectionTool.Duplicating)
			{
				RayCheck();
			}
			else
			{
				UpdateLength(length, false);
			}
			hasChecked = true;
		}
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		StopAllCoroutines();
	}
}
