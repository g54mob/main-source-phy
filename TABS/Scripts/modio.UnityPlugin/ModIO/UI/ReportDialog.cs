using System;
using System.Collections.Generic;
using ModIO.API;
using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	public class ReportDialog : MonoBehaviour, IBrowserView
	{
		public const ReportedResourceType RESOURCE_TYPE = ReportedResourceType.Mod;

		public int dropdownIndex;

		public GameObject secondSection;

		public ReportTypeDropdown dropdown;

		public GenericTextComponent detailsField;

		public GenericTextComponent contactNameField;

		public GenericTextComponent contactEmailField;

		public bool blockUser;

		private int m_modId;

		private List<Selectable> m_onFocusPriority;

		CanvasGroup IBrowserView.canvasGroup => base.gameObject.GetComponent<CanvasGroup>();

		bool IBrowserView.resetSelectionOnHide => true;

		bool IBrowserView.isRootView => false;

		List<Selectable> IBrowserView.onFocusPriority => m_onFocusPriority;

		GameObject IBrowserView.gameObject => base.gameObject;

		public void SetDropdownIndex(int index)
		{
			dropdownIndex = index;
		}

		public void UpdateSecondSection(int dropdownValue)
		{
			secondSection?.SetActive(dropdownValue != 0);
		}

		public void SetBlockUser(bool shouldBlockUser)
		{
			blockUser = shouldBlockUser;
		}

		public void SetModId(int modId)
		{
			if (m_modId == modId)
			{
				return;
			}
			m_modId = modId;
			if (modId == 0)
			{
				SetModProfile(null);
				return;
			}
			ModProfileRequestManager.instance.RequestModProfile(modId, SetModProfile, delegate
			{
				m_modId = 0;
			});
		}

		public void SetModProfile(ModProfile profile)
		{
			if (profile == null)
			{
				m_modId = 0;
			}
			else
			{
				m_modId = profile.id;
			}
			ModView component = GetComponent<ModView>();
			if (component != null)
			{
				component.profile = profile;
			}
		}

		public void Close()
		{
			ViewManager.instance.CloseWindowedView(this);
		}

		public void SubmitReport(Action onSuccess, Action<WebRequestError> onError)
		{
			SubmitReportParameters submitReportParameters = new SubmitReportParameters();
			submitReportParameters.resource = EditableReport.ResourceTypeToAPIString(ReportedResourceType.Mod);
			if (m_modId <= 0)
			{
				string displayMessage = "POPUP_REPORT_ERROR";
				onError(new WebRequestError
				{
					displayMessage = displayMessage
				});
				return;
			}
			submitReportParameters.id = m_modId;
			if (!dropdown.TryGetSelectedValue(out var enumValue, dropdownIndex))
			{
				string displayMessage2 = "POPUP_REPORT_NO_REPORT_TYPE";
				onError(new WebRequestError
				{
					displayMessage = displayMessage2
				});
				return;
			}
			submitReportParameters.type = enumValue;
			string text = contactEmailField.text;
			if (!string.IsNullOrEmpty(text) && !Utility.IsEmail(text))
			{
				string displayMessage3 = "POPUP_REPORT_INVALID_EMAIL";
				onError(new WebRequestError
				{
					displayMessage = displayMessage3
				});
				return;
			}
			if (string.IsNullOrEmpty(detailsField.text))
			{
				WebRequestError webRequestError = new WebRequestError();
				webRequestError.displayMessage = "POPUP_ERROR_INFRINGMENT_DETAILS_REQUIRED";
				onError(webRequestError);
				return;
			}
			submitReportParameters.contact = text;
			submitReportParameters.summary = detailsField.text;
			submitReportParameters.name = contactNameField.text;
			APIClient.SubmitReport(submitReportParameters, delegate(APIMessage message)
			{
				onSuccess();
				OnReportSuccessful(message);
			}, delegate(WebRequestError e)
			{
				e.errorMessage = e.displayMessage.Replace("The submitted data contained error(s).", string.Empty);
				e.displayMessage = "POPUP_MODIO_DATA_CONTAINS_ERRORS";
				onError(e);
				OnReportFailed(e);
			});
		}

		private void OnReportSuccessful(APIMessage response)
		{
			MessageSystem.QueueMessage(MessageDisplayData.Type.Success, "Report submission successful.\n" + response.message, 5f);
			APIClient.UnsubscribeFromMod(m_modId, null, WebRequestError.LogAsWarning);
		}

		private void OnReportFailed(WebRequestError error)
		{
			MessageSystem.QueueMessage(MessageDisplayData.Type.Error, "Report submission failed.\n" + error.displayMessage + "\n[Error Code: " + error.webRequest.responseCode + "]", 5f);
		}
	}
}
