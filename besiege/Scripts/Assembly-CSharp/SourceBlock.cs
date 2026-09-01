using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/Source Block")]
public class SourceBlock : BlockBehaviour
{
	[SerializeField]
	private int version = 1;

	public Transform particles;

	public override bool SetColliderIterations
	{
		get
		{
			return version > 0 || Prefab.SetColliderIterations;
		}
	}

	public override bool SetVelocityIterations
	{
		get
		{
			return version > 0 || Prefab.SetVelocityIterations;
		}
	}

	public override Vector3 GetCenter()
	{
		return base.transform.position;
	}

	public override void StartPhysics(bool isKinematic)
	{
		if (isSimulating)
		{
			SetMesh(VisualController.selectedSkin);
		}
		if (!isKinematic)
		{
			SetNonJoining();
		}
	}

	protected void SetMesh(BlockSkinLoader.SkinPack.Skin skin)
	{
		if (!isSimulating)
		{
			return;
		}
		if (OptionsMaster.skinsEnabled && skin != null && skin.pack.id == "supporter")
		{
			if (!particles.gameObject.activeInHierarchy)
			{
				particles.gameObject.SetActive(true);
				particles.localPosition = Vector3.zero;
				particles.localRotation = Quaternion.identity;
				particles.parent = base.transform;
			}
		}
		else if (particles.parent == base.transform)
		{
			particles.gameObject.SetActive(false);
			particles.parent = base.transform.parent;
		}
	}

	public override void OnSave(XDataHolder data)
	{
		data.Write("bmt-version", version);
		base.OnSave(data);
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
	}
}
