using System.Collections.Generic;
using ModIO.API;
using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	public class ReportDialog : MonoBehaviour, IBrowserView
	{
		public const ReportedResourceType RESOURCE_TYPE = ReportedResourceType.Mod;

		public ReportTypeDropdown dropdown;

		public InputField detailsField;

		public InputField contactNameField;

		public InputField contactEmailField;

		protected int m_modId;

		protected List<Selectable> m_onFocusPriority;

		CanvasGroup IBrowserView.canvasGroup
		{
			get
			{
				return base.gameObject.GetComponent<CanvasGroup>();
			}
		}

		bool IBrowserView.resetSelectionOnHide
		{
			get
			{
				return true;
			}
		}

		bool IBrowserView.isRootView
		{
			get
			{
				return false;
			}
		}

		List<Selectable> IBrowserView.onFocusPriority
		{
			get
			{
				return m_onFocusPriority;
			}
		}

		virtual GameObject IBrowserView.gameObject
		{
			get
			{
				return base.gameObject;
			}
		}

		protected virtual void Awake()
		{
			m_onFocusPriority = new List<Selectable>
			{
				dropdown.GetComponent<Dropdown>(),
				detailsField
			};
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
			ModManager.GetModProfile(modId, SetModProfile, delegate
			{
				m_modId = 0;
			});
		}

		public virtual void SetModProfile(ModProfile profile)
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

		public virtual void SubmitReport()
		{
			SubmitReportParameters submitReportParameters = new SubmitReportParameters();
			MessageDialog.Data messageData = default(MessageDialog.Data);
			submitReportParameters.resource = EditableReport.ResourceTypeToAPIString(ReportedResourceType.Mod);
			if (m_modId <= 0)
			{
				messageData.header = "Error Submitting Report";
				messageData.message = "The submission process encountered an error.\n[Error: Invalid mod id]";
				messageData.standardButtonCallback = delegate
				{
					ViewManager.instance.CloseWindowedView(ViewManager.instance.reportDialog);
					ViewManager.instance.CloseWindowedView(ViewManager.instance.messageDialog);
				};
				messageData.standardButtonText = "Back";
				messageData.onClose = delegate
				{
					ViewManager.instance.CloseWindowedView(ViewManager.instance.reportDialog);
					ViewManager.instance.CloseWindowedView(ViewManager.instance.messageDialog);
				};
				return;
			}
			submitReportParameters.id = m_modId;
			ReportType enumValue;
			if (!dropdown.TryGetSelectedValue(out enumValue))
			{
				messageData.header = "Error Submitting Report";
				messageData.message = "A report type needs to be selected from the dropdown options.";
				messageData.standardButtonCallback = delegate
				{
					ViewManager.instance.CloseWindowedView(ViewManager.instance.messageDialog);
				};
				messageData.standardButtonText = "Back";
				ViewManager.instance.ShowMessageDialog(messageData);
				return;
			}
			submitReportParameters.type = enumValue;
			string text = contactEmailField.text;
			if (!string.IsNullOrEmpty(text) && !Utility.IsEmail(text))
			{
				messageData.header = "Error Submitting Report";
				messageData.message = "Please enter a valid email address or leave the field empty.";
				messageData.standardButtonCallback = delegate
				{
					ViewManager.instance.CloseWindowedView(ViewManager.instance.messageDialog);
				};
				messageData.standardButtonText = "Back";
				ViewManager.instance.ShowMessageDialog(messageData);
				return;
			}
			submitReportParameters.contact = text;
			submitReportParameters.summary = detailsField.text;
			submitReportParameters.name = contactNameField.text;
			messageData.header = "Report Submission Status";
			messageData.message = "Please wait while we submit your report.";
			messageData.onClose = delegate
			{
				ViewManager.instance.CloseWindowedView(ViewManager.instance.reportDialog);
				ViewManager.instance.CloseWindowedView(ViewManager.instance.messageDialog);
			};
			messageData.standardButtonText = "...";
			ViewManager.instance.ShowMessageDialog(messageData);
			APIClient.SubmitReport(submitReportParameters, OnReportSuccessful, OnReportFailed);
		}

		private void OnReportSuccessful(APIMessage response)
		{
			MessageDialog.Data messageData = new MessageDialog.Data
			{
				header = "Report Submission Status",
				message = "Report submission successful.\n" + response.message,
				standardButtonCallback = delegate
				{
					ViewManager.instance.CloseWindowedView(ViewManager.instance.reportDialog);
					ViewManager.instance.CloseWindowedView(ViewManager.instance.messageDialog);
				},
				standardButtonText = "Done",
				onClose = delegate
				{
					ViewManager.instance.CloseWindowedView(ViewManager.instance.reportDialog);
					ViewManager.instance.CloseWindowedView(ViewManager.instance.messageDialog);
				}
			};
			ViewManager.instance.ShowMessageDialog(messageData);
		}

		private void OnReportFailed(WebRequestError error)
		{
			MessageDialog.Data messageData = new MessageDialog.Data
			{
				header = "Report Submission Status",
				message = "Report submission failed.\n" + error.displayMessage + "\n[Error Code: " + error.webRequest.responseCode + "]",
				standardButtonCallback = delegate
				{
					ViewManager.instance.CloseWindowedView(ViewManager.instance.reportDialog);
					ViewManager.instance.CloseWindowedView(ViewManager.instance.messageDialog);
				},
				standardButtonText = "Done",
				onClose = delegate
				{
					ViewManager.instance.CloseWindowedView(ViewManager.instance.reportDialog);
					ViewManager.instance.CloseWindowedView(ViewManager.instance.messageDialog);
				}
			};
			ViewManager.instance.ShowMessageDialog(messageData);
		}
	}
}
