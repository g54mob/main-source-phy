namespace FidelityFX.FSR3
{
	public interface IFsr3UpscalerCallbacks
	{
		void ApplyMipmapBias(float biasOffset);

		void UndoMipmapBias();
	}
}
