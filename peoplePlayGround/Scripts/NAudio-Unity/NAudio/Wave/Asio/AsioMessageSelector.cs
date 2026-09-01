namespace NAudio.Wave.Asio
{
	public enum AsioMessageSelector
	{
		kAsioSelectorSupported = 1,
		kAsioEngineVersion = 2,
		kAsioResetRequest = 3,
		kAsioBufferSizeChange = 4,
		kAsioResyncRequest = 5,
		kAsioLatenciesChanged = 6,
		kAsioSupportsTimeInfo = 7,
		kAsioSupportsTimeCode = 8,
		kAsioMMCCommand = 9,
		kAsioSupportsInputMonitor = 10,
		kAsioSupportsInputGain = 11,
		kAsioSupportsInputMeter = 12,
		kAsioSupportsOutputGain = 13,
		kAsioSupportsOutputMeter = 14,
		kAsioOverload = 15
	}
}
