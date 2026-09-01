using InControl;
using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LevelCreator
{
	public class UIScrollToSelection : MonoBehaviour
	{
		private const float ScrollDeltaThreshold = 400f;

		[SerializeField]
		private float scrollSpeed = 2f;

		[SerializeField]
		private RectTransform content;

		private RectTransform scrollWindow;

		private ScrollRect targetScrollRect;

		private EventSystem eventSystem;

		private GameObject selection;

		private PlayerActions playerActions;

		private void Start()
		{
			eventSystem = EventSystem.current;
			targetScrollRect = GetComponent<ScrollRect>();
			scrollWindow = targetScrollRect.GetComponent<RectTransform>();
			playerActions = PlayerActions.Instance;
			PlayerActions.Instance.OnLastInputTypeChanged += OnLastInputTypeChanged;
		}

		private void OnDestroy()
		{
			PlayerActions.Instance.OnLastInputTypeChanged -= OnLastInputTypeChanged;
		}

		private void OnLastInputTypeChanged(BindingSourceType obj)
		{
			EnableScrolling();
		}

		private void EnableScrolling()
		{
			if (playerActions.InputType != InputType.Controller)
			{
				targetScrollRect.enabled = true;
			}
			else
			{
				targetScrollRect.enabled = false;
			}
		}

		private void Update()
		{
			if (playerActions.InputType == InputType.Controller)
			{
				selection = eventSystem.currentSelectedGameObject;
				ScrollRectToLevelSelection();
			}
		}

		private void ScrollRectToLevelSelection()
		{
			if (!(selection == null))
			{
				Vector3 vector = selection.GetComponent<RectTransform>().position - scrollWindow.position;
				if (Mathf.Abs(vector.y) > 400f)
				{
					targetScrollRect.verticalNormalizedPosition += vector.normalized.y * Time.deltaTime * scrollSpeed;
				}
				targetScrollRect.verticalNormalizedPosition = Mathf.Clamp01(targetScrollRect.verticalNormalizedPosition);
				if (targetScrollRect.verticalScrollbar != null)
				{
					targetScrollRect.verticalScrollbar.value = targetScrollRect.verticalNormalizedPosition;
				}
			}
		}
	}
}
