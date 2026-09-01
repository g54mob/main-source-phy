using System;
using InControl;
using Landfall.TABS_Input;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LevelCreator
{
	public class PopUp : MonoBehaviour
	{
		public enum PopupArrowMode
		{
			Hidden = 0,
			UpLeft = 1,
			UpRight = 2,
			DownLeft = 3,
			DownRight = 4
		}

		public bool demandFocus;

		[HideIf("demandFocus")]
		public float lifeTime;

		private float elapsedLifeTime;

		private bool advanceLifeTime;

		public bool destroyOnAfterHiding;

		public string message;

		public LeanTweenType easeShow;

		[ShowIf("ValidateEaseInType")]
		public AnimationCurve easeInCurveInitial;

		private AnimationCurve easeInCurve;

		public LeanTweenType easeHide;

		[ShowIf("ValidateEaseOutType")]
		public AnimationCurve easeOutCurveInitial;

		private AnimationCurve easeOutCurve;

		private PopupArrowMode m_popupArrowMode;

		[SerializeField]
		private RectTransform m_upLeftArrow;

		[SerializeField]
		private RectTransform m_upRightArrow;

		[SerializeField]
		private RectTransform m_downLeftArrow;

		[SerializeField]
		private RectTransform m_downRightArrow;

		private DMEditor dmEditor;

		public UnityEvent onShowComplete;

		public UnityEvent onHideComplete;

		private bool isHiding = true;

		public bool hideHintDisplayed;

		private float hideHintTimer;

		public static InputState inputState = new InputState("PopUpState");

		public Canvas instantiatedCanvas;

		public static Canvas canvasPrefab;

		public static PopUp prefabReference;

		public static void InitPopupSystem(PopUp prefabReference, Canvas canvasPrefab)
		{
			PopUp.prefabReference = prefabReference;
			PopUp.canvasPrefab = canvasPrefab;
			TutorialPopUps.LoadShownPopups();
		}

		private void Start()
		{
			easeInCurve = easeInCurveInitial;
			easeOutCurve = easeOutCurveInitial;
			dmEditor = DMEditor.Instance;
			base.transform.localScale = Vector3.zero;
			if (demandFocus)
			{
				PlayerActions.Instance.OnLastInputTypeChanged += OnLastInputTypeChanged;
			}
		}

		private void OnLastInputTypeChanged(BindingSourceType obj)
		{
			if (!isHiding && PlayerActions.Instance.InputType == InputType.Controller)
			{
				EventSystem.current.SetSelectedGameObject(base.gameObject);
			}
		}

		public void Show(float delay, UnityEngine.Object sender)
		{
			LeanTween.delayedCall(delay, (System.Action)delegate
			{
				if (sender != null)
				{
					Show();
				}
			});
		}

		public void Show(float delay)
		{
			LeanTween.delayedCall(delay, (System.Action)delegate
			{
				Show();
			});
		}

		public void Show()
		{
			LTDescr lTDescr = LeanTween.scale(base.gameObject, Vector3.one, 0.5f).setOnComplete((System.Action)delegate
			{
				ShowComplete();
			});
			if (ValidateEaseInType())
			{
				lTDescr.setEase(easeInCurve);
			}
			else
			{
				lTDescr.setEase(easeShow);
			}
			isHiding = false;
		}

		private void ShowComplete()
		{
			if (demandFocus)
			{
				InputManager.PushState(inputState);
				dmEditor.SetInputMode(DMEditor.InputMode.UIOnly);
				dmEditor.playerController.SetMovementLock(locked: true);
				if (PlayerActions.Instance.InputType == InputType.Controller)
				{
					EventSystem.current.SetSelectedGameObject(base.gameObject);
				}
			}
			else
			{
				advanceLifeTime = true;
			}
			onShowComplete.Invoke();
		}

		public void Hide()
		{
			if (!isHiding)
			{
				isHiding = true;
				LTDescr lTDescr = LeanTween.scale(base.gameObject, Vector3.zero, 0.5f).setOnComplete((System.Action)delegate
				{
					HideComplete();
				});
				if (ValidateEaseOutType())
				{
					lTDescr.setEase(easeOutCurve);
				}
				else
				{
					lTDescr.setEase(easeHide);
				}
			}
		}

		private void HideComplete()
		{
			InputManager.RemoveState(inputState);
			if (demandFocus)
			{
				dmEditor.playerController.SetMovementLock(locked: false);
				dmEditor.SetInputMode(DMEditor.InputMode.Game);
			}
			advanceLifeTime = false;
			onHideComplete.Invoke();
			if (destroyOnAfterHiding)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		private void Update()
		{
			if (advanceLifeTime)
			{
				elapsedLifeTime += Time.deltaTime;
				if (elapsedLifeTime >= lifeTime)
				{
					Hide();
				}
			}
			if (!hideHintDisplayed && demandFocus && !isHiding)
			{
				hideHintTimer += Time.deltaTime;
				if (hideHintTimer > 5f)
				{
					hideHintDisplayed = true;
					Vector3 position = new Vector3(0f, -0.3f, 0f);
					string text = ((PlayerActions.Instance.InputType != InputType.Controller) ? "Click to continue" : "Press A to continue");
					PopUp popUp = CreatePopUp(position, text, demandFocus: false, 20f, 15f, isContinuePopUp: true);
					popUp.gameObject.AddComponent<LayoutElement>().ignoreLayout = true;
					popUp.transform.SetParent(base.gameObject.transform, worldPositionStays: false);
					popUp.Show();
				}
			}
		}

		private bool ValidateEaseInType()
		{
			return easeShow == LeanTweenType.animationCurve;
		}

		private bool ValidateEaseOutType()
		{
			return easeHide == LeanTweenType.animationCurve;
		}

		private void OnDestroy()
		{
			if (demandFocus)
			{
				PlayerActions.Instance.OnLastInputTypeChanged -= OnLastInputTypeChanged;
			}
			if (instantiatedCanvas != null)
			{
				UnityEngine.Object.Destroy(instantiatedCanvas.gameObject);
			}
		}

		public static PopUp CreatePopUp(Vector3 position, string message, bool demandFocus, float lifeTime = 2f, float fontSize = 0f, bool isContinuePopUp = false, PopupArrowMode arrowMode = PopupArrowMode.Hidden)
		{
			if (prefabReference == null || canvasPrefab == null)
			{
				return null;
			}
			PopUp popup = UnityEngine.Object.Instantiate(prefabReference);
			popup.instantiatedCanvas = UnityEngine.Object.Instantiate(canvasPrefab);
			Rect rect = popup.instantiatedCanvas.GetComponent<RectTransform>().rect;
			popup.transform.position = new Vector3(position.x * rect.width / 2f, position.y * rect.height / 2f, position.z);
			popup.demandFocus = demandFocus;
			popup.lifeTime = lifeTime;
			popup.destroyOnAfterHiding = true;
			popup.hideHintDisplayed = isContinuePopUp;
			if (!isContinuePopUp)
			{
				popup.transform.SetParent(popup.instantiatedCanvas.transform, worldPositionStays: false);
			}
			popup.GetComponent<Button>().interactable = demandFocus;
			popup.message = message;
			LocalizeText componentInChildren = popup.GetComponentInChildren<LocalizeText>();
			componentInChildren.LocaleID = message;
			if (fontSize != 0f)
			{
				componentInChildren.Text.fontSize = fontSize;
			}
			Utility.DelayAction(popup, delegate
			{
				popup.GetComponent<RectTransform>();
				popup.m_upLeftArrow.gameObject.SetActive(value: false);
				popup.m_upRightArrow.gameObject.SetActive(value: false);
				popup.m_downLeftArrow.gameObject.SetActive(value: false);
				popup.m_downRightArrow.gameObject.SetActive(value: false);
				switch (arrowMode)
				{
				case PopupArrowMode.UpLeft:
					popup.m_upLeftArrow.gameObject.SetActive(value: true);
					break;
				case PopupArrowMode.UpRight:
					popup.m_upRightArrow.gameObject.SetActive(value: true);
					break;
				case PopupArrowMode.DownLeft:
					popup.m_downLeftArrow.gameObject.SetActive(value: true);
					break;
				case PopupArrowMode.DownRight:
					popup.m_downRightArrow.gameObject.SetActive(value: true);
					break;
				case PopupArrowMode.Hidden:
					break;
				}
			});
			return popup;
		}
	}
}
