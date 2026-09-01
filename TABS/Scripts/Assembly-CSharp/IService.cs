public interface IService
{
	void OnRegister();

	void OnAwake();

	void OnStart();

	void OnUpdate();

	void OnFixedUpdate();

	void OnLateUpdate();

	void UnRegister();
}
