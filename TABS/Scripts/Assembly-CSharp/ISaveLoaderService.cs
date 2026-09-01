using System.Collections.Generic;
using Landfall.TABS;
using Landfall.TABS.Workshop;

public interface ISaveLoaderService : IService
{
	bool IsReady { get; }

	bool HasBeatenLevel(DatabaseID lvl, DatabaseID campaignID);

	void AddBeatenLevel(DatabaseID lvl, DatabaseID campaignID);

	void ClearLevels(List<TABSCampaignAsset> availableCampaigns);

	void ClearSecrets();

	bool HasUnlockedSecret(string key);

	List<SecretUnlockCondition> UnlockSecret(string key);

	void SetFactionBar(Faction[] factions);

	Faction[] GetFactionBar();

	void AddWinStreakLevel(DatabaseID lvl, DatabaseID campaignID);

	void ClearWinStreak(DatabaseID campaignID);

	bool HasBeatenCampaignWithoutLosing(DatabaseID campaignID);

	bool LevelIsInWinStreak(DatabaseID lvl, DatabaseID campaignID);

	void AddNonPossessedLevel(DatabaseID lvl, DatabaseID campaignID);

	void ClearNonPossessedCampaign(DatabaseID campaignID);

	bool HasBeatenNonPossessedCamapaign(DatabaseID camapaignID);
}
