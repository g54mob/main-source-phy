using InControl;
using Landfall.TABS_Input;
using UnityEngine;

namespace TFBGames
{
	public class XboxControllerService : ServicePrefab
	{
		[SerializeField]
		private GameObject canvasPopUpUIPrefab;

		private GameObject controllerPopUpUI;

		private CodeAnimation codeAnimation;

		public override void OnUpdate()
		{
			if (!(controllerPopUpUI == null) && PlayerActions.Instance.m_anybuttonpressed.WasPressed && !codeAnimation.isPlaying)
			{
				codeAnimation.PlayOut();
			}
		}

		private void OnControllerAttached(InputDevice inputDevice)
		{
			if (inputDevice.DeviceClass == InputDeviceClass.Controller && codeAnimation != null)
			{
				codeAnimation.PlayOut();
			}
		}

		private void OnControllerDetached(InputDevice inputDevice)
		{
			if (inputDevice.DeviceClass != InputDeviceClass.Controller)
			{
				return;
			}
			if (controllerPopUpUI == null)
			{
				if (!(canvasPopUpUIPrefab != null))
				{
					return;
				}
				controllerPopUpUI = Object.Instantiate(canvasPopUpUIPrefab);
				if (controllerPopUpUI != null)
				{
					codeAnimation = controllerPopUpUI.GetComponentInChildren<CodeAnimation>();
				}
				if (codeAnimation == null)
				{
					return;
				}
				CodeAnimationInstance[] animations = codeAnimation.animations;
				foreach (CodeAnimationInstance codeAnimationInstance in animations)
				{
					if (codeAnimationInstance.animationUse == CodeAnimationInstance.AnimationUse.Out)
					{
						codeAnimationInstance.endEvent.AddListener(OnPlayedOut);
						break;
					}
				}
			}
			else if (codeAnimation != null)
			{
				codeAnimation.PlayIn();
			}
		}

		private void OnPlayedOut()
		{
			Object.Destroy(controllerPopUpUI);
		}
	}
}
