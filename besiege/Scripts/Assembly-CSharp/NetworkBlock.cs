using System;
using System.Collections.Generic;
using UnityEngine;

public class NetworkBlock : NetworkEntity
{
	public enum BlockState
	{
		BaseBlock = 1,
		Broken = 2,
		Burned = 4,
		Frozen = 8,
		Exploded = 0x20,
		Killed = 0x40
	}

	public static bool applyingState;

	private static float jointDamageValue = 4f;

	[HideInInspector]
	public int BreakCount;

	public bool hasBase;

	public NetworkEntity baseEntity;

	public NetworkBlock[] children = new NetworkBlock[0];

	[HideInInspector]
	public bool isBlock;

	[HideInInspector]
	public BlockBehaviour blockBehaviour;

	[NonSerialized]
	[HideInInspector]
	public Quaternion transformRotation;

	[HideInInspector]
	public bool changedPos;

	[HideInInspector]
	public bool changedRot;

	public bool hasCogMotorDamage;

	public CogMotorDamage cogMotorDamage;

	public WheelSmoke wheelSmoke;

	public bool hasFireController;

	public FireController fireController;

	public FireTag fireTag;

	public IceTag iceTag;

	public bool hasWheelSmoke;

	public bool isDestroyed;

	protected byte blockState;

	public bool isBaseBlock;

	public Transform baseBlock;

	public NetworkBlock baseNetworkBlock;

	protected Vector3 position;

	protected Quaternion rotation;

	protected bool _hasCollider;

	protected bool hasChildManager;

	protected EntityChildManager childManager;

	private Vector3 basePos;

	private float BASE_THRESHOLD = 1f;

	private float totalDamageAdded;

	private bool smokeActive;

	private int skipFrames;

	private int pollFrame;

	private bool posActive;

	private bool rotActive;

	private Matrix4x4 transformMatrix;

	private ServerMachine serverMachine;

	private bool isClusterBase;

	private int baseCurrentFrame;

	private bool isBreakingOff;

	private float baseThreshold = 1.5f;

	private float baseSqrThreshold;

	private float baseDist;

	private bool partOfCluster;

	private int lastSentPos;

	private int lastSentRot;

	public Vector3 Position
	{
		get
		{
			return posTracker.lastVec;
		}
	}

	public Quaternion Rotation
	{
		get
		{
			return rotTracker.lastRot;
		}
	}

	public Vector3 Velocity
	{
		get
		{
			return (!isBaseBlock) ? baseNetworkBlock.posTracker.NormalizedDeltaVector : posTracker.NormalizedDeltaVector;
		}
	}

	public Vector3 AngularVelocity
	{
		get
		{
			return (!isBaseBlock) ? baseNetworkBlock.rotTracker.AngularVelocity : rotTracker.AngularVelocity;
		}
	}

	public float AngularVelocityMag
	{
		get
		{
			return (!isBaseBlock) ? baseNetworkBlock.rotTracker.AngularVelocityMag : rotTracker.AngularVelocityMag;
		}
	}

	public override bool IsChanged
	{
		get
		{
			return (!isBaseBlock) ? hasChangedState : (hasChangedPos || hasChangedRot || hasChangedState);
		}
	}

	public override void UpdateBaseInterval()
	{
		if (isBaseBlock)
		{
			base.UpdateBaseInterval();
		}
		else
		{
			baseInterval = NetworkScene.ServerSettings.sendRate * (float)(NetworkScene.ServerSettings.skipChildCount + 1);
		}
		if (isInitialized && !isTracking)
		{
			posTracker.OverrideInterval(baseInterval);
			rotTracker.OverrideInterval(baseInterval);
		}
	}

	public virtual void FetchComponents()
	{
		fireTag = blockBehaviour.fireTag;
		iceTag = blockBehaviour.iceTag;
		BlockPrefab prefab = blockBehaviour.Prefab;
		if (prefab.canBurn && fireTag != null && fireTag.hasController)
		{
			fireController = fireTag.fireControllerCode;
			hasFireController = true;
		}
		switch (prefab.Type)
		{
		case BlockType.Wheel:
		case BlockType.LargeWheel:
		case BlockType.LargeWheelUnpowered:
			wheelSmoke = GetComponent<WheelSmoke>();
			hasWheelSmoke = true;
			break;
		case BlockType.Flamethrower:
			fireController = (blockBehaviour as FlamethrowerController).fireController;
			hasFireController = true;
			break;
		case BlockType.Pin:
			pollTransform = false;
			break;
		case BlockType.CircularSaw:
		case BlockType.Drill:
			hasCogMotorDamage = true;
			cogMotorDamage = GetComponent<CogMotorDamage>();
			break;
		}
	}

	public override void UpdateTransforms()
	{
		if (!isBlock || !blockBehaviour.isSimulating)
		{
			base.UpdateTransforms();
		}
	}

	public virtual void Init(uint blockIdentifier, NetworkController controller, Transform baseEnt, bool track)
	{
		AwakeBase();
		baseBlock = baseEnt;
		isBaseBlock = baseBlock == base.transform;
		isClusterBase = isBaseBlock;
		isEssential = isBaseBlock;
		hasChildManager = false;
		BreakCount = 0;
		UpdateBaseInterval();
		if (!isBaseBlock)
		{
			baseNetworkBlock = baseBlock.GetComponent<NetworkBlock>();
		}
		Init(blockIdentifier, controller, track);
		ResetTransformData();
		if (isBlock)
		{
			if (blockBehaviour.HasParentMachine)
			{
				serverMachine = blockBehaviour.ParentMachine as ServerMachine;
			}
			switch (blockBehaviour.Prefab.Type)
			{
			case BlockType.DoubleWoodenBlock:
			case BlockType.SingleWoodenBlock:
			case BlockType.WoodenPole:
			case BlockType.Log:
			{
				FragmentVisualController obj = blockBehaviour.VisualController as FragmentVisualController;
				obj.onVisualBreak = (Action)Delegate.Combine(obj.onVisualBreak, new Action(OnVisualBreak));
				break;
			}
			case BlockType.Boulder:
			{
				BreakOnForceBoulder component = GetComponent<BreakOnForceBoulder>();
				if ((bool)component.BreakInto)
				{
					childManager = new EntityChildManager(this, controller, isTracking);
					hasChildManager = true;
					NetworkBlock component2 = component.BreakInto.GetComponent<NetworkBlock>();
					BreakCount = component2.children.Length;
				}
				break;
			}
			case BlockType.BuildSurface:
				fireTag = blockBehaviour.GetComponent<FireTag>();
				hasFireController = fireTag != null;
				fireController = ((!hasFireController) ? null : fireTag.fireControllerCode);
				break;
			case BlockType.Spring:
			case BlockType.RopeWinch:
			case BlockType.RopeMeasure:
				SetupSpring(blockBehaviour as GenericDraggedBlock);
				break;
			case BlockType.Brace:
			{
				if (blockBehaviour.stripped)
				{
					break;
				}
				BraceCode braceCode = blockBehaviour as BraceCode;
				if (BraceCode.BraceType(braceCode.transform.localScale, braceCode.cylinder.localScale.y) == BraceState.Regular)
				{
					SetupBrace(braceCode);
					posTracker.SetData(baseInterval, GetPos(position));
					rotTracker.SetData(baseInterval, GetRot(rotation));
					if (!isBaseBlock)
					{
						basePos = posTracker.lastVec;
						break;
					}
					position = myTransform.position;
					rotation = myTransform.rotation;
					transformRotation = Quaternion.Inverse(rotation);
					transformMatrix = myTransform.worldToLocalMatrix;
				}
				break;
			}
			}
			if (!serverMachine.SimPhysics)
			{
				SetupClientBlock();
			}
			else
			{
				if (!isBaseBlock)
				{
					baseDist = (baseNetworkBlock.position - trackTransform.position).sqrMagnitude;
					baseSqrThreshold = baseDist * baseThreshold;
					partOfCluster = true;
				}
				else
				{
					partOfCluster = false;
				}
				serverMachine.RegisterIntact(blockBehaviour);
			}
		}
		if (track && pollTransform)
		{
			pollFrame = (int)(blockIdentifier % (NetworkScene.ServerSettings.skipChildCount + 1));
		}
	}

	private BlockBehaviour GetConnectedBlock(bool isOwn, bool isDynamic, bool dynamicToggle)
	{
		List<BlockLink> blockNeighbours = serverMachine.GetBlockNeighbours(blockBehaviour.NodeIndex);
		if (blockNeighbours != null)
		{
			for (int i = 0; i < blockNeighbours.Count; i++)
			{
				BlockLink blockLink = blockNeighbours[i];
				for (int j = 0; j < blockLink.Triggers.Count; j++)
				{
					BlockTrigger blockTrigger = blockLink.Triggers[j];
					if (blockTrigger.isOwnLink == isOwn && (!dynamicToggle || blockTrigger.isDynamic == isDynamic))
					{
						return serverMachine.GetSimBlock(blockLink.Other.Block);
					}
				}
			}
		}
		return null;
	}

	public virtual void BreakIntoChildren(Transform breakInstance)
	{
		NetworkBlock component = breakInstance.GetComponent<NetworkBlock>();
		childManager.InitBlockChildren(component);
	}

	public void SetupBrace(BraceCode brace)
	{
		Vector3 vector = myTransform.position;
		Quaternion quaternion = myTransform.rotation;
		Vector3 vector2 = (position = brace.cylinder.position);
		Quaternion quaternion2 = (rotation = brace.cylinder.rotation);
		if (isTracking)
		{
			trackTransform = brace.cylinder;
			return;
		}
		Vector3 vector3 = brace.startPoint.position;
		Quaternion quaternion3 = brace.startPoint.rotation;
		Vector3 vector4 = brace.endPoint.position;
		Quaternion quaternion4 = brace.endPoint.rotation;
		Vector3 vector5 = brace.startPoint.TransformVector(brace.startPoint.localScale);
		Vector3 vector6 = brace.endPoint.TransformVector(brace.endPoint.localScale);
		myTransform.position = vector2;
		myTransform.rotation = quaternion2;
		myTransform.localScale = Vector3.one;
		brace.cylinder.localPosition = Vector3.zero;
		brace.cylinder.localRotation = Quaternion.identity;
		brace.startPoint.position = vector3;
		brace.endPoint.position = vector4;
		brace.startPoint.rotation = quaternion3;
		brace.endPoint.rotation = quaternion4;
		brace.startPoint.localScale = brace.startPoint.InverseTransformVector(vector5);
		brace.endPoint.localScale = brace.endPoint.InverseTransformVector(vector6);
		brace.posOffset = myTransform.InverseTransformPoint(vector);
		brace.rotOffset = Quaternion.Inverse(quaternion) * quaternion2;
		brace.rotInvOffset = Quaternion.Inverse(brace.rotOffset);
	}

	private void SetupSpring(GenericDraggedBlock dragged)
	{
		Transform startPoint = dragged.startPoint;
		Transform endPoint = dragged.endPoint;
		List<BlockLink> blockNeighbours = serverMachine.GetBlockNeighbours(blockBehaviour.NodeIndex);
		bool flag = false;
		bool flag2 = false;
		BlockBehaviour block = null;
		BlockBehaviour block2 = null;
		for (int i = 0; i < blockNeighbours.Count; i++)
		{
			BlockLink blockLink = blockNeighbours[i];
			foreach (BlockTrigger trigger in blockLink.Triggers)
			{
				if (trigger.isOwnLink)
				{
					if (trigger.isDynamic)
					{
						flag = true;
						block = blockLink.Other.Block;
					}
					else
					{
						flag2 = true;
						block2 = blockLink.Other.Block;
					}
				}
			}
		}
		bool flag3 = false;
		bool flag4 = false;
		if (isTracking)
		{
			if (flag && flag2)
			{
				pollTransform = false;
			}
			else if (!flag && flag2)
			{
				trackTransform = endPoint;
				flag3 = true;
			}
			else
			{
				trackTransform = startPoint;
				flag4 = true;
			}
		}
		else
		{
			if (flag)
			{
				BlockBehaviour simBlock = serverMachine.GetSimBlock(block);
				endPoint.SetParent(simBlock.transform, true);
			}
			if (flag2)
			{
				BlockBehaviour simBlock2 = serverMachine.GetSimBlock(block2);
				startPoint.SetParent(simBlock2.transform, true);
			}
			if (!flag && flag2)
			{
				flag3 = true;
			}
			else if (flag && !flag2)
			{
				flag4 = true;
			}
			else if (!flag && !flag2)
			{
				flag4 = true;
			}
		}
		if (flag4 || flag3)
		{
			Vector3 pos;
			Quaternion rot;
			if (isTracking)
			{
				pos = (position = trackTransform.position);
				rot = (rotation = trackTransform.rotation);
			}
			else
			{
				if (!flag2)
				{
					startPoint.SetParent(base.transform.parent, true);
				}
				if (!flag)
				{
					endPoint.SetParent(base.transform.parent, true);
				}
				pos = ((!flag4) ? endPoint.position : startPoint.position);
				rot = ((!flag4) ? endPoint.rotation : startPoint.rotation);
				myTransform.position = pos;
				myTransform.rotation = rot;
				if (!flag2)
				{
					startPoint.SetParent(myTransform, true);
				}
				if (!flag)
				{
					endPoint.SetParent(myTransform, true);
				}
			}
			posTracker.SetData(baseInterval, GetPos(pos));
			rotTracker.SetData(baseInterval, GetRot(rot));
			ResetTransformData();
		}
		if (!blockBehaviour.SimPhysics && dragged.parentVis)
		{
			if (flag2)
			{
				BlockBehaviour simBlock3 = serverMachine.GetSimBlock(block2);
				simBlock3.CreateSimLists();
				simBlock3.visAddedToMe.Add(dragged.startVis);
			}
			if (flag)
			{
				BlockBehaviour simBlock4 = serverMachine.GetSimBlock(block);
				simBlock4.CreateSimLists();
				simBlock4.visAddedToMe.Add(dragged.endVis);
			}
		}
	}

	private void ResetTransformData()
	{
		if (!isBaseBlock)
		{
			basePos = posTracker.lastVec;
			return;
		}
		position = myTransform.position;
		rotation = myTransform.rotation;
		transformRotation = Quaternion.Inverse(rotation);
		transformMatrix = myTransform.worldToLocalMatrix;
	}

	public void SetupClientBlock()
	{
		BlockType type = blockBehaviour.Prefab.Type;
		bool flag = blockBehaviour.Prefab.hasBVC && blockBehaviour.Prefab.hasFragment;
		if (type == BlockType.BuildSurface)
		{
			BuildSurface buildSurface = blockBehaviour as BuildSurface;
			if (!buildSurface.isValid)
			{
				return;
			}
			Collider[] componentsInChildren = buildSurface.simColliderParent.GetComponentsInChildren<Collider>();
			int i;
			for (i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].enabled = false;
			}
			for (i = 0; i < buildSurface.nodes.Length; i++)
			{
				if (!(buildSurface.nodes[i] == null))
				{
					GameObject gameObject = buildSurface.nodes[i].gameObject;
					if (gameObject.activeInHierarchy)
					{
						gameObject.SetActive(false);
					}
				}
			}
			for (i = 0; i < buildSurface.edges.Length; i++)
			{
				if (!(buildSurface.edges[i] == null))
				{
					GameObject gameObject2 = buildSurface.edges[i].gameObject;
					if (gameObject2.activeInHierarchy)
					{
						gameObject2.SetActive(false);
					}
				}
			}
			Dictionary<int, List<BlockBehaviour>> jointDict = new Dictionary<int, List<BlockBehaviour>>();
			List<BlockLink> blockNeighbours = serverMachine.GetBlockNeighbours(blockBehaviour.NodeIndex);
			blockNeighbours.ForEach(delegate(BlockLink x)
			{
				BlockBehaviour block = x.Other.Block;
				for (i = 0; i < x.Triggers.Count; i++)
				{
					BlockTrigger blockTrigger = x.Triggers[i];
					if (blockTrigger.isOwnLink)
					{
						List<BlockBehaviour> value = new List<BlockBehaviour>();
						if (!jointDict.TryGetValue(blockTrigger.Index, out value))
						{
							value = new List<BlockBehaviour>();
							jointDict.Add(blockTrigger.Index, value);
						}
						value.Add(block.SimBlock);
					}
				}
			});
			serverMachine.AddPendingSurfaceConnection(buildSurface, jointDict);
			return;
		}
		if (!flag)
		{
			switch (type)
			{
			case BlockType.Balloon:
				break;
			case BlockType.SmallWheel:
			{
				BlockBehaviour connectedBlock = GetConnectedBlock(true, false, false);
				if (!object.ReferenceEquals(connectedBlock, null))
				{
					(blockBehaviour as SmallWheel).SetVisualConnection(connectedBlock.transform);
				}
				return;
			}
			default:
				return;
			}
		}
		BlockBehaviour connectedBlock2 = GetConnectedBlock(true, false, false);
		if (!object.ReferenceEquals(connectedBlock2, null))
		{
			if (flag)
			{
				FilterRendererPair filterRendererPair = (blockBehaviour.VisualController as FragmentVisualController).brokenVis[0];
				Renderer renderer = filterRendererPair.renderer;
				renderer.transform.parent = connectedBlock2.transform;
				connectedBlock2.visAddedToMe.Add(renderer);
				filterRendererPair.active = false;
			}
			else
			{
				(blockBehaviour as BalloonController).endPoint.parent = connectedBlock2.transform;
			}
		}
	}

	public static void StripTransform(Transform obj)
	{
		ConstantForce[] componentsInChildren = obj.GetComponentsInChildren<ConstantForce>(true);
		ConstantForce[] array = componentsInChildren;
		foreach (ConstantForce obj2 in array)
		{
			UnityEngine.Object.Destroy(obj2);
		}
		Joint[] componentsInChildren2 = obj.GetComponentsInChildren<Joint>(true);
		Joint[] array2 = componentsInChildren2;
		foreach (Joint obj3 in array2)
		{
			UnityEngine.Object.Destroy(obj3);
		}
		Rigidbody[] componentsInChildren3 = obj.GetComponentsInChildren<Rigidbody>(true);
		Rigidbody[] array3 = componentsInChildren3;
		foreach (Rigidbody rigidbody in array3)
		{
			if (rigidbody.isKinematic || !rigidbody.GetComponent<TimedObjectDestructor>() || (bool)rigidbody.GetComponent<NetworkBlock>())
			{
				UnityEngine.Object.Destroy(rigidbody);
			}
		}
	}

	public override bool UpdateEntity(float delta)
	{
		changedPos = false;
		changedRot = false;
		if (StatMaster.isHeadless)
		{
			if (posTracker.isActive)
			{
				posTracker.Update(delta);
				changedPos = true;
			}
			if (rotTracker.isActive)
			{
				rotTracker.Update(delta);
				changedRot = true;
			}
			if (isClusterBase && (changedPos || changedRot))
			{
				if (changedPos)
				{
					position = posTracker.Vector;
				}
				if (changedRot)
				{
					rotation = rotTracker.Rotation;
				}
				transformMatrix = myTransform.localToWorldMatrix;
			}
		}
		else if (isClusterBase)
		{
			if (posTracker.isActive)
			{
				posTracker.Update(delta);
				position = posTracker.Vector;
				trackTransform.position = posTracker.Vector;
				changedPos = true;
			}
			if (rotTracker.isActive)
			{
				rotTracker.Update(delta);
				rotation = rotTracker.Rotation;
				trackTransform.rotation = rotation;
				changedRot = true;
			}
			if (changedPos || changedRot)
			{
				transformMatrix = myTransform.localToWorldMatrix;
			}
		}
		else if (isBaseBlock)
		{
			if (posTracker.isActive)
			{
				posTracker.Update(delta);
				trackTransform.position = posTracker.Vector;
			}
			if (rotTracker.isActive)
			{
				rotTracker.Update(delta);
				trackTransform.rotation = rotTracker.Rotation;
			}
		}
		else
		{
			if (posTracker.isActive)
			{
				posTracker.Update(delta);
				posActive = true;
			}
			if (rotTracker.isActive)
			{
				rotTracker.Update(delta);
				rotActive = true;
			}
			if (posActive)
			{
				trackTransform.position = baseNetworkBlock.transformMatrix.MultiplyPoint3x4(posTracker.Vector);
			}
			if (rotActive)
			{
				trackTransform.localRotation = rotTracker.Rotation;
			}
		}
		return true;
	}

	public override int GetDataSize()
	{
		return NetworkEntity.GetDataSize((isBaseBlock ? ((hasChangedPos ? 1 : 0) | (hasChangedRot ? 2 : 0)) : 0) | (hasChangedState ? 4 : 0));
	}

	public override int EncodeState(byte[] buffer, int offset)
	{
		int num = offset;
		if (isBaseBlock)
		{
			buffer[offset] = (byte)((hasChangedPos ? 1 : 0) | (hasChangedRot ? 2 : 0) | (hasChangedState ? 4 : 0));
			offset++;
			if (hasChangedState)
			{
				buffer[offset] = blockState;
				offset++;
			}
			if (hasChangedPos)
			{
				NetworkCompression.CompressPosition(posTracker.lastVec, buffer, offset);
				offset += 6;
			}
			if (hasChangedRot)
			{
				NetworkCompression.CompressRotation(rotTracker.lastRot, buffer, offset);
				offset += 7;
			}
		}
		else
		{
			buffer[offset] = (byte)(hasChangedState ? 4u : 0u);
			offset++;
			if (hasChangedState)
			{
				buffer[offset] = blockState;
				offset++;
			}
		}
		return offset - num;
	}

	public override int DecodeState(byte[] data, int offset)
	{
		int num = offset;
		int changed = data[offset];
		offset++;
		if (NetworkEntity.StateChanged(changed))
		{
			ApplyState(data[offset]);
			offset++;
		}
		changedPos = (changedRot = false);
		bool flag = false;
		if (NetworkEntity.PosChanged(changed))
		{
			NetworkCompression.DecompressPosition(data, offset, out posHolder);
			position = GetPos(posHolder);
			posTracker.Override(position, position);
			flag = true;
			offset += 6;
			trackTransform.position = position;
		}
		if (NetworkEntity.RotChanged(changed))
		{
			NetworkCompression.DecompressRotation(data, offset, out rotHolder);
			rotation = GetRot(rotHolder);
			rotTracker.Override(rotation, rotation);
			trackTransform.rotation = rotation;
			flag = true;
			offset += 7;
		}
		if (isClusterBase && flag)
		{
			transformMatrix = myTransform.localToWorldMatrix;
		}
		return offset - num;
	}

	protected virtual void ApplyState(byte state)
	{
		applyingState = true;
		if (ContainsState(state, BlockState.BaseBlock))
		{
			SetEvent(0u, EntityEvent.Base);
		}
		if (ContainsState(state, BlockState.Burned))
		{
			SetEvent(0u, EntityEvent.Ignite, 0);
		}
		if (ContainsState(state, BlockState.Killed))
		{
			SetEvent(0u, EntityEvent.Kill);
		}
		if (ContainsState(state, BlockState.Broken))
		{
			SetEvent(0u, EntityEvent.Break);
		}
		if (ContainsState(state, BlockState.Frozen))
		{
			SetEvent(0u, EntityEvent.Freeze);
		}
		if (ContainsState(state, BlockState.Exploded))
		{
			SetEvent(0u, EntityEvent.Explode);
		}
		applyingState = false;
	}

	public bool ContainsState(byte state, BlockState blockState)
	{
		byte b = (byte)blockState;
		return (state & b) != 0;
	}

	public void RegisterJointDamage(ConfigurableJoint[] configJoints, HingeJoint[] hingeJoints)
	{
		int num = 0;
		for (num = 0; num < configJoints.Length; num++)
		{
			if (configJoints[num].connectedBody != null)
			{
				serverMachine.DamageController.AddTotalDamage(jointDamageValue);
				totalDamageAdded += jointDamageValue;
			}
		}
		for (num = 0; num < hingeJoints.Length; num++)
		{
			if (hingeJoints[num].connectedBody != null)
			{
				serverMachine.DamageController.AddTotalDamage(jointDamageValue);
				totalDamageAdded += jointDamageValue;
			}
		}
	}

	protected virtual void OnDestroy()
	{
		isDestroyed = true;
		if (isTracking && isBlock)
		{
			serverMachine.DamageController.RemoveTotalDamage(totalDamageAdded);
		}
	}

	public override int PollObject(bool fullUpdate, byte[] data, int offset)
	{
		int num = offset;
		offset++;
		if (isDestroyed || !pollTransform)
		{
			int eventCount = sendEntity.eventCount;
			if (eventCount > 0)
			{
				data[num] = (byte)(eventCount << 3);
				Buffer.BlockCopy(sendEntity.EventList, 0, data, offset, eventCount);
				sendEntity.eventCount = 0;
			}
			return offset - num + eventCount;
		}
		bool flag = false;
		Vector3 vector;
		Quaternion rot;
		if (isBaseBlock)
		{
			position = trackTransform.position;
			vector = NetworkEntity.ClampPosition(position);
			rotation = trackTransform.rotation;
			rot = rotation;
			if (isClusterBase)
			{
				transformRotation = Quaternion.Inverse(rotation);
				transformMatrix = trackTransform.worldToLocalMatrix;
			}
			if (partOfCluster && blockBehaviour.isIntact)
			{
				float sqrMagnitude = (baseNetworkBlock.position - position).sqrMagnitude;
				if (sqrMagnitude > baseSqrThreshold)
				{
					serverMachine.ApplyDamage(blockBehaviour, MachineDamageType.ClusterLeave);
					partOfCluster = false;
				}
			}
		}
		else
		{
			if (SkipFrame())
			{
				int eventCount = sendEntity.eventCount;
				if (eventCount > 0)
				{
					data[num] = (byte)(eventCount << 3);
					Buffer.BlockCopy(sendEntity.EventList, 0, data, offset, eventCount);
					sendEntity.eventCount = 0;
				}
				return offset - num + eventCount;
			}
			position = trackTransform.position;
			vector = NetworkEntity.ClampPosition(baseNetworkBlock.transformMatrix.MultiplyPoint3x4(position));
			if (!fullUpdate)
			{
				float num2 = vector.x - basePos.x;
				float num3 = vector.y - basePos.y;
				float num4 = vector.z - basePos.z;
				float num5 = ((!(num2 < 0f)) ? num2 : (0f - num2)) + ((!(num3 < 0f)) ? num3 : (0f - num3)) + ((!(num4 < 0f)) ? num4 : (0f - num4));
				if (!(num5 > BASE_THRESHOLD))
				{
					int eventCount = sendEntity.eventCount;
					if (eventCount > 0)
					{
						data[num] = (byte)(eventCount << 3);
						Buffer.BlockCopy(sendEntity.EventList, 0, data, offset, eventCount);
						sendEntity.eventCount = 0;
					}
					return offset - num + eventCount;
				}
				BreakOff(0u);
				flag = true;
				vector = NetworkEntity.ClampPosition(position);
				rotation = trackTransform.rotation;
				rot = rotation;
			}
			else
			{
				rotation = trackTransform.rotation;
				rot = baseNetworkBlock.transformRotation * rotation;
			}
		}
		int num6 = 0;
		if (hasWheelSmoke && smokeActive != wheelSmoke.smokeActive)
		{
			smokeActive = wheelSmoke.smokeActive;
			Event(EntityEvent.ToggleSmoke, (byte)(smokeActive ? 1u : 0u));
		}
		if (hasCogMotorDamage && cogMotorDamage.hasEmitted)
		{
			float num7;
			if (blockBehaviour.Prefab.Type == BlockType.CircularSaw)
			{
				num7 = ((!(cogMotorDamage.eulerX < 0f)) ? cogMotorDamage.eulerX : (360f + cogMotorDamage.eulerX));
				num7 = num7 / 360f * 255f;
			}
			else
			{
				num7 = Mathf.Clamp(cogMotorDamage.drillDistance * 100f, 0f, 255f);
			}
			Event(EntityEvent.EmitSparks, (byte)num7);
			cogMotorDamage.hasEmitted = false;
		}
		bool flag2 = false;
		bool flag3 = ++lastSentPos > OptionsMaster.resendTransformFrames;
		if (flag || flag3 || !posTracker.WithinThreshold(vector))
		{
			if (!isBaseBlock && fullUpdate)
			{
				float num2 = vector.x - basePos.x;
				float num3 = vector.y - basePos.y;
				float num4 = vector.z - basePos.z;
				float num5 = ((!(num2 < 0f)) ? num2 : (0f - num2)) + ((!(num3 < 0f)) ? num3 : (0f - num3)) + ((!(num4 < 0f)) ? num4 : (0f - num4));
				if (num5 > BASE_THRESHOLD)
				{
					BreakOff(0u);
					flag = true;
					vector = NetworkEntity.ClampPosition(position);
					rot = rotation;
				}
			}
			if (!flag && isBreakingOff)
			{
				if (baseCurrentFrame < OptionsMaster.baseEventFrames)
				{
					Event(EntityEvent.Base);
					baseCurrentFrame++;
				}
				else
				{
					isBreakingOff = false;
				}
			}
			int eventCount = sendEntity.eventCount;
			if (eventCount > 0)
			{
				Buffer.BlockCopy(sendEntity.EventList, 0, data, offset, eventCount);
				sendEntity.eventCount = 0;
				offset += eventCount;
				num6 |= eventCount << 3;
			}
			flag2 = true;
			lastSentPos = 0;
			NetworkCompression.CompressPosition(vector, data, offset);
			offset += 6;
			num6 |= 1;
			posTracker.Store(vector);
			hasChangedPos = true;
		}
		bool flag4 = ++lastSentRot > OptionsMaster.resendTransformFrames;
		if (flag || flag4 || !rotTracker.WithinThreshold(rot))
		{
			if (!flag2)
			{
				int eventCount = sendEntity.eventCount;
				if (eventCount > 0)
				{
					Buffer.BlockCopy(sendEntity.EventList, 0, data, offset, eventCount);
					sendEntity.eventCount = 0;
					offset += eventCount;
					num6 |= eventCount << 3;
				}
				flag2 = true;
			}
			lastSentRot = 0;
			NetworkCompression.CompressRotation(rot, data, offset);
			offset += 7;
			num6 |= 4;
			rotTracker.Store(rot);
			hasChangedRot = true;
		}
		if (!flag2)
		{
			int eventCount = sendEntity.eventCount;
			if (eventCount > 0)
			{
				Buffer.BlockCopy(sendEntity.EventList, 0, data, offset, eventCount);
				sendEntity.eventCount = 0;
				offset += eventCount;
				num6 |= eventCount << 3;
			}
		}
		data[num] = (byte)num6;
		if (turningOff)
		{
			pollTransform = false;
			turningOff = false;
		}
		return offset - num;
	}

	public int DataLength(EntityEvent evt)
	{
		switch (evt)
		{
		case EntityEvent.Ignite:
		case EntityEvent.ToggleSmoke:
		case EntityEvent.SoundOnCollide:
		case EntityEvent.SetDamageLevel:
		case EntityEvent.ChangeMesh:
		case EntityEvent.AIKilled:
		case EntityEvent.AttackHitParticles:
		case EntityEvent.RSCPlay2:
		case EntityEvent.Fade:
		case EntityEvent.EmitSparks:
		case EntityEvent.ToggleVacuum:
		case EntityEvent.PlayGrabSound:
		case EntityEvent.SurfaceFragmentBreak:
		case EntityEvent.WaterSplash:
			return 1;
		case EntityEvent.ParentToBlock:
			return 1;
		default:
			return 0;
		}
	}

	public override void SetData(uint frame, byte[] data, int offset, bool hasPos, bool hasRot, int eventCount)
	{
		offset++;
		int num = offset;
		int num2;
		for (num2 = 0; num2 < eventCount; num2++)
		{
			EntityEvent entityEvent = (EntityEvent)data[num + num2];
			if (entityEvent == EntityEvent.Base)
			{
				SetEvent(frame, entityEvent);
			}
			num2 += DataLength(entityEvent);
		}
		offset += eventCount;
		if (hasPos)
		{
			if (frame >= lastPosFrame)
			{
				NetworkCompression.DecompressPosition(data, offset, out posHolder);
				if (!isBaseBlock && frame - lastPosFrame >= OptionsMaster.resendTransformFrames)
				{
					float num3 = posHolder.x - basePos.x;
					float num4 = posHolder.y - basePos.y;
					float num5 = posHolder.z - basePos.z;
					float num6 = ((!(num3 < 0f)) ? num3 : (0f - num3)) + ((!(num4 < 0f)) ? num4 : (0f - num4)) + ((!(num5 < 0f)) ? num5 : (0f - num5));
					if (num6 > BASE_THRESHOLD)
					{
						SetEvent(frame, EntityEvent.Base);
					}
				}
				posTracker.Set(posHolder);
				hasChangedPos = true;
				lastPosFrame = frame;
			}
			offset += 6;
		}
		if (hasRot)
		{
			if (frame >= lastRotFrame)
			{
				NetworkCompression.DecompressRotation(data, offset, out rotHolder);
				rotTracker.Set(rotHolder);
				hasChangedRot = true;
				lastRotFrame = frame;
			}
			offset += 7;
		}
		for (num2 = 0; num2 < eventCount; num2++)
		{
			EntityEvent entityEvent2 = (EntityEvent)data[num + num2];
			int num7 = DataLength(entityEvent2);
			if (num7 > 0)
			{
				int num8 = num + (num2 + 1);
				if (num7 == 1)
				{
					SetEvent(frame, entityEvent2, data[num8]);
				}
				else
				{
					byte[] array = new byte[num7];
					Buffer.BlockCopy(data, num8, array, 0, num7);
					SetEvent(frame, entityEvent2, array);
				}
				num2 += num7;
			}
			else if (entityEvent2 != EntityEvent.Base)
			{
				SetEvent(frame, entityEvent2);
			}
		}
	}

	protected override Vector3 GetPos(Vector3 pos)
	{
		if (!isBaseBlock)
		{
			return baseNetworkBlock.transformMatrix.MultiplyPoint3x4(pos);
		}
		return pos;
	}

	protected override Quaternion GetRot(Quaternion rot)
	{
		if (!isBaseBlock)
		{
			return baseNetworkBlock.transformRotation * rot;
		}
		return rot;
	}

	protected bool SkipFrame()
	{
		if (++skipFrames > NetworkScene.ServerSettings.skipChildCount)
		{
			skipFrames = 0;
		}
		return skipFrames != pollFrame;
	}

	public override void NewFrame(uint frame)
	{
		if (isBaseBlock || !SkipFrame())
		{
			base.NewFrame(frame);
		}
	}

	public virtual void Event(EntityEvent evt, byte[] eventData)
	{
		if (DataLength(evt) < 2)
		{
			Debug.LogWarning(string.Concat("Event ", evt, " should be a 1 byte event!"));
		}
		if (sendEntity.eventCount + 1 + eventData.Length < SendEntity.MAX_EVENTS)
		{
			sendEntity.AddEvent((byte)evt);
			for (int i = 0; i < eventData.Length; i++)
			{
				sendEntity.AddEvent(eventData[i]);
			}
		}
	}

	public virtual void Event(EntityEvent evt, byte eventData)
	{
		if (DataLength(evt) != 1)
		{
			Debug.LogWarning(string.Concat("Event ", evt, " is incompatible with single byte events!"));
		}
		int num = -1;
		if (evt == EntityEvent.Ignite)
		{
			num = 4;
		}
		if (num != -1)
		{
			isEssential = true;
			blockState |= (byte)num;
			hasChangedState = true;
		}
		if (sendEntity.eventCount + 2 < SendEntity.MAX_EVENTS)
		{
			sendEntity.AddEvent((byte)evt);
			sendEntity.AddEvent(eventData);
		}
	}

	public virtual void Event(EntityEvent evt)
	{
		int num = -1;
		switch (evt)
		{
		case EntityEvent.Base:
			num = 1;
			break;
		case EntityEvent.Break:
			num = 2;
			if (isBlock)
			{
				if (blockBehaviour.Prefab.Type == BlockType.Boulder)
				{
					BreakOnForceBoulder component = GetComponent<BreakOnForceBoulder>();
					component.Break();
					BreakIntoChildren(component.BrokenInstance);
				}
				serverMachine.ApplyDamage(blockBehaviour, MachineDamageType.Break);
			}
			break;
		case EntityEvent.Explode:
			num = 32;
			pollTransform = false;
			break;
		case EntityEvent.Kill:
			num = 64;
			break;
		case EntityEvent.Freeze:
			num = 8;
			break;
		default:
			if (evt == EntityEvent.Break || evt == EntityEvent.VisBreak)
			{
				num = 2;
				if (evt == EntityEvent.Break)
				{
					pollTransform = false;
				}
			}
			break;
		}
		if (num != -1)
		{
			isEssential = true;
			blockState |= (byte)num;
			hasChangedState = true;
		}
		if (evt == EntityEvent.Douse)
		{
			int num2 = -5;
			byte b = (byte)num2;
			blockState &= b;
		}
		if (sendEntity.eventCount + 1 < SendEntity.MAX_EVENTS)
		{
			sendEntity.AddEvent((byte)evt);
		}
	}

	protected void BreakOff(uint frame)
	{
		if (!isBaseBlock)
		{
			if (!isBaseBlock && !isTracking)
			{
				base.transform.SetParent(base.transform.parent.parent, true);
			}
			isBreakingOff = true;
			isBaseBlock = true;
			baseBlock = base.transform;
			baseNetworkBlock = this;
			blockBehaviour.ClusterIndex = -1;
			if (!isTracking)
			{
				posTracker.prevVec = ((lastPosFrame <= frame) ? baseBlock.TransformPoint(posTracker.lastVec) : posTracker.lastVec);
				posTracker.Vector = trackTransform.position;
				rotTracker.prevRot = ((lastRotFrame <= frame) ? (baseNetworkBlock.rotation * rotTracker.lastRot) : posTracker.lastRot);
				rotTracker.Rotation = trackTransform.rotation;
				UpdateBaseInterval();
			}
			else
			{
				Event(EntityEvent.Base);
			}
		}
	}

	protected void OnJointBreak(float breakForce)
	{
		if (isTracking && isBlock)
		{
			serverMachine.DamageController.ApplyJointDamage(jointDamageValue);
			serverMachine.ApplyDamage(blockBehaviour, MachineDamageType.JointBreak);
		}
	}

	private void OnVisualBreak()
	{
		if (!StatMaster.isClient && isBlock)
		{
			Event(EntityEvent.VisBreak);
			serverMachine.ApplyDamage(blockBehaviour, MachineDamageType.Break);
		}
	}

	public virtual void SetEvent(uint frame, EntityEvent evt, byte[] data)
	{
	}

	public virtual void SetEvent(uint frame, EntityEvent evt, byte data)
	{
		switch (evt)
		{
		case EntityEvent.Ignite:
			if (isBlock && blockBehaviour.Prefab.Type == BlockType.BuildSurface && (blockBehaviour as BuildSurface).FragmentController != null && (blockBehaviour as BuildSurface).FragmentController.FragmentsActivated)
			{
				break;
			}
			if (isBlock && blockBehaviour.Prefab.Type == BlockType.Rocket)
			{
				if (base.gameObject.activeInHierarchy)
				{
					(blockBehaviour as TimedRocket).Fire(0f);
				}
				break;
			}
			if (fireController != null)
			{
				float fireDuration = (float)(int)data / 255f * fireController.randomAmount * 2f - fireController.randomAmount;
				fireController.SetFireDuration(fireDuration);
			}
			if (fireTag != null)
			{
				fireTag.Ignite();
			}
			break;
		case EntityEvent.SoundOnCollide:
			if (isBlock)
			{
				switch (blockBehaviour.Prefab.Type)
				{
				case BlockType.BuildSurface:
				{
					BuildSurface buildSurface = blockBehaviour as BuildSurface;
					buildSurface.PlaySound();
					break;
				}
				case BlockType.Buoyancy:
				case BlockType.BigBarrel:
				{
					BuoyancyDensityController buoyancyDensityController = blockBehaviour as BuoyancyDensityController;
					buoyancyDensityController.PlaySound();
					break;
				}
				case BlockType.MetalJaw:
				{
					SpringReleaseBlock springReleaseBlock = blockBehaviour as SpringReleaseBlock;
					springReleaseBlock.PlaySound((float)(int)data / 255f);
					break;
				}
				default:
				{
					SoundOnCollide component3 = GetComponent<SoundOnCollide>();
					if (component3 != null)
					{
						component3.PlaySound((float)(int)data / 255f);
					}
					break;
				}
				}
			}
			else
			{
				SoundOnCollide component4 = GetComponent<SoundOnCollide>();
				if (component4 != null)
				{
					component4.PlaySound((float)(int)data / 255f);
				}
			}
			break;
		case EntityEvent.SetDamageLevel:
		{
			if (isBlock)
			{
				if (blockBehaviour.Prefab.hasBVC)
				{
					float damageLevel = (float)(int)data / 255f;
					blockBehaviour.VisualController.SetDamageLevel(damageLevel);
				}
				break;
			}
			ShipPartHitManager component = GetComponent<ShipPartHitManager>();
			if (component != null)
			{
				float f = (float)(int)data / 255f;
				component.ShipHit(Mathf.CeilToInt(f));
			}
			break;
		}
		case EntityEvent.ParentToBlock:
			if (isBlock)
			{
				SkateboardWheel component2 = GetComponent<SkateboardWheel>();
				if ((bool)component2)
				{
					component2.ClientReparent(data);
				}
			}
			break;
		case EntityEvent.ToggleVacuum:
			if (isBlock && blockBehaviour.Prefab.Type == BlockType.Vacuum)
			{
				(blockBehaviour as VacuumBlock).ToggleParticles(data == 1);
			}
			break;
		case EntityEvent.SurfaceFragmentBreak:
			if (isBlock && blockBehaviour.Prefab.Type == BlockType.BuildSurface)
			{
				(blockBehaviour as BuildSurface).OnRemoteFragmentBreak(data);
			}
			break;
		case EntityEvent.ToggleSmoke:
			if (hasWheelSmoke)
			{
				wheelSmoke.ToggleSmoke(data == 1);
			}
			break;
		case EntityEvent.PlayGrabSound:
			if (isBlock && blockBehaviour.Prefab.Type == BlockType.Grabber)
			{
				(blockBehaviour as GrabberBlock).joinOnTriggerBlock.PlayGrabSound(data == 1);
			}
			break;
		case EntityEvent.RSCPlay2:
		{
			RandomSoundController componentInChildren = GetComponentInChildren<RandomSoundController>();
			if (componentInChildren != null)
			{
				float volume = (float)(int)data / 255f;
				componentInChildren.Play2(volume);
			}
			break;
		}
		case EntityEvent.EmitSparks:
			if (hasCogMotorDamage)
			{
				if (blockBehaviour.Prefab.Type == BlockType.CircularSaw)
				{
					cogMotorDamage.EmitSparksClient((float)(int)data / 255f * 360f);
				}
				else
				{
					cogMotorDamage.EmitDrillSparksClient((float)(int)data / 100f);
				}
			}
			break;
		case EntityEvent.WaterSplash:
		{
			Vector3 emitPosition = base.transform.position;
			emitPosition.y = WaterController.CheckHeightMap(emitPosition.x, emitPosition.z);
			int particleSet = data - 1;
			WaterController.currentInstance.EmitWaterParticles(emitPosition, particleSet);
			break;
		}
		}
	}

	public override void SetEvent(uint frame, EntityEvent evt)
	{
		base.SetEvent(frame, evt);
		if (StatMaster.isHosting)
		{
			Event(evt);
			sendEntity.eventCount = 0;
		}
		switch (evt)
		{
		case EntityEvent.Freeze:
			if (iceTag != null)
			{
				iceTag.Freeze();
			}
			else
			{
				Debug.LogWarning("Freezing '" + Machine.GetObjectPath(base.gameObject) + "', but no IceTag found!", base.gameObject);
			}
			break;
		case EntityEvent.IgniteBurning:
			if (fireTag != null)
			{
				fireTag.Ignite();
			}
			break;
		case EntityEvent.Kill:
			if (isBlock)
			{
				if (blockBehaviour.Prefab.Type == BlockType.Brace)
				{
					(blockBehaviour as BraceCode).RemoveBrace();
				}
				else
				{
					Debug.LogWarning(string.Concat("Trying to remove braces on block ", blockBehaviour.Prefab.Type, "!"));
				}
			}
			break;
		case EntityEvent.SetBloodyLevel:
			if (isBlock && blockBehaviour.Prefab.hasBVC)
			{
				blockBehaviour.VisualController.SetBloodyLevel(1f, StatMaster.BloodColor);
			}
			break;
		case EntityEvent.Break:
			if (isBlock)
			{
				switch (blockBehaviour.Prefab.Type)
				{
				case BlockType.Balloon:
					(blockBehaviour as BalloonController).Pop();
					break;
				case BlockType.SqrBalloon:
					(blockBehaviour as SqrBalloonController).Pop();
					break;
				case BlockType.Boulder:
				{
					BreakOnForceBoulder component = GetComponent<BreakOnForceBoulder>();
					component.Break();
					BreakIntoChildren(component.BrokenInstance);
					break;
				}
				case BlockType.Spring:
				case BlockType.RopeWinch:
					(blockBehaviour as SpringCode).Snap();
					break;
				case BlockType.RopeMeasure:
					(blockBehaviour as RopeMeasure).Snap();
					break;
				case BlockType.Sail:
					Debug.LogError("[NetworkBlock.BreakEvent] Not implemented for Sails.");
					break;
				case BlockType.BuildSurface:
					(blockBehaviour as BuildSurface).OnRemoteBreak();
					break;
				case BlockType.Harpoon:
					(blockBehaviour as HarpoonController).Snap();
					break;
				case BlockType.SmallWheel:
					(blockBehaviour as SmallWheel).Break();
					break;
				}
			}
			break;
		case EntityEvent.Douse:
			if (fireController != null)
			{
				fireController.DouseFire();
			}
			break;
		case EntityEvent.Base:
			BreakOff(frame);
			break;
		case EntityEvent.VisBreak:
			if (isBlock)
			{
				if (blockBehaviour.Prefab.hasFragment)
				{
					(blockBehaviour.VisualController as FragmentVisualController).OnJointBreak(0f);
				}
				else if (blockBehaviour.Prefab.Type == BlockType.Balloon)
				{
					(blockBehaviour as BalloonController).Snap();
				}
			}
			break;
		case EntityEvent.ParticleOnCollide:
		{
			ParticleOnCollide componentInChildren3 = GetComponentInChildren<ParticleOnCollide>();
			if (componentInChildren3 != null)
			{
				componentInChildren3.PlayParticles();
			}
			break;
		}
		case EntityEvent.Explode:
			if (!base.gameObject.activeInHierarchy)
			{
				break;
			}
			if (isBlock)
			{
				TimedRocket timedRocket = blockBehaviour as TimedRocket;
				if (timedRocket != null)
				{
					timedRocket.OnExplode();
				}
				ControllableBomb controllableBomb = blockBehaviour as ControllableBomb;
				if (controllableBomb != null)
				{
					controllableBomb.ExplodeMessage();
				}
				ExplodeOnCollideBlock explodeOnCollideBlock = blockBehaviour as ExplodeOnCollideBlock;
				if (explodeOnCollideBlock != null)
				{
					explodeOnCollideBlock.Explodey();
				}
			}
			else
			{
				ExplodeOnCollide componentInChildren5 = GetComponentInChildren<ExplodeOnCollide>();
				if (componentInChildren5 != null)
				{
					componentInChildren5.Explodey();
				}
				SmallExplosion componentInChildren6 = GetComponentInChildren<SmallExplosion>();
				if (componentInChildren6 != null)
				{
					componentInChildren6.StartCoroutine(componentInChildren6.Explode());
				}
			}
			break;
		case EntityEvent.RSCPlay:
		{
			RandomSoundController componentInChildren4 = GetComponentInChildren<RandomSoundController>();
			if (componentInChildren4 != null)
			{
				componentInChildren4.Play();
			}
			break;
		}
		case EntityEvent.RSCPlay3:
		{
			RandomSoundController componentInChildren2 = GetComponentInChildren<RandomSoundController>();
			if (componentInChildren2 != null)
			{
				componentInChildren2.Play3();
			}
			break;
		}
		case EntityEvent.RSCStop:
		{
			RandomSoundController componentInChildren = GetComponentInChildren<RandomSoundController>();
			if (componentInChildren != null)
			{
				componentInChildren.Stop();
			}
			break;
		}
		}
	}
}
