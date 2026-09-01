public struct RequiredMod
{
	public string Name;

	public string WorkshopId;

	public string UniqueIdentity;

	public readonly bool HasWorkshopId => !string.IsNullOrWhiteSpace(WorkshopId);
}
