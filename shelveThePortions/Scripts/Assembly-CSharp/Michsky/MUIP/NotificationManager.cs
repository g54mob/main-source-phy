using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Michsky.MUIP
{
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Animator))]
	public class NotificationManager : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
	{
		public enum StartBehaviour
		{
			None = 0,
			Disable = 1,
			Open = 2
		}

		public enum CloseBehaviour
		{
			None = 0,
			Disable = 1,
			Destroy = 2
		}

		public enum SlideDirection
		{
			Default = 0,
			Left = 1,
			Right = 2
		}

		public Sprite icon;

		public string title = "Notification Title";

		[TextArea(1, 4)]
		public string description = "Notification description";

		public Animator notificationAnimator;

		public Image iconObj;

		public TextMeshProUGUI titleObj;

		public TextMeshProUGUI descriptionObj;

		public bool enableTimer = true;

		public float timer = 3f;

		[SerializeField]
		private bool useCustomContent;

		public bool closeOnClick;

		public bool useStacking;

		[HideInInspector]
		public bool isOn;

		public StartBehaviour startBehaviour = StartBehaviour.Disable;

		public CloseBehaviour closeBehaviour = CloseBehaviour.Disable;

		public SlideDirection slideDirection;

		public UnityEvent onOpen = new UnityEvent();

		public UnityEvent onClose = new UnityEvent();

		private void Awake()
		{
			isOn = false;
			if (!useCustomContent)
			{
				UpdateUI();
			}
			if (notificationAnimator == null)
			{
				notificationAnimator = base.gameObject.GetComponent<Animator>();
			}
			if (useStacking)
			{
				try
				{
					base.transform.GetComponentInParent<NotificationStacking>().AddToStack(this);
				}
				catch
				{
					Debug.LogError("<b>[Notification]</b> 'Stacking' is enabled but 'Notification Stacking' cannot be found in parent.", this);
				}
			}
		}

		private void Start()
		{
			if (startBehaviour == StartBehaviour.Disable)
			{
				base.gameObject.SetActive(value: false);
			}
			else if (startBehaviour == StartBehaviour.Open)
			{
				Open();
			}
		}

		public void Open()
		{
			if (!isOn)
			{
				base.gameObject.SetActive(value: true);
				isOn = true;
				StopCoroutine("StartTimer");
				StopCoroutine("DisableNotification");
				notificationAnimator.Play("In");
				onOpen.Invoke();
				if (enableTimer)
				{
					StartCoroutine("StartTimer");
				}
			}
		}

		public void Close()
		{
			if (isOn)
			{
				isOn = false;
				notificationAnimator.Play("Out");
				onClose.Invoke();
				StopCoroutine("StartTimer");
				StopCoroutine("DisableNotification");
				StartCoroutine("DisableNotification");
			}
		}

		public void OpenNotification()
		{
			Open();
		}

		public void CloseNotification()
		{
			Close();
		}

		public void UpdateUI()
		{
			if (iconObj != null)
			{
				iconObj.sprite = icon;
			}
			if (titleObj != null)
			{
				titleObj.text = title;
			}
			if (descriptionObj != null)
			{
				descriptionObj.text = description;
			}
			if (slideDirection == SlideDirection.Left)
			{
				base.transform.localScale = new Vector3(-1f, base.transform.localScale.y, base.transform.localScale.z);
				base.transform.GetChild(0).transform.localScale = new Vector3(-1f, base.transform.GetChild(0).transform.localScale.y, base.transform.GetChild(0).transform.localScale.z);
			}
			else if (slideDirection == SlideDirection.Right)
			{
				base.transform.localScale = new Vector3(1f, base.transform.localScale.y, base.transform.localScale.z);
				base.transform.GetChild(0).transform.localScale = new Vector3(1f, base.transform.GetChild(0).transform.localScale.y, base.transform.GetChild(0).transform.localScale.z);
			}
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (closeOnClick)
			{
				Close();
			}
		}

		private IEnumerator StartTimer()
		{
			yield return new WaitForSecondsRealtime(timer);
			Close();
		}

		private IEnumerator DisableNotification()
		{
			yield return new WaitForSecondsRealtime(1f);
			if (closeBehaviour == CloseBehaviour.Disable)
			{
				base.gameObject.SetActive(value: false);
				isOn = false;
			}
			else if (closeBehaviour == CloseBehaviour.Destroy)
			{
				Object.Destroy(base.gameObject);
			}
		}
	}
}
