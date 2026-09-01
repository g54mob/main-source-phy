using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Localisation;
using SleepyNodes;
using UnityEngine;

[DisallowMultipleComponent]
public class FireMission : MonoBehaviour
{
	public class TimerValue
	{
		public float InitialSeconds;

		public float CurrentSeconds;

		public double StartedAt;
	}

	[CompilerGenerated]
	private sealed class _003CInternal_MoveEntity_003Ed__36 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public FireMission _003C_003E4__this;

		public Vector3 worldPos;

		public MapEntity entity;

		public float timespan;

		public double endsAt;

		public double startedAt;

		private Vector3 _003CdesiredLocation_003E5__2;

		private Vector3 _003CstartingLocation_003E5__3;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[DebuggerHidden]
		public _003CInternal_MoveEntity_003Ed__36(int _003C_003E1__state)
		{
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	[Header("Identity & Randomization")]
	public bool useFixedSeed;

	public int fixedSeed;

	[Header("Coordinate Root")]
	public RectTransform coordinateRoot;

	public EntityLocation POI_Prefab;

	[Header("Grid Settings")]
	public float cellWidth;

	public float cellHeight;

	public bool yIncreasesUp;

	public float distanceToKmScale;

	[Header("Options")]
	public bool clearSpawnedMarkers;

	public bool DebugLogs;

	[Header("Fallback Behavior")]
	public bool selectOnlyActivePoints;

	public bool useAlternateTextWhenNoActive;

	public string altTextNoActiveTarget;

	public string altTextNoActiveEnemy;

	public string altTextNoActiveAlly;

	public string altTextNoActiveOptionalTarget;

	[Header("Runtime Data")]
	public int seed;

	public Dictionary<string, MapEntity> Entities;

	public Dictionary<string, TimerValue> RunningTimers;

	public List<ImpactGraph> RunningImpactGraphs;

	public static FireMission Instance { get; private set; }

	private void OnValidate()
	{
	}

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDestroy()
	{
	}

	public void GenerateMission()
	{
	}

	public Vector3[] GetGridBounds()
	{
		return null;
	}

	private void Update()
	{
	}

	public Vector2 ToLocalSpace(Vector3 worldPos)
	{
		return default(Vector2);
	}

	public MapEntity CreateMapEntity(string id, TextIdentifier name, int entityIDIndex, Vector3 worldPos, EntityRoles role, int health, int armour, int stars, MapEntityStates startingState, string icon)
	{
		return null;
	}

	public void MoveMapEntity(MapEntity entity, Vector3 worldPos, bool continousMovement, float timespan)
	{
	}

	public void MoveMapEntity(MapEntity entity, Vector3 worldPos, bool continousMovement, float timespan, double startedAt, double endsAt)
	{
	}

	[IteratorStateMachine(typeof(_003CInternal_MoveEntity_003Ed__36))]
	public IEnumerator Internal_MoveEntity(MapEntity entity, Vector3 worldPos, float timespan, double startedAt, double endsAt)
	{
		return null;
	}

	public void RegisterMapEntity(MapEntity entity)
	{
	}

	private void SpawnRuntimeObjectForEntity(MapEntity entity)
	{
	}

	public string GetNoActiveAlternateTextForRoles(HashSet<EntityRoles> roles)
	{
		return null;
	}

	public bool TryGetMapEntity(string id, out MapEntity entity)
	{
		entity = null;
		return false;
	}

	public void ProcessNotification(string notifID)
	{
	}

	public void ProcessEvent(EventNode.EventData evt)
	{
	}

	public void SetEntityState(MapEntity entity, MapEntityStates newState)
	{
	}

	private void AutoAssignCoordinateRootIfNeeded()
	{
	}

	private void ClearSpawnedMarkersIfNeeded()
	{
	}

	internal Vector2 SampleAreaPosition(RectTransform zone, System.Random rng)
	{
		return default(Vector2);
	}

	internal Vector3 RandomPointWorldInside(RectTransform zone, System.Random rng)
	{
		return default(Vector3);
	}

	public void PositionInRootSpace(GameObject go, Vector2 rootLocalPos)
	{
	}
}
