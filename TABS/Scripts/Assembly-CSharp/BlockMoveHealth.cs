using System;
using UnityEngine;

public class BlockMoveHealth : MonoBehaviour
{
	public float maxHealth = 500f;

	private float health;

	public float rechargeTime = 10f;

	private float counter;

	private BlockMove block;

	private void Start()
	{
		health = maxHealth;
		block = GetComponent<BlockMove>();
		BlockMove blockMove = block;
		blockMove.blockAction = (Action<GameObject, HitData>)Delegate.Combine(blockMove.blockAction, new Action<GameObject, HitData>(DoBlock));
	}

	public void DoBlock(GameObject proj, HitData hit)
	{
		ProjectileHit component = proj.GetComponent<ProjectileHit>();
		if ((bool)component)
		{
			health -= component.damage;
		}
		if (health <= 0f)
		{
			block.SetDisabled();
			counter = 0f;
		}
	}

	private void Update()
	{
		counter += Time.deltaTime;
		if (counter > rechargeTime && health <= 0f)
		{
			health = maxHealth;
			block.SetEnabled();
		}
	}
}
