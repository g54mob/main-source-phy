namespace FidelityFX.FSR3
{
	public class Fsr3UpscalerCallbacksBase : IFsr3UpscalerCallbacks
	{
		protected float CurrentBiasOffset;

		public virtual void ApplyMipmapBias(float biasOffset)
		{
		}

		public virtual void UndoMipmapBias()
		{
		}
	}
}
