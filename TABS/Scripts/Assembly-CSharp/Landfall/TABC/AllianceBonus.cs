using System;
using UnityEngine;

namespace Landfall.TABC
{
	[Serializable]
	public class AllianceBonus
	{
		public int unitsNeeded = 2;

		[TextArea(5, 20)]
		public string description;

		public BuffObject[] buffObjects;
	}
}
