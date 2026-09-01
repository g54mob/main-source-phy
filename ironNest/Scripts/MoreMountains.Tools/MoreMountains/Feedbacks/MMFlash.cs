using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace MoreMountains.Feedbacks
{
	[RequireComponent(typeof(Image))]
	[RequireComponent(typeof(CanvasGroup))]
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/Various/MMFlash")]
	public class MMFlash : MMMonoBehaviour
	{
		[MMInspectorGroup("Flash", true, 121, false)]
		[Tooltip("whether to listen on a channel defined by an int or by a MMChannel scriptable object. Ints are simple to setup but can get messy and make it harder to remember what int corresponds to what. MMChannel scriptable objects require you to create them in advance, but come with a readable name and are more scalable")]
		public MMChannelModes ChannelMode;

		[Tooltip("the channel to listen to - has to match the one on the feedback")]
		[MMFEnumCondition("ChannelMode", new int[] { 0 })]
		public int Channel;

		[Tooltip("the MMChannel definition asset to use to listen for events. The feedbacks targeting this shaker will have to reference that same MMChannel definition to receive events - to create a MMChannel, right click anywhere in your project (usually in a Data folder) and go MoreMountains > MMChannel, then name it with some unique name")]
		[MMFEnumCondition("ChannelMode", new int[] { 1 })]
		public MMChannel MMChannelDefinition;

		[Tooltip("the ID of this MMFlash object. When triggering a MMFlashEvent you can specify an ID, and only MMFlash objects with this ID will answer the call and flash, allowing you to have more than one flash object in a scene")]
		public int FlashID;

		[Tooltip("if this is true, the MMFlash will stop before playing on every new event received")]
		public bool Interruptable;

		[MMInspectorGroup("Interpolation", true, 122, false)]
		[Tooltip("the animation curve to use when flashing in")]
		public MMTweenType FlashInTween;

		[Tooltip("the animation curve to use when flashing out")]
		public MMTweenType FlashOutTween;

		[MMInspectorGroup("Debug", true, 123, false)]
		[Tooltip("the set of test settings to use when pressing the DebugTest button")]
		public MMFlashDebugSettings DebugSettings;

		[Tooltip("a test button that calls the DebugTest method")]
		[MMFInspectorButton("DebugTest")]
		public bool DebugTestButton;

		protected Image _image;

		protected CanvasGroup _canvasGroup;

		protected bool _flashing;

		protected float _targetAlpha;

		protected Color _initialColor;

		protected float _delta;

		protected float _flashStartedTimestamp;

		protected int _direction;

		protected float _duration;

		protected TimescaleModes _timescaleMode;

		protected MMTweenType _currentTween;

		public virtual float GetTime()
		{
			return 0f;
		}

		public virtual float GetDeltaTime()
		{
			return 0f;
		}

		protected virtual void Start()
		{
		}

		protected virtual void Update()
		{
		}

		public virtual void DebugTest()
		{
		}

		public virtual void OnMMFlashEvent(Color flashColor, float duration, float alpha, int flashID, MMChannelData channelData, TimescaleModes timescaleMode, bool stop = false)
		{
		}

		public virtual void Flash(Color flashColor, float duration, float alpha, TimescaleModes timescaleMode)
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
