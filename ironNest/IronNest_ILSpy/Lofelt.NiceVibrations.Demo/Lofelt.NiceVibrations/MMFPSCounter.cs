using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.UI;

namespace Lofelt.NiceVibrations;

public class MMFPSCounter : MonoBehaviour
{
	public float UpdateInterval = 0.3f;

	protected float _framesAccumulated;

	protected float _framesDrawnInTheInterval;

	protected float _timeLeft;

	protected Text _text;

	protected int _currentFPS;

	private static string[] _stringsFrom00To300;

	protected virtual void Start()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		Object obj = default(Object);
		if (obj != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			Text text = default(Text);
			_text = text;
			_timeLeft = UpdateInterval;
		}
		else
		{
			Debug.LogWarning("FPSCounter requires a GUIText component.");
		}
	}

	protected virtual void Update()
	{
		//IL_0029: Invalid comparison between I4 and F4
		//IL_0059: Invalid comparison between I4 and F4
		float framesDrawnInTheInterval = _framesDrawnInTheInterval + 1f;
		_framesDrawnInTheInterval = framesDrawnInTheInterval;
		float timeScale = Time.timeScale;
		float deltaTime = Time.deltaTime;
		float num = timeScale / deltaTime;
		float framesAccumulated = num + _framesAccumulated;
		_framesAccumulated = framesAccumulated;
		float deltaTime2 = Time.deltaTime;
		if (0f < (_timeLeft -= deltaTime2))
		{
			return;
		}
		float num2 = _framesAccumulated / _framesDrawnInTheInterval;
		int num3 = default(int);
		if (!(0f > num2) && !(num2 > 300f))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvttss2si eax,xmm0\"");
			_currentFPS = num3;
			if (num3 > 300)
			{
				goto IL_016c;
			}
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvttss2si eax,xmm1\"");
			_currentFPS = num3;
		}
		string[] stringsFrom00To = _stringsFrom00To300;
		int currentFPS = _currentFPS;
		_text.text = stringsFrom00To[currentFPS];
		goto IL_016c;
		IL_016c:
		_framesAccumulated = 0f;
		_timeLeft = UpdateInterval;
	}

	static MMFPSCounter()
	{
		string[] array = new string[301];
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 636 Invalid \"Jump target not found in method: 0x180A89822\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 651 Invalid \"Jump target not found in method: 0x180A89828\"");
		array[0] = "00";
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 669 Invalid \"Jump target not found in method: 0x180A89828\"");
		array[1] = "01";
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 687 Invalid \"Jump target not found in method: 0x180A89828\"");
		array[2] = "02";
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 705 Invalid \"Jump target not found in method: 0x180A89828\"");
		array[3] = "03";
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 723 Invalid \"Jump target not found in method: 0x180A89828\"");
		array[4] = "04";
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 741 Invalid \"Jump target not found in method: 0x180A89828\"");
		array[5] = "05";
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 759 Invalid \"Jump target not found in method: 0x180A89828\"");
		array[6] = "06";
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 777 Invalid \"Jump target not found in method: 0x180A89828\"");
		array[7] = "07";
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 795 Invalid \"Jump target not found in method: 0x180A89828\"");
		array[8] = "08";
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 813 Invalid \"Jump target not found in method: 0x180A89828\"");
		array[9] = "09";
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 831 Invalid \"Jump target not found in method: 0x180A89828\"");
		array[10] = "10";
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 849 Invalid \"Jump target not found in method: 0x180A89828\"");
		array[11] = "11";
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 867 Invalid \"Jump target not found in method: 0x180A89828\"");
		array[12] = "12";
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 885 Invalid \"Jump target not found in method: 0x180A89828\"");
		array[13] = "13";
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 903 Invalid \"Jump target not found in method: 0x180A89828\"");
		array[14] = "14";
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 921 Invalid \"Jump target not found in method: 0x180A89828\"");
		array[15] = "15";
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 939 Invalid \"Jump target not found in method: 0x180A89828\"");
		array[16] = "16";
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 957 Invalid \"Jump target not found in method: 0x180A89828\"");
	}
}
