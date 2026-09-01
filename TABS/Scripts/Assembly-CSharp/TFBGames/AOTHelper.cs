using Landfall.TABS.AI.Components;
using Landfall.TABS.AI.Components.Modifiers;
using Landfall.TABS.AI.Components.Tags;
using Unity.Entities;
using UnityEngine;

namespace TFBGames
{
	internal static class AOTHelper
	{
		static AOTHelper()
		{
			AOTTargetingHelper();
		}

		private static void AOTTargetingHelper()
		{
			if (Application.platform == RuntimePlatform.TizenPlayer)
			{
				EntityManager entityManager = new EntityManager();
				entityManager.AddComponentData(default(Entity), default(EnemyLeastWeightTargeting));
				entityManager.AddComponentData(default(Entity), default(FriendlyLeastHealthTargeting));
				entityManager.AddComponentData(default(Entity), default(FriendlyHighestPriceTargeting));
				entityManager.AddComponentData(default(Entity), default(FindNearestFriendTargeting));
				entityManager.AddComponentData(default(Entity), default(KeepPreferredDistance));
				entityManager.AddComponentData(default(Entity), default(KeepRangedDistance));
				entityManager.AddComponentData(default(Entity), default(FleeDistance));
				entityManager.AddComponentData(default(Entity), default(NeverStopRunning));
				entityManager.AddComponentData(default(Entity), default(ConfusedMovement));
				entityManager.AddComponentData(default(Entity), default(HasTargetTag));
				entityManager.AddComponentData(default(Entity), default(TargetData));
				entityManager.RemoveComponent<EnemyLeastWeightTargeting>(default(Entity));
				entityManager.RemoveComponent<FriendlyLeastHealthTargeting>(default(Entity));
				entityManager.RemoveComponent<FriendlyHighestPriceTargeting>(default(Entity));
				entityManager.RemoveComponent<FindNearestFriendTargeting>(default(Entity));
				entityManager.RemoveComponent<KeepPreferredDistance>(default(Entity));
				entityManager.RemoveComponent<KeepRangedDistance>(default(Entity));
				entityManager.RemoveComponent<FleeDistance>(default(Entity));
				entityManager.RemoveComponent<NeverStopRunning>(default(Entity));
				entityManager.RemoveComponent<ConfusedMovement>(default(Entity));
			}
		}
	}
}
