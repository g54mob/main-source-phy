namespace Sony.PS4.SaveData
{
	public enum FunctionTypes
	{
		Invalid = 0,
		Mount = 1,
		Unmount = 2,
		GetMountInfo = 3,
		GetMountParams = 4,
		SetMountParams = 5,
		SaveIcon = 6,
		LoadIcon = 7,
		Delete = 8,
		DirNameSearch = 9,
		Backup = 10,
		CheckBackup = 11,
		RestoreBackup = 12,
		FileOps = 13,
		OpenDialog = 14,
		NotificationUnmountWithBackup = 15,
		NotificationBackup = 16,
		NotificationAborted = 17,
		NotificationDialogOpened = 18,
		NotificationDialogClosed = 19
	}
}
