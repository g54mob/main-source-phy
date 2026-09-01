using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class FullScreenConnection : Connection<bool>
{
	protected bool? lastKnownFullScreen;

	protected int lastSetFrame;

	public override bool Get()
	{
		//IL_0090: Expected O, but got I4
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_0010: Expected O, but got I4
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Expected O, but got Unknown
		int frameCount = Time.frameCount;
		object obj = frameCount - lastSetFrame;
		if ((nint)obj > 3)
		{
			lastKnownFullScreen = (bool?)(object)0;
		}
		object obj2 = this + 40;
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj3 = default(object);
		if (obj3 == null)
		{
			return Screen.fullScreen;
		}
		object obj4 = this + 40;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CBE10");
		bool result = default(bool);
		return result;
	}

	public unsafe override void Set(bool fullScreen)
	{
		//IL_0049: Expected O, but got I4
		//IL_0015: Expected O, but got I4
		ScreenOrchestrator instance = ScreenOrchestrator.Instance;
		bool flag2 = default(bool);
		bool? flag = (byte)(&flag2) != 0;
		instance.requestedFullScreen = (bool?)(object)0;
		int frameCount = Time.frameCount;
		lastSetFrame = frameCount;
		bool? flag3 = (byte)(&flag2) != 0;
		lastKnownFullScreen = (bool?)(object)0;
		base.NotifyListenersIfChanged(fullScreen);
	}
}
