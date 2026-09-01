namespace XGamingRuntime
{
	public enum XblWriteSessionStatus : uint
	{
		Unknown = 0u,
		AccessDenied = 1u,
		Created = 2u,
		Conflict = 3u,
		HandleNotFound = 4u,
		OutOfSync = 5u,
		SessionDeleted = 6u,
		Updated = 7u
	}
}
