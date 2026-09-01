using System;
using Cpp2ILInjected;
using TMPro;
using UnityEngine;

public class AdjustTimeScale : MonoBehaviour
{
	private TextMeshProUGUI textMesh;

	private void Start()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		TextMeshProUGUI textMeshProUGUI = default(TextMeshProUGUI);
		textMesh = textMeshProUGUI;
	}

	private void Update()
	{
		//IL_0232: Invalid comparison between F4 and I4
		//IL_001c: Invalid comparison between I4 and F4
		float axis = Input.GetAxis("Mouse ScrollWheel");
		TextMeshProUGUI textMeshProUGUI;
		if (!(axis > 0f))
		{
			float axis2 = Input.GetAxis("Mouse ScrollWheel");
			if (!(0f > axis2))
			{
				return;
			}
			float timeScale = Time.timeScale;
			if (!(timeScale < 0.2f))
			{
				float timeScale2 = Time.timeScale;
				float timeScale3 = timeScale2 - 0.1f;
				Time.timeScale = timeScale3;
			}
			float timeScale4 = Time.timeScale;
			float fixedDeltaTime = timeScale4 * 0.02f;
			Time.fixedDeltaTime = fixedDeltaTime;
			if (!(textMesh != null))
			{
				return;
			}
			textMeshProUGUI = textMesh;
			float timeScale5 = Time.timeScale;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm6\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371440");
		}
		else
		{
			float timeScale6 = Time.timeScale;
			if (1f > timeScale6)
			{
				float timeScale7 = Time.timeScale;
				float timeScale8 = timeScale7 + 0.1f;
				Time.timeScale = timeScale8;
			}
			float timeScale9 = Time.timeScale;
			float fixedDeltaTime2 = timeScale9 * 0.02f;
			Time.fixedDeltaTime = fixedDeltaTime2;
			if (!(textMesh != null))
			{
				return;
			}
			textMeshProUGUI = textMesh;
			float timeScale10 = Time.timeScale;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm6,xmm6\"");
			double num = Math.Round(timeScale10, 2, MidpointRounding.ToEven);
		}
		double num2 = default(double);
		string text = num2.ToString();
		string text2 = "Time Scale : " + text;
		textMeshProUGUI.text = text2;
	}

	private void OnApplicationQuit()
	{
		Time.timeScale = 1f;
		Time.fixedDeltaTime = 0.02f;
	}
}
