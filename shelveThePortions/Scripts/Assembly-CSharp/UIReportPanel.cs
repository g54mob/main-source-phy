using UnityEngine;

public class UIReportPanel : MonoBehaviour
{
	public void OpenFeedbackURL()
	{
		Singleton<GoToURL>.Instance.GoToUrl(URLHolder.FeedbackURL);
	}

	public void OpenBugURL()
	{
		Singleton<GoToURL>.Instance.GoToUrl(URLHolder.ReportBugURL);
	}
}
