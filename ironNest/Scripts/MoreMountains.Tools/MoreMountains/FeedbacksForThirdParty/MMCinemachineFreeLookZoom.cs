using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using Unity.Cinemachine;
using UnityEngine;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/Cinemachine/MMCinemachineFreeLookZoom")]
	[RequireComponent(typeof(CinemachineCamera))]
	public class MMCinemachineFreeLookZoom : MonoBehaviour
	{
		[Header("Channel")]
		[MMFInspectorGroup("Shaker Settings", true, 3, false, false)]
		[Tooltip("whether to listen on a channel defined by an int or by a MMChannel scriptable object. Ints are simple to setup but can get messy and make it harder to remember what int corresponds to what. MMChannel scriptable objects require you to create them in advance, but come with a readable name and are more scalable")]
		public MMChannelModes ChannelMode;

		[Tooltip("the channel to listen to - has to match the one on the feedback")]
		[MMFEnumCondition("ChannelMode", new int[] { 0 })]
		public int Channel;

		[Tooltip("the MMChannel definition asset to use to listen for events. The feedbacks targeting this shaker will have to reference that same MMChannel definition to receive events - to create a MMChannel, right click anywhere in your project (usually in a Data folder) and go MoreMountains > MMChannel, then name it with some unique name")]
		[MMFEnumCondition("ChannelMode", new int[] { 1 })]
		public MMChannel MMChannelDefinition;

		[Header("Transition Speed")]
		[Tooltip("the animation curve to apply to the zoom transition")]
		public MMTweenType ZoomTween;

		[Header("Test Zoom")]
		[Tooltip("the mode to apply the zoom in when using the test button in the inspector")]
		public MMCameraZoomModes TestMode;

		[Tooltip("the target field of view to apply the zoom in when using the test button in the inspector")]
		public float TestFieldOfView;

		[Tooltip("the transition duration to apply the zoom in when using the test button in the inspector")]
		public float TestTransitionDuration;

		[Tooltip("the duration to apply the zoom in when using the test button in the inspector")]
		public float TestDuration;

		[MMFInspectorButton("TestZoom")]
		public bool TestZoomButton;

		protected CinemachineCamera _freeLookCamera;

		protected float _initialFieldOfView;

		protected MMCameraZoomModes _mode;

		protected bool _zooming;

		protected float _startFieldOfView;

		protected float _transitionDuration;

		protected float _duration;

		protected float _targetFieldOfView;

		protected float _delta;

		protected int _direction;

		protected float _reachedDestinationTimestamp;

		protected bool _destinationReached;

		protected float _elapsedTime;

		protected float _zoomStartedAt;

		public virtual TimescaleModes TimescaleMode { get; set; }

		public virtual float GetTime()
		{
			return 0f;
		}

		public virtual float GetDeltaTime()
		{
			return 0f;
		}

		protected virtual void Awake()
		{
		}

		protected virtual void Update()
		{
		}

		public virtual void Zoom(MMCameraZoomModes mode, float newFieldOfView, float transitionDuration, float duration, bool relative = false, MMTweenType tweenType = null)
		{
		}

		protected virtual void TestZoom()
		{
		}

		public virtual void OnCameraZoomEvent(MMCameraZoomModes mode, float newFieldOfView, float transitionDuration, float duration, MMChannelData channelData, bool useUnscaledTime, bool stop = false, bool relative = false, bool restore = false, MMTweenType tweenType = null)
		{
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDisable()
		{
		}
	}
}
