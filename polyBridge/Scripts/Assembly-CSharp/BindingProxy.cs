using UnityEngine;

public class BindingProxy
{
	public BindingType m_BindingType;

	public KeyCode m_KeyCode;

	public KeyCode m_AltKeyCode;

	public BindingProxy(BindingType bindingType, KeyCode keyCode, KeyCode altKeyCode)
	{
		m_BindingType = bindingType;
		m_KeyCode = keyCode;
		m_AltKeyCode = altKeyCode;
	}
}
