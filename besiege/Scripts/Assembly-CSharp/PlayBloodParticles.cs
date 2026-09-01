using UnityEngine;

public class PlayBloodParticles : MonoBehaviour
{
	public ParticleSystem[] systems;

	private void Awake()
	{
		for (int i = 0; i < systems.Length; i++)
		{
			systems[i].startColor = StatMaster.BloodColor;
			systems[i].GetComponent<ParticleSystemRenderer>().material.SetColor("_TintColor", StatMaster.BloodColor);
			systems[i].Play();
		}
	}
}
