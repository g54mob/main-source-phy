using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements;

namespace Kamgam.SettingsGenerator
{
	public abstract class SettingResolverForVisualElement : SettingResolver, ISettingResolver
	{
		[CompilerGenerated]
		private sealed class _003CRefreshDelayedAsync_003Ed__13 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public SettingResolverForVisualElement _003C_003E4__this;

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

		public static string SettingsClassNamePrefix;

		public static string SettingsClassNameSeparator;

		public string BindingClass;

		protected UIDocument _document;

		protected VisualElement _visualElement;

		public UIDocument Document => null;

		public VisualElement VisualElement
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public static bool HasSettingClass(VisualElement element)
		{
			return false;
		}

		public static string GetSettingClassName(VisualElement element)
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

		public void BindTo(VisualElement element)
		{
		}

		public void Unbind()
		{
		}

		public override void OnDisable()
		{
		}

		protected virtual void resetUIElements()
		{
		}

		public override void OnDestroy()
		{
		}
	}
}
