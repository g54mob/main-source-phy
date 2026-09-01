namespace XGamingRuntime
{
	public enum XblErrorCondition : uint
	{
		NoError = 0u,
		GenericError = 1u,
		GenericOutOfRange = 2u,
		Auth = 3u,
		Network = 4u,
		HttpGeneric = 5u,
		Http304NotModified = 6u,
		Http404NotFound = 7u,
		Http412PreconditionFailed = 8u,
		Http429TooManyRequests = 9u,
		HttpServiceTimeout = 10u,
		Rta = 11u
	}
}
