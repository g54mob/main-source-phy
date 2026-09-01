using InternalModding.Resources;
using UnityEngine;

public class LevelGhost : MonoBehaviour
{
	public bool isLocal;

	private NetworkAuxAddPiece networkAuxAddPiece;

	private NetworkInterpolation posTracker;

	private NetworkInterpolation rotTracker;

	private NetworkInterpolation scaleTracker;

	private Material entityMat;

	private LevelPrefab levelPrefab;

	private float lastSend;

	private LevelEditor levelEditor;

	private PlayerData player;

	private Quaternion mouseRot;

	private Vector2 mouseRotPos;

	private bool _hasTransformData;

	private BesiegeNetworkManager networkManager;

	private float minScale;

	private float maxScale = 10f;

	private Vector3 ghostPos;

	private Quaternion ghostRot;

	private Vector3 ghostScale;

	public void Init(ushort id, bool isLocalGhost, Material ghostMat)
	{
		isLocal = isLocalGhost;
		if (!isLocal && !Playerlist.GetPlayer(id, out player))
		{
			Debug.LogWarning("Couldn't find PlayerData for id " + id + "!");
		}
		networkAuxAddPiece = NetworkAuxAddPiece.Instance;
		networkManager = BesiegeNetworkManager.Instance;
		posTracker = new NetworkInterpolation();
		rotTracker = new NetworkInterpolation();
		scaleTracker = new NetworkInterpolation();
		entityMat = ghostMat;
		_hasTransformData = false;
		ghostPos = Vector3.zero;
		ghostRot = Quaternion.identity;
		ghostScale = Vector3.zero;
	}

	public LevelPrefab GetPrefab()
	{
		return levelPrefab;
	}

	public void UpdateTransform(byte[] data, int offset)
	{
		NetworkCompression.DecompressPosition(data, offset, out ghostPos);
		offset += 6;
		NetworkCompression.DecompressRotation(data, offset, out ghostRot);
		offset += 7;
		NetworkCompression.DecompressVector(data, offset, minScale, maxScale, out ghostScale);
		offset += 6;
		if (_hasTransformData)
		{
			posTracker.Set(ghostPos);
			rotTracker.Set(ghostRot);
			scaleTracker.Set(ghostScale);
		}
		else
		{
			posTracker.SetData(NetworkScene.ServerSettings.sendRate, ghostPos);
			rotTracker.SetData(NetworkScene.ServerSettings.sendRate, ghostRot);
			scaleTracker.SetData(NetworkScene.ServerSettings.sendRate, ghostScale);
			_hasTransformData = true;
		}
	}

	public void UpdateTransform(Vector3 pos, Vector3 rot, Vector3 scale)
	{
		base.transform.position = pos;
		base.transform.rotation = Quaternion.Euler(rot);
		base.transform.localScale = scale;
	}

	public void Toggle(byte[] data)
	{
		if (player == null)
		{
			Debug.LogWarning("Couldn't toggle ghost - player doesn't exist!");
			return;
		}
		Vector3 vec = Vector3.zero;
		bool flag = data[0] == 1;
		if (flag)
		{
			if (levelEditor == null)
			{
				levelEditor = LevelEditor.Instance;
			}
			int num = 1;
			NetworkCompression.DecompressPosition(data, num, out vec);
			num += 6;
			player.customPos = vec;
			int num2 = NetworkCompression.ReadUInt16(data, num);
			LevelPrefab prefab;
			if (!levelEditor.GetPrefab(num2, out prefab))
			{
				Debug.LogError("Couldn't find prefab " + num2 + "!");
			}
			SetPrefab(prefab);
			player.activePrefab = prefab;
			posTracker.SetData(NetworkScene.ServerSettings.sendRate, vec);
		}
		else
		{
			posTracker.Stop();
		}
		player.useCustomPos = flag;
		Toggle(flag, vec);
	}

	public void Toggle(bool toggle, Vector3 pos)
	{
		if (toggle == base.gameObject.activeSelf)
		{
			return;
		}
		if (StatMaster.isMP && isLocal && (bool)levelPrefab)
		{
			byte[] array = new byte[1 + (toggle ? 8 : 0)];
			array[0] = (byte)(toggle ? 1u : 0u);
			if (toggle)
			{
				int num = 1;
				NetworkCompression.CompressPosition(pos, array, num);
				num += 6;
				NetworkCompression.WriteUInt16((ushort)levelPrefab.ID, array, num);
				lastSend = 0f;
			}
			networkAuxAddPiece.SendNetworkMessage(RPCMessageType.ToggleGhost, array);
		}
		UpdateTransform(pos, Vector3.zero, Vector3.one);
		base.gameObject.SetActive(toggle);
	}

	public void Update()
	{
		if (!StatMaster.isMP)
		{
			return;
		}
		float unscaledDeltaTime = Time.unscaledDeltaTime;
		if (isLocal)
		{
			lastSend += unscaledDeltaTime;
			float sendRate = NetworkScene.ServerSettings.sendRate;
			if (lastSend >= sendRate)
			{
				while (lastSend >= sendRate)
				{
					lastSend -= sendRate;
				}
				int ghostMessageHeaderSize = networkManager.GhostMessageHeaderSize;
				byte[] array = new byte[ghostMessageHeaderSize + 6 + 7 + 6];
				int num = ghostMessageHeaderSize;
				NetworkCompression.CompressPosition(base.transform.position, array, num);
				num += 6;
				NetworkCompression.CompressRotation(base.transform.rotation, array, num);
				num += 7;
				NetworkCompression.CompressVector(base.transform.localScale, minScale, maxScale, array, num);
				networkManager.SendGhostData(networkManager.PlayerID, array);
			}
		}
		else
		{
			if (posTracker.isActive)
			{
				posTracker.Update(unscaledDeltaTime);
				Vector3 vector = posTracker.Vector;
				base.transform.position = vector;
				player.customPos = vector;
			}
			if (rotTracker.isActive)
			{
				rotTracker.Update(unscaledDeltaTime);
				base.transform.rotation = rotTracker.Rotation;
			}
			if (scaleTracker.isActive)
			{
				scaleTracker.Update(unscaledDeltaTime);
				base.transform.localScale = scaleTracker.Vector;
			}
		}
	}

	public void SetPrefab(LevelPrefab prefab)
	{
		if (prefab == levelPrefab)
		{
			return;
		}
		if (base.transform.childCount > 0)
		{
			Object.Destroy(base.transform.GetChild(0).gameObject);
		}
		levelPrefab = prefab;
		if (prefab == null)
		{
			return;
		}
		GameObject gameObject = Object.Instantiate((!prefab.hasCustomGhost) ? prefab.gameObject : prefab.ghostPrefab);
		Transform transform = gameObject.transform;
		if (!prefab.hasCustomGhost)
		{
			BasicInfo component = gameObject.GetComponent<BasicInfo>();
			component.isBuildBlock = true;
			gameObject.SetActive(true);
		}
		transform.SetParent(base.transform, false);
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
		transform.localScale = Vector3.one;
		bool activeSelf = base.gameObject.activeSelf;
		base.gameObject.SetActive(true);
		if (!prefab.hasCustomGhost)
		{
			Joint[] componentsInChildren = gameObject.GetComponentsInChildren<Joint>();
			Joint[] array = componentsInChildren;
			foreach (Joint obj in array)
			{
				Object.Destroy(obj);
			}
			Component[] componentsInChildren2 = gameObject.GetComponentsInChildren<Component>();
			Component[] array2 = componentsInChildren2;
			foreach (Component component2 in array2)
			{
				if (component2 == null)
				{
					continue;
				}
				if (component2 is MeshRenderer || component2 is SkinnedMeshRenderer)
				{
					if (!prefab.applyGhostMaterial)
					{
						continue;
					}
					MeshRenderer meshRenderer = component2 as MeshRenderer;
					SkinnedMeshRenderer skinnedMeshRenderer = component2 as SkinnedMeshRenderer;
					if (meshRenderer != null)
					{
						if (meshRenderer.sharedMaterial.mainTexture == null && meshRenderer.sharedMaterial.renderQueue > 2500)
						{
							meshRenderer.materials = new Material[0];
							continue;
						}
						if (meshRenderer.materials.Length == 1)
						{
							Material material = new Material(entityMat);
							material.mainTexture = meshRenderer.material.mainTexture;
							material.mainTextureScale = meshRenderer.material.mainTextureScale;
							meshRenderer.material = material;
							continue;
						}
						Material[] array3 = new Material[meshRenderer.materials.Length];
						for (int k = 0; k < meshRenderer.materials.Length; k++)
						{
							Material material2 = new Material(entityMat);
							material2.mainTexture = meshRenderer.materials[k].mainTexture;
							material2.mainTextureScale = meshRenderer.materials[k].mainTextureScale;
							array3[k] = material2;
						}
						meshRenderer.materials = array3;
					}
					else
					{
						if (!(skinnedMeshRenderer != null))
						{
							continue;
						}
						if (skinnedMeshRenderer.materials.Length == 1)
						{
							Material material3 = new Material(entityMat);
							material3.mainTexture = skinnedMeshRenderer.material.mainTexture;
							material3.mainTextureScale = skinnedMeshRenderer.material.mainTextureScale;
							skinnedMeshRenderer.material = material3;
							continue;
						}
						Material[] array4 = new Material[skinnedMeshRenderer.materials.Length];
						for (int l = 0; l < skinnedMeshRenderer.materials.Length; l++)
						{
							Material material4 = new Material(entityMat);
							material4.mainTexture = skinnedMeshRenderer.materials[l].mainTexture;
							material4.mainTextureScale = skinnedMeshRenderer.materials[l].mainTextureScale;
							array4[l] = material4;
						}
						skinnedMeshRenderer.materials = array4;
					}
				}
				else if (!(component2 is MeshFilter) && !(component2 is TextMesh) && !(component2 is Transform) && !(component2 is LookAtCamera) && !(component2 is Cloth) && !(component2 is GrabModResource))
				{
					Object.Destroy(component2);
				}
			}
		}
		base.gameObject.SetActive(activeSelf);
	}
}
