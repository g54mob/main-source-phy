using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TwitchOptions : MonoBehaviour
{
	[SerializeField]
	private bool m_AllowNoneAuthConnection;

	[SerializeField]
	private GameObject m_templateTwitchAction;

	[SerializeField]
	private GameObject m_templateTwitchConnectStatusAction;

	[SerializeField]
	private GameObject m_templateTwitchAuthAction;

	[SerializeField]
	private GameObject m_templateTwitchAuthInfoAction;

	[SerializeField]
	private GameObject m_templateEmptyRowAction;

	[SerializeField]
	private Transform m_grid;

	private GameObject authRef;

	private GameObject connectRef;

	[SerializeField]
	private List<string> m_AuthInfoTexts = new List<string>();

	public void ResetToDefault()
	{
		authRef.GetComponent<UITwitchAuthConnect>().InputField.text = "";
		connectRef.GetComponent<UITwitchConnect>().InputField.text = "";
	}

	private GameObject SetupPref(GameObject go)
	{
		GameObject obj = Object.Instantiate(go, m_grid, worldPositionStays: false);
		obj.SetActive(value: true);
		obj.transform.SetAsFirstSibling();
		return obj;
	}

	public void SetupTwitch()
	{
		SetupPref(m_templateEmptyRowAction);
		for (int num = m_AuthInfoTexts.Count - 1; num >= 0; num--)
		{
			SetupPref(m_templateTwitchAuthInfoAction).GetComponentInChildren<TextMeshProUGUI>().text = m_AuthInfoTexts[num];
		}
		SetupPref(m_templateEmptyRowAction);
		SetupPref(m_templateTwitchConnectStatusAction);
		authRef = SetupPref(m_templateTwitchAuthAction);
		connectRef = SetupPref(m_templateTwitchAction);
		if (!m_AllowNoneAuthConnection)
		{
			connectRef.GetComponent<UITwitchConnect>().Button.gameObject.SetActive(value: false);
		}
	}
}
