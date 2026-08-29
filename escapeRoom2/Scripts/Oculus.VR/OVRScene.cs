using System;
using System.Collections.Generic;
using Meta.XR.Util;

[Feature(Feature.Scene)]
public static class OVRScene
{
	[Obsolete("Requesting space setup with labels is deprecated (v71) with no replacement.")]
	public static OVRTask<bool> RequestSpaceSetup(string labels)
	{
		if (!OVRPlugin.RequestSceneCapture(labels, out var requestId))
		{
			return OVRTask.FromResult(result: false);
		}
		return OVRTask.FromRequest<bool>(requestId);
	}

	public static OVRTask<bool> RequestSpaceSetup()
	{
		if (!OVRPlugin.RequestSceneCapture(string.Empty, out var requestId))
		{
			return OVRTask.FromResult(result: false);
		}
		return OVRTask.FromRequest<bool>(requestId);
	}

	[Obsolete("Requesting space setup with labels is deprecated (v71) with no replacement.")]
	public static OVRTask<bool> RequestSpaceSetup(IReadOnlyList<OVRSemanticLabels.Classification> classifications)
	{
		return RequestSpaceSetup(OVRSemanticLabels.ToApiString(classifications));
	}

	private static void ValidateRequestString(IEnumerable<string> labels, string paramName)
	{
		foreach (string item in labels.ToNonAlloc())
		{
			if (item == null || !OVRSceneManager.Classification.Set.Contains(item))
			{
				throw new ArgumentException("'" + item + "' is not a valid label. See OVRSemanticLabels.Classification.", paramName);
			}
		}
	}
}
