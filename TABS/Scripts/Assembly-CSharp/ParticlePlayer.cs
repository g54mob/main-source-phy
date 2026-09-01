using UnityEngine;

public class ParticlePlayer : ServicePrefab
{
	public VisualEffect[] effects;

	public void PlayEffect(int effectID, Vector3 position, Vector3 effectForward, SkinnedMeshRenderer skinnedMesh = null)
	{
		if (effectID < 0 || effectID >= effects.Length)
		{
			return;
		}
		effects[effectID].effectRoot.transform.position = position;
		effects[effectID].effectRoot.transform.rotation = Quaternion.LookRotation(effectForward);
		for (int i = 0; i < effects[effectID].particleEffects.Length; i++)
		{
			if ((bool)skinnedMesh)
			{
				ParticleSystem.ShapeModule shape = effects[effectID].particleEffects[i].part.shape;
				shape.skinnedMeshRenderer = skinnedMesh;
			}
			effects[effectID].particleEffects[i].part.Emit(effects[effectID].particleEffects[i].particlesToEmit);
		}
	}
}
