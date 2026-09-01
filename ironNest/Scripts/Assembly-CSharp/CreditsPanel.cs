using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CreditsPanel : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CCheckVisibility_003Ed__31 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CreditsPanel _003C_003E4__this;

		private WaitForSecondsRealtime _003Cdelay_003E5__2;

		private Vector3[] _003CviewportCorners_003E5__3;

		private Vector3[] _003CchunkCorners_003E5__4;

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
		public _003CCheckVisibility_003Ed__31(int _003C_003E1__state)
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

	[CompilerGenerated]
	private sealed class _003CLoadCredits_003Ed__29 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CreditsPanel _003C_003E4__this;

		private float _003CaccumulatedHeight_003E5__2;

		private List<CreditsSectionConfig>.Enumerator _003C_003E7__wrap2;

		private CreditsSection _003Csection_003E5__4;

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
		public _003CLoadCredits_003Ed__29(int _003C_003E1__state)
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

		private void _003C_003Em__Finally1()
		{
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	[CompilerGenerated]
	private sealed class _003CRevealCreditsCoroutine_003Ed__32 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CreditsPanel _003C_003E4__this;

		private float _003Ctimer_003E5__2;

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
		public _003CRevealCreditsCoroutine_003Ed__32(int _003C_003E1__state)
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

	[SerializeField]
	private RectTransform _contentParent;

	[SerializeField]
	private ScrollRect _scrollRect;

	[SerializeField]
	private CanvasGroup _canvasGroup;

	[SerializeField]
	private GameObject _scrollbarObject;

	[SerializeField]
	private GameObject _mainMenu;

	[SerializeField]
	private GameObject _exitHandler;

	[Header("Scrolling")]
	[SerializeField]
	private float _autoScrollSpeed;

	[SerializeField]
	[Min(0f)]
	private float _delayBeforeAutoScroll;

	[SerializeField]
	private float _manualScrollSpeed;

	[SerializeField]
	private InputActionReference _manualScrollAction;

	[SerializeField]
	private InputActionReference _sectionSkipAction;

	[Header("Sections from files")]
	[SerializeField]
	private CreditsSection _sectionPrefab;

	[SerializeField]
	private int _maxLinesPerSectionChunk;

	[SerializeField]
	private float _spaceBetweenSections;

	[SerializeField]
	private List<CreditsSectionConfig> _sectionConfigs;

	[Space]
	[SerializeField]
	private UnityEvent _onCreditsDisplayed;

	[SerializeField]
	private UnityEvent _onCreditsHidden;

	private readonly List<CreditsSection> _sections;

	private float _lastInputTime;

	private bool _hasStartedLoadingCredits;

	private bool _hasLoadedCredits;

	private bool _isDisplayedFromMainMenu;

	private const float VISIBILITY_CHECK_INTERVAL = 0.16f;

	private const float VISIBILITY_MARGIN = 250f;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Update()
	{
	}

	public void Show(bool isFromMainMenu)
	{
	}

	[IteratorStateMachine(typeof(_003CLoadCredits_003Ed__29))]
	private IEnumerator LoadCredits()
	{
		return null;
	}

	private void RevealCredits()
	{
	}

	[IteratorStateMachine(typeof(_003CCheckVisibility_003Ed__31))]
	private IEnumerator CheckVisibility()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CRevealCreditsCoroutine_003Ed__32))]
	private IEnumerator RevealCreditsCoroutine()
	{
		return null;
	}

	private void SectionSkipAction_performed(InputAction.CallbackContext ctx)
	{
	}
}
