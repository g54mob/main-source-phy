public interface IWorkshopItem
{
	ulong WorkshopItemId { get; }

	bool IsPublishedItem { get; }

	bool IsInstalled { get; }

	bool IsOwner { get; }

	uint DlcDependencyMask { get; }

	bool AreDlcRequirementsMet { get; }
}
