using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public static class CompatibilityUtils
{
	public static T FindObjectOfType<T>(bool includeInactive = false)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ rdx (Il2CppMethodInfo)+38]");
		if ((nint)0 == 0)
		{
		}
		return Object.FindFirstObjectByType<T>(includeInactive ? FindObjectsInactive.Include : FindObjectsInactive.Exclude);
	}

	public static T[] FindObjectsOfType<T>(bool includeInactive = false)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ rdx (Il2CppMethodInfo)+38]");
		if ((nint)0 == 0)
		{
		}
		return Object.FindObjectsByType<T>(includeInactive ? FindObjectsInactive.Include : FindObjectsInactive.Exclude, FindObjectsSortMode.None);
	}
}
