using System;
using UnityEngine;
using UnityEngine.UI;

public class TwitchDebugViewerInfo : MonoBehaviour
{
	public Text Text;

	[HideInInspector]
	public TwitchViewerInfo ViewerInfo;

	private void Start()
	{
	}

	private void Update()
	{
		if (!ViewerInfo)
		{
			ViewerInfo = UnityEngine.Object.FindObjectOfType<TwitchViewerInfo>();
		}
		if (ViewerInfo.IsCurrentlyGettingPop)
		{
			Text.text = "Updating";
			return;
		}
		Text.text = "Viewer Info" + Environment.NewLine;
		foreach (string item in ViewerInfo.Viewers.chatters.broadcaster)
		{
			Text text = Text;
			text.text = text.text + "B " + item + Environment.NewLine;
		}
		foreach (string viewer in ViewerInfo.Viewers.chatters.viewers)
		{
			Text text2 = Text;
			text2.text = text2.text + "V " + viewer + Environment.NewLine;
		}
		foreach (string moderator in ViewerInfo.Viewers.chatters.moderators)
		{
			Text text3 = Text;
			text3.text = text3.text + "M " + moderator + Environment.NewLine;
		}
		foreach (string vip in ViewerInfo.Viewers.chatters.vips)
		{
			Text text4 = Text;
			text4.text = text4.text + "V " + vip + Environment.NewLine;
		}
		foreach (string global_mod in ViewerInfo.Viewers.chatters.global_mods)
		{
			Text text5 = Text;
			text5.text = text5.text + "GM " + global_mod + Environment.NewLine;
		}
	}
}
