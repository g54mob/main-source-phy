using UnityEngine;
using UnityEngine.UI;

namespace MoreMountains.Tools
{
	public class MMSoundManagerTrackVolumeSlider : MonoBehaviour, MMEventListener<MMSoundManagerEvent>, MMEventListenerBase, MMEventListener<MMSoundManagerTrackEvent>, MMEventListener<MMSoundManagerTrackFadeEvent>
	{
		public enum Modes
		{
			Read = 0,
			Write = 1
		}

		[Header("Track Volume Settings")]
		[Tooltip("The track to change volume on")]
		public MMSoundManager.MMSoundManagerTracks Track;

		[Tooltip("The volume to apply to the track when the slider is at its minimum")]
		public float MinVolume;

		[Tooltip("The volume to apply to the track when the slider is at its maximum")]
		public float MaxVolume;

		[Header("Read/Write Mode")]
		[Tooltip("in read mode, the value of the slider will be applied to the volume of the track. in read mode, the slider will move to reflect the volume of the track")]
		public Modes Mode;

		[Tooltip("if this is true, the slider will automatically switch to read mode for the required duration when a track fade event is caught")]
		public bool ChangeModeOnTrackFade;

		[Tooltip("if this is true, the slider will automatically switch to read mode for the required duration when a track mute event is caught")]
		public bool ChangeModeOnMute;

		[Tooltip("if this is true, the slider will automatically switch to read mode for the required duration when a track unmute event is caught")]
		public bool ChangeModeOnUnmute;

		[Tooltip("if this is true, the slider will automatically switch to read mode for the required duration when a track volume change event is caught")]
		public bool ChangeModeOnTrackVolumeChange;

		[Tooltip("when switching automatically (and temporarily) to Read Mode, the minimum duration the slider will remain in that mode")]
		public float ModeSwitchBufferTime;

		protected Slider _slider;

		protected Modes _resetToMode;

		protected bool _resetNeeded;

		protected float _resetTimestamp;

		protected virtual void Awake()
		{
		}

		protected virtual void Start()
		{
		}

		protected virtual void LateUpdate()
		{
		}

		public virtual void ChangeModeToRead(float duration)
		{
		}

		public virtual void UpdateVolume(float newValue)
		{
		}

		public void OnMMEvent(MMSoundManagerEvent soundManagerEvent)
		{
		}

		public virtual void UpdateSliderValueWithTrackVolume()
		{
		}

		public void OnMMEvent(MMSoundManagerTrackEvent trackEvent)
		{
		}

		public void OnMMEvent(MMSoundManagerTrackFadeEvent fadeEvent)
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
