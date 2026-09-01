using UnityEngine.EventSystems;

public class ExtendedStandaloneInputModule : StandaloneInputModule
{
	private static ExtendedStandaloneInputModule _instance;

	public static PointerEventData GetPointerEventData(int pointerId = -1)
	{
		_instance.GetPointerData(pointerId, out var data, create: true);
		return data;
	}

	protected override void Awake()
	{
		base.Awake();
		_instance = this;
	}
}
