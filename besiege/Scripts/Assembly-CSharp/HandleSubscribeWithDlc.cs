using System;
using BesiegeDlc;
using ModIO;
using ModIO.UI;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Canvas/ModBrowser/HandleSubscribeWithDlc")]
[RequireComponent(typeof(Toggle))]
public class HandleSubscribeWithDlc : MonoBehaviour, IModViewElement
{
	private Toggle toggle;

	private ModView modView;

	private bool currentToggleState;

	virtual GameObject IModViewElement.gameObject
	{
		get
		{
			return base.gameObject;
		}
	}

	private void Start()
	{
		toggle = GetComponent<Toggle>();
		toggle.onValueChanged.AddListener(OnValueChanged);
		currentToggleState = toggle.isOn;
	}

	public void SetModView(ModView view)
	{
		modView = view;
	}

	private void OnValueChanged(bool isOn)
	{
		if (currentToggleState == isOn || modView == null || modView.profile == null)
		{
			return;
		}
		if (isOn)
		{
			if (!DoesUserHaveDlc())
			{
				ModProfile profile = modView.profile;
				Action warningButtonCallback = delegate
				{
					modView.AttemptSubscribe();
					ViewManager.instance.CloseWindowedView(ViewManager.instance.messageDialog);
				};
				Action standardButtonCallback = delegate
				{
					ViewManager.instance.CloseWindowedView(ViewManager.instance.messageDialog);
				};
				Action onClose = delegate
				{
					bool isOn2 = LocalUser.SubscribedModIds.Contains(profile.id);
					currentToggleState = isOn2;
					toggle.isOn = isOn2;
				};
				MessageDialog.Data messageData = new MessageDialog.Data
				{
					header = "Subscribe Confirmation (DLC required)",
					message = "You are about to subscribe to " + profile.name + " but you are missing a required dlc.\nThe item may not work correctly, do you want to continue?",
					warningButtonText = "Subscribe",
					warningButtonCallback = warningButtonCallback,
					standardButtonText = "Cancel",
					standardButtonCallback = standardButtonCallback,
					highlightButtonText = string.Empty,
					onClose = onClose
				};
				toggle.isOn = false;
				ViewManager.instance.ShowMessageDialog(messageData);
				return;
			}
			modView.AttemptSubscribe();
		}
		else
		{
			modView.AttemptUnsubscribe();
		}
		currentToggleState = isOn;
	}

	private bool DoesUserHaveDlc()
	{
		uint dlcDependencyMask;
		WorkshopManager.ParseItemMetadata(modView.profile.metadataBlob, out dlcDependencyMask);
		if (dlcDependencyMask == 0)
		{
			return true;
		}
		return DlcManager.Instance.HasPurchasedDlcMask(dlcDependencyMask);
	}
}
