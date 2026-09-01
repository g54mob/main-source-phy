using Cpp2ILInjected;

namespace Kamgam.SettingsGenerator;

public class PauseOnFocusLossConnectionSO : BoolConnectionSO
{
	private PauseOnFocusLossConnection _connection;

	public override IConnection<bool> GetConnection()
	{
		if (_connection != null)
		{
			return _connection;
		}
		PauseOnFocusLossConnection connection = new PauseOnFocusLossConnection();
		_connection = connection;
		return _connection;
	}

	public override void DestroyConnection()
	{
		//IL_0031: Expected I, but got O
		//IL_0041: Expected O, but got I
		//IL_0051: Expected O, but got I
		if (_connection != null)
		{
			PauseOnFocusLossConnection connection = _connection;
			nint num = (nint)connection;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rdx_v1 (Il2CppClass<Kamgam.SettingsGenerator.PauseOnFocusLossConnection>)+278]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rdx_v1 (Il2CppClass<Kamgam.SettingsGenerator.PauseOnFocusLossConnection>)+280]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v13 @ rax_v1 (should have been resolved before IL gen)");
		}
	}

	private void Create()
	{
		PauseOnFocusLossConnection connection = new PauseOnFocusLossConnection();
		_connection = connection;
	}
}
