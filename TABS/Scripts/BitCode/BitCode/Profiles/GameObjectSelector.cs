using System;
using UnityEngine;

namespace BitCode.Profiles
{
	[Serializable]
	public sealed class GameObjectSelector : Selector<GameObject, GameObjectProfileRules, GameObjectProfileRulesContainer>
	{
	}
}
