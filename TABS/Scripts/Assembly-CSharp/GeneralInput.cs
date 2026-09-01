using System;
using Landfall.TABS_Input;
using TFBGames;
using UnityEngine;

public class GeneralInput : MonoBehaviour, IRemotelyControllable
{
	public bool hasControl = true;

	public Vector3 inputDirection;

	public bool shift;

	private Action inputUpdate;

	public bool AllowRotationChange = true;

	[HideInInspector]
	public Transform Direction;

	private DataHandler data;

	private PlayerActions m_playerActions;

	public bool IsRemotelyControlled { get; private set; }

	private void Awake()
	{
		Direction = base.transform.parent.GetComponentInChildren<DirectionObject>().transform;
		data = GetComponentInChildren<DataHandler>();
		m_playerActions = PlayerActions.Instance;
	}

	public void SetRotation(Quaternion rotation)
	{
		if (AllowRotationChange)
		{
			Direction.rotation = rotation;
		}
	}

	private void Update()
	{
		if (data.Dead || !data.isGrounded)
		{
			return;
		}
		if (hasControl)
		{
			inputDirection.x = m_playerActions.m_move.X;
			inputDirection.z = m_playerActions.m_move.Y;
			if (Mathf.Abs(inputDirection.x) + Mathf.Abs(inputDirection.z) > 1f)
			{
				inputDirection.Normalize();
			}
			inputDirection.x *= 2f;
			if (inputDirection.z < 0f)
			{
				inputDirection.z *= 2f;
			}
		}
		if (inputUpdate != null)
		{
			inputUpdate();
		}
	}

	public void AddInputUpdateAction(Action action)
	{
		inputUpdate = (Action)Delegate.Combine(inputUpdate, action);
	}

	public void SetPlayerActions(PlayerActions actions)
	{
		m_playerActions = actions;
	}

	public void SetIsRemotelyControlled(bool isRemotelyControlled)
	{
		IsRemotelyControlled = isRemotelyControlled;
	}
}
