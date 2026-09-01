using System;
using UnityEngine;

public class BloodOnTexture : SimBehaviour
{
	public Renderer[] myMaterials;

	public bool isBloody;

	public ParticleSystem bloodParticles;

	public void BloodSplatter()
	{
		if (!base.isSimulating || basicInfo.infoType != BasicInfo.BasicInfoType.Block)
		{
			return;
		}
		BlockBehaviour blockBehaviour = basicInfo as BlockBehaviour;
		if (blockBehaviour.Prefab.hasBVC)
		{
			blockBehaviour.VisualController.SetBloodyLevel(1f, StatMaster.BloodColor);
			if (StatMaster.isHosting && base.SimPhysics && !StatMaster.IsLevelEditorOnly)
			{
				if (base.NetBlock != null)
				{
					base.NetBlock.Event(NetworkEntity.EntityEvent.SetBloodyLevel);
				}
				else
				{
					Debug.LogError("Missing NetworkBlock on '" + Machine.GetObjectPath(base.gameObject) + "'? " + Environment.StackTrace, base.gameObject);
				}
			}
		}
		isBloody = true;
		if (bloodParticles != null)
		{
			bloodParticles.Play();
		}
	}
}
