using System;
using System.Collections.Generic;
using UnityEngine;

public class MachineDamageController : MonoBehaviour
{
	private int intactCount;

	private List<BlockBehaviour> intactBlocks;

	private float totalDamage;

	private float currentDamage;

	private Action<float> damageFunc;

	private BlockLinkManager linkManager;

	private bool registerDamage;

	private float blockWeight = 0.25f;

	private float intactWeight = 0.75f;

	protected void Awake()
	{
		intactBlocks = new List<BlockBehaviour>();
		registerDamage = true;
	}

	public void Init(Action<float> damageCallback, BlockLinkManager blockLinkManager)
	{
		damageFunc = damageCallback;
		linkManager = blockLinkManager;
	}

	public void Toggle(bool toggle)
	{
		registerDamage = toggle;
	}

	public void AddTotalDamage(float amount)
	{
		totalDamage += amount;
	}

	public void RemoveTotalDamage(float amount)
	{
		totalDamage -= amount;
	}

	public void ResetTotalDamage()
	{
		totalDamage = 0f;
	}

	public void Reset()
	{
		currentDamage = 0f;
		intactBlocks.Clear();
	}

	public void SaveIntactBlocks()
	{
		intactCount = intactBlocks.Count;
	}

	public void RegisterBlock(BlockBehaviour block)
	{
		block.isIntact = false;
		if (!registerDamage)
		{
			return;
		}
		switch (block.Prefab.blockDamageSetting)
		{
		case DamageIgnoreSetting.Ignore:
			return;
		case DamageIgnoreSetting.JointOnly:
		{
			bool flag = false;
			List<BlockLink> neighbours = linkManager.GetNeighbours(block.NodeIndex);
			if (neighbours != null)
			{
				for (int i = 0; i < neighbours.Count; i++)
				{
					BlockLink blockLink = neighbours[i];
					for (int j = 0; j < blockLink.Triggers.Count; j++)
					{
						BlockTrigger blockTrigger = blockLink.Triggers[j];
						if (!blockTrigger.isDynamic || blockTrigger.isOwnLink)
						{
							flag = true;
							break;
						}
					}
				}
			}
			if (!flag)
			{
				return;
			}
			break;
		}
		}
		block.isIntact = true;
		intactBlocks.Add(block);
	}

	private void UpdateDamage()
	{
		float num = intactCount - intactBlocks.Count;
		float num2 = num / (float)intactCount;
		float obj;
		if (totalDamage > 0f)
		{
			float num3 = currentDamage / totalDamage;
			obj = num3 * blockWeight + num2 * intactWeight;
		}
		else
		{
			obj = num2;
		}
		damageFunc(obj);
	}

	public void ApplyBlockDamage(BlockBehaviour block, float damageAmount)
	{
		if (registerDamage && block.isIntact)
		{
			currentDamage += damageAmount;
			UpdateDamage();
		}
	}

	public void ApplyJointDamage(float damageAmount)
	{
		if (registerDamage)
		{
			currentDamage += damageAmount;
			UpdateDamage();
		}
	}

	public void ApplyDamage(BlockBehaviour block, MachineDamageType damageType)
	{
		if (!registerDamage || !block.isIntact)
		{
			return;
		}
		bool flag = false;
		for (int i = 0; i < block.Prefab.blockDamageTypes.Length; i++)
		{
			if (block.Prefab.blockDamageTypes[i] == damageType)
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			BlockHealthBar component = block.GetComponent<BlockHealthBar>();
			if (component != null)
			{
				currentDamage += component.health;
			}
			block.isIntact = false;
			intactBlocks.Remove(block);
			UpdateDamage();
		}
	}
}
