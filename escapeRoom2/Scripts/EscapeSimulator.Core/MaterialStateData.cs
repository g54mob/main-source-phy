using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MaterialStateData : ScriptableObject
{
	[SerializeField]
	public List<MaterialState.MaterialStateRecord> materialStates;

	[SerializeField]
	public List<MaterialLink> linkedMaterials;
}
