using System;
using UnityEngine;

[Serializable]
public struct KeyUsage
{
	[Tooltip("Focus key for this slot.\nSupported tokens:\n- Any non-empty string\n- Case-sensitive\n- Whitespace allowed\nExamples: \"Player\", \"BossRoom\"\n")]
	public string key;

	[Tooltip("Number of allowed uses for this key across ALL targets. If 0, unlimited. If >0, only this many focus requests for this key will succeed before further requests are ignored. Usage resets when you call CinemachineFocusService.ResetUsageCounts().")]
	[Min(0f)]
	public int usageLimit;
}
