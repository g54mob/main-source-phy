public class MaterialToolTipText : ToolTipText
{
	public BridgeMaterialType m_MaterialType;

	private BridgeMaterial m_Material;

	public override string GetText()
	{
		string text = ((m_LocalizationKey != ToolTipLocalizationKey.TOOLTIP_MISSING) ? Localize.Get(m_LocalizationKey.ToString()) : Localize.Get(m_RawLocalizationKey));
		if (m_Material == null)
		{
			m_Material = BridgeMaterials.GetBridgeMaterial(m_MaterialType);
		}
		if (m_Material != null)
		{
			text = ((m_Material.m_MaterialType != BridgeMaterialType.PILLAR) ? (text + $"\n${m_Material.m_PricePerMeter}/m") : (text + $"\n${BridgePillars.BASE_COST} + {m_Material.m_PricePerMeter}/m"));
		}
		string text2 = Bindings.GetBinding(m_BindingType)?.GetTooltipBindingString();
		if (!string.IsNullOrEmpty(text2))
		{
			text = text + "\n(" + text2 + ")";
		}
		if (GameInput.GetActiveGameDevice() != GameDevice.KeyboardAndMouse)
		{
			int num = text.IndexOf('(');
			if (num == -1)
			{
				num = text.IndexOf('（');
			}
			if (num <= 0)
			{
				return text;
			}
			return text.Substring(0, num - 1);
		}
		return text;
	}
}
