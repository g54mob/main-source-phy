using UnityEngine;

namespace MoreMountains.Tools
{
	[RequireComponent(typeof(CanvasGroup))]
	[AddComponentMenu("More Mountains/Tools/Controls/MMTouchControls")]
	public class MMTouchControls : MonoBehaviour
	{
		public enum InputForcedMode
		{
			None = 0,
			Mobile = 1,
			Desktop = 2
		}

		[MMInformation("If you check Auto Mobile Detection, the engine will automatically switch to mobile controls when your build target is Android or iOS. You can also force mobile or desktop (keyboard, gamepad) controls using the dropdown below.\nNote that if you don't need mobile controls and/or GUI this component can also work on its own, just put it on an empty GameObject instead.", MMInformationAttribute.InformationType.Info, false)]
		[Tooltip("If you check Auto Mobile Detection, the engine will automatically switch to mobile controls when your build target is Android or iOS.You can also force mobile or desktop (keyboard, gamepad) controls using the dropdown below.Note that if you don't need mobile controls and/or GUI this component can also work on its own, just put it on an empty GameObject instead.")]
		public bool AutoMobileDetection;

		[Tooltip("Force desktop mode (gamepad, keyboard...) or mobile (touch controls)")]
		public InputForcedMode ForcedMode;

		protected CanvasGroup _canvasGroup;

		protected float _initialMobileControlsAlpha;

		public virtual bool IsMobile { get; protected set; }

		protected virtual void Start()
		{
		}

		public virtual void SetMobileControlsActive(bool state)
		{
		}
	}
}
