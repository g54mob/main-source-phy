using Landfall.TABS;
using UnityEngine;

public class TargetObject : MonoBehaviour
{
	[SerializeField]
	private Team m_team;

	[SerializeField]
	private float m_maxHealth = 100f;

	private float m_currentHealth;

	[SerializeField]
	private float m_thickness = 1f;

	[SerializeField]
	private float m_targetPriority = 1f;

	[SerializeField]
	private Transform m_transformToTarget;

	[SerializeField]
	private float m_attackRange = 1f;

	[SerializeField]
	private float m_preferredRange = 1f;

	public Team Team
	{
		get
		{
			return m_team;
		}
		set
		{
			m_team = value;
		}
	}

	public float MaxHealth
	{
		get
		{
			return m_maxHealth;
		}
		set
		{
			m_maxHealth = value;
		}
	}

	public float CurrentHealth
	{
		get
		{
			return m_currentHealth;
		}
		set
		{
			m_currentHealth = value;
		}
	}

	public float Thickness
	{
		get
		{
			return m_thickness;
		}
		set
		{
			m_thickness = value;
		}
	}

	public float TargetPriority
	{
		get
		{
			return m_targetPriority;
		}
		set
		{
			m_targetPriority = value;
		}
	}

	public Transform TransformToTarget => m_transformToTarget;

	public float AttackRange
	{
		get
		{
			return m_attackRange;
		}
		set
		{
			m_attackRange = value;
		}
	}

	public float PreferredRange
	{
		get
		{
			return m_preferredRange;
		}
		set
		{
			m_preferredRange = value;
		}
	}

	private void Awake()
	{
		m_currentHealth = m_maxHealth;
	}
}
