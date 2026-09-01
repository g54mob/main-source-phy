using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/AI/AIBrain")]
	public class AIBrain : MonoBehaviour
	{
		[Header("Debug")]
		[MMReadOnly]
		public GameObject Owner;

		public List<AIState> States;

		[MMReadOnly]
		public float TimeInThisState;

		[MMReadOnly]
		public Transform Target;

		[MMReadOnly]
		public Vector3 _lastKnownTargetPosition;

		[Header("State")]
		public bool BrainActive;

		public bool ResetBrainOnStart;

		public bool ResetBrainOnEnable;

		[Header("Frequencies")]
		public float ActionsFrequency;

		public float DecisionFrequency;

		public bool RandomizeFrequencies;

		[MMVector(new string[] { "min", "max" })]
		public Vector2 RandomActionFrequency;

		[MMVector(new string[] { "min", "max" })]
		public Vector2 RandomDecisionFrequency;

		protected AIDecision[] _decisions;

		protected AIAction[] _actions;

		protected float _lastActionsUpdate;

		protected float _lastDecisionsUpdate;

		protected AIState _initialState;

		protected AIState _newState;

		public virtual AIState CurrentState { get; protected set; }

		public virtual AIAction[] GetAttachedActions()
		{
			return null;
		}

		public virtual AIDecision[] GetAttachedDecisions()
		{
			return null;
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void Awake()
		{
		}

		protected virtual void Start()
		{
		}

		protected virtual void Update()
		{
		}

		public virtual void TransitionToState(string newStateName)
		{
		}

		protected virtual void OnExitState()
		{
		}

		protected virtual void InitializeDecisions()
		{
		}

		protected virtual void InitializeActions()
		{
		}

		protected AIState FindState(string stateName)
		{
			return null;
		}

		protected virtual void StoreLastKnownPosition()
		{
		}

		public virtual void ResetBrain()
		{
		}

		[ContextMenu("Delete unused actions and decisions")]
		public virtual void DeleteUnusedActionsAndDecisions()
		{
		}
	}
}
