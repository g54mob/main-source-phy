using UnityEngine;

[AddComponentMenu("UI/Tools/Machine Ground")]
public class MachineGround : MachineTransformTool
{
	protected static MachineGround _instance;

	public float floorOffset;

	public AddPiece AddPieceCode;

	public bool clicked;

	public Renderer bgRend;

	public Material clickedMaterial;

	private AudioSource audioSource;

	private bool hasBg;

	private static LayerMask mask = 0;

	public static MachineGround Instance
	{
		get
		{
			CheckInstance();
			return _instance;
		}
		set
		{
			_instance = value;
		}
	}

	public static float GetGround(float minY, Machine machine, out bool isWater)
	{
		isWater = false;
		float num = StatMaster.Bounding.floorPos;
		if (WaterController.Exist)
		{
			float waterTransformHeight = WaterController.waterTransformHeight;
			if (minY > waterTransformHeight + 0.02f && waterTransformHeight > num)
			{
				num = waterTransformHeight;
				isWater = true;
			}
		}
		if (StatMaster.Bounding.Enabled)
		{
			float waterTransformHeight = machine.boundingBoxController.floorPos.position.y;
			if (waterTransformHeight > num)
			{
				num = waterTransformHeight;
			}
		}
		return num;
	}

	private void Awake()
	{
		mask = AddPiece.CreateLayerMask(mask, 24, 28, 29);
		_instance = this;
	}

	public static void CheckInstance()
	{
		if (_instance == null)
		{
			_instance = Object.FindObjectOfType<MachineGround>();
		}
	}

	private void Start()
	{
		hasBg = bgRend != null;
		audioSource = GetComponent<AudioSource>();
	}

	public override void OnClicked()
	{
		if (!base.enabled)
		{
			if (hasBg)
			{
				bgRend.enabled = false;
			}
			return;
		}
		startMachine = Machine.Active();
		if ((bool)startMachine && !startMachine.isSimulating && startMachine.CanModify)
		{
			hasNetworkedTransform = false;
			audioSource.Play();
			clicked = true;
			if (hasBg)
			{
				bgRend.enabled = true;
			}
		}
	}

	public override void OnClickReleased()
	{
		if (StatMaster.advancedBuilding)
		{
			BlockGroundTool instance = BlockGroundTool.instance;
			if ((bool)instance)
			{
				instance.OnClicked();
				instance.OnClickReleased();
				clicked = false;
				audioSource.Play();
				return;
			}
		}
		if (base.enabled && clicked && (bool)startMachine)
		{
			StatMaster.Mode.isTranslating = true;
			Vector3 position = startMachine.Position;
			startMachine.SetRigidInterpolation(RigidbodyInterpolation.None);
			MoveDown(startMachine);
			if (startMachine.Position != position)
			{
				SendTransformInfo(startMachine);
				startMachine.SetPosition(startMachine.Position);
				startMachine.UndoSystem.ChangePosition(position);
				SingleInstanceFindOnly<AddPiece>.Instance.UpdateMiddleOfObject();
			}
			startMachine.RestoreRigidInterpolation();
			StatMaster.Mode.isTranslating = false;
			clicked = false;
			if (hasBg)
			{
				bgRend.enabled = false;
			}
			audioSource.Play();
			AdvancedBlockEditor.Instance.UpdateGizmo();
		}
	}

	public static void MoveDown(Machine machine)
	{
		if (!machine || machine.isSimulating || !machine.CanModify)
		{
			return;
		}
		Vector3 position = machine.Position;
		Vector3 position2 = position;
		Bounds bounds = machine.GetBounds(false);
		float num = bounds.min.y;
		bool isWater;
		float num2 = GetGround(num, machine, out isWater);
		Transform transform = null;
		Vector3 vector = Vector3.down;
		bool flag = StatMaster.Bounding.Enabled;
		float num3 = 0.008f;
		if (StatMaster.isMP)
		{
			if (flag)
			{
				transform = PlayerData.localPlayer.buildZone.transform;
				position2 = transform.InverseTransformPoint(position2);
				position2.y += 5.05f;
				vector = transform.InverseTransformVector(vector);
			}
			else
			{
				Vector3[] globalBoundPoints = (machine.boundingBoxController as NetworkBoundingBoxController).GetGlobalBoundPoints(bounds);
				for (int i = 0; i < globalBoundPoints.Length; i++)
				{
					float y = globalBoundPoints[i].y;
					if (i == 0 || y < num)
					{
						num = y;
					}
				}
				num2 = StatMaster.Bounding.worldCenter.y - StatMaster.Bounding.worldExtents.y;
			}
		}
		else
		{
			float floorHeight = SingleInstanceFindOnly<AddPiece>.Instance.floorHeight;
			if (floorHeight > num2)
			{
				num2 = floorHeight;
			}
			if (!isWater)
			{
				num3 += 0.01f;
			}
		}
		if (!flag)
		{
			Transform root = Machine.Active().transform.root;
			float num4 = num - num2;
			foreach (BlockBehaviour buildingBlock in machine.BuildingBlocks)
			{
				if (buildingBlock == null || buildingBlock.myBounds == null)
				{
					continue;
				}
				BlockType type = buildingBlock.Prefab.Type;
				if (type != BlockType.Pin && type != BlockType.CameraBlock)
				{
					Bounds bounds2 = buildingBlock.myBounds.GetBounds(false);
					RaycastHit hitInfo;
					if (Physics.BoxCast(bounds2.center + Vector3.up * 0.25f, bounds2.extents, Vector3.down, out hitInfo, Quaternion.identity, 1000f, mask, QueryTriggerInteraction.Ignore) && !(hitInfo.collider.transform.root == root) && hitInfo.distance < num4)
					{
						num4 = hitInfo.distance;
						num2 = num - num4;
					}
				}
			}
		}
		if (StatMaster.isMP && flag)
		{
			float t = (vector.x + 1f) / 2f;
			float t2 = (vector.y + 1f) / 2f;
			float t3 = (vector.z + 1f) / 2f;
			bool flag2 = Mathf.Approximately(Mathf.Abs(vector.x), 1f);
			bool flag3 = Mathf.Approximately(Mathf.Abs(vector.y), 1f);
			bool flag4 = Mathf.Approximately(Mathf.Abs(vector.z), 1f);
			if (!flag2 && !flag3 && !flag4)
			{
				t2 = -1f;
				flag3 = true;
			}
			if (flag2)
			{
				position2.x -= Mathf.Lerp(bounds.min.x - StatMaster.Bounding.leftPos - num3, bounds.max.x - StatMaster.Bounding.rightPos + num3, t);
			}
			if (flag3)
			{
				position2.y -= Mathf.Lerp(bounds.min.y - num2 - num3, bounds.max.y - StatMaster.Bounding.roofHeight + num3, t2);
			}
			if (flag4)
			{
				position2.z -= Mathf.Lerp(bounds.min.z - StatMaster.Bounding.backPos - num3, bounds.max.z - StatMaster.Bounding.frontPos + num3, t3);
			}
			position2 = transform.TransformPoint(new Vector3(position2.x, position2.y - 5.05f, position2.z));
		}
		else
		{
			position2.y -= num - (num2 + num3);
		}
		machine.Position = position2;
	}
}
