using System;
using UnityEngine;

public class NetworkProjectile : NetworkBlock
{
	public ushort playerId;

	public bool hasProjectileScript;

	public ProjectileScript projectileScript;

	public ProjectileInfo projectileInfo;

	public float bodyMass;

	public float bodyDrag;

	public float bodyAngularDrag;

	public RigidbodyInterpolation bodyInterpolation;

	public bool bodyKinematic;

	public CollisionDetectionMode bodyCollisionMode;

	private bool hasGyro;

	protected override void AwakeBase()
	{
		if (isAwake)
		{
			return;
		}
		base.AwakeBase();
		hasGyro = false;
		if (hasProjectileScript)
		{
			hasGyro = projectileScript.gyro != null;
			if (hasGyro)
			{
				trackTransform = projectileScript.gyro;
			}
		}
	}

	private bool IsPlayerProjectile(NetworkProjectileType type)
	{
		int length = Enum.GetValues(typeof(NetworkProjectileType)).Length;
		switch (type)
		{
		case NetworkProjectileType.Cannon:
		case NetworkProjectileType.CrossbowArrow:
		case NetworkProjectileType.SurfaceFragment:
		case NetworkProjectileType.Harpoon:
			return true;
		default:
			return (int)type >= length;
		}
	}

	protected virtual void SetParentMachine(ushort playerId)
	{
		if (IsPlayerProjectile(projectileInfo.projectileType))
		{
			ServerMachine machine;
			if (NetworkScene.Instance.GetMachine(playerId, out machine))
			{
				projectileInfo.SetParentMachine(machine);
			}
			else
			{
				projectileInfo.ResetParentMachine();
			}
		}
	}

	public virtual void Spawn(uint frame, ushort playerId, byte[] spawnInfo, bool explode = false)
	{
		int num = 0;
		this.playerId = playerId;
		SetParentMachine(playerId);
		Vector3 vec;
		NetworkCompression.DecompressPosition(spawnInfo, num, out vec);
		base.transform.position = vec;
		num += 6;
		Quaternion rot;
		NetworkCompression.DecompressRotation(spawnInfo, num, out rot);
		base.transform.rotation = rot;
		if (projectileInfo.noRigidbody)
		{
			projectileInfo.Rigidbody = base.gameObject.AddComponent<Rigidbody>();
			projectileInfo.noRigidbody = false;
		}
		Rigidbody rigidbody = projectileInfo.Rigidbody;
		if (StatMaster.isHosting)
		{
			rigidbody.isKinematic = bodyKinematic;
			rigidbody.mass = bodyMass;
			rigidbody.drag = bodyDrag;
			rigidbody.angularDrag = bodyAngularDrag;
			rigidbody.interpolation = bodyInterpolation;
			rigidbody.collisionDetectionMode = bodyCollisionMode;
		}
		else
		{
			rigidbody.isKinematic = true;
			base.gameObject.SetActive(false);
		}
		turnOffOnDisable = false;
		Vector3 vec2 = Vector3.one;
		if (hasProjectileScript)
		{
			projectileScript.ownerID = playerId;
			projectileScript.enabled = true;
		}
		if (spawnInfo.Length != 13)
		{
			num += 7;
			NetworkCompression.DecompressVector(spawnInfo, num, 0f, 100f, out vec2);
			if (hasProjectileScript)
			{
				projectileScript.SetScale(vec2);
			}
			else if (projectileInfo.projectileType == NetworkProjectileType.ChainShot)
			{
				base.gameObject.GetComponent<CannonChainBall>().SetSize(vec2.x);
			}
			else
			{
				base.transform.localScale = vec2;
			}
		}
	}

	public virtual void Despawn(byte[] despawnInfo)
	{
	}

	public virtual void ReturnToPool()
	{
		myTransform.localRotation = Quaternion.identity;
		myTransform.localScale = Vector3.one;
		if (!projectileInfo.noRigidbody)
		{
			projectileInfo.Rigidbody.interpolation = RigidbodyInterpolation.None;
			Rigidbody rigidbody = projectileInfo.Rigidbody;
			Vector3 zero = Vector3.zero;
			projectileInfo.Rigidbody.velocity = zero;
			rigidbody.angularVelocity = zero;
		}
		if (hasGyro && !hasChangedState)
		{
			Transform transform = projectileScript.gyro.transform;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
		}
	}

	public override int PollObject(bool fullUpdate, byte[] data, int offset)
	{
		int num = offset;
		offset++;
		int eventCount = sendEntity.eventCount;
		position = trackTransform.position;
		Vector3 vector = NetworkEntity.ClampPosition(position);
		rotation = trackTransform.rotation;
		Quaternion rot = rotation;
		int num2 = 0;
		bool flag = false;
		if (!posTracker.WithinThreshold(vector))
		{
			if (eventCount > 0)
			{
				Buffer.BlockCopy(sendEntity.EventList, 0, data, offset, eventCount);
				sendEntity.eventCount = 0;
				offset += eventCount;
				num2 |= eventCount << 3;
			}
			flag = true;
			NetworkCompression.CompressPosition(vector, data, offset);
			offset += 6;
			num2 |= 1;
			posTracker.Store(vector);
			hasChangedPos = true;
		}
		if (!flag && eventCount > 0)
		{
			Buffer.BlockCopy(sendEntity.EventList, 0, data, offset, eventCount);
			sendEntity.eventCount = 0;
			offset += eventCount;
			num2 |= eventCount << 3;
		}
		if (!rotTracker.WithinThreshold(rot))
		{
			NetworkCompression.CompressRotation(rot, data, offset);
			offset += 7;
			num2 |= 4;
			rotTracker.Store(rot);
			hasChangedRot = true;
		}
		data[num] = (byte)num2;
		if (turningOff)
		{
			pollTransform = false;
			turningOff = false;
		}
		return offset - num;
	}

	public virtual bool IsChildOf(Transform obj)
	{
		return base.transform.IsChildOf(obj);
	}
}
