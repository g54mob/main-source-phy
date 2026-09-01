using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements;

namespace Kamgam.LocalizationForSettings.UIElements
{
	public abstract class LocalizeVisualElement : LocalizeBase
	{
		[CompilerGenerated]
		private sealed class _003CRefreshDelayedAsync_003Ed__13 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public LocalizeVisualElement _003C_003E4__this;

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
			public _003CRefreshDelayedAsync_003Ed__13(int _003C_003E1__state)
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

		public static string LocalizationClassNamePrefix;

		public string BindingClass;

		protected UIDocument _document;

		protected VisualElement _visualElement;

		public UIDocument Document => null;

		public VisualElement VisualElement => null;

		public static bool HasLocalizationClass(VisualElement element)
		{
			return false;
		}

		public static string GetLocalizationClassName(VisualElement element)
		{
			return null;
		}

		public abstract Type GetElementType();

		public VisualElement GetBindingClassElement()
		{
			return null;
		}

		protected virtual void detachFromPanel(DetachFromPanelEvent evt)
		{
		}

		[IteratorStateMachine(typeof(_003CRefreshDelayedAsync_003Ed__13))]
		protected virtual IEnumerator RefreshDelayedAsync()
		{
			return null;
		}

		protected virtual VisualElement getFinalElement(VisualElement ele)
		{
			return null;
		}

		public virtual void BindTo(VisualElement element)
		{
		}

		public virtual void Unbind()
		{
		}

		public override void OnDisable()
		{
		}

		protected virtual void resetUIElements()
		{
		}

		public void OnDestroy()
		{
		}
	}
}
