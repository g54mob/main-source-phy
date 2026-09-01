using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class EntityLocation : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CReportLocationNextFrame_003Ed__46 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public EntityLocation _003C_003E4__this;

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
		public _003CReportLocationNextFrame_003Ed__46(int _003C_003E1__state)
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

	[Header("References")]
	public Image Image_Stars;

	public Sprite[] Sprites_Stars;

	public Image Image_Armour;

	public Sprite[] Sprites_Armour;

	public Image Image_Icon;

	public CanvasGroup VisibilityGroup;

	public GameObject VisualRoot;

	[Header("Reveal")]
	public bool UpdateVisualOnMove;

	public string RevealAreaTag;

	public float RectanglePadding;

	public bool StartWithVisualRootHidden;

	public float ScanWindowDurationSeconds;

	public float ScanIntervalSeconds;

	[Header("Events")]
	public UnityEvent<EntityLocation> OnDestroyed_Ally;

	public UnityEvent<EntityLocation> OnDestroyed;

	public UnityEvent<EntityLocation> OnTakeDamage_Ally;

	public UnityEvent<EntityLocation> OnTakeDamage;

	public UnityEvent<EntityLocation> OnImmuneToShellHit;

	public UnityEvent<EntityLocation> OnMove;

	public UnityEvent<EntityLocation> OnRevealed;

	[HideInInspector]
	public MapEntity Entity;

	[HideInInspector]
	public static Dictionary<string, MapEntityIcon> PossibleMapIcons;

	private static RectTransform _rootCanvasRect;

	private static bool _warnedNoCanvas;

	private static bool _warnedMissingRevealTag;

	private static Vector3 _lastRootPos;

	private static Quaternion _lastRootRot;

	private static Vector3 _lastRootScale;

	private static bool _rootTransformCached;

	private Vector3 _visualRootWorldPosition;

	private bool _hasVisualRootWorldPosition;

	private bool _hasReportedLocation;

	private bool _scanActive;

	private float _scanWindowEndTime;

	private float _nextScanTime;

	[HideInInspector]
	public Vector2 LocalPosition => default(Vector2);

	[HideInInspector]
	public event Action OnStateUpdated
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	public void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void LateUpdate()
	{
	}

	public void Init(MapEntity entity)
	{
	}

	private void OnDestroy()
	{
	}

	[IteratorStateMachine(typeof(_003CReportLocationNextFrame_003Ed__46))]
	private IEnumerator ReportLocationNextFrame()
	{
		return null;
	}

	private bool RecalculateAndRegister(bool forceRegister)
	{
		return false;
	}

	public bool TakeDamage(ShellDefinition shell, int damage, string shellInstanceId = "")
	{
		return false;
	}

	public void OnEntityMoved()
	{
	}

	public void OnEntityStateChanged(MapEntityStates oldState, MapEntityStates newState)
	{
	}

	private void OnImpact(Vector2 impactLocation, float impactRadius)
	{
	}

	private bool EvaluateRevealArea()
	{
		return false;
	}

	private bool CheckTaggedRectangles(Vector3 worldPos)
	{
		return false;
	}

	private void RevealVisualRoot()
	{
	}

	private void HideVisualRoot()
	{
	}

	private void KeepVisualRootLocked()
	{
	}

	public void StartScanWindow()
	{
	}

	public void StopScanWindow()
	{
	}

	private RectTransform ResolveRootCanvasRect()
	{
		return null;
	}

	private void CacheRootTransform(RectTransform rootRect)
	{
	}

	private static bool IsWorldPointInsideRectTransform(RectTransform rect, Vector3 worldPoint, float padding)
	{
		return false;
	}
}
