using MoreMountains.Tools;

namespace MoreMountains.Feedbacks
{
	public class MMFeedbacksAuthorizations : MMMonoBehaviour
	{
		[MMInspectorGroup("Animation", true, 16, false)]
		[MMInspectorButton("ToggleAnimation")]
		public bool ToggleAnimationButton;

		public bool AnimationParameter;

		public bool AnimatorSpeed;

		[MMInspectorGroup("Audio", true, 17, false)]
		[MMInspectorButton("ToggleAudio")]
		public bool ToggleAudioButton;

		public bool AudioFilterDistortion;

		public bool AudioFilterEcho;

		public bool AudioFilterHighPass;

		public bool AudioFilterLowPass;

		public bool AudioFilterReverb;

		public bool AudioMixerSnapshotTransition;

		public bool AudioSource;

		public bool AudioSourcePitch;

		public bool AudioSourceStereoPan;

		public bool AudioSourceVolume;

		public bool MMPlaylist;

		public bool MMSoundManagerAllSoundsControl;

		public bool MMSoundManagerSaveAndLoad;

		public bool MMSoundManagerSound;

		public bool MMSoundManagerSoundControl;

		public bool MMSoundManagerSoundFade;

		public bool MMSoundManagerTrackControl;

		public bool MMSoundManagerTrackFade;

		public bool Sound;

		[MMInspectorGroup("Camera", true, 18, false)]
		[MMInspectorButton("ToggleCamera")]
		public bool ToggleCameraButton;

		public bool CameraShake;

		public bool CameraZoom;

		public bool CinemachineImpulse;

		public bool CinemachineImpulseClear;

		public bool CinemachineImpulseSource;

		public bool CinemachineTransition;

		public bool ClippingPlanes;

		public bool Fade;

		public bool FieldOfView;

		public bool Flash;

		public bool OrthographicSize;

		[MMInspectorGroup("Debug", true, 19, false)]
		[MMInspectorButton("ToggleDebug")]
		public bool ToggleDebugButton;

		public bool Comment;

		public bool Log;

		[MMInspectorGroup("Events", true, 20, false)]
		[MMInspectorButton("ToggleEvents")]
		public bool ToggleEventsButton;

		public bool MMGameEvent;

		public bool UnityEvents;

		[MMInspectorGroup("GameObject", true, 47, false)]
		[MMInspectorButton("ToggleGameObject")]
		public bool ToggleGameObjectButton;

		public bool Broadcast;

		public bool Collider;

		public bool Collider2D;

		public bool DestroyTargetObject;

		public bool EnableBehaviour;

		public bool FloatController;

		public bool InstantiateObject;

		public bool MMRadioSignal;

		public bool Rigidbody;

		public bool Rigidbody2D;

		public bool SetActive;

		[MMInspectorGroup("Haptics", true, 22, false)]
		[MMInspectorButton("ToggleHaptics")]
		public bool ToggleHapticsButton;

		public bool HapticClip;

		public bool HapticContinuous;

		public bool HapticControl;

		public bool HapticEmphasis;

		public bool HapticPreset;

		[MMInspectorGroup("Light", true, 23, false)]
		[MMInspectorButton("ToggleLight")]
		public bool ToggleLightButton;

		public bool Light;

		[MMInspectorGroup("Loop", true, 24, false)]
		[MMInspectorButton("ToggleLoop")]
		public bool ToggleLoopButton;

		public bool Looper;

		public bool LooperStart;

		[MMInspectorGroup("Particles", true, 25, false)]
		[MMInspectorButton("ToggleParticles")]
		public bool ToggleParticlesButton;

		public bool ParticlesInstantiation;

		public bool ParticlesPlay;

		[MMInspectorGroup("Pause", true, 26, false)]
		[MMInspectorButton("TogglePause")]
		public bool TogglePauseButton;

		public bool HoldingPause;

		public bool Pause;

		[MMInspectorGroup("Post Process", true, 27, false)]
		[MMInspectorButton("TogglePostProcess")]
		public bool TogglePostProcessButton;

		public bool Bloom;

		public bool ChromaticAberration;

		public bool ColorGrading;

		public bool DepthOfField;

		public bool GlobalPPVolumeAutoBlend;

		public bool LensDistortion;

		public bool PPMovingFilter;

		public bool Vignette;

		[MMInspectorGroup("Flicker", true, 28, false)]
		[MMInspectorButton("ToggleFlicker")]
		public bool ToggleFlickerButton;

		public bool Flicker;

		public bool Fog;

		public bool Material;

		public bool MMBlink;

		public bool ShaderGlobal;

		public bool ShaderController;

		public bool Skybox;

		public bool SpriteRenderer;

		public bool TextureOffset;

		public bool TextureScale;

		[MMInspectorGroup("Scene", true, 29, false)]
		[MMInspectorButton("ToggleScene")]
		public bool ToggleSceneButton;

		public bool LoadScene;

		public bool UnloadScene;

		[MMInspectorGroup("Time", true, 31, false)]
		[MMInspectorButton("ToggleTime")]
		public bool ToggleTimeButton;

		public bool FreezeFrame;

		public bool TimescaleModifier;

		[MMInspectorGroup("Transform", true, 32, false)]
		[MMInspectorButton("ToggleTransform")]
		public bool ToggleTransformButton;

		public bool Destination;

		public bool Position;

		public bool PositionShake;

		public bool RotatePositionAround;

		public bool Rotation;

		public bool RotationShake;

		public bool Scale;

		public bool ScaleShake;

		public bool SquashAndStretch;

		public bool Wiggle;

		[MMInspectorGroup("UI", true, 33, false)]
		[MMInspectorButton("ToggleUI")]
		public bool ToggleUiButton;

		public bool CanvasGroup;

		public bool CanvasGroupBlocksRaycasts;

		public bool FloatingText;

		public bool Graphic;

		public bool GraphicCrossFade;

		public bool Image;

		public bool ImageAlpha;

		public bool ImageFill;

		public bool ImageRaycastTarget;

		public bool ImageTextureOffset;

		public bool ImageTextureScale;

		public bool RectTransformAnchor;

		public bool RectTransformOffset;

		public bool RectTransformPivot;

		public bool RectTransformSizeDelta;

		public bool Text;

		public bool TextColor;

		public bool TextFontSize;

		public bool VideoPlayer;

		[MMInspectorGroup("TextMesh Pro", true, 30, false)]
		[MMInspectorButton("ToggleTextMeshPro")]
		public bool ToggleTextMeshProButton;

		public bool TMPAlpha;

		public bool TMPCharacterSpacing;

		public bool TMPColor;

		public bool TMPCountTo;

		public bool TMPDilate;

		public bool TMPFontSize;

		public bool TMPLineSpacing;

		public bool TMPOutlineColor;

		public bool TMPOutlineWidth;

		public bool TMPParagraphSpacing;

		public bool TMPSoftness;

		public bool TMPText;

		public bool TMPTextReveal;

		public bool TMPWordSpacing;

		private void ToggleAnimation()
		{
		}

		private void ToggleAudio()
		{
		}

		private void ToggleCamera()
		{
		}

		private void ToggleDebug()
		{
		}

		private void ToggleEvents()
		{
		}

		private void ToggleGameObject()
		{
		}

		private void ToggleHaptics()
		{
		}

		private void ToggleLight()
		{
		}

		private void ToggleLoop()
		{
		}

		private void ToggleParticles()
		{
		}

		private void TogglePause()
		{
		}

		private void TogglePostProcess()
		{
		}

		private void ToggleFlicker()
		{
		}

		private void ToggleScene()
		{
		}

		private void ToggleTime()
		{
		}

		private void ToggleTransform()
		{
		}

		private void ToggleUI()
		{
		}

		private void ToggleTextMeshPro()
		{
		}

		private void Start()
		{
		}
	}
}
