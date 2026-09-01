using UnityEngine;

public class NetworkHarpoon : NetworkProjectile
{
	public HarpoonTrigger harpoonTrigger;

	public ProjectileInfo info;

	private ServerMachine machine;

	private HarpoonController controller;

	public override void Despawn(byte[] despawnInfo)
	{
		base.Despawn(despawnInfo);
		if (controller == null || controller.originalHarpoon == null)
		{
			return;
		}
		controller.originalHarpoon.gameObject.SetActive(true);
		OverrideHarpoon(controller.originalHarpoon);
		if (info.SimPhysics)
		{
			info.Rigidbody.isKinematic = true;
			harpoonTrigger.harpoonTrigger.enabled = false;
			harpoonTrigger.harpoonCollider.enabled = false;
			if (harpoonTrigger.harpoonJointcurrent != null)
			{
				Object.Destroy(harpoonTrigger.harpoonJointcurrent);
			}
		}
		harpoonTrigger.OnAttach = null;
		harpoonTrigger.attached = false;
		harpoonTrigger.stopSelfPropulsion = true;
	}

	private void OverrideHarpoon(HarpoonTrigger harpoonScript)
	{
		harpoonScript.controller = controller;
		controller.harpoonScript = harpoonScript;
		controller.harpoonLoaded = harpoonScript == controller.originalHarpoon;
		controller.endPoint = harpoonScript.transform;
		Transform transform = harpoonScript.transform.Find("Vis/RopeEndPoint");
		if (transform != null)
		{
			controller.endRopePoint = transform;
		}
	}

	public void Repair(HarpoonTrigger harpoonScript)
	{
		Transform transform = ProjectileManager.Instance.projectilePrefabs[5].transform.FindChild("Vis");
		GameObject gameObject = (GameObject)Object.Instantiate(transform.gameObject, harpoonScript.transform);
		gameObject.name = "Vis";
		gameObject.transform.localPosition = transform.localPosition;
		gameObject.transform.localRotation = transform.localRotation;
		gameObject.transform.localScale = transform.localScale;
		controller.endRopePoint = gameObject.transform.GetChild(0);
		harpoonScript.particleOnCollide[0] = gameObject.transform.GetComponentInChildren<ParticleSystem>();
		harpoonScript.visual = gameObject.transform;
	}

	public override void Spawn(uint frame, ushort playerId, byte[] spawnInfo, bool explode = false)
	{
		int num = 0;
		base.playerId = playerId;
		SetParentMachine(playerId);
		Vector3 vec;
		NetworkCompression.DecompressPosition(spawnInfo, num, out vec);
		num += 6;
		Quaternion rot;
		NetworkCompression.DecompressRotation(spawnInfo, num, out rot);
		num += 7;
		Vector3 vector = vec;
		trackTransform.position = vector;
		position = vector;
		Quaternion quaternion = rot;
		trackTransform.rotation = quaternion;
		rotation = quaternion;
		if (num >= spawnInfo.Length)
		{
			return;
		}
		int num2 = (int)NetworkCompression.ReadUInt(false, spawnInfo, num);
		machine = projectileInfo.ParentMachine as ServerMachine;
		BlockBehaviour block;
		if (machine.GetBlockFromIndex(num2, out block))
		{
			if (block.Prefab.Type == BlockType.Harpoon && block.hasSimBlock)
			{
				controller = block.SimBlock as HarpoonController;
				blockBehaviour = controller;
				controller.originalHarpoon.gameObject.SetActive(false);
				controller.ShootSFX();
				base.transform.localScale = controller.transform.localScale;
				if (harpoonTrigger.transform.FindChild("Vis") == null)
				{
					Repair(harpoonTrigger);
				}
				OverrideHarpoon(harpoonTrigger);
			}
		}
		else
		{
			Debug.LogError("Couldn't find block " + num2 + " on machine " + machine.PlayerID + "!");
		}
	}
}
