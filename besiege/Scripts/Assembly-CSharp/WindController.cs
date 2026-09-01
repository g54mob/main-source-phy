using System.Collections;
using UnityEngine;

[AddComponentMenu("Physics/Wind/Shelter")]
public class WindController : ExternalForce
{
	public bool debugWind;

	public WindZone windZone;

	public float updateSpeed = 0.25f;

	public float windForce = 10f;

	public Collider WindCollider;

	[HideInInspector]
	public Vector3 windPower;

	[HideInInspector]
	public WindEntity windEntity;

	private bool setupDone;

	private float prevWindForce;

	private int blockCount;

	private Vector3 windDir;

	private Vector3 windPos;

	private Bounds windBounds;

	public bool useCollider;

	private ExternalForceObject EFO;

	private Quaternion windDirRotation = Quaternion.identity;

	public bool useCurve;

	public AnimationCurve curve;

	public float evaluationSpeed = 1f;

	private float curveTime;

	private float startWindForce;

	protected override void Start()
	{
		base.Start();
		if (base.isSimulating)
		{
			if (StatMaster.isMP && StatMaster.isClient && !StatMaster.isLocalSim)
			{
				base.enabled = false;
			}
			startWindForce = windForce;
			prevWindForce = windForce;
			if (useCollider && WindCollider != null)
			{
				windBounds = WindCollider.bounds;
			}
			Object.Destroy(WindCollider, 1f);
			windDir = ((!(base.transform.localScale.x < 0f)) ? base.transform.forward : (-base.transform.forward));
			windPos = windDir * 1000f;
			windForce = startWindForce;
			windPower = windDir * windForce * 0.5f;
			windDirRotation = fromtwovectors(windDir, Vector3.forward);
			StartCoroutine(Setup());
		}
	}

	public void UpdateBounds()
	{
		if (useCollider && windEntity.Rigidbody != null)
		{
			windBounds.center = windEntity.Rigidbody.position;
		}
		windDir = ((!(base.transform.localScale.x < 0f)) ? base.transform.forward : (-base.transform.forward));
		windDirRotation = fromtwovectors(windDir, Vector3.forward);
		windPos = windDir * 1000f;
		windPower = windDir * windForce;
		worldMatrix = base.transform.worldToLocalMatrix;
	}

	private IEnumerator Setup()
	{
		yield return null;
		blockCount = ReferenceMaster.GetAllSimulationBlocks().Count;
		int size = (int)((blockCount <= 8) ? ((float)blockCount) : ((float)blockCount * 0.3f));
		EFOArray = new ExternalForceObject[size];
		worldMatrix = base.transform.worldToLocalMatrix;
		InvokeRepeating("UpdateWindBlocks", 0f, updateSpeed);
		setupDone = true;
	}

	protected void Update()
	{
		if (!StatMaster.levelSimulating)
		{
			if (StatMaster.isMP && !windEntity.PhysicsEnabled && setupDone)
			{
				setupDone = false;
				windEntity.RestoreVisuals();
			}
		}
		else if (StatMaster.isMP && !windEntity.PhysicsEnabled)
		{
			if (!setupDone)
			{
				windEntity.UpdateVisuals();
				setupDone = true;
			}
		}
		else
		{
			if (!setupDone || !base.isSimulating)
			{
				return;
			}
			if (useCurve)
			{
				curveTime += Time.deltaTime * evaluationSpeed;
				windForce = startWindForce * curve.Evaluate(curveTime);
				windPower = windDir * windForce * 0.5f;
				if (curveTime > 1000f)
				{
					curveTime -= 1000f;
				}
			}
			if (prevWindForce != windForce && (bool)windZone)
			{
				prevWindForce = windForce;
				windZone.windMain = windForce / 75f;
			}
			CalculateForcePositions();
		}
	}

	private void FixedUpdate()
	{
		if (!base.isSimulating || !setupDone)
		{
			return;
		}
		for (int i = 0; i < ExternalForceObjectCount; i++)
		{
			EFO = EFOArray[i];
			if (object.ReferenceEquals(EFO, null))
			{
				break;
			}
			if (!EFO.basicInfo.isDestroyed && !EFO.basicInfo.noRigidbody && !EFO.waitForUpdate)
			{
				EFO.basicInfo.Rigidbody.AddForceAtPosition(EFO.force, EFO.closestPoint, EFO.forceMode);
				EFO.basicInfo.Rigidbody.AddForceAtPosition(EFO.force, EFO.furthestPoint, EFO.forceMode);
				if (debugWind)
				{
					Debug.DrawRay(EFO.closestPoint, windDir * 0.1f, Color.red, 0.25f, false);
					Debug.DrawRay(EFO.furthestPoint, windDir * 0.1f, Color.green, 0.25f, false);
				}
			}
		}
	}

	private void CalculateForcePositions()
	{
		Vector3 vector2 = default(Vector3);
		for (int i = 0; i < ExternalForceObjectCount; i++)
		{
			EFO = EFOArray[i];
			if (object.ReferenceEquals(EFO, null))
			{
				break;
			}
			if (!object.ReferenceEquals(EFO.basicInfo, null) && !EFO.basicInfo.isDestroyed && !EFO.basicInfo.noRigidbody)
			{
				EFO.basicInfo.inWind = true;
				Vector3 extents = EFO.basicInfo.DefaultBounds.extents;
				Vector3 worldCenterOfMass = EFO.basicInfo.Rigidbody.worldCenterOfMass;
				Quaternion rBRot = EFO.basicInfo.RBRot;
				Vector3 vector = Quaternion.Inverse(rBRot) * windPos;
				vector2.x = ((!(vector.x > 0f)) ? (0f - extents.x) : extents.x);
				vector2.y = ((!(vector.y > 0f)) ? (0f - extents.y) : extents.y);
				vector2.z = ((!(vector.z > 0f)) ? (0f - extents.z) : extents.z);
				EFO.closestPoint = worldCenterOfMass + rBRot * -vector2 * 0.5f;
				EFO.furthestPoint = rBRot * vector2 + worldCenterOfMass;
				vector2 = windDirRotation * (rBRot * extents);
				float num = ((!(vector2.x < 0f)) ? vector2.x : (0f - vector2.x));
				float num2 = ((!(vector2.y < 0f)) ? vector2.y : (0f - vector2.y));
				float num3 = num * num2;
				float num4 = num3 / EFO.basicInfo.MaxAreaSize;
				num4 = ((!(num4 > 1f)) ? num4 : 1f) * (1f - EFO.basicInfo.ShelterAmount);
				EFO.power = EFO.powerScale * num4;
				EFO.force = windPower * EFO.power;
				if (!EFO.basicInfo.InWater && StatMaster.aeroCoded)
				{
					EFO.basicInfo.dragScale = EFO.dragScale * (EFO.power / 2f);
				}
				if (debugWind)
				{
				}
				if (EFO.waitForUpdate)
				{
					EFO.waitForUpdate = false;
				}
			}
		}
	}

	protected override bool ValidateEFO(BasicInfo b)
	{
		if (b.isDestroyed || b.noRigidbody || b.isKinematic || b.IgnoredByWind || b.isDisabled || !b.isSimulating)
		{
			return false;
		}
		return true;
	}

	protected void UpdateWindBlocks()
	{
		if (base.isSimulating)
		{
			ExternalForceObjectCount = 0;
			for (int i = 0; i < ReferenceMaster.ExternalForceObjectsArray.Length; i++)
			{
				processBasicInfo(ReferenceMaster.ExternalForceObjectsArray[i]);
			}
			for (int i = 0; i < ReferenceMaster.ExternalForceTemp.Count; i++)
			{
				processBasicInfo(ReferenceMaster.ExternalForceTemp[i]);
			}
			CalculateForcePositions();
		}
	}

	private void processBasicInfo(BasicInfo bInfo)
	{
		if (!ValidateEFO(bInfo))
		{
			return;
		}
		if (bInfo.ShelterAmount == 1f)
		{
			ExitWind(bInfo);
			return;
		}
		if (bInfo.gotBounds && bInfo.density < 0.01f)
		{
			bInfo.IgnoredByWind = true;
			ExitWind(bInfo);
			return;
		}
		BlockBehaviour blockBehaviour = null;
		BlockType blockType = BlockType.Brace;
		if (bInfo.infoType == BasicInfo.BasicInfoType.Block)
		{
			blockBehaviour = bInfo as BlockBehaviour;
			blockType = blockBehaviour.Prefab.Type;
			if (Machine.IsDraggedBlock(blockType) || blockType == BlockType.Pin || blockType == BlockType.CameraBlock)
			{
				ExitWind(bInfo);
				return;
			}
		}
		Vector3 worldRBCenter = bInfo.WorldRBCenter;
		if (useCollider && !windBounds.Contains(worldRBCenter))
		{
			ExitWind(bInfo);
			return;
		}
		if (bInfo.infoType != BasicInfo.BasicInfoType.Block)
		{
			AddEFO(worldRBCenter, bInfo, ForceMode.Force, 1f);
			return;
		}
		if (blockType == BlockType.WoodenPole || blockType == BlockType.DoubleWoodenBlock)
		{
			ShorteningBlock shorteningBlock = blockBehaviour as ShorteningBlock;
			if (shorteningBlock.MeshRenderer.enabled)
			{
				AddEFOVelSpace(worldRBCenter, bInfo, ForceMode.Force, 2f);
				return;
			}
		}
		switch (blockType)
		{
		case BlockType.Wing:
		case BlockType.WingPanel:
			AddEFOVelSpace(worldRBCenter, bInfo, ForceMode.Force, 2f);
			break;
		case BlockType.Rocket:
			AddEFOVelSpace(worldRBCenter, bInfo, ForceMode.Force, 1f);
			break;
		default:
			CheckIfFormost(worldRBCenter, bInfo);
			break;
		}
	}

	private void ExitWind(BasicInfo bInfo)
	{
		if (bInfo.inWind)
		{
			bInfo.inWind = false;
		}
	}

	internal override void AddEFOVelSpace(Vector3 pos, BasicInfo basic, ForceMode forceMode, float powerScale)
	{
		Vector3 pos2 = worldMatrix.MultiplyPoint3x4(pos);
		base.AddEFOVelSpace(pos2, basic, forceMode, powerScale);
	}
}
