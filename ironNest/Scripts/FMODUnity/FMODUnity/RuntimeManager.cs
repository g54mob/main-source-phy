using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using AOT;
using FMOD;
using FMOD.Studio;
using UnityEngine;

namespace FMODUnity
{
	[AddComponentMenu(null)]
	public class RuntimeManager : MonoBehaviour
	{
		private struct LoadedBank
		{
			public Bank Bank;

			public int RefCount;
		}

		private class GuidComparer : IEqualityComparer<GUID>
		{
			bool IEqualityComparer<GUID>.Equals(GUID x, GUID y)
			{
				return false;
			}

			int IEqualityComparer<GUID>.GetHashCode(GUID obj)
			{
				return 0;
			}
		}

		private class AttachedInstance
		{
			public EventInstance instance;

			public Transform transform;

			public Rigidbody rigidBody;

			public Vector3 lastFramePosition;

			public bool nonRigidbodyVelocity;

			public Rigidbody2D rigidBody2D;
		}

		[CompilerGenerated]
		private sealed class _003CBanksToLoad_003Ed__66 : IEnumerable<string>, IEnumerable, IEnumerator<string>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private string _003C_003E2__current;

			private int _003C_003El__initialThreadId;

			private Settings fmodSettings;

			public Settings _003C_003E3__fmodSettings;

			private List<string>.Enumerator _003C_003E7__wrap1;

			private string _003CmasterBankFileName_003E5__3;

			string IEnumerator<string>.Current
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
			public _003CBanksToLoad_003Ed__66(int _003C_003E1__state)
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

			private void _003C_003Em__Finally2()
			{
			}

			private void _003C_003Em__Finally3()
			{
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}

			[DebuggerHidden]
			IEnumerator<string> IEnumerable<string>.GetEnumerator()
			{
				return null;
			}

			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return null;
			}
		}

		public const string BankStubPrefix = "bank stub:";

		private static SystemNotInitializedException initException;

		private static RuntimeManager instance;

		private Platform currentPlatform;

		private DEBUG_CALLBACK debugCallback;

		private FMOD.SYSTEM_CALLBACK errorCallback;

		private FMOD.Studio.System studioSystem;

		private FMOD.System coreSystem;

		private DSP mixerHead;

		private bool isMuted;

		private Dictionary<GUID, EventDescription> cachedDescriptions;

		private Dictionary<string, LoadedBank> loadedBanks;

		private List<string> sampleLoadRequests;

		private List<AttachedInstance> attachedInstances;

		private bool listenerWarningIssued;

		protected bool isOverlayEnabled;

		private FMODRuntimeManagerOnGUIHelper overlayDrawer;

		private Rect windowRect;

		private string lastDebugText;

		private float lastDebugUpdate;

		private int loadingBanksRef;

		private static byte[] masterBusPrefix;

		private static byte[] eventSet3DAttributes;

		private static byte[] systemGetBus;

		public static bool IsMuted => false;

		private static RuntimeManager Instance => null;

		public static FMOD.Studio.System StudioSystem => default(FMOD.Studio.System);

		public static FMOD.System CoreSystem => default(FMOD.System);

		public static bool IsInitialized => false;

		public static bool HaveAllBanksLoaded => false;

		public static bool HaveMasterBanksLoaded => false;

		static RuntimeManager()
		{
		}

		[MonoPInvokeCallback(typeof(DEBUG_CALLBACK))]
		private static RESULT DEBUG_CALLBACK(DEBUG_FLAGS flags, IntPtr filePtr, int line, IntPtr funcPtr, IntPtr messagePtr)
		{
			return default(RESULT);
		}

		[MonoPInvokeCallback(typeof(FMOD.SYSTEM_CALLBACK))]
		private static RESULT ERROR_CALLBACK(IntPtr system, FMOD.SYSTEM_CALLBACK_TYPE type, IntPtr commanddata1, IntPtr commanddata2, IntPtr userdata)
		{
			return default(RESULT);
		}

		private void CheckInitResult(RESULT result, string cause)
		{
		}

		private void ReleaseStudioSystem()
		{
		}

		private RESULT Initialize()
		{
			return default(RESULT);
		}

		private int GetChannelCountForFormat(CodecType format)
		{
			return 0;
		}

		private static void SetThreadAffinities(Platform platform)
		{
		}

		private void Update()
		{
		}

		private static AttachedInstance FindOrAddAttachedInstance(EventInstance instance, Transform transform, ATTRIBUTES_3D attributes)
		{
			return null;
		}

		public static void AttachInstanceToGameObject(EventInstance instance, GameObject gameObject, bool nonRigidbodyVelocity = false)
		{
		}

		[Obsolete("This overload has been deprecated in favor of passing a GameObject instead of a Transform.", false)]
		public static void AttachInstanceToGameObject(EventInstance instance, Transform transform, bool nonRigidbodyVelocity = false)
		{
		}

		public static void AttachInstanceToGameObject(EventInstance instance, GameObject gameObject, Rigidbody rigidBody)
		{
		}

		[Obsolete("This overload has been deprecated in favor of passing a GameObject instead of a Transform.", false)]
		public static void AttachInstanceToGameObject(EventInstance instance, Transform transform, Rigidbody rigidBody)
		{
		}

		public static void AttachInstanceToGameObject(EventInstance instance, GameObject gameObject, Rigidbody2D rigidBody2D)
		{
		}

		[Obsolete("This overload has been deprecated in favor of passing a GameObject instead of a Transform.", false)]
		public static void AttachInstanceToGameObject(EventInstance instance, Transform transform, Rigidbody2D rigidBody2D)
		{
		}

		public static void DetachInstanceFromGameObject(EventInstance instance)
		{
		}

		internal void ExecuteOnGUI()
		{
		}

		private void Start()
		{
		}

		private void UpdateDebugText()
		{
		}

		private void DrawDebugOverlay(int windowID)
		{
		}

		private void OnDestroy()
		{
		}

		private void OnApplicationPause(bool pauseStatus)
		{
		}

		private static void ReferenceLoadedBank(string bankName, bool loadSamples)
		{
		}

		private void RegisterLoadedBank(LoadedBank loadedBank, string bankPath, string bankName, bool loadSamples, RESULT loadResult)
		{
		}

		private void ExecuteSampleLoadRequestsIfReady()
		{
		}

		public static void LoadBank(string bankName, bool loadSamples = false)
		{
		}

		private static void LoadBank(string bankName, bool loadSamples, string bankId)
		{
		}

		public static void LoadBank(TextAsset asset, bool loadSamples = false)
		{
		}

		private static void LoadBank(TextAsset asset, bool loadSamples, string bankId)
		{
		}

		private void LoadBanks(Settings fmodSettings)
		{
		}

		[IteratorStateMachine(typeof(_003CBanksToLoad_003Ed__66))]
		private IEnumerable<string> BanksToLoad(Settings fmodSettings)
		{
			return null;
		}

		public static void UnloadBank(string bankName)
		{
		}

		public static void UnloadBank(TextAsset asset)
		{
		}

		[Obsolete("[FMOD] Deprecated. Use AnySampleDataLoading instead.")]
		public static bool AnyBankLoading()
		{
			return false;
		}

		public static bool AnySampleDataLoading()
		{
			return false;
		}

		[Obsolete("[FMOD] Deprecated. Use WaitForAllSampleLoading instead.")]
		public static void WaitForAllLoads()
		{
		}

		public static void WaitForAllSampleLoading()
		{
		}

		public static GUID PathToGUID(string path)
		{
			return default(GUID);
		}

		public static EventReference PathToEventReference(string path)
		{
			return default(EventReference);
		}

		public static EventInstance CreateInstance(EventReference eventReference)
		{
			return default(EventInstance);
		}

		public static EventInstance CreateInstance(string path)
		{
			return default(EventInstance);
		}

		public static EventInstance CreateInstance(GUID guid)
		{
			return default(EventInstance);
		}

		public static void PlayOneShot(EventReference eventReference, Vector3 position = default(Vector3))
		{
		}

		public static void PlayOneShot(string path, Vector3 position = default(Vector3))
		{
		}

		public static void PlayOneShot(GUID guid, Vector3 position = default(Vector3))
		{
		}

		public static void PlayOneShotAttached(EventReference eventReference, GameObject gameObject)
		{
		}

		public static void PlayOneShotAttached(string path, GameObject gameObject)
		{
		}

		public static void PlayOneShotAttached(GUID guid, GameObject gameObject)
		{
		}

		private static bool CreateInstanceWithinMaxDistance(GUID guid, Vector3 position, out EventInstance instance)
		{
			instance = default(EventInstance);
			return false;
		}

		public static EventDescription GetEventDescription(EventReference eventReference)
		{
			return default(EventDescription);
		}

		public static EventDescription GetEventDescription(string path)
		{
			return default(EventDescription);
		}

		public static EventDescription GetEventDescription(GUID guid)
		{
			return default(EventDescription);
		}

		public static void SetListenerLocation(GameObject gameObject, Rigidbody rigidBody, GameObject attenuationObject = null)
		{
		}

		public static void SetListenerLocation(int listenerIndex, GameObject gameObject, Rigidbody rigidBody, GameObject attenuationObject = null)
		{
		}

		public static void SetListenerLocation(GameObject gameObject, Rigidbody2D rigidBody2D, GameObject attenuationObject = null)
		{
		}

		public static void SetListenerLocation(int listenerIndex, GameObject gameObject, Rigidbody2D rigidBody2D, GameObject attenuationObject = null)
		{
		}

		public static void SetListenerLocation(int listenerIndex, GameObject gameObject, GameObject attenuationObject = null, Vector3 velocity = default(Vector3))
		{
		}

		public static void SetListenerLocation(GameObject gameObject, GameObject attenuationObject = null)
		{
		}

		public static void SetListenerLocation(int listenerIndex, GameObject gameObject, GameObject attenuationObject = null)
		{
		}

		public static Bus GetBus(string path)
		{
			return default(Bus);
		}

		public static VCA GetVCA(string path)
		{
			return default(VCA);
		}

		public static void PauseAllEvents(bool paused)
		{
		}

		public static void MuteAllEvents(bool muted)
		{
		}

		private static void ApplyMuteState()
		{
		}

		public static bool HasBankLoaded(string loadedBank)
		{
			return false;
		}

		private void SetOverlayPosition()
		{
		}
	}
}
