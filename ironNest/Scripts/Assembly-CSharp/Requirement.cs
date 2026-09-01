using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

[Serializable]
public class Requirement
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum RequirementTypes
	{
		None = 0,
		ShellSlotCount = 1,
		TurretIsMoving = 2
	}

	[JsonConverter(typeof(StringEnumConverter))]
	public enum OperationTypes
	{
		Equals = 0,
		NotEquals = 1,
		LessThan = 2,
		GreaterThan = 3,
		LessThanOrEquals = 4,
		GreaterThanOrEquals = 5
	}

	[JsonConverter(typeof(StringEnumConverter))]
	public enum ShellSlots
	{
		Right = 0,
		Left = 1,
		Any = 2,
		PunchardVaribale = 3
	}

	public RequirementTypes RequirementType;

	public OperationTypes Operation;

	public ShellSlots ShellSlot;

	public string StringValue;

	public int IntValue;

	public bool BoolValue;

	public bool Execute(Dictionary<string, object> variables)
	{
		return false;
	}
}
