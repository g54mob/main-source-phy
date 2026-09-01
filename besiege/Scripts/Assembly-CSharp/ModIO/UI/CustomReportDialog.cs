using System.Collections.Generic;
using Localisation;
using ModIO.API;
using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	public class CustomReportDialog : ReportDialog, IBrowserView
	{
		public enum ToggleType
		{
			Rude = 0,
			Sensitive = 1,
			Crash = 2,
			Bug = 3
		}

		public Text header;

		public Toggle[] toggles = new Toggle[0];

		public Button cancel;

		public Button report;

		public Text reportText;

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

		private void SetHeader(string name)
		{
			string translation = LocalisationManager.GetTranslation(4191);
			string[] array = translation.Split('"');
			translation = "<color=#FF2F70>";
			translation += array[0].Replace("\\", string.Empty);
			translation += "</color> '";
			for (int i = 1; i < array.Length; i++)
			{
				translation += array[i].Replace("\\", string.Empty);
			}
			translation += "'";
			header.text = string.Format(translation, name);
		}

		private void GetReport(out ReportType t, out string s)
		{
			int num = -1;
			for (int i = 0; i < toggles.Length; i++)
			{
				if (toggles[i].isOn)
				{
					num = i;
					break;
				}
			}
			s = "...";
			t = ReportType.Other;
			switch (num)
			{
			case 0:
				t = ReportType.RudeContent;
				s = LocalisationManager.GetTranslation(LocalisationManager.DefaultTranslationFile(), 4194);
				break;
			case 1:
				t = ReportType.IllegalContent;
				s = LocalisationManager.GetTranslation(LocalisationManager.DefaultTranslationFile(), 4195);
				break;
			case 2:
				t = ReportType.NotWorking;
				s = LocalisationManager.GetTranslation(LocalisationManager.DefaultTranslationFile(), 4196);
				break;
			case 3:
				t = ReportType.Other;
				s = LocalisationManager.GetTranslation(LocalisationManager.DefaultTranslationFile(), 4197);
				break;
			}
		}

		protected override void Awake()
		{
			for (int i = 0; i < toggles.Length; i++)
			{
				toggles[i].onValueChanged.AddListener(SetOn);
			}
			cancel.onClick.AddListener(base.Close);
			report.onClick.AddListener(SubmitReport);
			m_onFocusPriority = new List<Selectable>
			{
				cancel,
				toggles[0]
			};
		}

		private void OnEnable()
		{
			reportText.CrossFadeAlpha(0.5f, 0f, true);
			report.interactable = false;
			for (int i = 0; i < toggles.Length; i++)
			{
				toggles[i].isOn = false;
			}
		}

		private void SetOn(bool e)
		{
			if (e)
			{
				report.interactable = true;
				reportText.CrossFadeAlpha(1f, 0f, true);
				return;
			}
			for (int i = 0; i < toggles.Length; i++)
			{
				if (toggles[i].isOn)
				{
					return;
				}
			}
			reportText.CrossFadeAlpha(0.5f, 0f, true);
			report.interactable = false;
		}

		public override void SetModProfile(ModProfile profile)
		{
			if (profile == null)
			{
				header.text = LocalisationManager.GetTranslation(4091);
				m_modId = 0;
			}
			else
			{
				SetHeader(profile.name);
				m_modId = profile.id;
			}
			ModView component = GetComponent<ModView>();
			if (component != null)
			{
				component.profile = profile;
			}
		}

		public override void SubmitReport()
		{
			SubmitReportParameters submitReportParameters = new SubmitReportParameters();
			MessageDialog.Data data = default(MessageDialog.Data);
			submitReportParameters.resource = EditableReport.ResourceTypeToAPIString(ReportedResourceType.Mod);
			if (m_modId <= 0)
			{
				data.header = LocalisationManager.GetTranslation(4172);
				data.message = string.Format(LocalisationManager.GetTranslation(4170), "Invalid mod id");
				data.standardButtonCallback = delegate
				{
					ViewManager.instance.CloseWindowedView(ViewManager.instance.reportDialog);
				};
				data.standardButtonText = LocalisationManager.GetTranslation(1892);
				data.onClose = delegate
				{
					ViewManager.instance.CloseWindowedView(ViewManager.instance.reportDialog);
				};
			}
			else
			{
				submitReportParameters.id = m_modId;
				ReportType t;
				string s;
				GetReport(out t, out s);
				submitReportParameters.type = t;
				string profileURL = LocalUser.Profile.profileURL;
				submitReportParameters.contact = profileURL;
				submitReportParameters.summary = s;
				submitReportParameters.name = LocalUser.Profile.username;
				APIClient.SubmitReport(submitReportParameters, OnReportSuccessful, OnReportFailed);
			}
		}

		private void OnReportSuccessful(APIMessage response)
		{
			string translation = LocalisationManager.GetTranslation(4171);
			MessageSystem.QueueMessage(MessageDisplayData.Type.Success, translation);
			Close();
		}

		private void OnReportFailed(WebRequestError error)
		{
			MessageDialog.Data messageData = new MessageDialog.Data
			{
				header = LocalisationManager.GetTranslation(4172),
				message = string.Format(LocalisationManager.GetTranslation(4170), error.webRequest.responseCode.ToString()),
				standardButtonCallback = delegate
				{
					ViewManager.instance.CloseWindowedView(ViewManager.instance.reportDialog);
					ViewManager.instance.CloseWindowedView(ViewManager.instance.messageDialog);
				},
				standardButtonText = LocalisationManager.GetTranslation(1892),
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
