using UnityEngine;

public class SurfaceFragmentProjectile : NetworkProjectile
{
	private BuildSurface surface;

	private Fragment fragment;

	private ServerMachine machine;

	private int fragmentIndex;

	private bool isVisible;

	public override void ResetEntity()
	{
		if (!isVisible)
		{
			Debug.LogError("Missing transform for SurfaceFragmentProjectile!");
			posTracker.SetData(baseInterval, Vector3.zero);
			rotTracker.SetData(baseInterval, Quaternion.identity);
			lastPosFrame = (lastRotFrame = 0u);
		}
		else
		{
			Vector3 vec = trackTransform.position;
			posTracker.SetData(baseInterval, vec);
			Quaternion rot = trackTransform.rotation;
			rotTracker.SetData(baseInterval, rot);
			lastPosFrame = (lastRotFrame = 0u);
		}
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
		isVisible = false;
		if (num >= spawnInfo.Length)
		{
			return;
		}
		int num2 = (int)NetworkCompression.ReadUInt(false, spawnInfo, num);
		num += 4;
		fragmentIndex = spawnInfo[num++];
		machine = projectileInfo.ParentMachine as ServerMachine;
		BlockBehaviour block;
		if (machine.GetBlockFromIndex(num2, out block))
		{
			if (block.Prefab.Type != BlockType.BuildSurface || !block.hasSimBlock)
			{
				return;
			}
			surface = block.SimBlock as BuildSurface;
			if (!(surface.VisualController as SurfaceVisualController).hasBroken)
			{
				surface.OnRemoteBreak();
			}
			fragment = surface.FragmentController.fragments[fragmentIndex];
			Transform transform = (trackTransform = fragment.Object.transform);
			fragment.Projectile = this;
			isVisible = true;
			if (!surface.SimPhysics)
			{
				projectileInfo.MeshRenderer = fragment.Renderer;
				if (!fragment.Object.activeSelf)
				{
					fragment.Object.SetActive(true);
				}
				transform.SetParent(machine.SimulationMachine.transform, false);
				Vector3 vector = vec;
				trackTransform.position = vector;
				position = vector;
				Quaternion quaternion = rot;
				trackTransform.rotation = quaternion;
				rotation = quaternion;
				trackTransform.localScale = block.transform.localScale;
			}
		}
		else
		{
			Debug.LogError("Couldn't find block " + num2 + " on machine " + machine.PlayerID + "!");
		}
	}

	public override void Despawn(byte[] despawnInfo)
	{
		if (isVisible && surface != null && !surface.SimPhysics)
		{
			surface.OnRemoteFragmentBreak(fragmentIndex);
		}
	}

	public override bool UpdateEntity(float delta)
	{
		if (!isVisible || surface == null)
		{
			return false;
		}
		return base.UpdateEntity(delta);
	}

	public override bool IsChildOf(Transform obj)
	{
		return trackTransform.IsChildOf(obj);
	}
}
