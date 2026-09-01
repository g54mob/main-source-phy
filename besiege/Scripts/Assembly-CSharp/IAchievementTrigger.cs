internal interface IAchievementTrigger
{
	void OnUpdate(int levelIndex);

	void OnEnterGlobalSimulation(int levelIndex);

	void OnExitGlobalSimulation(int levelIndex);

	void OnSinglePlayerLevelComplete(int levelIndex, float completionTime, Machine machine);
}
