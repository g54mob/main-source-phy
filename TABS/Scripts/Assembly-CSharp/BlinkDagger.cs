using System.Collections;
using UnityEngine;

public class BlinkDagger : AttackEffect
{
	private MeleeWeapon weapon;

	public GameObject chatBubble;

	public ChatMessage[] messages;

	private ParticlePlayer m_particlePlayer;

	private void Awake()
	{
		m_particlePlayer = ServiceLocator.GetService<ParticlePlayer>();
	}

	private void Start()
	{
		weapon = GetComponent<MeleeWeapon>();
	}

	private IEnumerator Go(Rigidbody target)
	{
		yield return new WaitForSeconds(0.05f);
		BlinkEffect.Blink(weapon.connectedData, target, weapon);
	}

	public override void DoEffect(Rigidbody target, Vector3 targetDir)
	{
		if ((bool)weapon.connectedData)
		{
			m_particlePlayer.PlayEffect(1, weapon.connectedData.hip.position - Vector3.up, Vector3.up, weapon.connectedData.transform.root.GetComponentInChildren<SkinnedMeshRenderer>());
			StartCoroutine(Go(target));
		}
	}
}
