using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

[DisallowMultipleComponent]
public class ImpactExplosionSpawner : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CSpawnExplosionNextFrame_003Ed__9 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ImpactExplosionSpawner _003C_003E4__this;

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
		public _003CSpawnExplosionNextFrame_003Ed__9(int _003C_003E1__state)
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

	[Tooltip("Prefab to instantiate for the explosion VFX.")]
	public GameObject explosionPrefab;

	[Tooltip("Optional: direct reference to the External Environment parent. If unset, the script will search by name.")]
	public Transform externalParent;

	[Tooltip("Name used to find the External Environment parent if externalParent is not set.")]
	public string externalEnvironmentName;

	[Tooltip("Automatically destroy the spawned VFX after this many seconds. 0 = do not auto-destroy.")]
	public float destroyAfterSeconds;

	[Header("Raycast surface snap (to avoid spawning inside mountains)")]
	[Tooltip("Enable raycasting to find environment surface along parent's local Z axis (local Z is treated as up/down).")]
	public bool enableSurfaceSnap;

	[Tooltip("Distance above the nominal local-Z to start the raycast (in parent's local Z units).")]
	public float raycastStartOffset;

	[Tooltip("Maximum distance to search for a surface from the start point (in world units).")]
	public float raycastMaxDistance;

	[Tooltip("Layer mask used by the raycast. Set to the layers that contain the terrain/environment.")]
	public LayerMask raycastLayerMask;

	private void OnEnable()
	{
	}

	[IteratorStateMachine(typeof(_003CSpawnExplosionNextFrame_003Ed__9))]
	private IEnumerator SpawnExplosionNextFrame()
	{
		return null;
	}
}
