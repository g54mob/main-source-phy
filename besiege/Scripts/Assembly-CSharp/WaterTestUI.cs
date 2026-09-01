using UnityEngine;

public class WaterTestUI : MonoBehaviour
{
	public UIButtonExtended waves;

	public UIButtonExtended log;

	public UIButtonExtended foremost;

	public UIButtonExtended surfaceDrag;

	public UIButtonExtended wheelLimit;

	public UIButtonExtended wingWeight;

	public void Awake()
	{
		waves.Click += Waves;
		log.Click += Log;
		foremost.Click += Foremost;
		surfaceDrag.Click += SurfaceDrag;
		wheelLimit.Click += WheelLimit;
		wingWeight.Click += WingWeight;
		bool toggle = true;
		bool toggle2 = true;
		bool toggle3 = true;
		bool toggle4 = true;
		bool toggle5 = true;
		bool toggle6 = true;
		waves.ToggleBG(toggle);
		log.ToggleBG(toggle2);
		foremost.ToggleBG(toggle3);
		surfaceDrag.ToggleBG(toggle4);
		wheelLimit.ToggleBG(toggle5);
		wingWeight.ToggleBG(toggle6);
	}

	private void Waves()
	{
		bool toggle = true;
		waves.ToggleBG(toggle);
	}

	private void Log()
	{
		bool toggle = true;
		log.ToggleBG(toggle);
	}

	private void Foremost()
	{
		bool toggle = true;
		foremost.ToggleBG(toggle);
	}

	private void SurfaceDrag()
	{
		bool toggle = true;
		surfaceDrag.ToggleBG(toggle);
	}

	private void WheelLimit()
	{
		bool toggle = true;
		wheelLimit.ToggleBG(toggle);
	}

	private void WingWeight()
	{
		bool toggle = true;
		wingWeight.ToggleBG(toggle);
	}
}
