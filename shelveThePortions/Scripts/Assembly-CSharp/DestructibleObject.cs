using System;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
	[Header("Health")]
	[SerializeField]
	private float baseMaxHealth;

	[SerializeField]
	private float maxHealth;

	[SerializeField]
	private float currentHealth;

	[SerializeField]
	public bool isPlayer;

	public Action OnDestroyAction;

	public Action OnDamageAction;

	public Action OnHealthChangedAction;

	[Space(20f)]
	[SerializeField]
	private bool IsDebugOn;

	private void OnEnable()
	{
		currentHealth = maxHealth;
	}

	public void DoDamage(float damage)
	{
		if (!IsDead() && !(damage <= 0f))
		{
			currentHealth -= damage;
			if (currentHealth <= 0f)
			{
				currentHealth = 0f;
			}
			if (OnDamageAction != null)
			{
				OnDamageAction();
			}
			if (OnHealthChangedAction != null)
			{
				OnHealthChangedAction();
			}
			if (currentHealth == 0f && OnDestroyAction != null)
			{
				OnDestroyAction();
			}
		}
	}

	public void Heal(float amount)
	{
		currentHealth += amount;
		if (currentHealth > maxHealth)
		{
			currentHealth = maxHealth;
		}
		if (OnHealthChangedAction != null)
		{
			OnHealthChangedAction();
		}
	}

	public bool CanHeal()
	{
		if (maxHealth == 0f)
		{
			return false;
		}
		return currentHealth < maxHealth;
	}

	public bool IsDead()
	{
		return currentHealth <= 0f;
	}

	public float GetCurrnetHealth()
	{
		return currentHealth;
	}

	public float GetMaxHealth()
	{
		return maxHealth;
	}

	public float GetHealthPercent()
	{
		return currentHealth / maxHealth;
	}

	public void SetCurrnetHealth(float currentHealth)
	{
		this.currentHealth = currentHealth;
	}

	public void SetBaseMaxHealth(float health)
	{
		baseMaxHealth = health;
		maxHealth = health;
		currentHealth = maxHealth;
	}

	public void SetHealthPercentMultipler(float percent)
	{
		if (percent <= 0f)
		{
			currentHealth = 0f;
			maxHealth = 0f;
		}
		else
		{
			float num = currentHealth / maxHealth;
			maxHealth = baseMaxHealth * percent;
			currentHealth = maxHealth * num;
		}
		if (OnHealthChangedAction != null)
		{
			OnHealthChangedAction();
		}
	}

	public void Kill()
	{
		DoDamage(currentHealth);
	}
}
