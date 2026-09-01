using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class LightDetectorComponent : MonoBehaviour
{
	public void Awake()
	{
		if (LightDetector._instance == null)
		{
			LightDetector instance = new LightDetector();
			LightDetector._instance = instance;
		}
		LightDetector instance2 = LightDetector._instance;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		Object obj = default(Object);
		if (obj != null && !instance2._lights.Contains((Light)obj))
		{
			instance2._lights.Add((Light)obj);
			LightDetector.OnNewLightFoundDelegate onNewLightFound = instance2.OnNewLightFound;
			if (instance2.OnNewLightFound != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v297.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
			}
		}
	}
}
