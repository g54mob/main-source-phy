using UnityEngine;

public class SimpleMenuSlider : ClickBehaviour, ICanBeReset
{
	public FloatExtraOption floatExtraOption;

	public float min;

	public float max;

	private float diff;

	public Renderer sliderBar;

	public TextMesh textMesh;

	public Transform sliderButton;

	public Transform whitePartOfTheSliderBar;

	private float minScreenX;

	private float maxScreenX;

	private bool calculateWithMouse;

	private float worldXMinPosition;

	private float worldXDiff;

	private bool hasStarted;

	private Camera hudCam;

	private void Awake()
	{
		releaseOnlyOver = true;
	}

	private void Start()
	{
		hasStarted = true;
		hudCam = GameObject.Find("HUD Cam").GetComponent<Camera>();
		OnEnable();
	}

	private void OnEnable()
	{
		if (hasStarted)
		{
			minScreenX = hudCam.WorldToScreenPoint(sliderBar.bounds.min).x;
			maxScreenX = hudCam.WorldToScreenPoint(sliderBar.bounds.max).x;
			diff = max - min;
			worldXMinPosition = sliderBar.bounds.min.x;
			worldXDiff = sliderBar.bounds.max.x - worldXMinPosition;
			float num = (float)floatExtraOption.parsedValue;
			if (max < num)
			{
				SetGraphics(1f);
			}
			else if (min > num)
			{
				SetGraphics(0f);
			}
			else
			{
				SetGraphics((num - min) / diff);
			}
		}
	}

	private void SetGraphics(float percent)
	{
		if (hasStarted)
		{
			sliderButton.position = new Vector3(worldXMinPosition + worldXDiff * percent, sliderButton.position.y, sliderButton.position.z);
			whitePartOfTheSliderBar.position = new Vector3(worldXMinPosition + worldXDiff * percent / 2f, whitePartOfTheSliderBar.position.y, whitePartOfTheSliderBar.position.z);
			whitePartOfTheSliderBar.localScale = new Vector3(2.2f * percent, whitePartOfTheSliderBar.localScale.y, whitePartOfTheSliderBar.localScale.z);
			string text = floatExtraOption.argumentNames[0];
			textMesh.text = ((text.Length <= 0) ? floatExtraOption.currentValue.ToString() : (text + " (" + floatExtraOption.currentValue + ")"));
		}
	}

	public override void OnClicked()
	{
		calculateWithMouse = true;
	}

	public override void OnClickReleased()
	{
		calculateWithMouse = false;
	}

	private void Update()
	{
		if (calculateWithMouse)
		{
			float num = Mathf.Min(Mathf.Max(minScreenX, InputManager.CursorPosition().x), maxScreenX);
			float num2 = (num - minScreenX) / (maxScreenX - minScreenX);
			SetGraphics(num2);
			floatExtraOption.SetValue(min + diff * num2);
		}
	}

	public void AddPercentage(float multipleOfBar)
	{
		float num = (float)floatExtraOption.parsedValue;
		float value = num + diff * multipleOfBar;
		SetValue(value);
	}

	public float GetValue()
	{
		return (float)floatExtraOption.parsedValue;
	}

	public void SetValue(float newValue)
	{
		if (max <= newValue)
		{
			floatExtraOption.SetValue(max);
			SetGraphics(1f);
		}
		else if (min >= newValue)
		{
			floatExtraOption.SetValue(min);
			SetGraphics(0f);
		}
		else
		{
			float graphics = (newValue - min) / (max - min);
			floatExtraOption.SetValue(newValue);
			SetGraphics(graphics);
		}
	}

	public void Nudge(float direction)
	{
		float num = (float)floatExtraOption.parsedValue;
		float value = Mathf.Clamp(num + direction, min, max);
		SetValue(Mathf.Clamp(value, min, max));
	}

	public void Reset()
	{
		floatExtraOption.Reset();
		float num = (float)floatExtraOption.parsedValue;
		SetGraphics((num - min) / diff);
	}
}
