using Modding;
using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/SliderBlock")]
public class SliderBlock : BlockBehaviour
{
	public ConfigurableJoint myJoint;

	public TriggerSetJointShift jointTrigger;

	public Collider extendedCol;

	public BoxCollider occluder;

	private bool isLoadFlip;

	private bool initializedFlip;

	private LayerMask m;

	private LayerMask m2;

	private bool counterPosition;

	public override Vector3 GetTarget()
	{
		return (!Flipped) ? base.transform.position : (base.transform.position - base.transform.forward);
	}

	protected override void Start()
	{
		base.Start();
		initializedFlip = false;
		counterPosition = false;
		if (!isSimulating)
		{
			m = AddPiece.CreateLayerMask(SingleInstanceFindOnly<AddPiece>.Instance.layerMasky, 2, 24, 29);
			m2 = AddPiece.CreateLayerMask(SingleInstanceFindOnly<AddPiece>.Instance.layerMasky, 24, 29);
			SetDefaultExtension();
		}
		else if (extendedCol != null)
		{
			Object.Destroy(extendedCol.gameObject);
		}
	}

	public override bool OnFlip(bool sound, bool isUndo)
	{
		UpdateParentMachine();
		if (!base.HasParentMachine || (isSimulating && !SimPhysics))
		{
			return false;
		}
		Machine parentMachine = base.ParentMachine;
		if (isSimulating)
		{
			return true;
		}
		bool flag = parentMachine.isLocalMachine && StatMaster.Bounding.Enabled;
		bool flag2 = !StatMaster.Mode.allowIntersection && parentMachine.isLocalMachine && !isLoadFlip;
		bool flag3 = !StatMaster.Mode.allowIntersection && !isUndo && flag2;
		initializedFlip = true;
		if (!counterPosition)
		{
			SetDefaultExtension();
			if (flag && flag2)
			{
				parentMachine.CheckBounds();
			}
			return true;
		}
		if (flag)
		{
			if (flag3)
			{
				RaycastHit[] array = Physics.RaycastAll(base.transform.position + base.transform.forward * ((!Flipped) ? 0f : 0.95f), base.transform.forward * ((!Flipped) ? (-1f) : 1f), 0.95f, m);
				for (int i = 0; i < array.Length; i++)
				{
					Collider collider = array[i].collider;
					if (collider.attachedRigidbody != Rigidbody && !collider.transform.root.name.Equals("_PERSISTENT"))
					{
						OnFlipIntersect();
						return false;
					}
				}
			}
			SetDefaultExtension();
			if (flag2)
			{
				parentMachine.CheckBounds();
			}
		}
		else
		{
			if (flag2)
			{
				RaycastHit[] array = Physics.RaycastAll(base.transform.position + base.transform.forward * ((!Flipped) ? 0f : 0.95f), base.transform.forward * ((!Flipped) ? (-1f) : 1f), 0.95f, m2);
				for (int i = 0; i < array.Length; i++)
				{
					Collider collider2 = array[i].collider;
					if (collider2.attachedRigidbody != Rigidbody && !collider2.transform.root.name.Equals("_PERSISTENT"))
					{
						OnFlipIntersect();
						return false;
					}
				}
			}
			SetDefaultExtension();
		}
		if (sound)
		{
			ReferenceMaster.PlayFlip();
		}
		return true;
	}

	private void SetDefaultExtension()
	{
		if (!noRigidbody)
		{
			Rigidbody.interpolation = RigidbodyInterpolation.None;
		}
		if (!stripped)
		{
			jointTrigger.shift = Flipped;
			jointTrigger.transform.localPosition = ((!Flipped) ? Vector3.zero : Vector3.back);
			occluder.size = ((!Flipped) ? new Vector3(1.05f, 1.05f, 0.1f) : new Vector3(0.8f, 0.7f, 0.1f));
			extendedCol.gameObject.SetActive(Flipped);
		}
		if (counterPosition)
		{
			base.transform.position += base.transform.forward * ((!Flipped) ? (-1f) : 1f);
			Position = base.transform.localPosition;
		}
		counterPosition = true;
		if (!noRigidbody)
		{
			Rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
		}
		if (!stripped)
		{
			SingleInstance<Events>.Instance.CollidersChanged(this);
		}
	}

	private void ReverseExtension()
	{
		SetDefaultExtension();
	}

	private void OnFlipIntersect()
	{
		Flipped = !Flipped;
		IntersectWarning.Warning();
	}

	public override void OnSave(XDataHolder data)
	{
		base.OnSave(data);
		if (Flipped)
		{
			data.Write("preextended", Flipped);
		}
	}

	public override void OnLoad(XDataHolder data)
	{
		base.OnLoad(data);
		if (!isSimulating || SimPhysics)
		{
			isLoadFlip = true;
			counterPosition = false;
			if (data.HasKey("preextended"))
			{
				Flipped = data.ReadBool("preextended");
				PostFlip(false, false);
			}
			else if (!initializedFlip)
			{
				Flipped = false;
				PostFlip(false, false);
			}
			isLoadFlip = false;
		}
	}
}
