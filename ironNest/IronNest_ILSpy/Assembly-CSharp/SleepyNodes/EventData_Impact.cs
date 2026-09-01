using System.Collections.Generic;
using UnityEngine;

namespace SleepyNodes;

public class EventData_Impact : EventNode.EventData
{
	public ShellDefinition ImpactShell;

	public Vector2 ImpactLocation;

	public bool TriggerNormalEvents = true;

	public List<MapEntity> ImpactEntities;
}
