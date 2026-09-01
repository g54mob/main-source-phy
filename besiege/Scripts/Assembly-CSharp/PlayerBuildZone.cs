using UnityEngine;

public class PlayerBuildZone : MonoBehaviour
{
	[HideInInspector]
	public Bounds bounds;

	[HideInInspector]
	public PlayerData player;

	public NetworkBoundingBoxController boundingBoxController;

	public BoxCollider[] colliders;

	public GameObject curtainIcon;

	public BuildZoneObject spawnZone;

	public bool hasSpawnZone;

	public Renderer[] teamVisRenderers = new Renderer[0];

	public Transform zoneTransform;

	public LevelSettings.LevelEnvironment currentEnv;

	private Color initialVisColor;

	private HammerAndNailAnim hammerAndNail;

	private Transform tempTransform;

	private NetworkAuxAddPiece auxAddPiece;

	private float nonLocalMaxAudioDistance = 20f;

	protected void Awake()
	{
		zoneTransform = base.transform;
		Transform transform = zoneTransform.FindChild("HAMMER");
		if (transform != null)
		{
			hammerAndNail = transform.GetComponent<HammerAndNailAnim>();
		}
		auxAddPiece = NetworkAuxAddPiece.Instance;
		GameObject gameObject = new GameObject("ZoneTransformation Helper");
		tempTransform = gameObject.transform;
		tempTransform.parent = GameObject.Find("TRANSFORM HELPERS").transform;
		if (teamVisRenderers.Length > 0)
		{
			initialVisColor = teamVisRenderers[0].material.GetColor("_TintColor");
		}
		if (curtainIcon != null)
		{
			ToggleCurtain(false);
		}
	}

	public void ToggleCurtain(bool toggle)
	{
		curtainIcon.SetActive(toggle);
	}

	public void UpdateCurtainIcon()
	{
		curtainIcon.transform.position = player.machine.Position;
	}

	public void AnimateHammer(Vector3 hit, Vector3 pos, Vector3 fwd, bool localMachine)
	{
		hammerAndNail.Animate(hit, pos, fwd);
	}

	protected void OnDestroy()
	{
		if (tempTransform != null)
		{
			Object.DestroyImmediate(tempTransform.gameObject);
		}
	}

	public void SetSpawnZone(BuildZoneObject zoneObj)
	{
		spawnZone = zoneObj;
		hasSpawnZone = true;
		UpdateTeam(zoneObj.Team, currentEnv);
	}

	public void RemoveSpawnZone()
	{
		hasSpawnZone = false;
		UpdateTeam(MPTeam.None, currentEnv);
	}

	public void UpdateTeam(MPTeam team, LevelSettings.LevelEnvironment env)
	{
		if (player.team == team && env == currentEnv)
		{
			return;
		}
		player.team = team;
		currentEnv = env;
		if (StatMaster.isHeadless)
		{
			return;
		}
		Color color = ((env != LevelSettings.LevelEnvironment.Water) ? initialVisColor : ReferenceMaster.Instance.waterPlayerBoundColor);
		Color color2 = ((team != MPTeam.None) ? ReferenceMaster.Instance.zoneColors[(int)team] : color);
		color2.a = initialVisColor.a;
		boundingBoxController.startColor = color2;
		PlayerLabel label;
		if (auxAddPiece.hud.playerLabelManager.Get(player, out label))
		{
			label.UpdateTeam();
		}
		Renderer[] array = teamVisRenderers;
		foreach (Renderer renderer in array)
		{
			if ((bool)renderer && renderer.material.HasProperty("_TintColor"))
			{
				Color color3 = color2;
				color3.a = renderer.material.GetColor("_TintColor").a;
				renderer.material.SetColor("_TintColor", color3);
			}
		}
	}

	private void AdjustNonLocalAudioSource(AudioSource aSrc)
	{
		aSrc.maxDistance = nonLocalMaxAudioDistance;
		aSrc.spatialBlend = 1f;
	}

	public void Init(PlayerData playerData)
	{
		player = playerData;
		boundingBoxController.SetPlayer(playerData);
		if (!player.isLocalPlayer)
		{
			AdjustNonLocalAudioSource(hammerAndNail.soundController.audioSource);
			AdjustNonLocalAudioSource(hammerAndNail.woodHitAudioController.audioSource);
			AdjustNonLocalAudioSource(boundingBoxController.aSource);
			for (int i = 0; i < colliders.Length; i++)
			{
				Object.Destroy(colliders[i]);
			}
		}
		else
		{
			boundingBoxController.addPiece = NetworkAddPiece.Instance;
			boundingBoxController.enabled = true;
		}
		RemoveSpawnZone();
	}

	public void ResetBounds()
	{
		boundingBoxController.SetFloorPos(true);
	}

	private void ResetVisuals()
	{
		boundingBoxController.ResetRenders();
	}

	public void ResetTransform()
	{
		zoneTransform.position = Vector3.up * 5.05f;
		zoneTransform.rotation = Quaternion.identity;
	}

	public void UndoRotation(Transform buildMachine, bool isTemp)
	{
		if (!isTemp)
		{
			tempTransform.position = buildMachine.position;
			tempTransform.rotation = buildMachine.rotation;
		}
		Transform parent = tempTransform.parent;
		tempTransform.SetParent(zoneTransform, true);
		Quaternion rotation = zoneTransform.rotation;
		zoneTransform.rotation = Quaternion.identity;
		tempTransform.parent = parent;
		zoneTransform.rotation = rotation;
		if (!isTemp)
		{
			buildMachine.position = tempTransform.position;
			buildMachine.rotation = tempTransform.rotation;
		}
	}

	public void ApplyRotation(Transform machineTransform, bool isTemp)
	{
		if (!isTemp)
		{
			tempTransform.position = machineTransform.position;
			tempTransform.rotation = machineTransform.rotation;
		}
		Transform parent = tempTransform.parent;
		Quaternion rotation = zoneTransform.rotation;
		zoneTransform.rotation = Quaternion.identity;
		tempTransform.parent = zoneTransform;
		zoneTransform.rotation = rotation;
		tempTransform.parent = parent;
		if (!isTemp)
		{
			machineTransform.position = tempTransform.position;
			machineTransform.rotation = tempTransform.rotation;
		}
	}

	public void UndoTransform(Transform buildMachine, bool isTemp)
	{
		if (!isTemp)
		{
			tempTransform.position = buildMachine.position;
			tempTransform.rotation = buildMachine.rotation;
		}
		tempTransform.SetParent(zoneTransform, true);
		Vector3 position = zoneTransform.position;
		Quaternion rotation = zoneTransform.rotation;
		ResetTransform();
		tempTransform.parent = null;
		zoneTransform.position = position;
		zoneTransform.rotation = rotation;
		if (!isTemp)
		{
			buildMachine.position = tempTransform.position;
			buildMachine.rotation = tempTransform.rotation;
		}
	}

	public void ApplyTransform(Transform machineTransform, bool isTemp)
	{
		if (!isTemp)
		{
			tempTransform.position = machineTransform.position;
			tempTransform.rotation = machineTransform.rotation;
		}
		Vector3 position = zoneTransform.position;
		Quaternion rotation = zoneTransform.rotation;
		ResetTransform();
		tempTransform.parent = zoneTransform;
		zoneTransform.rotation = rotation;
		zoneTransform.position = position;
		tempTransform.parent = null;
		if (!isTemp)
		{
			machineTransform.position = tempTransform.position;
			machineTransform.rotation = tempTransform.rotation;
		}
	}

	public void ApplyTransform(Vector3 pos, Quaternion rot, out Vector3 newPos, out Quaternion newRot)
	{
		bool flag = false;
		if (tempTransform == null)
		{
			CreateTempTransform(pos, rot);
			flag = true;
		}
		else
		{
			tempTransform.position = pos;
			tempTransform.rotation = rot;
		}
		ApplyTransform(tempTransform, true);
		newPos = tempTransform.position;
		newRot = tempTransform.rotation;
		if (flag)
		{
			DestroyTempTransform();
		}
	}

	public void UndoTransform(Vector3 pos, Quaternion rot, out Vector3 newPos, out Quaternion newRot)
	{
		bool flag = false;
		if (tempTransform == null)
		{
			CreateTempTransform(pos, rot);
			flag = true;
		}
		else
		{
			tempTransform.position = pos;
			tempTransform.rotation = rot;
		}
		UndoTransform(tempTransform, true);
		newPos = tempTransform.position;
		newRot = tempTransform.rotation;
		if (flag)
		{
			DestroyTempTransform();
		}
	}

	private void CreateTempTransform(Vector3 pos, Quaternion rot)
	{
		GameObject gameObject = new GameObject("Transformation Helper");
		tempTransform = gameObject.transform;
		tempTransform.position = pos;
		tempTransform.rotation = rot;
	}

	private void DestroyTempTransform()
	{
		Object.DestroyImmediate(tempTransform.gameObject);
	}
}
