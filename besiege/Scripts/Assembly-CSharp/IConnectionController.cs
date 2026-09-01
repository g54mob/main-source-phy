public interface IConnectionController
{
	bool DoneTesting { get; }

	bool IsInitialized { get; }

	void Setup(ExtendedNATHelper natHelper);

	void Retest();
}
