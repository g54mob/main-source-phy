using UnityEngine;

public class NetworkEntity : MonoBehaviour
{
	public enum EntityEvent
	{
		Break = 0,
		Base = 1,
		InsigniaFlash = 2,
		VisBreak = 3,
		Ignite = 4,
		IgniteBurning = 5,
		Freeze = 6,
		Douse = 7,
		Explode = 8,
		ToggleSmoke = 9,
		SoundOnCollide = 10,
		Kill = 11,
		ParticleOnCollide = 12,
		ParticleOnTrigger = 13,
		SetDamageLevel = 14,
		ChangeMesh = 15,
		DropPot = 16,
		AIKilled = 17,
		BloodBurstHit = 18,
		BloodParticle = 19,
		SetBloodyLevel = 20,
		AttackSwingParticles = 21,
		AttackHitParticles = 22,
		RSCPlay = 23,
		RSCPlay2 = 24,
		RSCPlay3 = 25,
		RSCStop = 26,
		StopDizzyParticles = 27,
		PlayDizzyParticles = 28,
		BobPlayPause = 29,
		Fade = 30,
		EmitSparks = 31,
		ToggleVacuum = 32,
		PlayGrabSound = 33,
		SurfaceFragmentBreak = 34,
		WaterSplash = 35,
		CannonParticles = 36,
		ParentToBlock = 37
	}

	[HideInInspector]
	public SendEntity sendEntity;

	[HideInInspector]
	public bool isEssential = true;

	[HideInInspector]
	public uint staticIndex;

	[HideInInspector]
	public bool turningOff;

	[HideInInspector]
	public bool addedToController;

	public static int pollCount;

	public uint id;

	public bool pollTransform = true;

	[HideInInspector]
	public Transform myTransform;

	[HideInInspector]
	public Transform trackTransform;

	protected NetworkInterpolation posTracker;

	protected NetworkInterpolation rotTracker;

	protected Transform moveTransform;

	protected bool isTracking;

	protected NetworkController networkController;

	protected bool turnOffOnDisable = true;

	protected bool manualDeactivate;

	protected uint lastPosFrame;

	protected uint lastRotFrame;

	protected bool hasChangedPos;

	protected bool hasChangedRot;

	protected bool hasChangedState;

	protected float baseInterval;

	protected bool isInitialized;

	protected bool isAwake;

	protected Vector3 posHolder;

	protected Quaternion rotHolder;

	public bool HasChangedState
	{
		get
		{
			return hasChangedState;
		}
	}

	public virtual bool IsChanged
	{
		get
		{
			return hasChangedPos || hasChangedRot || hasChangedState;
		}
	}

	protected virtual void Awake()
	{
		AwakeBase();
	}

	protected virtual void AwakeBase()
	{
		if (!isAwake)
		{
			isAwake = true;
			isTracking = false;
			isInitialized = false;
			posHolder = default(Vector3);
			rotHolder = default(Quaternion);
			sendEntity = new SendEntity(true);
			posTracker = new NetworkInterpolation();
			rotTracker = new NetworkInterpolation();
			UpdateTransforms();
			UpdateBaseInterval();
		}
	}

	public virtual void UpdateTransforms()
	{
		myTransform = (trackTransform = base.transform);
	}

	public virtual void UpdateBaseInterval()
	{
		baseInterval = NetworkScene.ServerSettings.sendRate;
	}

	public virtual void SetTrackTransform(Transform t)
	{
		trackTransform = t;
	}

	public virtual void Init(uint identifier, NetworkController controller, bool track)
	{
		AwakeBase();
		sendEntity.id = identifier;
		id = identifier;
		isTracking = track;
		networkController = controller;
		ResetEntity();
		hasChangedPos = (hasChangedRot = (hasChangedState = false));
		isInitialized = true;
	}

	public static Vector3 ClampPosition(Vector3 pos)
	{
		float x = ((pos.x < NetworkCompression.wMinX) ? NetworkCompression.wMinX : ((!(pos.x > NetworkCompression.wMaxX)) ? pos.x : NetworkCompression.wMaxX));
		float y = ((pos.y < NetworkCompression.wMinY) ? NetworkCompression.wMinY : ((!(pos.y > NetworkCompression.wMaxY)) ? pos.y : NetworkCompression.wMaxY));
		float z = ((pos.z < NetworkCompression.wMinZ) ? NetworkCompression.wMinZ : ((!(pos.z > NetworkCompression.wMaxZ)) ? pos.z : NetworkCompression.wMaxZ));
		return new Vector3(x, y, z);
	}

	public virtual void OnDisable()
	{
		if (isInitialized && turnOffOnDisable && !manualDeactivate)
		{
			turningOff = true;
		}
	}

	public virtual void ResetEntity()
	{
		Vector3 pos = GetPos(myTransform.position);
		posTracker.SetData(baseInterval, pos);
		Quaternion rot = GetRot(myTransform.rotation);
		rotTracker.SetData(baseInterval, rot);
		lastPosFrame = (lastRotFrame = 0u);
	}

	protected static bool PosChanged(int changed)
	{
		return (changed & 1) != 0;
	}

	protected static bool RotChanged(int changed)
	{
		return (changed & 2) != 0;
	}

	protected static bool StateChanged(int changed)
	{
		return (changed & 4) != 0;
	}

	public virtual int GetDataSize()
	{
		return GetDataSize((hasChangedPos ? 1 : 0) | (hasChangedRot ? 2 : 0) | (hasChangedState ? 4 : 0));
	}

	public static int GetDataSize(int changed)
	{
		return 1 + (PosChanged(changed) ? 6 : 0) + (RotChanged(changed) ? 7 : 0) + (StateChanged(changed) ? 1 : 0);
	}

	public static int GetMaxDataSize()
	{
		return 15;
	}

	public virtual int EncodeState(byte[] buffer, int offset)
	{
		int num = offset;
		buffer[offset] = (byte)((hasChangedPos ? 1 : 0) | (hasChangedRot ? 2 : 0));
		offset++;
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
		return offset - num;
	}

	public virtual int DecodeState(byte[] data, int offset)
	{
		int num = offset;
		int changed = data[offset];
		offset++;
		if (PosChanged(changed))
		{
			NetworkCompression.DecompressPosition(data, offset, out posHolder);
			posTracker.SetData(baseInterval, posHolder);
			trackTransform.position = GetPos(posHolder);
			offset += 6;
		}
		if (RotChanged(changed))
		{
			NetworkCompression.DecompressRotation(data, offset, out rotHolder);
			rotTracker.SetData(baseInterval, rotHolder);
			trackTransform.rotation = GetRot(rotHolder);
			offset += 7;
		}
		return offset - num;
	}

	protected virtual Vector3 GetPos(Vector3 pos)
	{
		return pos;
	}

	protected virtual Quaternion GetRot(Quaternion rot)
	{
		return rot;
	}

	public virtual bool UpdateEntity(float delta)
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
		return true;
	}

	public virtual int PollObject(bool fullUpdate, byte[] data, int offset)
	{
		int num = offset;
		offset++;
		int num2 = 0;
		if (pollTransform)
		{
			Vector3 position = trackTransform.position;
			if (!posTracker.WithinThreshold(position))
			{
				NetworkCompression.CompressPosition(position, data, offset);
				offset += 6;
				num2 |= 1;
				posTracker.Store(position);
				hasChangedPos = true;
			}
			Quaternion rotation = trackTransform.rotation;
			if (!rotTracker.WithinThreshold(rotation))
			{
				NetworkCompression.CompressRotation(rotation, data, offset);
				offset += 7;
				num2 |= 4;
				rotTracker.Store(rotation);
				hasChangedRot = true;
			}
		}
		data[num] = (byte)num2;
		if (turningOff)
		{
			pollTransform = false;
			turningOff = false;
		}
		return offset - num;
	}

	public virtual bool PollObject(bool fullUpdate)
	{
		if (!pollTransform)
		{
			return false;
		}
		bool result = false;
		Vector3 vector = ClampPosition(GetPos(trackTransform.position));
		if (!posTracker.WithinThreshold(vector))
		{
			NetworkCompression.CompressPosition(vector, sendEntity.Position, 0);
			sendEntity.hasPosition = true;
			hasChangedPos = true;
			posTracker.Store(vector);
			result = true;
		}
		Quaternion rot = GetRot(trackTransform.rotation);
		if (!rotTracker.WithinThreshold(rot))
		{
			NetworkCompression.CompressRotation(rot, sendEntity.Rotation, 0);
			sendEntity.hasRotation = true;
			hasChangedRot = true;
			rotTracker.Store(rot);
			result = true;
		}
		return result;
	}

	public virtual void SetData(uint frame, byte[] data, int offset, bool hasPos, bool hasRot, int eventCount)
	{
		offset++;
		if (hasPos)
		{
			if (frame >= lastPosFrame)
			{
				NetworkCompression.DecompressPosition(data, offset, out posHolder);
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
	}

	public virtual void NewFrame(uint frame)
	{
		if (posTracker.newData && lastPosFrame < frame)
		{
			posTracker.Stop();
			lastPosFrame = frame;
		}
		if (rotTracker.newData && lastRotFrame < frame)
		{
			rotTracker.Stop();
			lastRotFrame = frame;
		}
	}

	public void SetPosition(uint frame, Vector3 pos)
	{
		if (frame >= lastPosFrame)
		{
			posTracker.Set(pos);
			lastPosFrame = frame;
		}
	}

	public void SetRotation(uint frame, Quaternion rot)
	{
		if (frame >= lastRotFrame)
		{
			rotTracker.Set(rot);
			lastRotFrame = frame;
		}
	}

	public virtual void SetEvent(uint frame, EntityEvent evt)
	{
	}
}
