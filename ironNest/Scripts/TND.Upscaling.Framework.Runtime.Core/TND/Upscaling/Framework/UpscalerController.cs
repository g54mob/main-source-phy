using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace TND.Upscaling.Framework
{
	[RequireComponent(typeof(Camera))]
	public abstract class UpscalerController : MonoBehaviour
	{
		[Serializable]
		internal struct UpscalerSettingsPair
		{
			public string identifier;

			public UpscalerSettingsBase settings;
		}

		[CompilerGenerated]
		private sealed class _003CCUpdateSettingsCaches_003Ed__57 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UpscalerController _003C_003E4__this;

			private WaitForEndOfFrame _003CendOfFrame_003E5__2;

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
			public _003CCUpdateSettingsCaches_003Ed__57(int _003C_003E1__state)
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
		private sealed class _003CEnumerateUpscalerChain_003Ed__55 : IEnumerator<IUpscalerPlugin>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private IUpscalerPlugin _003C_003E2__current;

			public UpscalerController _003C_003E4__this;

			private int _003Ci_003E5__2;

			IUpscalerPlugin IEnumerator<IUpscalerPlugin>.Current
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
			public _003CEnumerateUpscalerChain_003Ed__55(int _003C_003E1__state)
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
		internal List<string> upscalerChain;

		[SerializeField]
		protected internal UpscalerQuality qualityMode;

		[SerializeField]
		protected internal bool enableSharpening;

		[SerializeField]
		[Range(0f, 1f)]
		protected internal float sharpness;

		[SerializeField]
		protected internal UpscalerInjectionPoint injectionPoint;

		[SerializeField]
		internal List<UpscalerSettingsPair> upscalerSettings;

		[Header("Reactive Mask")]
		[SerializeField]
		protected internal bool autoGenerateReactiveMask;

		[SerializeField]
		protected internal AutoReactiveSettings autoReactiveSettings;

		[SerializeField]
		protected internal bool enableCustomReactiveMask;

		[SerializeField]
		protected internal LayerMask customReactiveMaskLayer;

		protected internal abstract Vector2Int DisplaySize { get; }

		protected internal abstract Vector2Int MaxRenderSize { get; }

		protected internal virtual Vector2Int ScaledRenderSize => default(Vector2Int);

		protected internal float RenderScale => 0f;

		protected internal UpscalerContext UpscalerContext { get; set; }

		protected internal virtual bool EnableOpaqueOnlyCopy { get; internal set; }

		protected internal virtual Texture OpaqueOnlyTexture => null;

		protected internal virtual bool EnableAutoReactiveMask => false;

		protected internal virtual Texture AutoReactiveMask => null;

		protected internal virtual bool EnableCustomReactiveMask => false;

		protected internal virtual Texture CustomReactiveMask => null;

		protected internal GraphicsFormat ReactiveMaskFormat => default(GraphicsFormat);

		protected internal virtual bool ResetHistory => false;

		protected virtual void Awake()
		{
		}

		protected virtual void OnValidate()
		{
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDisable()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void LateUpdate()
		{
		}

		protected void ValidateQualityMode()
		{
		}

		protected UpscalerName GetPrimaryUpscalerName()
		{
			return default(UpscalerName);
		}

		internal string GetPrimaryUpscalerIdentifier()
		{
			return null;
		}

		protected bool SetPrimaryUpscaler(UpscalerName upscalerName)
		{
			return false;
		}

		protected bool SetPrimaryUpscaler(IUpscalerPlugin upscalerPlugin)
		{
			return false;
		}

		protected IUpscalerPlugin GetCurrentlyActiveUpscalerPlugin()
		{
			return null;
		}

		protected static void ForEachUpscalerPlugin(Action<IUpscalerPlugin> callback)
		{
		}

		protected internal bool TryGetUpscalerSettings(IUpscalerPlugin upscalerPlugin, out UpscalerSettingsBase settings)
		{
			settings = null;
			return false;
		}

		[IteratorStateMachine(typeof(_003CEnumerateUpscalerChain_003Ed__55))]
		internal IEnumerator<IUpscalerPlugin> EnumerateUpscalerChain()
		{
			return null;
		}

		protected static float GetScaleFactor(UpscalerQuality qualityMode)
		{
			return 0f;
		}

		[IteratorStateMachine(typeof(_003CCUpdateSettingsCaches_003Ed__57))]
		private IEnumerator CUpdateSettingsCaches()
		{
			return null;
		}
	}
}
