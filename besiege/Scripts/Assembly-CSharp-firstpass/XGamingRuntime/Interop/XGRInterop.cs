using System;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	internal class XGRInterop
	{
		internal const int LOCALE_NAME_MAX_LENGTH = 85;

		internal const int XPACKAGE_IDENTIFIER_MAX_LENGTH = 33;

		public const int XUserGamertagComponentClassicMaxBytes = 16;

		public const int XUserGamertagComponentModernMaxBytes = 97;

		public const int XUserGamertagComponentModernSuffixMaxBytes = 15;

		public const int XUserGamertagComponentUniqueModernMaxBytes = 101;

		private const string ThunkDllName = "XGamingRuntimeThunks";

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XClosedCaptionGetProperties(out XClosedCaptionProperties properties);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XClosedCaptionSetEnabled(NativeBool enabled);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XHighContrastGetMode(out XHighContrastMode mode);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XSpeechToTextSendString(byte[] speakerName, byte[] content, XSpeechToTextType type);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XSpeechToTextSetPositionHint(XSpeechToTextPositionHint position);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XSpeechToTextFinalizeHypothesisString(uint hypothesisId, byte[] content);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XSpeechToTextUpdateHypothesisString(uint hypothesisId, byte[] content);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XSpeechToTextBeginHypothesisString(byte[] speakerName, byte[] content, XSpeechToTextType type, out uint hypothesisId);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XSpeechToTextCancelHypothesisString(uint hypothesisId);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XGameGetXboxTitleId(out uint titleId);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XGameInviteRegisterForEvent(XTaskQueueHandle queue, IntPtr context, XGameInviteEventCallback callback, out XTaskQueueRegistrationToken token);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern NativeBool XGameInviteUnregisterForEvent(XTaskQueueRegistrationToken token, NativeBool wait);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XGameSaveInitializeProvider(XUserHandle userContext, byte[] configurationId, [MarshalAs(UnmanagedType.U1)] bool syncOnDemand, out XGameSaveProviderHandle provider);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XGameSaveInitializeProviderAsync(XUserHandle userContext, byte[] configurationId, [MarshalAs(UnmanagedType.U1)] bool syncOnDemand, XAsyncBlockPtr asyncBlock);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XGameSaveInitializeProviderResult(XAsyncBlockPtr asyncBlock, out XGameSaveProviderHandle provider);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern void XGameSaveCloseProvider(XGameSaveProviderHandle provider);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XGameSaveGetRemainingQuota(XGameSaveProviderHandle provider, out long remainingQuota);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XGameSaveGetRemainingQuotaAsync(XGameSaveProviderHandle provider, XAsyncBlockPtr asyncBlock);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XGameSaveGetRemainingQuotaResult(XAsyncBlockPtr asyncBlock, out long remainingQuota);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XGameSaveDeleteContainer(XGameSaveProviderHandle provider, byte[] containerName);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XGameSaveDeleteContainerAsync(XGameSaveProviderHandle provider, byte[] containerName, XAsyncBlockPtr asyncBlock);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XGameSaveDeleteContainerResult(XAsyncBlockPtr asyncBlock);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XGameSaveCreateContainer(XGameSaveProviderHandle provider, byte[] containerName, out XGameSaveContainerHandle containerContext);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern void XGameSaveCloseContainer(XGameSaveContainerHandle containerContext);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XGameSaveGetContainerInfo(XGameSaveProviderHandle provider, byte[] containerName, IntPtr context, XGameSaveContainerInfoCallback callback);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XGameSaveEnumerateContainerInfo(XGameSaveProviderHandle provider, IntPtr context, XGameSaveContainerInfoCallback callback);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XGameSaveEnumerateContainerInfoByName(XGameSaveProviderHandle provider, byte[] containerNamePrefix, IntPtr context, XGameSaveContainerInfoCallback callback);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XGameSaveEnumerateBlobInfo(XGameSaveContainerHandle container, IntPtr context, XGameSaveBlobInfoCallback callback);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XGameSaveEnumerateBlobInfoByName(XGameSaveContainerHandle container, byte[] blobNamePrefix, IntPtr context, XGameSaveBlobInfoCallback callback);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XGameSaveReadBlobData(XGameSaveContainerHandle container, IntPtr blobNames, ref uint countOfBlobs, SizeT blobsSize, IntPtr allocatedBlobPtr);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XGameSaveReadBlobDataAsync(XGameSaveContainerHandle container, IntPtr blobNames, uint countOfBlobs, XAsyncBlockPtr asyncBlock);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XGameSaveReadBlobDataResult(XAsyncBlockPtr asyncBlock, SizeT blobsSize, IntPtr allocatedBlobPtr, out uint countOfBlobs);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XGameSaveCreateUpdate(XGameSaveContainerHandle container, byte[] containerDisplayName, ref XGameSaveUpdateHandle updateContext);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern void XGameSaveCloseUpdate(XGameSaveUpdateHandle context);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XGameSaveSubmitBlobWrite(XGameSaveUpdateHandle context, byte[] blobName, byte[] data, SizeT byteCount);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XGameSaveSubmitBlobDelete(XGameSaveUpdateHandle updateContext, byte[] blobName);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XGameSaveSubmitUpdate(XGameSaveUpdateHandle updateContext);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XGameSaveSubmitUpdateAsync(XGameSaveUpdateHandle updateContext, XAsyncBlockPtr asyncBlock);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XGameSaveSubmitUpdateResult(XAsyncBlockPtr asyncBlock);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XGameUiShowAchievementsAsync(XAsyncBlockPtr asyncBlock, XUserHandle requestingUser, uint titleId);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XGameUiShowAchievementsResult(XAsyncBlockPtr asyncBlock);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XGameUiShowMessageDialogAsync(XAsyncBlockPtr asyncBlock, byte[] titleText, byte[] contentText, byte[] firstButtonText, byte[] secondButtonText, byte[] thirdButtonText, XGameUiMessageDialogButton defaultButton, XGameUiMessageDialogButton cancelButton);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XGameUiShowMessageDialogResult(XAsyncBlockPtr asyncBlock, out XGameUiMessageDialogButton resultButton);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XGameUiShowErrorDialogAsync(XAsyncBlockPtr asyncBlock, int errorCode, [Optional] byte[] context);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XGameUiShowErrorDialogResult(XAsyncBlockPtr asyncBlock);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XGameUiShowSendGameInviteAsync(XAsyncBlockPtr asyncBlock, XUserHandle requestingUser, byte[] sessionConfigurationId, byte[] sessionTemplateName, byte[] sessionId, byte[] invitationText, byte[] customActivationContext);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XGameUiShowSendGameInviteResult(XAsyncBlockPtr asyncBlock);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XGameUiShowPlayerProfileCardAsync(XAsyncBlockPtr asyncBlock, XUserHandle requestingUser, ulong targetPlayer);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XGameUiShowPlayerProfileCardResult(XAsyncBlockPtr asyncBlock);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XGameUiShowPlayerPickerAsync(XAsyncBlockPtr asyncBlock, XUserHandle requestingUser, byte[] promptText, uint selectFromPlayersCount, [In] ulong[] selectFromPlayers, uint preSelectedPlayersCount, [In] ulong[] preSelectedPlayers, uint minSelectionCount, uint maxSelectionCount);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XGameUiShowPlayerPickerResultCount(XAsyncBlockPtr asyncBlock, out uint resultPlayersCount);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XGameUiShowPlayerPickerResult(XAsyncBlockPtr asyncBlock, uint resultPlayersCount, [In][Out][MarshalAs(UnmanagedType.LPArray)] ulong[] resultPlayers, out uint resultPlayersUsed);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XGameUiSetNotificationPositionHint(XGameUiNotificationPositionHint position);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XGameUiShowTextEntryAsync(XAsyncBlockPtr asyncBlock, byte[] titleText, byte[] descriptionText, byte[] defaultText, XGameUiTextEntryInputScope inputScope, uint maxTextLength);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XGameUiShowTextEntryResultSize(XAsyncBlockPtr asyncBlock, out uint resultTextBufferSize);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XGameUiShowTextEntryResult(XAsyncBlockPtr asyncBlock, uint resultTextBufferSize, [Out] byte[] resultTextBuffer, out uint resultTextBufferUsed);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XGameUiShowWebAuthenticationAsync(XAsyncBlockPtr asyncBlock, XUserHandle requestingUser, byte[] requestUri, byte[] completionUri);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XGameUiShowWebAuthenticationResultSize(XAsyncBlockPtr asyncBlock, out SizeT bufferSize);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XGameUiShowWebAuthenticationResult(XAsyncBlockPtr asyncBlock, SizeT bufferSize, IntPtr buffer, out IntPtr ptrToBuffer, out SizeT bufferUsed);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XLaunchUri(XUserHandle requestingUser, byte[] uri);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern void XPackageCloseMountHandle(XPackageMountHandle mount);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XPackageEnumerateFeatures(byte[] packageIdentifier, IntPtr context, XPackageFeatureEnumerationCallback callback);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XPackageEstimateDownloadSize(byte[] packageIdentifier, uint selectorCount, [In] XPackageChunkSelector[] selectors, out ulong downloadSize, out NativeBool shouldPresentUserConfirmation);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XPackageEnumeratePackages(XPackageKind kind, XPackageEnumerationScope scope, IntPtr context, XPackageEnumerationCallback callback);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XPackageRegisterPackageInstalled(XTaskQueueHandle queue, IntPtr context, XPackageInstalledCallback callback, out XTaskQueueRegistrationToken token);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XPackageGetWriteStats(out XPackageWriteStats writeStats);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XPackageUninstallUWPInstance(byte[] packageName);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XPackageRegisterInstallationProgressChanged(XPackageInstallationMonitorHandle installationMonitor, IntPtr context, XPackageInstallationProgressCallback callback, out XTaskQueueRegistrationToken token);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XPackageMount(byte[] packageIdentifier, out XPackageMountHandle mount);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XPackageGetCurrentProcessPackageIdentifier(SizeT bufferSize, byte[] buffer);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XPackageGetMountPath(XPackageMountHandle mount, SizeT pathSize, byte[] path);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern NativeBool XPackageIsPackagedProcess();

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern NativeBool XPackageUnregisterPackageInstalled(XTaskQueueRegistrationToken token, NativeBool wait);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XPackageCreateInstallationMonitor(byte[] packageIdentifier, uint selectorCount, [In] XPackageChunkSelector[] selectors, uint minimumUpdateIntervalMs, XTaskQueueHandle queue, out XPackageInstallationMonitorHandle installationMonitor);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern void XPackageCloseInstallationMonitorHandle(XPackageInstallationMonitorHandle installationMonitor);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern NativeBool XPackageUnregisterInstallationProgressChanged(XPackageInstallationMonitorHandle installationMonitor, XTaskQueueRegistrationToken token, NativeBool wait);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XPackageGetMountPathSize(XPackageMountHandle mount, out SizeT pathSize);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XPackageGetUserLocale(SizeT localeSize, byte[] locale);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern void XPackageGetInstallationProgress(XPackageInstallationMonitorHandle installationMonitor, out XPackageInstallationProgress progress);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern NativeBool XPackageUpdateInstallationMonitor(XPackageInstallationMonitorHandle installationMonitor);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreCreateContext(XUserHandle user, out XStoreContextHandle storeContextHandle);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern void XStoreCloseContextHandle(XStoreContextHandle storeContextHandle);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern NativeBool XStoreIsAvailabilityPurchasable(XStoreAvailability availability);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreAcquireLicenseForPackageAsync(XStoreContextHandle storeContextHandle, byte[] packageIdentifier, XAsyncBlockPtr async);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreAcquireLicenseForPackageResult(XAsyncBlockPtr async, out XStoreLicenseHandle storeLicenseHandle);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreCanAcquireLicenseForPackageAsync(XStoreContextHandle storeContextHandle, byte[] packageIdentifier, XAsyncBlockPtr async);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreCanAcquireLicenseForPackageResult(XAsyncBlockPtr async, out XStoreCanAcquireLicenseResult storeCanAcquireLicense);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreCanAcquireLicenseForStoreIdAsync(XStoreContextHandle storeContextHandle, byte[] storeProductId, XAsyncBlockPtr async);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreCanAcquireLicenseForStoreIdResult(XAsyncBlockPtr async, out XStoreCanAcquireLicenseResult storeCanAcquireLicense);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern void XStoreCloseLicenseHandle(XStoreLicenseHandle storeLicenseHandle);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern NativeBool XStoreIsLicenseValid(XStoreLicenseHandle storeLicenseHandle);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreQueryAddOnLicensesAsync(XStoreContextHandle storeContextHandle, XAsyncBlockPtr async);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreQueryAddOnLicensesResult(XAsyncBlockPtr async, uint count, [Out] XStoreAddonLicense[] addOnLicenses);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreQueryAddOnLicensesResultCount(XAsyncBlockPtr async, out uint count);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreQueryGameLicenseAsync(XStoreContextHandle storeContextHandle, XAsyncBlockPtr async);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreQueryGameLicenseResult(XAsyncBlockPtr async, out XStoreGameLicense license);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreQueryLicenseTokenAsync(XStoreContextHandle storeContextHandle, [In] UTF8StringPtr[] productIds, SizeT productIdsCount, byte[] customDeveloperString, XAsyncBlockPtr async);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreQueryLicenseTokenResult(XAsyncBlockPtr async, SizeT size, byte[] result);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreQueryLicenseTokenResultSize(XAsyncBlockPtr async, out SizeT size);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreRegisterGameLicenseChanged(XStoreContextHandle storeContextHandle, XTaskQueueHandle queue, IntPtr context, XStoreGameLicenseChangedCallback callback, out XTaskQueueRegistrationToken token);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreRegisterPackageLicenseLost(XStoreLicenseHandle licenseHandle, XTaskQueueHandle queue, IntPtr context, XStorePackageLicenseLostCallback callback, out XTaskQueueRegistrationToken token);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern NativeBool XStoreUnregisterGameLicenseChanged(XStoreContextHandle storeContextHandle, XTaskQueueRegistrationToken token, NativeBool wait);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern NativeBool XStoreUnregisterPackageLicenseLost(XStoreLicenseHandle licenseHandle, XTaskQueueRegistrationToken token, NativeBool wait);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreAcquireLicenseForDurablesAsync(XStoreContextHandle storeContextHandle, byte[] storeId, XAsyncBlockPtr async);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreAcquireLicenseForDurablesResult(XAsyncBlockPtr async, out XStoreLicenseHandle storeLicenseHandle);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreQueryGameAndDlcPackageUpdatesAsync(XStoreContextHandle storeContextHandle, XAsyncBlockPtr async);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreQueryGameAndDlcPackageUpdatesResultCount(XAsyncBlockPtr async, out uint count);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreQueryGameAndDlcPackageUpdatesResult(XAsyncBlockPtr async, uint count, [Out] XStorePackageUpdate[] packageUpdates);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreDownloadAndInstallPackagesAsync(XStoreContextHandle storeContextHandle, [In] UTF8StringPtr[] storeIds, SizeT storeIdsCount, XAsyncBlockPtr async);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreDownloadAndInstallPackagesResultCount(XAsyncBlockPtr async, out uint count);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreDownloadAndInstallPackagesResult(XAsyncBlockPtr async, uint count, [In] byte[] packageIdentifiers);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreDownloadAndInstallPackageUpdatesAsync(XStoreContextHandle storeContextHandle, [In] UTF8StringPtr[] packageIdentifiers, SizeT packageIdentifiersCount, XAsyncBlockPtr async);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreDownloadAndInstallPackageUpdatesResult(XAsyncBlockPtr async);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreDownloadPackageUpdatesAsync(XStoreContextHandle storeContextHandle, [In] UTF8StringPtr[] packageIdentifiers, SizeT packageIdentifiersCount, XAsyncBlockPtr async);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreDownloadPackageUpdatesResult(XAsyncBlockPtr async);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreQueryPackageIdentifier(byte[] storeId, SizeT size, byte[] packageIdentifier);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreShowRedeemTokenUIAsync(XStoreContextHandle storeContextHandle, byte[] token, [In] UTF8StringPtr[] allowedStoreIds, SizeT allowedStoreIdsCount, NativeBool disallowCsvRedemption, XAsyncBlockPtr async);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreShowRedeemTokenUIResult(XAsyncBlockPtr async);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreShowRateAndReviewUIAsync(XStoreContextHandle storeContextHandle, XAsyncBlockPtr async);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreShowRateAndReviewUIResult(XAsyncBlockPtr async, out XStoreRateAndReviewResult result);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreShowPurchaseUIAsync(XStoreContextHandle storeContextHandle, byte[] storeId, byte[] name, byte[] extendedJsonData, XAsyncBlockPtr async);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreShowPurchaseUIResult(XAsyncBlockPtr async);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreQueryConsumableBalanceRemainingAsync(XStoreContextHandle storeContextHandle, byte[] storeProductId, XAsyncBlockPtr async);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreQueryConsumableBalanceRemainingResult(XAsyncBlockPtr async, out XStoreConsumableResult consumableResult);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreReportConsumableFulfillmentAsync(XStoreContextHandle storeContextHandle, byte[] storeProductId, uint quantity, Guid trackingId, XAsyncBlockPtr async);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreReportConsumableFulfillmentResult(XAsyncBlockPtr async, out XStoreConsumableResult consumableResult);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreGetUserCollectionsIdAsync(XStoreContextHandle storeContextHandle, byte[] serviceTicket, byte[] publisherUserId, XAsyncBlockPtr async);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreGetUserCollectionsIdResultSize(XAsyncBlockPtr async, out SizeT size);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreGetUserCollectionsIdResult(XAsyncBlockPtr async, SizeT size, byte[] result);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreGetUserPurchaseIdAsync(XStoreContextHandle storeContextHandle, byte[] serviceTicket, byte[] publisherUserId, XAsyncBlockPtr async);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreGetUserPurchaseIdResultSize(XAsyncBlockPtr async, out SizeT size);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreGetUserPurchaseIdResult(XAsyncBlockPtr async, SizeT size, byte[] result);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreQueryAssociatedProductsAsync(XStoreContextHandle storeContextHandle, XStoreProductKind productKinds, uint maxItemsToRetrievePerPage, XAsyncBlockPtr async);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreQueryAssociatedProductsResult(XAsyncBlockPtr async, out XStoreProductQueryHandle productQueryHandle);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern void XStoreCloseProductsQueryHandle(XStoreProductQueryHandle productQueryHandle);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern NativeBool XStoreProductsQueryHasMorePages(XStoreProductQueryHandle productQueryHandle);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreEnumerateProductsQuery(XStoreProductQueryHandle productQueryHandle, IntPtr context, XStoreProductQueryCallback callback);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreProductsQueryNextPageAsync(XStoreProductQueryHandle productQueryHandle, XAsyncBlockPtr async);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreProductsQueryNextPageResult(XAsyncBlockPtr async, out XStoreProductQueryHandle productQueryHandle);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreQueryEntitledProductsAsync(XStoreContextHandle storeContextHandle, XStoreProductKind productKinds, uint maxItemsToRetrievePerPage, XAsyncBlockPtr async);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreQueryEntitledProductsResult(XAsyncBlockPtr async, out XStoreProductQueryHandle productQueryHandle);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreQueryProductForCurrentGameAsync(XStoreContextHandle storeContextHandle, XAsyncBlockPtr async);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreQueryProductForCurrentGameResult(XAsyncBlockPtr async, out XStoreProductQueryHandle productQueryHandle);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreQueryProductForPackageAsync(XStoreContextHandle storeContextHandle, XStoreProductKind productKinds, byte[] packageIdentifier, XAsyncBlockPtr async);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreQueryProductForPackageResult(XAsyncBlockPtr async, out XStoreProductQueryHandle productQueryHandle);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreQueryProductsAsync(XStoreContextHandle storeContextHandle, XStoreProductKind productKinds, [In] UTF8StringPtr[] storeIds, SizeT storeIdsCount, [In] UTF8StringPtr[] actionFilters, SizeT actionFiltersCount, XAsyncBlockPtr async);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XStoreQueryProductsResult(XAsyncBlockPtr async, out XStoreProductQueryHandle productQueryHandle);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern NativeBool XThreadIsTimeSensitive();

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern int XThreadSetTimeSensitive(NativeBool isTimeSensitiveThread);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		internal static extern void XThreadAssertNotTimeSensitive();

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XUserDuplicateHandle(XUserHandle handle, out XUserHandle duplicatedHandle);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern void XUserCloseHandle(XUserHandle user);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XUserCompare(XUserHandle user1, XUserHandle user2);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XUserGetMaxUsers(out uint maxUsers);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XUserAddAsync(XUserAddOptions addOptions, XAsyncBlockPtr asyncBlock);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XUserAddResult(XAsyncBlockPtr asyncBlock, out XUserHandle newUser);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XUserGetId(XUserHandle user, out ulong userId);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XUserFindUserById(ulong userId, out XUserHandle handle);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XUserGetLocalId(XUserHandle user, out XUserLocalId userId);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XUserFindUserByLocalId(XUserLocalId userLocalId, out XUserHandle handle);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XUserGetIsGuest(XUserHandle user, [MarshalAs(UnmanagedType.U1)] out bool isGuest);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XUserGetState(XUserHandle user, out XUserState state);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XUserGetGamertag(XUserHandle user, XUserGamertagComponent gamertagComponent, SizeT gamertagSize, [Out] byte[] gamertag, out SizeT gamertagUsed);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XUserGetGamerPictureAsync(XUserHandle user, XUserGamerPictureSize pictureSize, XAsyncBlockPtr asyncBlock);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XUserGetGamerPictureResultSize(XAsyncBlockPtr asyncBlock, out SizeT bufferSize);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XUserGetGamerPictureResult(XAsyncBlockPtr asyncBlock, SizeT bufferSize, [Out] byte[] buffer, out SizeT bufferUsed);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XUserGetAgeGroup(XUserHandle userLocalId, out XUserAgeGroup ageGroup);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XUserCheckPrivilege(XUserHandle user, XUserPrivilegeOptions options, XUserPrivilege privilege, [MarshalAs(UnmanagedType.U1)] out bool hasPrivilege, out XUserPrivilegeDenyReason reason);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XUserResolvePrivilegeWithUiAsync(XUserHandle user, XUserPrivilegeOptions options, XUserPrivilege privilege, XAsyncBlockPtr asyncBlock);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XUserResolvePrivilegeWithUiResult(XAsyncBlockPtr asyncBlock);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XUserGetTokenAndSignatureUtf16Async(XUserHandle user, XUserGetTokenAndSignatureOptions options, [MarshalAs(UnmanagedType.LPWStr)] string method, [MarshalAs(UnmanagedType.LPWStr)] string url, SizeT headerCount, [Optional] XUserGetTokenAndSignatureUtf16HttpHeader[] headers, SizeT bodySize, [Optional] byte[] body, XAsyncBlockPtr asyncBlock);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XUserGetTokenAndSignatureUtf16ResultSize(XAsyncBlockPtr asyncBlock, out SizeT bufferSize);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XUserGetTokenAndSignatureUtf16Result(XAsyncBlockPtr asyncBlock, SizeT bufferSize, IntPtr buffer, out IntPtr ptrToBuffer, out SizeT bufferUsed);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XUserResolveIssueWithUiUtf16Async(XUserHandle user, [Optional][MarshalAs(UnmanagedType.LPWStr)] string url, XAsyncBlockPtr asyncBlock);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XUserResolveIssueWithUiUtf16Result(XAsyncBlockPtr asyncBlock);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XUserRegisterForChangeEvent(XTaskQueueHandle queue, IntPtr context, XUserChangeEventCallback callback, out XTaskQueueRegistrationToken token);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool XUserUnregisterForChangeEvent(XTaskQueueRegistrationToken token, bool wait);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XUserGetSignOutDeferral(out XUserSignOutDeferralHandle deferral);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern void XUserCloseSignOutDeferralHandle(XUserSignOutDeferralHandle deferral);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XGameRuntimeInitialize();

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern void XGameRuntimeUninitialize();

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XTaskQueueCreate(XTaskQueueDispatchMode workDispatchMode, XTaskQueueDispatchMode completionDispatchMode, out XTaskQueueHandle queue);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern void XTaskQueueCloseHandle(XTaskQueueHandle queue);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern void XTaskQueueDispatch(XTaskQueueHandle queue, XTaskQueuePort port, uint timeoutInMs);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XAsyncGetStatus(XAsyncBlockPtr asyncBlock, [MarshalAs(UnmanagedType.U1)] bool wait);

		[DllImport("XGamingRuntimeThunks", CallingConvention = CallingConvention.StdCall)]
		public static extern int XAsyncGetResultSize(XAsyncBlockPtr asyncBlock, out SizeT bufferSize);
	}
}
