using UnityEngine;

public class PunchcardVariable : MonoBehaviour
{
	public enum VariableTypes
	{
		Int = 0,
		Float = 1,
		Text = 2,
		Coordinate = 3,
		Bool = 4,
		ShellSlot = 5
	}

	public string VariableID;

	public VariableTypes VariableType;

	[Header("Values")]
	public int VariableInt;

	public float VariableFloat;

	public string VariableText;

	public GridReference VariableCoordinate;

	public bool VariableBool;

	public ShellSlotPool.ShellSlotSides VariableShellSlot;

	public object Get()
	{
		return null;
	}

	public void SetInt(int value)
	{
	}

	public void SetFloat(float value)
	{
	}

	public void SetText(string value)
	{
	}

	public void SetText(bool value)
	{
	}

	public void SetCoordinate(GridReference value)
	{
	}

	public void SetCoordinate_GridLocation(GridLocations location)
	{
	}

	public void SetCoordinate_GridLocation(string location)
	{
	}

	public void SetCoordinate_GridLocation_L(string l)
	{
	}

	public void SetCoordinate_GridLocation_L_FromIndex(float l)
	{
	}

	public void SetCoordinate_GridLocation_L_FromIndex(int l)
	{
	}

	public void SetCoordinate_GridLocation_N(float n)
	{
	}

	public void SetCoordinate_GridLocation_N(int n)
	{
	}

	public void SetCoordinate_GridLocation_X(int x)
	{
	}

	public void SetCoordinate_GridLocation_Y(int y)
	{
	}

	public void SetShellSlot(float f)
	{
	}

	public void SetShellSlot(int f)
	{
	}

	public void SetShellSlot(bool right)
	{
	}

	public void SetShellSlot_Right()
	{
	}

	public void SetShellSlot_Left()
	{
	}
}
