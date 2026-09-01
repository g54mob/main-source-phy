namespace NAudio.Wave
{
	public enum WaveCallbackStrategy
	{
		FunctionCallback = 0,
		NewWindow = 1,
		ExistingWindow = 2,
		Event = 3
	}
}
