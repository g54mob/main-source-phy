using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using mattmc3.dotmore.Collections.Generic;

public class ProjectileManager : MonoBehaviour
{
	public class ProjectilePool
	{
		public Stack<NetworkProjectile> Pool;

		public List<NetworkProjectile> Active;

		public int ActiveCount;

		public ProjectilePool()
		{
			Pool = new Stack<NetworkProjectile>();
			Active = new List<NetworkProjectile>();
			ActiveCount = 0;
		}
	}

	private class SpawnInfo
	{
		public ushort networkId;

		public NetworkProjectileType projectileType;

		public ushort playerId;

		public byte[] data;

		public int Size()
		{
			return 5 + data.Length;
		}
	}

	private class DespawnInfo
	{
		public ushort networkId;

		public byte[] data;

		public int Size()
		{
			return 2 + data.Length;
		}
	}

	public GameObject[] projectilePrefabs;

	public static ProjectileManager Instance;

	private static int DEFAULT_POOL_SIZE = 50;

	private static int MAX_POOL_SIZE = 200;

	private NetworkController networkController;

	private List<SpawnInfo> spawnDataPool;

	private List<DespawnInfo> despawnDataPool;

	private Dictionary<ushort, uint> lastEventFrame;

	private int dataPoolLength;

	private List<ProjectilePool> projectilePool;

	private List<NetworkProjectile> spawnedProjectiles;

	private Transform poolParent;

	private bool isClearing;

	private List<ushort> eventFrameKeys;

	public int BufferLength
	{
		get
		{
			return networkController.FullBufferLength;
		}
	}

	public bool SendShort
	{
		get
		{
			return networkController.SendShort;
		}
	}

	public int ProjectileCount
	{
		get
		{
			return spawnedProjectiles.Count;
		}
	}

	public int SpawnDataLength
	{
		get
		{
			return 2 + spawnDataPool.Count + despawnDataPool.Count + dataPoolLength;
		}
	}

	public static void SetParent(Transform t)
	{
		t.parent = Instance.poolParent;
	}

	public void PollObjects()
	{
		networkController.PollObjects(true, networkController.objList, networkController.objCount);
	}

	protected void Awake()
	{
		networkController = base.gameObject.AddComponent<NetworkController>();
		Instance = this;
		spawnDataPool = new List<SpawnInfo>();
		despawnDataPool = new List<DespawnInfo>();
		projectilePool = new List<ProjectilePool>(projectilePrefabs.Length);
		int capacity = projectilePrefabs.Length * MAX_POOL_SIZE;
		spawnedProjectiles = new List<NetworkProjectile>(capacity);
		networkController.SetCapacity(capacity);
		GameObject gameObject = new GameObject("Projectile Pool");
		poolParent = gameObject.transform;
		poolParent.parent = base.transform;
		int capacity2 = projectilePrefabs.Length * MAX_POOL_SIZE;
		lastEventFrame = new Dictionary<ushort, uint>(capacity2);
		eventFrameKeys = new List<ushort>(capacity2);
		for (int i = 0; i < projectilePrefabs.Length; i++)
		{
			CreatePool(i);
		}
	}

	public void AddAdditionalProjectile(int id, GameObject prefab)
	{
		if (id != projectilePrefabs.Length)
		{
			throw new ArgumentException("ID " + id + " is not in sequential order!");
		}
		projectilePrefabs = projectilePrefabs.Append(prefab).ToArray();
		int capacity = projectilePrefabs.Length * MAX_POOL_SIZE;
		networkController.SetCapacity(capacity);
		spawnedProjectiles.Capacity = capacity;
		eventFrameKeys.Capacity = capacity;
		CreatePool(id);
	}

	public void ClearAdditionalProjectiles()
	{
		int length = Enum.GetValues(typeof(NetworkProjectileType)).Length;
		projectilePrefabs = projectilePrefabs.Take(length).ToArray();
		for (int i = length; i < this.projectilePool.Count; i++)
		{
			ProjectilePool projectilePool = this.projectilePool[i];
			while (projectilePool.Pool.Count > 0)
			{
				NetworkProjectile networkProjectile = projectilePool.Pool.Pop();
				UnityEngine.Object.Destroy(networkProjectile.gameObject);
			}
			for (int j = 0; j < MAX_POOL_SIZE; j++)
			{
				ushort num = (ushort)(i * MAX_POOL_SIZE + j);
				lastEventFrame.Remove(num);
				eventFrameKeys.Remove(num);
			}
		}
		this.projectilePool = this.projectilePool.Take(length).ToList();
	}

	private void CreatePool(int i)
	{
		ProjectilePool projectilePool = new ProjectilePool();
		for (int j = 0; j < DEFAULT_POOL_SIZE; j++)
		{
			NetworkProjectile networkProjectile = Create((NetworkProjectileType)i);
			networkProjectile.transform.parent = poolParent;
			projectilePool.Pool.Push(networkProjectile);
		}
		for (int j = 0; j < MAX_POOL_SIZE; j++)
		{
			ushort num = (ushort)(i * MAX_POOL_SIZE + j);
			lastEventFrame.Add(num, 0u);
			eventFrameKeys.Add(num);
		}
		this.projectilePool.Add(projectilePool);
	}

	public ProjectilePool GetPool(int id)
	{
		return projectilePool[id];
	}

	public void InitSim(bool track)
	{
		networkController.InitSim(track);
	}

	public void WriteBufferData(byte[] buffer, int offset)
	{
		networkController.WriteBufferData(true, buffer, offset);
	}

	public int GetSimFrame()
	{
		return 2 + spawnedProjectiles.Count * 18;
	}

	public void WriteSimFrame(byte[] data, int offset)
	{
		NetworkCompression.WriteUInt16((ushort)spawnedProjectiles.Count, data, offset);
		offset += 2;
		for (int i = 0; i < spawnedProjectiles.Count; i++)
		{
			NetworkProjectile networkProjectile = spawnedProjectiles[i];
			NetworkProjectileType projectileType = networkProjectile.projectileInfo.projectileType;
			data[offset] = (byte)projectileType;
			offset++;
			if ((int)projectileType < projectilePrefabs.Length)
			{
				NetworkCompression.WriteUInt16(networkProjectile.playerId, data, offset);
				offset += 2;
				NetworkCompression.WriteUInt16((ushort)networkProjectile.id, data, offset);
				offset += 2;
				Transform transform = networkProjectile.transform;
				NetworkCompression.CompressPosition(transform.position, data, offset);
				offset += 6;
				NetworkCompression.CompressRotation(transform.rotation, data, offset);
				offset += 7;
			}
			else
			{
				Debug.LogError("Invalid projectile type: " + projectileType);
			}
		}
	}

	public int ReadSimFrame(byte[] data, int offset)
	{
		int num = offset;
		ushort num2 = NetworkCompression.ReadUInt16(data, offset);
		offset += 2;
		for (int i = 0; i < num2; i++)
		{
			NetworkProjectileType networkProjectileType = (NetworkProjectileType)data[offset];
			offset++;
			ushort playerId = NetworkCompression.ReadUInt16(data, offset);
			offset += 2;
			byte[] array = new byte[0];
			if ((int)networkProjectileType < projectilePrefabs.Length)
			{
				array = new byte[15];
				Buffer.BlockCopy(data, offset, array, 0, array.Length);
				offset += array.Length;
			}
			else
			{
				Debug.LogError("Invalid projectile type: " + networkProjectileType);
			}
			Spawn(networkProjectileType, 0u, playerId, array);
		}
		return offset - num;
	}

	public void ResetFrame()
	{
		networkController.ResetFrame();
	}

	public int ReadBufferData(uint frame, byte[] transformData, int offset)
	{
		return networkController.ReadBufferData(frame, transformData, offset);
	}

	public void NewFrame(uint frame)
	{
		networkController.NewFrame(frame);
	}

	public void UpdateProjectiles(float delta)
	{
		for (int i = 0; i < spawnedProjectiles.Count; i++)
		{
			spawnedProjectiles[i].UpdateEntity(delta);
		}
	}

	public void ClearSpawnData()
	{
		dataPoolLength = 0;
		spawnDataPool.Clear();
		despawnDataPool.Clear();
	}

	public void WriteSpawnData(byte[] data, int offset)
	{
		data[offset] = (byte)despawnDataPool.Count;
		offset++;
		for (int i = 0; i < despawnDataPool.Count; i++)
		{
			DespawnInfo despawnInfo = despawnDataPool[i];
			int num = despawnInfo.Size();
			data[offset] = (byte)num;
			offset++;
			NetworkCompression.WriteUInt16(despawnInfo.networkId, data, offset);
			offset += 2;
			Buffer.BlockCopy(despawnInfo.data, 0, data, offset, despawnInfo.data.Length);
			offset += despawnInfo.data.Length;
		}
		data[offset] = (byte)spawnDataPool.Count;
		offset++;
		for (int i = 0; i < spawnDataPool.Count; i++)
		{
			SpawnInfo spawnInfo = spawnDataPool[i];
			int num2 = spawnInfo.Size();
			data[offset] = (byte)num2;
			offset++;
			data[offset] = (byte)spawnInfo.projectileType;
			offset++;
			NetworkCompression.WriteUInt16(spawnInfo.playerId, data, offset);
			offset += 2;
			NetworkCompression.WriteUInt16(spawnInfo.networkId, data, offset);
			offset += 2;
			Buffer.BlockCopy(spawnInfo.data, 0, data, offset, spawnInfo.data.Length);
			offset += spawnInfo.data.Length;
		}
		ClearSpawnData();
	}

	private bool IsNewData(ushort networkId, uint frame)
	{
		if (frame < lastEventFrame[networkId])
		{
			return false;
		}
		lastEventFrame[networkId] = frame;
		return true;
	}

	public int ReadSpawnData(uint frame, byte[] data, int offset)
	{
		int num = offset;
		int num2 = data[offset];
		offset++;
		int num3 = 0;
		int num4 = 0;
		for (int i = 0; i < num2; i++)
		{
			int num5 = data[offset] - 2;
			num5 = ((num5 >= 0) ? num5 : 0);
			num3 += ((num5 < 0) ? 1 : 0);
			offset++;
			ushort networkId = NetworkCompression.ReadUInt16(data, offset);
			offset += 2;
			byte[] array = new byte[num5];
			Buffer.BlockCopy(data, offset, array, 0, num5);
			offset += num5;
			NetworkProjectile projectile;
			if (IsNewData(networkId, frame) && GetProjectile(networkId, out projectile))
			{
				Despawn(projectile, array);
			}
		}
		int num6 = data[offset];
		offset++;
		for (int i = 0; i < num6; i++)
		{
			int num7 = data[offset] - 3;
			num7 = ((num7 >= 0) ? num7 : 0);
			num4 += ((num7 < 0) ? 1 : 0);
			offset++;
			NetworkProjectileType projectileType = (NetworkProjectileType)data[offset];
			ushort playerId = NetworkCompression.ReadUInt16(data, offset + 1);
			offset += 3;
			byte[] array2 = new byte[num7];
			Buffer.BlockCopy(data, offset, array2, 0, num7);
			ushort networkId = NetworkCompression.ReadUInt16(data, offset);
			offset += num7;
			if (IsNewData(networkId, frame))
			{
				Spawn(projectileType, frame, playerId, array2);
			}
		}
		if (num3 > 0 || num4 > 0)
		{
			Debug.LogError("ProjectileManager.ReadSpawnData: tried to despawn " + num3 + " invalid projectiles, and spawn " + num4 + " invalid projectiles.");
		}
		return offset - num;
	}

	public Transform Spawn(NetworkProjectileType projectileType, uint frame, ushort playerId, byte[] spawnInfo, bool explode = false)
	{
		int i = 0;
		ProjectilePool projectilePool = this.projectilePool[(int)projectileType];
		ushort num;
		NetworkProjectile projectile2;
		if (StatMaster.isHosting)
		{
			while (projectilePool.ActiveCount >= MAX_POOL_SIZE)
			{
				NetworkProjectile projectile = projectilePool.Active[0];
				Despawn(projectile);
			}
			if (spawnedProjectiles.Count > 0)
			{
				for (; i < spawnedProjectiles.Count && spawnedProjectiles[i].id == i; i++)
				{
				}
			}
			num = (ushort)i;
		}
		else
		{
			num = NetworkCompression.ReadUInt16(spawnInfo, 0);
			if (GetProjectile(num, out projectile2))
			{
				Despawn(projectile2);
			}
			if (spawnedProjectiles.Count > 0)
			{
				for (; i < spawnedProjectiles.Count && spawnedProjectiles[i].id < num; i++)
				{
				}
			}
			byte[] array = new byte[spawnInfo.Length - 2];
			Buffer.BlockCopy(spawnInfo, 2, array, 0, array.Length);
			spawnInfo = array;
		}
		projectile2 = ((projectilePool.Pool.Count != 0) ? projectilePool.Pool.Pop() : Create(projectileType));
		if (projectile2 == null)
		{
			projectile2 = Create(projectileType);
		}
		projectilePool.ActiveCount++;
		projectilePool.Active.Add(projectile2);
		Transform transform = projectile2.transform;
		spawnedProjectiles.Insert(i, projectile2);
		transform.SetParent(base.transform, false);
		projectile2.Spawn(frame, playerId, spawnInfo, explode);
		projectile2.gameObject.SetActive(true);
		projectile2.Init(num, networkController, transform, StatMaster.isHosting);
		networkController.Add(projectile2);
		ServerHealth.countDirty = true;
		if (StatMaster.isHosting)
		{
			ClearSpawn(num);
			SpawnInfo spawnInfo2 = new SpawnInfo();
			spawnInfo2.projectileType = projectileType;
			spawnInfo2.playerId = playerId;
			spawnInfo2.networkId = num;
			spawnInfo2.data = spawnInfo;
			spawnDataPool.Add(spawnInfo2);
			dataPoolLength += spawnInfo2.Size();
		}
		return projectile2.transform;
	}

	public void Clear()
	{
		isClearing = true;
		while (spawnedProjectiles.Count > 0)
		{
			Despawn(spawnedProjectiles[0]);
		}
		for (int i = 0; i < projectilePrefabs.Length; i++)
		{
			projectilePool[i].ActiveCount = 0;
		}
		for (int i = 0; i < eventFrameKeys.Count; i++)
		{
			lastEventFrame[eventFrameKeys[i]] = 0u;
		}
		isClearing = false;
	}

	public void DespawnParentedProjectiles(Transform objTransform)
	{
		ProjectilePool projectilePool = this.projectilePool[1];
		List<NetworkProjectile> list = new List<NetworkProjectile>(projectilePool.Active);
		list.AddRange(this.projectilePool[2].Active);
		list.AddRange(this.projectilePool[3].Active);
		list.AddRange(this.projectilePool[4].Active);
		for (int i = Enum.GetValues(typeof(NetworkProjectileType)).Length; i < projectilePrefabs.Length; i++)
		{
			list.AddRange(this.projectilePool[i].Active);
		}
		foreach (NetworkProjectile item in list)
		{
			if (item != null && item.IsChildOf(objTransform))
			{
				Despawn(item);
			}
		}
	}

	public void Despawn(Transform projectileTransform)
	{
		NetworkProjectile component = projectileTransform.GetComponent<NetworkProjectile>();
		if (component != null)
		{
			Despawn(component, null);
		}
	}

	public void Despawn(NetworkProjectile projectile)
	{
		Despawn(projectile, null);
	}

	public void Despawn(NetworkProjectile projectile, byte[] despawnInfo)
	{
		if (projectile == null)
		{
			return;
		}
		ProjectilePool projectilePool = this.projectilePool[(int)projectile.projectileInfo.projectileType];
		if (projectilePool.Active.Contains(projectile))
		{
			Transform transform = projectile.transform;
			transform.SetParent(poolParent, false);
			projectile.Despawn(despawnInfo);
			projectile.gameObject.SetActive(false);
			projectilePool.ActiveCount--;
			projectilePool.Active.Remove(projectile);
			spawnedProjectiles.Remove(projectile);
			networkController.Remove(projectile);
			ServerHealth.countDirty = true;
			if (StatMaster.isHosting && !isClearing)
			{
				ClearDespawn((ushort)projectile.id);
				DespawnInfo despawnInfo2 = new DespawnInfo();
				despawnInfo2.networkId = (ushort)projectile.id;
				despawnInfo2.data = ((despawnInfo == null) ? new byte[0] : despawnInfo);
				despawnDataPool.Add(despawnInfo2);
				dataPoolLength += despawnInfo2.Size();
			}
			if (!projectile.HasChangedState)
			{
				projectile.ReturnToPool();
				projectilePool.Pool.Push(projectile);
			}
			else
			{
				UnityEngine.Object.Destroy(projectile.gameObject);
			}
		}
	}

	private void ClearSpawn(ushort networkId)
	{
		List<SpawnInfo> list = new List<SpawnInfo>(spawnDataPool);
		for (int i = 0; i < list.Count; i++)
		{
			SpawnInfo spawnInfo = list[i];
			if (spawnInfo.networkId == networkId)
			{
				dataPoolLength -= spawnInfo.Size();
				spawnDataPool.Remove(spawnInfo);
			}
		}
	}

	private void ClearDespawn(ushort networkId)
	{
		List<DespawnInfo> list = new List<DespawnInfo>(despawnDataPool);
		for (int i = 0; i < list.Count; i++)
		{
			DespawnInfo despawnInfo = list[i];
			if (despawnInfo.networkId == networkId)
			{
				dataPoolLength -= despawnInfo.Size();
				despawnDataPool.Remove(despawnInfo);
			}
		}
	}

	private NetworkProjectile Create(NetworkProjectileType projectileType)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(projectilePrefabs[(int)projectileType]);
		NetworkProjectile component = gameObject.GetComponent<NetworkProjectile>();
		component.UpdateTransforms();
		return component;
	}

	private bool GetProjectile(uint networkId, out NetworkProjectile projectile)
	{
		for (int i = 0; i < spawnedProjectiles.Count; i++)
		{
			NetworkProjectile networkProjectile = spawnedProjectiles[i];
			if (networkProjectile.id == networkId)
			{
				projectile = networkProjectile;
				return true;
			}
		}
		projectile = null;
		return false;
	}
}
