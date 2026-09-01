using System;
using PlayFab.AuthenticationModels;
using PlayFab.ClientModels;
using PlayFab.CloudScriptModels;
using PlayFab.DataModels;
using PlayFab.EventsModels;
using PlayFab.ExperimentationModels;
using PlayFab.GroupsModels;
using PlayFab.InsightsModels;
using PlayFab.Internal;
using PlayFab.LocalizationModels;
using PlayFab.MultiplayerModels;
using PlayFab.ProfilesModels;
using PlayFab.SharedModels;

namespace PlayFab.Events
{
	public class PlayFabEvents
	{
		public delegate void PlayFabErrorEvent(PlayFabRequestCommon request, PlayFabError error);

		public delegate void PlayFabResultEvent<in TResult>(TResult result) where TResult : PlayFabResultCommon;

		public delegate void PlayFabRequestEvent<in TRequest>(TRequest request) where TRequest : PlayFabRequestCommon;

		private static PlayFabEvents _instance;

		public event PlayFabRequestEvent<GetEntityTokenRequest> OnAuthenticationGetEntityTokenRequestEvent;

		public event PlayFabResultEvent<GetEntityTokenResponse> OnAuthenticationGetEntityTokenResultEvent;

		public event PlayFabRequestEvent<ValidateEntityTokenRequest> OnAuthenticationValidateEntityTokenRequestEvent;

		public event PlayFabResultEvent<ValidateEntityTokenResponse> OnAuthenticationValidateEntityTokenResultEvent;

		public event PlayFabResultEvent<LoginResult> OnLoginResultEvent;

		public event PlayFabRequestEvent<AcceptTradeRequest> OnAcceptTradeRequestEvent;

		public event PlayFabResultEvent<AcceptTradeResponse> OnAcceptTradeResultEvent;

		public event PlayFabRequestEvent<AddFriendRequest> OnAddFriendRequestEvent;

		public event PlayFabResultEvent<AddFriendResult> OnAddFriendResultEvent;

		public event PlayFabRequestEvent<AddGenericIDRequest> OnAddGenericIDRequestEvent;

		public event PlayFabResultEvent<AddGenericIDResult> OnAddGenericIDResultEvent;

		public event PlayFabRequestEvent<AddOrUpdateContactEmailRequest> OnAddOrUpdateContactEmailRequestEvent;

		public event PlayFabResultEvent<AddOrUpdateContactEmailResult> OnAddOrUpdateContactEmailResultEvent;

		public event PlayFabRequestEvent<AddSharedGroupMembersRequest> OnAddSharedGroupMembersRequestEvent;

		public event PlayFabResultEvent<AddSharedGroupMembersResult> OnAddSharedGroupMembersResultEvent;

		public event PlayFabRequestEvent<AddUsernamePasswordRequest> OnAddUsernamePasswordRequestEvent;

		public event PlayFabResultEvent<AddUsernamePasswordResult> OnAddUsernamePasswordResultEvent;

		public event PlayFabRequestEvent<AddUserVirtualCurrencyRequest> OnAddUserVirtualCurrencyRequestEvent;

		public event PlayFabResultEvent<ModifyUserVirtualCurrencyResult> OnAddUserVirtualCurrencyResultEvent;

		public event PlayFabRequestEvent<AndroidDevicePushNotificationRegistrationRequest> OnAndroidDevicePushNotificationRegistrationRequestEvent;

		public event PlayFabResultEvent<AndroidDevicePushNotificationRegistrationResult> OnAndroidDevicePushNotificationRegistrationResultEvent;

		public event PlayFabRequestEvent<AttributeInstallRequest> OnAttributeInstallRequestEvent;

		public event PlayFabResultEvent<AttributeInstallResult> OnAttributeInstallResultEvent;

		public event PlayFabRequestEvent<CancelTradeRequest> OnCancelTradeRequestEvent;

		public event PlayFabResultEvent<CancelTradeResponse> OnCancelTradeResultEvent;

		public event PlayFabRequestEvent<ConfirmPurchaseRequest> OnConfirmPurchaseRequestEvent;

		public event PlayFabResultEvent<ConfirmPurchaseResult> OnConfirmPurchaseResultEvent;

		public event PlayFabRequestEvent<ConsumeItemRequest> OnConsumeItemRequestEvent;

		public event PlayFabResultEvent<ConsumeItemResult> OnConsumeItemResultEvent;

		public event PlayFabRequestEvent<ConsumeMicrosoftStoreEntitlementsRequest> OnConsumeMicrosoftStoreEntitlementsRequestEvent;

		public event PlayFabResultEvent<ConsumeMicrosoftStoreEntitlementsResponse> OnConsumeMicrosoftStoreEntitlementsResultEvent;

		public event PlayFabRequestEvent<ConsumePSNEntitlementsRequest> OnConsumePSNEntitlementsRequestEvent;

		public event PlayFabResultEvent<ConsumePSNEntitlementsResult> OnConsumePSNEntitlementsResultEvent;

		public event PlayFabRequestEvent<ConsumeXboxEntitlementsRequest> OnConsumeXboxEntitlementsRequestEvent;

		public event PlayFabResultEvent<ConsumeXboxEntitlementsResult> OnConsumeXboxEntitlementsResultEvent;

		public event PlayFabRequestEvent<CreateSharedGroupRequest> OnCreateSharedGroupRequestEvent;

		public event PlayFabResultEvent<CreateSharedGroupResult> OnCreateSharedGroupResultEvent;

		public event PlayFabRequestEvent<ExecuteCloudScriptRequest> OnExecuteCloudScriptRequestEvent;

		public event PlayFabResultEvent<PlayFab.ClientModels.ExecuteCloudScriptResult> OnExecuteCloudScriptResultEvent;

		public event PlayFabRequestEvent<GetAccountInfoRequest> OnGetAccountInfoRequestEvent;

		public event PlayFabResultEvent<GetAccountInfoResult> OnGetAccountInfoResultEvent;

		public event PlayFabRequestEvent<GetAdPlacementsRequest> OnGetAdPlacementsRequestEvent;

		public event PlayFabResultEvent<GetAdPlacementsResult> OnGetAdPlacementsResultEvent;

		public event PlayFabRequestEvent<ListUsersCharactersRequest> OnGetAllUsersCharactersRequestEvent;

		public event PlayFabResultEvent<ListUsersCharactersResult> OnGetAllUsersCharactersResultEvent;

		public event PlayFabRequestEvent<GetCatalogItemsRequest> OnGetCatalogItemsRequestEvent;

		public event PlayFabResultEvent<GetCatalogItemsResult> OnGetCatalogItemsResultEvent;

		public event PlayFabRequestEvent<GetCharacterDataRequest> OnGetCharacterDataRequestEvent;

		public event PlayFabResultEvent<GetCharacterDataResult> OnGetCharacterDataResultEvent;

		public event PlayFabRequestEvent<GetCharacterInventoryRequest> OnGetCharacterInventoryRequestEvent;

		public event PlayFabResultEvent<GetCharacterInventoryResult> OnGetCharacterInventoryResultEvent;

		public event PlayFabRequestEvent<GetCharacterLeaderboardRequest> OnGetCharacterLeaderboardRequestEvent;

		public event PlayFabResultEvent<GetCharacterLeaderboardResult> OnGetCharacterLeaderboardResultEvent;

		public event PlayFabRequestEvent<GetCharacterDataRequest> OnGetCharacterReadOnlyDataRequestEvent;

		public event PlayFabResultEvent<GetCharacterDataResult> OnGetCharacterReadOnlyDataResultEvent;

		public event PlayFabRequestEvent<GetCharacterStatisticsRequest> OnGetCharacterStatisticsRequestEvent;

		public event PlayFabResultEvent<GetCharacterStatisticsResult> OnGetCharacterStatisticsResultEvent;

		public event PlayFabRequestEvent<GetContentDownloadUrlRequest> OnGetContentDownloadUrlRequestEvent;

		public event PlayFabResultEvent<GetContentDownloadUrlResult> OnGetContentDownloadUrlResultEvent;

		public event PlayFabRequestEvent<CurrentGamesRequest> OnGetCurrentGamesRequestEvent;

		public event PlayFabResultEvent<CurrentGamesResult> OnGetCurrentGamesResultEvent;

		public event PlayFabRequestEvent<GetFriendLeaderboardRequest> OnGetFriendLeaderboardRequestEvent;

		public event PlayFabResultEvent<GetLeaderboardResult> OnGetFriendLeaderboardResultEvent;

		public event PlayFabRequestEvent<GetFriendLeaderboardAroundPlayerRequest> OnGetFriendLeaderboardAroundPlayerRequestEvent;

		public event PlayFabResultEvent<GetFriendLeaderboardAroundPlayerResult> OnGetFriendLeaderboardAroundPlayerResultEvent;

		public event PlayFabRequestEvent<GetFriendsListRequest> OnGetFriendsListRequestEvent;

		public event PlayFabResultEvent<GetFriendsListResult> OnGetFriendsListResultEvent;

		public event PlayFabRequestEvent<GameServerRegionsRequest> OnGetGameServerRegionsRequestEvent;

		public event PlayFabResultEvent<GameServerRegionsResult> OnGetGameServerRegionsResultEvent;

		public event PlayFabRequestEvent<GetLeaderboardRequest> OnGetLeaderboardRequestEvent;

		public event PlayFabResultEvent<GetLeaderboardResult> OnGetLeaderboardResultEvent;

		public event PlayFabRequestEvent<GetLeaderboardAroundCharacterRequest> OnGetLeaderboardAroundCharacterRequestEvent;

		public event PlayFabResultEvent<GetLeaderboardAroundCharacterResult> OnGetLeaderboardAroundCharacterResultEvent;

		public event PlayFabRequestEvent<GetLeaderboardAroundPlayerRequest> OnGetLeaderboardAroundPlayerRequestEvent;

		public event PlayFabResultEvent<GetLeaderboardAroundPlayerResult> OnGetLeaderboardAroundPlayerResultEvent;

		public event PlayFabRequestEvent<GetLeaderboardForUsersCharactersRequest> OnGetLeaderboardForUserCharactersRequestEvent;

		public event PlayFabResultEvent<GetLeaderboardForUsersCharactersResult> OnGetLeaderboardForUserCharactersResultEvent;

		public event PlayFabRequestEvent<GetPaymentTokenRequest> OnGetPaymentTokenRequestEvent;

		public event PlayFabResultEvent<GetPaymentTokenResult> OnGetPaymentTokenResultEvent;

		public event PlayFabRequestEvent<GetPhotonAuthenticationTokenRequest> OnGetPhotonAuthenticationTokenRequestEvent;

		public event PlayFabResultEvent<GetPhotonAuthenticationTokenResult> OnGetPhotonAuthenticationTokenResultEvent;

		public event PlayFabRequestEvent<GetPlayerCombinedInfoRequest> OnGetPlayerCombinedInfoRequestEvent;

		public event PlayFabResultEvent<GetPlayerCombinedInfoResult> OnGetPlayerCombinedInfoResultEvent;

		public event PlayFabRequestEvent<GetPlayerProfileRequest> OnGetPlayerProfileRequestEvent;

		public event PlayFabResultEvent<GetPlayerProfileResult> OnGetPlayerProfileResultEvent;

		public event PlayFabRequestEvent<GetPlayerSegmentsRequest> OnGetPlayerSegmentsRequestEvent;

		public event PlayFabResultEvent<GetPlayerSegmentsResult> OnGetPlayerSegmentsResultEvent;

		public event PlayFabRequestEvent<GetPlayerStatisticsRequest> OnGetPlayerStatisticsRequestEvent;

		public event PlayFabResultEvent<GetPlayerStatisticsResult> OnGetPlayerStatisticsResultEvent;

		public event PlayFabRequestEvent<GetPlayerStatisticVersionsRequest> OnGetPlayerStatisticVersionsRequestEvent;

		public event PlayFabResultEvent<GetPlayerStatisticVersionsResult> OnGetPlayerStatisticVersionsResultEvent;

		public event PlayFabRequestEvent<GetPlayerTagsRequest> OnGetPlayerTagsRequestEvent;

		public event PlayFabResultEvent<GetPlayerTagsResult> OnGetPlayerTagsResultEvent;

		public event PlayFabRequestEvent<GetPlayerTradesRequest> OnGetPlayerTradesRequestEvent;

		public event PlayFabResultEvent<GetPlayerTradesResponse> OnGetPlayerTradesResultEvent;

		public event PlayFabRequestEvent<GetPlayFabIDsFromFacebookIDsRequest> OnGetPlayFabIDsFromFacebookIDsRequestEvent;

		public event PlayFabResultEvent<GetPlayFabIDsFromFacebookIDsResult> OnGetPlayFabIDsFromFacebookIDsResultEvent;

		public event PlayFabRequestEvent<GetPlayFabIDsFromFacebookInstantGamesIdsRequest> OnGetPlayFabIDsFromFacebookInstantGamesIdsRequestEvent;

		public event PlayFabResultEvent<GetPlayFabIDsFromFacebookInstantGamesIdsResult> OnGetPlayFabIDsFromFacebookInstantGamesIdsResultEvent;

		public event PlayFabRequestEvent<GetPlayFabIDsFromGameCenterIDsRequest> OnGetPlayFabIDsFromGameCenterIDsRequestEvent;

		public event PlayFabResultEvent<GetPlayFabIDsFromGameCenterIDsResult> OnGetPlayFabIDsFromGameCenterIDsResultEvent;

		public event PlayFabRequestEvent<GetPlayFabIDsFromGenericIDsRequest> OnGetPlayFabIDsFromGenericIDsRequestEvent;

		public event PlayFabResultEvent<GetPlayFabIDsFromGenericIDsResult> OnGetPlayFabIDsFromGenericIDsResultEvent;

		public event PlayFabRequestEvent<GetPlayFabIDsFromGoogleIDsRequest> OnGetPlayFabIDsFromGoogleIDsRequestEvent;

		public event PlayFabResultEvent<GetPlayFabIDsFromGoogleIDsResult> OnGetPlayFabIDsFromGoogleIDsResultEvent;

		public event PlayFabRequestEvent<GetPlayFabIDsFromKongregateIDsRequest> OnGetPlayFabIDsFromKongregateIDsRequestEvent;

		public event PlayFabResultEvent<GetPlayFabIDsFromKongregateIDsResult> OnGetPlayFabIDsFromKongregateIDsResultEvent;

		public event PlayFabRequestEvent<GetPlayFabIDsFromNintendoSwitchDeviceIdsRequest> OnGetPlayFabIDsFromNintendoSwitchDeviceIdsRequestEvent;

		public event PlayFabResultEvent<GetPlayFabIDsFromNintendoSwitchDeviceIdsResult> OnGetPlayFabIDsFromNintendoSwitchDeviceIdsResultEvent;

		public event PlayFabRequestEvent<GetPlayFabIDsFromPSNAccountIDsRequest> OnGetPlayFabIDsFromPSNAccountIDsRequestEvent;

		public event PlayFabResultEvent<GetPlayFabIDsFromPSNAccountIDsResult> OnGetPlayFabIDsFromPSNAccountIDsResultEvent;

		public event PlayFabRequestEvent<GetPlayFabIDsFromSteamIDsRequest> OnGetPlayFabIDsFromSteamIDsRequestEvent;

		public event PlayFabResultEvent<GetPlayFabIDsFromSteamIDsResult> OnGetPlayFabIDsFromSteamIDsResultEvent;

		public event PlayFabRequestEvent<GetPlayFabIDsFromTwitchIDsRequest> OnGetPlayFabIDsFromTwitchIDsRequestEvent;

		public event PlayFabResultEvent<GetPlayFabIDsFromTwitchIDsResult> OnGetPlayFabIDsFromTwitchIDsResultEvent;

		public event PlayFabRequestEvent<GetPlayFabIDsFromXboxLiveIDsRequest> OnGetPlayFabIDsFromXboxLiveIDsRequestEvent;

		public event PlayFabResultEvent<GetPlayFabIDsFromXboxLiveIDsResult> OnGetPlayFabIDsFromXboxLiveIDsResultEvent;

		public event PlayFabRequestEvent<GetPublisherDataRequest> OnGetPublisherDataRequestEvent;

		public event PlayFabResultEvent<GetPublisherDataResult> OnGetPublisherDataResultEvent;

		public event PlayFabRequestEvent<GetPurchaseRequest> OnGetPurchaseRequestEvent;

		public event PlayFabResultEvent<GetPurchaseResult> OnGetPurchaseResultEvent;

		public event PlayFabRequestEvent<GetSharedGroupDataRequest> OnGetSharedGroupDataRequestEvent;

		public event PlayFabResultEvent<GetSharedGroupDataResult> OnGetSharedGroupDataResultEvent;

		public event PlayFabRequestEvent<GetStoreItemsRequest> OnGetStoreItemsRequestEvent;

		public event PlayFabResultEvent<GetStoreItemsResult> OnGetStoreItemsResultEvent;

		public event PlayFabRequestEvent<GetTimeRequest> OnGetTimeRequestEvent;

		public event PlayFabResultEvent<GetTimeResult> OnGetTimeResultEvent;

		public event PlayFabRequestEvent<GetTitleDataRequest> OnGetTitleDataRequestEvent;

		public event PlayFabResultEvent<GetTitleDataResult> OnGetTitleDataResultEvent;

		public event PlayFabRequestEvent<GetTitleNewsRequest> OnGetTitleNewsRequestEvent;

		public event PlayFabResultEvent<GetTitleNewsResult> OnGetTitleNewsResultEvent;

		public event PlayFabRequestEvent<GetTitlePublicKeyRequest> OnGetTitlePublicKeyRequestEvent;

		public event PlayFabResultEvent<GetTitlePublicKeyResult> OnGetTitlePublicKeyResultEvent;

		public event PlayFabRequestEvent<GetTradeStatusRequest> OnGetTradeStatusRequestEvent;

		public event PlayFabResultEvent<GetTradeStatusResponse> OnGetTradeStatusResultEvent;

		public event PlayFabRequestEvent<GetUserDataRequest> OnGetUserDataRequestEvent;

		public event PlayFabResultEvent<GetUserDataResult> OnGetUserDataResultEvent;

		public event PlayFabRequestEvent<GetUserInventoryRequest> OnGetUserInventoryRequestEvent;

		public event PlayFabResultEvent<GetUserInventoryResult> OnGetUserInventoryResultEvent;

		public event PlayFabRequestEvent<GetUserDataRequest> OnGetUserPublisherDataRequestEvent;

		public event PlayFabResultEvent<GetUserDataResult> OnGetUserPublisherDataResultEvent;

		public event PlayFabRequestEvent<GetUserDataRequest> OnGetUserPublisherReadOnlyDataRequestEvent;

		public event PlayFabResultEvent<GetUserDataResult> OnGetUserPublisherReadOnlyDataResultEvent;

		public event PlayFabRequestEvent<GetUserDataRequest> OnGetUserReadOnlyDataRequestEvent;

		public event PlayFabResultEvent<GetUserDataResult> OnGetUserReadOnlyDataResultEvent;

		public event PlayFabRequestEvent<GetWindowsHelloChallengeRequest> OnGetWindowsHelloChallengeRequestEvent;

		public event PlayFabResultEvent<GetWindowsHelloChallengeResponse> OnGetWindowsHelloChallengeResultEvent;

		public event PlayFabRequestEvent<GrantCharacterToUserRequest> OnGrantCharacterToUserRequestEvent;

		public event PlayFabResultEvent<GrantCharacterToUserResult> OnGrantCharacterToUserResultEvent;

		public event PlayFabRequestEvent<LinkAndroidDeviceIDRequest> OnLinkAndroidDeviceIDRequestEvent;

		public event PlayFabResultEvent<LinkAndroidDeviceIDResult> OnLinkAndroidDeviceIDResultEvent;

		public event PlayFabRequestEvent<LinkAppleRequest> OnLinkAppleRequestEvent;

		public event PlayFabResultEvent<PlayFab.ClientModels.EmptyResult> OnLinkAppleResultEvent;

		public event PlayFabRequestEvent<LinkCustomIDRequest> OnLinkCustomIDRequestEvent;

		public event PlayFabResultEvent<LinkCustomIDResult> OnLinkCustomIDResultEvent;

		public event PlayFabRequestEvent<LinkFacebookAccountRequest> OnLinkFacebookAccountRequestEvent;

		public event PlayFabResultEvent<LinkFacebookAccountResult> OnLinkFacebookAccountResultEvent;

		public event PlayFabRequestEvent<LinkFacebookInstantGamesIdRequest> OnLinkFacebookInstantGamesIdRequestEvent;

		public event PlayFabResultEvent<LinkFacebookInstantGamesIdResult> OnLinkFacebookInstantGamesIdResultEvent;

		public event PlayFabRequestEvent<LinkGameCenterAccountRequest> OnLinkGameCenterAccountRequestEvent;

		public event PlayFabResultEvent<LinkGameCenterAccountResult> OnLinkGameCenterAccountResultEvent;

		public event PlayFabRequestEvent<LinkGoogleAccountRequest> OnLinkGoogleAccountRequestEvent;

		public event PlayFabResultEvent<LinkGoogleAccountResult> OnLinkGoogleAccountResultEvent;

		public event PlayFabRequestEvent<LinkIOSDeviceIDRequest> OnLinkIOSDeviceIDRequestEvent;

		public event PlayFabResultEvent<LinkIOSDeviceIDResult> OnLinkIOSDeviceIDResultEvent;

		public event PlayFabRequestEvent<LinkKongregateAccountRequest> OnLinkKongregateRequestEvent;

		public event PlayFabResultEvent<LinkKongregateAccountResult> OnLinkKongregateResultEvent;

		public event PlayFabRequestEvent<LinkNintendoServiceAccountRequest> OnLinkNintendoServiceAccountRequestEvent;

		public event PlayFabResultEvent<PlayFab.ClientModels.EmptyResult> OnLinkNintendoServiceAccountResultEvent;

		public event PlayFabRequestEvent<LinkNintendoSwitchDeviceIdRequest> OnLinkNintendoSwitchDeviceIdRequestEvent;

		public event PlayFabResultEvent<LinkNintendoSwitchDeviceIdResult> OnLinkNintendoSwitchDeviceIdResultEvent;

		public event PlayFabRequestEvent<LinkOpenIdConnectRequest> OnLinkOpenIdConnectRequestEvent;

		public event PlayFabResultEvent<PlayFab.ClientModels.EmptyResult> OnLinkOpenIdConnectResultEvent;

		public event PlayFabRequestEvent<LinkPSNAccountRequest> OnLinkPSNAccountRequestEvent;

		public event PlayFabResultEvent<LinkPSNAccountResult> OnLinkPSNAccountResultEvent;

		public event PlayFabRequestEvent<LinkSteamAccountRequest> OnLinkSteamAccountRequestEvent;

		public event PlayFabResultEvent<LinkSteamAccountResult> OnLinkSteamAccountResultEvent;

		public event PlayFabRequestEvent<LinkTwitchAccountRequest> OnLinkTwitchRequestEvent;

		public event PlayFabResultEvent<LinkTwitchAccountResult> OnLinkTwitchResultEvent;

		public event PlayFabRequestEvent<LinkWindowsHelloAccountRequest> OnLinkWindowsHelloRequestEvent;

		public event PlayFabResultEvent<LinkWindowsHelloAccountResponse> OnLinkWindowsHelloResultEvent;

		public event PlayFabRequestEvent<LinkXboxAccountRequest> OnLinkXboxAccountRequestEvent;

		public event PlayFabResultEvent<LinkXboxAccountResult> OnLinkXboxAccountResultEvent;

		public event PlayFabRequestEvent<LoginWithAndroidDeviceIDRequest> OnLoginWithAndroidDeviceIDRequestEvent;

		public event PlayFabRequestEvent<LoginWithAppleRequest> OnLoginWithAppleRequestEvent;

		public event PlayFabRequestEvent<LoginWithCustomIDRequest> OnLoginWithCustomIDRequestEvent;

		public event PlayFabRequestEvent<LoginWithEmailAddressRequest> OnLoginWithEmailAddressRequestEvent;

		public event PlayFabRequestEvent<LoginWithFacebookRequest> OnLoginWithFacebookRequestEvent;

		public event PlayFabRequestEvent<LoginWithFacebookInstantGamesIdRequest> OnLoginWithFacebookInstantGamesIdRequestEvent;

		public event PlayFabRequestEvent<LoginWithGameCenterRequest> OnLoginWithGameCenterRequestEvent;

		public event PlayFabRequestEvent<LoginWithGoogleAccountRequest> OnLoginWithGoogleAccountRequestEvent;

		public event PlayFabRequestEvent<LoginWithIOSDeviceIDRequest> OnLoginWithIOSDeviceIDRequestEvent;

		public event PlayFabRequestEvent<LoginWithKongregateRequest> OnLoginWithKongregateRequestEvent;

		public event PlayFabRequestEvent<LoginWithNintendoServiceAccountRequest> OnLoginWithNintendoServiceAccountRequestEvent;

		public event PlayFabRequestEvent<LoginWithNintendoSwitchDeviceIdRequest> OnLoginWithNintendoSwitchDeviceIdRequestEvent;

		public event PlayFabRequestEvent<LoginWithOpenIdConnectRequest> OnLoginWithOpenIdConnectRequestEvent;

		public event PlayFabRequestEvent<LoginWithPlayFabRequest> OnLoginWithPlayFabRequestEvent;

		public event PlayFabRequestEvent<LoginWithPSNRequest> OnLoginWithPSNRequestEvent;

		public event PlayFabRequestEvent<LoginWithSteamRequest> OnLoginWithSteamRequestEvent;

		public event PlayFabRequestEvent<LoginWithTwitchRequest> OnLoginWithTwitchRequestEvent;

		public event PlayFabRequestEvent<LoginWithWindowsHelloRequest> OnLoginWithWindowsHelloRequestEvent;

		public event PlayFabRequestEvent<LoginWithXboxRequest> OnLoginWithXboxRequestEvent;

		public event PlayFabRequestEvent<MatchmakeRequest> OnMatchmakeRequestEvent;

		public event PlayFabResultEvent<MatchmakeResult> OnMatchmakeResultEvent;

		public event PlayFabRequestEvent<OpenTradeRequest> OnOpenTradeRequestEvent;

		public event PlayFabResultEvent<OpenTradeResponse> OnOpenTradeResultEvent;

		public event PlayFabRequestEvent<PayForPurchaseRequest> OnPayForPurchaseRequestEvent;

		public event PlayFabResultEvent<PayForPurchaseResult> OnPayForPurchaseResultEvent;

		public event PlayFabRequestEvent<PurchaseItemRequest> OnPurchaseItemRequestEvent;

		public event PlayFabResultEvent<PurchaseItemResult> OnPurchaseItemResultEvent;

		public event PlayFabRequestEvent<RedeemCouponRequest> OnRedeemCouponRequestEvent;

		public event PlayFabResultEvent<RedeemCouponResult> OnRedeemCouponResultEvent;

		public event PlayFabRequestEvent<RefreshPSNAuthTokenRequest> OnRefreshPSNAuthTokenRequestEvent;

		public event PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse> OnRefreshPSNAuthTokenResultEvent;

		public event PlayFabRequestEvent<RegisterForIOSPushNotificationRequest> OnRegisterForIOSPushNotificationRequestEvent;

		public event PlayFabResultEvent<RegisterForIOSPushNotificationResult> OnRegisterForIOSPushNotificationResultEvent;

		public event PlayFabRequestEvent<RegisterPlayFabUserRequest> OnRegisterPlayFabUserRequestEvent;

		public event PlayFabResultEvent<RegisterPlayFabUserResult> OnRegisterPlayFabUserResultEvent;

		public event PlayFabRequestEvent<RegisterWithWindowsHelloRequest> OnRegisterWithWindowsHelloRequestEvent;

		public event PlayFabRequestEvent<RemoveContactEmailRequest> OnRemoveContactEmailRequestEvent;

		public event PlayFabResultEvent<RemoveContactEmailResult> OnRemoveContactEmailResultEvent;

		public event PlayFabRequestEvent<RemoveFriendRequest> OnRemoveFriendRequestEvent;

		public event PlayFabResultEvent<RemoveFriendResult> OnRemoveFriendResultEvent;

		public event PlayFabRequestEvent<RemoveGenericIDRequest> OnRemoveGenericIDRequestEvent;

		public event PlayFabResultEvent<RemoveGenericIDResult> OnRemoveGenericIDResultEvent;

		public event PlayFabRequestEvent<RemoveSharedGroupMembersRequest> OnRemoveSharedGroupMembersRequestEvent;

		public event PlayFabResultEvent<RemoveSharedGroupMembersResult> OnRemoveSharedGroupMembersResultEvent;

		public event PlayFabRequestEvent<ReportAdActivityRequest> OnReportAdActivityRequestEvent;

		public event PlayFabResultEvent<ReportAdActivityResult> OnReportAdActivityResultEvent;

		public event PlayFabRequestEvent<DeviceInfoRequest> OnReportDeviceInfoRequestEvent;

		public event PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse> OnReportDeviceInfoResultEvent;

		public event PlayFabRequestEvent<ReportPlayerClientRequest> OnReportPlayerRequestEvent;

		public event PlayFabResultEvent<ReportPlayerClientResult> OnReportPlayerResultEvent;

		public event PlayFabRequestEvent<RestoreIOSPurchasesRequest> OnRestoreIOSPurchasesRequestEvent;

		public event PlayFabResultEvent<RestoreIOSPurchasesResult> OnRestoreIOSPurchasesResultEvent;

		public event PlayFabRequestEvent<RewardAdActivityRequest> OnRewardAdActivityRequestEvent;

		public event PlayFabResultEvent<RewardAdActivityResult> OnRewardAdActivityResultEvent;

		public event PlayFabRequestEvent<SendAccountRecoveryEmailRequest> OnSendAccountRecoveryEmailRequestEvent;

		public event PlayFabResultEvent<SendAccountRecoveryEmailResult> OnSendAccountRecoveryEmailResultEvent;

		public event PlayFabRequestEvent<SetFriendTagsRequest> OnSetFriendTagsRequestEvent;

		public event PlayFabResultEvent<SetFriendTagsResult> OnSetFriendTagsResultEvent;

		public event PlayFabRequestEvent<SetPlayerSecretRequest> OnSetPlayerSecretRequestEvent;

		public event PlayFabResultEvent<SetPlayerSecretResult> OnSetPlayerSecretResultEvent;

		public event PlayFabRequestEvent<StartGameRequest> OnStartGameRequestEvent;

		public event PlayFabResultEvent<StartGameResult> OnStartGameResultEvent;

		public event PlayFabRequestEvent<StartPurchaseRequest> OnStartPurchaseRequestEvent;

		public event PlayFabResultEvent<StartPurchaseResult> OnStartPurchaseResultEvent;

		public event PlayFabRequestEvent<SubtractUserVirtualCurrencyRequest> OnSubtractUserVirtualCurrencyRequestEvent;

		public event PlayFabResultEvent<ModifyUserVirtualCurrencyResult> OnSubtractUserVirtualCurrencyResultEvent;

		public event PlayFabRequestEvent<UnlinkAndroidDeviceIDRequest> OnUnlinkAndroidDeviceIDRequestEvent;

		public event PlayFabResultEvent<UnlinkAndroidDeviceIDResult> OnUnlinkAndroidDeviceIDResultEvent;

		public event PlayFabRequestEvent<UnlinkAppleRequest> OnUnlinkAppleRequestEvent;

		public event PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse> OnUnlinkAppleResultEvent;

		public event PlayFabRequestEvent<UnlinkCustomIDRequest> OnUnlinkCustomIDRequestEvent;

		public event PlayFabResultEvent<UnlinkCustomIDResult> OnUnlinkCustomIDResultEvent;

		public event PlayFabRequestEvent<UnlinkFacebookAccountRequest> OnUnlinkFacebookAccountRequestEvent;

		public event PlayFabResultEvent<UnlinkFacebookAccountResult> OnUnlinkFacebookAccountResultEvent;

		public event PlayFabRequestEvent<UnlinkFacebookInstantGamesIdRequest> OnUnlinkFacebookInstantGamesIdRequestEvent;

		public event PlayFabResultEvent<UnlinkFacebookInstantGamesIdResult> OnUnlinkFacebookInstantGamesIdResultEvent;

		public event PlayFabRequestEvent<UnlinkGameCenterAccountRequest> OnUnlinkGameCenterAccountRequestEvent;

		public event PlayFabResultEvent<UnlinkGameCenterAccountResult> OnUnlinkGameCenterAccountResultEvent;

		public event PlayFabRequestEvent<UnlinkGoogleAccountRequest> OnUnlinkGoogleAccountRequestEvent;

		public event PlayFabResultEvent<UnlinkGoogleAccountResult> OnUnlinkGoogleAccountResultEvent;

		public event PlayFabRequestEvent<UnlinkIOSDeviceIDRequest> OnUnlinkIOSDeviceIDRequestEvent;

		public event PlayFabResultEvent<UnlinkIOSDeviceIDResult> OnUnlinkIOSDeviceIDResultEvent;

		public event PlayFabRequestEvent<UnlinkKongregateAccountRequest> OnUnlinkKongregateRequestEvent;

		public event PlayFabResultEvent<UnlinkKongregateAccountResult> OnUnlinkKongregateResultEvent;

		public event PlayFabRequestEvent<UnlinkNintendoServiceAccountRequest> OnUnlinkNintendoServiceAccountRequestEvent;

		public event PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse> OnUnlinkNintendoServiceAccountResultEvent;

		public event PlayFabRequestEvent<UnlinkNintendoSwitchDeviceIdRequest> OnUnlinkNintendoSwitchDeviceIdRequestEvent;

		public event PlayFabResultEvent<UnlinkNintendoSwitchDeviceIdResult> OnUnlinkNintendoSwitchDeviceIdResultEvent;

		public event PlayFabRequestEvent<UnlinkOpenIdConnectRequest> OnUnlinkOpenIdConnectRequestEvent;

		public event PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse> OnUnlinkOpenIdConnectResultEvent;

		public event PlayFabRequestEvent<UnlinkPSNAccountRequest> OnUnlinkPSNAccountRequestEvent;

		public event PlayFabResultEvent<UnlinkPSNAccountResult> OnUnlinkPSNAccountResultEvent;

		public event PlayFabRequestEvent<UnlinkSteamAccountRequest> OnUnlinkSteamAccountRequestEvent;

		public event PlayFabResultEvent<UnlinkSteamAccountResult> OnUnlinkSteamAccountResultEvent;

		public event PlayFabRequestEvent<UnlinkTwitchAccountRequest> OnUnlinkTwitchRequestEvent;

		public event PlayFabResultEvent<UnlinkTwitchAccountResult> OnUnlinkTwitchResultEvent;

		public event PlayFabRequestEvent<UnlinkWindowsHelloAccountRequest> OnUnlinkWindowsHelloRequestEvent;

		public event PlayFabResultEvent<UnlinkWindowsHelloAccountResponse> OnUnlinkWindowsHelloResultEvent;

		public event PlayFabRequestEvent<UnlinkXboxAccountRequest> OnUnlinkXboxAccountRequestEvent;

		public event PlayFabResultEvent<UnlinkXboxAccountResult> OnUnlinkXboxAccountResultEvent;

		public event PlayFabRequestEvent<UnlockContainerInstanceRequest> OnUnlockContainerInstanceRequestEvent;

		public event PlayFabResultEvent<UnlockContainerItemResult> OnUnlockContainerInstanceResultEvent;

		public event PlayFabRequestEvent<UnlockContainerItemRequest> OnUnlockContainerItemRequestEvent;

		public event PlayFabResultEvent<UnlockContainerItemResult> OnUnlockContainerItemResultEvent;

		public event PlayFabRequestEvent<UpdateAvatarUrlRequest> OnUpdateAvatarUrlRequestEvent;

		public event PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse> OnUpdateAvatarUrlResultEvent;

		public event PlayFabRequestEvent<UpdateCharacterDataRequest> OnUpdateCharacterDataRequestEvent;

		public event PlayFabResultEvent<UpdateCharacterDataResult> OnUpdateCharacterDataResultEvent;

		public event PlayFabRequestEvent<UpdateCharacterStatisticsRequest> OnUpdateCharacterStatisticsRequestEvent;

		public event PlayFabResultEvent<UpdateCharacterStatisticsResult> OnUpdateCharacterStatisticsResultEvent;

		public event PlayFabRequestEvent<UpdatePlayerStatisticsRequest> OnUpdatePlayerStatisticsRequestEvent;

		public event PlayFabResultEvent<UpdatePlayerStatisticsResult> OnUpdatePlayerStatisticsResultEvent;

		public event PlayFabRequestEvent<UpdateSharedGroupDataRequest> OnUpdateSharedGroupDataRequestEvent;

		public event PlayFabResultEvent<UpdateSharedGroupDataResult> OnUpdateSharedGroupDataResultEvent;

		public event PlayFabRequestEvent<UpdateUserDataRequest> OnUpdateUserDataRequestEvent;

		public event PlayFabResultEvent<UpdateUserDataResult> OnUpdateUserDataResultEvent;

		public event PlayFabRequestEvent<UpdateUserDataRequest> OnUpdateUserPublisherDataRequestEvent;

		public event PlayFabResultEvent<UpdateUserDataResult> OnUpdateUserPublisherDataResultEvent;

		public event PlayFabRequestEvent<UpdateUserTitleDisplayNameRequest> OnUpdateUserTitleDisplayNameRequestEvent;

		public event PlayFabResultEvent<UpdateUserTitleDisplayNameResult> OnUpdateUserTitleDisplayNameResultEvent;

		public event PlayFabRequestEvent<ValidateAmazonReceiptRequest> OnValidateAmazonIAPReceiptRequestEvent;

		public event PlayFabResultEvent<ValidateAmazonReceiptResult> OnValidateAmazonIAPReceiptResultEvent;

		public event PlayFabRequestEvent<ValidateGooglePlayPurchaseRequest> OnValidateGooglePlayPurchaseRequestEvent;

		public event PlayFabResultEvent<ValidateGooglePlayPurchaseResult> OnValidateGooglePlayPurchaseResultEvent;

		public event PlayFabRequestEvent<ValidateIOSReceiptRequest> OnValidateIOSReceiptRequestEvent;

		public event PlayFabResultEvent<ValidateIOSReceiptResult> OnValidateIOSReceiptResultEvent;

		public event PlayFabRequestEvent<ValidateWindowsReceiptRequest> OnValidateWindowsStoreReceiptRequestEvent;

		public event PlayFabResultEvent<ValidateWindowsReceiptResult> OnValidateWindowsStoreReceiptResultEvent;

		public event PlayFabRequestEvent<WriteClientCharacterEventRequest> OnWriteCharacterEventRequestEvent;

		public event PlayFabResultEvent<WriteEventResponse> OnWriteCharacterEventResultEvent;

		public event PlayFabRequestEvent<WriteClientPlayerEventRequest> OnWritePlayerEventRequestEvent;

		public event PlayFabResultEvent<WriteEventResponse> OnWritePlayerEventResultEvent;

		public event PlayFabRequestEvent<WriteTitleEventRequest> OnWriteTitleEventRequestEvent;

		public event PlayFabResultEvent<WriteEventResponse> OnWriteTitleEventResultEvent;

		public event PlayFabRequestEvent<ExecuteEntityCloudScriptRequest> OnCloudScriptExecuteEntityCloudScriptRequestEvent;

		public event PlayFabResultEvent<PlayFab.CloudScriptModels.ExecuteCloudScriptResult> OnCloudScriptExecuteEntityCloudScriptResultEvent;

		public event PlayFabRequestEvent<ExecuteFunctionRequest> OnCloudScriptExecuteFunctionRequestEvent;

		public event PlayFabResultEvent<ExecuteFunctionResult> OnCloudScriptExecuteFunctionResultEvent;

		public event PlayFabRequestEvent<ListFunctionsRequest> OnCloudScriptListFunctionsRequestEvent;

		public event PlayFabResultEvent<ListFunctionsResult> OnCloudScriptListFunctionsResultEvent;

		public event PlayFabRequestEvent<ListFunctionsRequest> OnCloudScriptListHttpFunctionsRequestEvent;

		public event PlayFabResultEvent<ListHttpFunctionsResult> OnCloudScriptListHttpFunctionsResultEvent;

		public event PlayFabRequestEvent<ListFunctionsRequest> OnCloudScriptListQueuedFunctionsRequestEvent;

		public event PlayFabResultEvent<ListQueuedFunctionsResult> OnCloudScriptListQueuedFunctionsResultEvent;

		public event PlayFabRequestEvent<PostFunctionResultForEntityTriggeredActionRequest> OnCloudScriptPostFunctionResultForEntityTriggeredActionRequestEvent;

		public event PlayFabResultEvent<PlayFab.CloudScriptModels.EmptyResult> OnCloudScriptPostFunctionResultForEntityTriggeredActionResultEvent;

		public event PlayFabRequestEvent<PostFunctionResultForFunctionExecutionRequest> OnCloudScriptPostFunctionResultForFunctionExecutionRequestEvent;

		public event PlayFabResultEvent<PlayFab.CloudScriptModels.EmptyResult> OnCloudScriptPostFunctionResultForFunctionExecutionResultEvent;

		public event PlayFabRequestEvent<PostFunctionResultForPlayerTriggeredActionRequest> OnCloudScriptPostFunctionResultForPlayerTriggeredActionRequestEvent;

		public event PlayFabResultEvent<PlayFab.CloudScriptModels.EmptyResult> OnCloudScriptPostFunctionResultForPlayerTriggeredActionResultEvent;

		public event PlayFabRequestEvent<PostFunctionResultForScheduledTaskRequest> OnCloudScriptPostFunctionResultForScheduledTaskRequestEvent;

		public event PlayFabResultEvent<PlayFab.CloudScriptModels.EmptyResult> OnCloudScriptPostFunctionResultForScheduledTaskResultEvent;

		public event PlayFabRequestEvent<RegisterHttpFunctionRequest> OnCloudScriptRegisterHttpFunctionRequestEvent;

		public event PlayFabResultEvent<PlayFab.CloudScriptModels.EmptyResult> OnCloudScriptRegisterHttpFunctionResultEvent;

		public event PlayFabRequestEvent<RegisterQueuedFunctionRequest> OnCloudScriptRegisterQueuedFunctionRequestEvent;

		public event PlayFabResultEvent<PlayFab.CloudScriptModels.EmptyResult> OnCloudScriptRegisterQueuedFunctionResultEvent;

		public event PlayFabRequestEvent<UnregisterFunctionRequest> OnCloudScriptUnregisterFunctionRequestEvent;

		public event PlayFabResultEvent<PlayFab.CloudScriptModels.EmptyResult> OnCloudScriptUnregisterFunctionResultEvent;

		public event PlayFabRequestEvent<AbortFileUploadsRequest> OnDataAbortFileUploadsRequestEvent;

		public event PlayFabResultEvent<AbortFileUploadsResponse> OnDataAbortFileUploadsResultEvent;

		public event PlayFabRequestEvent<DeleteFilesRequest> OnDataDeleteFilesRequestEvent;

		public event PlayFabResultEvent<DeleteFilesResponse> OnDataDeleteFilesResultEvent;

		public event PlayFabRequestEvent<FinalizeFileUploadsRequest> OnDataFinalizeFileUploadsRequestEvent;

		public event PlayFabResultEvent<FinalizeFileUploadsResponse> OnDataFinalizeFileUploadsResultEvent;

		public event PlayFabRequestEvent<GetFilesRequest> OnDataGetFilesRequestEvent;

		public event PlayFabResultEvent<GetFilesResponse> OnDataGetFilesResultEvent;

		public event PlayFabRequestEvent<GetObjectsRequest> OnDataGetObjectsRequestEvent;

		public event PlayFabResultEvent<GetObjectsResponse> OnDataGetObjectsResultEvent;

		public event PlayFabRequestEvent<InitiateFileUploadsRequest> OnDataInitiateFileUploadsRequestEvent;

		public event PlayFabResultEvent<InitiateFileUploadsResponse> OnDataInitiateFileUploadsResultEvent;

		public event PlayFabRequestEvent<SetObjectsRequest> OnDataSetObjectsRequestEvent;

		public event PlayFabResultEvent<SetObjectsResponse> OnDataSetObjectsResultEvent;

		public event PlayFabRequestEvent<WriteEventsRequest> OnEventsWriteEventsRequestEvent;

		public event PlayFabResultEvent<WriteEventsResponse> OnEventsWriteEventsResultEvent;

		public event PlayFabRequestEvent<WriteEventsRequest> OnEventsWriteTelemetryEventsRequestEvent;

		public event PlayFabResultEvent<WriteEventsResponse> OnEventsWriteTelemetryEventsResultEvent;

		public event PlayFabRequestEvent<CreateExclusionGroupRequest> OnExperimentationCreateExclusionGroupRequestEvent;

		public event PlayFabResultEvent<CreateExclusionGroupResult> OnExperimentationCreateExclusionGroupResultEvent;

		public event PlayFabRequestEvent<CreateExperimentRequest> OnExperimentationCreateExperimentRequestEvent;

		public event PlayFabResultEvent<CreateExperimentResult> OnExperimentationCreateExperimentResultEvent;

		public event PlayFabRequestEvent<DeleteExclusionGroupRequest> OnExperimentationDeleteExclusionGroupRequestEvent;

		public event PlayFabResultEvent<PlayFab.ExperimentationModels.EmptyResponse> OnExperimentationDeleteExclusionGroupResultEvent;

		public event PlayFabRequestEvent<DeleteExperimentRequest> OnExperimentationDeleteExperimentRequestEvent;

		public event PlayFabResultEvent<PlayFab.ExperimentationModels.EmptyResponse> OnExperimentationDeleteExperimentResultEvent;

		public event PlayFabRequestEvent<GetExclusionGroupsRequest> OnExperimentationGetExclusionGroupsRequestEvent;

		public event PlayFabResultEvent<GetExclusionGroupsResult> OnExperimentationGetExclusionGroupsResultEvent;

		public event PlayFabRequestEvent<GetExclusionGroupTrafficRequest> OnExperimentationGetExclusionGroupTrafficRequestEvent;

		public event PlayFabResultEvent<GetExclusionGroupTrafficResult> OnExperimentationGetExclusionGroupTrafficResultEvent;

		public event PlayFabRequestEvent<GetExperimentsRequest> OnExperimentationGetExperimentsRequestEvent;

		public event PlayFabResultEvent<GetExperimentsResult> OnExperimentationGetExperimentsResultEvent;

		public event PlayFabRequestEvent<GetLatestScorecardRequest> OnExperimentationGetLatestScorecardRequestEvent;

		public event PlayFabResultEvent<GetLatestScorecardResult> OnExperimentationGetLatestScorecardResultEvent;

		public event PlayFabRequestEvent<GetTreatmentAssignmentRequest> OnExperimentationGetTreatmentAssignmentRequestEvent;

		public event PlayFabResultEvent<GetTreatmentAssignmentResult> OnExperimentationGetTreatmentAssignmentResultEvent;

		public event PlayFabRequestEvent<StartExperimentRequest> OnExperimentationStartExperimentRequestEvent;

		public event PlayFabResultEvent<PlayFab.ExperimentationModels.EmptyResponse> OnExperimentationStartExperimentResultEvent;

		public event PlayFabRequestEvent<StopExperimentRequest> OnExperimentationStopExperimentRequestEvent;

		public event PlayFabResultEvent<PlayFab.ExperimentationModels.EmptyResponse> OnExperimentationStopExperimentResultEvent;

		public event PlayFabRequestEvent<UpdateExclusionGroupRequest> OnExperimentationUpdateExclusionGroupRequestEvent;

		public event PlayFabResultEvent<PlayFab.ExperimentationModels.EmptyResponse> OnExperimentationUpdateExclusionGroupResultEvent;

		public event PlayFabRequestEvent<UpdateExperimentRequest> OnExperimentationUpdateExperimentRequestEvent;

		public event PlayFabResultEvent<PlayFab.ExperimentationModels.EmptyResponse> OnExperimentationUpdateExperimentResultEvent;

		public event PlayFabRequestEvent<AcceptGroupApplicationRequest> OnGroupsAcceptGroupApplicationRequestEvent;

		public event PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse> OnGroupsAcceptGroupApplicationResultEvent;

		public event PlayFabRequestEvent<AcceptGroupInvitationRequest> OnGroupsAcceptGroupInvitationRequestEvent;

		public event PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse> OnGroupsAcceptGroupInvitationResultEvent;

		public event PlayFabRequestEvent<AddMembersRequest> OnGroupsAddMembersRequestEvent;

		public event PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse> OnGroupsAddMembersResultEvent;

		public event PlayFabRequestEvent<ApplyToGroupRequest> OnGroupsApplyToGroupRequestEvent;

		public event PlayFabResultEvent<ApplyToGroupResponse> OnGroupsApplyToGroupResultEvent;

		public event PlayFabRequestEvent<BlockEntityRequest> OnGroupsBlockEntityRequestEvent;

		public event PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse> OnGroupsBlockEntityResultEvent;

		public event PlayFabRequestEvent<ChangeMemberRoleRequest> OnGroupsChangeMemberRoleRequestEvent;

		public event PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse> OnGroupsChangeMemberRoleResultEvent;

		public event PlayFabRequestEvent<CreateGroupRequest> OnGroupsCreateGroupRequestEvent;

		public event PlayFabResultEvent<CreateGroupResponse> OnGroupsCreateGroupResultEvent;

		public event PlayFabRequestEvent<CreateGroupRoleRequest> OnGroupsCreateRoleRequestEvent;

		public event PlayFabResultEvent<CreateGroupRoleResponse> OnGroupsCreateRoleResultEvent;

		public event PlayFabRequestEvent<DeleteGroupRequest> OnGroupsDeleteGroupRequestEvent;

		public event PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse> OnGroupsDeleteGroupResultEvent;

		public event PlayFabRequestEvent<DeleteRoleRequest> OnGroupsDeleteRoleRequestEvent;

		public event PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse> OnGroupsDeleteRoleResultEvent;

		public event PlayFabRequestEvent<GetGroupRequest> OnGroupsGetGroupRequestEvent;

		public event PlayFabResultEvent<GetGroupResponse> OnGroupsGetGroupResultEvent;

		public event PlayFabRequestEvent<InviteToGroupRequest> OnGroupsInviteToGroupRequestEvent;

		public event PlayFabResultEvent<InviteToGroupResponse> OnGroupsInviteToGroupResultEvent;

		public event PlayFabRequestEvent<IsMemberRequest> OnGroupsIsMemberRequestEvent;

		public event PlayFabResultEvent<IsMemberResponse> OnGroupsIsMemberResultEvent;

		public event PlayFabRequestEvent<ListGroupApplicationsRequest> OnGroupsListGroupApplicationsRequestEvent;

		public event PlayFabResultEvent<ListGroupApplicationsResponse> OnGroupsListGroupApplicationsResultEvent;

		public event PlayFabRequestEvent<ListGroupBlocksRequest> OnGroupsListGroupBlocksRequestEvent;

		public event PlayFabResultEvent<ListGroupBlocksResponse> OnGroupsListGroupBlocksResultEvent;

		public event PlayFabRequestEvent<ListGroupInvitationsRequest> OnGroupsListGroupInvitationsRequestEvent;

		public event PlayFabResultEvent<ListGroupInvitationsResponse> OnGroupsListGroupInvitationsResultEvent;

		public event PlayFabRequestEvent<ListGroupMembersRequest> OnGroupsListGroupMembersRequestEvent;

		public event PlayFabResultEvent<ListGroupMembersResponse> OnGroupsListGroupMembersResultEvent;

		public event PlayFabRequestEvent<ListMembershipRequest> OnGroupsListMembershipRequestEvent;

		public event PlayFabResultEvent<ListMembershipResponse> OnGroupsListMembershipResultEvent;

		public event PlayFabRequestEvent<ListMembershipOpportunitiesRequest> OnGroupsListMembershipOpportunitiesRequestEvent;

		public event PlayFabResultEvent<ListMembershipOpportunitiesResponse> OnGroupsListMembershipOpportunitiesResultEvent;

		public event PlayFabRequestEvent<RemoveGroupApplicationRequest> OnGroupsRemoveGroupApplicationRequestEvent;

		public event PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse> OnGroupsRemoveGroupApplicationResultEvent;

		public event PlayFabRequestEvent<RemoveGroupInvitationRequest> OnGroupsRemoveGroupInvitationRequestEvent;

		public event PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse> OnGroupsRemoveGroupInvitationResultEvent;

		public event PlayFabRequestEvent<RemoveMembersRequest> OnGroupsRemoveMembersRequestEvent;

		public event PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse> OnGroupsRemoveMembersResultEvent;

		public event PlayFabRequestEvent<UnblockEntityRequest> OnGroupsUnblockEntityRequestEvent;

		public event PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse> OnGroupsUnblockEntityResultEvent;

		public event PlayFabRequestEvent<UpdateGroupRequest> OnGroupsUpdateGroupRequestEvent;

		public event PlayFabResultEvent<UpdateGroupResponse> OnGroupsUpdateGroupResultEvent;

		public event PlayFabRequestEvent<UpdateGroupRoleRequest> OnGroupsUpdateRoleRequestEvent;

		public event PlayFabResultEvent<UpdateGroupRoleResponse> OnGroupsUpdateRoleResultEvent;

		public event PlayFabRequestEvent<InsightsEmptyRequest> OnInsightsGetDetailsRequestEvent;

		public event PlayFabResultEvent<InsightsGetDetailsResponse> OnInsightsGetDetailsResultEvent;

		public event PlayFabRequestEvent<InsightsEmptyRequest> OnInsightsGetLimitsRequestEvent;

		public event PlayFabResultEvent<InsightsGetLimitsResponse> OnInsightsGetLimitsResultEvent;

		public event PlayFabRequestEvent<InsightsGetOperationStatusRequest> OnInsightsGetOperationStatusRequestEvent;

		public event PlayFabResultEvent<InsightsGetOperationStatusResponse> OnInsightsGetOperationStatusResultEvent;

		public event PlayFabRequestEvent<InsightsGetPendingOperationsRequest> OnInsightsGetPendingOperationsRequestEvent;

		public event PlayFabResultEvent<InsightsGetPendingOperationsResponse> OnInsightsGetPendingOperationsResultEvent;

		public event PlayFabRequestEvent<InsightsSetPerformanceRequest> OnInsightsSetPerformanceRequestEvent;

		public event PlayFabResultEvent<InsightsOperationResponse> OnInsightsSetPerformanceResultEvent;

		public event PlayFabRequestEvent<InsightsSetStorageRetentionRequest> OnInsightsSetStorageRetentionRequestEvent;

		public event PlayFabResultEvent<InsightsOperationResponse> OnInsightsSetStorageRetentionResultEvent;

		public event PlayFabRequestEvent<GetLanguageListRequest> OnLocalizationGetLanguageListRequestEvent;

		public event PlayFabResultEvent<GetLanguageListResponse> OnLocalizationGetLanguageListResultEvent;

		public event PlayFabRequestEvent<CancelAllMatchmakingTicketsForPlayerRequest> OnMultiplayerCancelAllMatchmakingTicketsForPlayerRequestEvent;

		public event PlayFabResultEvent<CancelAllMatchmakingTicketsForPlayerResult> OnMultiplayerCancelAllMatchmakingTicketsForPlayerResultEvent;

		public event PlayFabRequestEvent<CancelAllServerBackfillTicketsForPlayerRequest> OnMultiplayerCancelAllServerBackfillTicketsForPlayerRequestEvent;

		public event PlayFabResultEvent<CancelAllServerBackfillTicketsForPlayerResult> OnMultiplayerCancelAllServerBackfillTicketsForPlayerResultEvent;

		public event PlayFabRequestEvent<CancelMatchmakingTicketRequest> OnMultiplayerCancelMatchmakingTicketRequestEvent;

		public event PlayFabResultEvent<CancelMatchmakingTicketResult> OnMultiplayerCancelMatchmakingTicketResultEvent;

		public event PlayFabRequestEvent<CancelServerBackfillTicketRequest> OnMultiplayerCancelServerBackfillTicketRequestEvent;

		public event PlayFabResultEvent<CancelServerBackfillTicketResult> OnMultiplayerCancelServerBackfillTicketResultEvent;

		public event PlayFabRequestEvent<CreateBuildAliasRequest> OnMultiplayerCreateBuildAliasRequestEvent;

		public event PlayFabResultEvent<BuildAliasDetailsResponse> OnMultiplayerCreateBuildAliasResultEvent;

		public event PlayFabRequestEvent<CreateBuildWithCustomContainerRequest> OnMultiplayerCreateBuildWithCustomContainerRequestEvent;

		public event PlayFabResultEvent<CreateBuildWithCustomContainerResponse> OnMultiplayerCreateBuildWithCustomContainerResultEvent;

		public event PlayFabRequestEvent<CreateBuildWithManagedContainerRequest> OnMultiplayerCreateBuildWithManagedContainerRequestEvent;

		public event PlayFabResultEvent<CreateBuildWithManagedContainerResponse> OnMultiplayerCreateBuildWithManagedContainerResultEvent;

		public event PlayFabRequestEvent<CreateBuildWithProcessBasedServerRequest> OnMultiplayerCreateBuildWithProcessBasedServerRequestEvent;

		public event PlayFabResultEvent<CreateBuildWithProcessBasedServerResponse> OnMultiplayerCreateBuildWithProcessBasedServerResultEvent;

		public event PlayFabRequestEvent<CreateMatchmakingTicketRequest> OnMultiplayerCreateMatchmakingTicketRequestEvent;

		public event PlayFabResultEvent<CreateMatchmakingTicketResult> OnMultiplayerCreateMatchmakingTicketResultEvent;

		public event PlayFabRequestEvent<CreateRemoteUserRequest> OnMultiplayerCreateRemoteUserRequestEvent;

		public event PlayFabResultEvent<CreateRemoteUserResponse> OnMultiplayerCreateRemoteUserResultEvent;

		public event PlayFabRequestEvent<CreateServerBackfillTicketRequest> OnMultiplayerCreateServerBackfillTicketRequestEvent;

		public event PlayFabResultEvent<CreateServerBackfillTicketResult> OnMultiplayerCreateServerBackfillTicketResultEvent;

		public event PlayFabRequestEvent<CreateServerMatchmakingTicketRequest> OnMultiplayerCreateServerMatchmakingTicketRequestEvent;

		public event PlayFabResultEvent<CreateMatchmakingTicketResult> OnMultiplayerCreateServerMatchmakingTicketResultEvent;

		public event PlayFabRequestEvent<DeleteAssetRequest> OnMultiplayerDeleteAssetRequestEvent;

		public event PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse> OnMultiplayerDeleteAssetResultEvent;

		public event PlayFabRequestEvent<DeleteBuildRequest> OnMultiplayerDeleteBuildRequestEvent;

		public event PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse> OnMultiplayerDeleteBuildResultEvent;

		public event PlayFabRequestEvent<DeleteBuildAliasRequest> OnMultiplayerDeleteBuildAliasRequestEvent;

		public event PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse> OnMultiplayerDeleteBuildAliasResultEvent;

		public event PlayFabRequestEvent<DeleteBuildRegionRequest> OnMultiplayerDeleteBuildRegionRequestEvent;

		public event PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse> OnMultiplayerDeleteBuildRegionResultEvent;

		public event PlayFabRequestEvent<DeleteCertificateRequest> OnMultiplayerDeleteCertificateRequestEvent;

		public event PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse> OnMultiplayerDeleteCertificateResultEvent;

		public event PlayFabRequestEvent<DeleteContainerImageRequest> OnMultiplayerDeleteContainerImageRepositoryRequestEvent;

		public event PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse> OnMultiplayerDeleteContainerImageRepositoryResultEvent;

		public event PlayFabRequestEvent<DeleteRemoteUserRequest> OnMultiplayerDeleteRemoteUserRequestEvent;

		public event PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse> OnMultiplayerDeleteRemoteUserResultEvent;

		public event PlayFabRequestEvent<EnableMultiplayerServersForTitleRequest> OnMultiplayerEnableMultiplayerServersForTitleRequestEvent;

		public event PlayFabResultEvent<EnableMultiplayerServersForTitleResponse> OnMultiplayerEnableMultiplayerServersForTitleResultEvent;

		public event PlayFabRequestEvent<GetAssetUploadUrlRequest> OnMultiplayerGetAssetUploadUrlRequestEvent;

		public event PlayFabResultEvent<GetAssetUploadUrlResponse> OnMultiplayerGetAssetUploadUrlResultEvent;

		public event PlayFabRequestEvent<GetBuildRequest> OnMultiplayerGetBuildRequestEvent;

		public event PlayFabResultEvent<GetBuildResponse> OnMultiplayerGetBuildResultEvent;

		public event PlayFabRequestEvent<GetBuildAliasRequest> OnMultiplayerGetBuildAliasRequestEvent;

		public event PlayFabResultEvent<BuildAliasDetailsResponse> OnMultiplayerGetBuildAliasResultEvent;

		public event PlayFabRequestEvent<GetContainerRegistryCredentialsRequest> OnMultiplayerGetContainerRegistryCredentialsRequestEvent;

		public event PlayFabResultEvent<GetContainerRegistryCredentialsResponse> OnMultiplayerGetContainerRegistryCredentialsResultEvent;

		public event PlayFabRequestEvent<GetMatchRequest> OnMultiplayerGetMatchRequestEvent;

		public event PlayFabResultEvent<GetMatchResult> OnMultiplayerGetMatchResultEvent;

		public event PlayFabRequestEvent<GetMatchmakingQueueRequest> OnMultiplayerGetMatchmakingQueueRequestEvent;

		public event PlayFabResultEvent<GetMatchmakingQueueResult> OnMultiplayerGetMatchmakingQueueResultEvent;

		public event PlayFabRequestEvent<GetMatchmakingTicketRequest> OnMultiplayerGetMatchmakingTicketRequestEvent;

		public event PlayFabResultEvent<GetMatchmakingTicketResult> OnMultiplayerGetMatchmakingTicketResultEvent;

		public event PlayFabRequestEvent<GetMultiplayerServerDetailsRequest> OnMultiplayerGetMultiplayerServerDetailsRequestEvent;

		public event PlayFabResultEvent<GetMultiplayerServerDetailsResponse> OnMultiplayerGetMultiplayerServerDetailsResultEvent;

		public event PlayFabRequestEvent<GetMultiplayerServerLogsRequest> OnMultiplayerGetMultiplayerServerLogsRequestEvent;

		public event PlayFabResultEvent<GetMultiplayerServerLogsResponse> OnMultiplayerGetMultiplayerServerLogsResultEvent;

		public event PlayFabRequestEvent<GetMultiplayerSessionLogsBySessionIdRequest> OnMultiplayerGetMultiplayerSessionLogsBySessionIdRequestEvent;

		public event PlayFabResultEvent<GetMultiplayerServerLogsResponse> OnMultiplayerGetMultiplayerSessionLogsBySessionIdResultEvent;

		public event PlayFabRequestEvent<GetQueueStatisticsRequest> OnMultiplayerGetQueueStatisticsRequestEvent;

		public event PlayFabResultEvent<GetQueueStatisticsResult> OnMultiplayerGetQueueStatisticsResultEvent;

		public event PlayFabRequestEvent<GetRemoteLoginEndpointRequest> OnMultiplayerGetRemoteLoginEndpointRequestEvent;

		public event PlayFabResultEvent<GetRemoteLoginEndpointResponse> OnMultiplayerGetRemoteLoginEndpointResultEvent;

		public event PlayFabRequestEvent<GetServerBackfillTicketRequest> OnMultiplayerGetServerBackfillTicketRequestEvent;

		public event PlayFabResultEvent<GetServerBackfillTicketResult> OnMultiplayerGetServerBackfillTicketResultEvent;

		public event PlayFabRequestEvent<GetTitleEnabledForMultiplayerServersStatusRequest> OnMultiplayerGetTitleEnabledForMultiplayerServersStatusRequestEvent;

		public event PlayFabResultEvent<GetTitleEnabledForMultiplayerServersStatusResponse> OnMultiplayerGetTitleEnabledForMultiplayerServersStatusResultEvent;

		public event PlayFabRequestEvent<GetTitleMultiplayerServersQuotasRequest> OnMultiplayerGetTitleMultiplayerServersQuotasRequestEvent;

		public event PlayFabResultEvent<GetTitleMultiplayerServersQuotasResponse> OnMultiplayerGetTitleMultiplayerServersQuotasResultEvent;

		public event PlayFabRequestEvent<JoinMatchmakingTicketRequest> OnMultiplayerJoinMatchmakingTicketRequestEvent;

		public event PlayFabResultEvent<JoinMatchmakingTicketResult> OnMultiplayerJoinMatchmakingTicketResultEvent;

		public event PlayFabRequestEvent<ListMultiplayerServersRequest> OnMultiplayerListArchivedMultiplayerServersRequestEvent;

		public event PlayFabResultEvent<ListMultiplayerServersResponse> OnMultiplayerListArchivedMultiplayerServersResultEvent;

		public event PlayFabRequestEvent<ListAssetSummariesRequest> OnMultiplayerListAssetSummariesRequestEvent;

		public event PlayFabResultEvent<ListAssetSummariesResponse> OnMultiplayerListAssetSummariesResultEvent;

		public event PlayFabRequestEvent<MultiplayerEmptyRequest> OnMultiplayerListBuildAliasesRequestEvent;

		public event PlayFabResultEvent<ListBuildAliasesForTitleResponse> OnMultiplayerListBuildAliasesResultEvent;

		public event PlayFabRequestEvent<ListBuildSummariesRequest> OnMultiplayerListBuildSummariesRequestEvent;

		public event PlayFabResultEvent<ListBuildSummariesResponse> OnMultiplayerListBuildSummariesResultEvent;

		public event PlayFabRequestEvent<ListCertificateSummariesRequest> OnMultiplayerListCertificateSummariesRequestEvent;

		public event PlayFabResultEvent<ListCertificateSummariesResponse> OnMultiplayerListCertificateSummariesResultEvent;

		public event PlayFabRequestEvent<ListContainerImagesRequest> OnMultiplayerListContainerImagesRequestEvent;

		public event PlayFabResultEvent<ListContainerImagesResponse> OnMultiplayerListContainerImagesResultEvent;

		public event PlayFabRequestEvent<ListContainerImageTagsRequest> OnMultiplayerListContainerImageTagsRequestEvent;

		public event PlayFabResultEvent<ListContainerImageTagsResponse> OnMultiplayerListContainerImageTagsResultEvent;

		public event PlayFabRequestEvent<ListMatchmakingQueuesRequest> OnMultiplayerListMatchmakingQueuesRequestEvent;

		public event PlayFabResultEvent<ListMatchmakingQueuesResult> OnMultiplayerListMatchmakingQueuesResultEvent;

		public event PlayFabRequestEvent<ListMatchmakingTicketsForPlayerRequest> OnMultiplayerListMatchmakingTicketsForPlayerRequestEvent;

		public event PlayFabResultEvent<ListMatchmakingTicketsForPlayerResult> OnMultiplayerListMatchmakingTicketsForPlayerResultEvent;

		public event PlayFabRequestEvent<ListMultiplayerServersRequest> OnMultiplayerListMultiplayerServersRequestEvent;

		public event PlayFabResultEvent<ListMultiplayerServersResponse> OnMultiplayerListMultiplayerServersResultEvent;

		public event PlayFabRequestEvent<ListPartyQosServersRequest> OnMultiplayerListPartyQosServersRequestEvent;

		public event PlayFabResultEvent<ListPartyQosServersResponse> OnMultiplayerListPartyQosServersResultEvent;

		public event PlayFabRequestEvent<ListQosServersForTitleRequest> OnMultiplayerListQosServersForTitleRequestEvent;

		public event PlayFabResultEvent<ListQosServersForTitleResponse> OnMultiplayerListQosServersForTitleResultEvent;

		public event PlayFabRequestEvent<ListServerBackfillTicketsForPlayerRequest> OnMultiplayerListServerBackfillTicketsForPlayerRequestEvent;

		public event PlayFabResultEvent<ListServerBackfillTicketsForPlayerResult> OnMultiplayerListServerBackfillTicketsForPlayerResultEvent;

		public event PlayFabRequestEvent<ListVirtualMachineSummariesRequest> OnMultiplayerListVirtualMachineSummariesRequestEvent;

		public event PlayFabResultEvent<ListVirtualMachineSummariesResponse> OnMultiplayerListVirtualMachineSummariesResultEvent;

		public event PlayFabRequestEvent<RemoveMatchmakingQueueRequest> OnMultiplayerRemoveMatchmakingQueueRequestEvent;

		public event PlayFabResultEvent<RemoveMatchmakingQueueResult> OnMultiplayerRemoveMatchmakingQueueResultEvent;

		public event PlayFabRequestEvent<RequestMultiplayerServerRequest> OnMultiplayerRequestMultiplayerServerRequestEvent;

		public event PlayFabResultEvent<RequestMultiplayerServerResponse> OnMultiplayerRequestMultiplayerServerResultEvent;

		public event PlayFabRequestEvent<RolloverContainerRegistryCredentialsRequest> OnMultiplayerRolloverContainerRegistryCredentialsRequestEvent;

		public event PlayFabResultEvent<RolloverContainerRegistryCredentialsResponse> OnMultiplayerRolloverContainerRegistryCredentialsResultEvent;

		public event PlayFabRequestEvent<SetMatchmakingQueueRequest> OnMultiplayerSetMatchmakingQueueRequestEvent;

		public event PlayFabResultEvent<SetMatchmakingQueueResult> OnMultiplayerSetMatchmakingQueueResultEvent;

		public event PlayFabRequestEvent<ShutdownMultiplayerServerRequest> OnMultiplayerShutdownMultiplayerServerRequestEvent;

		public event PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse> OnMultiplayerShutdownMultiplayerServerResultEvent;

		public event PlayFabRequestEvent<UntagContainerImageRequest> OnMultiplayerUntagContainerImageRequestEvent;

		public event PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse> OnMultiplayerUntagContainerImageResultEvent;

		public event PlayFabRequestEvent<UpdateBuildAliasRequest> OnMultiplayerUpdateBuildAliasRequestEvent;

		public event PlayFabResultEvent<BuildAliasDetailsResponse> OnMultiplayerUpdateBuildAliasResultEvent;

		public event PlayFabRequestEvent<UpdateBuildRegionRequest> OnMultiplayerUpdateBuildRegionRequestEvent;

		public event PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse> OnMultiplayerUpdateBuildRegionResultEvent;

		public event PlayFabRequestEvent<UpdateBuildRegionsRequest> OnMultiplayerUpdateBuildRegionsRequestEvent;

		public event PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse> OnMultiplayerUpdateBuildRegionsResultEvent;

		public event PlayFabRequestEvent<UploadCertificateRequest> OnMultiplayerUploadCertificateRequestEvent;

		public event PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse> OnMultiplayerUploadCertificateResultEvent;

		public event PlayFabRequestEvent<GetGlobalPolicyRequest> OnProfilesGetGlobalPolicyRequestEvent;

		public event PlayFabResultEvent<GetGlobalPolicyResponse> OnProfilesGetGlobalPolicyResultEvent;

		public event PlayFabRequestEvent<GetEntityProfileRequest> OnProfilesGetProfileRequestEvent;

		public event PlayFabResultEvent<GetEntityProfileResponse> OnProfilesGetProfileResultEvent;

		public event PlayFabRequestEvent<GetEntityProfilesRequest> OnProfilesGetProfilesRequestEvent;

		public event PlayFabResultEvent<GetEntityProfilesResponse> OnProfilesGetProfilesResultEvent;

		public event PlayFabRequestEvent<GetTitlePlayersFromMasterPlayerAccountIdsRequest> OnProfilesGetTitlePlayersFromMasterPlayerAccountIdsRequestEvent;

		public event PlayFabResultEvent<GetTitlePlayersFromMasterPlayerAccountIdsResponse> OnProfilesGetTitlePlayersFromMasterPlayerAccountIdsResultEvent;

		public event PlayFabRequestEvent<SetGlobalPolicyRequest> OnProfilesSetGlobalPolicyRequestEvent;

		public event PlayFabResultEvent<SetGlobalPolicyResponse> OnProfilesSetGlobalPolicyResultEvent;

		public event PlayFabRequestEvent<SetProfileLanguageRequest> OnProfilesSetProfileLanguageRequestEvent;

		public event PlayFabResultEvent<SetProfileLanguageResponse> OnProfilesSetProfileLanguageResultEvent;

		public event PlayFabRequestEvent<SetEntityProfilePolicyRequest> OnProfilesSetProfilePolicyRequestEvent;

		public event PlayFabResultEvent<SetEntityProfilePolicyResponse> OnProfilesSetProfilePolicyResultEvent;

		public event PlayFabErrorEvent OnGlobalErrorEvent;

		private PlayFabEvents()
		{
		}

		public static PlayFabEvents Init()
		{
			if (_instance == null)
			{
				_instance = new PlayFabEvents();
			}
			PlayFabHttp.ApiProcessingEventHandler += _instance.OnProcessingEvent;
			PlayFabHttp.ApiProcessingErrorEventHandler += _instance.OnProcessingErrorEvent;
			return _instance;
		}

		public void UnregisterInstance(object instance)
		{
			if (this.OnLoginResultEvent != null)
			{
				Delegate[] invocationList = this.OnLoginResultEvent.GetInvocationList();
				foreach (Delegate obj in invocationList)
				{
					if (object.ReferenceEquals(obj.Target, instance))
					{
						this.OnLoginResultEvent = (PlayFabResultEvent<LoginResult>)Delegate.Remove(this.OnLoginResultEvent, (PlayFabResultEvent<LoginResult>)obj);
					}
				}
			}
			if (this.OnAcceptTradeRequestEvent != null)
			{
				Delegate[] invocationList2 = this.OnAcceptTradeRequestEvent.GetInvocationList();
				foreach (Delegate obj2 in invocationList2)
				{
					if (object.ReferenceEquals(obj2.Target, instance))
					{
						this.OnAcceptTradeRequestEvent = (PlayFabRequestEvent<AcceptTradeRequest>)Delegate.Remove(this.OnAcceptTradeRequestEvent, (PlayFabRequestEvent<AcceptTradeRequest>)obj2);
					}
				}
			}
			if (this.OnAcceptTradeResultEvent != null)
			{
				Delegate[] invocationList3 = this.OnAcceptTradeResultEvent.GetInvocationList();
				foreach (Delegate obj3 in invocationList3)
				{
					if (object.ReferenceEquals(obj3.Target, instance))
					{
						this.OnAcceptTradeResultEvent = (PlayFabResultEvent<AcceptTradeResponse>)Delegate.Remove(this.OnAcceptTradeResultEvent, (PlayFabResultEvent<AcceptTradeResponse>)obj3);
					}
				}
			}
			if (this.OnAddFriendRequestEvent != null)
			{
				Delegate[] invocationList4 = this.OnAddFriendRequestEvent.GetInvocationList();
				foreach (Delegate obj4 in invocationList4)
				{
					if (object.ReferenceEquals(obj4.Target, instance))
					{
						this.OnAddFriendRequestEvent = (PlayFabRequestEvent<AddFriendRequest>)Delegate.Remove(this.OnAddFriendRequestEvent, (PlayFabRequestEvent<AddFriendRequest>)obj4);
					}
				}
			}
			if (this.OnAddFriendResultEvent != null)
			{
				Delegate[] invocationList5 = this.OnAddFriendResultEvent.GetInvocationList();
				foreach (Delegate obj5 in invocationList5)
				{
					if (object.ReferenceEquals(obj5.Target, instance))
					{
						this.OnAddFriendResultEvent = (PlayFabResultEvent<AddFriendResult>)Delegate.Remove(this.OnAddFriendResultEvent, (PlayFabResultEvent<AddFriendResult>)obj5);
					}
				}
			}
			if (this.OnAddGenericIDRequestEvent != null)
			{
				Delegate[] invocationList6 = this.OnAddGenericIDRequestEvent.GetInvocationList();
				foreach (Delegate obj6 in invocationList6)
				{
					if (object.ReferenceEquals(obj6.Target, instance))
					{
						this.OnAddGenericIDRequestEvent = (PlayFabRequestEvent<AddGenericIDRequest>)Delegate.Remove(this.OnAddGenericIDRequestEvent, (PlayFabRequestEvent<AddGenericIDRequest>)obj6);
					}
				}
			}
			if (this.OnAddGenericIDResultEvent != null)
			{
				Delegate[] invocationList7 = this.OnAddGenericIDResultEvent.GetInvocationList();
				foreach (Delegate obj7 in invocationList7)
				{
					if (object.ReferenceEquals(obj7.Target, instance))
					{
						this.OnAddGenericIDResultEvent = (PlayFabResultEvent<AddGenericIDResult>)Delegate.Remove(this.OnAddGenericIDResultEvent, (PlayFabResultEvent<AddGenericIDResult>)obj7);
					}
				}
			}
			if (this.OnAddOrUpdateContactEmailRequestEvent != null)
			{
				Delegate[] invocationList8 = this.OnAddOrUpdateContactEmailRequestEvent.GetInvocationList();
				foreach (Delegate obj8 in invocationList8)
				{
					if (object.ReferenceEquals(obj8.Target, instance))
					{
						this.OnAddOrUpdateContactEmailRequestEvent = (PlayFabRequestEvent<AddOrUpdateContactEmailRequest>)Delegate.Remove(this.OnAddOrUpdateContactEmailRequestEvent, (PlayFabRequestEvent<AddOrUpdateContactEmailRequest>)obj8);
					}
				}
			}
			if (this.OnAddOrUpdateContactEmailResultEvent != null)
			{
				Delegate[] invocationList9 = this.OnAddOrUpdateContactEmailResultEvent.GetInvocationList();
				foreach (Delegate obj9 in invocationList9)
				{
					if (object.ReferenceEquals(obj9.Target, instance))
					{
						this.OnAddOrUpdateContactEmailResultEvent = (PlayFabResultEvent<AddOrUpdateContactEmailResult>)Delegate.Remove(this.OnAddOrUpdateContactEmailResultEvent, (PlayFabResultEvent<AddOrUpdateContactEmailResult>)obj9);
					}
				}
			}
			if (this.OnAddSharedGroupMembersRequestEvent != null)
			{
				Delegate[] invocationList10 = this.OnAddSharedGroupMembersRequestEvent.GetInvocationList();
				foreach (Delegate obj10 in invocationList10)
				{
					if (object.ReferenceEquals(obj10.Target, instance))
					{
						this.OnAddSharedGroupMembersRequestEvent = (PlayFabRequestEvent<AddSharedGroupMembersRequest>)Delegate.Remove(this.OnAddSharedGroupMembersRequestEvent, (PlayFabRequestEvent<AddSharedGroupMembersRequest>)obj10);
					}
				}
			}
			if (this.OnAddSharedGroupMembersResultEvent != null)
			{
				Delegate[] invocationList11 = this.OnAddSharedGroupMembersResultEvent.GetInvocationList();
				foreach (Delegate obj11 in invocationList11)
				{
					if (object.ReferenceEquals(obj11.Target, instance))
					{
						this.OnAddSharedGroupMembersResultEvent = (PlayFabResultEvent<AddSharedGroupMembersResult>)Delegate.Remove(this.OnAddSharedGroupMembersResultEvent, (PlayFabResultEvent<AddSharedGroupMembersResult>)obj11);
					}
				}
			}
			if (this.OnAddUsernamePasswordRequestEvent != null)
			{
				Delegate[] invocationList12 = this.OnAddUsernamePasswordRequestEvent.GetInvocationList();
				foreach (Delegate obj12 in invocationList12)
				{
					if (object.ReferenceEquals(obj12.Target, instance))
					{
						this.OnAddUsernamePasswordRequestEvent = (PlayFabRequestEvent<AddUsernamePasswordRequest>)Delegate.Remove(this.OnAddUsernamePasswordRequestEvent, (PlayFabRequestEvent<AddUsernamePasswordRequest>)obj12);
					}
				}
			}
			if (this.OnAddUsernamePasswordResultEvent != null)
			{
				Delegate[] invocationList13 = this.OnAddUsernamePasswordResultEvent.GetInvocationList();
				foreach (Delegate obj13 in invocationList13)
				{
					if (object.ReferenceEquals(obj13.Target, instance))
					{
						this.OnAddUsernamePasswordResultEvent = (PlayFabResultEvent<AddUsernamePasswordResult>)Delegate.Remove(this.OnAddUsernamePasswordResultEvent, (PlayFabResultEvent<AddUsernamePasswordResult>)obj13);
					}
				}
			}
			if (this.OnAddUserVirtualCurrencyRequestEvent != null)
			{
				Delegate[] invocationList14 = this.OnAddUserVirtualCurrencyRequestEvent.GetInvocationList();
				foreach (Delegate obj14 in invocationList14)
				{
					if (object.ReferenceEquals(obj14.Target, instance))
					{
						this.OnAddUserVirtualCurrencyRequestEvent = (PlayFabRequestEvent<AddUserVirtualCurrencyRequest>)Delegate.Remove(this.OnAddUserVirtualCurrencyRequestEvent, (PlayFabRequestEvent<AddUserVirtualCurrencyRequest>)obj14);
					}
				}
			}
			if (this.OnAddUserVirtualCurrencyResultEvent != null)
			{
				Delegate[] invocationList15 = this.OnAddUserVirtualCurrencyResultEvent.GetInvocationList();
				foreach (Delegate obj15 in invocationList15)
				{
					if (object.ReferenceEquals(obj15.Target, instance))
					{
						this.OnAddUserVirtualCurrencyResultEvent = (PlayFabResultEvent<ModifyUserVirtualCurrencyResult>)Delegate.Remove(this.OnAddUserVirtualCurrencyResultEvent, (PlayFabResultEvent<ModifyUserVirtualCurrencyResult>)obj15);
					}
				}
			}
			if (this.OnAndroidDevicePushNotificationRegistrationRequestEvent != null)
			{
				Delegate[] invocationList16 = this.OnAndroidDevicePushNotificationRegistrationRequestEvent.GetInvocationList();
				foreach (Delegate obj16 in invocationList16)
				{
					if (object.ReferenceEquals(obj16.Target, instance))
					{
						this.OnAndroidDevicePushNotificationRegistrationRequestEvent = (PlayFabRequestEvent<AndroidDevicePushNotificationRegistrationRequest>)Delegate.Remove(this.OnAndroidDevicePushNotificationRegistrationRequestEvent, (PlayFabRequestEvent<AndroidDevicePushNotificationRegistrationRequest>)obj16);
					}
				}
			}
			if (this.OnAndroidDevicePushNotificationRegistrationResultEvent != null)
			{
				Delegate[] invocationList17 = this.OnAndroidDevicePushNotificationRegistrationResultEvent.GetInvocationList();
				foreach (Delegate obj17 in invocationList17)
				{
					if (object.ReferenceEquals(obj17.Target, instance))
					{
						this.OnAndroidDevicePushNotificationRegistrationResultEvent = (PlayFabResultEvent<AndroidDevicePushNotificationRegistrationResult>)Delegate.Remove(this.OnAndroidDevicePushNotificationRegistrationResultEvent, (PlayFabResultEvent<AndroidDevicePushNotificationRegistrationResult>)obj17);
					}
				}
			}
			if (this.OnAttributeInstallRequestEvent != null)
			{
				Delegate[] invocationList18 = this.OnAttributeInstallRequestEvent.GetInvocationList();
				foreach (Delegate obj18 in invocationList18)
				{
					if (object.ReferenceEquals(obj18.Target, instance))
					{
						this.OnAttributeInstallRequestEvent = (PlayFabRequestEvent<AttributeInstallRequest>)Delegate.Remove(this.OnAttributeInstallRequestEvent, (PlayFabRequestEvent<AttributeInstallRequest>)obj18);
					}
				}
			}
			if (this.OnAttributeInstallResultEvent != null)
			{
				Delegate[] invocationList19 = this.OnAttributeInstallResultEvent.GetInvocationList();
				foreach (Delegate obj19 in invocationList19)
				{
					if (object.ReferenceEquals(obj19.Target, instance))
					{
						this.OnAttributeInstallResultEvent = (PlayFabResultEvent<AttributeInstallResult>)Delegate.Remove(this.OnAttributeInstallResultEvent, (PlayFabResultEvent<AttributeInstallResult>)obj19);
					}
				}
			}
			if (this.OnCancelTradeRequestEvent != null)
			{
				Delegate[] invocationList20 = this.OnCancelTradeRequestEvent.GetInvocationList();
				foreach (Delegate obj20 in invocationList20)
				{
					if (object.ReferenceEquals(obj20.Target, instance))
					{
						this.OnCancelTradeRequestEvent = (PlayFabRequestEvent<CancelTradeRequest>)Delegate.Remove(this.OnCancelTradeRequestEvent, (PlayFabRequestEvent<CancelTradeRequest>)obj20);
					}
				}
			}
			if (this.OnCancelTradeResultEvent != null)
			{
				Delegate[] invocationList21 = this.OnCancelTradeResultEvent.GetInvocationList();
				foreach (Delegate obj21 in invocationList21)
				{
					if (object.ReferenceEquals(obj21.Target, instance))
					{
						this.OnCancelTradeResultEvent = (PlayFabResultEvent<CancelTradeResponse>)Delegate.Remove(this.OnCancelTradeResultEvent, (PlayFabResultEvent<CancelTradeResponse>)obj21);
					}
				}
			}
			if (this.OnConfirmPurchaseRequestEvent != null)
			{
				Delegate[] invocationList22 = this.OnConfirmPurchaseRequestEvent.GetInvocationList();
				foreach (Delegate obj22 in invocationList22)
				{
					if (object.ReferenceEquals(obj22.Target, instance))
					{
						this.OnConfirmPurchaseRequestEvent = (PlayFabRequestEvent<ConfirmPurchaseRequest>)Delegate.Remove(this.OnConfirmPurchaseRequestEvent, (PlayFabRequestEvent<ConfirmPurchaseRequest>)obj22);
					}
				}
			}
			if (this.OnConfirmPurchaseResultEvent != null)
			{
				Delegate[] invocationList23 = this.OnConfirmPurchaseResultEvent.GetInvocationList();
				foreach (Delegate obj23 in invocationList23)
				{
					if (object.ReferenceEquals(obj23.Target, instance))
					{
						this.OnConfirmPurchaseResultEvent = (PlayFabResultEvent<ConfirmPurchaseResult>)Delegate.Remove(this.OnConfirmPurchaseResultEvent, (PlayFabResultEvent<ConfirmPurchaseResult>)obj23);
					}
				}
			}
			if (this.OnConsumeItemRequestEvent != null)
			{
				Delegate[] invocationList24 = this.OnConsumeItemRequestEvent.GetInvocationList();
				foreach (Delegate obj24 in invocationList24)
				{
					if (object.ReferenceEquals(obj24.Target, instance))
					{
						this.OnConsumeItemRequestEvent = (PlayFabRequestEvent<ConsumeItemRequest>)Delegate.Remove(this.OnConsumeItemRequestEvent, (PlayFabRequestEvent<ConsumeItemRequest>)obj24);
					}
				}
			}
			if (this.OnConsumeItemResultEvent != null)
			{
				Delegate[] invocationList25 = this.OnConsumeItemResultEvent.GetInvocationList();
				foreach (Delegate obj25 in invocationList25)
				{
					if (object.ReferenceEquals(obj25.Target, instance))
					{
						this.OnConsumeItemResultEvent = (PlayFabResultEvent<ConsumeItemResult>)Delegate.Remove(this.OnConsumeItemResultEvent, (PlayFabResultEvent<ConsumeItemResult>)obj25);
					}
				}
			}
			if (this.OnConsumeMicrosoftStoreEntitlementsRequestEvent != null)
			{
				Delegate[] invocationList26 = this.OnConsumeMicrosoftStoreEntitlementsRequestEvent.GetInvocationList();
				foreach (Delegate obj26 in invocationList26)
				{
					if (object.ReferenceEquals(obj26.Target, instance))
					{
						this.OnConsumeMicrosoftStoreEntitlementsRequestEvent = (PlayFabRequestEvent<ConsumeMicrosoftStoreEntitlementsRequest>)Delegate.Remove(this.OnConsumeMicrosoftStoreEntitlementsRequestEvent, (PlayFabRequestEvent<ConsumeMicrosoftStoreEntitlementsRequest>)obj26);
					}
				}
			}
			if (this.OnConsumeMicrosoftStoreEntitlementsResultEvent != null)
			{
				Delegate[] invocationList27 = this.OnConsumeMicrosoftStoreEntitlementsResultEvent.GetInvocationList();
				foreach (Delegate obj27 in invocationList27)
				{
					if (object.ReferenceEquals(obj27.Target, instance))
					{
						this.OnConsumeMicrosoftStoreEntitlementsResultEvent = (PlayFabResultEvent<ConsumeMicrosoftStoreEntitlementsResponse>)Delegate.Remove(this.OnConsumeMicrosoftStoreEntitlementsResultEvent, (PlayFabResultEvent<ConsumeMicrosoftStoreEntitlementsResponse>)obj27);
					}
				}
			}
			if (this.OnConsumePSNEntitlementsRequestEvent != null)
			{
				Delegate[] invocationList28 = this.OnConsumePSNEntitlementsRequestEvent.GetInvocationList();
				foreach (Delegate obj28 in invocationList28)
				{
					if (object.ReferenceEquals(obj28.Target, instance))
					{
						this.OnConsumePSNEntitlementsRequestEvent = (PlayFabRequestEvent<ConsumePSNEntitlementsRequest>)Delegate.Remove(this.OnConsumePSNEntitlementsRequestEvent, (PlayFabRequestEvent<ConsumePSNEntitlementsRequest>)obj28);
					}
				}
			}
			if (this.OnConsumePSNEntitlementsResultEvent != null)
			{
				Delegate[] invocationList29 = this.OnConsumePSNEntitlementsResultEvent.GetInvocationList();
				foreach (Delegate obj29 in invocationList29)
				{
					if (object.ReferenceEquals(obj29.Target, instance))
					{
						this.OnConsumePSNEntitlementsResultEvent = (PlayFabResultEvent<ConsumePSNEntitlementsResult>)Delegate.Remove(this.OnConsumePSNEntitlementsResultEvent, (PlayFabResultEvent<ConsumePSNEntitlementsResult>)obj29);
					}
				}
			}
			if (this.OnConsumeXboxEntitlementsRequestEvent != null)
			{
				Delegate[] invocationList30 = this.OnConsumeXboxEntitlementsRequestEvent.GetInvocationList();
				foreach (Delegate obj30 in invocationList30)
				{
					if (object.ReferenceEquals(obj30.Target, instance))
					{
						this.OnConsumeXboxEntitlementsRequestEvent = (PlayFabRequestEvent<ConsumeXboxEntitlementsRequest>)Delegate.Remove(this.OnConsumeXboxEntitlementsRequestEvent, (PlayFabRequestEvent<ConsumeXboxEntitlementsRequest>)obj30);
					}
				}
			}
			if (this.OnConsumeXboxEntitlementsResultEvent != null)
			{
				Delegate[] invocationList31 = this.OnConsumeXboxEntitlementsResultEvent.GetInvocationList();
				foreach (Delegate obj31 in invocationList31)
				{
					if (object.ReferenceEquals(obj31.Target, instance))
					{
						this.OnConsumeXboxEntitlementsResultEvent = (PlayFabResultEvent<ConsumeXboxEntitlementsResult>)Delegate.Remove(this.OnConsumeXboxEntitlementsResultEvent, (PlayFabResultEvent<ConsumeXboxEntitlementsResult>)obj31);
					}
				}
			}
			if (this.OnCreateSharedGroupRequestEvent != null)
			{
				Delegate[] invocationList32 = this.OnCreateSharedGroupRequestEvent.GetInvocationList();
				foreach (Delegate obj32 in invocationList32)
				{
					if (object.ReferenceEquals(obj32.Target, instance))
					{
						this.OnCreateSharedGroupRequestEvent = (PlayFabRequestEvent<CreateSharedGroupRequest>)Delegate.Remove(this.OnCreateSharedGroupRequestEvent, (PlayFabRequestEvent<CreateSharedGroupRequest>)obj32);
					}
				}
			}
			if (this.OnCreateSharedGroupResultEvent != null)
			{
				Delegate[] invocationList33 = this.OnCreateSharedGroupResultEvent.GetInvocationList();
				foreach (Delegate obj33 in invocationList33)
				{
					if (object.ReferenceEquals(obj33.Target, instance))
					{
						this.OnCreateSharedGroupResultEvent = (PlayFabResultEvent<CreateSharedGroupResult>)Delegate.Remove(this.OnCreateSharedGroupResultEvent, (PlayFabResultEvent<CreateSharedGroupResult>)obj33);
					}
				}
			}
			if (this.OnExecuteCloudScriptRequestEvent != null)
			{
				Delegate[] invocationList34 = this.OnExecuteCloudScriptRequestEvent.GetInvocationList();
				foreach (Delegate obj34 in invocationList34)
				{
					if (object.ReferenceEquals(obj34.Target, instance))
					{
						this.OnExecuteCloudScriptRequestEvent = (PlayFabRequestEvent<ExecuteCloudScriptRequest>)Delegate.Remove(this.OnExecuteCloudScriptRequestEvent, (PlayFabRequestEvent<ExecuteCloudScriptRequest>)obj34);
					}
				}
			}
			if (this.OnExecuteCloudScriptResultEvent != null)
			{
				Delegate[] invocationList35 = this.OnExecuteCloudScriptResultEvent.GetInvocationList();
				foreach (Delegate obj35 in invocationList35)
				{
					if (object.ReferenceEquals(obj35.Target, instance))
					{
						this.OnExecuteCloudScriptResultEvent = (PlayFabResultEvent<PlayFab.ClientModels.ExecuteCloudScriptResult>)Delegate.Remove(this.OnExecuteCloudScriptResultEvent, (PlayFabResultEvent<PlayFab.ClientModels.ExecuteCloudScriptResult>)obj35);
					}
				}
			}
			if (this.OnGetAccountInfoRequestEvent != null)
			{
				Delegate[] invocationList36 = this.OnGetAccountInfoRequestEvent.GetInvocationList();
				foreach (Delegate obj36 in invocationList36)
				{
					if (object.ReferenceEquals(obj36.Target, instance))
					{
						this.OnGetAccountInfoRequestEvent = (PlayFabRequestEvent<GetAccountInfoRequest>)Delegate.Remove(this.OnGetAccountInfoRequestEvent, (PlayFabRequestEvent<GetAccountInfoRequest>)obj36);
					}
				}
			}
			if (this.OnGetAccountInfoResultEvent != null)
			{
				Delegate[] invocationList37 = this.OnGetAccountInfoResultEvent.GetInvocationList();
				foreach (Delegate obj37 in invocationList37)
				{
					if (object.ReferenceEquals(obj37.Target, instance))
					{
						this.OnGetAccountInfoResultEvent = (PlayFabResultEvent<GetAccountInfoResult>)Delegate.Remove(this.OnGetAccountInfoResultEvent, (PlayFabResultEvent<GetAccountInfoResult>)obj37);
					}
				}
			}
			if (this.OnGetAdPlacementsRequestEvent != null)
			{
				Delegate[] invocationList38 = this.OnGetAdPlacementsRequestEvent.GetInvocationList();
				foreach (Delegate obj38 in invocationList38)
				{
					if (object.ReferenceEquals(obj38.Target, instance))
					{
						this.OnGetAdPlacementsRequestEvent = (PlayFabRequestEvent<GetAdPlacementsRequest>)Delegate.Remove(this.OnGetAdPlacementsRequestEvent, (PlayFabRequestEvent<GetAdPlacementsRequest>)obj38);
					}
				}
			}
			if (this.OnGetAdPlacementsResultEvent != null)
			{
				Delegate[] invocationList39 = this.OnGetAdPlacementsResultEvent.GetInvocationList();
				foreach (Delegate obj39 in invocationList39)
				{
					if (object.ReferenceEquals(obj39.Target, instance))
					{
						this.OnGetAdPlacementsResultEvent = (PlayFabResultEvent<GetAdPlacementsResult>)Delegate.Remove(this.OnGetAdPlacementsResultEvent, (PlayFabResultEvent<GetAdPlacementsResult>)obj39);
					}
				}
			}
			if (this.OnGetAllUsersCharactersRequestEvent != null)
			{
				Delegate[] invocationList40 = this.OnGetAllUsersCharactersRequestEvent.GetInvocationList();
				foreach (Delegate obj40 in invocationList40)
				{
					if (object.ReferenceEquals(obj40.Target, instance))
					{
						this.OnGetAllUsersCharactersRequestEvent = (PlayFabRequestEvent<ListUsersCharactersRequest>)Delegate.Remove(this.OnGetAllUsersCharactersRequestEvent, (PlayFabRequestEvent<ListUsersCharactersRequest>)obj40);
					}
				}
			}
			if (this.OnGetAllUsersCharactersResultEvent != null)
			{
				Delegate[] invocationList41 = this.OnGetAllUsersCharactersResultEvent.GetInvocationList();
				foreach (Delegate obj41 in invocationList41)
				{
					if (object.ReferenceEquals(obj41.Target, instance))
					{
						this.OnGetAllUsersCharactersResultEvent = (PlayFabResultEvent<ListUsersCharactersResult>)Delegate.Remove(this.OnGetAllUsersCharactersResultEvent, (PlayFabResultEvent<ListUsersCharactersResult>)obj41);
					}
				}
			}
			if (this.OnGetCatalogItemsRequestEvent != null)
			{
				Delegate[] invocationList42 = this.OnGetCatalogItemsRequestEvent.GetInvocationList();
				foreach (Delegate obj42 in invocationList42)
				{
					if (object.ReferenceEquals(obj42.Target, instance))
					{
						this.OnGetCatalogItemsRequestEvent = (PlayFabRequestEvent<GetCatalogItemsRequest>)Delegate.Remove(this.OnGetCatalogItemsRequestEvent, (PlayFabRequestEvent<GetCatalogItemsRequest>)obj42);
					}
				}
			}
			if (this.OnGetCatalogItemsResultEvent != null)
			{
				Delegate[] invocationList43 = this.OnGetCatalogItemsResultEvent.GetInvocationList();
				foreach (Delegate obj43 in invocationList43)
				{
					if (object.ReferenceEquals(obj43.Target, instance))
					{
						this.OnGetCatalogItemsResultEvent = (PlayFabResultEvent<GetCatalogItemsResult>)Delegate.Remove(this.OnGetCatalogItemsResultEvent, (PlayFabResultEvent<GetCatalogItemsResult>)obj43);
					}
				}
			}
			if (this.OnGetCharacterDataRequestEvent != null)
			{
				Delegate[] invocationList44 = this.OnGetCharacterDataRequestEvent.GetInvocationList();
				foreach (Delegate obj44 in invocationList44)
				{
					if (object.ReferenceEquals(obj44.Target, instance))
					{
						this.OnGetCharacterDataRequestEvent = (PlayFabRequestEvent<GetCharacterDataRequest>)Delegate.Remove(this.OnGetCharacterDataRequestEvent, (PlayFabRequestEvent<GetCharacterDataRequest>)obj44);
					}
				}
			}
			if (this.OnGetCharacterDataResultEvent != null)
			{
				Delegate[] invocationList45 = this.OnGetCharacterDataResultEvent.GetInvocationList();
				foreach (Delegate obj45 in invocationList45)
				{
					if (object.ReferenceEquals(obj45.Target, instance))
					{
						this.OnGetCharacterDataResultEvent = (PlayFabResultEvent<GetCharacterDataResult>)Delegate.Remove(this.OnGetCharacterDataResultEvent, (PlayFabResultEvent<GetCharacterDataResult>)obj45);
					}
				}
			}
			if (this.OnGetCharacterInventoryRequestEvent != null)
			{
				Delegate[] invocationList46 = this.OnGetCharacterInventoryRequestEvent.GetInvocationList();
				foreach (Delegate obj46 in invocationList46)
				{
					if (object.ReferenceEquals(obj46.Target, instance))
					{
						this.OnGetCharacterInventoryRequestEvent = (PlayFabRequestEvent<GetCharacterInventoryRequest>)Delegate.Remove(this.OnGetCharacterInventoryRequestEvent, (PlayFabRequestEvent<GetCharacterInventoryRequest>)obj46);
					}
				}
			}
			if (this.OnGetCharacterInventoryResultEvent != null)
			{
				Delegate[] invocationList47 = this.OnGetCharacterInventoryResultEvent.GetInvocationList();
				foreach (Delegate obj47 in invocationList47)
				{
					if (object.ReferenceEquals(obj47.Target, instance))
					{
						this.OnGetCharacterInventoryResultEvent = (PlayFabResultEvent<GetCharacterInventoryResult>)Delegate.Remove(this.OnGetCharacterInventoryResultEvent, (PlayFabResultEvent<GetCharacterInventoryResult>)obj47);
					}
				}
			}
			if (this.OnGetCharacterLeaderboardRequestEvent != null)
			{
				Delegate[] invocationList48 = this.OnGetCharacterLeaderboardRequestEvent.GetInvocationList();
				foreach (Delegate obj48 in invocationList48)
				{
					if (object.ReferenceEquals(obj48.Target, instance))
					{
						this.OnGetCharacterLeaderboardRequestEvent = (PlayFabRequestEvent<GetCharacterLeaderboardRequest>)Delegate.Remove(this.OnGetCharacterLeaderboardRequestEvent, (PlayFabRequestEvent<GetCharacterLeaderboardRequest>)obj48);
					}
				}
			}
			if (this.OnGetCharacterLeaderboardResultEvent != null)
			{
				Delegate[] invocationList49 = this.OnGetCharacterLeaderboardResultEvent.GetInvocationList();
				foreach (Delegate obj49 in invocationList49)
				{
					if (object.ReferenceEquals(obj49.Target, instance))
					{
						this.OnGetCharacterLeaderboardResultEvent = (PlayFabResultEvent<GetCharacterLeaderboardResult>)Delegate.Remove(this.OnGetCharacterLeaderboardResultEvent, (PlayFabResultEvent<GetCharacterLeaderboardResult>)obj49);
					}
				}
			}
			if (this.OnGetCharacterReadOnlyDataRequestEvent != null)
			{
				Delegate[] invocationList50 = this.OnGetCharacterReadOnlyDataRequestEvent.GetInvocationList();
				foreach (Delegate obj50 in invocationList50)
				{
					if (object.ReferenceEquals(obj50.Target, instance))
					{
						this.OnGetCharacterReadOnlyDataRequestEvent = (PlayFabRequestEvent<GetCharacterDataRequest>)Delegate.Remove(this.OnGetCharacterReadOnlyDataRequestEvent, (PlayFabRequestEvent<GetCharacterDataRequest>)obj50);
					}
				}
			}
			if (this.OnGetCharacterReadOnlyDataResultEvent != null)
			{
				Delegate[] invocationList51 = this.OnGetCharacterReadOnlyDataResultEvent.GetInvocationList();
				foreach (Delegate obj51 in invocationList51)
				{
					if (object.ReferenceEquals(obj51.Target, instance))
					{
						this.OnGetCharacterReadOnlyDataResultEvent = (PlayFabResultEvent<GetCharacterDataResult>)Delegate.Remove(this.OnGetCharacterReadOnlyDataResultEvent, (PlayFabResultEvent<GetCharacterDataResult>)obj51);
					}
				}
			}
			if (this.OnGetCharacterStatisticsRequestEvent != null)
			{
				Delegate[] invocationList52 = this.OnGetCharacterStatisticsRequestEvent.GetInvocationList();
				foreach (Delegate obj52 in invocationList52)
				{
					if (object.ReferenceEquals(obj52.Target, instance))
					{
						this.OnGetCharacterStatisticsRequestEvent = (PlayFabRequestEvent<GetCharacterStatisticsRequest>)Delegate.Remove(this.OnGetCharacterStatisticsRequestEvent, (PlayFabRequestEvent<GetCharacterStatisticsRequest>)obj52);
					}
				}
			}
			if (this.OnGetCharacterStatisticsResultEvent != null)
			{
				Delegate[] invocationList53 = this.OnGetCharacterStatisticsResultEvent.GetInvocationList();
				foreach (Delegate obj53 in invocationList53)
				{
					if (object.ReferenceEquals(obj53.Target, instance))
					{
						this.OnGetCharacterStatisticsResultEvent = (PlayFabResultEvent<GetCharacterStatisticsResult>)Delegate.Remove(this.OnGetCharacterStatisticsResultEvent, (PlayFabResultEvent<GetCharacterStatisticsResult>)obj53);
					}
				}
			}
			if (this.OnGetContentDownloadUrlRequestEvent != null)
			{
				Delegate[] invocationList54 = this.OnGetContentDownloadUrlRequestEvent.GetInvocationList();
				foreach (Delegate obj54 in invocationList54)
				{
					if (object.ReferenceEquals(obj54.Target, instance))
					{
						this.OnGetContentDownloadUrlRequestEvent = (PlayFabRequestEvent<GetContentDownloadUrlRequest>)Delegate.Remove(this.OnGetContentDownloadUrlRequestEvent, (PlayFabRequestEvent<GetContentDownloadUrlRequest>)obj54);
					}
				}
			}
			if (this.OnGetContentDownloadUrlResultEvent != null)
			{
				Delegate[] invocationList55 = this.OnGetContentDownloadUrlResultEvent.GetInvocationList();
				foreach (Delegate obj55 in invocationList55)
				{
					if (object.ReferenceEquals(obj55.Target, instance))
					{
						this.OnGetContentDownloadUrlResultEvent = (PlayFabResultEvent<GetContentDownloadUrlResult>)Delegate.Remove(this.OnGetContentDownloadUrlResultEvent, (PlayFabResultEvent<GetContentDownloadUrlResult>)obj55);
					}
				}
			}
			if (this.OnGetCurrentGamesRequestEvent != null)
			{
				Delegate[] invocationList56 = this.OnGetCurrentGamesRequestEvent.GetInvocationList();
				foreach (Delegate obj56 in invocationList56)
				{
					if (object.ReferenceEquals(obj56.Target, instance))
					{
						this.OnGetCurrentGamesRequestEvent = (PlayFabRequestEvent<CurrentGamesRequest>)Delegate.Remove(this.OnGetCurrentGamesRequestEvent, (PlayFabRequestEvent<CurrentGamesRequest>)obj56);
					}
				}
			}
			if (this.OnGetCurrentGamesResultEvent != null)
			{
				Delegate[] invocationList57 = this.OnGetCurrentGamesResultEvent.GetInvocationList();
				foreach (Delegate obj57 in invocationList57)
				{
					if (object.ReferenceEquals(obj57.Target, instance))
					{
						this.OnGetCurrentGamesResultEvent = (PlayFabResultEvent<CurrentGamesResult>)Delegate.Remove(this.OnGetCurrentGamesResultEvent, (PlayFabResultEvent<CurrentGamesResult>)obj57);
					}
				}
			}
			if (this.OnGetFriendLeaderboardRequestEvent != null)
			{
				Delegate[] invocationList58 = this.OnGetFriendLeaderboardRequestEvent.GetInvocationList();
				foreach (Delegate obj58 in invocationList58)
				{
					if (object.ReferenceEquals(obj58.Target, instance))
					{
						this.OnGetFriendLeaderboardRequestEvent = (PlayFabRequestEvent<GetFriendLeaderboardRequest>)Delegate.Remove(this.OnGetFriendLeaderboardRequestEvent, (PlayFabRequestEvent<GetFriendLeaderboardRequest>)obj58);
					}
				}
			}
			if (this.OnGetFriendLeaderboardResultEvent != null)
			{
				Delegate[] invocationList59 = this.OnGetFriendLeaderboardResultEvent.GetInvocationList();
				foreach (Delegate obj59 in invocationList59)
				{
					if (object.ReferenceEquals(obj59.Target, instance))
					{
						this.OnGetFriendLeaderboardResultEvent = (PlayFabResultEvent<GetLeaderboardResult>)Delegate.Remove(this.OnGetFriendLeaderboardResultEvent, (PlayFabResultEvent<GetLeaderboardResult>)obj59);
					}
				}
			}
			if (this.OnGetFriendLeaderboardAroundPlayerRequestEvent != null)
			{
				Delegate[] invocationList60 = this.OnGetFriendLeaderboardAroundPlayerRequestEvent.GetInvocationList();
				foreach (Delegate obj60 in invocationList60)
				{
					if (object.ReferenceEquals(obj60.Target, instance))
					{
						this.OnGetFriendLeaderboardAroundPlayerRequestEvent = (PlayFabRequestEvent<GetFriendLeaderboardAroundPlayerRequest>)Delegate.Remove(this.OnGetFriendLeaderboardAroundPlayerRequestEvent, (PlayFabRequestEvent<GetFriendLeaderboardAroundPlayerRequest>)obj60);
					}
				}
			}
			if (this.OnGetFriendLeaderboardAroundPlayerResultEvent != null)
			{
				Delegate[] invocationList61 = this.OnGetFriendLeaderboardAroundPlayerResultEvent.GetInvocationList();
				foreach (Delegate obj61 in invocationList61)
				{
					if (object.ReferenceEquals(obj61.Target, instance))
					{
						this.OnGetFriendLeaderboardAroundPlayerResultEvent = (PlayFabResultEvent<GetFriendLeaderboardAroundPlayerResult>)Delegate.Remove(this.OnGetFriendLeaderboardAroundPlayerResultEvent, (PlayFabResultEvent<GetFriendLeaderboardAroundPlayerResult>)obj61);
					}
				}
			}
			if (this.OnGetFriendsListRequestEvent != null)
			{
				Delegate[] invocationList62 = this.OnGetFriendsListRequestEvent.GetInvocationList();
				foreach (Delegate obj62 in invocationList62)
				{
					if (object.ReferenceEquals(obj62.Target, instance))
					{
						this.OnGetFriendsListRequestEvent = (PlayFabRequestEvent<GetFriendsListRequest>)Delegate.Remove(this.OnGetFriendsListRequestEvent, (PlayFabRequestEvent<GetFriendsListRequest>)obj62);
					}
				}
			}
			if (this.OnGetFriendsListResultEvent != null)
			{
				Delegate[] invocationList63 = this.OnGetFriendsListResultEvent.GetInvocationList();
				foreach (Delegate obj63 in invocationList63)
				{
					if (object.ReferenceEquals(obj63.Target, instance))
					{
						this.OnGetFriendsListResultEvent = (PlayFabResultEvent<GetFriendsListResult>)Delegate.Remove(this.OnGetFriendsListResultEvent, (PlayFabResultEvent<GetFriendsListResult>)obj63);
					}
				}
			}
			if (this.OnGetGameServerRegionsRequestEvent != null)
			{
				Delegate[] invocationList64 = this.OnGetGameServerRegionsRequestEvent.GetInvocationList();
				foreach (Delegate obj64 in invocationList64)
				{
					if (object.ReferenceEquals(obj64.Target, instance))
					{
						this.OnGetGameServerRegionsRequestEvent = (PlayFabRequestEvent<GameServerRegionsRequest>)Delegate.Remove(this.OnGetGameServerRegionsRequestEvent, (PlayFabRequestEvent<GameServerRegionsRequest>)obj64);
					}
				}
			}
			if (this.OnGetGameServerRegionsResultEvent != null)
			{
				Delegate[] invocationList65 = this.OnGetGameServerRegionsResultEvent.GetInvocationList();
				foreach (Delegate obj65 in invocationList65)
				{
					if (object.ReferenceEquals(obj65.Target, instance))
					{
						this.OnGetGameServerRegionsResultEvent = (PlayFabResultEvent<GameServerRegionsResult>)Delegate.Remove(this.OnGetGameServerRegionsResultEvent, (PlayFabResultEvent<GameServerRegionsResult>)obj65);
					}
				}
			}
			if (this.OnGetLeaderboardRequestEvent != null)
			{
				Delegate[] invocationList66 = this.OnGetLeaderboardRequestEvent.GetInvocationList();
				foreach (Delegate obj66 in invocationList66)
				{
					if (object.ReferenceEquals(obj66.Target, instance))
					{
						this.OnGetLeaderboardRequestEvent = (PlayFabRequestEvent<GetLeaderboardRequest>)Delegate.Remove(this.OnGetLeaderboardRequestEvent, (PlayFabRequestEvent<GetLeaderboardRequest>)obj66);
					}
				}
			}
			if (this.OnGetLeaderboardResultEvent != null)
			{
				Delegate[] invocationList67 = this.OnGetLeaderboardResultEvent.GetInvocationList();
				foreach (Delegate obj67 in invocationList67)
				{
					if (object.ReferenceEquals(obj67.Target, instance))
					{
						this.OnGetLeaderboardResultEvent = (PlayFabResultEvent<GetLeaderboardResult>)Delegate.Remove(this.OnGetLeaderboardResultEvent, (PlayFabResultEvent<GetLeaderboardResult>)obj67);
					}
				}
			}
			if (this.OnGetLeaderboardAroundCharacterRequestEvent != null)
			{
				Delegate[] invocationList68 = this.OnGetLeaderboardAroundCharacterRequestEvent.GetInvocationList();
				foreach (Delegate obj68 in invocationList68)
				{
					if (object.ReferenceEquals(obj68.Target, instance))
					{
						this.OnGetLeaderboardAroundCharacterRequestEvent = (PlayFabRequestEvent<GetLeaderboardAroundCharacterRequest>)Delegate.Remove(this.OnGetLeaderboardAroundCharacterRequestEvent, (PlayFabRequestEvent<GetLeaderboardAroundCharacterRequest>)obj68);
					}
				}
			}
			if (this.OnGetLeaderboardAroundCharacterResultEvent != null)
			{
				Delegate[] invocationList69 = this.OnGetLeaderboardAroundCharacterResultEvent.GetInvocationList();
				foreach (Delegate obj69 in invocationList69)
				{
					if (object.ReferenceEquals(obj69.Target, instance))
					{
						this.OnGetLeaderboardAroundCharacterResultEvent = (PlayFabResultEvent<GetLeaderboardAroundCharacterResult>)Delegate.Remove(this.OnGetLeaderboardAroundCharacterResultEvent, (PlayFabResultEvent<GetLeaderboardAroundCharacterResult>)obj69);
					}
				}
			}
			if (this.OnGetLeaderboardAroundPlayerRequestEvent != null)
			{
				Delegate[] invocationList70 = this.OnGetLeaderboardAroundPlayerRequestEvent.GetInvocationList();
				foreach (Delegate obj70 in invocationList70)
				{
					if (object.ReferenceEquals(obj70.Target, instance))
					{
						this.OnGetLeaderboardAroundPlayerRequestEvent = (PlayFabRequestEvent<GetLeaderboardAroundPlayerRequest>)Delegate.Remove(this.OnGetLeaderboardAroundPlayerRequestEvent, (PlayFabRequestEvent<GetLeaderboardAroundPlayerRequest>)obj70);
					}
				}
			}
			if (this.OnGetLeaderboardAroundPlayerResultEvent != null)
			{
				Delegate[] invocationList71 = this.OnGetLeaderboardAroundPlayerResultEvent.GetInvocationList();
				foreach (Delegate obj71 in invocationList71)
				{
					if (object.ReferenceEquals(obj71.Target, instance))
					{
						this.OnGetLeaderboardAroundPlayerResultEvent = (PlayFabResultEvent<GetLeaderboardAroundPlayerResult>)Delegate.Remove(this.OnGetLeaderboardAroundPlayerResultEvent, (PlayFabResultEvent<GetLeaderboardAroundPlayerResult>)obj71);
					}
				}
			}
			if (this.OnGetLeaderboardForUserCharactersRequestEvent != null)
			{
				Delegate[] invocationList72 = this.OnGetLeaderboardForUserCharactersRequestEvent.GetInvocationList();
				foreach (Delegate obj72 in invocationList72)
				{
					if (object.ReferenceEquals(obj72.Target, instance))
					{
						this.OnGetLeaderboardForUserCharactersRequestEvent = (PlayFabRequestEvent<GetLeaderboardForUsersCharactersRequest>)Delegate.Remove(this.OnGetLeaderboardForUserCharactersRequestEvent, (PlayFabRequestEvent<GetLeaderboardForUsersCharactersRequest>)obj72);
					}
				}
			}
			if (this.OnGetLeaderboardForUserCharactersResultEvent != null)
			{
				Delegate[] invocationList73 = this.OnGetLeaderboardForUserCharactersResultEvent.GetInvocationList();
				foreach (Delegate obj73 in invocationList73)
				{
					if (object.ReferenceEquals(obj73.Target, instance))
					{
						this.OnGetLeaderboardForUserCharactersResultEvent = (PlayFabResultEvent<GetLeaderboardForUsersCharactersResult>)Delegate.Remove(this.OnGetLeaderboardForUserCharactersResultEvent, (PlayFabResultEvent<GetLeaderboardForUsersCharactersResult>)obj73);
					}
				}
			}
			if (this.OnGetPaymentTokenRequestEvent != null)
			{
				Delegate[] invocationList74 = this.OnGetPaymentTokenRequestEvent.GetInvocationList();
				foreach (Delegate obj74 in invocationList74)
				{
					if (object.ReferenceEquals(obj74.Target, instance))
					{
						this.OnGetPaymentTokenRequestEvent = (PlayFabRequestEvent<GetPaymentTokenRequest>)Delegate.Remove(this.OnGetPaymentTokenRequestEvent, (PlayFabRequestEvent<GetPaymentTokenRequest>)obj74);
					}
				}
			}
			if (this.OnGetPaymentTokenResultEvent != null)
			{
				Delegate[] invocationList75 = this.OnGetPaymentTokenResultEvent.GetInvocationList();
				foreach (Delegate obj75 in invocationList75)
				{
					if (object.ReferenceEquals(obj75.Target, instance))
					{
						this.OnGetPaymentTokenResultEvent = (PlayFabResultEvent<GetPaymentTokenResult>)Delegate.Remove(this.OnGetPaymentTokenResultEvent, (PlayFabResultEvent<GetPaymentTokenResult>)obj75);
					}
				}
			}
			if (this.OnGetPhotonAuthenticationTokenRequestEvent != null)
			{
				Delegate[] invocationList76 = this.OnGetPhotonAuthenticationTokenRequestEvent.GetInvocationList();
				foreach (Delegate obj76 in invocationList76)
				{
					if (object.ReferenceEquals(obj76.Target, instance))
					{
						this.OnGetPhotonAuthenticationTokenRequestEvent = (PlayFabRequestEvent<GetPhotonAuthenticationTokenRequest>)Delegate.Remove(this.OnGetPhotonAuthenticationTokenRequestEvent, (PlayFabRequestEvent<GetPhotonAuthenticationTokenRequest>)obj76);
					}
				}
			}
			if (this.OnGetPhotonAuthenticationTokenResultEvent != null)
			{
				Delegate[] invocationList77 = this.OnGetPhotonAuthenticationTokenResultEvent.GetInvocationList();
				foreach (Delegate obj77 in invocationList77)
				{
					if (object.ReferenceEquals(obj77.Target, instance))
					{
						this.OnGetPhotonAuthenticationTokenResultEvent = (PlayFabResultEvent<GetPhotonAuthenticationTokenResult>)Delegate.Remove(this.OnGetPhotonAuthenticationTokenResultEvent, (PlayFabResultEvent<GetPhotonAuthenticationTokenResult>)obj77);
					}
				}
			}
			if (this.OnGetPlayerCombinedInfoRequestEvent != null)
			{
				Delegate[] invocationList78 = this.OnGetPlayerCombinedInfoRequestEvent.GetInvocationList();
				foreach (Delegate obj78 in invocationList78)
				{
					if (object.ReferenceEquals(obj78.Target, instance))
					{
						this.OnGetPlayerCombinedInfoRequestEvent = (PlayFabRequestEvent<GetPlayerCombinedInfoRequest>)Delegate.Remove(this.OnGetPlayerCombinedInfoRequestEvent, (PlayFabRequestEvent<GetPlayerCombinedInfoRequest>)obj78);
					}
				}
			}
			if (this.OnGetPlayerCombinedInfoResultEvent != null)
			{
				Delegate[] invocationList79 = this.OnGetPlayerCombinedInfoResultEvent.GetInvocationList();
				foreach (Delegate obj79 in invocationList79)
				{
					if (object.ReferenceEquals(obj79.Target, instance))
					{
						this.OnGetPlayerCombinedInfoResultEvent = (PlayFabResultEvent<GetPlayerCombinedInfoResult>)Delegate.Remove(this.OnGetPlayerCombinedInfoResultEvent, (PlayFabResultEvent<GetPlayerCombinedInfoResult>)obj79);
					}
				}
			}
			if (this.OnGetPlayerProfileRequestEvent != null)
			{
				Delegate[] invocationList80 = this.OnGetPlayerProfileRequestEvent.GetInvocationList();
				foreach (Delegate obj80 in invocationList80)
				{
					if (object.ReferenceEquals(obj80.Target, instance))
					{
						this.OnGetPlayerProfileRequestEvent = (PlayFabRequestEvent<GetPlayerProfileRequest>)Delegate.Remove(this.OnGetPlayerProfileRequestEvent, (PlayFabRequestEvent<GetPlayerProfileRequest>)obj80);
					}
				}
			}
			if (this.OnGetPlayerProfileResultEvent != null)
			{
				Delegate[] invocationList81 = this.OnGetPlayerProfileResultEvent.GetInvocationList();
				foreach (Delegate obj81 in invocationList81)
				{
					if (object.ReferenceEquals(obj81.Target, instance))
					{
						this.OnGetPlayerProfileResultEvent = (PlayFabResultEvent<GetPlayerProfileResult>)Delegate.Remove(this.OnGetPlayerProfileResultEvent, (PlayFabResultEvent<GetPlayerProfileResult>)obj81);
					}
				}
			}
			if (this.OnGetPlayerSegmentsRequestEvent != null)
			{
				Delegate[] invocationList82 = this.OnGetPlayerSegmentsRequestEvent.GetInvocationList();
				foreach (Delegate obj82 in invocationList82)
				{
					if (object.ReferenceEquals(obj82.Target, instance))
					{
						this.OnGetPlayerSegmentsRequestEvent = (PlayFabRequestEvent<GetPlayerSegmentsRequest>)Delegate.Remove(this.OnGetPlayerSegmentsRequestEvent, (PlayFabRequestEvent<GetPlayerSegmentsRequest>)obj82);
					}
				}
			}
			if (this.OnGetPlayerSegmentsResultEvent != null)
			{
				Delegate[] invocationList83 = this.OnGetPlayerSegmentsResultEvent.GetInvocationList();
				foreach (Delegate obj83 in invocationList83)
				{
					if (object.ReferenceEquals(obj83.Target, instance))
					{
						this.OnGetPlayerSegmentsResultEvent = (PlayFabResultEvent<GetPlayerSegmentsResult>)Delegate.Remove(this.OnGetPlayerSegmentsResultEvent, (PlayFabResultEvent<GetPlayerSegmentsResult>)obj83);
					}
				}
			}
			if (this.OnGetPlayerStatisticsRequestEvent != null)
			{
				Delegate[] invocationList84 = this.OnGetPlayerStatisticsRequestEvent.GetInvocationList();
				foreach (Delegate obj84 in invocationList84)
				{
					if (object.ReferenceEquals(obj84.Target, instance))
					{
						this.OnGetPlayerStatisticsRequestEvent = (PlayFabRequestEvent<GetPlayerStatisticsRequest>)Delegate.Remove(this.OnGetPlayerStatisticsRequestEvent, (PlayFabRequestEvent<GetPlayerStatisticsRequest>)obj84);
					}
				}
			}
			if (this.OnGetPlayerStatisticsResultEvent != null)
			{
				Delegate[] invocationList85 = this.OnGetPlayerStatisticsResultEvent.GetInvocationList();
				foreach (Delegate obj85 in invocationList85)
				{
					if (object.ReferenceEquals(obj85.Target, instance))
					{
						this.OnGetPlayerStatisticsResultEvent = (PlayFabResultEvent<GetPlayerStatisticsResult>)Delegate.Remove(this.OnGetPlayerStatisticsResultEvent, (PlayFabResultEvent<GetPlayerStatisticsResult>)obj85);
					}
				}
			}
			if (this.OnGetPlayerStatisticVersionsRequestEvent != null)
			{
				Delegate[] invocationList86 = this.OnGetPlayerStatisticVersionsRequestEvent.GetInvocationList();
				foreach (Delegate obj86 in invocationList86)
				{
					if (object.ReferenceEquals(obj86.Target, instance))
					{
						this.OnGetPlayerStatisticVersionsRequestEvent = (PlayFabRequestEvent<GetPlayerStatisticVersionsRequest>)Delegate.Remove(this.OnGetPlayerStatisticVersionsRequestEvent, (PlayFabRequestEvent<GetPlayerStatisticVersionsRequest>)obj86);
					}
				}
			}
			if (this.OnGetPlayerStatisticVersionsResultEvent != null)
			{
				Delegate[] invocationList87 = this.OnGetPlayerStatisticVersionsResultEvent.GetInvocationList();
				foreach (Delegate obj87 in invocationList87)
				{
					if (object.ReferenceEquals(obj87.Target, instance))
					{
						this.OnGetPlayerStatisticVersionsResultEvent = (PlayFabResultEvent<GetPlayerStatisticVersionsResult>)Delegate.Remove(this.OnGetPlayerStatisticVersionsResultEvent, (PlayFabResultEvent<GetPlayerStatisticVersionsResult>)obj87);
					}
				}
			}
			if (this.OnGetPlayerTagsRequestEvent != null)
			{
				Delegate[] invocationList88 = this.OnGetPlayerTagsRequestEvent.GetInvocationList();
				foreach (Delegate obj88 in invocationList88)
				{
					if (object.ReferenceEquals(obj88.Target, instance))
					{
						this.OnGetPlayerTagsRequestEvent = (PlayFabRequestEvent<GetPlayerTagsRequest>)Delegate.Remove(this.OnGetPlayerTagsRequestEvent, (PlayFabRequestEvent<GetPlayerTagsRequest>)obj88);
					}
				}
			}
			if (this.OnGetPlayerTagsResultEvent != null)
			{
				Delegate[] invocationList89 = this.OnGetPlayerTagsResultEvent.GetInvocationList();
				foreach (Delegate obj89 in invocationList89)
				{
					if (object.ReferenceEquals(obj89.Target, instance))
					{
						this.OnGetPlayerTagsResultEvent = (PlayFabResultEvent<GetPlayerTagsResult>)Delegate.Remove(this.OnGetPlayerTagsResultEvent, (PlayFabResultEvent<GetPlayerTagsResult>)obj89);
					}
				}
			}
			if (this.OnGetPlayerTradesRequestEvent != null)
			{
				Delegate[] invocationList90 = this.OnGetPlayerTradesRequestEvent.GetInvocationList();
				foreach (Delegate obj90 in invocationList90)
				{
					if (object.ReferenceEquals(obj90.Target, instance))
					{
						this.OnGetPlayerTradesRequestEvent = (PlayFabRequestEvent<GetPlayerTradesRequest>)Delegate.Remove(this.OnGetPlayerTradesRequestEvent, (PlayFabRequestEvent<GetPlayerTradesRequest>)obj90);
					}
				}
			}
			if (this.OnGetPlayerTradesResultEvent != null)
			{
				Delegate[] invocationList91 = this.OnGetPlayerTradesResultEvent.GetInvocationList();
				foreach (Delegate obj91 in invocationList91)
				{
					if (object.ReferenceEquals(obj91.Target, instance))
					{
						this.OnGetPlayerTradesResultEvent = (PlayFabResultEvent<GetPlayerTradesResponse>)Delegate.Remove(this.OnGetPlayerTradesResultEvent, (PlayFabResultEvent<GetPlayerTradesResponse>)obj91);
					}
				}
			}
			if (this.OnGetPlayFabIDsFromFacebookIDsRequestEvent != null)
			{
				Delegate[] invocationList92 = this.OnGetPlayFabIDsFromFacebookIDsRequestEvent.GetInvocationList();
				foreach (Delegate obj92 in invocationList92)
				{
					if (object.ReferenceEquals(obj92.Target, instance))
					{
						this.OnGetPlayFabIDsFromFacebookIDsRequestEvent = (PlayFabRequestEvent<GetPlayFabIDsFromFacebookIDsRequest>)Delegate.Remove(this.OnGetPlayFabIDsFromFacebookIDsRequestEvent, (PlayFabRequestEvent<GetPlayFabIDsFromFacebookIDsRequest>)obj92);
					}
				}
			}
			if (this.OnGetPlayFabIDsFromFacebookIDsResultEvent != null)
			{
				Delegate[] invocationList93 = this.OnGetPlayFabIDsFromFacebookIDsResultEvent.GetInvocationList();
				foreach (Delegate obj93 in invocationList93)
				{
					if (object.ReferenceEquals(obj93.Target, instance))
					{
						this.OnGetPlayFabIDsFromFacebookIDsResultEvent = (PlayFabResultEvent<GetPlayFabIDsFromFacebookIDsResult>)Delegate.Remove(this.OnGetPlayFabIDsFromFacebookIDsResultEvent, (PlayFabResultEvent<GetPlayFabIDsFromFacebookIDsResult>)obj93);
					}
				}
			}
			if (this.OnGetPlayFabIDsFromFacebookInstantGamesIdsRequestEvent != null)
			{
				Delegate[] invocationList94 = this.OnGetPlayFabIDsFromFacebookInstantGamesIdsRequestEvent.GetInvocationList();
				foreach (Delegate obj94 in invocationList94)
				{
					if (object.ReferenceEquals(obj94.Target, instance))
					{
						this.OnGetPlayFabIDsFromFacebookInstantGamesIdsRequestEvent = (PlayFabRequestEvent<GetPlayFabIDsFromFacebookInstantGamesIdsRequest>)Delegate.Remove(this.OnGetPlayFabIDsFromFacebookInstantGamesIdsRequestEvent, (PlayFabRequestEvent<GetPlayFabIDsFromFacebookInstantGamesIdsRequest>)obj94);
					}
				}
			}
			if (this.OnGetPlayFabIDsFromFacebookInstantGamesIdsResultEvent != null)
			{
				Delegate[] invocationList95 = this.OnGetPlayFabIDsFromFacebookInstantGamesIdsResultEvent.GetInvocationList();
				foreach (Delegate obj95 in invocationList95)
				{
					if (object.ReferenceEquals(obj95.Target, instance))
					{
						this.OnGetPlayFabIDsFromFacebookInstantGamesIdsResultEvent = (PlayFabResultEvent<GetPlayFabIDsFromFacebookInstantGamesIdsResult>)Delegate.Remove(this.OnGetPlayFabIDsFromFacebookInstantGamesIdsResultEvent, (PlayFabResultEvent<GetPlayFabIDsFromFacebookInstantGamesIdsResult>)obj95);
					}
				}
			}
			if (this.OnGetPlayFabIDsFromGameCenterIDsRequestEvent != null)
			{
				Delegate[] invocationList96 = this.OnGetPlayFabIDsFromGameCenterIDsRequestEvent.GetInvocationList();
				foreach (Delegate obj96 in invocationList96)
				{
					if (object.ReferenceEquals(obj96.Target, instance))
					{
						this.OnGetPlayFabIDsFromGameCenterIDsRequestEvent = (PlayFabRequestEvent<GetPlayFabIDsFromGameCenterIDsRequest>)Delegate.Remove(this.OnGetPlayFabIDsFromGameCenterIDsRequestEvent, (PlayFabRequestEvent<GetPlayFabIDsFromGameCenterIDsRequest>)obj96);
					}
				}
			}
			if (this.OnGetPlayFabIDsFromGameCenterIDsResultEvent != null)
			{
				Delegate[] invocationList97 = this.OnGetPlayFabIDsFromGameCenterIDsResultEvent.GetInvocationList();
				foreach (Delegate obj97 in invocationList97)
				{
					if (object.ReferenceEquals(obj97.Target, instance))
					{
						this.OnGetPlayFabIDsFromGameCenterIDsResultEvent = (PlayFabResultEvent<GetPlayFabIDsFromGameCenterIDsResult>)Delegate.Remove(this.OnGetPlayFabIDsFromGameCenterIDsResultEvent, (PlayFabResultEvent<GetPlayFabIDsFromGameCenterIDsResult>)obj97);
					}
				}
			}
			if (this.OnGetPlayFabIDsFromGenericIDsRequestEvent != null)
			{
				Delegate[] invocationList98 = this.OnGetPlayFabIDsFromGenericIDsRequestEvent.GetInvocationList();
				foreach (Delegate obj98 in invocationList98)
				{
					if (object.ReferenceEquals(obj98.Target, instance))
					{
						this.OnGetPlayFabIDsFromGenericIDsRequestEvent = (PlayFabRequestEvent<GetPlayFabIDsFromGenericIDsRequest>)Delegate.Remove(this.OnGetPlayFabIDsFromGenericIDsRequestEvent, (PlayFabRequestEvent<GetPlayFabIDsFromGenericIDsRequest>)obj98);
					}
				}
			}
			if (this.OnGetPlayFabIDsFromGenericIDsResultEvent != null)
			{
				Delegate[] invocationList99 = this.OnGetPlayFabIDsFromGenericIDsResultEvent.GetInvocationList();
				foreach (Delegate obj99 in invocationList99)
				{
					if (object.ReferenceEquals(obj99.Target, instance))
					{
						this.OnGetPlayFabIDsFromGenericIDsResultEvent = (PlayFabResultEvent<GetPlayFabIDsFromGenericIDsResult>)Delegate.Remove(this.OnGetPlayFabIDsFromGenericIDsResultEvent, (PlayFabResultEvent<GetPlayFabIDsFromGenericIDsResult>)obj99);
					}
				}
			}
			if (this.OnGetPlayFabIDsFromGoogleIDsRequestEvent != null)
			{
				Delegate[] invocationList100 = this.OnGetPlayFabIDsFromGoogleIDsRequestEvent.GetInvocationList();
				foreach (Delegate obj100 in invocationList100)
				{
					if (object.ReferenceEquals(obj100.Target, instance))
					{
						this.OnGetPlayFabIDsFromGoogleIDsRequestEvent = (PlayFabRequestEvent<GetPlayFabIDsFromGoogleIDsRequest>)Delegate.Remove(this.OnGetPlayFabIDsFromGoogleIDsRequestEvent, (PlayFabRequestEvent<GetPlayFabIDsFromGoogleIDsRequest>)obj100);
					}
				}
			}
			if (this.OnGetPlayFabIDsFromGoogleIDsResultEvent != null)
			{
				Delegate[] invocationList101 = this.OnGetPlayFabIDsFromGoogleIDsResultEvent.GetInvocationList();
				foreach (Delegate obj101 in invocationList101)
				{
					if (object.ReferenceEquals(obj101.Target, instance))
					{
						this.OnGetPlayFabIDsFromGoogleIDsResultEvent = (PlayFabResultEvent<GetPlayFabIDsFromGoogleIDsResult>)Delegate.Remove(this.OnGetPlayFabIDsFromGoogleIDsResultEvent, (PlayFabResultEvent<GetPlayFabIDsFromGoogleIDsResult>)obj101);
					}
				}
			}
			if (this.OnGetPlayFabIDsFromKongregateIDsRequestEvent != null)
			{
				Delegate[] invocationList102 = this.OnGetPlayFabIDsFromKongregateIDsRequestEvent.GetInvocationList();
				foreach (Delegate obj102 in invocationList102)
				{
					if (object.ReferenceEquals(obj102.Target, instance))
					{
						this.OnGetPlayFabIDsFromKongregateIDsRequestEvent = (PlayFabRequestEvent<GetPlayFabIDsFromKongregateIDsRequest>)Delegate.Remove(this.OnGetPlayFabIDsFromKongregateIDsRequestEvent, (PlayFabRequestEvent<GetPlayFabIDsFromKongregateIDsRequest>)obj102);
					}
				}
			}
			if (this.OnGetPlayFabIDsFromKongregateIDsResultEvent != null)
			{
				Delegate[] invocationList103 = this.OnGetPlayFabIDsFromKongregateIDsResultEvent.GetInvocationList();
				foreach (Delegate obj103 in invocationList103)
				{
					if (object.ReferenceEquals(obj103.Target, instance))
					{
						this.OnGetPlayFabIDsFromKongregateIDsResultEvent = (PlayFabResultEvent<GetPlayFabIDsFromKongregateIDsResult>)Delegate.Remove(this.OnGetPlayFabIDsFromKongregateIDsResultEvent, (PlayFabResultEvent<GetPlayFabIDsFromKongregateIDsResult>)obj103);
					}
				}
			}
			if (this.OnGetPlayFabIDsFromNintendoSwitchDeviceIdsRequestEvent != null)
			{
				Delegate[] invocationList104 = this.OnGetPlayFabIDsFromNintendoSwitchDeviceIdsRequestEvent.GetInvocationList();
				foreach (Delegate obj104 in invocationList104)
				{
					if (object.ReferenceEquals(obj104.Target, instance))
					{
						this.OnGetPlayFabIDsFromNintendoSwitchDeviceIdsRequestEvent = (PlayFabRequestEvent<GetPlayFabIDsFromNintendoSwitchDeviceIdsRequest>)Delegate.Remove(this.OnGetPlayFabIDsFromNintendoSwitchDeviceIdsRequestEvent, (PlayFabRequestEvent<GetPlayFabIDsFromNintendoSwitchDeviceIdsRequest>)obj104);
					}
				}
			}
			if (this.OnGetPlayFabIDsFromNintendoSwitchDeviceIdsResultEvent != null)
			{
				Delegate[] invocationList105 = this.OnGetPlayFabIDsFromNintendoSwitchDeviceIdsResultEvent.GetInvocationList();
				foreach (Delegate obj105 in invocationList105)
				{
					if (object.ReferenceEquals(obj105.Target, instance))
					{
						this.OnGetPlayFabIDsFromNintendoSwitchDeviceIdsResultEvent = (PlayFabResultEvent<GetPlayFabIDsFromNintendoSwitchDeviceIdsResult>)Delegate.Remove(this.OnGetPlayFabIDsFromNintendoSwitchDeviceIdsResultEvent, (PlayFabResultEvent<GetPlayFabIDsFromNintendoSwitchDeviceIdsResult>)obj105);
					}
				}
			}
			if (this.OnGetPlayFabIDsFromPSNAccountIDsRequestEvent != null)
			{
				Delegate[] invocationList106 = this.OnGetPlayFabIDsFromPSNAccountIDsRequestEvent.GetInvocationList();
				foreach (Delegate obj106 in invocationList106)
				{
					if (object.ReferenceEquals(obj106.Target, instance))
					{
						this.OnGetPlayFabIDsFromPSNAccountIDsRequestEvent = (PlayFabRequestEvent<GetPlayFabIDsFromPSNAccountIDsRequest>)Delegate.Remove(this.OnGetPlayFabIDsFromPSNAccountIDsRequestEvent, (PlayFabRequestEvent<GetPlayFabIDsFromPSNAccountIDsRequest>)obj106);
					}
				}
			}
			if (this.OnGetPlayFabIDsFromPSNAccountIDsResultEvent != null)
			{
				Delegate[] invocationList107 = this.OnGetPlayFabIDsFromPSNAccountIDsResultEvent.GetInvocationList();
				foreach (Delegate obj107 in invocationList107)
				{
					if (object.ReferenceEquals(obj107.Target, instance))
					{
						this.OnGetPlayFabIDsFromPSNAccountIDsResultEvent = (PlayFabResultEvent<GetPlayFabIDsFromPSNAccountIDsResult>)Delegate.Remove(this.OnGetPlayFabIDsFromPSNAccountIDsResultEvent, (PlayFabResultEvent<GetPlayFabIDsFromPSNAccountIDsResult>)obj107);
					}
				}
			}
			if (this.OnGetPlayFabIDsFromSteamIDsRequestEvent != null)
			{
				Delegate[] invocationList108 = this.OnGetPlayFabIDsFromSteamIDsRequestEvent.GetInvocationList();
				foreach (Delegate obj108 in invocationList108)
				{
					if (object.ReferenceEquals(obj108.Target, instance))
					{
						this.OnGetPlayFabIDsFromSteamIDsRequestEvent = (PlayFabRequestEvent<GetPlayFabIDsFromSteamIDsRequest>)Delegate.Remove(this.OnGetPlayFabIDsFromSteamIDsRequestEvent, (PlayFabRequestEvent<GetPlayFabIDsFromSteamIDsRequest>)obj108);
					}
				}
			}
			if (this.OnGetPlayFabIDsFromSteamIDsResultEvent != null)
			{
				Delegate[] invocationList109 = this.OnGetPlayFabIDsFromSteamIDsResultEvent.GetInvocationList();
				foreach (Delegate obj109 in invocationList109)
				{
					if (object.ReferenceEquals(obj109.Target, instance))
					{
						this.OnGetPlayFabIDsFromSteamIDsResultEvent = (PlayFabResultEvent<GetPlayFabIDsFromSteamIDsResult>)Delegate.Remove(this.OnGetPlayFabIDsFromSteamIDsResultEvent, (PlayFabResultEvent<GetPlayFabIDsFromSteamIDsResult>)obj109);
					}
				}
			}
			if (this.OnGetPlayFabIDsFromTwitchIDsRequestEvent != null)
			{
				Delegate[] invocationList110 = this.OnGetPlayFabIDsFromTwitchIDsRequestEvent.GetInvocationList();
				foreach (Delegate obj110 in invocationList110)
				{
					if (object.ReferenceEquals(obj110.Target, instance))
					{
						this.OnGetPlayFabIDsFromTwitchIDsRequestEvent = (PlayFabRequestEvent<GetPlayFabIDsFromTwitchIDsRequest>)Delegate.Remove(this.OnGetPlayFabIDsFromTwitchIDsRequestEvent, (PlayFabRequestEvent<GetPlayFabIDsFromTwitchIDsRequest>)obj110);
					}
				}
			}
			if (this.OnGetPlayFabIDsFromTwitchIDsResultEvent != null)
			{
				Delegate[] invocationList111 = this.OnGetPlayFabIDsFromTwitchIDsResultEvent.GetInvocationList();
				foreach (Delegate obj111 in invocationList111)
				{
					if (object.ReferenceEquals(obj111.Target, instance))
					{
						this.OnGetPlayFabIDsFromTwitchIDsResultEvent = (PlayFabResultEvent<GetPlayFabIDsFromTwitchIDsResult>)Delegate.Remove(this.OnGetPlayFabIDsFromTwitchIDsResultEvent, (PlayFabResultEvent<GetPlayFabIDsFromTwitchIDsResult>)obj111);
					}
				}
			}
			if (this.OnGetPlayFabIDsFromXboxLiveIDsRequestEvent != null)
			{
				Delegate[] invocationList112 = this.OnGetPlayFabIDsFromXboxLiveIDsRequestEvent.GetInvocationList();
				foreach (Delegate obj112 in invocationList112)
				{
					if (object.ReferenceEquals(obj112.Target, instance))
					{
						this.OnGetPlayFabIDsFromXboxLiveIDsRequestEvent = (PlayFabRequestEvent<GetPlayFabIDsFromXboxLiveIDsRequest>)Delegate.Remove(this.OnGetPlayFabIDsFromXboxLiveIDsRequestEvent, (PlayFabRequestEvent<GetPlayFabIDsFromXboxLiveIDsRequest>)obj112);
					}
				}
			}
			if (this.OnGetPlayFabIDsFromXboxLiveIDsResultEvent != null)
			{
				Delegate[] invocationList113 = this.OnGetPlayFabIDsFromXboxLiveIDsResultEvent.GetInvocationList();
				foreach (Delegate obj113 in invocationList113)
				{
					if (object.ReferenceEquals(obj113.Target, instance))
					{
						this.OnGetPlayFabIDsFromXboxLiveIDsResultEvent = (PlayFabResultEvent<GetPlayFabIDsFromXboxLiveIDsResult>)Delegate.Remove(this.OnGetPlayFabIDsFromXboxLiveIDsResultEvent, (PlayFabResultEvent<GetPlayFabIDsFromXboxLiveIDsResult>)obj113);
					}
				}
			}
			if (this.OnGetPublisherDataRequestEvent != null)
			{
				Delegate[] invocationList114 = this.OnGetPublisherDataRequestEvent.GetInvocationList();
				foreach (Delegate obj114 in invocationList114)
				{
					if (object.ReferenceEquals(obj114.Target, instance))
					{
						this.OnGetPublisherDataRequestEvent = (PlayFabRequestEvent<GetPublisherDataRequest>)Delegate.Remove(this.OnGetPublisherDataRequestEvent, (PlayFabRequestEvent<GetPublisherDataRequest>)obj114);
					}
				}
			}
			if (this.OnGetPublisherDataResultEvent != null)
			{
				Delegate[] invocationList115 = this.OnGetPublisherDataResultEvent.GetInvocationList();
				foreach (Delegate obj115 in invocationList115)
				{
					if (object.ReferenceEquals(obj115.Target, instance))
					{
						this.OnGetPublisherDataResultEvent = (PlayFabResultEvent<GetPublisherDataResult>)Delegate.Remove(this.OnGetPublisherDataResultEvent, (PlayFabResultEvent<GetPublisherDataResult>)obj115);
					}
				}
			}
			if (this.OnGetPurchaseRequestEvent != null)
			{
				Delegate[] invocationList116 = this.OnGetPurchaseRequestEvent.GetInvocationList();
				foreach (Delegate obj116 in invocationList116)
				{
					if (object.ReferenceEquals(obj116.Target, instance))
					{
						this.OnGetPurchaseRequestEvent = (PlayFabRequestEvent<GetPurchaseRequest>)Delegate.Remove(this.OnGetPurchaseRequestEvent, (PlayFabRequestEvent<GetPurchaseRequest>)obj116);
					}
				}
			}
			if (this.OnGetPurchaseResultEvent != null)
			{
				Delegate[] invocationList117 = this.OnGetPurchaseResultEvent.GetInvocationList();
				foreach (Delegate obj117 in invocationList117)
				{
					if (object.ReferenceEquals(obj117.Target, instance))
					{
						this.OnGetPurchaseResultEvent = (PlayFabResultEvent<GetPurchaseResult>)Delegate.Remove(this.OnGetPurchaseResultEvent, (PlayFabResultEvent<GetPurchaseResult>)obj117);
					}
				}
			}
			if (this.OnGetSharedGroupDataRequestEvent != null)
			{
				Delegate[] invocationList118 = this.OnGetSharedGroupDataRequestEvent.GetInvocationList();
				foreach (Delegate obj118 in invocationList118)
				{
					if (object.ReferenceEquals(obj118.Target, instance))
					{
						this.OnGetSharedGroupDataRequestEvent = (PlayFabRequestEvent<GetSharedGroupDataRequest>)Delegate.Remove(this.OnGetSharedGroupDataRequestEvent, (PlayFabRequestEvent<GetSharedGroupDataRequest>)obj118);
					}
				}
			}
			if (this.OnGetSharedGroupDataResultEvent != null)
			{
				Delegate[] invocationList119 = this.OnGetSharedGroupDataResultEvent.GetInvocationList();
				foreach (Delegate obj119 in invocationList119)
				{
					if (object.ReferenceEquals(obj119.Target, instance))
					{
						this.OnGetSharedGroupDataResultEvent = (PlayFabResultEvent<GetSharedGroupDataResult>)Delegate.Remove(this.OnGetSharedGroupDataResultEvent, (PlayFabResultEvent<GetSharedGroupDataResult>)obj119);
					}
				}
			}
			if (this.OnGetStoreItemsRequestEvent != null)
			{
				Delegate[] invocationList120 = this.OnGetStoreItemsRequestEvent.GetInvocationList();
				foreach (Delegate obj120 in invocationList120)
				{
					if (object.ReferenceEquals(obj120.Target, instance))
					{
						this.OnGetStoreItemsRequestEvent = (PlayFabRequestEvent<GetStoreItemsRequest>)Delegate.Remove(this.OnGetStoreItemsRequestEvent, (PlayFabRequestEvent<GetStoreItemsRequest>)obj120);
					}
				}
			}
			if (this.OnGetStoreItemsResultEvent != null)
			{
				Delegate[] invocationList121 = this.OnGetStoreItemsResultEvent.GetInvocationList();
				foreach (Delegate obj121 in invocationList121)
				{
					if (object.ReferenceEquals(obj121.Target, instance))
					{
						this.OnGetStoreItemsResultEvent = (PlayFabResultEvent<GetStoreItemsResult>)Delegate.Remove(this.OnGetStoreItemsResultEvent, (PlayFabResultEvent<GetStoreItemsResult>)obj121);
					}
				}
			}
			if (this.OnGetTimeRequestEvent != null)
			{
				Delegate[] invocationList122 = this.OnGetTimeRequestEvent.GetInvocationList();
				foreach (Delegate obj122 in invocationList122)
				{
					if (object.ReferenceEquals(obj122.Target, instance))
					{
						this.OnGetTimeRequestEvent = (PlayFabRequestEvent<GetTimeRequest>)Delegate.Remove(this.OnGetTimeRequestEvent, (PlayFabRequestEvent<GetTimeRequest>)obj122);
					}
				}
			}
			if (this.OnGetTimeResultEvent != null)
			{
				Delegate[] invocationList123 = this.OnGetTimeResultEvent.GetInvocationList();
				foreach (Delegate obj123 in invocationList123)
				{
					if (object.ReferenceEquals(obj123.Target, instance))
					{
						this.OnGetTimeResultEvent = (PlayFabResultEvent<GetTimeResult>)Delegate.Remove(this.OnGetTimeResultEvent, (PlayFabResultEvent<GetTimeResult>)obj123);
					}
				}
			}
			if (this.OnGetTitleDataRequestEvent != null)
			{
				Delegate[] invocationList124 = this.OnGetTitleDataRequestEvent.GetInvocationList();
				foreach (Delegate obj124 in invocationList124)
				{
					if (object.ReferenceEquals(obj124.Target, instance))
					{
						this.OnGetTitleDataRequestEvent = (PlayFabRequestEvent<GetTitleDataRequest>)Delegate.Remove(this.OnGetTitleDataRequestEvent, (PlayFabRequestEvent<GetTitleDataRequest>)obj124);
					}
				}
			}
			if (this.OnGetTitleDataResultEvent != null)
			{
				Delegate[] invocationList125 = this.OnGetTitleDataResultEvent.GetInvocationList();
				foreach (Delegate obj125 in invocationList125)
				{
					if (object.ReferenceEquals(obj125.Target, instance))
					{
						this.OnGetTitleDataResultEvent = (PlayFabResultEvent<GetTitleDataResult>)Delegate.Remove(this.OnGetTitleDataResultEvent, (PlayFabResultEvent<GetTitleDataResult>)obj125);
					}
				}
			}
			if (this.OnGetTitleNewsRequestEvent != null)
			{
				Delegate[] invocationList126 = this.OnGetTitleNewsRequestEvent.GetInvocationList();
				foreach (Delegate obj126 in invocationList126)
				{
					if (object.ReferenceEquals(obj126.Target, instance))
					{
						this.OnGetTitleNewsRequestEvent = (PlayFabRequestEvent<GetTitleNewsRequest>)Delegate.Remove(this.OnGetTitleNewsRequestEvent, (PlayFabRequestEvent<GetTitleNewsRequest>)obj126);
					}
				}
			}
			if (this.OnGetTitleNewsResultEvent != null)
			{
				Delegate[] invocationList127 = this.OnGetTitleNewsResultEvent.GetInvocationList();
				foreach (Delegate obj127 in invocationList127)
				{
					if (object.ReferenceEquals(obj127.Target, instance))
					{
						this.OnGetTitleNewsResultEvent = (PlayFabResultEvent<GetTitleNewsResult>)Delegate.Remove(this.OnGetTitleNewsResultEvent, (PlayFabResultEvent<GetTitleNewsResult>)obj127);
					}
				}
			}
			if (this.OnGetTitlePublicKeyRequestEvent != null)
			{
				Delegate[] invocationList128 = this.OnGetTitlePublicKeyRequestEvent.GetInvocationList();
				foreach (Delegate obj128 in invocationList128)
				{
					if (object.ReferenceEquals(obj128.Target, instance))
					{
						this.OnGetTitlePublicKeyRequestEvent = (PlayFabRequestEvent<GetTitlePublicKeyRequest>)Delegate.Remove(this.OnGetTitlePublicKeyRequestEvent, (PlayFabRequestEvent<GetTitlePublicKeyRequest>)obj128);
					}
				}
			}
			if (this.OnGetTitlePublicKeyResultEvent != null)
			{
				Delegate[] invocationList129 = this.OnGetTitlePublicKeyResultEvent.GetInvocationList();
				foreach (Delegate obj129 in invocationList129)
				{
					if (object.ReferenceEquals(obj129.Target, instance))
					{
						this.OnGetTitlePublicKeyResultEvent = (PlayFabResultEvent<GetTitlePublicKeyResult>)Delegate.Remove(this.OnGetTitlePublicKeyResultEvent, (PlayFabResultEvent<GetTitlePublicKeyResult>)obj129);
					}
				}
			}
			if (this.OnGetTradeStatusRequestEvent != null)
			{
				Delegate[] invocationList130 = this.OnGetTradeStatusRequestEvent.GetInvocationList();
				foreach (Delegate obj130 in invocationList130)
				{
					if (object.ReferenceEquals(obj130.Target, instance))
					{
						this.OnGetTradeStatusRequestEvent = (PlayFabRequestEvent<GetTradeStatusRequest>)Delegate.Remove(this.OnGetTradeStatusRequestEvent, (PlayFabRequestEvent<GetTradeStatusRequest>)obj130);
					}
				}
			}
			if (this.OnGetTradeStatusResultEvent != null)
			{
				Delegate[] invocationList131 = this.OnGetTradeStatusResultEvent.GetInvocationList();
				foreach (Delegate obj131 in invocationList131)
				{
					if (object.ReferenceEquals(obj131.Target, instance))
					{
						this.OnGetTradeStatusResultEvent = (PlayFabResultEvent<GetTradeStatusResponse>)Delegate.Remove(this.OnGetTradeStatusResultEvent, (PlayFabResultEvent<GetTradeStatusResponse>)obj131);
					}
				}
			}
			if (this.OnGetUserDataRequestEvent != null)
			{
				Delegate[] invocationList132 = this.OnGetUserDataRequestEvent.GetInvocationList();
				foreach (Delegate obj132 in invocationList132)
				{
					if (object.ReferenceEquals(obj132.Target, instance))
					{
						this.OnGetUserDataRequestEvent = (PlayFabRequestEvent<GetUserDataRequest>)Delegate.Remove(this.OnGetUserDataRequestEvent, (PlayFabRequestEvent<GetUserDataRequest>)obj132);
					}
				}
			}
			if (this.OnGetUserDataResultEvent != null)
			{
				Delegate[] invocationList133 = this.OnGetUserDataResultEvent.GetInvocationList();
				foreach (Delegate obj133 in invocationList133)
				{
					if (object.ReferenceEquals(obj133.Target, instance))
					{
						this.OnGetUserDataResultEvent = (PlayFabResultEvent<GetUserDataResult>)Delegate.Remove(this.OnGetUserDataResultEvent, (PlayFabResultEvent<GetUserDataResult>)obj133);
					}
				}
			}
			if (this.OnGetUserInventoryRequestEvent != null)
			{
				Delegate[] invocationList134 = this.OnGetUserInventoryRequestEvent.GetInvocationList();
				foreach (Delegate obj134 in invocationList134)
				{
					if (object.ReferenceEquals(obj134.Target, instance))
					{
						this.OnGetUserInventoryRequestEvent = (PlayFabRequestEvent<GetUserInventoryRequest>)Delegate.Remove(this.OnGetUserInventoryRequestEvent, (PlayFabRequestEvent<GetUserInventoryRequest>)obj134);
					}
				}
			}
			if (this.OnGetUserInventoryResultEvent != null)
			{
				Delegate[] invocationList135 = this.OnGetUserInventoryResultEvent.GetInvocationList();
				foreach (Delegate obj135 in invocationList135)
				{
					if (object.ReferenceEquals(obj135.Target, instance))
					{
						this.OnGetUserInventoryResultEvent = (PlayFabResultEvent<GetUserInventoryResult>)Delegate.Remove(this.OnGetUserInventoryResultEvent, (PlayFabResultEvent<GetUserInventoryResult>)obj135);
					}
				}
			}
			if (this.OnGetUserPublisherDataRequestEvent != null)
			{
				Delegate[] invocationList136 = this.OnGetUserPublisherDataRequestEvent.GetInvocationList();
				foreach (Delegate obj136 in invocationList136)
				{
					if (object.ReferenceEquals(obj136.Target, instance))
					{
						this.OnGetUserPublisherDataRequestEvent = (PlayFabRequestEvent<GetUserDataRequest>)Delegate.Remove(this.OnGetUserPublisherDataRequestEvent, (PlayFabRequestEvent<GetUserDataRequest>)obj136);
					}
				}
			}
			if (this.OnGetUserPublisherDataResultEvent != null)
			{
				Delegate[] invocationList137 = this.OnGetUserPublisherDataResultEvent.GetInvocationList();
				foreach (Delegate obj137 in invocationList137)
				{
					if (object.ReferenceEquals(obj137.Target, instance))
					{
						this.OnGetUserPublisherDataResultEvent = (PlayFabResultEvent<GetUserDataResult>)Delegate.Remove(this.OnGetUserPublisherDataResultEvent, (PlayFabResultEvent<GetUserDataResult>)obj137);
					}
				}
			}
			if (this.OnGetUserPublisherReadOnlyDataRequestEvent != null)
			{
				Delegate[] invocationList138 = this.OnGetUserPublisherReadOnlyDataRequestEvent.GetInvocationList();
				foreach (Delegate obj138 in invocationList138)
				{
					if (object.ReferenceEquals(obj138.Target, instance))
					{
						this.OnGetUserPublisherReadOnlyDataRequestEvent = (PlayFabRequestEvent<GetUserDataRequest>)Delegate.Remove(this.OnGetUserPublisherReadOnlyDataRequestEvent, (PlayFabRequestEvent<GetUserDataRequest>)obj138);
					}
				}
			}
			if (this.OnGetUserPublisherReadOnlyDataResultEvent != null)
			{
				Delegate[] invocationList139 = this.OnGetUserPublisherReadOnlyDataResultEvent.GetInvocationList();
				foreach (Delegate obj139 in invocationList139)
				{
					if (object.ReferenceEquals(obj139.Target, instance))
					{
						this.OnGetUserPublisherReadOnlyDataResultEvent = (PlayFabResultEvent<GetUserDataResult>)Delegate.Remove(this.OnGetUserPublisherReadOnlyDataResultEvent, (PlayFabResultEvent<GetUserDataResult>)obj139);
					}
				}
			}
			if (this.OnGetUserReadOnlyDataRequestEvent != null)
			{
				Delegate[] invocationList140 = this.OnGetUserReadOnlyDataRequestEvent.GetInvocationList();
				foreach (Delegate obj140 in invocationList140)
				{
					if (object.ReferenceEquals(obj140.Target, instance))
					{
						this.OnGetUserReadOnlyDataRequestEvent = (PlayFabRequestEvent<GetUserDataRequest>)Delegate.Remove(this.OnGetUserReadOnlyDataRequestEvent, (PlayFabRequestEvent<GetUserDataRequest>)obj140);
					}
				}
			}
			if (this.OnGetUserReadOnlyDataResultEvent != null)
			{
				Delegate[] invocationList141 = this.OnGetUserReadOnlyDataResultEvent.GetInvocationList();
				foreach (Delegate obj141 in invocationList141)
				{
					if (object.ReferenceEquals(obj141.Target, instance))
					{
						this.OnGetUserReadOnlyDataResultEvent = (PlayFabResultEvent<GetUserDataResult>)Delegate.Remove(this.OnGetUserReadOnlyDataResultEvent, (PlayFabResultEvent<GetUserDataResult>)obj141);
					}
				}
			}
			if (this.OnGetWindowsHelloChallengeRequestEvent != null)
			{
				Delegate[] invocationList142 = this.OnGetWindowsHelloChallengeRequestEvent.GetInvocationList();
				foreach (Delegate obj142 in invocationList142)
				{
					if (object.ReferenceEquals(obj142.Target, instance))
					{
						this.OnGetWindowsHelloChallengeRequestEvent = (PlayFabRequestEvent<GetWindowsHelloChallengeRequest>)Delegate.Remove(this.OnGetWindowsHelloChallengeRequestEvent, (PlayFabRequestEvent<GetWindowsHelloChallengeRequest>)obj142);
					}
				}
			}
			if (this.OnGetWindowsHelloChallengeResultEvent != null)
			{
				Delegate[] invocationList143 = this.OnGetWindowsHelloChallengeResultEvent.GetInvocationList();
				foreach (Delegate obj143 in invocationList143)
				{
					if (object.ReferenceEquals(obj143.Target, instance))
					{
						this.OnGetWindowsHelloChallengeResultEvent = (PlayFabResultEvent<GetWindowsHelloChallengeResponse>)Delegate.Remove(this.OnGetWindowsHelloChallengeResultEvent, (PlayFabResultEvent<GetWindowsHelloChallengeResponse>)obj143);
					}
				}
			}
			if (this.OnGrantCharacterToUserRequestEvent != null)
			{
				Delegate[] invocationList144 = this.OnGrantCharacterToUserRequestEvent.GetInvocationList();
				foreach (Delegate obj144 in invocationList144)
				{
					if (object.ReferenceEquals(obj144.Target, instance))
					{
						this.OnGrantCharacterToUserRequestEvent = (PlayFabRequestEvent<GrantCharacterToUserRequest>)Delegate.Remove(this.OnGrantCharacterToUserRequestEvent, (PlayFabRequestEvent<GrantCharacterToUserRequest>)obj144);
					}
				}
			}
			if (this.OnGrantCharacterToUserResultEvent != null)
			{
				Delegate[] invocationList145 = this.OnGrantCharacterToUserResultEvent.GetInvocationList();
				foreach (Delegate obj145 in invocationList145)
				{
					if (object.ReferenceEquals(obj145.Target, instance))
					{
						this.OnGrantCharacterToUserResultEvent = (PlayFabResultEvent<GrantCharacterToUserResult>)Delegate.Remove(this.OnGrantCharacterToUserResultEvent, (PlayFabResultEvent<GrantCharacterToUserResult>)obj145);
					}
				}
			}
			if (this.OnLinkAndroidDeviceIDRequestEvent != null)
			{
				Delegate[] invocationList146 = this.OnLinkAndroidDeviceIDRequestEvent.GetInvocationList();
				foreach (Delegate obj146 in invocationList146)
				{
					if (object.ReferenceEquals(obj146.Target, instance))
					{
						this.OnLinkAndroidDeviceIDRequestEvent = (PlayFabRequestEvent<LinkAndroidDeviceIDRequest>)Delegate.Remove(this.OnLinkAndroidDeviceIDRequestEvent, (PlayFabRequestEvent<LinkAndroidDeviceIDRequest>)obj146);
					}
				}
			}
			if (this.OnLinkAndroidDeviceIDResultEvent != null)
			{
				Delegate[] invocationList147 = this.OnLinkAndroidDeviceIDResultEvent.GetInvocationList();
				foreach (Delegate obj147 in invocationList147)
				{
					if (object.ReferenceEquals(obj147.Target, instance))
					{
						this.OnLinkAndroidDeviceIDResultEvent = (PlayFabResultEvent<LinkAndroidDeviceIDResult>)Delegate.Remove(this.OnLinkAndroidDeviceIDResultEvent, (PlayFabResultEvent<LinkAndroidDeviceIDResult>)obj147);
					}
				}
			}
			if (this.OnLinkAppleRequestEvent != null)
			{
				Delegate[] invocationList148 = this.OnLinkAppleRequestEvent.GetInvocationList();
				foreach (Delegate obj148 in invocationList148)
				{
					if (object.ReferenceEquals(obj148.Target, instance))
					{
						this.OnLinkAppleRequestEvent = (PlayFabRequestEvent<LinkAppleRequest>)Delegate.Remove(this.OnLinkAppleRequestEvent, (PlayFabRequestEvent<LinkAppleRequest>)obj148);
					}
				}
			}
			if (this.OnLinkAppleResultEvent != null)
			{
				Delegate[] invocationList149 = this.OnLinkAppleResultEvent.GetInvocationList();
				foreach (Delegate obj149 in invocationList149)
				{
					if (object.ReferenceEquals(obj149.Target, instance))
					{
						this.OnLinkAppleResultEvent = (PlayFabResultEvent<PlayFab.ClientModels.EmptyResult>)Delegate.Remove(this.OnLinkAppleResultEvent, (PlayFabResultEvent<PlayFab.ClientModels.EmptyResult>)obj149);
					}
				}
			}
			if (this.OnLinkCustomIDRequestEvent != null)
			{
				Delegate[] invocationList150 = this.OnLinkCustomIDRequestEvent.GetInvocationList();
				foreach (Delegate obj150 in invocationList150)
				{
					if (object.ReferenceEquals(obj150.Target, instance))
					{
						this.OnLinkCustomIDRequestEvent = (PlayFabRequestEvent<LinkCustomIDRequest>)Delegate.Remove(this.OnLinkCustomIDRequestEvent, (PlayFabRequestEvent<LinkCustomIDRequest>)obj150);
					}
				}
			}
			if (this.OnLinkCustomIDResultEvent != null)
			{
				Delegate[] invocationList151 = this.OnLinkCustomIDResultEvent.GetInvocationList();
				foreach (Delegate obj151 in invocationList151)
				{
					if (object.ReferenceEquals(obj151.Target, instance))
					{
						this.OnLinkCustomIDResultEvent = (PlayFabResultEvent<LinkCustomIDResult>)Delegate.Remove(this.OnLinkCustomIDResultEvent, (PlayFabResultEvent<LinkCustomIDResult>)obj151);
					}
				}
			}
			if (this.OnLinkFacebookAccountRequestEvent != null)
			{
				Delegate[] invocationList152 = this.OnLinkFacebookAccountRequestEvent.GetInvocationList();
				foreach (Delegate obj152 in invocationList152)
				{
					if (object.ReferenceEquals(obj152.Target, instance))
					{
						this.OnLinkFacebookAccountRequestEvent = (PlayFabRequestEvent<LinkFacebookAccountRequest>)Delegate.Remove(this.OnLinkFacebookAccountRequestEvent, (PlayFabRequestEvent<LinkFacebookAccountRequest>)obj152);
					}
				}
			}
			if (this.OnLinkFacebookAccountResultEvent != null)
			{
				Delegate[] invocationList153 = this.OnLinkFacebookAccountResultEvent.GetInvocationList();
				foreach (Delegate obj153 in invocationList153)
				{
					if (object.ReferenceEquals(obj153.Target, instance))
					{
						this.OnLinkFacebookAccountResultEvent = (PlayFabResultEvent<LinkFacebookAccountResult>)Delegate.Remove(this.OnLinkFacebookAccountResultEvent, (PlayFabResultEvent<LinkFacebookAccountResult>)obj153);
					}
				}
			}
			if (this.OnLinkFacebookInstantGamesIdRequestEvent != null)
			{
				Delegate[] invocationList154 = this.OnLinkFacebookInstantGamesIdRequestEvent.GetInvocationList();
				foreach (Delegate obj154 in invocationList154)
				{
					if (object.ReferenceEquals(obj154.Target, instance))
					{
						this.OnLinkFacebookInstantGamesIdRequestEvent = (PlayFabRequestEvent<LinkFacebookInstantGamesIdRequest>)Delegate.Remove(this.OnLinkFacebookInstantGamesIdRequestEvent, (PlayFabRequestEvent<LinkFacebookInstantGamesIdRequest>)obj154);
					}
				}
			}
			if (this.OnLinkFacebookInstantGamesIdResultEvent != null)
			{
				Delegate[] invocationList155 = this.OnLinkFacebookInstantGamesIdResultEvent.GetInvocationList();
				foreach (Delegate obj155 in invocationList155)
				{
					if (object.ReferenceEquals(obj155.Target, instance))
					{
						this.OnLinkFacebookInstantGamesIdResultEvent = (PlayFabResultEvent<LinkFacebookInstantGamesIdResult>)Delegate.Remove(this.OnLinkFacebookInstantGamesIdResultEvent, (PlayFabResultEvent<LinkFacebookInstantGamesIdResult>)obj155);
					}
				}
			}
			if (this.OnLinkGameCenterAccountRequestEvent != null)
			{
				Delegate[] invocationList156 = this.OnLinkGameCenterAccountRequestEvent.GetInvocationList();
				foreach (Delegate obj156 in invocationList156)
				{
					if (object.ReferenceEquals(obj156.Target, instance))
					{
						this.OnLinkGameCenterAccountRequestEvent = (PlayFabRequestEvent<LinkGameCenterAccountRequest>)Delegate.Remove(this.OnLinkGameCenterAccountRequestEvent, (PlayFabRequestEvent<LinkGameCenterAccountRequest>)obj156);
					}
				}
			}
			if (this.OnLinkGameCenterAccountResultEvent != null)
			{
				Delegate[] invocationList157 = this.OnLinkGameCenterAccountResultEvent.GetInvocationList();
				foreach (Delegate obj157 in invocationList157)
				{
					if (object.ReferenceEquals(obj157.Target, instance))
					{
						this.OnLinkGameCenterAccountResultEvent = (PlayFabResultEvent<LinkGameCenterAccountResult>)Delegate.Remove(this.OnLinkGameCenterAccountResultEvent, (PlayFabResultEvent<LinkGameCenterAccountResult>)obj157);
					}
				}
			}
			if (this.OnLinkGoogleAccountRequestEvent != null)
			{
				Delegate[] invocationList158 = this.OnLinkGoogleAccountRequestEvent.GetInvocationList();
				foreach (Delegate obj158 in invocationList158)
				{
					if (object.ReferenceEquals(obj158.Target, instance))
					{
						this.OnLinkGoogleAccountRequestEvent = (PlayFabRequestEvent<LinkGoogleAccountRequest>)Delegate.Remove(this.OnLinkGoogleAccountRequestEvent, (PlayFabRequestEvent<LinkGoogleAccountRequest>)obj158);
					}
				}
			}
			if (this.OnLinkGoogleAccountResultEvent != null)
			{
				Delegate[] invocationList159 = this.OnLinkGoogleAccountResultEvent.GetInvocationList();
				foreach (Delegate obj159 in invocationList159)
				{
					if (object.ReferenceEquals(obj159.Target, instance))
					{
						this.OnLinkGoogleAccountResultEvent = (PlayFabResultEvent<LinkGoogleAccountResult>)Delegate.Remove(this.OnLinkGoogleAccountResultEvent, (PlayFabResultEvent<LinkGoogleAccountResult>)obj159);
					}
				}
			}
			if (this.OnLinkIOSDeviceIDRequestEvent != null)
			{
				Delegate[] invocationList160 = this.OnLinkIOSDeviceIDRequestEvent.GetInvocationList();
				foreach (Delegate obj160 in invocationList160)
				{
					if (object.ReferenceEquals(obj160.Target, instance))
					{
						this.OnLinkIOSDeviceIDRequestEvent = (PlayFabRequestEvent<LinkIOSDeviceIDRequest>)Delegate.Remove(this.OnLinkIOSDeviceIDRequestEvent, (PlayFabRequestEvent<LinkIOSDeviceIDRequest>)obj160);
					}
				}
			}
			if (this.OnLinkIOSDeviceIDResultEvent != null)
			{
				Delegate[] invocationList161 = this.OnLinkIOSDeviceIDResultEvent.GetInvocationList();
				foreach (Delegate obj161 in invocationList161)
				{
					if (object.ReferenceEquals(obj161.Target, instance))
					{
						this.OnLinkIOSDeviceIDResultEvent = (PlayFabResultEvent<LinkIOSDeviceIDResult>)Delegate.Remove(this.OnLinkIOSDeviceIDResultEvent, (PlayFabResultEvent<LinkIOSDeviceIDResult>)obj161);
					}
				}
			}
			if (this.OnLinkKongregateRequestEvent != null)
			{
				Delegate[] invocationList162 = this.OnLinkKongregateRequestEvent.GetInvocationList();
				foreach (Delegate obj162 in invocationList162)
				{
					if (object.ReferenceEquals(obj162.Target, instance))
					{
						this.OnLinkKongregateRequestEvent = (PlayFabRequestEvent<LinkKongregateAccountRequest>)Delegate.Remove(this.OnLinkKongregateRequestEvent, (PlayFabRequestEvent<LinkKongregateAccountRequest>)obj162);
					}
				}
			}
			if (this.OnLinkKongregateResultEvent != null)
			{
				Delegate[] invocationList163 = this.OnLinkKongregateResultEvent.GetInvocationList();
				foreach (Delegate obj163 in invocationList163)
				{
					if (object.ReferenceEquals(obj163.Target, instance))
					{
						this.OnLinkKongregateResultEvent = (PlayFabResultEvent<LinkKongregateAccountResult>)Delegate.Remove(this.OnLinkKongregateResultEvent, (PlayFabResultEvent<LinkKongregateAccountResult>)obj163);
					}
				}
			}
			if (this.OnLinkNintendoServiceAccountRequestEvent != null)
			{
				Delegate[] invocationList164 = this.OnLinkNintendoServiceAccountRequestEvent.GetInvocationList();
				foreach (Delegate obj164 in invocationList164)
				{
					if (object.ReferenceEquals(obj164.Target, instance))
					{
						this.OnLinkNintendoServiceAccountRequestEvent = (PlayFabRequestEvent<LinkNintendoServiceAccountRequest>)Delegate.Remove(this.OnLinkNintendoServiceAccountRequestEvent, (PlayFabRequestEvent<LinkNintendoServiceAccountRequest>)obj164);
					}
				}
			}
			if (this.OnLinkNintendoServiceAccountResultEvent != null)
			{
				Delegate[] invocationList165 = this.OnLinkNintendoServiceAccountResultEvent.GetInvocationList();
				foreach (Delegate obj165 in invocationList165)
				{
					if (object.ReferenceEquals(obj165.Target, instance))
					{
						this.OnLinkNintendoServiceAccountResultEvent = (PlayFabResultEvent<PlayFab.ClientModels.EmptyResult>)Delegate.Remove(this.OnLinkNintendoServiceAccountResultEvent, (PlayFabResultEvent<PlayFab.ClientModels.EmptyResult>)obj165);
					}
				}
			}
			if (this.OnLinkNintendoSwitchDeviceIdRequestEvent != null)
			{
				Delegate[] invocationList166 = this.OnLinkNintendoSwitchDeviceIdRequestEvent.GetInvocationList();
				foreach (Delegate obj166 in invocationList166)
				{
					if (object.ReferenceEquals(obj166.Target, instance))
					{
						this.OnLinkNintendoSwitchDeviceIdRequestEvent = (PlayFabRequestEvent<LinkNintendoSwitchDeviceIdRequest>)Delegate.Remove(this.OnLinkNintendoSwitchDeviceIdRequestEvent, (PlayFabRequestEvent<LinkNintendoSwitchDeviceIdRequest>)obj166);
					}
				}
			}
			if (this.OnLinkNintendoSwitchDeviceIdResultEvent != null)
			{
				Delegate[] invocationList167 = this.OnLinkNintendoSwitchDeviceIdResultEvent.GetInvocationList();
				foreach (Delegate obj167 in invocationList167)
				{
					if (object.ReferenceEquals(obj167.Target, instance))
					{
						this.OnLinkNintendoSwitchDeviceIdResultEvent = (PlayFabResultEvent<LinkNintendoSwitchDeviceIdResult>)Delegate.Remove(this.OnLinkNintendoSwitchDeviceIdResultEvent, (PlayFabResultEvent<LinkNintendoSwitchDeviceIdResult>)obj167);
					}
				}
			}
			if (this.OnLinkOpenIdConnectRequestEvent != null)
			{
				Delegate[] invocationList168 = this.OnLinkOpenIdConnectRequestEvent.GetInvocationList();
				foreach (Delegate obj168 in invocationList168)
				{
					if (object.ReferenceEquals(obj168.Target, instance))
					{
						this.OnLinkOpenIdConnectRequestEvent = (PlayFabRequestEvent<LinkOpenIdConnectRequest>)Delegate.Remove(this.OnLinkOpenIdConnectRequestEvent, (PlayFabRequestEvent<LinkOpenIdConnectRequest>)obj168);
					}
				}
			}
			if (this.OnLinkOpenIdConnectResultEvent != null)
			{
				Delegate[] invocationList169 = this.OnLinkOpenIdConnectResultEvent.GetInvocationList();
				foreach (Delegate obj169 in invocationList169)
				{
					if (object.ReferenceEquals(obj169.Target, instance))
					{
						this.OnLinkOpenIdConnectResultEvent = (PlayFabResultEvent<PlayFab.ClientModels.EmptyResult>)Delegate.Remove(this.OnLinkOpenIdConnectResultEvent, (PlayFabResultEvent<PlayFab.ClientModels.EmptyResult>)obj169);
					}
				}
			}
			if (this.OnLinkPSNAccountRequestEvent != null)
			{
				Delegate[] invocationList170 = this.OnLinkPSNAccountRequestEvent.GetInvocationList();
				foreach (Delegate obj170 in invocationList170)
				{
					if (object.ReferenceEquals(obj170.Target, instance))
					{
						this.OnLinkPSNAccountRequestEvent = (PlayFabRequestEvent<LinkPSNAccountRequest>)Delegate.Remove(this.OnLinkPSNAccountRequestEvent, (PlayFabRequestEvent<LinkPSNAccountRequest>)obj170);
					}
				}
			}
			if (this.OnLinkPSNAccountResultEvent != null)
			{
				Delegate[] invocationList171 = this.OnLinkPSNAccountResultEvent.GetInvocationList();
				foreach (Delegate obj171 in invocationList171)
				{
					if (object.ReferenceEquals(obj171.Target, instance))
					{
						this.OnLinkPSNAccountResultEvent = (PlayFabResultEvent<LinkPSNAccountResult>)Delegate.Remove(this.OnLinkPSNAccountResultEvent, (PlayFabResultEvent<LinkPSNAccountResult>)obj171);
					}
				}
			}
			if (this.OnLinkSteamAccountRequestEvent != null)
			{
				Delegate[] invocationList172 = this.OnLinkSteamAccountRequestEvent.GetInvocationList();
				foreach (Delegate obj172 in invocationList172)
				{
					if (object.ReferenceEquals(obj172.Target, instance))
					{
						this.OnLinkSteamAccountRequestEvent = (PlayFabRequestEvent<LinkSteamAccountRequest>)Delegate.Remove(this.OnLinkSteamAccountRequestEvent, (PlayFabRequestEvent<LinkSteamAccountRequest>)obj172);
					}
				}
			}
			if (this.OnLinkSteamAccountResultEvent != null)
			{
				Delegate[] invocationList173 = this.OnLinkSteamAccountResultEvent.GetInvocationList();
				foreach (Delegate obj173 in invocationList173)
				{
					if (object.ReferenceEquals(obj173.Target, instance))
					{
						this.OnLinkSteamAccountResultEvent = (PlayFabResultEvent<LinkSteamAccountResult>)Delegate.Remove(this.OnLinkSteamAccountResultEvent, (PlayFabResultEvent<LinkSteamAccountResult>)obj173);
					}
				}
			}
			if (this.OnLinkTwitchRequestEvent != null)
			{
				Delegate[] invocationList174 = this.OnLinkTwitchRequestEvent.GetInvocationList();
				foreach (Delegate obj174 in invocationList174)
				{
					if (object.ReferenceEquals(obj174.Target, instance))
					{
						this.OnLinkTwitchRequestEvent = (PlayFabRequestEvent<LinkTwitchAccountRequest>)Delegate.Remove(this.OnLinkTwitchRequestEvent, (PlayFabRequestEvent<LinkTwitchAccountRequest>)obj174);
					}
				}
			}
			if (this.OnLinkTwitchResultEvent != null)
			{
				Delegate[] invocationList175 = this.OnLinkTwitchResultEvent.GetInvocationList();
				foreach (Delegate obj175 in invocationList175)
				{
					if (object.ReferenceEquals(obj175.Target, instance))
					{
						this.OnLinkTwitchResultEvent = (PlayFabResultEvent<LinkTwitchAccountResult>)Delegate.Remove(this.OnLinkTwitchResultEvent, (PlayFabResultEvent<LinkTwitchAccountResult>)obj175);
					}
				}
			}
			if (this.OnLinkWindowsHelloRequestEvent != null)
			{
				Delegate[] invocationList176 = this.OnLinkWindowsHelloRequestEvent.GetInvocationList();
				foreach (Delegate obj176 in invocationList176)
				{
					if (object.ReferenceEquals(obj176.Target, instance))
					{
						this.OnLinkWindowsHelloRequestEvent = (PlayFabRequestEvent<LinkWindowsHelloAccountRequest>)Delegate.Remove(this.OnLinkWindowsHelloRequestEvent, (PlayFabRequestEvent<LinkWindowsHelloAccountRequest>)obj176);
					}
				}
			}
			if (this.OnLinkWindowsHelloResultEvent != null)
			{
				Delegate[] invocationList177 = this.OnLinkWindowsHelloResultEvent.GetInvocationList();
				foreach (Delegate obj177 in invocationList177)
				{
					if (object.ReferenceEquals(obj177.Target, instance))
					{
						this.OnLinkWindowsHelloResultEvent = (PlayFabResultEvent<LinkWindowsHelloAccountResponse>)Delegate.Remove(this.OnLinkWindowsHelloResultEvent, (PlayFabResultEvent<LinkWindowsHelloAccountResponse>)obj177);
					}
				}
			}
			if (this.OnLinkXboxAccountRequestEvent != null)
			{
				Delegate[] invocationList178 = this.OnLinkXboxAccountRequestEvent.GetInvocationList();
				foreach (Delegate obj178 in invocationList178)
				{
					if (object.ReferenceEquals(obj178.Target, instance))
					{
						this.OnLinkXboxAccountRequestEvent = (PlayFabRequestEvent<LinkXboxAccountRequest>)Delegate.Remove(this.OnLinkXboxAccountRequestEvent, (PlayFabRequestEvent<LinkXboxAccountRequest>)obj178);
					}
				}
			}
			if (this.OnLinkXboxAccountResultEvent != null)
			{
				Delegate[] invocationList179 = this.OnLinkXboxAccountResultEvent.GetInvocationList();
				foreach (Delegate obj179 in invocationList179)
				{
					if (object.ReferenceEquals(obj179.Target, instance))
					{
						this.OnLinkXboxAccountResultEvent = (PlayFabResultEvent<LinkXboxAccountResult>)Delegate.Remove(this.OnLinkXboxAccountResultEvent, (PlayFabResultEvent<LinkXboxAccountResult>)obj179);
					}
				}
			}
			if (this.OnLoginWithAndroidDeviceIDRequestEvent != null)
			{
				Delegate[] invocationList180 = this.OnLoginWithAndroidDeviceIDRequestEvent.GetInvocationList();
				foreach (Delegate obj180 in invocationList180)
				{
					if (object.ReferenceEquals(obj180.Target, instance))
					{
						this.OnLoginWithAndroidDeviceIDRequestEvent = (PlayFabRequestEvent<LoginWithAndroidDeviceIDRequest>)Delegate.Remove(this.OnLoginWithAndroidDeviceIDRequestEvent, (PlayFabRequestEvent<LoginWithAndroidDeviceIDRequest>)obj180);
					}
				}
			}
			if (this.OnLoginWithAppleRequestEvent != null)
			{
				Delegate[] invocationList181 = this.OnLoginWithAppleRequestEvent.GetInvocationList();
				foreach (Delegate obj181 in invocationList181)
				{
					if (object.ReferenceEquals(obj181.Target, instance))
					{
						this.OnLoginWithAppleRequestEvent = (PlayFabRequestEvent<LoginWithAppleRequest>)Delegate.Remove(this.OnLoginWithAppleRequestEvent, (PlayFabRequestEvent<LoginWithAppleRequest>)obj181);
					}
				}
			}
			if (this.OnLoginWithCustomIDRequestEvent != null)
			{
				Delegate[] invocationList182 = this.OnLoginWithCustomIDRequestEvent.GetInvocationList();
				foreach (Delegate obj182 in invocationList182)
				{
					if (object.ReferenceEquals(obj182.Target, instance))
					{
						this.OnLoginWithCustomIDRequestEvent = (PlayFabRequestEvent<LoginWithCustomIDRequest>)Delegate.Remove(this.OnLoginWithCustomIDRequestEvent, (PlayFabRequestEvent<LoginWithCustomIDRequest>)obj182);
					}
				}
			}
			if (this.OnLoginWithEmailAddressRequestEvent != null)
			{
				Delegate[] invocationList183 = this.OnLoginWithEmailAddressRequestEvent.GetInvocationList();
				foreach (Delegate obj183 in invocationList183)
				{
					if (object.ReferenceEquals(obj183.Target, instance))
					{
						this.OnLoginWithEmailAddressRequestEvent = (PlayFabRequestEvent<LoginWithEmailAddressRequest>)Delegate.Remove(this.OnLoginWithEmailAddressRequestEvent, (PlayFabRequestEvent<LoginWithEmailAddressRequest>)obj183);
					}
				}
			}
			if (this.OnLoginWithFacebookRequestEvent != null)
			{
				Delegate[] invocationList184 = this.OnLoginWithFacebookRequestEvent.GetInvocationList();
				foreach (Delegate obj184 in invocationList184)
				{
					if (object.ReferenceEquals(obj184.Target, instance))
					{
						this.OnLoginWithFacebookRequestEvent = (PlayFabRequestEvent<LoginWithFacebookRequest>)Delegate.Remove(this.OnLoginWithFacebookRequestEvent, (PlayFabRequestEvent<LoginWithFacebookRequest>)obj184);
					}
				}
			}
			if (this.OnLoginWithFacebookInstantGamesIdRequestEvent != null)
			{
				Delegate[] invocationList185 = this.OnLoginWithFacebookInstantGamesIdRequestEvent.GetInvocationList();
				foreach (Delegate obj185 in invocationList185)
				{
					if (object.ReferenceEquals(obj185.Target, instance))
					{
						this.OnLoginWithFacebookInstantGamesIdRequestEvent = (PlayFabRequestEvent<LoginWithFacebookInstantGamesIdRequest>)Delegate.Remove(this.OnLoginWithFacebookInstantGamesIdRequestEvent, (PlayFabRequestEvent<LoginWithFacebookInstantGamesIdRequest>)obj185);
					}
				}
			}
			if (this.OnLoginWithGameCenterRequestEvent != null)
			{
				Delegate[] invocationList186 = this.OnLoginWithGameCenterRequestEvent.GetInvocationList();
				foreach (Delegate obj186 in invocationList186)
				{
					if (object.ReferenceEquals(obj186.Target, instance))
					{
						this.OnLoginWithGameCenterRequestEvent = (PlayFabRequestEvent<LoginWithGameCenterRequest>)Delegate.Remove(this.OnLoginWithGameCenterRequestEvent, (PlayFabRequestEvent<LoginWithGameCenterRequest>)obj186);
					}
				}
			}
			if (this.OnLoginWithGoogleAccountRequestEvent != null)
			{
				Delegate[] invocationList187 = this.OnLoginWithGoogleAccountRequestEvent.GetInvocationList();
				foreach (Delegate obj187 in invocationList187)
				{
					if (object.ReferenceEquals(obj187.Target, instance))
					{
						this.OnLoginWithGoogleAccountRequestEvent = (PlayFabRequestEvent<LoginWithGoogleAccountRequest>)Delegate.Remove(this.OnLoginWithGoogleAccountRequestEvent, (PlayFabRequestEvent<LoginWithGoogleAccountRequest>)obj187);
					}
				}
			}
			if (this.OnLoginWithIOSDeviceIDRequestEvent != null)
			{
				Delegate[] invocationList188 = this.OnLoginWithIOSDeviceIDRequestEvent.GetInvocationList();
				foreach (Delegate obj188 in invocationList188)
				{
					if (object.ReferenceEquals(obj188.Target, instance))
					{
						this.OnLoginWithIOSDeviceIDRequestEvent = (PlayFabRequestEvent<LoginWithIOSDeviceIDRequest>)Delegate.Remove(this.OnLoginWithIOSDeviceIDRequestEvent, (PlayFabRequestEvent<LoginWithIOSDeviceIDRequest>)obj188);
					}
				}
			}
			if (this.OnLoginWithKongregateRequestEvent != null)
			{
				Delegate[] invocationList189 = this.OnLoginWithKongregateRequestEvent.GetInvocationList();
				foreach (Delegate obj189 in invocationList189)
				{
					if (object.ReferenceEquals(obj189.Target, instance))
					{
						this.OnLoginWithKongregateRequestEvent = (PlayFabRequestEvent<LoginWithKongregateRequest>)Delegate.Remove(this.OnLoginWithKongregateRequestEvent, (PlayFabRequestEvent<LoginWithKongregateRequest>)obj189);
					}
				}
			}
			if (this.OnLoginWithNintendoServiceAccountRequestEvent != null)
			{
				Delegate[] invocationList190 = this.OnLoginWithNintendoServiceAccountRequestEvent.GetInvocationList();
				foreach (Delegate obj190 in invocationList190)
				{
					if (object.ReferenceEquals(obj190.Target, instance))
					{
						this.OnLoginWithNintendoServiceAccountRequestEvent = (PlayFabRequestEvent<LoginWithNintendoServiceAccountRequest>)Delegate.Remove(this.OnLoginWithNintendoServiceAccountRequestEvent, (PlayFabRequestEvent<LoginWithNintendoServiceAccountRequest>)obj190);
					}
				}
			}
			if (this.OnLoginWithNintendoSwitchDeviceIdRequestEvent != null)
			{
				Delegate[] invocationList191 = this.OnLoginWithNintendoSwitchDeviceIdRequestEvent.GetInvocationList();
				foreach (Delegate obj191 in invocationList191)
				{
					if (object.ReferenceEquals(obj191.Target, instance))
					{
						this.OnLoginWithNintendoSwitchDeviceIdRequestEvent = (PlayFabRequestEvent<LoginWithNintendoSwitchDeviceIdRequest>)Delegate.Remove(this.OnLoginWithNintendoSwitchDeviceIdRequestEvent, (PlayFabRequestEvent<LoginWithNintendoSwitchDeviceIdRequest>)obj191);
					}
				}
			}
			if (this.OnLoginWithOpenIdConnectRequestEvent != null)
			{
				Delegate[] invocationList192 = this.OnLoginWithOpenIdConnectRequestEvent.GetInvocationList();
				foreach (Delegate obj192 in invocationList192)
				{
					if (object.ReferenceEquals(obj192.Target, instance))
					{
						this.OnLoginWithOpenIdConnectRequestEvent = (PlayFabRequestEvent<LoginWithOpenIdConnectRequest>)Delegate.Remove(this.OnLoginWithOpenIdConnectRequestEvent, (PlayFabRequestEvent<LoginWithOpenIdConnectRequest>)obj192);
					}
				}
			}
			if (this.OnLoginWithPlayFabRequestEvent != null)
			{
				Delegate[] invocationList193 = this.OnLoginWithPlayFabRequestEvent.GetInvocationList();
				foreach (Delegate obj193 in invocationList193)
				{
					if (object.ReferenceEquals(obj193.Target, instance))
					{
						this.OnLoginWithPlayFabRequestEvent = (PlayFabRequestEvent<LoginWithPlayFabRequest>)Delegate.Remove(this.OnLoginWithPlayFabRequestEvent, (PlayFabRequestEvent<LoginWithPlayFabRequest>)obj193);
					}
				}
			}
			if (this.OnLoginWithPSNRequestEvent != null)
			{
				Delegate[] invocationList194 = this.OnLoginWithPSNRequestEvent.GetInvocationList();
				foreach (Delegate obj194 in invocationList194)
				{
					if (object.ReferenceEquals(obj194.Target, instance))
					{
						this.OnLoginWithPSNRequestEvent = (PlayFabRequestEvent<LoginWithPSNRequest>)Delegate.Remove(this.OnLoginWithPSNRequestEvent, (PlayFabRequestEvent<LoginWithPSNRequest>)obj194);
					}
				}
			}
			if (this.OnLoginWithSteamRequestEvent != null)
			{
				Delegate[] invocationList195 = this.OnLoginWithSteamRequestEvent.GetInvocationList();
				foreach (Delegate obj195 in invocationList195)
				{
					if (object.ReferenceEquals(obj195.Target, instance))
					{
						this.OnLoginWithSteamRequestEvent = (PlayFabRequestEvent<LoginWithSteamRequest>)Delegate.Remove(this.OnLoginWithSteamRequestEvent, (PlayFabRequestEvent<LoginWithSteamRequest>)obj195);
					}
				}
			}
			if (this.OnLoginWithTwitchRequestEvent != null)
			{
				Delegate[] invocationList196 = this.OnLoginWithTwitchRequestEvent.GetInvocationList();
				foreach (Delegate obj196 in invocationList196)
				{
					if (object.ReferenceEquals(obj196.Target, instance))
					{
						this.OnLoginWithTwitchRequestEvent = (PlayFabRequestEvent<LoginWithTwitchRequest>)Delegate.Remove(this.OnLoginWithTwitchRequestEvent, (PlayFabRequestEvent<LoginWithTwitchRequest>)obj196);
					}
				}
			}
			if (this.OnLoginWithWindowsHelloRequestEvent != null)
			{
				Delegate[] invocationList197 = this.OnLoginWithWindowsHelloRequestEvent.GetInvocationList();
				foreach (Delegate obj197 in invocationList197)
				{
					if (object.ReferenceEquals(obj197.Target, instance))
					{
						this.OnLoginWithWindowsHelloRequestEvent = (PlayFabRequestEvent<LoginWithWindowsHelloRequest>)Delegate.Remove(this.OnLoginWithWindowsHelloRequestEvent, (PlayFabRequestEvent<LoginWithWindowsHelloRequest>)obj197);
					}
				}
			}
			if (this.OnLoginWithXboxRequestEvent != null)
			{
				Delegate[] invocationList198 = this.OnLoginWithXboxRequestEvent.GetInvocationList();
				foreach (Delegate obj198 in invocationList198)
				{
					if (object.ReferenceEquals(obj198.Target, instance))
					{
						this.OnLoginWithXboxRequestEvent = (PlayFabRequestEvent<LoginWithXboxRequest>)Delegate.Remove(this.OnLoginWithXboxRequestEvent, (PlayFabRequestEvent<LoginWithXboxRequest>)obj198);
					}
				}
			}
			if (this.OnMatchmakeRequestEvent != null)
			{
				Delegate[] invocationList199 = this.OnMatchmakeRequestEvent.GetInvocationList();
				foreach (Delegate obj199 in invocationList199)
				{
					if (object.ReferenceEquals(obj199.Target, instance))
					{
						this.OnMatchmakeRequestEvent = (PlayFabRequestEvent<MatchmakeRequest>)Delegate.Remove(this.OnMatchmakeRequestEvent, (PlayFabRequestEvent<MatchmakeRequest>)obj199);
					}
				}
			}
			if (this.OnMatchmakeResultEvent != null)
			{
				Delegate[] invocationList200 = this.OnMatchmakeResultEvent.GetInvocationList();
				foreach (Delegate obj200 in invocationList200)
				{
					if (object.ReferenceEquals(obj200.Target, instance))
					{
						this.OnMatchmakeResultEvent = (PlayFabResultEvent<MatchmakeResult>)Delegate.Remove(this.OnMatchmakeResultEvent, (PlayFabResultEvent<MatchmakeResult>)obj200);
					}
				}
			}
			if (this.OnOpenTradeRequestEvent != null)
			{
				Delegate[] invocationList201 = this.OnOpenTradeRequestEvent.GetInvocationList();
				foreach (Delegate obj201 in invocationList201)
				{
					if (object.ReferenceEquals(obj201.Target, instance))
					{
						this.OnOpenTradeRequestEvent = (PlayFabRequestEvent<OpenTradeRequest>)Delegate.Remove(this.OnOpenTradeRequestEvent, (PlayFabRequestEvent<OpenTradeRequest>)obj201);
					}
				}
			}
			if (this.OnOpenTradeResultEvent != null)
			{
				Delegate[] invocationList202 = this.OnOpenTradeResultEvent.GetInvocationList();
				foreach (Delegate obj202 in invocationList202)
				{
					if (object.ReferenceEquals(obj202.Target, instance))
					{
						this.OnOpenTradeResultEvent = (PlayFabResultEvent<OpenTradeResponse>)Delegate.Remove(this.OnOpenTradeResultEvent, (PlayFabResultEvent<OpenTradeResponse>)obj202);
					}
				}
			}
			if (this.OnPayForPurchaseRequestEvent != null)
			{
				Delegate[] invocationList203 = this.OnPayForPurchaseRequestEvent.GetInvocationList();
				foreach (Delegate obj203 in invocationList203)
				{
					if (object.ReferenceEquals(obj203.Target, instance))
					{
						this.OnPayForPurchaseRequestEvent = (PlayFabRequestEvent<PayForPurchaseRequest>)Delegate.Remove(this.OnPayForPurchaseRequestEvent, (PlayFabRequestEvent<PayForPurchaseRequest>)obj203);
					}
				}
			}
			if (this.OnPayForPurchaseResultEvent != null)
			{
				Delegate[] invocationList204 = this.OnPayForPurchaseResultEvent.GetInvocationList();
				foreach (Delegate obj204 in invocationList204)
				{
					if (object.ReferenceEquals(obj204.Target, instance))
					{
						this.OnPayForPurchaseResultEvent = (PlayFabResultEvent<PayForPurchaseResult>)Delegate.Remove(this.OnPayForPurchaseResultEvent, (PlayFabResultEvent<PayForPurchaseResult>)obj204);
					}
				}
			}
			if (this.OnPurchaseItemRequestEvent != null)
			{
				Delegate[] invocationList205 = this.OnPurchaseItemRequestEvent.GetInvocationList();
				foreach (Delegate obj205 in invocationList205)
				{
					if (object.ReferenceEquals(obj205.Target, instance))
					{
						this.OnPurchaseItemRequestEvent = (PlayFabRequestEvent<PurchaseItemRequest>)Delegate.Remove(this.OnPurchaseItemRequestEvent, (PlayFabRequestEvent<PurchaseItemRequest>)obj205);
					}
				}
			}
			if (this.OnPurchaseItemResultEvent != null)
			{
				Delegate[] invocationList206 = this.OnPurchaseItemResultEvent.GetInvocationList();
				foreach (Delegate obj206 in invocationList206)
				{
					if (object.ReferenceEquals(obj206.Target, instance))
					{
						this.OnPurchaseItemResultEvent = (PlayFabResultEvent<PurchaseItemResult>)Delegate.Remove(this.OnPurchaseItemResultEvent, (PlayFabResultEvent<PurchaseItemResult>)obj206);
					}
				}
			}
			if (this.OnRedeemCouponRequestEvent != null)
			{
				Delegate[] invocationList207 = this.OnRedeemCouponRequestEvent.GetInvocationList();
				foreach (Delegate obj207 in invocationList207)
				{
					if (object.ReferenceEquals(obj207.Target, instance))
					{
						this.OnRedeemCouponRequestEvent = (PlayFabRequestEvent<RedeemCouponRequest>)Delegate.Remove(this.OnRedeemCouponRequestEvent, (PlayFabRequestEvent<RedeemCouponRequest>)obj207);
					}
				}
			}
			if (this.OnRedeemCouponResultEvent != null)
			{
				Delegate[] invocationList208 = this.OnRedeemCouponResultEvent.GetInvocationList();
				foreach (Delegate obj208 in invocationList208)
				{
					if (object.ReferenceEquals(obj208.Target, instance))
					{
						this.OnRedeemCouponResultEvent = (PlayFabResultEvent<RedeemCouponResult>)Delegate.Remove(this.OnRedeemCouponResultEvent, (PlayFabResultEvent<RedeemCouponResult>)obj208);
					}
				}
			}
			if (this.OnRefreshPSNAuthTokenRequestEvent != null)
			{
				Delegate[] invocationList209 = this.OnRefreshPSNAuthTokenRequestEvent.GetInvocationList();
				foreach (Delegate obj209 in invocationList209)
				{
					if (object.ReferenceEquals(obj209.Target, instance))
					{
						this.OnRefreshPSNAuthTokenRequestEvent = (PlayFabRequestEvent<RefreshPSNAuthTokenRequest>)Delegate.Remove(this.OnRefreshPSNAuthTokenRequestEvent, (PlayFabRequestEvent<RefreshPSNAuthTokenRequest>)obj209);
					}
				}
			}
			if (this.OnRefreshPSNAuthTokenResultEvent != null)
			{
				Delegate[] invocationList210 = this.OnRefreshPSNAuthTokenResultEvent.GetInvocationList();
				foreach (Delegate obj210 in invocationList210)
				{
					if (object.ReferenceEquals(obj210.Target, instance))
					{
						this.OnRefreshPSNAuthTokenResultEvent = (PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse>)Delegate.Remove(this.OnRefreshPSNAuthTokenResultEvent, (PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse>)obj210);
					}
				}
			}
			if (this.OnRegisterForIOSPushNotificationRequestEvent != null)
			{
				Delegate[] invocationList211 = this.OnRegisterForIOSPushNotificationRequestEvent.GetInvocationList();
				foreach (Delegate obj211 in invocationList211)
				{
					if (object.ReferenceEquals(obj211.Target, instance))
					{
						this.OnRegisterForIOSPushNotificationRequestEvent = (PlayFabRequestEvent<RegisterForIOSPushNotificationRequest>)Delegate.Remove(this.OnRegisterForIOSPushNotificationRequestEvent, (PlayFabRequestEvent<RegisterForIOSPushNotificationRequest>)obj211);
					}
				}
			}
			if (this.OnRegisterForIOSPushNotificationResultEvent != null)
			{
				Delegate[] invocationList212 = this.OnRegisterForIOSPushNotificationResultEvent.GetInvocationList();
				foreach (Delegate obj212 in invocationList212)
				{
					if (object.ReferenceEquals(obj212.Target, instance))
					{
						this.OnRegisterForIOSPushNotificationResultEvent = (PlayFabResultEvent<RegisterForIOSPushNotificationResult>)Delegate.Remove(this.OnRegisterForIOSPushNotificationResultEvent, (PlayFabResultEvent<RegisterForIOSPushNotificationResult>)obj212);
					}
				}
			}
			if (this.OnRegisterPlayFabUserRequestEvent != null)
			{
				Delegate[] invocationList213 = this.OnRegisterPlayFabUserRequestEvent.GetInvocationList();
				foreach (Delegate obj213 in invocationList213)
				{
					if (object.ReferenceEquals(obj213.Target, instance))
					{
						this.OnRegisterPlayFabUserRequestEvent = (PlayFabRequestEvent<RegisterPlayFabUserRequest>)Delegate.Remove(this.OnRegisterPlayFabUserRequestEvent, (PlayFabRequestEvent<RegisterPlayFabUserRequest>)obj213);
					}
				}
			}
			if (this.OnRegisterPlayFabUserResultEvent != null)
			{
				Delegate[] invocationList214 = this.OnRegisterPlayFabUserResultEvent.GetInvocationList();
				foreach (Delegate obj214 in invocationList214)
				{
					if (object.ReferenceEquals(obj214.Target, instance))
					{
						this.OnRegisterPlayFabUserResultEvent = (PlayFabResultEvent<RegisterPlayFabUserResult>)Delegate.Remove(this.OnRegisterPlayFabUserResultEvent, (PlayFabResultEvent<RegisterPlayFabUserResult>)obj214);
					}
				}
			}
			if (this.OnRegisterWithWindowsHelloRequestEvent != null)
			{
				Delegate[] invocationList215 = this.OnRegisterWithWindowsHelloRequestEvent.GetInvocationList();
				foreach (Delegate obj215 in invocationList215)
				{
					if (object.ReferenceEquals(obj215.Target, instance))
					{
						this.OnRegisterWithWindowsHelloRequestEvent = (PlayFabRequestEvent<RegisterWithWindowsHelloRequest>)Delegate.Remove(this.OnRegisterWithWindowsHelloRequestEvent, (PlayFabRequestEvent<RegisterWithWindowsHelloRequest>)obj215);
					}
				}
			}
			if (this.OnRemoveContactEmailRequestEvent != null)
			{
				Delegate[] invocationList216 = this.OnRemoveContactEmailRequestEvent.GetInvocationList();
				foreach (Delegate obj216 in invocationList216)
				{
					if (object.ReferenceEquals(obj216.Target, instance))
					{
						this.OnRemoveContactEmailRequestEvent = (PlayFabRequestEvent<RemoveContactEmailRequest>)Delegate.Remove(this.OnRemoveContactEmailRequestEvent, (PlayFabRequestEvent<RemoveContactEmailRequest>)obj216);
					}
				}
			}
			if (this.OnRemoveContactEmailResultEvent != null)
			{
				Delegate[] invocationList217 = this.OnRemoveContactEmailResultEvent.GetInvocationList();
				foreach (Delegate obj217 in invocationList217)
				{
					if (object.ReferenceEquals(obj217.Target, instance))
					{
						this.OnRemoveContactEmailResultEvent = (PlayFabResultEvent<RemoveContactEmailResult>)Delegate.Remove(this.OnRemoveContactEmailResultEvent, (PlayFabResultEvent<RemoveContactEmailResult>)obj217);
					}
				}
			}
			if (this.OnRemoveFriendRequestEvent != null)
			{
				Delegate[] invocationList218 = this.OnRemoveFriendRequestEvent.GetInvocationList();
				foreach (Delegate obj218 in invocationList218)
				{
					if (object.ReferenceEquals(obj218.Target, instance))
					{
						this.OnRemoveFriendRequestEvent = (PlayFabRequestEvent<RemoveFriendRequest>)Delegate.Remove(this.OnRemoveFriendRequestEvent, (PlayFabRequestEvent<RemoveFriendRequest>)obj218);
					}
				}
			}
			if (this.OnRemoveFriendResultEvent != null)
			{
				Delegate[] invocationList219 = this.OnRemoveFriendResultEvent.GetInvocationList();
				foreach (Delegate obj219 in invocationList219)
				{
					if (object.ReferenceEquals(obj219.Target, instance))
					{
						this.OnRemoveFriendResultEvent = (PlayFabResultEvent<RemoveFriendResult>)Delegate.Remove(this.OnRemoveFriendResultEvent, (PlayFabResultEvent<RemoveFriendResult>)obj219);
					}
				}
			}
			if (this.OnRemoveGenericIDRequestEvent != null)
			{
				Delegate[] invocationList220 = this.OnRemoveGenericIDRequestEvent.GetInvocationList();
				foreach (Delegate obj220 in invocationList220)
				{
					if (object.ReferenceEquals(obj220.Target, instance))
					{
						this.OnRemoveGenericIDRequestEvent = (PlayFabRequestEvent<RemoveGenericIDRequest>)Delegate.Remove(this.OnRemoveGenericIDRequestEvent, (PlayFabRequestEvent<RemoveGenericIDRequest>)obj220);
					}
				}
			}
			if (this.OnRemoveGenericIDResultEvent != null)
			{
				Delegate[] invocationList221 = this.OnRemoveGenericIDResultEvent.GetInvocationList();
				foreach (Delegate obj221 in invocationList221)
				{
					if (object.ReferenceEquals(obj221.Target, instance))
					{
						this.OnRemoveGenericIDResultEvent = (PlayFabResultEvent<RemoveGenericIDResult>)Delegate.Remove(this.OnRemoveGenericIDResultEvent, (PlayFabResultEvent<RemoveGenericIDResult>)obj221);
					}
				}
			}
			if (this.OnRemoveSharedGroupMembersRequestEvent != null)
			{
				Delegate[] invocationList222 = this.OnRemoveSharedGroupMembersRequestEvent.GetInvocationList();
				foreach (Delegate obj222 in invocationList222)
				{
					if (object.ReferenceEquals(obj222.Target, instance))
					{
						this.OnRemoveSharedGroupMembersRequestEvent = (PlayFabRequestEvent<RemoveSharedGroupMembersRequest>)Delegate.Remove(this.OnRemoveSharedGroupMembersRequestEvent, (PlayFabRequestEvent<RemoveSharedGroupMembersRequest>)obj222);
					}
				}
			}
			if (this.OnRemoveSharedGroupMembersResultEvent != null)
			{
				Delegate[] invocationList223 = this.OnRemoveSharedGroupMembersResultEvent.GetInvocationList();
				foreach (Delegate obj223 in invocationList223)
				{
					if (object.ReferenceEquals(obj223.Target, instance))
					{
						this.OnRemoveSharedGroupMembersResultEvent = (PlayFabResultEvent<RemoveSharedGroupMembersResult>)Delegate.Remove(this.OnRemoveSharedGroupMembersResultEvent, (PlayFabResultEvent<RemoveSharedGroupMembersResult>)obj223);
					}
				}
			}
			if (this.OnReportAdActivityRequestEvent != null)
			{
				Delegate[] invocationList224 = this.OnReportAdActivityRequestEvent.GetInvocationList();
				foreach (Delegate obj224 in invocationList224)
				{
					if (object.ReferenceEquals(obj224.Target, instance))
					{
						this.OnReportAdActivityRequestEvent = (PlayFabRequestEvent<ReportAdActivityRequest>)Delegate.Remove(this.OnReportAdActivityRequestEvent, (PlayFabRequestEvent<ReportAdActivityRequest>)obj224);
					}
				}
			}
			if (this.OnReportAdActivityResultEvent != null)
			{
				Delegate[] invocationList225 = this.OnReportAdActivityResultEvent.GetInvocationList();
				foreach (Delegate obj225 in invocationList225)
				{
					if (object.ReferenceEquals(obj225.Target, instance))
					{
						this.OnReportAdActivityResultEvent = (PlayFabResultEvent<ReportAdActivityResult>)Delegate.Remove(this.OnReportAdActivityResultEvent, (PlayFabResultEvent<ReportAdActivityResult>)obj225);
					}
				}
			}
			if (this.OnReportDeviceInfoRequestEvent != null)
			{
				Delegate[] invocationList226 = this.OnReportDeviceInfoRequestEvent.GetInvocationList();
				foreach (Delegate obj226 in invocationList226)
				{
					if (object.ReferenceEquals(obj226.Target, instance))
					{
						this.OnReportDeviceInfoRequestEvent = (PlayFabRequestEvent<DeviceInfoRequest>)Delegate.Remove(this.OnReportDeviceInfoRequestEvent, (PlayFabRequestEvent<DeviceInfoRequest>)obj226);
					}
				}
			}
			if (this.OnReportDeviceInfoResultEvent != null)
			{
				Delegate[] invocationList227 = this.OnReportDeviceInfoResultEvent.GetInvocationList();
				foreach (Delegate obj227 in invocationList227)
				{
					if (object.ReferenceEquals(obj227.Target, instance))
					{
						this.OnReportDeviceInfoResultEvent = (PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse>)Delegate.Remove(this.OnReportDeviceInfoResultEvent, (PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse>)obj227);
					}
				}
			}
			if (this.OnReportPlayerRequestEvent != null)
			{
				Delegate[] invocationList228 = this.OnReportPlayerRequestEvent.GetInvocationList();
				foreach (Delegate obj228 in invocationList228)
				{
					if (object.ReferenceEquals(obj228.Target, instance))
					{
						this.OnReportPlayerRequestEvent = (PlayFabRequestEvent<ReportPlayerClientRequest>)Delegate.Remove(this.OnReportPlayerRequestEvent, (PlayFabRequestEvent<ReportPlayerClientRequest>)obj228);
					}
				}
			}
			if (this.OnReportPlayerResultEvent != null)
			{
				Delegate[] invocationList229 = this.OnReportPlayerResultEvent.GetInvocationList();
				foreach (Delegate obj229 in invocationList229)
				{
					if (object.ReferenceEquals(obj229.Target, instance))
					{
						this.OnReportPlayerResultEvent = (PlayFabResultEvent<ReportPlayerClientResult>)Delegate.Remove(this.OnReportPlayerResultEvent, (PlayFabResultEvent<ReportPlayerClientResult>)obj229);
					}
				}
			}
			if (this.OnRestoreIOSPurchasesRequestEvent != null)
			{
				Delegate[] invocationList230 = this.OnRestoreIOSPurchasesRequestEvent.GetInvocationList();
				foreach (Delegate obj230 in invocationList230)
				{
					if (object.ReferenceEquals(obj230.Target, instance))
					{
						this.OnRestoreIOSPurchasesRequestEvent = (PlayFabRequestEvent<RestoreIOSPurchasesRequest>)Delegate.Remove(this.OnRestoreIOSPurchasesRequestEvent, (PlayFabRequestEvent<RestoreIOSPurchasesRequest>)obj230);
					}
				}
			}
			if (this.OnRestoreIOSPurchasesResultEvent != null)
			{
				Delegate[] invocationList231 = this.OnRestoreIOSPurchasesResultEvent.GetInvocationList();
				foreach (Delegate obj231 in invocationList231)
				{
					if (object.ReferenceEquals(obj231.Target, instance))
					{
						this.OnRestoreIOSPurchasesResultEvent = (PlayFabResultEvent<RestoreIOSPurchasesResult>)Delegate.Remove(this.OnRestoreIOSPurchasesResultEvent, (PlayFabResultEvent<RestoreIOSPurchasesResult>)obj231);
					}
				}
			}
			if (this.OnRewardAdActivityRequestEvent != null)
			{
				Delegate[] invocationList232 = this.OnRewardAdActivityRequestEvent.GetInvocationList();
				foreach (Delegate obj232 in invocationList232)
				{
					if (object.ReferenceEquals(obj232.Target, instance))
					{
						this.OnRewardAdActivityRequestEvent = (PlayFabRequestEvent<RewardAdActivityRequest>)Delegate.Remove(this.OnRewardAdActivityRequestEvent, (PlayFabRequestEvent<RewardAdActivityRequest>)obj232);
					}
				}
			}
			if (this.OnRewardAdActivityResultEvent != null)
			{
				Delegate[] invocationList233 = this.OnRewardAdActivityResultEvent.GetInvocationList();
				foreach (Delegate obj233 in invocationList233)
				{
					if (object.ReferenceEquals(obj233.Target, instance))
					{
						this.OnRewardAdActivityResultEvent = (PlayFabResultEvent<RewardAdActivityResult>)Delegate.Remove(this.OnRewardAdActivityResultEvent, (PlayFabResultEvent<RewardAdActivityResult>)obj233);
					}
				}
			}
			if (this.OnSendAccountRecoveryEmailRequestEvent != null)
			{
				Delegate[] invocationList234 = this.OnSendAccountRecoveryEmailRequestEvent.GetInvocationList();
				foreach (Delegate obj234 in invocationList234)
				{
					if (object.ReferenceEquals(obj234.Target, instance))
					{
						this.OnSendAccountRecoveryEmailRequestEvent = (PlayFabRequestEvent<SendAccountRecoveryEmailRequest>)Delegate.Remove(this.OnSendAccountRecoveryEmailRequestEvent, (PlayFabRequestEvent<SendAccountRecoveryEmailRequest>)obj234);
					}
				}
			}
			if (this.OnSendAccountRecoveryEmailResultEvent != null)
			{
				Delegate[] invocationList235 = this.OnSendAccountRecoveryEmailResultEvent.GetInvocationList();
				foreach (Delegate obj235 in invocationList235)
				{
					if (object.ReferenceEquals(obj235.Target, instance))
					{
						this.OnSendAccountRecoveryEmailResultEvent = (PlayFabResultEvent<SendAccountRecoveryEmailResult>)Delegate.Remove(this.OnSendAccountRecoveryEmailResultEvent, (PlayFabResultEvent<SendAccountRecoveryEmailResult>)obj235);
					}
				}
			}
			if (this.OnSetFriendTagsRequestEvent != null)
			{
				Delegate[] invocationList236 = this.OnSetFriendTagsRequestEvent.GetInvocationList();
				foreach (Delegate obj236 in invocationList236)
				{
					if (object.ReferenceEquals(obj236.Target, instance))
					{
						this.OnSetFriendTagsRequestEvent = (PlayFabRequestEvent<SetFriendTagsRequest>)Delegate.Remove(this.OnSetFriendTagsRequestEvent, (PlayFabRequestEvent<SetFriendTagsRequest>)obj236);
					}
				}
			}
			if (this.OnSetFriendTagsResultEvent != null)
			{
				Delegate[] invocationList237 = this.OnSetFriendTagsResultEvent.GetInvocationList();
				foreach (Delegate obj237 in invocationList237)
				{
					if (object.ReferenceEquals(obj237.Target, instance))
					{
						this.OnSetFriendTagsResultEvent = (PlayFabResultEvent<SetFriendTagsResult>)Delegate.Remove(this.OnSetFriendTagsResultEvent, (PlayFabResultEvent<SetFriendTagsResult>)obj237);
					}
				}
			}
			if (this.OnSetPlayerSecretRequestEvent != null)
			{
				Delegate[] invocationList238 = this.OnSetPlayerSecretRequestEvent.GetInvocationList();
				foreach (Delegate obj238 in invocationList238)
				{
					if (object.ReferenceEquals(obj238.Target, instance))
					{
						this.OnSetPlayerSecretRequestEvent = (PlayFabRequestEvent<SetPlayerSecretRequest>)Delegate.Remove(this.OnSetPlayerSecretRequestEvent, (PlayFabRequestEvent<SetPlayerSecretRequest>)obj238);
					}
				}
			}
			if (this.OnSetPlayerSecretResultEvent != null)
			{
				Delegate[] invocationList239 = this.OnSetPlayerSecretResultEvent.GetInvocationList();
				foreach (Delegate obj239 in invocationList239)
				{
					if (object.ReferenceEquals(obj239.Target, instance))
					{
						this.OnSetPlayerSecretResultEvent = (PlayFabResultEvent<SetPlayerSecretResult>)Delegate.Remove(this.OnSetPlayerSecretResultEvent, (PlayFabResultEvent<SetPlayerSecretResult>)obj239);
					}
				}
			}
			if (this.OnStartGameRequestEvent != null)
			{
				Delegate[] invocationList240 = this.OnStartGameRequestEvent.GetInvocationList();
				foreach (Delegate obj240 in invocationList240)
				{
					if (object.ReferenceEquals(obj240.Target, instance))
					{
						this.OnStartGameRequestEvent = (PlayFabRequestEvent<StartGameRequest>)Delegate.Remove(this.OnStartGameRequestEvent, (PlayFabRequestEvent<StartGameRequest>)obj240);
					}
				}
			}
			if (this.OnStartGameResultEvent != null)
			{
				Delegate[] invocationList241 = this.OnStartGameResultEvent.GetInvocationList();
				foreach (Delegate obj241 in invocationList241)
				{
					if (object.ReferenceEquals(obj241.Target, instance))
					{
						this.OnStartGameResultEvent = (PlayFabResultEvent<StartGameResult>)Delegate.Remove(this.OnStartGameResultEvent, (PlayFabResultEvent<StartGameResult>)obj241);
					}
				}
			}
			if (this.OnStartPurchaseRequestEvent != null)
			{
				Delegate[] invocationList242 = this.OnStartPurchaseRequestEvent.GetInvocationList();
				foreach (Delegate obj242 in invocationList242)
				{
					if (object.ReferenceEquals(obj242.Target, instance))
					{
						this.OnStartPurchaseRequestEvent = (PlayFabRequestEvent<StartPurchaseRequest>)Delegate.Remove(this.OnStartPurchaseRequestEvent, (PlayFabRequestEvent<StartPurchaseRequest>)obj242);
					}
				}
			}
			if (this.OnStartPurchaseResultEvent != null)
			{
				Delegate[] invocationList243 = this.OnStartPurchaseResultEvent.GetInvocationList();
				foreach (Delegate obj243 in invocationList243)
				{
					if (object.ReferenceEquals(obj243.Target, instance))
					{
						this.OnStartPurchaseResultEvent = (PlayFabResultEvent<StartPurchaseResult>)Delegate.Remove(this.OnStartPurchaseResultEvent, (PlayFabResultEvent<StartPurchaseResult>)obj243);
					}
				}
			}
			if (this.OnSubtractUserVirtualCurrencyRequestEvent != null)
			{
				Delegate[] invocationList244 = this.OnSubtractUserVirtualCurrencyRequestEvent.GetInvocationList();
				foreach (Delegate obj244 in invocationList244)
				{
					if (object.ReferenceEquals(obj244.Target, instance))
					{
						this.OnSubtractUserVirtualCurrencyRequestEvent = (PlayFabRequestEvent<SubtractUserVirtualCurrencyRequest>)Delegate.Remove(this.OnSubtractUserVirtualCurrencyRequestEvent, (PlayFabRequestEvent<SubtractUserVirtualCurrencyRequest>)obj244);
					}
				}
			}
			if (this.OnSubtractUserVirtualCurrencyResultEvent != null)
			{
				Delegate[] invocationList245 = this.OnSubtractUserVirtualCurrencyResultEvent.GetInvocationList();
				foreach (Delegate obj245 in invocationList245)
				{
					if (object.ReferenceEquals(obj245.Target, instance))
					{
						this.OnSubtractUserVirtualCurrencyResultEvent = (PlayFabResultEvent<ModifyUserVirtualCurrencyResult>)Delegate.Remove(this.OnSubtractUserVirtualCurrencyResultEvent, (PlayFabResultEvent<ModifyUserVirtualCurrencyResult>)obj245);
					}
				}
			}
			if (this.OnUnlinkAndroidDeviceIDRequestEvent != null)
			{
				Delegate[] invocationList246 = this.OnUnlinkAndroidDeviceIDRequestEvent.GetInvocationList();
				foreach (Delegate obj246 in invocationList246)
				{
					if (object.ReferenceEquals(obj246.Target, instance))
					{
						this.OnUnlinkAndroidDeviceIDRequestEvent = (PlayFabRequestEvent<UnlinkAndroidDeviceIDRequest>)Delegate.Remove(this.OnUnlinkAndroidDeviceIDRequestEvent, (PlayFabRequestEvent<UnlinkAndroidDeviceIDRequest>)obj246);
					}
				}
			}
			if (this.OnUnlinkAndroidDeviceIDResultEvent != null)
			{
				Delegate[] invocationList247 = this.OnUnlinkAndroidDeviceIDResultEvent.GetInvocationList();
				foreach (Delegate obj247 in invocationList247)
				{
					if (object.ReferenceEquals(obj247.Target, instance))
					{
						this.OnUnlinkAndroidDeviceIDResultEvent = (PlayFabResultEvent<UnlinkAndroidDeviceIDResult>)Delegate.Remove(this.OnUnlinkAndroidDeviceIDResultEvent, (PlayFabResultEvent<UnlinkAndroidDeviceIDResult>)obj247);
					}
				}
			}
			if (this.OnUnlinkAppleRequestEvent != null)
			{
				Delegate[] invocationList248 = this.OnUnlinkAppleRequestEvent.GetInvocationList();
				foreach (Delegate obj248 in invocationList248)
				{
					if (object.ReferenceEquals(obj248.Target, instance))
					{
						this.OnUnlinkAppleRequestEvent = (PlayFabRequestEvent<UnlinkAppleRequest>)Delegate.Remove(this.OnUnlinkAppleRequestEvent, (PlayFabRequestEvent<UnlinkAppleRequest>)obj248);
					}
				}
			}
			if (this.OnUnlinkAppleResultEvent != null)
			{
				Delegate[] invocationList249 = this.OnUnlinkAppleResultEvent.GetInvocationList();
				foreach (Delegate obj249 in invocationList249)
				{
					if (object.ReferenceEquals(obj249.Target, instance))
					{
						this.OnUnlinkAppleResultEvent = (PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse>)Delegate.Remove(this.OnUnlinkAppleResultEvent, (PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse>)obj249);
					}
				}
			}
			if (this.OnUnlinkCustomIDRequestEvent != null)
			{
				Delegate[] invocationList250 = this.OnUnlinkCustomIDRequestEvent.GetInvocationList();
				foreach (Delegate obj250 in invocationList250)
				{
					if (object.ReferenceEquals(obj250.Target, instance))
					{
						this.OnUnlinkCustomIDRequestEvent = (PlayFabRequestEvent<UnlinkCustomIDRequest>)Delegate.Remove(this.OnUnlinkCustomIDRequestEvent, (PlayFabRequestEvent<UnlinkCustomIDRequest>)obj250);
					}
				}
			}
			if (this.OnUnlinkCustomIDResultEvent != null)
			{
				Delegate[] invocationList251 = this.OnUnlinkCustomIDResultEvent.GetInvocationList();
				foreach (Delegate obj251 in invocationList251)
				{
					if (object.ReferenceEquals(obj251.Target, instance))
					{
						this.OnUnlinkCustomIDResultEvent = (PlayFabResultEvent<UnlinkCustomIDResult>)Delegate.Remove(this.OnUnlinkCustomIDResultEvent, (PlayFabResultEvent<UnlinkCustomIDResult>)obj251);
					}
				}
			}
			if (this.OnUnlinkFacebookAccountRequestEvent != null)
			{
				Delegate[] invocationList252 = this.OnUnlinkFacebookAccountRequestEvent.GetInvocationList();
				foreach (Delegate obj252 in invocationList252)
				{
					if (object.ReferenceEquals(obj252.Target, instance))
					{
						this.OnUnlinkFacebookAccountRequestEvent = (PlayFabRequestEvent<UnlinkFacebookAccountRequest>)Delegate.Remove(this.OnUnlinkFacebookAccountRequestEvent, (PlayFabRequestEvent<UnlinkFacebookAccountRequest>)obj252);
					}
				}
			}
			if (this.OnUnlinkFacebookAccountResultEvent != null)
			{
				Delegate[] invocationList253 = this.OnUnlinkFacebookAccountResultEvent.GetInvocationList();
				foreach (Delegate obj253 in invocationList253)
				{
					if (object.ReferenceEquals(obj253.Target, instance))
					{
						this.OnUnlinkFacebookAccountResultEvent = (PlayFabResultEvent<UnlinkFacebookAccountResult>)Delegate.Remove(this.OnUnlinkFacebookAccountResultEvent, (PlayFabResultEvent<UnlinkFacebookAccountResult>)obj253);
					}
				}
			}
			if (this.OnUnlinkFacebookInstantGamesIdRequestEvent != null)
			{
				Delegate[] invocationList254 = this.OnUnlinkFacebookInstantGamesIdRequestEvent.GetInvocationList();
				foreach (Delegate obj254 in invocationList254)
				{
					if (object.ReferenceEquals(obj254.Target, instance))
					{
						this.OnUnlinkFacebookInstantGamesIdRequestEvent = (PlayFabRequestEvent<UnlinkFacebookInstantGamesIdRequest>)Delegate.Remove(this.OnUnlinkFacebookInstantGamesIdRequestEvent, (PlayFabRequestEvent<UnlinkFacebookInstantGamesIdRequest>)obj254);
					}
				}
			}
			if (this.OnUnlinkFacebookInstantGamesIdResultEvent != null)
			{
				Delegate[] invocationList255 = this.OnUnlinkFacebookInstantGamesIdResultEvent.GetInvocationList();
				foreach (Delegate obj255 in invocationList255)
				{
					if (object.ReferenceEquals(obj255.Target, instance))
					{
						this.OnUnlinkFacebookInstantGamesIdResultEvent = (PlayFabResultEvent<UnlinkFacebookInstantGamesIdResult>)Delegate.Remove(this.OnUnlinkFacebookInstantGamesIdResultEvent, (PlayFabResultEvent<UnlinkFacebookInstantGamesIdResult>)obj255);
					}
				}
			}
			if (this.OnUnlinkGameCenterAccountRequestEvent != null)
			{
				Delegate[] invocationList256 = this.OnUnlinkGameCenterAccountRequestEvent.GetInvocationList();
				foreach (Delegate obj256 in invocationList256)
				{
					if (object.ReferenceEquals(obj256.Target, instance))
					{
						this.OnUnlinkGameCenterAccountRequestEvent = (PlayFabRequestEvent<UnlinkGameCenterAccountRequest>)Delegate.Remove(this.OnUnlinkGameCenterAccountRequestEvent, (PlayFabRequestEvent<UnlinkGameCenterAccountRequest>)obj256);
					}
				}
			}
			if (this.OnUnlinkGameCenterAccountResultEvent != null)
			{
				Delegate[] invocationList257 = this.OnUnlinkGameCenterAccountResultEvent.GetInvocationList();
				foreach (Delegate obj257 in invocationList257)
				{
					if (object.ReferenceEquals(obj257.Target, instance))
					{
						this.OnUnlinkGameCenterAccountResultEvent = (PlayFabResultEvent<UnlinkGameCenterAccountResult>)Delegate.Remove(this.OnUnlinkGameCenterAccountResultEvent, (PlayFabResultEvent<UnlinkGameCenterAccountResult>)obj257);
					}
				}
			}
			if (this.OnUnlinkGoogleAccountRequestEvent != null)
			{
				Delegate[] invocationList258 = this.OnUnlinkGoogleAccountRequestEvent.GetInvocationList();
				foreach (Delegate obj258 in invocationList258)
				{
					if (object.ReferenceEquals(obj258.Target, instance))
					{
						this.OnUnlinkGoogleAccountRequestEvent = (PlayFabRequestEvent<UnlinkGoogleAccountRequest>)Delegate.Remove(this.OnUnlinkGoogleAccountRequestEvent, (PlayFabRequestEvent<UnlinkGoogleAccountRequest>)obj258);
					}
				}
			}
			if (this.OnUnlinkGoogleAccountResultEvent != null)
			{
				Delegate[] invocationList259 = this.OnUnlinkGoogleAccountResultEvent.GetInvocationList();
				foreach (Delegate obj259 in invocationList259)
				{
					if (object.ReferenceEquals(obj259.Target, instance))
					{
						this.OnUnlinkGoogleAccountResultEvent = (PlayFabResultEvent<UnlinkGoogleAccountResult>)Delegate.Remove(this.OnUnlinkGoogleAccountResultEvent, (PlayFabResultEvent<UnlinkGoogleAccountResult>)obj259);
					}
				}
			}
			if (this.OnUnlinkIOSDeviceIDRequestEvent != null)
			{
				Delegate[] invocationList260 = this.OnUnlinkIOSDeviceIDRequestEvent.GetInvocationList();
				foreach (Delegate obj260 in invocationList260)
				{
					if (object.ReferenceEquals(obj260.Target, instance))
					{
						this.OnUnlinkIOSDeviceIDRequestEvent = (PlayFabRequestEvent<UnlinkIOSDeviceIDRequest>)Delegate.Remove(this.OnUnlinkIOSDeviceIDRequestEvent, (PlayFabRequestEvent<UnlinkIOSDeviceIDRequest>)obj260);
					}
				}
			}
			if (this.OnUnlinkIOSDeviceIDResultEvent != null)
			{
				Delegate[] invocationList261 = this.OnUnlinkIOSDeviceIDResultEvent.GetInvocationList();
				foreach (Delegate obj261 in invocationList261)
				{
					if (object.ReferenceEquals(obj261.Target, instance))
					{
						this.OnUnlinkIOSDeviceIDResultEvent = (PlayFabResultEvent<UnlinkIOSDeviceIDResult>)Delegate.Remove(this.OnUnlinkIOSDeviceIDResultEvent, (PlayFabResultEvent<UnlinkIOSDeviceIDResult>)obj261);
					}
				}
			}
			if (this.OnUnlinkKongregateRequestEvent != null)
			{
				Delegate[] invocationList262 = this.OnUnlinkKongregateRequestEvent.GetInvocationList();
				foreach (Delegate obj262 in invocationList262)
				{
					if (object.ReferenceEquals(obj262.Target, instance))
					{
						this.OnUnlinkKongregateRequestEvent = (PlayFabRequestEvent<UnlinkKongregateAccountRequest>)Delegate.Remove(this.OnUnlinkKongregateRequestEvent, (PlayFabRequestEvent<UnlinkKongregateAccountRequest>)obj262);
					}
				}
			}
			if (this.OnUnlinkKongregateResultEvent != null)
			{
				Delegate[] invocationList263 = this.OnUnlinkKongregateResultEvent.GetInvocationList();
				foreach (Delegate obj263 in invocationList263)
				{
					if (object.ReferenceEquals(obj263.Target, instance))
					{
						this.OnUnlinkKongregateResultEvent = (PlayFabResultEvent<UnlinkKongregateAccountResult>)Delegate.Remove(this.OnUnlinkKongregateResultEvent, (PlayFabResultEvent<UnlinkKongregateAccountResult>)obj263);
					}
				}
			}
			if (this.OnUnlinkNintendoServiceAccountRequestEvent != null)
			{
				Delegate[] invocationList264 = this.OnUnlinkNintendoServiceAccountRequestEvent.GetInvocationList();
				foreach (Delegate obj264 in invocationList264)
				{
					if (object.ReferenceEquals(obj264.Target, instance))
					{
						this.OnUnlinkNintendoServiceAccountRequestEvent = (PlayFabRequestEvent<UnlinkNintendoServiceAccountRequest>)Delegate.Remove(this.OnUnlinkNintendoServiceAccountRequestEvent, (PlayFabRequestEvent<UnlinkNintendoServiceAccountRequest>)obj264);
					}
				}
			}
			if (this.OnUnlinkNintendoServiceAccountResultEvent != null)
			{
				Delegate[] invocationList265 = this.OnUnlinkNintendoServiceAccountResultEvent.GetInvocationList();
				foreach (Delegate obj265 in invocationList265)
				{
					if (object.ReferenceEquals(obj265.Target, instance))
					{
						this.OnUnlinkNintendoServiceAccountResultEvent = (PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse>)Delegate.Remove(this.OnUnlinkNintendoServiceAccountResultEvent, (PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse>)obj265);
					}
				}
			}
			if (this.OnUnlinkNintendoSwitchDeviceIdRequestEvent != null)
			{
				Delegate[] invocationList266 = this.OnUnlinkNintendoSwitchDeviceIdRequestEvent.GetInvocationList();
				foreach (Delegate obj266 in invocationList266)
				{
					if (object.ReferenceEquals(obj266.Target, instance))
					{
						this.OnUnlinkNintendoSwitchDeviceIdRequestEvent = (PlayFabRequestEvent<UnlinkNintendoSwitchDeviceIdRequest>)Delegate.Remove(this.OnUnlinkNintendoSwitchDeviceIdRequestEvent, (PlayFabRequestEvent<UnlinkNintendoSwitchDeviceIdRequest>)obj266);
					}
				}
			}
			if (this.OnUnlinkNintendoSwitchDeviceIdResultEvent != null)
			{
				Delegate[] invocationList267 = this.OnUnlinkNintendoSwitchDeviceIdResultEvent.GetInvocationList();
				foreach (Delegate obj267 in invocationList267)
				{
					if (object.ReferenceEquals(obj267.Target, instance))
					{
						this.OnUnlinkNintendoSwitchDeviceIdResultEvent = (PlayFabResultEvent<UnlinkNintendoSwitchDeviceIdResult>)Delegate.Remove(this.OnUnlinkNintendoSwitchDeviceIdResultEvent, (PlayFabResultEvent<UnlinkNintendoSwitchDeviceIdResult>)obj267);
					}
				}
			}
			if (this.OnUnlinkOpenIdConnectRequestEvent != null)
			{
				Delegate[] invocationList268 = this.OnUnlinkOpenIdConnectRequestEvent.GetInvocationList();
				foreach (Delegate obj268 in invocationList268)
				{
					if (object.ReferenceEquals(obj268.Target, instance))
					{
						this.OnUnlinkOpenIdConnectRequestEvent = (PlayFabRequestEvent<UnlinkOpenIdConnectRequest>)Delegate.Remove(this.OnUnlinkOpenIdConnectRequestEvent, (PlayFabRequestEvent<UnlinkOpenIdConnectRequest>)obj268);
					}
				}
			}
			if (this.OnUnlinkOpenIdConnectResultEvent != null)
			{
				Delegate[] invocationList269 = this.OnUnlinkOpenIdConnectResultEvent.GetInvocationList();
				foreach (Delegate obj269 in invocationList269)
				{
					if (object.ReferenceEquals(obj269.Target, instance))
					{
						this.OnUnlinkOpenIdConnectResultEvent = (PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse>)Delegate.Remove(this.OnUnlinkOpenIdConnectResultEvent, (PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse>)obj269);
					}
				}
			}
			if (this.OnUnlinkPSNAccountRequestEvent != null)
			{
				Delegate[] invocationList270 = this.OnUnlinkPSNAccountRequestEvent.GetInvocationList();
				foreach (Delegate obj270 in invocationList270)
				{
					if (object.ReferenceEquals(obj270.Target, instance))
					{
						this.OnUnlinkPSNAccountRequestEvent = (PlayFabRequestEvent<UnlinkPSNAccountRequest>)Delegate.Remove(this.OnUnlinkPSNAccountRequestEvent, (PlayFabRequestEvent<UnlinkPSNAccountRequest>)obj270);
					}
				}
			}
			if (this.OnUnlinkPSNAccountResultEvent != null)
			{
				Delegate[] invocationList271 = this.OnUnlinkPSNAccountResultEvent.GetInvocationList();
				foreach (Delegate obj271 in invocationList271)
				{
					if (object.ReferenceEquals(obj271.Target, instance))
					{
						this.OnUnlinkPSNAccountResultEvent = (PlayFabResultEvent<UnlinkPSNAccountResult>)Delegate.Remove(this.OnUnlinkPSNAccountResultEvent, (PlayFabResultEvent<UnlinkPSNAccountResult>)obj271);
					}
				}
			}
			if (this.OnUnlinkSteamAccountRequestEvent != null)
			{
				Delegate[] invocationList272 = this.OnUnlinkSteamAccountRequestEvent.GetInvocationList();
				foreach (Delegate obj272 in invocationList272)
				{
					if (object.ReferenceEquals(obj272.Target, instance))
					{
						this.OnUnlinkSteamAccountRequestEvent = (PlayFabRequestEvent<UnlinkSteamAccountRequest>)Delegate.Remove(this.OnUnlinkSteamAccountRequestEvent, (PlayFabRequestEvent<UnlinkSteamAccountRequest>)obj272);
					}
				}
			}
			if (this.OnUnlinkSteamAccountResultEvent != null)
			{
				Delegate[] invocationList273 = this.OnUnlinkSteamAccountResultEvent.GetInvocationList();
				foreach (Delegate obj273 in invocationList273)
				{
					if (object.ReferenceEquals(obj273.Target, instance))
					{
						this.OnUnlinkSteamAccountResultEvent = (PlayFabResultEvent<UnlinkSteamAccountResult>)Delegate.Remove(this.OnUnlinkSteamAccountResultEvent, (PlayFabResultEvent<UnlinkSteamAccountResult>)obj273);
					}
				}
			}
			if (this.OnUnlinkTwitchRequestEvent != null)
			{
				Delegate[] invocationList274 = this.OnUnlinkTwitchRequestEvent.GetInvocationList();
				foreach (Delegate obj274 in invocationList274)
				{
					if (object.ReferenceEquals(obj274.Target, instance))
					{
						this.OnUnlinkTwitchRequestEvent = (PlayFabRequestEvent<UnlinkTwitchAccountRequest>)Delegate.Remove(this.OnUnlinkTwitchRequestEvent, (PlayFabRequestEvent<UnlinkTwitchAccountRequest>)obj274);
					}
				}
			}
			if (this.OnUnlinkTwitchResultEvent != null)
			{
				Delegate[] invocationList275 = this.OnUnlinkTwitchResultEvent.GetInvocationList();
				foreach (Delegate obj275 in invocationList275)
				{
					if (object.ReferenceEquals(obj275.Target, instance))
					{
						this.OnUnlinkTwitchResultEvent = (PlayFabResultEvent<UnlinkTwitchAccountResult>)Delegate.Remove(this.OnUnlinkTwitchResultEvent, (PlayFabResultEvent<UnlinkTwitchAccountResult>)obj275);
					}
				}
			}
			if (this.OnUnlinkWindowsHelloRequestEvent != null)
			{
				Delegate[] invocationList276 = this.OnUnlinkWindowsHelloRequestEvent.GetInvocationList();
				foreach (Delegate obj276 in invocationList276)
				{
					if (object.ReferenceEquals(obj276.Target, instance))
					{
						this.OnUnlinkWindowsHelloRequestEvent = (PlayFabRequestEvent<UnlinkWindowsHelloAccountRequest>)Delegate.Remove(this.OnUnlinkWindowsHelloRequestEvent, (PlayFabRequestEvent<UnlinkWindowsHelloAccountRequest>)obj276);
					}
				}
			}
			if (this.OnUnlinkWindowsHelloResultEvent != null)
			{
				Delegate[] invocationList277 = this.OnUnlinkWindowsHelloResultEvent.GetInvocationList();
				foreach (Delegate obj277 in invocationList277)
				{
					if (object.ReferenceEquals(obj277.Target, instance))
					{
						this.OnUnlinkWindowsHelloResultEvent = (PlayFabResultEvent<UnlinkWindowsHelloAccountResponse>)Delegate.Remove(this.OnUnlinkWindowsHelloResultEvent, (PlayFabResultEvent<UnlinkWindowsHelloAccountResponse>)obj277);
					}
				}
			}
			if (this.OnUnlinkXboxAccountRequestEvent != null)
			{
				Delegate[] invocationList278 = this.OnUnlinkXboxAccountRequestEvent.GetInvocationList();
				foreach (Delegate obj278 in invocationList278)
				{
					if (object.ReferenceEquals(obj278.Target, instance))
					{
						this.OnUnlinkXboxAccountRequestEvent = (PlayFabRequestEvent<UnlinkXboxAccountRequest>)Delegate.Remove(this.OnUnlinkXboxAccountRequestEvent, (PlayFabRequestEvent<UnlinkXboxAccountRequest>)obj278);
					}
				}
			}
			if (this.OnUnlinkXboxAccountResultEvent != null)
			{
				Delegate[] invocationList279 = this.OnUnlinkXboxAccountResultEvent.GetInvocationList();
				foreach (Delegate obj279 in invocationList279)
				{
					if (object.ReferenceEquals(obj279.Target, instance))
					{
						this.OnUnlinkXboxAccountResultEvent = (PlayFabResultEvent<UnlinkXboxAccountResult>)Delegate.Remove(this.OnUnlinkXboxAccountResultEvent, (PlayFabResultEvent<UnlinkXboxAccountResult>)obj279);
					}
				}
			}
			if (this.OnUnlockContainerInstanceRequestEvent != null)
			{
				Delegate[] invocationList280 = this.OnUnlockContainerInstanceRequestEvent.GetInvocationList();
				foreach (Delegate obj280 in invocationList280)
				{
					if (object.ReferenceEquals(obj280.Target, instance))
					{
						this.OnUnlockContainerInstanceRequestEvent = (PlayFabRequestEvent<UnlockContainerInstanceRequest>)Delegate.Remove(this.OnUnlockContainerInstanceRequestEvent, (PlayFabRequestEvent<UnlockContainerInstanceRequest>)obj280);
					}
				}
			}
			if (this.OnUnlockContainerInstanceResultEvent != null)
			{
				Delegate[] invocationList281 = this.OnUnlockContainerInstanceResultEvent.GetInvocationList();
				foreach (Delegate obj281 in invocationList281)
				{
					if (object.ReferenceEquals(obj281.Target, instance))
					{
						this.OnUnlockContainerInstanceResultEvent = (PlayFabResultEvent<UnlockContainerItemResult>)Delegate.Remove(this.OnUnlockContainerInstanceResultEvent, (PlayFabResultEvent<UnlockContainerItemResult>)obj281);
					}
				}
			}
			if (this.OnUnlockContainerItemRequestEvent != null)
			{
				Delegate[] invocationList282 = this.OnUnlockContainerItemRequestEvent.GetInvocationList();
				foreach (Delegate obj282 in invocationList282)
				{
					if (object.ReferenceEquals(obj282.Target, instance))
					{
						this.OnUnlockContainerItemRequestEvent = (PlayFabRequestEvent<UnlockContainerItemRequest>)Delegate.Remove(this.OnUnlockContainerItemRequestEvent, (PlayFabRequestEvent<UnlockContainerItemRequest>)obj282);
					}
				}
			}
			if (this.OnUnlockContainerItemResultEvent != null)
			{
				Delegate[] invocationList283 = this.OnUnlockContainerItemResultEvent.GetInvocationList();
				foreach (Delegate obj283 in invocationList283)
				{
					if (object.ReferenceEquals(obj283.Target, instance))
					{
						this.OnUnlockContainerItemResultEvent = (PlayFabResultEvent<UnlockContainerItemResult>)Delegate.Remove(this.OnUnlockContainerItemResultEvent, (PlayFabResultEvent<UnlockContainerItemResult>)obj283);
					}
				}
			}
			if (this.OnUpdateAvatarUrlRequestEvent != null)
			{
				Delegate[] invocationList284 = this.OnUpdateAvatarUrlRequestEvent.GetInvocationList();
				foreach (Delegate obj284 in invocationList284)
				{
					if (object.ReferenceEquals(obj284.Target, instance))
					{
						this.OnUpdateAvatarUrlRequestEvent = (PlayFabRequestEvent<UpdateAvatarUrlRequest>)Delegate.Remove(this.OnUpdateAvatarUrlRequestEvent, (PlayFabRequestEvent<UpdateAvatarUrlRequest>)obj284);
					}
				}
			}
			if (this.OnUpdateAvatarUrlResultEvent != null)
			{
				Delegate[] invocationList285 = this.OnUpdateAvatarUrlResultEvent.GetInvocationList();
				foreach (Delegate obj285 in invocationList285)
				{
					if (object.ReferenceEquals(obj285.Target, instance))
					{
						this.OnUpdateAvatarUrlResultEvent = (PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse>)Delegate.Remove(this.OnUpdateAvatarUrlResultEvent, (PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse>)obj285);
					}
				}
			}
			if (this.OnUpdateCharacterDataRequestEvent != null)
			{
				Delegate[] invocationList286 = this.OnUpdateCharacterDataRequestEvent.GetInvocationList();
				foreach (Delegate obj286 in invocationList286)
				{
					if (object.ReferenceEquals(obj286.Target, instance))
					{
						this.OnUpdateCharacterDataRequestEvent = (PlayFabRequestEvent<UpdateCharacterDataRequest>)Delegate.Remove(this.OnUpdateCharacterDataRequestEvent, (PlayFabRequestEvent<UpdateCharacterDataRequest>)obj286);
					}
				}
			}
			if (this.OnUpdateCharacterDataResultEvent != null)
			{
				Delegate[] invocationList287 = this.OnUpdateCharacterDataResultEvent.GetInvocationList();
				foreach (Delegate obj287 in invocationList287)
				{
					if (object.ReferenceEquals(obj287.Target, instance))
					{
						this.OnUpdateCharacterDataResultEvent = (PlayFabResultEvent<UpdateCharacterDataResult>)Delegate.Remove(this.OnUpdateCharacterDataResultEvent, (PlayFabResultEvent<UpdateCharacterDataResult>)obj287);
					}
				}
			}
			if (this.OnUpdateCharacterStatisticsRequestEvent != null)
			{
				Delegate[] invocationList288 = this.OnUpdateCharacterStatisticsRequestEvent.GetInvocationList();
				foreach (Delegate obj288 in invocationList288)
				{
					if (object.ReferenceEquals(obj288.Target, instance))
					{
						this.OnUpdateCharacterStatisticsRequestEvent = (PlayFabRequestEvent<UpdateCharacterStatisticsRequest>)Delegate.Remove(this.OnUpdateCharacterStatisticsRequestEvent, (PlayFabRequestEvent<UpdateCharacterStatisticsRequest>)obj288);
					}
				}
			}
			if (this.OnUpdateCharacterStatisticsResultEvent != null)
			{
				Delegate[] invocationList289 = this.OnUpdateCharacterStatisticsResultEvent.GetInvocationList();
				foreach (Delegate obj289 in invocationList289)
				{
					if (object.ReferenceEquals(obj289.Target, instance))
					{
						this.OnUpdateCharacterStatisticsResultEvent = (PlayFabResultEvent<UpdateCharacterStatisticsResult>)Delegate.Remove(this.OnUpdateCharacterStatisticsResultEvent, (PlayFabResultEvent<UpdateCharacterStatisticsResult>)obj289);
					}
				}
			}
			if (this.OnUpdatePlayerStatisticsRequestEvent != null)
			{
				Delegate[] invocationList290 = this.OnUpdatePlayerStatisticsRequestEvent.GetInvocationList();
				foreach (Delegate obj290 in invocationList290)
				{
					if (object.ReferenceEquals(obj290.Target, instance))
					{
						this.OnUpdatePlayerStatisticsRequestEvent = (PlayFabRequestEvent<UpdatePlayerStatisticsRequest>)Delegate.Remove(this.OnUpdatePlayerStatisticsRequestEvent, (PlayFabRequestEvent<UpdatePlayerStatisticsRequest>)obj290);
					}
				}
			}
			if (this.OnUpdatePlayerStatisticsResultEvent != null)
			{
				Delegate[] invocationList291 = this.OnUpdatePlayerStatisticsResultEvent.GetInvocationList();
				foreach (Delegate obj291 in invocationList291)
				{
					if (object.ReferenceEquals(obj291.Target, instance))
					{
						this.OnUpdatePlayerStatisticsResultEvent = (PlayFabResultEvent<UpdatePlayerStatisticsResult>)Delegate.Remove(this.OnUpdatePlayerStatisticsResultEvent, (PlayFabResultEvent<UpdatePlayerStatisticsResult>)obj291);
					}
				}
			}
			if (this.OnUpdateSharedGroupDataRequestEvent != null)
			{
				Delegate[] invocationList292 = this.OnUpdateSharedGroupDataRequestEvent.GetInvocationList();
				foreach (Delegate obj292 in invocationList292)
				{
					if (object.ReferenceEquals(obj292.Target, instance))
					{
						this.OnUpdateSharedGroupDataRequestEvent = (PlayFabRequestEvent<UpdateSharedGroupDataRequest>)Delegate.Remove(this.OnUpdateSharedGroupDataRequestEvent, (PlayFabRequestEvent<UpdateSharedGroupDataRequest>)obj292);
					}
				}
			}
			if (this.OnUpdateSharedGroupDataResultEvent != null)
			{
				Delegate[] invocationList293 = this.OnUpdateSharedGroupDataResultEvent.GetInvocationList();
				foreach (Delegate obj293 in invocationList293)
				{
					if (object.ReferenceEquals(obj293.Target, instance))
					{
						this.OnUpdateSharedGroupDataResultEvent = (PlayFabResultEvent<UpdateSharedGroupDataResult>)Delegate.Remove(this.OnUpdateSharedGroupDataResultEvent, (PlayFabResultEvent<UpdateSharedGroupDataResult>)obj293);
					}
				}
			}
			if (this.OnUpdateUserDataRequestEvent != null)
			{
				Delegate[] invocationList294 = this.OnUpdateUserDataRequestEvent.GetInvocationList();
				foreach (Delegate obj294 in invocationList294)
				{
					if (object.ReferenceEquals(obj294.Target, instance))
					{
						this.OnUpdateUserDataRequestEvent = (PlayFabRequestEvent<UpdateUserDataRequest>)Delegate.Remove(this.OnUpdateUserDataRequestEvent, (PlayFabRequestEvent<UpdateUserDataRequest>)obj294);
					}
				}
			}
			if (this.OnUpdateUserDataResultEvent != null)
			{
				Delegate[] invocationList295 = this.OnUpdateUserDataResultEvent.GetInvocationList();
				foreach (Delegate obj295 in invocationList295)
				{
					if (object.ReferenceEquals(obj295.Target, instance))
					{
						this.OnUpdateUserDataResultEvent = (PlayFabResultEvent<UpdateUserDataResult>)Delegate.Remove(this.OnUpdateUserDataResultEvent, (PlayFabResultEvent<UpdateUserDataResult>)obj295);
					}
				}
			}
			if (this.OnUpdateUserPublisherDataRequestEvent != null)
			{
				Delegate[] invocationList296 = this.OnUpdateUserPublisherDataRequestEvent.GetInvocationList();
				foreach (Delegate obj296 in invocationList296)
				{
					if (object.ReferenceEquals(obj296.Target, instance))
					{
						this.OnUpdateUserPublisherDataRequestEvent = (PlayFabRequestEvent<UpdateUserDataRequest>)Delegate.Remove(this.OnUpdateUserPublisherDataRequestEvent, (PlayFabRequestEvent<UpdateUserDataRequest>)obj296);
					}
				}
			}
			if (this.OnUpdateUserPublisherDataResultEvent != null)
			{
				Delegate[] invocationList297 = this.OnUpdateUserPublisherDataResultEvent.GetInvocationList();
				foreach (Delegate obj297 in invocationList297)
				{
					if (object.ReferenceEquals(obj297.Target, instance))
					{
						this.OnUpdateUserPublisherDataResultEvent = (PlayFabResultEvent<UpdateUserDataResult>)Delegate.Remove(this.OnUpdateUserPublisherDataResultEvent, (PlayFabResultEvent<UpdateUserDataResult>)obj297);
					}
				}
			}
			if (this.OnUpdateUserTitleDisplayNameRequestEvent != null)
			{
				Delegate[] invocationList298 = this.OnUpdateUserTitleDisplayNameRequestEvent.GetInvocationList();
				foreach (Delegate obj298 in invocationList298)
				{
					if (object.ReferenceEquals(obj298.Target, instance))
					{
						this.OnUpdateUserTitleDisplayNameRequestEvent = (PlayFabRequestEvent<UpdateUserTitleDisplayNameRequest>)Delegate.Remove(this.OnUpdateUserTitleDisplayNameRequestEvent, (PlayFabRequestEvent<UpdateUserTitleDisplayNameRequest>)obj298);
					}
				}
			}
			if (this.OnUpdateUserTitleDisplayNameResultEvent != null)
			{
				Delegate[] invocationList299 = this.OnUpdateUserTitleDisplayNameResultEvent.GetInvocationList();
				foreach (Delegate obj299 in invocationList299)
				{
					if (object.ReferenceEquals(obj299.Target, instance))
					{
						this.OnUpdateUserTitleDisplayNameResultEvent = (PlayFabResultEvent<UpdateUserTitleDisplayNameResult>)Delegate.Remove(this.OnUpdateUserTitleDisplayNameResultEvent, (PlayFabResultEvent<UpdateUserTitleDisplayNameResult>)obj299);
					}
				}
			}
			if (this.OnValidateAmazonIAPReceiptRequestEvent != null)
			{
				Delegate[] invocationList300 = this.OnValidateAmazonIAPReceiptRequestEvent.GetInvocationList();
				foreach (Delegate obj300 in invocationList300)
				{
					if (object.ReferenceEquals(obj300.Target, instance))
					{
						this.OnValidateAmazonIAPReceiptRequestEvent = (PlayFabRequestEvent<ValidateAmazonReceiptRequest>)Delegate.Remove(this.OnValidateAmazonIAPReceiptRequestEvent, (PlayFabRequestEvent<ValidateAmazonReceiptRequest>)obj300);
					}
				}
			}
			if (this.OnValidateAmazonIAPReceiptResultEvent != null)
			{
				Delegate[] invocationList301 = this.OnValidateAmazonIAPReceiptResultEvent.GetInvocationList();
				foreach (Delegate obj301 in invocationList301)
				{
					if (object.ReferenceEquals(obj301.Target, instance))
					{
						this.OnValidateAmazonIAPReceiptResultEvent = (PlayFabResultEvent<ValidateAmazonReceiptResult>)Delegate.Remove(this.OnValidateAmazonIAPReceiptResultEvent, (PlayFabResultEvent<ValidateAmazonReceiptResult>)obj301);
					}
				}
			}
			if (this.OnValidateGooglePlayPurchaseRequestEvent != null)
			{
				Delegate[] invocationList302 = this.OnValidateGooglePlayPurchaseRequestEvent.GetInvocationList();
				foreach (Delegate obj302 in invocationList302)
				{
					if (object.ReferenceEquals(obj302.Target, instance))
					{
						this.OnValidateGooglePlayPurchaseRequestEvent = (PlayFabRequestEvent<ValidateGooglePlayPurchaseRequest>)Delegate.Remove(this.OnValidateGooglePlayPurchaseRequestEvent, (PlayFabRequestEvent<ValidateGooglePlayPurchaseRequest>)obj302);
					}
				}
			}
			if (this.OnValidateGooglePlayPurchaseResultEvent != null)
			{
				Delegate[] invocationList303 = this.OnValidateGooglePlayPurchaseResultEvent.GetInvocationList();
				foreach (Delegate obj303 in invocationList303)
				{
					if (object.ReferenceEquals(obj303.Target, instance))
					{
						this.OnValidateGooglePlayPurchaseResultEvent = (PlayFabResultEvent<ValidateGooglePlayPurchaseResult>)Delegate.Remove(this.OnValidateGooglePlayPurchaseResultEvent, (PlayFabResultEvent<ValidateGooglePlayPurchaseResult>)obj303);
					}
				}
			}
			if (this.OnValidateIOSReceiptRequestEvent != null)
			{
				Delegate[] invocationList304 = this.OnValidateIOSReceiptRequestEvent.GetInvocationList();
				foreach (Delegate obj304 in invocationList304)
				{
					if (object.ReferenceEquals(obj304.Target, instance))
					{
						this.OnValidateIOSReceiptRequestEvent = (PlayFabRequestEvent<ValidateIOSReceiptRequest>)Delegate.Remove(this.OnValidateIOSReceiptRequestEvent, (PlayFabRequestEvent<ValidateIOSReceiptRequest>)obj304);
					}
				}
			}
			if (this.OnValidateIOSReceiptResultEvent != null)
			{
				Delegate[] invocationList305 = this.OnValidateIOSReceiptResultEvent.GetInvocationList();
				foreach (Delegate obj305 in invocationList305)
				{
					if (object.ReferenceEquals(obj305.Target, instance))
					{
						this.OnValidateIOSReceiptResultEvent = (PlayFabResultEvent<ValidateIOSReceiptResult>)Delegate.Remove(this.OnValidateIOSReceiptResultEvent, (PlayFabResultEvent<ValidateIOSReceiptResult>)obj305);
					}
				}
			}
			if (this.OnValidateWindowsStoreReceiptRequestEvent != null)
			{
				Delegate[] invocationList306 = this.OnValidateWindowsStoreReceiptRequestEvent.GetInvocationList();
				foreach (Delegate obj306 in invocationList306)
				{
					if (object.ReferenceEquals(obj306.Target, instance))
					{
						this.OnValidateWindowsStoreReceiptRequestEvent = (PlayFabRequestEvent<ValidateWindowsReceiptRequest>)Delegate.Remove(this.OnValidateWindowsStoreReceiptRequestEvent, (PlayFabRequestEvent<ValidateWindowsReceiptRequest>)obj306);
					}
				}
			}
			if (this.OnValidateWindowsStoreReceiptResultEvent != null)
			{
				Delegate[] invocationList307 = this.OnValidateWindowsStoreReceiptResultEvent.GetInvocationList();
				foreach (Delegate obj307 in invocationList307)
				{
					if (object.ReferenceEquals(obj307.Target, instance))
					{
						this.OnValidateWindowsStoreReceiptResultEvent = (PlayFabResultEvent<ValidateWindowsReceiptResult>)Delegate.Remove(this.OnValidateWindowsStoreReceiptResultEvent, (PlayFabResultEvent<ValidateWindowsReceiptResult>)obj307);
					}
				}
			}
			if (this.OnWriteCharacterEventRequestEvent != null)
			{
				Delegate[] invocationList308 = this.OnWriteCharacterEventRequestEvent.GetInvocationList();
				foreach (Delegate obj308 in invocationList308)
				{
					if (object.ReferenceEquals(obj308.Target, instance))
					{
						this.OnWriteCharacterEventRequestEvent = (PlayFabRequestEvent<WriteClientCharacterEventRequest>)Delegate.Remove(this.OnWriteCharacterEventRequestEvent, (PlayFabRequestEvent<WriteClientCharacterEventRequest>)obj308);
					}
				}
			}
			if (this.OnWriteCharacterEventResultEvent != null)
			{
				Delegate[] invocationList309 = this.OnWriteCharacterEventResultEvent.GetInvocationList();
				foreach (Delegate obj309 in invocationList309)
				{
					if (object.ReferenceEquals(obj309.Target, instance))
					{
						this.OnWriteCharacterEventResultEvent = (PlayFabResultEvent<WriteEventResponse>)Delegate.Remove(this.OnWriteCharacterEventResultEvent, (PlayFabResultEvent<WriteEventResponse>)obj309);
					}
				}
			}
			if (this.OnWritePlayerEventRequestEvent != null)
			{
				Delegate[] invocationList310 = this.OnWritePlayerEventRequestEvent.GetInvocationList();
				foreach (Delegate obj310 in invocationList310)
				{
					if (object.ReferenceEquals(obj310.Target, instance))
					{
						this.OnWritePlayerEventRequestEvent = (PlayFabRequestEvent<WriteClientPlayerEventRequest>)Delegate.Remove(this.OnWritePlayerEventRequestEvent, (PlayFabRequestEvent<WriteClientPlayerEventRequest>)obj310);
					}
				}
			}
			if (this.OnWritePlayerEventResultEvent != null)
			{
				Delegate[] invocationList311 = this.OnWritePlayerEventResultEvent.GetInvocationList();
				foreach (Delegate obj311 in invocationList311)
				{
					if (object.ReferenceEquals(obj311.Target, instance))
					{
						this.OnWritePlayerEventResultEvent = (PlayFabResultEvent<WriteEventResponse>)Delegate.Remove(this.OnWritePlayerEventResultEvent, (PlayFabResultEvent<WriteEventResponse>)obj311);
					}
				}
			}
			if (this.OnWriteTitleEventRequestEvent != null)
			{
				Delegate[] invocationList312 = this.OnWriteTitleEventRequestEvent.GetInvocationList();
				foreach (Delegate obj312 in invocationList312)
				{
					if (object.ReferenceEquals(obj312.Target, instance))
					{
						this.OnWriteTitleEventRequestEvent = (PlayFabRequestEvent<WriteTitleEventRequest>)Delegate.Remove(this.OnWriteTitleEventRequestEvent, (PlayFabRequestEvent<WriteTitleEventRequest>)obj312);
					}
				}
			}
			if (this.OnWriteTitleEventResultEvent != null)
			{
				Delegate[] invocationList313 = this.OnWriteTitleEventResultEvent.GetInvocationList();
				foreach (Delegate obj313 in invocationList313)
				{
					if (object.ReferenceEquals(obj313.Target, instance))
					{
						this.OnWriteTitleEventResultEvent = (PlayFabResultEvent<WriteEventResponse>)Delegate.Remove(this.OnWriteTitleEventResultEvent, (PlayFabResultEvent<WriteEventResponse>)obj313);
					}
				}
			}
			if (this.OnAuthenticationGetEntityTokenRequestEvent != null)
			{
				Delegate[] invocationList314 = this.OnAuthenticationGetEntityTokenRequestEvent.GetInvocationList();
				foreach (Delegate obj314 in invocationList314)
				{
					if (object.ReferenceEquals(obj314.Target, instance))
					{
						this.OnAuthenticationGetEntityTokenRequestEvent = (PlayFabRequestEvent<GetEntityTokenRequest>)Delegate.Remove(this.OnAuthenticationGetEntityTokenRequestEvent, (PlayFabRequestEvent<GetEntityTokenRequest>)obj314);
					}
				}
			}
			if (this.OnAuthenticationGetEntityTokenResultEvent != null)
			{
				Delegate[] invocationList315 = this.OnAuthenticationGetEntityTokenResultEvent.GetInvocationList();
				foreach (Delegate obj315 in invocationList315)
				{
					if (object.ReferenceEquals(obj315.Target, instance))
					{
						this.OnAuthenticationGetEntityTokenResultEvent = (PlayFabResultEvent<GetEntityTokenResponse>)Delegate.Remove(this.OnAuthenticationGetEntityTokenResultEvent, (PlayFabResultEvent<GetEntityTokenResponse>)obj315);
					}
				}
			}
			if (this.OnAuthenticationValidateEntityTokenRequestEvent != null)
			{
				Delegate[] invocationList316 = this.OnAuthenticationValidateEntityTokenRequestEvent.GetInvocationList();
				foreach (Delegate obj316 in invocationList316)
				{
					if (object.ReferenceEquals(obj316.Target, instance))
					{
						this.OnAuthenticationValidateEntityTokenRequestEvent = (PlayFabRequestEvent<ValidateEntityTokenRequest>)Delegate.Remove(this.OnAuthenticationValidateEntityTokenRequestEvent, (PlayFabRequestEvent<ValidateEntityTokenRequest>)obj316);
					}
				}
			}
			if (this.OnAuthenticationValidateEntityTokenResultEvent != null)
			{
				Delegate[] invocationList317 = this.OnAuthenticationValidateEntityTokenResultEvent.GetInvocationList();
				foreach (Delegate obj317 in invocationList317)
				{
					if (object.ReferenceEquals(obj317.Target, instance))
					{
						this.OnAuthenticationValidateEntityTokenResultEvent = (PlayFabResultEvent<ValidateEntityTokenResponse>)Delegate.Remove(this.OnAuthenticationValidateEntityTokenResultEvent, (PlayFabResultEvent<ValidateEntityTokenResponse>)obj317);
					}
				}
			}
			if (this.OnCloudScriptExecuteEntityCloudScriptRequestEvent != null)
			{
				Delegate[] invocationList318 = this.OnCloudScriptExecuteEntityCloudScriptRequestEvent.GetInvocationList();
				foreach (Delegate obj318 in invocationList318)
				{
					if (object.ReferenceEquals(obj318.Target, instance))
					{
						this.OnCloudScriptExecuteEntityCloudScriptRequestEvent = (PlayFabRequestEvent<ExecuteEntityCloudScriptRequest>)Delegate.Remove(this.OnCloudScriptExecuteEntityCloudScriptRequestEvent, (PlayFabRequestEvent<ExecuteEntityCloudScriptRequest>)obj318);
					}
				}
			}
			if (this.OnCloudScriptExecuteEntityCloudScriptResultEvent != null)
			{
				Delegate[] invocationList319 = this.OnCloudScriptExecuteEntityCloudScriptResultEvent.GetInvocationList();
				foreach (Delegate obj319 in invocationList319)
				{
					if (object.ReferenceEquals(obj319.Target, instance))
					{
						this.OnCloudScriptExecuteEntityCloudScriptResultEvent = (PlayFabResultEvent<PlayFab.CloudScriptModels.ExecuteCloudScriptResult>)Delegate.Remove(this.OnCloudScriptExecuteEntityCloudScriptResultEvent, (PlayFabResultEvent<PlayFab.CloudScriptModels.ExecuteCloudScriptResult>)obj319);
					}
				}
			}
			if (this.OnCloudScriptExecuteFunctionRequestEvent != null)
			{
				Delegate[] invocationList320 = this.OnCloudScriptExecuteFunctionRequestEvent.GetInvocationList();
				foreach (Delegate obj320 in invocationList320)
				{
					if (object.ReferenceEquals(obj320.Target, instance))
					{
						this.OnCloudScriptExecuteFunctionRequestEvent = (PlayFabRequestEvent<ExecuteFunctionRequest>)Delegate.Remove(this.OnCloudScriptExecuteFunctionRequestEvent, (PlayFabRequestEvent<ExecuteFunctionRequest>)obj320);
					}
				}
			}
			if (this.OnCloudScriptExecuteFunctionResultEvent != null)
			{
				Delegate[] invocationList321 = this.OnCloudScriptExecuteFunctionResultEvent.GetInvocationList();
				foreach (Delegate obj321 in invocationList321)
				{
					if (object.ReferenceEquals(obj321.Target, instance))
					{
						this.OnCloudScriptExecuteFunctionResultEvent = (PlayFabResultEvent<ExecuteFunctionResult>)Delegate.Remove(this.OnCloudScriptExecuteFunctionResultEvent, (PlayFabResultEvent<ExecuteFunctionResult>)obj321);
					}
				}
			}
			if (this.OnCloudScriptListFunctionsRequestEvent != null)
			{
				Delegate[] invocationList322 = this.OnCloudScriptListFunctionsRequestEvent.GetInvocationList();
				foreach (Delegate obj322 in invocationList322)
				{
					if (object.ReferenceEquals(obj322.Target, instance))
					{
						this.OnCloudScriptListFunctionsRequestEvent = (PlayFabRequestEvent<ListFunctionsRequest>)Delegate.Remove(this.OnCloudScriptListFunctionsRequestEvent, (PlayFabRequestEvent<ListFunctionsRequest>)obj322);
					}
				}
			}
			if (this.OnCloudScriptListFunctionsResultEvent != null)
			{
				Delegate[] invocationList323 = this.OnCloudScriptListFunctionsResultEvent.GetInvocationList();
				foreach (Delegate obj323 in invocationList323)
				{
					if (object.ReferenceEquals(obj323.Target, instance))
					{
						this.OnCloudScriptListFunctionsResultEvent = (PlayFabResultEvent<ListFunctionsResult>)Delegate.Remove(this.OnCloudScriptListFunctionsResultEvent, (PlayFabResultEvent<ListFunctionsResult>)obj323);
					}
				}
			}
			if (this.OnCloudScriptListHttpFunctionsRequestEvent != null)
			{
				Delegate[] invocationList324 = this.OnCloudScriptListHttpFunctionsRequestEvent.GetInvocationList();
				foreach (Delegate obj324 in invocationList324)
				{
					if (object.ReferenceEquals(obj324.Target, instance))
					{
						this.OnCloudScriptListHttpFunctionsRequestEvent = (PlayFabRequestEvent<ListFunctionsRequest>)Delegate.Remove(this.OnCloudScriptListHttpFunctionsRequestEvent, (PlayFabRequestEvent<ListFunctionsRequest>)obj324);
					}
				}
			}
			if (this.OnCloudScriptListHttpFunctionsResultEvent != null)
			{
				Delegate[] invocationList325 = this.OnCloudScriptListHttpFunctionsResultEvent.GetInvocationList();
				foreach (Delegate obj325 in invocationList325)
				{
					if (object.ReferenceEquals(obj325.Target, instance))
					{
						this.OnCloudScriptListHttpFunctionsResultEvent = (PlayFabResultEvent<ListHttpFunctionsResult>)Delegate.Remove(this.OnCloudScriptListHttpFunctionsResultEvent, (PlayFabResultEvent<ListHttpFunctionsResult>)obj325);
					}
				}
			}
			if (this.OnCloudScriptListQueuedFunctionsRequestEvent != null)
			{
				Delegate[] invocationList326 = this.OnCloudScriptListQueuedFunctionsRequestEvent.GetInvocationList();
				foreach (Delegate obj326 in invocationList326)
				{
					if (object.ReferenceEquals(obj326.Target, instance))
					{
						this.OnCloudScriptListQueuedFunctionsRequestEvent = (PlayFabRequestEvent<ListFunctionsRequest>)Delegate.Remove(this.OnCloudScriptListQueuedFunctionsRequestEvent, (PlayFabRequestEvent<ListFunctionsRequest>)obj326);
					}
				}
			}
			if (this.OnCloudScriptListQueuedFunctionsResultEvent != null)
			{
				Delegate[] invocationList327 = this.OnCloudScriptListQueuedFunctionsResultEvent.GetInvocationList();
				foreach (Delegate obj327 in invocationList327)
				{
					if (object.ReferenceEquals(obj327.Target, instance))
					{
						this.OnCloudScriptListQueuedFunctionsResultEvent = (PlayFabResultEvent<ListQueuedFunctionsResult>)Delegate.Remove(this.OnCloudScriptListQueuedFunctionsResultEvent, (PlayFabResultEvent<ListQueuedFunctionsResult>)obj327);
					}
				}
			}
			if (this.OnCloudScriptPostFunctionResultForEntityTriggeredActionRequestEvent != null)
			{
				Delegate[] invocationList328 = this.OnCloudScriptPostFunctionResultForEntityTriggeredActionRequestEvent.GetInvocationList();
				foreach (Delegate obj328 in invocationList328)
				{
					if (object.ReferenceEquals(obj328.Target, instance))
					{
						this.OnCloudScriptPostFunctionResultForEntityTriggeredActionRequestEvent = (PlayFabRequestEvent<PostFunctionResultForEntityTriggeredActionRequest>)Delegate.Remove(this.OnCloudScriptPostFunctionResultForEntityTriggeredActionRequestEvent, (PlayFabRequestEvent<PostFunctionResultForEntityTriggeredActionRequest>)obj328);
					}
				}
			}
			if (this.OnCloudScriptPostFunctionResultForEntityTriggeredActionResultEvent != null)
			{
				Delegate[] invocationList329 = this.OnCloudScriptPostFunctionResultForEntityTriggeredActionResultEvent.GetInvocationList();
				foreach (Delegate obj329 in invocationList329)
				{
					if (object.ReferenceEquals(obj329.Target, instance))
					{
						this.OnCloudScriptPostFunctionResultForEntityTriggeredActionResultEvent = (PlayFabResultEvent<PlayFab.CloudScriptModels.EmptyResult>)Delegate.Remove(this.OnCloudScriptPostFunctionResultForEntityTriggeredActionResultEvent, (PlayFabResultEvent<PlayFab.CloudScriptModels.EmptyResult>)obj329);
					}
				}
			}
			if (this.OnCloudScriptPostFunctionResultForFunctionExecutionRequestEvent != null)
			{
				Delegate[] invocationList330 = this.OnCloudScriptPostFunctionResultForFunctionExecutionRequestEvent.GetInvocationList();
				foreach (Delegate obj330 in invocationList330)
				{
					if (object.ReferenceEquals(obj330.Target, instance))
					{
						this.OnCloudScriptPostFunctionResultForFunctionExecutionRequestEvent = (PlayFabRequestEvent<PostFunctionResultForFunctionExecutionRequest>)Delegate.Remove(this.OnCloudScriptPostFunctionResultForFunctionExecutionRequestEvent, (PlayFabRequestEvent<PostFunctionResultForFunctionExecutionRequest>)obj330);
					}
				}
			}
			if (this.OnCloudScriptPostFunctionResultForFunctionExecutionResultEvent != null)
			{
				Delegate[] invocationList331 = this.OnCloudScriptPostFunctionResultForFunctionExecutionResultEvent.GetInvocationList();
				foreach (Delegate obj331 in invocationList331)
				{
					if (object.ReferenceEquals(obj331.Target, instance))
					{
						this.OnCloudScriptPostFunctionResultForFunctionExecutionResultEvent = (PlayFabResultEvent<PlayFab.CloudScriptModels.EmptyResult>)Delegate.Remove(this.OnCloudScriptPostFunctionResultForFunctionExecutionResultEvent, (PlayFabResultEvent<PlayFab.CloudScriptModels.EmptyResult>)obj331);
					}
				}
			}
			if (this.OnCloudScriptPostFunctionResultForPlayerTriggeredActionRequestEvent != null)
			{
				Delegate[] invocationList332 = this.OnCloudScriptPostFunctionResultForPlayerTriggeredActionRequestEvent.GetInvocationList();
				foreach (Delegate obj332 in invocationList332)
				{
					if (object.ReferenceEquals(obj332.Target, instance))
					{
						this.OnCloudScriptPostFunctionResultForPlayerTriggeredActionRequestEvent = (PlayFabRequestEvent<PostFunctionResultForPlayerTriggeredActionRequest>)Delegate.Remove(this.OnCloudScriptPostFunctionResultForPlayerTriggeredActionRequestEvent, (PlayFabRequestEvent<PostFunctionResultForPlayerTriggeredActionRequest>)obj332);
					}
				}
			}
			if (this.OnCloudScriptPostFunctionResultForPlayerTriggeredActionResultEvent != null)
			{
				Delegate[] invocationList333 = this.OnCloudScriptPostFunctionResultForPlayerTriggeredActionResultEvent.GetInvocationList();
				foreach (Delegate obj333 in invocationList333)
				{
					if (object.ReferenceEquals(obj333.Target, instance))
					{
						this.OnCloudScriptPostFunctionResultForPlayerTriggeredActionResultEvent = (PlayFabResultEvent<PlayFab.CloudScriptModels.EmptyResult>)Delegate.Remove(this.OnCloudScriptPostFunctionResultForPlayerTriggeredActionResultEvent, (PlayFabResultEvent<PlayFab.CloudScriptModels.EmptyResult>)obj333);
					}
				}
			}
			if (this.OnCloudScriptPostFunctionResultForScheduledTaskRequestEvent != null)
			{
				Delegate[] invocationList334 = this.OnCloudScriptPostFunctionResultForScheduledTaskRequestEvent.GetInvocationList();
				foreach (Delegate obj334 in invocationList334)
				{
					if (object.ReferenceEquals(obj334.Target, instance))
					{
						this.OnCloudScriptPostFunctionResultForScheduledTaskRequestEvent = (PlayFabRequestEvent<PostFunctionResultForScheduledTaskRequest>)Delegate.Remove(this.OnCloudScriptPostFunctionResultForScheduledTaskRequestEvent, (PlayFabRequestEvent<PostFunctionResultForScheduledTaskRequest>)obj334);
					}
				}
			}
			if (this.OnCloudScriptPostFunctionResultForScheduledTaskResultEvent != null)
			{
				Delegate[] invocationList335 = this.OnCloudScriptPostFunctionResultForScheduledTaskResultEvent.GetInvocationList();
				foreach (Delegate obj335 in invocationList335)
				{
					if (object.ReferenceEquals(obj335.Target, instance))
					{
						this.OnCloudScriptPostFunctionResultForScheduledTaskResultEvent = (PlayFabResultEvent<PlayFab.CloudScriptModels.EmptyResult>)Delegate.Remove(this.OnCloudScriptPostFunctionResultForScheduledTaskResultEvent, (PlayFabResultEvent<PlayFab.CloudScriptModels.EmptyResult>)obj335);
					}
				}
			}
			if (this.OnCloudScriptRegisterHttpFunctionRequestEvent != null)
			{
				Delegate[] invocationList336 = this.OnCloudScriptRegisterHttpFunctionRequestEvent.GetInvocationList();
				foreach (Delegate obj336 in invocationList336)
				{
					if (object.ReferenceEquals(obj336.Target, instance))
					{
						this.OnCloudScriptRegisterHttpFunctionRequestEvent = (PlayFabRequestEvent<RegisterHttpFunctionRequest>)Delegate.Remove(this.OnCloudScriptRegisterHttpFunctionRequestEvent, (PlayFabRequestEvent<RegisterHttpFunctionRequest>)obj336);
					}
				}
			}
			if (this.OnCloudScriptRegisterHttpFunctionResultEvent != null)
			{
				Delegate[] invocationList337 = this.OnCloudScriptRegisterHttpFunctionResultEvent.GetInvocationList();
				foreach (Delegate obj337 in invocationList337)
				{
					if (object.ReferenceEquals(obj337.Target, instance))
					{
						this.OnCloudScriptRegisterHttpFunctionResultEvent = (PlayFabResultEvent<PlayFab.CloudScriptModels.EmptyResult>)Delegate.Remove(this.OnCloudScriptRegisterHttpFunctionResultEvent, (PlayFabResultEvent<PlayFab.CloudScriptModels.EmptyResult>)obj337);
					}
				}
			}
			if (this.OnCloudScriptRegisterQueuedFunctionRequestEvent != null)
			{
				Delegate[] invocationList338 = this.OnCloudScriptRegisterQueuedFunctionRequestEvent.GetInvocationList();
				foreach (Delegate obj338 in invocationList338)
				{
					if (object.ReferenceEquals(obj338.Target, instance))
					{
						this.OnCloudScriptRegisterQueuedFunctionRequestEvent = (PlayFabRequestEvent<RegisterQueuedFunctionRequest>)Delegate.Remove(this.OnCloudScriptRegisterQueuedFunctionRequestEvent, (PlayFabRequestEvent<RegisterQueuedFunctionRequest>)obj338);
					}
				}
			}
			if (this.OnCloudScriptRegisterQueuedFunctionResultEvent != null)
			{
				Delegate[] invocationList339 = this.OnCloudScriptRegisterQueuedFunctionResultEvent.GetInvocationList();
				foreach (Delegate obj339 in invocationList339)
				{
					if (object.ReferenceEquals(obj339.Target, instance))
					{
						this.OnCloudScriptRegisterQueuedFunctionResultEvent = (PlayFabResultEvent<PlayFab.CloudScriptModels.EmptyResult>)Delegate.Remove(this.OnCloudScriptRegisterQueuedFunctionResultEvent, (PlayFabResultEvent<PlayFab.CloudScriptModels.EmptyResult>)obj339);
					}
				}
			}
			if (this.OnCloudScriptUnregisterFunctionRequestEvent != null)
			{
				Delegate[] invocationList340 = this.OnCloudScriptUnregisterFunctionRequestEvent.GetInvocationList();
				foreach (Delegate obj340 in invocationList340)
				{
					if (object.ReferenceEquals(obj340.Target, instance))
					{
						this.OnCloudScriptUnregisterFunctionRequestEvent = (PlayFabRequestEvent<UnregisterFunctionRequest>)Delegate.Remove(this.OnCloudScriptUnregisterFunctionRequestEvent, (PlayFabRequestEvent<UnregisterFunctionRequest>)obj340);
					}
				}
			}
			if (this.OnCloudScriptUnregisterFunctionResultEvent != null)
			{
				Delegate[] invocationList341 = this.OnCloudScriptUnregisterFunctionResultEvent.GetInvocationList();
				foreach (Delegate obj341 in invocationList341)
				{
					if (object.ReferenceEquals(obj341.Target, instance))
					{
						this.OnCloudScriptUnregisterFunctionResultEvent = (PlayFabResultEvent<PlayFab.CloudScriptModels.EmptyResult>)Delegate.Remove(this.OnCloudScriptUnregisterFunctionResultEvent, (PlayFabResultEvent<PlayFab.CloudScriptModels.EmptyResult>)obj341);
					}
				}
			}
			if (this.OnDataAbortFileUploadsRequestEvent != null)
			{
				Delegate[] invocationList342 = this.OnDataAbortFileUploadsRequestEvent.GetInvocationList();
				foreach (Delegate obj342 in invocationList342)
				{
					if (object.ReferenceEquals(obj342.Target, instance))
					{
						this.OnDataAbortFileUploadsRequestEvent = (PlayFabRequestEvent<AbortFileUploadsRequest>)Delegate.Remove(this.OnDataAbortFileUploadsRequestEvent, (PlayFabRequestEvent<AbortFileUploadsRequest>)obj342);
					}
				}
			}
			if (this.OnDataAbortFileUploadsResultEvent != null)
			{
				Delegate[] invocationList343 = this.OnDataAbortFileUploadsResultEvent.GetInvocationList();
				foreach (Delegate obj343 in invocationList343)
				{
					if (object.ReferenceEquals(obj343.Target, instance))
					{
						this.OnDataAbortFileUploadsResultEvent = (PlayFabResultEvent<AbortFileUploadsResponse>)Delegate.Remove(this.OnDataAbortFileUploadsResultEvent, (PlayFabResultEvent<AbortFileUploadsResponse>)obj343);
					}
				}
			}
			if (this.OnDataDeleteFilesRequestEvent != null)
			{
				Delegate[] invocationList344 = this.OnDataDeleteFilesRequestEvent.GetInvocationList();
				foreach (Delegate obj344 in invocationList344)
				{
					if (object.ReferenceEquals(obj344.Target, instance))
					{
						this.OnDataDeleteFilesRequestEvent = (PlayFabRequestEvent<DeleteFilesRequest>)Delegate.Remove(this.OnDataDeleteFilesRequestEvent, (PlayFabRequestEvent<DeleteFilesRequest>)obj344);
					}
				}
			}
			if (this.OnDataDeleteFilesResultEvent != null)
			{
				Delegate[] invocationList345 = this.OnDataDeleteFilesResultEvent.GetInvocationList();
				foreach (Delegate obj345 in invocationList345)
				{
					if (object.ReferenceEquals(obj345.Target, instance))
					{
						this.OnDataDeleteFilesResultEvent = (PlayFabResultEvent<DeleteFilesResponse>)Delegate.Remove(this.OnDataDeleteFilesResultEvent, (PlayFabResultEvent<DeleteFilesResponse>)obj345);
					}
				}
			}
			if (this.OnDataFinalizeFileUploadsRequestEvent != null)
			{
				Delegate[] invocationList346 = this.OnDataFinalizeFileUploadsRequestEvent.GetInvocationList();
				foreach (Delegate obj346 in invocationList346)
				{
					if (object.ReferenceEquals(obj346.Target, instance))
					{
						this.OnDataFinalizeFileUploadsRequestEvent = (PlayFabRequestEvent<FinalizeFileUploadsRequest>)Delegate.Remove(this.OnDataFinalizeFileUploadsRequestEvent, (PlayFabRequestEvent<FinalizeFileUploadsRequest>)obj346);
					}
				}
			}
			if (this.OnDataFinalizeFileUploadsResultEvent != null)
			{
				Delegate[] invocationList347 = this.OnDataFinalizeFileUploadsResultEvent.GetInvocationList();
				foreach (Delegate obj347 in invocationList347)
				{
					if (object.ReferenceEquals(obj347.Target, instance))
					{
						this.OnDataFinalizeFileUploadsResultEvent = (PlayFabResultEvent<FinalizeFileUploadsResponse>)Delegate.Remove(this.OnDataFinalizeFileUploadsResultEvent, (PlayFabResultEvent<FinalizeFileUploadsResponse>)obj347);
					}
				}
			}
			if (this.OnDataGetFilesRequestEvent != null)
			{
				Delegate[] invocationList348 = this.OnDataGetFilesRequestEvent.GetInvocationList();
				foreach (Delegate obj348 in invocationList348)
				{
					if (object.ReferenceEquals(obj348.Target, instance))
					{
						this.OnDataGetFilesRequestEvent = (PlayFabRequestEvent<GetFilesRequest>)Delegate.Remove(this.OnDataGetFilesRequestEvent, (PlayFabRequestEvent<GetFilesRequest>)obj348);
					}
				}
			}
			if (this.OnDataGetFilesResultEvent != null)
			{
				Delegate[] invocationList349 = this.OnDataGetFilesResultEvent.GetInvocationList();
				foreach (Delegate obj349 in invocationList349)
				{
					if (object.ReferenceEquals(obj349.Target, instance))
					{
						this.OnDataGetFilesResultEvent = (PlayFabResultEvent<GetFilesResponse>)Delegate.Remove(this.OnDataGetFilesResultEvent, (PlayFabResultEvent<GetFilesResponse>)obj349);
					}
				}
			}
			if (this.OnDataGetObjectsRequestEvent != null)
			{
				Delegate[] invocationList350 = this.OnDataGetObjectsRequestEvent.GetInvocationList();
				foreach (Delegate obj350 in invocationList350)
				{
					if (object.ReferenceEquals(obj350.Target, instance))
					{
						this.OnDataGetObjectsRequestEvent = (PlayFabRequestEvent<GetObjectsRequest>)Delegate.Remove(this.OnDataGetObjectsRequestEvent, (PlayFabRequestEvent<GetObjectsRequest>)obj350);
					}
				}
			}
			if (this.OnDataGetObjectsResultEvent != null)
			{
				Delegate[] invocationList351 = this.OnDataGetObjectsResultEvent.GetInvocationList();
				foreach (Delegate obj351 in invocationList351)
				{
					if (object.ReferenceEquals(obj351.Target, instance))
					{
						this.OnDataGetObjectsResultEvent = (PlayFabResultEvent<GetObjectsResponse>)Delegate.Remove(this.OnDataGetObjectsResultEvent, (PlayFabResultEvent<GetObjectsResponse>)obj351);
					}
				}
			}
			if (this.OnDataInitiateFileUploadsRequestEvent != null)
			{
				Delegate[] invocationList352 = this.OnDataInitiateFileUploadsRequestEvent.GetInvocationList();
				foreach (Delegate obj352 in invocationList352)
				{
					if (object.ReferenceEquals(obj352.Target, instance))
					{
						this.OnDataInitiateFileUploadsRequestEvent = (PlayFabRequestEvent<InitiateFileUploadsRequest>)Delegate.Remove(this.OnDataInitiateFileUploadsRequestEvent, (PlayFabRequestEvent<InitiateFileUploadsRequest>)obj352);
					}
				}
			}
			if (this.OnDataInitiateFileUploadsResultEvent != null)
			{
				Delegate[] invocationList353 = this.OnDataInitiateFileUploadsResultEvent.GetInvocationList();
				foreach (Delegate obj353 in invocationList353)
				{
					if (object.ReferenceEquals(obj353.Target, instance))
					{
						this.OnDataInitiateFileUploadsResultEvent = (PlayFabResultEvent<InitiateFileUploadsResponse>)Delegate.Remove(this.OnDataInitiateFileUploadsResultEvent, (PlayFabResultEvent<InitiateFileUploadsResponse>)obj353);
					}
				}
			}
			if (this.OnDataSetObjectsRequestEvent != null)
			{
				Delegate[] invocationList354 = this.OnDataSetObjectsRequestEvent.GetInvocationList();
				foreach (Delegate obj354 in invocationList354)
				{
					if (object.ReferenceEquals(obj354.Target, instance))
					{
						this.OnDataSetObjectsRequestEvent = (PlayFabRequestEvent<SetObjectsRequest>)Delegate.Remove(this.OnDataSetObjectsRequestEvent, (PlayFabRequestEvent<SetObjectsRequest>)obj354);
					}
				}
			}
			if (this.OnDataSetObjectsResultEvent != null)
			{
				Delegate[] invocationList355 = this.OnDataSetObjectsResultEvent.GetInvocationList();
				foreach (Delegate obj355 in invocationList355)
				{
					if (object.ReferenceEquals(obj355.Target, instance))
					{
						this.OnDataSetObjectsResultEvent = (PlayFabResultEvent<SetObjectsResponse>)Delegate.Remove(this.OnDataSetObjectsResultEvent, (PlayFabResultEvent<SetObjectsResponse>)obj355);
					}
				}
			}
			if (this.OnEventsWriteEventsRequestEvent != null)
			{
				Delegate[] invocationList356 = this.OnEventsWriteEventsRequestEvent.GetInvocationList();
				foreach (Delegate obj356 in invocationList356)
				{
					if (object.ReferenceEquals(obj356.Target, instance))
					{
						this.OnEventsWriteEventsRequestEvent = (PlayFabRequestEvent<WriteEventsRequest>)Delegate.Remove(this.OnEventsWriteEventsRequestEvent, (PlayFabRequestEvent<WriteEventsRequest>)obj356);
					}
				}
			}
			if (this.OnEventsWriteEventsResultEvent != null)
			{
				Delegate[] invocationList357 = this.OnEventsWriteEventsResultEvent.GetInvocationList();
				foreach (Delegate obj357 in invocationList357)
				{
					if (object.ReferenceEquals(obj357.Target, instance))
					{
						this.OnEventsWriteEventsResultEvent = (PlayFabResultEvent<WriteEventsResponse>)Delegate.Remove(this.OnEventsWriteEventsResultEvent, (PlayFabResultEvent<WriteEventsResponse>)obj357);
					}
				}
			}
			if (this.OnEventsWriteTelemetryEventsRequestEvent != null)
			{
				Delegate[] invocationList358 = this.OnEventsWriteTelemetryEventsRequestEvent.GetInvocationList();
				foreach (Delegate obj358 in invocationList358)
				{
					if (object.ReferenceEquals(obj358.Target, instance))
					{
						this.OnEventsWriteTelemetryEventsRequestEvent = (PlayFabRequestEvent<WriteEventsRequest>)Delegate.Remove(this.OnEventsWriteTelemetryEventsRequestEvent, (PlayFabRequestEvent<WriteEventsRequest>)obj358);
					}
				}
			}
			if (this.OnEventsWriteTelemetryEventsResultEvent != null)
			{
				Delegate[] invocationList359 = this.OnEventsWriteTelemetryEventsResultEvent.GetInvocationList();
				foreach (Delegate obj359 in invocationList359)
				{
					if (object.ReferenceEquals(obj359.Target, instance))
					{
						this.OnEventsWriteTelemetryEventsResultEvent = (PlayFabResultEvent<WriteEventsResponse>)Delegate.Remove(this.OnEventsWriteTelemetryEventsResultEvent, (PlayFabResultEvent<WriteEventsResponse>)obj359);
					}
				}
			}
			if (this.OnExperimentationCreateExclusionGroupRequestEvent != null)
			{
				Delegate[] invocationList360 = this.OnExperimentationCreateExclusionGroupRequestEvent.GetInvocationList();
				foreach (Delegate obj360 in invocationList360)
				{
					if (object.ReferenceEquals(obj360.Target, instance))
					{
						this.OnExperimentationCreateExclusionGroupRequestEvent = (PlayFabRequestEvent<CreateExclusionGroupRequest>)Delegate.Remove(this.OnExperimentationCreateExclusionGroupRequestEvent, (PlayFabRequestEvent<CreateExclusionGroupRequest>)obj360);
					}
				}
			}
			if (this.OnExperimentationCreateExclusionGroupResultEvent != null)
			{
				Delegate[] invocationList361 = this.OnExperimentationCreateExclusionGroupResultEvent.GetInvocationList();
				foreach (Delegate obj361 in invocationList361)
				{
					if (object.ReferenceEquals(obj361.Target, instance))
					{
						this.OnExperimentationCreateExclusionGroupResultEvent = (PlayFabResultEvent<CreateExclusionGroupResult>)Delegate.Remove(this.OnExperimentationCreateExclusionGroupResultEvent, (PlayFabResultEvent<CreateExclusionGroupResult>)obj361);
					}
				}
			}
			if (this.OnExperimentationCreateExperimentRequestEvent != null)
			{
				Delegate[] invocationList362 = this.OnExperimentationCreateExperimentRequestEvent.GetInvocationList();
				foreach (Delegate obj362 in invocationList362)
				{
					if (object.ReferenceEquals(obj362.Target, instance))
					{
						this.OnExperimentationCreateExperimentRequestEvent = (PlayFabRequestEvent<CreateExperimentRequest>)Delegate.Remove(this.OnExperimentationCreateExperimentRequestEvent, (PlayFabRequestEvent<CreateExperimentRequest>)obj362);
					}
				}
			}
			if (this.OnExperimentationCreateExperimentResultEvent != null)
			{
				Delegate[] invocationList363 = this.OnExperimentationCreateExperimentResultEvent.GetInvocationList();
				foreach (Delegate obj363 in invocationList363)
				{
					if (object.ReferenceEquals(obj363.Target, instance))
					{
						this.OnExperimentationCreateExperimentResultEvent = (PlayFabResultEvent<CreateExperimentResult>)Delegate.Remove(this.OnExperimentationCreateExperimentResultEvent, (PlayFabResultEvent<CreateExperimentResult>)obj363);
					}
				}
			}
			if (this.OnExperimentationDeleteExclusionGroupRequestEvent != null)
			{
				Delegate[] invocationList364 = this.OnExperimentationDeleteExclusionGroupRequestEvent.GetInvocationList();
				foreach (Delegate obj364 in invocationList364)
				{
					if (object.ReferenceEquals(obj364.Target, instance))
					{
						this.OnExperimentationDeleteExclusionGroupRequestEvent = (PlayFabRequestEvent<DeleteExclusionGroupRequest>)Delegate.Remove(this.OnExperimentationDeleteExclusionGroupRequestEvent, (PlayFabRequestEvent<DeleteExclusionGroupRequest>)obj364);
					}
				}
			}
			if (this.OnExperimentationDeleteExclusionGroupResultEvent != null)
			{
				Delegate[] invocationList365 = this.OnExperimentationDeleteExclusionGroupResultEvent.GetInvocationList();
				foreach (Delegate obj365 in invocationList365)
				{
					if (object.ReferenceEquals(obj365.Target, instance))
					{
						this.OnExperimentationDeleteExclusionGroupResultEvent = (PlayFabResultEvent<PlayFab.ExperimentationModels.EmptyResponse>)Delegate.Remove(this.OnExperimentationDeleteExclusionGroupResultEvent, (PlayFabResultEvent<PlayFab.ExperimentationModels.EmptyResponse>)obj365);
					}
				}
			}
			if (this.OnExperimentationDeleteExperimentRequestEvent != null)
			{
				Delegate[] invocationList366 = this.OnExperimentationDeleteExperimentRequestEvent.GetInvocationList();
				foreach (Delegate obj366 in invocationList366)
				{
					if (object.ReferenceEquals(obj366.Target, instance))
					{
						this.OnExperimentationDeleteExperimentRequestEvent = (PlayFabRequestEvent<DeleteExperimentRequest>)Delegate.Remove(this.OnExperimentationDeleteExperimentRequestEvent, (PlayFabRequestEvent<DeleteExperimentRequest>)obj366);
					}
				}
			}
			if (this.OnExperimentationDeleteExperimentResultEvent != null)
			{
				Delegate[] invocationList367 = this.OnExperimentationDeleteExperimentResultEvent.GetInvocationList();
				foreach (Delegate obj367 in invocationList367)
				{
					if (object.ReferenceEquals(obj367.Target, instance))
					{
						this.OnExperimentationDeleteExperimentResultEvent = (PlayFabResultEvent<PlayFab.ExperimentationModels.EmptyResponse>)Delegate.Remove(this.OnExperimentationDeleteExperimentResultEvent, (PlayFabResultEvent<PlayFab.ExperimentationModels.EmptyResponse>)obj367);
					}
				}
			}
			if (this.OnExperimentationGetExclusionGroupsRequestEvent != null)
			{
				Delegate[] invocationList368 = this.OnExperimentationGetExclusionGroupsRequestEvent.GetInvocationList();
				foreach (Delegate obj368 in invocationList368)
				{
					if (object.ReferenceEquals(obj368.Target, instance))
					{
						this.OnExperimentationGetExclusionGroupsRequestEvent = (PlayFabRequestEvent<GetExclusionGroupsRequest>)Delegate.Remove(this.OnExperimentationGetExclusionGroupsRequestEvent, (PlayFabRequestEvent<GetExclusionGroupsRequest>)obj368);
					}
				}
			}
			if (this.OnExperimentationGetExclusionGroupsResultEvent != null)
			{
				Delegate[] invocationList369 = this.OnExperimentationGetExclusionGroupsResultEvent.GetInvocationList();
				foreach (Delegate obj369 in invocationList369)
				{
					if (object.ReferenceEquals(obj369.Target, instance))
					{
						this.OnExperimentationGetExclusionGroupsResultEvent = (PlayFabResultEvent<GetExclusionGroupsResult>)Delegate.Remove(this.OnExperimentationGetExclusionGroupsResultEvent, (PlayFabResultEvent<GetExclusionGroupsResult>)obj369);
					}
				}
			}
			if (this.OnExperimentationGetExclusionGroupTrafficRequestEvent != null)
			{
				Delegate[] invocationList370 = this.OnExperimentationGetExclusionGroupTrafficRequestEvent.GetInvocationList();
				foreach (Delegate obj370 in invocationList370)
				{
					if (object.ReferenceEquals(obj370.Target, instance))
					{
						this.OnExperimentationGetExclusionGroupTrafficRequestEvent = (PlayFabRequestEvent<GetExclusionGroupTrafficRequest>)Delegate.Remove(this.OnExperimentationGetExclusionGroupTrafficRequestEvent, (PlayFabRequestEvent<GetExclusionGroupTrafficRequest>)obj370);
					}
				}
			}
			if (this.OnExperimentationGetExclusionGroupTrafficResultEvent != null)
			{
				Delegate[] invocationList371 = this.OnExperimentationGetExclusionGroupTrafficResultEvent.GetInvocationList();
				foreach (Delegate obj371 in invocationList371)
				{
					if (object.ReferenceEquals(obj371.Target, instance))
					{
						this.OnExperimentationGetExclusionGroupTrafficResultEvent = (PlayFabResultEvent<GetExclusionGroupTrafficResult>)Delegate.Remove(this.OnExperimentationGetExclusionGroupTrafficResultEvent, (PlayFabResultEvent<GetExclusionGroupTrafficResult>)obj371);
					}
				}
			}
			if (this.OnExperimentationGetExperimentsRequestEvent != null)
			{
				Delegate[] invocationList372 = this.OnExperimentationGetExperimentsRequestEvent.GetInvocationList();
				foreach (Delegate obj372 in invocationList372)
				{
					if (object.ReferenceEquals(obj372.Target, instance))
					{
						this.OnExperimentationGetExperimentsRequestEvent = (PlayFabRequestEvent<GetExperimentsRequest>)Delegate.Remove(this.OnExperimentationGetExperimentsRequestEvent, (PlayFabRequestEvent<GetExperimentsRequest>)obj372);
					}
				}
			}
			if (this.OnExperimentationGetExperimentsResultEvent != null)
			{
				Delegate[] invocationList373 = this.OnExperimentationGetExperimentsResultEvent.GetInvocationList();
				foreach (Delegate obj373 in invocationList373)
				{
					if (object.ReferenceEquals(obj373.Target, instance))
					{
						this.OnExperimentationGetExperimentsResultEvent = (PlayFabResultEvent<GetExperimentsResult>)Delegate.Remove(this.OnExperimentationGetExperimentsResultEvent, (PlayFabResultEvent<GetExperimentsResult>)obj373);
					}
				}
			}
			if (this.OnExperimentationGetLatestScorecardRequestEvent != null)
			{
				Delegate[] invocationList374 = this.OnExperimentationGetLatestScorecardRequestEvent.GetInvocationList();
				foreach (Delegate obj374 in invocationList374)
				{
					if (object.ReferenceEquals(obj374.Target, instance))
					{
						this.OnExperimentationGetLatestScorecardRequestEvent = (PlayFabRequestEvent<GetLatestScorecardRequest>)Delegate.Remove(this.OnExperimentationGetLatestScorecardRequestEvent, (PlayFabRequestEvent<GetLatestScorecardRequest>)obj374);
					}
				}
			}
			if (this.OnExperimentationGetLatestScorecardResultEvent != null)
			{
				Delegate[] invocationList375 = this.OnExperimentationGetLatestScorecardResultEvent.GetInvocationList();
				foreach (Delegate obj375 in invocationList375)
				{
					if (object.ReferenceEquals(obj375.Target, instance))
					{
						this.OnExperimentationGetLatestScorecardResultEvent = (PlayFabResultEvent<GetLatestScorecardResult>)Delegate.Remove(this.OnExperimentationGetLatestScorecardResultEvent, (PlayFabResultEvent<GetLatestScorecardResult>)obj375);
					}
				}
			}
			if (this.OnExperimentationGetTreatmentAssignmentRequestEvent != null)
			{
				Delegate[] invocationList376 = this.OnExperimentationGetTreatmentAssignmentRequestEvent.GetInvocationList();
				foreach (Delegate obj376 in invocationList376)
				{
					if (object.ReferenceEquals(obj376.Target, instance))
					{
						this.OnExperimentationGetTreatmentAssignmentRequestEvent = (PlayFabRequestEvent<GetTreatmentAssignmentRequest>)Delegate.Remove(this.OnExperimentationGetTreatmentAssignmentRequestEvent, (PlayFabRequestEvent<GetTreatmentAssignmentRequest>)obj376);
					}
				}
			}
			if (this.OnExperimentationGetTreatmentAssignmentResultEvent != null)
			{
				Delegate[] invocationList377 = this.OnExperimentationGetTreatmentAssignmentResultEvent.GetInvocationList();
				foreach (Delegate obj377 in invocationList377)
				{
					if (object.ReferenceEquals(obj377.Target, instance))
					{
						this.OnExperimentationGetTreatmentAssignmentResultEvent = (PlayFabResultEvent<GetTreatmentAssignmentResult>)Delegate.Remove(this.OnExperimentationGetTreatmentAssignmentResultEvent, (PlayFabResultEvent<GetTreatmentAssignmentResult>)obj377);
					}
				}
			}
			if (this.OnExperimentationStartExperimentRequestEvent != null)
			{
				Delegate[] invocationList378 = this.OnExperimentationStartExperimentRequestEvent.GetInvocationList();
				foreach (Delegate obj378 in invocationList378)
				{
					if (object.ReferenceEquals(obj378.Target, instance))
					{
						this.OnExperimentationStartExperimentRequestEvent = (PlayFabRequestEvent<StartExperimentRequest>)Delegate.Remove(this.OnExperimentationStartExperimentRequestEvent, (PlayFabRequestEvent<StartExperimentRequest>)obj378);
					}
				}
			}
			if (this.OnExperimentationStartExperimentResultEvent != null)
			{
				Delegate[] invocationList379 = this.OnExperimentationStartExperimentResultEvent.GetInvocationList();
				foreach (Delegate obj379 in invocationList379)
				{
					if (object.ReferenceEquals(obj379.Target, instance))
					{
						this.OnExperimentationStartExperimentResultEvent = (PlayFabResultEvent<PlayFab.ExperimentationModels.EmptyResponse>)Delegate.Remove(this.OnExperimentationStartExperimentResultEvent, (PlayFabResultEvent<PlayFab.ExperimentationModels.EmptyResponse>)obj379);
					}
				}
			}
			if (this.OnExperimentationStopExperimentRequestEvent != null)
			{
				Delegate[] invocationList380 = this.OnExperimentationStopExperimentRequestEvent.GetInvocationList();
				foreach (Delegate obj380 in invocationList380)
				{
					if (object.ReferenceEquals(obj380.Target, instance))
					{
						this.OnExperimentationStopExperimentRequestEvent = (PlayFabRequestEvent<StopExperimentRequest>)Delegate.Remove(this.OnExperimentationStopExperimentRequestEvent, (PlayFabRequestEvent<StopExperimentRequest>)obj380);
					}
				}
			}
			if (this.OnExperimentationStopExperimentResultEvent != null)
			{
				Delegate[] invocationList381 = this.OnExperimentationStopExperimentResultEvent.GetInvocationList();
				foreach (Delegate obj381 in invocationList381)
				{
					if (object.ReferenceEquals(obj381.Target, instance))
					{
						this.OnExperimentationStopExperimentResultEvent = (PlayFabResultEvent<PlayFab.ExperimentationModels.EmptyResponse>)Delegate.Remove(this.OnExperimentationStopExperimentResultEvent, (PlayFabResultEvent<PlayFab.ExperimentationModels.EmptyResponse>)obj381);
					}
				}
			}
			if (this.OnExperimentationUpdateExclusionGroupRequestEvent != null)
			{
				Delegate[] invocationList382 = this.OnExperimentationUpdateExclusionGroupRequestEvent.GetInvocationList();
				foreach (Delegate obj382 in invocationList382)
				{
					if (object.ReferenceEquals(obj382.Target, instance))
					{
						this.OnExperimentationUpdateExclusionGroupRequestEvent = (PlayFabRequestEvent<UpdateExclusionGroupRequest>)Delegate.Remove(this.OnExperimentationUpdateExclusionGroupRequestEvent, (PlayFabRequestEvent<UpdateExclusionGroupRequest>)obj382);
					}
				}
			}
			if (this.OnExperimentationUpdateExclusionGroupResultEvent != null)
			{
				Delegate[] invocationList383 = this.OnExperimentationUpdateExclusionGroupResultEvent.GetInvocationList();
				foreach (Delegate obj383 in invocationList383)
				{
					if (object.ReferenceEquals(obj383.Target, instance))
					{
						this.OnExperimentationUpdateExclusionGroupResultEvent = (PlayFabResultEvent<PlayFab.ExperimentationModels.EmptyResponse>)Delegate.Remove(this.OnExperimentationUpdateExclusionGroupResultEvent, (PlayFabResultEvent<PlayFab.ExperimentationModels.EmptyResponse>)obj383);
					}
				}
			}
			if (this.OnExperimentationUpdateExperimentRequestEvent != null)
			{
				Delegate[] invocationList384 = this.OnExperimentationUpdateExperimentRequestEvent.GetInvocationList();
				foreach (Delegate obj384 in invocationList384)
				{
					if (object.ReferenceEquals(obj384.Target, instance))
					{
						this.OnExperimentationUpdateExperimentRequestEvent = (PlayFabRequestEvent<UpdateExperimentRequest>)Delegate.Remove(this.OnExperimentationUpdateExperimentRequestEvent, (PlayFabRequestEvent<UpdateExperimentRequest>)obj384);
					}
				}
			}
			if (this.OnExperimentationUpdateExperimentResultEvent != null)
			{
				Delegate[] invocationList385 = this.OnExperimentationUpdateExperimentResultEvent.GetInvocationList();
				foreach (Delegate obj385 in invocationList385)
				{
					if (object.ReferenceEquals(obj385.Target, instance))
					{
						this.OnExperimentationUpdateExperimentResultEvent = (PlayFabResultEvent<PlayFab.ExperimentationModels.EmptyResponse>)Delegate.Remove(this.OnExperimentationUpdateExperimentResultEvent, (PlayFabResultEvent<PlayFab.ExperimentationModels.EmptyResponse>)obj385);
					}
				}
			}
			if (this.OnInsightsGetDetailsRequestEvent != null)
			{
				Delegate[] invocationList386 = this.OnInsightsGetDetailsRequestEvent.GetInvocationList();
				foreach (Delegate obj386 in invocationList386)
				{
					if (object.ReferenceEquals(obj386.Target, instance))
					{
						this.OnInsightsGetDetailsRequestEvent = (PlayFabRequestEvent<InsightsEmptyRequest>)Delegate.Remove(this.OnInsightsGetDetailsRequestEvent, (PlayFabRequestEvent<InsightsEmptyRequest>)obj386);
					}
				}
			}
			if (this.OnInsightsGetDetailsResultEvent != null)
			{
				Delegate[] invocationList387 = this.OnInsightsGetDetailsResultEvent.GetInvocationList();
				foreach (Delegate obj387 in invocationList387)
				{
					if (object.ReferenceEquals(obj387.Target, instance))
					{
						this.OnInsightsGetDetailsResultEvent = (PlayFabResultEvent<InsightsGetDetailsResponse>)Delegate.Remove(this.OnInsightsGetDetailsResultEvent, (PlayFabResultEvent<InsightsGetDetailsResponse>)obj387);
					}
				}
			}
			if (this.OnInsightsGetLimitsRequestEvent != null)
			{
				Delegate[] invocationList388 = this.OnInsightsGetLimitsRequestEvent.GetInvocationList();
				foreach (Delegate obj388 in invocationList388)
				{
					if (object.ReferenceEquals(obj388.Target, instance))
					{
						this.OnInsightsGetLimitsRequestEvent = (PlayFabRequestEvent<InsightsEmptyRequest>)Delegate.Remove(this.OnInsightsGetLimitsRequestEvent, (PlayFabRequestEvent<InsightsEmptyRequest>)obj388);
					}
				}
			}
			if (this.OnInsightsGetLimitsResultEvent != null)
			{
				Delegate[] invocationList389 = this.OnInsightsGetLimitsResultEvent.GetInvocationList();
				foreach (Delegate obj389 in invocationList389)
				{
					if (object.ReferenceEquals(obj389.Target, instance))
					{
						this.OnInsightsGetLimitsResultEvent = (PlayFabResultEvent<InsightsGetLimitsResponse>)Delegate.Remove(this.OnInsightsGetLimitsResultEvent, (PlayFabResultEvent<InsightsGetLimitsResponse>)obj389);
					}
				}
			}
			if (this.OnInsightsGetOperationStatusRequestEvent != null)
			{
				Delegate[] invocationList390 = this.OnInsightsGetOperationStatusRequestEvent.GetInvocationList();
				foreach (Delegate obj390 in invocationList390)
				{
					if (object.ReferenceEquals(obj390.Target, instance))
					{
						this.OnInsightsGetOperationStatusRequestEvent = (PlayFabRequestEvent<InsightsGetOperationStatusRequest>)Delegate.Remove(this.OnInsightsGetOperationStatusRequestEvent, (PlayFabRequestEvent<InsightsGetOperationStatusRequest>)obj390);
					}
				}
			}
			if (this.OnInsightsGetOperationStatusResultEvent != null)
			{
				Delegate[] invocationList391 = this.OnInsightsGetOperationStatusResultEvent.GetInvocationList();
				foreach (Delegate obj391 in invocationList391)
				{
					if (object.ReferenceEquals(obj391.Target, instance))
					{
						this.OnInsightsGetOperationStatusResultEvent = (PlayFabResultEvent<InsightsGetOperationStatusResponse>)Delegate.Remove(this.OnInsightsGetOperationStatusResultEvent, (PlayFabResultEvent<InsightsGetOperationStatusResponse>)obj391);
					}
				}
			}
			if (this.OnInsightsGetPendingOperationsRequestEvent != null)
			{
				Delegate[] invocationList392 = this.OnInsightsGetPendingOperationsRequestEvent.GetInvocationList();
				foreach (Delegate obj392 in invocationList392)
				{
					if (object.ReferenceEquals(obj392.Target, instance))
					{
						this.OnInsightsGetPendingOperationsRequestEvent = (PlayFabRequestEvent<InsightsGetPendingOperationsRequest>)Delegate.Remove(this.OnInsightsGetPendingOperationsRequestEvent, (PlayFabRequestEvent<InsightsGetPendingOperationsRequest>)obj392);
					}
				}
			}
			if (this.OnInsightsGetPendingOperationsResultEvent != null)
			{
				Delegate[] invocationList393 = this.OnInsightsGetPendingOperationsResultEvent.GetInvocationList();
				foreach (Delegate obj393 in invocationList393)
				{
					if (object.ReferenceEquals(obj393.Target, instance))
					{
						this.OnInsightsGetPendingOperationsResultEvent = (PlayFabResultEvent<InsightsGetPendingOperationsResponse>)Delegate.Remove(this.OnInsightsGetPendingOperationsResultEvent, (PlayFabResultEvent<InsightsGetPendingOperationsResponse>)obj393);
					}
				}
			}
			if (this.OnInsightsSetPerformanceRequestEvent != null)
			{
				Delegate[] invocationList394 = this.OnInsightsSetPerformanceRequestEvent.GetInvocationList();
				foreach (Delegate obj394 in invocationList394)
				{
					if (object.ReferenceEquals(obj394.Target, instance))
					{
						this.OnInsightsSetPerformanceRequestEvent = (PlayFabRequestEvent<InsightsSetPerformanceRequest>)Delegate.Remove(this.OnInsightsSetPerformanceRequestEvent, (PlayFabRequestEvent<InsightsSetPerformanceRequest>)obj394);
					}
				}
			}
			if (this.OnInsightsSetPerformanceResultEvent != null)
			{
				Delegate[] invocationList395 = this.OnInsightsSetPerformanceResultEvent.GetInvocationList();
				foreach (Delegate obj395 in invocationList395)
				{
					if (object.ReferenceEquals(obj395.Target, instance))
					{
						this.OnInsightsSetPerformanceResultEvent = (PlayFabResultEvent<InsightsOperationResponse>)Delegate.Remove(this.OnInsightsSetPerformanceResultEvent, (PlayFabResultEvent<InsightsOperationResponse>)obj395);
					}
				}
			}
			if (this.OnInsightsSetStorageRetentionRequestEvent != null)
			{
				Delegate[] invocationList396 = this.OnInsightsSetStorageRetentionRequestEvent.GetInvocationList();
				foreach (Delegate obj396 in invocationList396)
				{
					if (object.ReferenceEquals(obj396.Target, instance))
					{
						this.OnInsightsSetStorageRetentionRequestEvent = (PlayFabRequestEvent<InsightsSetStorageRetentionRequest>)Delegate.Remove(this.OnInsightsSetStorageRetentionRequestEvent, (PlayFabRequestEvent<InsightsSetStorageRetentionRequest>)obj396);
					}
				}
			}
			if (this.OnInsightsSetStorageRetentionResultEvent != null)
			{
				Delegate[] invocationList397 = this.OnInsightsSetStorageRetentionResultEvent.GetInvocationList();
				foreach (Delegate obj397 in invocationList397)
				{
					if (object.ReferenceEquals(obj397.Target, instance))
					{
						this.OnInsightsSetStorageRetentionResultEvent = (PlayFabResultEvent<InsightsOperationResponse>)Delegate.Remove(this.OnInsightsSetStorageRetentionResultEvent, (PlayFabResultEvent<InsightsOperationResponse>)obj397);
					}
				}
			}
			if (this.OnGroupsAcceptGroupApplicationRequestEvent != null)
			{
				Delegate[] invocationList398 = this.OnGroupsAcceptGroupApplicationRequestEvent.GetInvocationList();
				foreach (Delegate obj398 in invocationList398)
				{
					if (object.ReferenceEquals(obj398.Target, instance))
					{
						this.OnGroupsAcceptGroupApplicationRequestEvent = (PlayFabRequestEvent<AcceptGroupApplicationRequest>)Delegate.Remove(this.OnGroupsAcceptGroupApplicationRequestEvent, (PlayFabRequestEvent<AcceptGroupApplicationRequest>)obj398);
					}
				}
			}
			if (this.OnGroupsAcceptGroupApplicationResultEvent != null)
			{
				Delegate[] invocationList399 = this.OnGroupsAcceptGroupApplicationResultEvent.GetInvocationList();
				foreach (Delegate obj399 in invocationList399)
				{
					if (object.ReferenceEquals(obj399.Target, instance))
					{
						this.OnGroupsAcceptGroupApplicationResultEvent = (PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)Delegate.Remove(this.OnGroupsAcceptGroupApplicationResultEvent, (PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)obj399);
					}
				}
			}
			if (this.OnGroupsAcceptGroupInvitationRequestEvent != null)
			{
				Delegate[] invocationList400 = this.OnGroupsAcceptGroupInvitationRequestEvent.GetInvocationList();
				foreach (Delegate obj400 in invocationList400)
				{
					if (object.ReferenceEquals(obj400.Target, instance))
					{
						this.OnGroupsAcceptGroupInvitationRequestEvent = (PlayFabRequestEvent<AcceptGroupInvitationRequest>)Delegate.Remove(this.OnGroupsAcceptGroupInvitationRequestEvent, (PlayFabRequestEvent<AcceptGroupInvitationRequest>)obj400);
					}
				}
			}
			if (this.OnGroupsAcceptGroupInvitationResultEvent != null)
			{
				Delegate[] invocationList401 = this.OnGroupsAcceptGroupInvitationResultEvent.GetInvocationList();
				foreach (Delegate obj401 in invocationList401)
				{
					if (object.ReferenceEquals(obj401.Target, instance))
					{
						this.OnGroupsAcceptGroupInvitationResultEvent = (PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)Delegate.Remove(this.OnGroupsAcceptGroupInvitationResultEvent, (PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)obj401);
					}
				}
			}
			if (this.OnGroupsAddMembersRequestEvent != null)
			{
				Delegate[] invocationList402 = this.OnGroupsAddMembersRequestEvent.GetInvocationList();
				foreach (Delegate obj402 in invocationList402)
				{
					if (object.ReferenceEquals(obj402.Target, instance))
					{
						this.OnGroupsAddMembersRequestEvent = (PlayFabRequestEvent<AddMembersRequest>)Delegate.Remove(this.OnGroupsAddMembersRequestEvent, (PlayFabRequestEvent<AddMembersRequest>)obj402);
					}
				}
			}
			if (this.OnGroupsAddMembersResultEvent != null)
			{
				Delegate[] invocationList403 = this.OnGroupsAddMembersResultEvent.GetInvocationList();
				foreach (Delegate obj403 in invocationList403)
				{
					if (object.ReferenceEquals(obj403.Target, instance))
					{
						this.OnGroupsAddMembersResultEvent = (PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)Delegate.Remove(this.OnGroupsAddMembersResultEvent, (PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)obj403);
					}
				}
			}
			if (this.OnGroupsApplyToGroupRequestEvent != null)
			{
				Delegate[] invocationList404 = this.OnGroupsApplyToGroupRequestEvent.GetInvocationList();
				foreach (Delegate obj404 in invocationList404)
				{
					if (object.ReferenceEquals(obj404.Target, instance))
					{
						this.OnGroupsApplyToGroupRequestEvent = (PlayFabRequestEvent<ApplyToGroupRequest>)Delegate.Remove(this.OnGroupsApplyToGroupRequestEvent, (PlayFabRequestEvent<ApplyToGroupRequest>)obj404);
					}
				}
			}
			if (this.OnGroupsApplyToGroupResultEvent != null)
			{
				Delegate[] invocationList405 = this.OnGroupsApplyToGroupResultEvent.GetInvocationList();
				foreach (Delegate obj405 in invocationList405)
				{
					if (object.ReferenceEquals(obj405.Target, instance))
					{
						this.OnGroupsApplyToGroupResultEvent = (PlayFabResultEvent<ApplyToGroupResponse>)Delegate.Remove(this.OnGroupsApplyToGroupResultEvent, (PlayFabResultEvent<ApplyToGroupResponse>)obj405);
					}
				}
			}
			if (this.OnGroupsBlockEntityRequestEvent != null)
			{
				Delegate[] invocationList406 = this.OnGroupsBlockEntityRequestEvent.GetInvocationList();
				foreach (Delegate obj406 in invocationList406)
				{
					if (object.ReferenceEquals(obj406.Target, instance))
					{
						this.OnGroupsBlockEntityRequestEvent = (PlayFabRequestEvent<BlockEntityRequest>)Delegate.Remove(this.OnGroupsBlockEntityRequestEvent, (PlayFabRequestEvent<BlockEntityRequest>)obj406);
					}
				}
			}
			if (this.OnGroupsBlockEntityResultEvent != null)
			{
				Delegate[] invocationList407 = this.OnGroupsBlockEntityResultEvent.GetInvocationList();
				foreach (Delegate obj407 in invocationList407)
				{
					if (object.ReferenceEquals(obj407.Target, instance))
					{
						this.OnGroupsBlockEntityResultEvent = (PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)Delegate.Remove(this.OnGroupsBlockEntityResultEvent, (PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)obj407);
					}
				}
			}
			if (this.OnGroupsChangeMemberRoleRequestEvent != null)
			{
				Delegate[] invocationList408 = this.OnGroupsChangeMemberRoleRequestEvent.GetInvocationList();
				foreach (Delegate obj408 in invocationList408)
				{
					if (object.ReferenceEquals(obj408.Target, instance))
					{
						this.OnGroupsChangeMemberRoleRequestEvent = (PlayFabRequestEvent<ChangeMemberRoleRequest>)Delegate.Remove(this.OnGroupsChangeMemberRoleRequestEvent, (PlayFabRequestEvent<ChangeMemberRoleRequest>)obj408);
					}
				}
			}
			if (this.OnGroupsChangeMemberRoleResultEvent != null)
			{
				Delegate[] invocationList409 = this.OnGroupsChangeMemberRoleResultEvent.GetInvocationList();
				foreach (Delegate obj409 in invocationList409)
				{
					if (object.ReferenceEquals(obj409.Target, instance))
					{
						this.OnGroupsChangeMemberRoleResultEvent = (PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)Delegate.Remove(this.OnGroupsChangeMemberRoleResultEvent, (PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)obj409);
					}
				}
			}
			if (this.OnGroupsCreateGroupRequestEvent != null)
			{
				Delegate[] invocationList410 = this.OnGroupsCreateGroupRequestEvent.GetInvocationList();
				foreach (Delegate obj410 in invocationList410)
				{
					if (object.ReferenceEquals(obj410.Target, instance))
					{
						this.OnGroupsCreateGroupRequestEvent = (PlayFabRequestEvent<CreateGroupRequest>)Delegate.Remove(this.OnGroupsCreateGroupRequestEvent, (PlayFabRequestEvent<CreateGroupRequest>)obj410);
					}
				}
			}
			if (this.OnGroupsCreateGroupResultEvent != null)
			{
				Delegate[] invocationList411 = this.OnGroupsCreateGroupResultEvent.GetInvocationList();
				foreach (Delegate obj411 in invocationList411)
				{
					if (object.ReferenceEquals(obj411.Target, instance))
					{
						this.OnGroupsCreateGroupResultEvent = (PlayFabResultEvent<CreateGroupResponse>)Delegate.Remove(this.OnGroupsCreateGroupResultEvent, (PlayFabResultEvent<CreateGroupResponse>)obj411);
					}
				}
			}
			if (this.OnGroupsCreateRoleRequestEvent != null)
			{
				Delegate[] invocationList412 = this.OnGroupsCreateRoleRequestEvent.GetInvocationList();
				foreach (Delegate obj412 in invocationList412)
				{
					if (object.ReferenceEquals(obj412.Target, instance))
					{
						this.OnGroupsCreateRoleRequestEvent = (PlayFabRequestEvent<CreateGroupRoleRequest>)Delegate.Remove(this.OnGroupsCreateRoleRequestEvent, (PlayFabRequestEvent<CreateGroupRoleRequest>)obj412);
					}
				}
			}
			if (this.OnGroupsCreateRoleResultEvent != null)
			{
				Delegate[] invocationList413 = this.OnGroupsCreateRoleResultEvent.GetInvocationList();
				foreach (Delegate obj413 in invocationList413)
				{
					if (object.ReferenceEquals(obj413.Target, instance))
					{
						this.OnGroupsCreateRoleResultEvent = (PlayFabResultEvent<CreateGroupRoleResponse>)Delegate.Remove(this.OnGroupsCreateRoleResultEvent, (PlayFabResultEvent<CreateGroupRoleResponse>)obj413);
					}
				}
			}
			if (this.OnGroupsDeleteGroupRequestEvent != null)
			{
				Delegate[] invocationList414 = this.OnGroupsDeleteGroupRequestEvent.GetInvocationList();
				foreach (Delegate obj414 in invocationList414)
				{
					if (object.ReferenceEquals(obj414.Target, instance))
					{
						this.OnGroupsDeleteGroupRequestEvent = (PlayFabRequestEvent<DeleteGroupRequest>)Delegate.Remove(this.OnGroupsDeleteGroupRequestEvent, (PlayFabRequestEvent<DeleteGroupRequest>)obj414);
					}
				}
			}
			if (this.OnGroupsDeleteGroupResultEvent != null)
			{
				Delegate[] invocationList415 = this.OnGroupsDeleteGroupResultEvent.GetInvocationList();
				foreach (Delegate obj415 in invocationList415)
				{
					if (object.ReferenceEquals(obj415.Target, instance))
					{
						this.OnGroupsDeleteGroupResultEvent = (PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)Delegate.Remove(this.OnGroupsDeleteGroupResultEvent, (PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)obj415);
					}
				}
			}
			if (this.OnGroupsDeleteRoleRequestEvent != null)
			{
				Delegate[] invocationList416 = this.OnGroupsDeleteRoleRequestEvent.GetInvocationList();
				foreach (Delegate obj416 in invocationList416)
				{
					if (object.ReferenceEquals(obj416.Target, instance))
					{
						this.OnGroupsDeleteRoleRequestEvent = (PlayFabRequestEvent<DeleteRoleRequest>)Delegate.Remove(this.OnGroupsDeleteRoleRequestEvent, (PlayFabRequestEvent<DeleteRoleRequest>)obj416);
					}
				}
			}
			if (this.OnGroupsDeleteRoleResultEvent != null)
			{
				Delegate[] invocationList417 = this.OnGroupsDeleteRoleResultEvent.GetInvocationList();
				foreach (Delegate obj417 in invocationList417)
				{
					if (object.ReferenceEquals(obj417.Target, instance))
					{
						this.OnGroupsDeleteRoleResultEvent = (PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)Delegate.Remove(this.OnGroupsDeleteRoleResultEvent, (PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)obj417);
					}
				}
			}
			if (this.OnGroupsGetGroupRequestEvent != null)
			{
				Delegate[] invocationList418 = this.OnGroupsGetGroupRequestEvent.GetInvocationList();
				foreach (Delegate obj418 in invocationList418)
				{
					if (object.ReferenceEquals(obj418.Target, instance))
					{
						this.OnGroupsGetGroupRequestEvent = (PlayFabRequestEvent<GetGroupRequest>)Delegate.Remove(this.OnGroupsGetGroupRequestEvent, (PlayFabRequestEvent<GetGroupRequest>)obj418);
					}
				}
			}
			if (this.OnGroupsGetGroupResultEvent != null)
			{
				Delegate[] invocationList419 = this.OnGroupsGetGroupResultEvent.GetInvocationList();
				foreach (Delegate obj419 in invocationList419)
				{
					if (object.ReferenceEquals(obj419.Target, instance))
					{
						this.OnGroupsGetGroupResultEvent = (PlayFabResultEvent<GetGroupResponse>)Delegate.Remove(this.OnGroupsGetGroupResultEvent, (PlayFabResultEvent<GetGroupResponse>)obj419);
					}
				}
			}
			if (this.OnGroupsInviteToGroupRequestEvent != null)
			{
				Delegate[] invocationList420 = this.OnGroupsInviteToGroupRequestEvent.GetInvocationList();
				foreach (Delegate obj420 in invocationList420)
				{
					if (object.ReferenceEquals(obj420.Target, instance))
					{
						this.OnGroupsInviteToGroupRequestEvent = (PlayFabRequestEvent<InviteToGroupRequest>)Delegate.Remove(this.OnGroupsInviteToGroupRequestEvent, (PlayFabRequestEvent<InviteToGroupRequest>)obj420);
					}
				}
			}
			if (this.OnGroupsInviteToGroupResultEvent != null)
			{
				Delegate[] invocationList421 = this.OnGroupsInviteToGroupResultEvent.GetInvocationList();
				foreach (Delegate obj421 in invocationList421)
				{
					if (object.ReferenceEquals(obj421.Target, instance))
					{
						this.OnGroupsInviteToGroupResultEvent = (PlayFabResultEvent<InviteToGroupResponse>)Delegate.Remove(this.OnGroupsInviteToGroupResultEvent, (PlayFabResultEvent<InviteToGroupResponse>)obj421);
					}
				}
			}
			if (this.OnGroupsIsMemberRequestEvent != null)
			{
				Delegate[] invocationList422 = this.OnGroupsIsMemberRequestEvent.GetInvocationList();
				foreach (Delegate obj422 in invocationList422)
				{
					if (object.ReferenceEquals(obj422.Target, instance))
					{
						this.OnGroupsIsMemberRequestEvent = (PlayFabRequestEvent<IsMemberRequest>)Delegate.Remove(this.OnGroupsIsMemberRequestEvent, (PlayFabRequestEvent<IsMemberRequest>)obj422);
					}
				}
			}
			if (this.OnGroupsIsMemberResultEvent != null)
			{
				Delegate[] invocationList423 = this.OnGroupsIsMemberResultEvent.GetInvocationList();
				foreach (Delegate obj423 in invocationList423)
				{
					if (object.ReferenceEquals(obj423.Target, instance))
					{
						this.OnGroupsIsMemberResultEvent = (PlayFabResultEvent<IsMemberResponse>)Delegate.Remove(this.OnGroupsIsMemberResultEvent, (PlayFabResultEvent<IsMemberResponse>)obj423);
					}
				}
			}
			if (this.OnGroupsListGroupApplicationsRequestEvent != null)
			{
				Delegate[] invocationList424 = this.OnGroupsListGroupApplicationsRequestEvent.GetInvocationList();
				foreach (Delegate obj424 in invocationList424)
				{
					if (object.ReferenceEquals(obj424.Target, instance))
					{
						this.OnGroupsListGroupApplicationsRequestEvent = (PlayFabRequestEvent<ListGroupApplicationsRequest>)Delegate.Remove(this.OnGroupsListGroupApplicationsRequestEvent, (PlayFabRequestEvent<ListGroupApplicationsRequest>)obj424);
					}
				}
			}
			if (this.OnGroupsListGroupApplicationsResultEvent != null)
			{
				Delegate[] invocationList425 = this.OnGroupsListGroupApplicationsResultEvent.GetInvocationList();
				foreach (Delegate obj425 in invocationList425)
				{
					if (object.ReferenceEquals(obj425.Target, instance))
					{
						this.OnGroupsListGroupApplicationsResultEvent = (PlayFabResultEvent<ListGroupApplicationsResponse>)Delegate.Remove(this.OnGroupsListGroupApplicationsResultEvent, (PlayFabResultEvent<ListGroupApplicationsResponse>)obj425);
					}
				}
			}
			if (this.OnGroupsListGroupBlocksRequestEvent != null)
			{
				Delegate[] invocationList426 = this.OnGroupsListGroupBlocksRequestEvent.GetInvocationList();
				foreach (Delegate obj426 in invocationList426)
				{
					if (object.ReferenceEquals(obj426.Target, instance))
					{
						this.OnGroupsListGroupBlocksRequestEvent = (PlayFabRequestEvent<ListGroupBlocksRequest>)Delegate.Remove(this.OnGroupsListGroupBlocksRequestEvent, (PlayFabRequestEvent<ListGroupBlocksRequest>)obj426);
					}
				}
			}
			if (this.OnGroupsListGroupBlocksResultEvent != null)
			{
				Delegate[] invocationList427 = this.OnGroupsListGroupBlocksResultEvent.GetInvocationList();
				foreach (Delegate obj427 in invocationList427)
				{
					if (object.ReferenceEquals(obj427.Target, instance))
					{
						this.OnGroupsListGroupBlocksResultEvent = (PlayFabResultEvent<ListGroupBlocksResponse>)Delegate.Remove(this.OnGroupsListGroupBlocksResultEvent, (PlayFabResultEvent<ListGroupBlocksResponse>)obj427);
					}
				}
			}
			if (this.OnGroupsListGroupInvitationsRequestEvent != null)
			{
				Delegate[] invocationList428 = this.OnGroupsListGroupInvitationsRequestEvent.GetInvocationList();
				foreach (Delegate obj428 in invocationList428)
				{
					if (object.ReferenceEquals(obj428.Target, instance))
					{
						this.OnGroupsListGroupInvitationsRequestEvent = (PlayFabRequestEvent<ListGroupInvitationsRequest>)Delegate.Remove(this.OnGroupsListGroupInvitationsRequestEvent, (PlayFabRequestEvent<ListGroupInvitationsRequest>)obj428);
					}
				}
			}
			if (this.OnGroupsListGroupInvitationsResultEvent != null)
			{
				Delegate[] invocationList429 = this.OnGroupsListGroupInvitationsResultEvent.GetInvocationList();
				foreach (Delegate obj429 in invocationList429)
				{
					if (object.ReferenceEquals(obj429.Target, instance))
					{
						this.OnGroupsListGroupInvitationsResultEvent = (PlayFabResultEvent<ListGroupInvitationsResponse>)Delegate.Remove(this.OnGroupsListGroupInvitationsResultEvent, (PlayFabResultEvent<ListGroupInvitationsResponse>)obj429);
					}
				}
			}
			if (this.OnGroupsListGroupMembersRequestEvent != null)
			{
				Delegate[] invocationList430 = this.OnGroupsListGroupMembersRequestEvent.GetInvocationList();
				foreach (Delegate obj430 in invocationList430)
				{
					if (object.ReferenceEquals(obj430.Target, instance))
					{
						this.OnGroupsListGroupMembersRequestEvent = (PlayFabRequestEvent<ListGroupMembersRequest>)Delegate.Remove(this.OnGroupsListGroupMembersRequestEvent, (PlayFabRequestEvent<ListGroupMembersRequest>)obj430);
					}
				}
			}
			if (this.OnGroupsListGroupMembersResultEvent != null)
			{
				Delegate[] invocationList431 = this.OnGroupsListGroupMembersResultEvent.GetInvocationList();
				foreach (Delegate obj431 in invocationList431)
				{
					if (object.ReferenceEquals(obj431.Target, instance))
					{
						this.OnGroupsListGroupMembersResultEvent = (PlayFabResultEvent<ListGroupMembersResponse>)Delegate.Remove(this.OnGroupsListGroupMembersResultEvent, (PlayFabResultEvent<ListGroupMembersResponse>)obj431);
					}
				}
			}
			if (this.OnGroupsListMembershipRequestEvent != null)
			{
				Delegate[] invocationList432 = this.OnGroupsListMembershipRequestEvent.GetInvocationList();
				foreach (Delegate obj432 in invocationList432)
				{
					if (object.ReferenceEquals(obj432.Target, instance))
					{
						this.OnGroupsListMembershipRequestEvent = (PlayFabRequestEvent<ListMembershipRequest>)Delegate.Remove(this.OnGroupsListMembershipRequestEvent, (PlayFabRequestEvent<ListMembershipRequest>)obj432);
					}
				}
			}
			if (this.OnGroupsListMembershipResultEvent != null)
			{
				Delegate[] invocationList433 = this.OnGroupsListMembershipResultEvent.GetInvocationList();
				foreach (Delegate obj433 in invocationList433)
				{
					if (object.ReferenceEquals(obj433.Target, instance))
					{
						this.OnGroupsListMembershipResultEvent = (PlayFabResultEvent<ListMembershipResponse>)Delegate.Remove(this.OnGroupsListMembershipResultEvent, (PlayFabResultEvent<ListMembershipResponse>)obj433);
					}
				}
			}
			if (this.OnGroupsListMembershipOpportunitiesRequestEvent != null)
			{
				Delegate[] invocationList434 = this.OnGroupsListMembershipOpportunitiesRequestEvent.GetInvocationList();
				foreach (Delegate obj434 in invocationList434)
				{
					if (object.ReferenceEquals(obj434.Target, instance))
					{
						this.OnGroupsListMembershipOpportunitiesRequestEvent = (PlayFabRequestEvent<ListMembershipOpportunitiesRequest>)Delegate.Remove(this.OnGroupsListMembershipOpportunitiesRequestEvent, (PlayFabRequestEvent<ListMembershipOpportunitiesRequest>)obj434);
					}
				}
			}
			if (this.OnGroupsListMembershipOpportunitiesResultEvent != null)
			{
				Delegate[] invocationList435 = this.OnGroupsListMembershipOpportunitiesResultEvent.GetInvocationList();
				foreach (Delegate obj435 in invocationList435)
				{
					if (object.ReferenceEquals(obj435.Target, instance))
					{
						this.OnGroupsListMembershipOpportunitiesResultEvent = (PlayFabResultEvent<ListMembershipOpportunitiesResponse>)Delegate.Remove(this.OnGroupsListMembershipOpportunitiesResultEvent, (PlayFabResultEvent<ListMembershipOpportunitiesResponse>)obj435);
					}
				}
			}
			if (this.OnGroupsRemoveGroupApplicationRequestEvent != null)
			{
				Delegate[] invocationList436 = this.OnGroupsRemoveGroupApplicationRequestEvent.GetInvocationList();
				foreach (Delegate obj436 in invocationList436)
				{
					if (object.ReferenceEquals(obj436.Target, instance))
					{
						this.OnGroupsRemoveGroupApplicationRequestEvent = (PlayFabRequestEvent<RemoveGroupApplicationRequest>)Delegate.Remove(this.OnGroupsRemoveGroupApplicationRequestEvent, (PlayFabRequestEvent<RemoveGroupApplicationRequest>)obj436);
					}
				}
			}
			if (this.OnGroupsRemoveGroupApplicationResultEvent != null)
			{
				Delegate[] invocationList437 = this.OnGroupsRemoveGroupApplicationResultEvent.GetInvocationList();
				foreach (Delegate obj437 in invocationList437)
				{
					if (object.ReferenceEquals(obj437.Target, instance))
					{
						this.OnGroupsRemoveGroupApplicationResultEvent = (PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)Delegate.Remove(this.OnGroupsRemoveGroupApplicationResultEvent, (PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)obj437);
					}
				}
			}
			if (this.OnGroupsRemoveGroupInvitationRequestEvent != null)
			{
				Delegate[] invocationList438 = this.OnGroupsRemoveGroupInvitationRequestEvent.GetInvocationList();
				foreach (Delegate obj438 in invocationList438)
				{
					if (object.ReferenceEquals(obj438.Target, instance))
					{
						this.OnGroupsRemoveGroupInvitationRequestEvent = (PlayFabRequestEvent<RemoveGroupInvitationRequest>)Delegate.Remove(this.OnGroupsRemoveGroupInvitationRequestEvent, (PlayFabRequestEvent<RemoveGroupInvitationRequest>)obj438);
					}
				}
			}
			if (this.OnGroupsRemoveGroupInvitationResultEvent != null)
			{
				Delegate[] invocationList439 = this.OnGroupsRemoveGroupInvitationResultEvent.GetInvocationList();
				foreach (Delegate obj439 in invocationList439)
				{
					if (object.ReferenceEquals(obj439.Target, instance))
					{
						this.OnGroupsRemoveGroupInvitationResultEvent = (PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)Delegate.Remove(this.OnGroupsRemoveGroupInvitationResultEvent, (PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)obj439);
					}
				}
			}
			if (this.OnGroupsRemoveMembersRequestEvent != null)
			{
				Delegate[] invocationList440 = this.OnGroupsRemoveMembersRequestEvent.GetInvocationList();
				foreach (Delegate obj440 in invocationList440)
				{
					if (object.ReferenceEquals(obj440.Target, instance))
					{
						this.OnGroupsRemoveMembersRequestEvent = (PlayFabRequestEvent<RemoveMembersRequest>)Delegate.Remove(this.OnGroupsRemoveMembersRequestEvent, (PlayFabRequestEvent<RemoveMembersRequest>)obj440);
					}
				}
			}
			if (this.OnGroupsRemoveMembersResultEvent != null)
			{
				Delegate[] invocationList441 = this.OnGroupsRemoveMembersResultEvent.GetInvocationList();
				foreach (Delegate obj441 in invocationList441)
				{
					if (object.ReferenceEquals(obj441.Target, instance))
					{
						this.OnGroupsRemoveMembersResultEvent = (PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)Delegate.Remove(this.OnGroupsRemoveMembersResultEvent, (PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)obj441);
					}
				}
			}
			if (this.OnGroupsUnblockEntityRequestEvent != null)
			{
				Delegate[] invocationList442 = this.OnGroupsUnblockEntityRequestEvent.GetInvocationList();
				foreach (Delegate obj442 in invocationList442)
				{
					if (object.ReferenceEquals(obj442.Target, instance))
					{
						this.OnGroupsUnblockEntityRequestEvent = (PlayFabRequestEvent<UnblockEntityRequest>)Delegate.Remove(this.OnGroupsUnblockEntityRequestEvent, (PlayFabRequestEvent<UnblockEntityRequest>)obj442);
					}
				}
			}
			if (this.OnGroupsUnblockEntityResultEvent != null)
			{
				Delegate[] invocationList443 = this.OnGroupsUnblockEntityResultEvent.GetInvocationList();
				foreach (Delegate obj443 in invocationList443)
				{
					if (object.ReferenceEquals(obj443.Target, instance))
					{
						this.OnGroupsUnblockEntityResultEvent = (PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)Delegate.Remove(this.OnGroupsUnblockEntityResultEvent, (PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)obj443);
					}
				}
			}
			if (this.OnGroupsUpdateGroupRequestEvent != null)
			{
				Delegate[] invocationList444 = this.OnGroupsUpdateGroupRequestEvent.GetInvocationList();
				foreach (Delegate obj444 in invocationList444)
				{
					if (object.ReferenceEquals(obj444.Target, instance))
					{
						this.OnGroupsUpdateGroupRequestEvent = (PlayFabRequestEvent<UpdateGroupRequest>)Delegate.Remove(this.OnGroupsUpdateGroupRequestEvent, (PlayFabRequestEvent<UpdateGroupRequest>)obj444);
					}
				}
			}
			if (this.OnGroupsUpdateGroupResultEvent != null)
			{
				Delegate[] invocationList445 = this.OnGroupsUpdateGroupResultEvent.GetInvocationList();
				foreach (Delegate obj445 in invocationList445)
				{
					if (object.ReferenceEquals(obj445.Target, instance))
					{
						this.OnGroupsUpdateGroupResultEvent = (PlayFabResultEvent<UpdateGroupResponse>)Delegate.Remove(this.OnGroupsUpdateGroupResultEvent, (PlayFabResultEvent<UpdateGroupResponse>)obj445);
					}
				}
			}
			if (this.OnGroupsUpdateRoleRequestEvent != null)
			{
				Delegate[] invocationList446 = this.OnGroupsUpdateRoleRequestEvent.GetInvocationList();
				foreach (Delegate obj446 in invocationList446)
				{
					if (object.ReferenceEquals(obj446.Target, instance))
					{
						this.OnGroupsUpdateRoleRequestEvent = (PlayFabRequestEvent<UpdateGroupRoleRequest>)Delegate.Remove(this.OnGroupsUpdateRoleRequestEvent, (PlayFabRequestEvent<UpdateGroupRoleRequest>)obj446);
					}
				}
			}
			if (this.OnGroupsUpdateRoleResultEvent != null)
			{
				Delegate[] invocationList447 = this.OnGroupsUpdateRoleResultEvent.GetInvocationList();
				foreach (Delegate obj447 in invocationList447)
				{
					if (object.ReferenceEquals(obj447.Target, instance))
					{
						this.OnGroupsUpdateRoleResultEvent = (PlayFabResultEvent<UpdateGroupRoleResponse>)Delegate.Remove(this.OnGroupsUpdateRoleResultEvent, (PlayFabResultEvent<UpdateGroupRoleResponse>)obj447);
					}
				}
			}
			if (this.OnLocalizationGetLanguageListRequestEvent != null)
			{
				Delegate[] invocationList448 = this.OnLocalizationGetLanguageListRequestEvent.GetInvocationList();
				foreach (Delegate obj448 in invocationList448)
				{
					if (object.ReferenceEquals(obj448.Target, instance))
					{
						this.OnLocalizationGetLanguageListRequestEvent = (PlayFabRequestEvent<GetLanguageListRequest>)Delegate.Remove(this.OnLocalizationGetLanguageListRequestEvent, (PlayFabRequestEvent<GetLanguageListRequest>)obj448);
					}
				}
			}
			if (this.OnLocalizationGetLanguageListResultEvent != null)
			{
				Delegate[] invocationList449 = this.OnLocalizationGetLanguageListResultEvent.GetInvocationList();
				foreach (Delegate obj449 in invocationList449)
				{
					if (object.ReferenceEquals(obj449.Target, instance))
					{
						this.OnLocalizationGetLanguageListResultEvent = (PlayFabResultEvent<GetLanguageListResponse>)Delegate.Remove(this.OnLocalizationGetLanguageListResultEvent, (PlayFabResultEvent<GetLanguageListResponse>)obj449);
					}
				}
			}
			if (this.OnMultiplayerCancelAllMatchmakingTicketsForPlayerRequestEvent != null)
			{
				Delegate[] invocationList450 = this.OnMultiplayerCancelAllMatchmakingTicketsForPlayerRequestEvent.GetInvocationList();
				foreach (Delegate obj450 in invocationList450)
				{
					if (object.ReferenceEquals(obj450.Target, instance))
					{
						this.OnMultiplayerCancelAllMatchmakingTicketsForPlayerRequestEvent = (PlayFabRequestEvent<CancelAllMatchmakingTicketsForPlayerRequest>)Delegate.Remove(this.OnMultiplayerCancelAllMatchmakingTicketsForPlayerRequestEvent, (PlayFabRequestEvent<CancelAllMatchmakingTicketsForPlayerRequest>)obj450);
					}
				}
			}
			if (this.OnMultiplayerCancelAllMatchmakingTicketsForPlayerResultEvent != null)
			{
				Delegate[] invocationList451 = this.OnMultiplayerCancelAllMatchmakingTicketsForPlayerResultEvent.GetInvocationList();
				foreach (Delegate obj451 in invocationList451)
				{
					if (object.ReferenceEquals(obj451.Target, instance))
					{
						this.OnMultiplayerCancelAllMatchmakingTicketsForPlayerResultEvent = (PlayFabResultEvent<CancelAllMatchmakingTicketsForPlayerResult>)Delegate.Remove(this.OnMultiplayerCancelAllMatchmakingTicketsForPlayerResultEvent, (PlayFabResultEvent<CancelAllMatchmakingTicketsForPlayerResult>)obj451);
					}
				}
			}
			if (this.OnMultiplayerCancelAllServerBackfillTicketsForPlayerRequestEvent != null)
			{
				Delegate[] invocationList452 = this.OnMultiplayerCancelAllServerBackfillTicketsForPlayerRequestEvent.GetInvocationList();
				foreach (Delegate obj452 in invocationList452)
				{
					if (object.ReferenceEquals(obj452.Target, instance))
					{
						this.OnMultiplayerCancelAllServerBackfillTicketsForPlayerRequestEvent = (PlayFabRequestEvent<CancelAllServerBackfillTicketsForPlayerRequest>)Delegate.Remove(this.OnMultiplayerCancelAllServerBackfillTicketsForPlayerRequestEvent, (PlayFabRequestEvent<CancelAllServerBackfillTicketsForPlayerRequest>)obj452);
					}
				}
			}
			if (this.OnMultiplayerCancelAllServerBackfillTicketsForPlayerResultEvent != null)
			{
				Delegate[] invocationList453 = this.OnMultiplayerCancelAllServerBackfillTicketsForPlayerResultEvent.GetInvocationList();
				foreach (Delegate obj453 in invocationList453)
				{
					if (object.ReferenceEquals(obj453.Target, instance))
					{
						this.OnMultiplayerCancelAllServerBackfillTicketsForPlayerResultEvent = (PlayFabResultEvent<CancelAllServerBackfillTicketsForPlayerResult>)Delegate.Remove(this.OnMultiplayerCancelAllServerBackfillTicketsForPlayerResultEvent, (PlayFabResultEvent<CancelAllServerBackfillTicketsForPlayerResult>)obj453);
					}
				}
			}
			if (this.OnMultiplayerCancelMatchmakingTicketRequestEvent != null)
			{
				Delegate[] invocationList454 = this.OnMultiplayerCancelMatchmakingTicketRequestEvent.GetInvocationList();
				foreach (Delegate obj454 in invocationList454)
				{
					if (object.ReferenceEquals(obj454.Target, instance))
					{
						this.OnMultiplayerCancelMatchmakingTicketRequestEvent = (PlayFabRequestEvent<CancelMatchmakingTicketRequest>)Delegate.Remove(this.OnMultiplayerCancelMatchmakingTicketRequestEvent, (PlayFabRequestEvent<CancelMatchmakingTicketRequest>)obj454);
					}
				}
			}
			if (this.OnMultiplayerCancelMatchmakingTicketResultEvent != null)
			{
				Delegate[] invocationList455 = this.OnMultiplayerCancelMatchmakingTicketResultEvent.GetInvocationList();
				foreach (Delegate obj455 in invocationList455)
				{
					if (object.ReferenceEquals(obj455.Target, instance))
					{
						this.OnMultiplayerCancelMatchmakingTicketResultEvent = (PlayFabResultEvent<CancelMatchmakingTicketResult>)Delegate.Remove(this.OnMultiplayerCancelMatchmakingTicketResultEvent, (PlayFabResultEvent<CancelMatchmakingTicketResult>)obj455);
					}
				}
			}
			if (this.OnMultiplayerCancelServerBackfillTicketRequestEvent != null)
			{
				Delegate[] invocationList456 = this.OnMultiplayerCancelServerBackfillTicketRequestEvent.GetInvocationList();
				foreach (Delegate obj456 in invocationList456)
				{
					if (object.ReferenceEquals(obj456.Target, instance))
					{
						this.OnMultiplayerCancelServerBackfillTicketRequestEvent = (PlayFabRequestEvent<CancelServerBackfillTicketRequest>)Delegate.Remove(this.OnMultiplayerCancelServerBackfillTicketRequestEvent, (PlayFabRequestEvent<CancelServerBackfillTicketRequest>)obj456);
					}
				}
			}
			if (this.OnMultiplayerCancelServerBackfillTicketResultEvent != null)
			{
				Delegate[] invocationList457 = this.OnMultiplayerCancelServerBackfillTicketResultEvent.GetInvocationList();
				foreach (Delegate obj457 in invocationList457)
				{
					if (object.ReferenceEquals(obj457.Target, instance))
					{
						this.OnMultiplayerCancelServerBackfillTicketResultEvent = (PlayFabResultEvent<CancelServerBackfillTicketResult>)Delegate.Remove(this.OnMultiplayerCancelServerBackfillTicketResultEvent, (PlayFabResultEvent<CancelServerBackfillTicketResult>)obj457);
					}
				}
			}
			if (this.OnMultiplayerCreateBuildAliasRequestEvent != null)
			{
				Delegate[] invocationList458 = this.OnMultiplayerCreateBuildAliasRequestEvent.GetInvocationList();
				foreach (Delegate obj458 in invocationList458)
				{
					if (object.ReferenceEquals(obj458.Target, instance))
					{
						this.OnMultiplayerCreateBuildAliasRequestEvent = (PlayFabRequestEvent<CreateBuildAliasRequest>)Delegate.Remove(this.OnMultiplayerCreateBuildAliasRequestEvent, (PlayFabRequestEvent<CreateBuildAliasRequest>)obj458);
					}
				}
			}
			if (this.OnMultiplayerCreateBuildAliasResultEvent != null)
			{
				Delegate[] invocationList459 = this.OnMultiplayerCreateBuildAliasResultEvent.GetInvocationList();
				foreach (Delegate obj459 in invocationList459)
				{
					if (object.ReferenceEquals(obj459.Target, instance))
					{
						this.OnMultiplayerCreateBuildAliasResultEvent = (PlayFabResultEvent<BuildAliasDetailsResponse>)Delegate.Remove(this.OnMultiplayerCreateBuildAliasResultEvent, (PlayFabResultEvent<BuildAliasDetailsResponse>)obj459);
					}
				}
			}
			if (this.OnMultiplayerCreateBuildWithCustomContainerRequestEvent != null)
			{
				Delegate[] invocationList460 = this.OnMultiplayerCreateBuildWithCustomContainerRequestEvent.GetInvocationList();
				foreach (Delegate obj460 in invocationList460)
				{
					if (object.ReferenceEquals(obj460.Target, instance))
					{
						this.OnMultiplayerCreateBuildWithCustomContainerRequestEvent = (PlayFabRequestEvent<CreateBuildWithCustomContainerRequest>)Delegate.Remove(this.OnMultiplayerCreateBuildWithCustomContainerRequestEvent, (PlayFabRequestEvent<CreateBuildWithCustomContainerRequest>)obj460);
					}
				}
			}
			if (this.OnMultiplayerCreateBuildWithCustomContainerResultEvent != null)
			{
				Delegate[] invocationList461 = this.OnMultiplayerCreateBuildWithCustomContainerResultEvent.GetInvocationList();
				foreach (Delegate obj461 in invocationList461)
				{
					if (object.ReferenceEquals(obj461.Target, instance))
					{
						this.OnMultiplayerCreateBuildWithCustomContainerResultEvent = (PlayFabResultEvent<CreateBuildWithCustomContainerResponse>)Delegate.Remove(this.OnMultiplayerCreateBuildWithCustomContainerResultEvent, (PlayFabResultEvent<CreateBuildWithCustomContainerResponse>)obj461);
					}
				}
			}
			if (this.OnMultiplayerCreateBuildWithManagedContainerRequestEvent != null)
			{
				Delegate[] invocationList462 = this.OnMultiplayerCreateBuildWithManagedContainerRequestEvent.GetInvocationList();
				foreach (Delegate obj462 in invocationList462)
				{
					if (object.ReferenceEquals(obj462.Target, instance))
					{
						this.OnMultiplayerCreateBuildWithManagedContainerRequestEvent = (PlayFabRequestEvent<CreateBuildWithManagedContainerRequest>)Delegate.Remove(this.OnMultiplayerCreateBuildWithManagedContainerRequestEvent, (PlayFabRequestEvent<CreateBuildWithManagedContainerRequest>)obj462);
					}
				}
			}
			if (this.OnMultiplayerCreateBuildWithManagedContainerResultEvent != null)
			{
				Delegate[] invocationList463 = this.OnMultiplayerCreateBuildWithManagedContainerResultEvent.GetInvocationList();
				foreach (Delegate obj463 in invocationList463)
				{
					if (object.ReferenceEquals(obj463.Target, instance))
					{
						this.OnMultiplayerCreateBuildWithManagedContainerResultEvent = (PlayFabResultEvent<CreateBuildWithManagedContainerResponse>)Delegate.Remove(this.OnMultiplayerCreateBuildWithManagedContainerResultEvent, (PlayFabResultEvent<CreateBuildWithManagedContainerResponse>)obj463);
					}
				}
			}
			if (this.OnMultiplayerCreateBuildWithProcessBasedServerRequestEvent != null)
			{
				Delegate[] invocationList464 = this.OnMultiplayerCreateBuildWithProcessBasedServerRequestEvent.GetInvocationList();
				foreach (Delegate obj464 in invocationList464)
				{
					if (object.ReferenceEquals(obj464.Target, instance))
					{
						this.OnMultiplayerCreateBuildWithProcessBasedServerRequestEvent = (PlayFabRequestEvent<CreateBuildWithProcessBasedServerRequest>)Delegate.Remove(this.OnMultiplayerCreateBuildWithProcessBasedServerRequestEvent, (PlayFabRequestEvent<CreateBuildWithProcessBasedServerRequest>)obj464);
					}
				}
			}
			if (this.OnMultiplayerCreateBuildWithProcessBasedServerResultEvent != null)
			{
				Delegate[] invocationList465 = this.OnMultiplayerCreateBuildWithProcessBasedServerResultEvent.GetInvocationList();
				foreach (Delegate obj465 in invocationList465)
				{
					if (object.ReferenceEquals(obj465.Target, instance))
					{
						this.OnMultiplayerCreateBuildWithProcessBasedServerResultEvent = (PlayFabResultEvent<CreateBuildWithProcessBasedServerResponse>)Delegate.Remove(this.OnMultiplayerCreateBuildWithProcessBasedServerResultEvent, (PlayFabResultEvent<CreateBuildWithProcessBasedServerResponse>)obj465);
					}
				}
			}
			if (this.OnMultiplayerCreateMatchmakingTicketRequestEvent != null)
			{
				Delegate[] invocationList466 = this.OnMultiplayerCreateMatchmakingTicketRequestEvent.GetInvocationList();
				foreach (Delegate obj466 in invocationList466)
				{
					if (object.ReferenceEquals(obj466.Target, instance))
					{
						this.OnMultiplayerCreateMatchmakingTicketRequestEvent = (PlayFabRequestEvent<CreateMatchmakingTicketRequest>)Delegate.Remove(this.OnMultiplayerCreateMatchmakingTicketRequestEvent, (PlayFabRequestEvent<CreateMatchmakingTicketRequest>)obj466);
					}
				}
			}
			if (this.OnMultiplayerCreateMatchmakingTicketResultEvent != null)
			{
				Delegate[] invocationList467 = this.OnMultiplayerCreateMatchmakingTicketResultEvent.GetInvocationList();
				foreach (Delegate obj467 in invocationList467)
				{
					if (object.ReferenceEquals(obj467.Target, instance))
					{
						this.OnMultiplayerCreateMatchmakingTicketResultEvent = (PlayFabResultEvent<CreateMatchmakingTicketResult>)Delegate.Remove(this.OnMultiplayerCreateMatchmakingTicketResultEvent, (PlayFabResultEvent<CreateMatchmakingTicketResult>)obj467);
					}
				}
			}
			if (this.OnMultiplayerCreateRemoteUserRequestEvent != null)
			{
				Delegate[] invocationList468 = this.OnMultiplayerCreateRemoteUserRequestEvent.GetInvocationList();
				foreach (Delegate obj468 in invocationList468)
				{
					if (object.ReferenceEquals(obj468.Target, instance))
					{
						this.OnMultiplayerCreateRemoteUserRequestEvent = (PlayFabRequestEvent<CreateRemoteUserRequest>)Delegate.Remove(this.OnMultiplayerCreateRemoteUserRequestEvent, (PlayFabRequestEvent<CreateRemoteUserRequest>)obj468);
					}
				}
			}
			if (this.OnMultiplayerCreateRemoteUserResultEvent != null)
			{
				Delegate[] invocationList469 = this.OnMultiplayerCreateRemoteUserResultEvent.GetInvocationList();
				foreach (Delegate obj469 in invocationList469)
				{
					if (object.ReferenceEquals(obj469.Target, instance))
					{
						this.OnMultiplayerCreateRemoteUserResultEvent = (PlayFabResultEvent<CreateRemoteUserResponse>)Delegate.Remove(this.OnMultiplayerCreateRemoteUserResultEvent, (PlayFabResultEvent<CreateRemoteUserResponse>)obj469);
					}
				}
			}
			if (this.OnMultiplayerCreateServerBackfillTicketRequestEvent != null)
			{
				Delegate[] invocationList470 = this.OnMultiplayerCreateServerBackfillTicketRequestEvent.GetInvocationList();
				foreach (Delegate obj470 in invocationList470)
				{
					if (object.ReferenceEquals(obj470.Target, instance))
					{
						this.OnMultiplayerCreateServerBackfillTicketRequestEvent = (PlayFabRequestEvent<CreateServerBackfillTicketRequest>)Delegate.Remove(this.OnMultiplayerCreateServerBackfillTicketRequestEvent, (PlayFabRequestEvent<CreateServerBackfillTicketRequest>)obj470);
					}
				}
			}
			if (this.OnMultiplayerCreateServerBackfillTicketResultEvent != null)
			{
				Delegate[] invocationList471 = this.OnMultiplayerCreateServerBackfillTicketResultEvent.GetInvocationList();
				foreach (Delegate obj471 in invocationList471)
				{
					if (object.ReferenceEquals(obj471.Target, instance))
					{
						this.OnMultiplayerCreateServerBackfillTicketResultEvent = (PlayFabResultEvent<CreateServerBackfillTicketResult>)Delegate.Remove(this.OnMultiplayerCreateServerBackfillTicketResultEvent, (PlayFabResultEvent<CreateServerBackfillTicketResult>)obj471);
					}
				}
			}
			if (this.OnMultiplayerCreateServerMatchmakingTicketRequestEvent != null)
			{
				Delegate[] invocationList472 = this.OnMultiplayerCreateServerMatchmakingTicketRequestEvent.GetInvocationList();
				foreach (Delegate obj472 in invocationList472)
				{
					if (object.ReferenceEquals(obj472.Target, instance))
					{
						this.OnMultiplayerCreateServerMatchmakingTicketRequestEvent = (PlayFabRequestEvent<CreateServerMatchmakingTicketRequest>)Delegate.Remove(this.OnMultiplayerCreateServerMatchmakingTicketRequestEvent, (PlayFabRequestEvent<CreateServerMatchmakingTicketRequest>)obj472);
					}
				}
			}
			if (this.OnMultiplayerCreateServerMatchmakingTicketResultEvent != null)
			{
				Delegate[] invocationList473 = this.OnMultiplayerCreateServerMatchmakingTicketResultEvent.GetInvocationList();
				foreach (Delegate obj473 in invocationList473)
				{
					if (object.ReferenceEquals(obj473.Target, instance))
					{
						this.OnMultiplayerCreateServerMatchmakingTicketResultEvent = (PlayFabResultEvent<CreateMatchmakingTicketResult>)Delegate.Remove(this.OnMultiplayerCreateServerMatchmakingTicketResultEvent, (PlayFabResultEvent<CreateMatchmakingTicketResult>)obj473);
					}
				}
			}
			if (this.OnMultiplayerDeleteAssetRequestEvent != null)
			{
				Delegate[] invocationList474 = this.OnMultiplayerDeleteAssetRequestEvent.GetInvocationList();
				foreach (Delegate obj474 in invocationList474)
				{
					if (object.ReferenceEquals(obj474.Target, instance))
					{
						this.OnMultiplayerDeleteAssetRequestEvent = (PlayFabRequestEvent<DeleteAssetRequest>)Delegate.Remove(this.OnMultiplayerDeleteAssetRequestEvent, (PlayFabRequestEvent<DeleteAssetRequest>)obj474);
					}
				}
			}
			if (this.OnMultiplayerDeleteAssetResultEvent != null)
			{
				Delegate[] invocationList475 = this.OnMultiplayerDeleteAssetResultEvent.GetInvocationList();
				foreach (Delegate obj475 in invocationList475)
				{
					if (object.ReferenceEquals(obj475.Target, instance))
					{
						this.OnMultiplayerDeleteAssetResultEvent = (PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)Delegate.Remove(this.OnMultiplayerDeleteAssetResultEvent, (PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)obj475);
					}
				}
			}
			if (this.OnMultiplayerDeleteBuildRequestEvent != null)
			{
				Delegate[] invocationList476 = this.OnMultiplayerDeleteBuildRequestEvent.GetInvocationList();
				foreach (Delegate obj476 in invocationList476)
				{
					if (object.ReferenceEquals(obj476.Target, instance))
					{
						this.OnMultiplayerDeleteBuildRequestEvent = (PlayFabRequestEvent<DeleteBuildRequest>)Delegate.Remove(this.OnMultiplayerDeleteBuildRequestEvent, (PlayFabRequestEvent<DeleteBuildRequest>)obj476);
					}
				}
			}
			if (this.OnMultiplayerDeleteBuildResultEvent != null)
			{
				Delegate[] invocationList477 = this.OnMultiplayerDeleteBuildResultEvent.GetInvocationList();
				foreach (Delegate obj477 in invocationList477)
				{
					if (object.ReferenceEquals(obj477.Target, instance))
					{
						this.OnMultiplayerDeleteBuildResultEvent = (PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)Delegate.Remove(this.OnMultiplayerDeleteBuildResultEvent, (PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)obj477);
					}
				}
			}
			if (this.OnMultiplayerDeleteBuildAliasRequestEvent != null)
			{
				Delegate[] invocationList478 = this.OnMultiplayerDeleteBuildAliasRequestEvent.GetInvocationList();
				foreach (Delegate obj478 in invocationList478)
				{
					if (object.ReferenceEquals(obj478.Target, instance))
					{
						this.OnMultiplayerDeleteBuildAliasRequestEvent = (PlayFabRequestEvent<DeleteBuildAliasRequest>)Delegate.Remove(this.OnMultiplayerDeleteBuildAliasRequestEvent, (PlayFabRequestEvent<DeleteBuildAliasRequest>)obj478);
					}
				}
			}
			if (this.OnMultiplayerDeleteBuildAliasResultEvent != null)
			{
				Delegate[] invocationList479 = this.OnMultiplayerDeleteBuildAliasResultEvent.GetInvocationList();
				foreach (Delegate obj479 in invocationList479)
				{
					if (object.ReferenceEquals(obj479.Target, instance))
					{
						this.OnMultiplayerDeleteBuildAliasResultEvent = (PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)Delegate.Remove(this.OnMultiplayerDeleteBuildAliasResultEvent, (PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)obj479);
					}
				}
			}
			if (this.OnMultiplayerDeleteBuildRegionRequestEvent != null)
			{
				Delegate[] invocationList480 = this.OnMultiplayerDeleteBuildRegionRequestEvent.GetInvocationList();
				foreach (Delegate obj480 in invocationList480)
				{
					if (object.ReferenceEquals(obj480.Target, instance))
					{
						this.OnMultiplayerDeleteBuildRegionRequestEvent = (PlayFabRequestEvent<DeleteBuildRegionRequest>)Delegate.Remove(this.OnMultiplayerDeleteBuildRegionRequestEvent, (PlayFabRequestEvent<DeleteBuildRegionRequest>)obj480);
					}
				}
			}
			if (this.OnMultiplayerDeleteBuildRegionResultEvent != null)
			{
				Delegate[] invocationList481 = this.OnMultiplayerDeleteBuildRegionResultEvent.GetInvocationList();
				foreach (Delegate obj481 in invocationList481)
				{
					if (object.ReferenceEquals(obj481.Target, instance))
					{
						this.OnMultiplayerDeleteBuildRegionResultEvent = (PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)Delegate.Remove(this.OnMultiplayerDeleteBuildRegionResultEvent, (PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)obj481);
					}
				}
			}
			if (this.OnMultiplayerDeleteCertificateRequestEvent != null)
			{
				Delegate[] invocationList482 = this.OnMultiplayerDeleteCertificateRequestEvent.GetInvocationList();
				foreach (Delegate obj482 in invocationList482)
				{
					if (object.ReferenceEquals(obj482.Target, instance))
					{
						this.OnMultiplayerDeleteCertificateRequestEvent = (PlayFabRequestEvent<DeleteCertificateRequest>)Delegate.Remove(this.OnMultiplayerDeleteCertificateRequestEvent, (PlayFabRequestEvent<DeleteCertificateRequest>)obj482);
					}
				}
			}
			if (this.OnMultiplayerDeleteCertificateResultEvent != null)
			{
				Delegate[] invocationList483 = this.OnMultiplayerDeleteCertificateResultEvent.GetInvocationList();
				foreach (Delegate obj483 in invocationList483)
				{
					if (object.ReferenceEquals(obj483.Target, instance))
					{
						this.OnMultiplayerDeleteCertificateResultEvent = (PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)Delegate.Remove(this.OnMultiplayerDeleteCertificateResultEvent, (PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)obj483);
					}
				}
			}
			if (this.OnMultiplayerDeleteContainerImageRepositoryRequestEvent != null)
			{
				Delegate[] invocationList484 = this.OnMultiplayerDeleteContainerImageRepositoryRequestEvent.GetInvocationList();
				foreach (Delegate obj484 in invocationList484)
				{
					if (object.ReferenceEquals(obj484.Target, instance))
					{
						this.OnMultiplayerDeleteContainerImageRepositoryRequestEvent = (PlayFabRequestEvent<DeleteContainerImageRequest>)Delegate.Remove(this.OnMultiplayerDeleteContainerImageRepositoryRequestEvent, (PlayFabRequestEvent<DeleteContainerImageRequest>)obj484);
					}
				}
			}
			if (this.OnMultiplayerDeleteContainerImageRepositoryResultEvent != null)
			{
				Delegate[] invocationList485 = this.OnMultiplayerDeleteContainerImageRepositoryResultEvent.GetInvocationList();
				foreach (Delegate obj485 in invocationList485)
				{
					if (object.ReferenceEquals(obj485.Target, instance))
					{
						this.OnMultiplayerDeleteContainerImageRepositoryResultEvent = (PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)Delegate.Remove(this.OnMultiplayerDeleteContainerImageRepositoryResultEvent, (PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)obj485);
					}
				}
			}
			if (this.OnMultiplayerDeleteRemoteUserRequestEvent != null)
			{
				Delegate[] invocationList486 = this.OnMultiplayerDeleteRemoteUserRequestEvent.GetInvocationList();
				foreach (Delegate obj486 in invocationList486)
				{
					if (object.ReferenceEquals(obj486.Target, instance))
					{
						this.OnMultiplayerDeleteRemoteUserRequestEvent = (PlayFabRequestEvent<DeleteRemoteUserRequest>)Delegate.Remove(this.OnMultiplayerDeleteRemoteUserRequestEvent, (PlayFabRequestEvent<DeleteRemoteUserRequest>)obj486);
					}
				}
			}
			if (this.OnMultiplayerDeleteRemoteUserResultEvent != null)
			{
				Delegate[] invocationList487 = this.OnMultiplayerDeleteRemoteUserResultEvent.GetInvocationList();
				foreach (Delegate obj487 in invocationList487)
				{
					if (object.ReferenceEquals(obj487.Target, instance))
					{
						this.OnMultiplayerDeleteRemoteUserResultEvent = (PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)Delegate.Remove(this.OnMultiplayerDeleteRemoteUserResultEvent, (PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)obj487);
					}
				}
			}
			if (this.OnMultiplayerEnableMultiplayerServersForTitleRequestEvent != null)
			{
				Delegate[] invocationList488 = this.OnMultiplayerEnableMultiplayerServersForTitleRequestEvent.GetInvocationList();
				foreach (Delegate obj488 in invocationList488)
				{
					if (object.ReferenceEquals(obj488.Target, instance))
					{
						this.OnMultiplayerEnableMultiplayerServersForTitleRequestEvent = (PlayFabRequestEvent<EnableMultiplayerServersForTitleRequest>)Delegate.Remove(this.OnMultiplayerEnableMultiplayerServersForTitleRequestEvent, (PlayFabRequestEvent<EnableMultiplayerServersForTitleRequest>)obj488);
					}
				}
			}
			if (this.OnMultiplayerEnableMultiplayerServersForTitleResultEvent != null)
			{
				Delegate[] invocationList489 = this.OnMultiplayerEnableMultiplayerServersForTitleResultEvent.GetInvocationList();
				foreach (Delegate obj489 in invocationList489)
				{
					if (object.ReferenceEquals(obj489.Target, instance))
					{
						this.OnMultiplayerEnableMultiplayerServersForTitleResultEvent = (PlayFabResultEvent<EnableMultiplayerServersForTitleResponse>)Delegate.Remove(this.OnMultiplayerEnableMultiplayerServersForTitleResultEvent, (PlayFabResultEvent<EnableMultiplayerServersForTitleResponse>)obj489);
					}
				}
			}
			if (this.OnMultiplayerGetAssetUploadUrlRequestEvent != null)
			{
				Delegate[] invocationList490 = this.OnMultiplayerGetAssetUploadUrlRequestEvent.GetInvocationList();
				foreach (Delegate obj490 in invocationList490)
				{
					if (object.ReferenceEquals(obj490.Target, instance))
					{
						this.OnMultiplayerGetAssetUploadUrlRequestEvent = (PlayFabRequestEvent<GetAssetUploadUrlRequest>)Delegate.Remove(this.OnMultiplayerGetAssetUploadUrlRequestEvent, (PlayFabRequestEvent<GetAssetUploadUrlRequest>)obj490);
					}
				}
			}
			if (this.OnMultiplayerGetAssetUploadUrlResultEvent != null)
			{
				Delegate[] invocationList491 = this.OnMultiplayerGetAssetUploadUrlResultEvent.GetInvocationList();
				foreach (Delegate obj491 in invocationList491)
				{
					if (object.ReferenceEquals(obj491.Target, instance))
					{
						this.OnMultiplayerGetAssetUploadUrlResultEvent = (PlayFabResultEvent<GetAssetUploadUrlResponse>)Delegate.Remove(this.OnMultiplayerGetAssetUploadUrlResultEvent, (PlayFabResultEvent<GetAssetUploadUrlResponse>)obj491);
					}
				}
			}
			if (this.OnMultiplayerGetBuildRequestEvent != null)
			{
				Delegate[] invocationList492 = this.OnMultiplayerGetBuildRequestEvent.GetInvocationList();
				foreach (Delegate obj492 in invocationList492)
				{
					if (object.ReferenceEquals(obj492.Target, instance))
					{
						this.OnMultiplayerGetBuildRequestEvent = (PlayFabRequestEvent<GetBuildRequest>)Delegate.Remove(this.OnMultiplayerGetBuildRequestEvent, (PlayFabRequestEvent<GetBuildRequest>)obj492);
					}
				}
			}
			if (this.OnMultiplayerGetBuildResultEvent != null)
			{
				Delegate[] invocationList493 = this.OnMultiplayerGetBuildResultEvent.GetInvocationList();
				foreach (Delegate obj493 in invocationList493)
				{
					if (object.ReferenceEquals(obj493.Target, instance))
					{
						this.OnMultiplayerGetBuildResultEvent = (PlayFabResultEvent<GetBuildResponse>)Delegate.Remove(this.OnMultiplayerGetBuildResultEvent, (PlayFabResultEvent<GetBuildResponse>)obj493);
					}
				}
			}
			if (this.OnMultiplayerGetBuildAliasRequestEvent != null)
			{
				Delegate[] invocationList494 = this.OnMultiplayerGetBuildAliasRequestEvent.GetInvocationList();
				foreach (Delegate obj494 in invocationList494)
				{
					if (object.ReferenceEquals(obj494.Target, instance))
					{
						this.OnMultiplayerGetBuildAliasRequestEvent = (PlayFabRequestEvent<GetBuildAliasRequest>)Delegate.Remove(this.OnMultiplayerGetBuildAliasRequestEvent, (PlayFabRequestEvent<GetBuildAliasRequest>)obj494);
					}
				}
			}
			if (this.OnMultiplayerGetBuildAliasResultEvent != null)
			{
				Delegate[] invocationList495 = this.OnMultiplayerGetBuildAliasResultEvent.GetInvocationList();
				foreach (Delegate obj495 in invocationList495)
				{
					if (object.ReferenceEquals(obj495.Target, instance))
					{
						this.OnMultiplayerGetBuildAliasResultEvent = (PlayFabResultEvent<BuildAliasDetailsResponse>)Delegate.Remove(this.OnMultiplayerGetBuildAliasResultEvent, (PlayFabResultEvent<BuildAliasDetailsResponse>)obj495);
					}
				}
			}
			if (this.OnMultiplayerGetContainerRegistryCredentialsRequestEvent != null)
			{
				Delegate[] invocationList496 = this.OnMultiplayerGetContainerRegistryCredentialsRequestEvent.GetInvocationList();
				foreach (Delegate obj496 in invocationList496)
				{
					if (object.ReferenceEquals(obj496.Target, instance))
					{
						this.OnMultiplayerGetContainerRegistryCredentialsRequestEvent = (PlayFabRequestEvent<GetContainerRegistryCredentialsRequest>)Delegate.Remove(this.OnMultiplayerGetContainerRegistryCredentialsRequestEvent, (PlayFabRequestEvent<GetContainerRegistryCredentialsRequest>)obj496);
					}
				}
			}
			if (this.OnMultiplayerGetContainerRegistryCredentialsResultEvent != null)
			{
				Delegate[] invocationList497 = this.OnMultiplayerGetContainerRegistryCredentialsResultEvent.GetInvocationList();
				foreach (Delegate obj497 in invocationList497)
				{
					if (object.ReferenceEquals(obj497.Target, instance))
					{
						this.OnMultiplayerGetContainerRegistryCredentialsResultEvent = (PlayFabResultEvent<GetContainerRegistryCredentialsResponse>)Delegate.Remove(this.OnMultiplayerGetContainerRegistryCredentialsResultEvent, (PlayFabResultEvent<GetContainerRegistryCredentialsResponse>)obj497);
					}
				}
			}
			if (this.OnMultiplayerGetMatchRequestEvent != null)
			{
				Delegate[] invocationList498 = this.OnMultiplayerGetMatchRequestEvent.GetInvocationList();
				foreach (Delegate obj498 in invocationList498)
				{
					if (object.ReferenceEquals(obj498.Target, instance))
					{
						this.OnMultiplayerGetMatchRequestEvent = (PlayFabRequestEvent<GetMatchRequest>)Delegate.Remove(this.OnMultiplayerGetMatchRequestEvent, (PlayFabRequestEvent<GetMatchRequest>)obj498);
					}
				}
			}
			if (this.OnMultiplayerGetMatchResultEvent != null)
			{
				Delegate[] invocationList499 = this.OnMultiplayerGetMatchResultEvent.GetInvocationList();
				foreach (Delegate obj499 in invocationList499)
				{
					if (object.ReferenceEquals(obj499.Target, instance))
					{
						this.OnMultiplayerGetMatchResultEvent = (PlayFabResultEvent<GetMatchResult>)Delegate.Remove(this.OnMultiplayerGetMatchResultEvent, (PlayFabResultEvent<GetMatchResult>)obj499);
					}
				}
			}
			if (this.OnMultiplayerGetMatchmakingQueueRequestEvent != null)
			{
				Delegate[] invocationList500 = this.OnMultiplayerGetMatchmakingQueueRequestEvent.GetInvocationList();
				foreach (Delegate obj500 in invocationList500)
				{
					if (object.ReferenceEquals(obj500.Target, instance))
					{
						this.OnMultiplayerGetMatchmakingQueueRequestEvent = (PlayFabRequestEvent<GetMatchmakingQueueRequest>)Delegate.Remove(this.OnMultiplayerGetMatchmakingQueueRequestEvent, (PlayFabRequestEvent<GetMatchmakingQueueRequest>)obj500);
					}
				}
			}
			if (this.OnMultiplayerGetMatchmakingQueueResultEvent != null)
			{
				Delegate[] invocationList501 = this.OnMultiplayerGetMatchmakingQueueResultEvent.GetInvocationList();
				foreach (Delegate obj501 in invocationList501)
				{
					if (object.ReferenceEquals(obj501.Target, instance))
					{
						this.OnMultiplayerGetMatchmakingQueueResultEvent = (PlayFabResultEvent<GetMatchmakingQueueResult>)Delegate.Remove(this.OnMultiplayerGetMatchmakingQueueResultEvent, (PlayFabResultEvent<GetMatchmakingQueueResult>)obj501);
					}
				}
			}
			if (this.OnMultiplayerGetMatchmakingTicketRequestEvent != null)
			{
				Delegate[] invocationList502 = this.OnMultiplayerGetMatchmakingTicketRequestEvent.GetInvocationList();
				foreach (Delegate obj502 in invocationList502)
				{
					if (object.ReferenceEquals(obj502.Target, instance))
					{
						this.OnMultiplayerGetMatchmakingTicketRequestEvent = (PlayFabRequestEvent<GetMatchmakingTicketRequest>)Delegate.Remove(this.OnMultiplayerGetMatchmakingTicketRequestEvent, (PlayFabRequestEvent<GetMatchmakingTicketRequest>)obj502);
					}
				}
			}
			if (this.OnMultiplayerGetMatchmakingTicketResultEvent != null)
			{
				Delegate[] invocationList503 = this.OnMultiplayerGetMatchmakingTicketResultEvent.GetInvocationList();
				foreach (Delegate obj503 in invocationList503)
				{
					if (object.ReferenceEquals(obj503.Target, instance))
					{
						this.OnMultiplayerGetMatchmakingTicketResultEvent = (PlayFabResultEvent<GetMatchmakingTicketResult>)Delegate.Remove(this.OnMultiplayerGetMatchmakingTicketResultEvent, (PlayFabResultEvent<GetMatchmakingTicketResult>)obj503);
					}
				}
			}
			if (this.OnMultiplayerGetMultiplayerServerDetailsRequestEvent != null)
			{
				Delegate[] invocationList504 = this.OnMultiplayerGetMultiplayerServerDetailsRequestEvent.GetInvocationList();
				foreach (Delegate obj504 in invocationList504)
				{
					if (object.ReferenceEquals(obj504.Target, instance))
					{
						this.OnMultiplayerGetMultiplayerServerDetailsRequestEvent = (PlayFabRequestEvent<GetMultiplayerServerDetailsRequest>)Delegate.Remove(this.OnMultiplayerGetMultiplayerServerDetailsRequestEvent, (PlayFabRequestEvent<GetMultiplayerServerDetailsRequest>)obj504);
					}
				}
			}
			if (this.OnMultiplayerGetMultiplayerServerDetailsResultEvent != null)
			{
				Delegate[] invocationList505 = this.OnMultiplayerGetMultiplayerServerDetailsResultEvent.GetInvocationList();
				foreach (Delegate obj505 in invocationList505)
				{
					if (object.ReferenceEquals(obj505.Target, instance))
					{
						this.OnMultiplayerGetMultiplayerServerDetailsResultEvent = (PlayFabResultEvent<GetMultiplayerServerDetailsResponse>)Delegate.Remove(this.OnMultiplayerGetMultiplayerServerDetailsResultEvent, (PlayFabResultEvent<GetMultiplayerServerDetailsResponse>)obj505);
					}
				}
			}
			if (this.OnMultiplayerGetMultiplayerServerLogsRequestEvent != null)
			{
				Delegate[] invocationList506 = this.OnMultiplayerGetMultiplayerServerLogsRequestEvent.GetInvocationList();
				foreach (Delegate obj506 in invocationList506)
				{
					if (object.ReferenceEquals(obj506.Target, instance))
					{
						this.OnMultiplayerGetMultiplayerServerLogsRequestEvent = (PlayFabRequestEvent<GetMultiplayerServerLogsRequest>)Delegate.Remove(this.OnMultiplayerGetMultiplayerServerLogsRequestEvent, (PlayFabRequestEvent<GetMultiplayerServerLogsRequest>)obj506);
					}
				}
			}
			if (this.OnMultiplayerGetMultiplayerServerLogsResultEvent != null)
			{
				Delegate[] invocationList507 = this.OnMultiplayerGetMultiplayerServerLogsResultEvent.GetInvocationList();
				foreach (Delegate obj507 in invocationList507)
				{
					if (object.ReferenceEquals(obj507.Target, instance))
					{
						this.OnMultiplayerGetMultiplayerServerLogsResultEvent = (PlayFabResultEvent<GetMultiplayerServerLogsResponse>)Delegate.Remove(this.OnMultiplayerGetMultiplayerServerLogsResultEvent, (PlayFabResultEvent<GetMultiplayerServerLogsResponse>)obj507);
					}
				}
			}
			if (this.OnMultiplayerGetMultiplayerSessionLogsBySessionIdRequestEvent != null)
			{
				Delegate[] invocationList508 = this.OnMultiplayerGetMultiplayerSessionLogsBySessionIdRequestEvent.GetInvocationList();
				foreach (Delegate obj508 in invocationList508)
				{
					if (object.ReferenceEquals(obj508.Target, instance))
					{
						this.OnMultiplayerGetMultiplayerSessionLogsBySessionIdRequestEvent = (PlayFabRequestEvent<GetMultiplayerSessionLogsBySessionIdRequest>)Delegate.Remove(this.OnMultiplayerGetMultiplayerSessionLogsBySessionIdRequestEvent, (PlayFabRequestEvent<GetMultiplayerSessionLogsBySessionIdRequest>)obj508);
					}
				}
			}
			if (this.OnMultiplayerGetMultiplayerSessionLogsBySessionIdResultEvent != null)
			{
				Delegate[] invocationList509 = this.OnMultiplayerGetMultiplayerSessionLogsBySessionIdResultEvent.GetInvocationList();
				foreach (Delegate obj509 in invocationList509)
				{
					if (object.ReferenceEquals(obj509.Target, instance))
					{
						this.OnMultiplayerGetMultiplayerSessionLogsBySessionIdResultEvent = (PlayFabResultEvent<GetMultiplayerServerLogsResponse>)Delegate.Remove(this.OnMultiplayerGetMultiplayerSessionLogsBySessionIdResultEvent, (PlayFabResultEvent<GetMultiplayerServerLogsResponse>)obj509);
					}
				}
			}
			if (this.OnMultiplayerGetQueueStatisticsRequestEvent != null)
			{
				Delegate[] invocationList510 = this.OnMultiplayerGetQueueStatisticsRequestEvent.GetInvocationList();
				foreach (Delegate obj510 in invocationList510)
				{
					if (object.ReferenceEquals(obj510.Target, instance))
					{
						this.OnMultiplayerGetQueueStatisticsRequestEvent = (PlayFabRequestEvent<GetQueueStatisticsRequest>)Delegate.Remove(this.OnMultiplayerGetQueueStatisticsRequestEvent, (PlayFabRequestEvent<GetQueueStatisticsRequest>)obj510);
					}
				}
			}
			if (this.OnMultiplayerGetQueueStatisticsResultEvent != null)
			{
				Delegate[] invocationList511 = this.OnMultiplayerGetQueueStatisticsResultEvent.GetInvocationList();
				foreach (Delegate obj511 in invocationList511)
				{
					if (object.ReferenceEquals(obj511.Target, instance))
					{
						this.OnMultiplayerGetQueueStatisticsResultEvent = (PlayFabResultEvent<GetQueueStatisticsResult>)Delegate.Remove(this.OnMultiplayerGetQueueStatisticsResultEvent, (PlayFabResultEvent<GetQueueStatisticsResult>)obj511);
					}
				}
			}
			if (this.OnMultiplayerGetRemoteLoginEndpointRequestEvent != null)
			{
				Delegate[] invocationList512 = this.OnMultiplayerGetRemoteLoginEndpointRequestEvent.GetInvocationList();
				foreach (Delegate obj512 in invocationList512)
				{
					if (object.ReferenceEquals(obj512.Target, instance))
					{
						this.OnMultiplayerGetRemoteLoginEndpointRequestEvent = (PlayFabRequestEvent<GetRemoteLoginEndpointRequest>)Delegate.Remove(this.OnMultiplayerGetRemoteLoginEndpointRequestEvent, (PlayFabRequestEvent<GetRemoteLoginEndpointRequest>)obj512);
					}
				}
			}
			if (this.OnMultiplayerGetRemoteLoginEndpointResultEvent != null)
			{
				Delegate[] invocationList513 = this.OnMultiplayerGetRemoteLoginEndpointResultEvent.GetInvocationList();
				foreach (Delegate obj513 in invocationList513)
				{
					if (object.ReferenceEquals(obj513.Target, instance))
					{
						this.OnMultiplayerGetRemoteLoginEndpointResultEvent = (PlayFabResultEvent<GetRemoteLoginEndpointResponse>)Delegate.Remove(this.OnMultiplayerGetRemoteLoginEndpointResultEvent, (PlayFabResultEvent<GetRemoteLoginEndpointResponse>)obj513);
					}
				}
			}
			if (this.OnMultiplayerGetServerBackfillTicketRequestEvent != null)
			{
				Delegate[] invocationList514 = this.OnMultiplayerGetServerBackfillTicketRequestEvent.GetInvocationList();
				foreach (Delegate obj514 in invocationList514)
				{
					if (object.ReferenceEquals(obj514.Target, instance))
					{
						this.OnMultiplayerGetServerBackfillTicketRequestEvent = (PlayFabRequestEvent<GetServerBackfillTicketRequest>)Delegate.Remove(this.OnMultiplayerGetServerBackfillTicketRequestEvent, (PlayFabRequestEvent<GetServerBackfillTicketRequest>)obj514);
					}
				}
			}
			if (this.OnMultiplayerGetServerBackfillTicketResultEvent != null)
			{
				Delegate[] invocationList515 = this.OnMultiplayerGetServerBackfillTicketResultEvent.GetInvocationList();
				foreach (Delegate obj515 in invocationList515)
				{
					if (object.ReferenceEquals(obj515.Target, instance))
					{
						this.OnMultiplayerGetServerBackfillTicketResultEvent = (PlayFabResultEvent<GetServerBackfillTicketResult>)Delegate.Remove(this.OnMultiplayerGetServerBackfillTicketResultEvent, (PlayFabResultEvent<GetServerBackfillTicketResult>)obj515);
					}
				}
			}
			if (this.OnMultiplayerGetTitleEnabledForMultiplayerServersStatusRequestEvent != null)
			{
				Delegate[] invocationList516 = this.OnMultiplayerGetTitleEnabledForMultiplayerServersStatusRequestEvent.GetInvocationList();
				foreach (Delegate obj516 in invocationList516)
				{
					if (object.ReferenceEquals(obj516.Target, instance))
					{
						this.OnMultiplayerGetTitleEnabledForMultiplayerServersStatusRequestEvent = (PlayFabRequestEvent<GetTitleEnabledForMultiplayerServersStatusRequest>)Delegate.Remove(this.OnMultiplayerGetTitleEnabledForMultiplayerServersStatusRequestEvent, (PlayFabRequestEvent<GetTitleEnabledForMultiplayerServersStatusRequest>)obj516);
					}
				}
			}
			if (this.OnMultiplayerGetTitleEnabledForMultiplayerServersStatusResultEvent != null)
			{
				Delegate[] invocationList517 = this.OnMultiplayerGetTitleEnabledForMultiplayerServersStatusResultEvent.GetInvocationList();
				foreach (Delegate obj517 in invocationList517)
				{
					if (object.ReferenceEquals(obj517.Target, instance))
					{
						this.OnMultiplayerGetTitleEnabledForMultiplayerServersStatusResultEvent = (PlayFabResultEvent<GetTitleEnabledForMultiplayerServersStatusResponse>)Delegate.Remove(this.OnMultiplayerGetTitleEnabledForMultiplayerServersStatusResultEvent, (PlayFabResultEvent<GetTitleEnabledForMultiplayerServersStatusResponse>)obj517);
					}
				}
			}
			if (this.OnMultiplayerGetTitleMultiplayerServersQuotasRequestEvent != null)
			{
				Delegate[] invocationList518 = this.OnMultiplayerGetTitleMultiplayerServersQuotasRequestEvent.GetInvocationList();
				foreach (Delegate obj518 in invocationList518)
				{
					if (object.ReferenceEquals(obj518.Target, instance))
					{
						this.OnMultiplayerGetTitleMultiplayerServersQuotasRequestEvent = (PlayFabRequestEvent<GetTitleMultiplayerServersQuotasRequest>)Delegate.Remove(this.OnMultiplayerGetTitleMultiplayerServersQuotasRequestEvent, (PlayFabRequestEvent<GetTitleMultiplayerServersQuotasRequest>)obj518);
					}
				}
			}
			if (this.OnMultiplayerGetTitleMultiplayerServersQuotasResultEvent != null)
			{
				Delegate[] invocationList519 = this.OnMultiplayerGetTitleMultiplayerServersQuotasResultEvent.GetInvocationList();
				foreach (Delegate obj519 in invocationList519)
				{
					if (object.ReferenceEquals(obj519.Target, instance))
					{
						this.OnMultiplayerGetTitleMultiplayerServersQuotasResultEvent = (PlayFabResultEvent<GetTitleMultiplayerServersQuotasResponse>)Delegate.Remove(this.OnMultiplayerGetTitleMultiplayerServersQuotasResultEvent, (PlayFabResultEvent<GetTitleMultiplayerServersQuotasResponse>)obj519);
					}
				}
			}
			if (this.OnMultiplayerJoinMatchmakingTicketRequestEvent != null)
			{
				Delegate[] invocationList520 = this.OnMultiplayerJoinMatchmakingTicketRequestEvent.GetInvocationList();
				foreach (Delegate obj520 in invocationList520)
				{
					if (object.ReferenceEquals(obj520.Target, instance))
					{
						this.OnMultiplayerJoinMatchmakingTicketRequestEvent = (PlayFabRequestEvent<JoinMatchmakingTicketRequest>)Delegate.Remove(this.OnMultiplayerJoinMatchmakingTicketRequestEvent, (PlayFabRequestEvent<JoinMatchmakingTicketRequest>)obj520);
					}
				}
			}
			if (this.OnMultiplayerJoinMatchmakingTicketResultEvent != null)
			{
				Delegate[] invocationList521 = this.OnMultiplayerJoinMatchmakingTicketResultEvent.GetInvocationList();
				foreach (Delegate obj521 in invocationList521)
				{
					if (object.ReferenceEquals(obj521.Target, instance))
					{
						this.OnMultiplayerJoinMatchmakingTicketResultEvent = (PlayFabResultEvent<JoinMatchmakingTicketResult>)Delegate.Remove(this.OnMultiplayerJoinMatchmakingTicketResultEvent, (PlayFabResultEvent<JoinMatchmakingTicketResult>)obj521);
					}
				}
			}
			if (this.OnMultiplayerListArchivedMultiplayerServersRequestEvent != null)
			{
				Delegate[] invocationList522 = this.OnMultiplayerListArchivedMultiplayerServersRequestEvent.GetInvocationList();
				foreach (Delegate obj522 in invocationList522)
				{
					if (object.ReferenceEquals(obj522.Target, instance))
					{
						this.OnMultiplayerListArchivedMultiplayerServersRequestEvent = (PlayFabRequestEvent<ListMultiplayerServersRequest>)Delegate.Remove(this.OnMultiplayerListArchivedMultiplayerServersRequestEvent, (PlayFabRequestEvent<ListMultiplayerServersRequest>)obj522);
					}
				}
			}
			if (this.OnMultiplayerListArchivedMultiplayerServersResultEvent != null)
			{
				Delegate[] invocationList523 = this.OnMultiplayerListArchivedMultiplayerServersResultEvent.GetInvocationList();
				foreach (Delegate obj523 in invocationList523)
				{
					if (object.ReferenceEquals(obj523.Target, instance))
					{
						this.OnMultiplayerListArchivedMultiplayerServersResultEvent = (PlayFabResultEvent<ListMultiplayerServersResponse>)Delegate.Remove(this.OnMultiplayerListArchivedMultiplayerServersResultEvent, (PlayFabResultEvent<ListMultiplayerServersResponse>)obj523);
					}
				}
			}
			if (this.OnMultiplayerListAssetSummariesRequestEvent != null)
			{
				Delegate[] invocationList524 = this.OnMultiplayerListAssetSummariesRequestEvent.GetInvocationList();
				foreach (Delegate obj524 in invocationList524)
				{
					if (object.ReferenceEquals(obj524.Target, instance))
					{
						this.OnMultiplayerListAssetSummariesRequestEvent = (PlayFabRequestEvent<ListAssetSummariesRequest>)Delegate.Remove(this.OnMultiplayerListAssetSummariesRequestEvent, (PlayFabRequestEvent<ListAssetSummariesRequest>)obj524);
					}
				}
			}
			if (this.OnMultiplayerListAssetSummariesResultEvent != null)
			{
				Delegate[] invocationList525 = this.OnMultiplayerListAssetSummariesResultEvent.GetInvocationList();
				foreach (Delegate obj525 in invocationList525)
				{
					if (object.ReferenceEquals(obj525.Target, instance))
					{
						this.OnMultiplayerListAssetSummariesResultEvent = (PlayFabResultEvent<ListAssetSummariesResponse>)Delegate.Remove(this.OnMultiplayerListAssetSummariesResultEvent, (PlayFabResultEvent<ListAssetSummariesResponse>)obj525);
					}
				}
			}
			if (this.OnMultiplayerListBuildAliasesRequestEvent != null)
			{
				Delegate[] invocationList526 = this.OnMultiplayerListBuildAliasesRequestEvent.GetInvocationList();
				foreach (Delegate obj526 in invocationList526)
				{
					if (object.ReferenceEquals(obj526.Target, instance))
					{
						this.OnMultiplayerListBuildAliasesRequestEvent = (PlayFabRequestEvent<MultiplayerEmptyRequest>)Delegate.Remove(this.OnMultiplayerListBuildAliasesRequestEvent, (PlayFabRequestEvent<MultiplayerEmptyRequest>)obj526);
					}
				}
			}
			if (this.OnMultiplayerListBuildAliasesResultEvent != null)
			{
				Delegate[] invocationList527 = this.OnMultiplayerListBuildAliasesResultEvent.GetInvocationList();
				foreach (Delegate obj527 in invocationList527)
				{
					if (object.ReferenceEquals(obj527.Target, instance))
					{
						this.OnMultiplayerListBuildAliasesResultEvent = (PlayFabResultEvent<ListBuildAliasesForTitleResponse>)Delegate.Remove(this.OnMultiplayerListBuildAliasesResultEvent, (PlayFabResultEvent<ListBuildAliasesForTitleResponse>)obj527);
					}
				}
			}
			if (this.OnMultiplayerListBuildSummariesRequestEvent != null)
			{
				Delegate[] invocationList528 = this.OnMultiplayerListBuildSummariesRequestEvent.GetInvocationList();
				foreach (Delegate obj528 in invocationList528)
				{
					if (object.ReferenceEquals(obj528.Target, instance))
					{
						this.OnMultiplayerListBuildSummariesRequestEvent = (PlayFabRequestEvent<ListBuildSummariesRequest>)Delegate.Remove(this.OnMultiplayerListBuildSummariesRequestEvent, (PlayFabRequestEvent<ListBuildSummariesRequest>)obj528);
					}
				}
			}
			if (this.OnMultiplayerListBuildSummariesResultEvent != null)
			{
				Delegate[] invocationList529 = this.OnMultiplayerListBuildSummariesResultEvent.GetInvocationList();
				foreach (Delegate obj529 in invocationList529)
				{
					if (object.ReferenceEquals(obj529.Target, instance))
					{
						this.OnMultiplayerListBuildSummariesResultEvent = (PlayFabResultEvent<ListBuildSummariesResponse>)Delegate.Remove(this.OnMultiplayerListBuildSummariesResultEvent, (PlayFabResultEvent<ListBuildSummariesResponse>)obj529);
					}
				}
			}
			if (this.OnMultiplayerListCertificateSummariesRequestEvent != null)
			{
				Delegate[] invocationList530 = this.OnMultiplayerListCertificateSummariesRequestEvent.GetInvocationList();
				foreach (Delegate obj530 in invocationList530)
				{
					if (object.ReferenceEquals(obj530.Target, instance))
					{
						this.OnMultiplayerListCertificateSummariesRequestEvent = (PlayFabRequestEvent<ListCertificateSummariesRequest>)Delegate.Remove(this.OnMultiplayerListCertificateSummariesRequestEvent, (PlayFabRequestEvent<ListCertificateSummariesRequest>)obj530);
					}
				}
			}
			if (this.OnMultiplayerListCertificateSummariesResultEvent != null)
			{
				Delegate[] invocationList531 = this.OnMultiplayerListCertificateSummariesResultEvent.GetInvocationList();
				foreach (Delegate obj531 in invocationList531)
				{
					if (object.ReferenceEquals(obj531.Target, instance))
					{
						this.OnMultiplayerListCertificateSummariesResultEvent = (PlayFabResultEvent<ListCertificateSummariesResponse>)Delegate.Remove(this.OnMultiplayerListCertificateSummariesResultEvent, (PlayFabResultEvent<ListCertificateSummariesResponse>)obj531);
					}
				}
			}
			if (this.OnMultiplayerListContainerImagesRequestEvent != null)
			{
				Delegate[] invocationList532 = this.OnMultiplayerListContainerImagesRequestEvent.GetInvocationList();
				foreach (Delegate obj532 in invocationList532)
				{
					if (object.ReferenceEquals(obj532.Target, instance))
					{
						this.OnMultiplayerListContainerImagesRequestEvent = (PlayFabRequestEvent<ListContainerImagesRequest>)Delegate.Remove(this.OnMultiplayerListContainerImagesRequestEvent, (PlayFabRequestEvent<ListContainerImagesRequest>)obj532);
					}
				}
			}
			if (this.OnMultiplayerListContainerImagesResultEvent != null)
			{
				Delegate[] invocationList533 = this.OnMultiplayerListContainerImagesResultEvent.GetInvocationList();
				foreach (Delegate obj533 in invocationList533)
				{
					if (object.ReferenceEquals(obj533.Target, instance))
					{
						this.OnMultiplayerListContainerImagesResultEvent = (PlayFabResultEvent<ListContainerImagesResponse>)Delegate.Remove(this.OnMultiplayerListContainerImagesResultEvent, (PlayFabResultEvent<ListContainerImagesResponse>)obj533);
					}
				}
			}
			if (this.OnMultiplayerListContainerImageTagsRequestEvent != null)
			{
				Delegate[] invocationList534 = this.OnMultiplayerListContainerImageTagsRequestEvent.GetInvocationList();
				foreach (Delegate obj534 in invocationList534)
				{
					if (object.ReferenceEquals(obj534.Target, instance))
					{
						this.OnMultiplayerListContainerImageTagsRequestEvent = (PlayFabRequestEvent<ListContainerImageTagsRequest>)Delegate.Remove(this.OnMultiplayerListContainerImageTagsRequestEvent, (PlayFabRequestEvent<ListContainerImageTagsRequest>)obj534);
					}
				}
			}
			if (this.OnMultiplayerListContainerImageTagsResultEvent != null)
			{
				Delegate[] invocationList535 = this.OnMultiplayerListContainerImageTagsResultEvent.GetInvocationList();
				foreach (Delegate obj535 in invocationList535)
				{
					if (object.ReferenceEquals(obj535.Target, instance))
					{
						this.OnMultiplayerListContainerImageTagsResultEvent = (PlayFabResultEvent<ListContainerImageTagsResponse>)Delegate.Remove(this.OnMultiplayerListContainerImageTagsResultEvent, (PlayFabResultEvent<ListContainerImageTagsResponse>)obj535);
					}
				}
			}
			if (this.OnMultiplayerListMatchmakingQueuesRequestEvent != null)
			{
				Delegate[] invocationList536 = this.OnMultiplayerListMatchmakingQueuesRequestEvent.GetInvocationList();
				foreach (Delegate obj536 in invocationList536)
				{
					if (object.ReferenceEquals(obj536.Target, instance))
					{
						this.OnMultiplayerListMatchmakingQueuesRequestEvent = (PlayFabRequestEvent<ListMatchmakingQueuesRequest>)Delegate.Remove(this.OnMultiplayerListMatchmakingQueuesRequestEvent, (PlayFabRequestEvent<ListMatchmakingQueuesRequest>)obj536);
					}
				}
			}
			if (this.OnMultiplayerListMatchmakingQueuesResultEvent != null)
			{
				Delegate[] invocationList537 = this.OnMultiplayerListMatchmakingQueuesResultEvent.GetInvocationList();
				foreach (Delegate obj537 in invocationList537)
				{
					if (object.ReferenceEquals(obj537.Target, instance))
					{
						this.OnMultiplayerListMatchmakingQueuesResultEvent = (PlayFabResultEvent<ListMatchmakingQueuesResult>)Delegate.Remove(this.OnMultiplayerListMatchmakingQueuesResultEvent, (PlayFabResultEvent<ListMatchmakingQueuesResult>)obj537);
					}
				}
			}
			if (this.OnMultiplayerListMatchmakingTicketsForPlayerRequestEvent != null)
			{
				Delegate[] invocationList538 = this.OnMultiplayerListMatchmakingTicketsForPlayerRequestEvent.GetInvocationList();
				foreach (Delegate obj538 in invocationList538)
				{
					if (object.ReferenceEquals(obj538.Target, instance))
					{
						this.OnMultiplayerListMatchmakingTicketsForPlayerRequestEvent = (PlayFabRequestEvent<ListMatchmakingTicketsForPlayerRequest>)Delegate.Remove(this.OnMultiplayerListMatchmakingTicketsForPlayerRequestEvent, (PlayFabRequestEvent<ListMatchmakingTicketsForPlayerRequest>)obj538);
					}
				}
			}
			if (this.OnMultiplayerListMatchmakingTicketsForPlayerResultEvent != null)
			{
				Delegate[] invocationList539 = this.OnMultiplayerListMatchmakingTicketsForPlayerResultEvent.GetInvocationList();
				foreach (Delegate obj539 in invocationList539)
				{
					if (object.ReferenceEquals(obj539.Target, instance))
					{
						this.OnMultiplayerListMatchmakingTicketsForPlayerResultEvent = (PlayFabResultEvent<ListMatchmakingTicketsForPlayerResult>)Delegate.Remove(this.OnMultiplayerListMatchmakingTicketsForPlayerResultEvent, (PlayFabResultEvent<ListMatchmakingTicketsForPlayerResult>)obj539);
					}
				}
			}
			if (this.OnMultiplayerListMultiplayerServersRequestEvent != null)
			{
				Delegate[] invocationList540 = this.OnMultiplayerListMultiplayerServersRequestEvent.GetInvocationList();
				foreach (Delegate obj540 in invocationList540)
				{
					if (object.ReferenceEquals(obj540.Target, instance))
					{
						this.OnMultiplayerListMultiplayerServersRequestEvent = (PlayFabRequestEvent<ListMultiplayerServersRequest>)Delegate.Remove(this.OnMultiplayerListMultiplayerServersRequestEvent, (PlayFabRequestEvent<ListMultiplayerServersRequest>)obj540);
					}
				}
			}
			if (this.OnMultiplayerListMultiplayerServersResultEvent != null)
			{
				Delegate[] invocationList541 = this.OnMultiplayerListMultiplayerServersResultEvent.GetInvocationList();
				foreach (Delegate obj541 in invocationList541)
				{
					if (object.ReferenceEquals(obj541.Target, instance))
					{
						this.OnMultiplayerListMultiplayerServersResultEvent = (PlayFabResultEvent<ListMultiplayerServersResponse>)Delegate.Remove(this.OnMultiplayerListMultiplayerServersResultEvent, (PlayFabResultEvent<ListMultiplayerServersResponse>)obj541);
					}
				}
			}
			if (this.OnMultiplayerListPartyQosServersRequestEvent != null)
			{
				Delegate[] invocationList542 = this.OnMultiplayerListPartyQosServersRequestEvent.GetInvocationList();
				foreach (Delegate obj542 in invocationList542)
				{
					if (object.ReferenceEquals(obj542.Target, instance))
					{
						this.OnMultiplayerListPartyQosServersRequestEvent = (PlayFabRequestEvent<ListPartyQosServersRequest>)Delegate.Remove(this.OnMultiplayerListPartyQosServersRequestEvent, (PlayFabRequestEvent<ListPartyQosServersRequest>)obj542);
					}
				}
			}
			if (this.OnMultiplayerListPartyQosServersResultEvent != null)
			{
				Delegate[] invocationList543 = this.OnMultiplayerListPartyQosServersResultEvent.GetInvocationList();
				foreach (Delegate obj543 in invocationList543)
				{
					if (object.ReferenceEquals(obj543.Target, instance))
					{
						this.OnMultiplayerListPartyQosServersResultEvent = (PlayFabResultEvent<ListPartyQosServersResponse>)Delegate.Remove(this.OnMultiplayerListPartyQosServersResultEvent, (PlayFabResultEvent<ListPartyQosServersResponse>)obj543);
					}
				}
			}
			if (this.OnMultiplayerListQosServersForTitleRequestEvent != null)
			{
				Delegate[] invocationList544 = this.OnMultiplayerListQosServersForTitleRequestEvent.GetInvocationList();
				foreach (Delegate obj544 in invocationList544)
				{
					if (object.ReferenceEquals(obj544.Target, instance))
					{
						this.OnMultiplayerListQosServersForTitleRequestEvent = (PlayFabRequestEvent<ListQosServersForTitleRequest>)Delegate.Remove(this.OnMultiplayerListQosServersForTitleRequestEvent, (PlayFabRequestEvent<ListQosServersForTitleRequest>)obj544);
					}
				}
			}
			if (this.OnMultiplayerListQosServersForTitleResultEvent != null)
			{
				Delegate[] invocationList545 = this.OnMultiplayerListQosServersForTitleResultEvent.GetInvocationList();
				foreach (Delegate obj545 in invocationList545)
				{
					if (object.ReferenceEquals(obj545.Target, instance))
					{
						this.OnMultiplayerListQosServersForTitleResultEvent = (PlayFabResultEvent<ListQosServersForTitleResponse>)Delegate.Remove(this.OnMultiplayerListQosServersForTitleResultEvent, (PlayFabResultEvent<ListQosServersForTitleResponse>)obj545);
					}
				}
			}
			if (this.OnMultiplayerListServerBackfillTicketsForPlayerRequestEvent != null)
			{
				Delegate[] invocationList546 = this.OnMultiplayerListServerBackfillTicketsForPlayerRequestEvent.GetInvocationList();
				foreach (Delegate obj546 in invocationList546)
				{
					if (object.ReferenceEquals(obj546.Target, instance))
					{
						this.OnMultiplayerListServerBackfillTicketsForPlayerRequestEvent = (PlayFabRequestEvent<ListServerBackfillTicketsForPlayerRequest>)Delegate.Remove(this.OnMultiplayerListServerBackfillTicketsForPlayerRequestEvent, (PlayFabRequestEvent<ListServerBackfillTicketsForPlayerRequest>)obj546);
					}
				}
			}
			if (this.OnMultiplayerListServerBackfillTicketsForPlayerResultEvent != null)
			{
				Delegate[] invocationList547 = this.OnMultiplayerListServerBackfillTicketsForPlayerResultEvent.GetInvocationList();
				foreach (Delegate obj547 in invocationList547)
				{
					if (object.ReferenceEquals(obj547.Target, instance))
					{
						this.OnMultiplayerListServerBackfillTicketsForPlayerResultEvent = (PlayFabResultEvent<ListServerBackfillTicketsForPlayerResult>)Delegate.Remove(this.OnMultiplayerListServerBackfillTicketsForPlayerResultEvent, (PlayFabResultEvent<ListServerBackfillTicketsForPlayerResult>)obj547);
					}
				}
			}
			if (this.OnMultiplayerListVirtualMachineSummariesRequestEvent != null)
			{
				Delegate[] invocationList548 = this.OnMultiplayerListVirtualMachineSummariesRequestEvent.GetInvocationList();
				foreach (Delegate obj548 in invocationList548)
				{
					if (object.ReferenceEquals(obj548.Target, instance))
					{
						this.OnMultiplayerListVirtualMachineSummariesRequestEvent = (PlayFabRequestEvent<ListVirtualMachineSummariesRequest>)Delegate.Remove(this.OnMultiplayerListVirtualMachineSummariesRequestEvent, (PlayFabRequestEvent<ListVirtualMachineSummariesRequest>)obj548);
					}
				}
			}
			if (this.OnMultiplayerListVirtualMachineSummariesResultEvent != null)
			{
				Delegate[] invocationList549 = this.OnMultiplayerListVirtualMachineSummariesResultEvent.GetInvocationList();
				foreach (Delegate obj549 in invocationList549)
				{
					if (object.ReferenceEquals(obj549.Target, instance))
					{
						this.OnMultiplayerListVirtualMachineSummariesResultEvent = (PlayFabResultEvent<ListVirtualMachineSummariesResponse>)Delegate.Remove(this.OnMultiplayerListVirtualMachineSummariesResultEvent, (PlayFabResultEvent<ListVirtualMachineSummariesResponse>)obj549);
					}
				}
			}
			if (this.OnMultiplayerRemoveMatchmakingQueueRequestEvent != null)
			{
				Delegate[] invocationList550 = this.OnMultiplayerRemoveMatchmakingQueueRequestEvent.GetInvocationList();
				foreach (Delegate obj550 in invocationList550)
				{
					if (object.ReferenceEquals(obj550.Target, instance))
					{
						this.OnMultiplayerRemoveMatchmakingQueueRequestEvent = (PlayFabRequestEvent<RemoveMatchmakingQueueRequest>)Delegate.Remove(this.OnMultiplayerRemoveMatchmakingQueueRequestEvent, (PlayFabRequestEvent<RemoveMatchmakingQueueRequest>)obj550);
					}
				}
			}
			if (this.OnMultiplayerRemoveMatchmakingQueueResultEvent != null)
			{
				Delegate[] invocationList551 = this.OnMultiplayerRemoveMatchmakingQueueResultEvent.GetInvocationList();
				foreach (Delegate obj551 in invocationList551)
				{
					if (object.ReferenceEquals(obj551.Target, instance))
					{
						this.OnMultiplayerRemoveMatchmakingQueueResultEvent = (PlayFabResultEvent<RemoveMatchmakingQueueResult>)Delegate.Remove(this.OnMultiplayerRemoveMatchmakingQueueResultEvent, (PlayFabResultEvent<RemoveMatchmakingQueueResult>)obj551);
					}
				}
			}
			if (this.OnMultiplayerRequestMultiplayerServerRequestEvent != null)
			{
				Delegate[] invocationList552 = this.OnMultiplayerRequestMultiplayerServerRequestEvent.GetInvocationList();
				foreach (Delegate obj552 in invocationList552)
				{
					if (object.ReferenceEquals(obj552.Target, instance))
					{
						this.OnMultiplayerRequestMultiplayerServerRequestEvent = (PlayFabRequestEvent<RequestMultiplayerServerRequest>)Delegate.Remove(this.OnMultiplayerRequestMultiplayerServerRequestEvent, (PlayFabRequestEvent<RequestMultiplayerServerRequest>)obj552);
					}
				}
			}
			if (this.OnMultiplayerRequestMultiplayerServerResultEvent != null)
			{
				Delegate[] invocationList553 = this.OnMultiplayerRequestMultiplayerServerResultEvent.GetInvocationList();
				foreach (Delegate obj553 in invocationList553)
				{
					if (object.ReferenceEquals(obj553.Target, instance))
					{
						this.OnMultiplayerRequestMultiplayerServerResultEvent = (PlayFabResultEvent<RequestMultiplayerServerResponse>)Delegate.Remove(this.OnMultiplayerRequestMultiplayerServerResultEvent, (PlayFabResultEvent<RequestMultiplayerServerResponse>)obj553);
					}
				}
			}
			if (this.OnMultiplayerRolloverContainerRegistryCredentialsRequestEvent != null)
			{
				Delegate[] invocationList554 = this.OnMultiplayerRolloverContainerRegistryCredentialsRequestEvent.GetInvocationList();
				foreach (Delegate obj554 in invocationList554)
				{
					if (object.ReferenceEquals(obj554.Target, instance))
					{
						this.OnMultiplayerRolloverContainerRegistryCredentialsRequestEvent = (PlayFabRequestEvent<RolloverContainerRegistryCredentialsRequest>)Delegate.Remove(this.OnMultiplayerRolloverContainerRegistryCredentialsRequestEvent, (PlayFabRequestEvent<RolloverContainerRegistryCredentialsRequest>)obj554);
					}
				}
			}
			if (this.OnMultiplayerRolloverContainerRegistryCredentialsResultEvent != null)
			{
				Delegate[] invocationList555 = this.OnMultiplayerRolloverContainerRegistryCredentialsResultEvent.GetInvocationList();
				foreach (Delegate obj555 in invocationList555)
				{
					if (object.ReferenceEquals(obj555.Target, instance))
					{
						this.OnMultiplayerRolloverContainerRegistryCredentialsResultEvent = (PlayFabResultEvent<RolloverContainerRegistryCredentialsResponse>)Delegate.Remove(this.OnMultiplayerRolloverContainerRegistryCredentialsResultEvent, (PlayFabResultEvent<RolloverContainerRegistryCredentialsResponse>)obj555);
					}
				}
			}
			if (this.OnMultiplayerSetMatchmakingQueueRequestEvent != null)
			{
				Delegate[] invocationList556 = this.OnMultiplayerSetMatchmakingQueueRequestEvent.GetInvocationList();
				foreach (Delegate obj556 in invocationList556)
				{
					if (object.ReferenceEquals(obj556.Target, instance))
					{
						this.OnMultiplayerSetMatchmakingQueueRequestEvent = (PlayFabRequestEvent<SetMatchmakingQueueRequest>)Delegate.Remove(this.OnMultiplayerSetMatchmakingQueueRequestEvent, (PlayFabRequestEvent<SetMatchmakingQueueRequest>)obj556);
					}
				}
			}
			if (this.OnMultiplayerSetMatchmakingQueueResultEvent != null)
			{
				Delegate[] invocationList557 = this.OnMultiplayerSetMatchmakingQueueResultEvent.GetInvocationList();
				foreach (Delegate obj557 in invocationList557)
				{
					if (object.ReferenceEquals(obj557.Target, instance))
					{
						this.OnMultiplayerSetMatchmakingQueueResultEvent = (PlayFabResultEvent<SetMatchmakingQueueResult>)Delegate.Remove(this.OnMultiplayerSetMatchmakingQueueResultEvent, (PlayFabResultEvent<SetMatchmakingQueueResult>)obj557);
					}
				}
			}
			if (this.OnMultiplayerShutdownMultiplayerServerRequestEvent != null)
			{
				Delegate[] invocationList558 = this.OnMultiplayerShutdownMultiplayerServerRequestEvent.GetInvocationList();
				foreach (Delegate obj558 in invocationList558)
				{
					if (object.ReferenceEquals(obj558.Target, instance))
					{
						this.OnMultiplayerShutdownMultiplayerServerRequestEvent = (PlayFabRequestEvent<ShutdownMultiplayerServerRequest>)Delegate.Remove(this.OnMultiplayerShutdownMultiplayerServerRequestEvent, (PlayFabRequestEvent<ShutdownMultiplayerServerRequest>)obj558);
					}
				}
			}
			if (this.OnMultiplayerShutdownMultiplayerServerResultEvent != null)
			{
				Delegate[] invocationList559 = this.OnMultiplayerShutdownMultiplayerServerResultEvent.GetInvocationList();
				foreach (Delegate obj559 in invocationList559)
				{
					if (object.ReferenceEquals(obj559.Target, instance))
					{
						this.OnMultiplayerShutdownMultiplayerServerResultEvent = (PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)Delegate.Remove(this.OnMultiplayerShutdownMultiplayerServerResultEvent, (PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)obj559);
					}
				}
			}
			if (this.OnMultiplayerUntagContainerImageRequestEvent != null)
			{
				Delegate[] invocationList560 = this.OnMultiplayerUntagContainerImageRequestEvent.GetInvocationList();
				foreach (Delegate obj560 in invocationList560)
				{
					if (object.ReferenceEquals(obj560.Target, instance))
					{
						this.OnMultiplayerUntagContainerImageRequestEvent = (PlayFabRequestEvent<UntagContainerImageRequest>)Delegate.Remove(this.OnMultiplayerUntagContainerImageRequestEvent, (PlayFabRequestEvent<UntagContainerImageRequest>)obj560);
					}
				}
			}
			if (this.OnMultiplayerUntagContainerImageResultEvent != null)
			{
				Delegate[] invocationList561 = this.OnMultiplayerUntagContainerImageResultEvent.GetInvocationList();
				foreach (Delegate obj561 in invocationList561)
				{
					if (object.ReferenceEquals(obj561.Target, instance))
					{
						this.OnMultiplayerUntagContainerImageResultEvent = (PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)Delegate.Remove(this.OnMultiplayerUntagContainerImageResultEvent, (PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)obj561);
					}
				}
			}
			if (this.OnMultiplayerUpdateBuildAliasRequestEvent != null)
			{
				Delegate[] invocationList562 = this.OnMultiplayerUpdateBuildAliasRequestEvent.GetInvocationList();
				foreach (Delegate obj562 in invocationList562)
				{
					if (object.ReferenceEquals(obj562.Target, instance))
					{
						this.OnMultiplayerUpdateBuildAliasRequestEvent = (PlayFabRequestEvent<UpdateBuildAliasRequest>)Delegate.Remove(this.OnMultiplayerUpdateBuildAliasRequestEvent, (PlayFabRequestEvent<UpdateBuildAliasRequest>)obj562);
					}
				}
			}
			if (this.OnMultiplayerUpdateBuildAliasResultEvent != null)
			{
				Delegate[] invocationList563 = this.OnMultiplayerUpdateBuildAliasResultEvent.GetInvocationList();
				foreach (Delegate obj563 in invocationList563)
				{
					if (object.ReferenceEquals(obj563.Target, instance))
					{
						this.OnMultiplayerUpdateBuildAliasResultEvent = (PlayFabResultEvent<BuildAliasDetailsResponse>)Delegate.Remove(this.OnMultiplayerUpdateBuildAliasResultEvent, (PlayFabResultEvent<BuildAliasDetailsResponse>)obj563);
					}
				}
			}
			if (this.OnMultiplayerUpdateBuildRegionRequestEvent != null)
			{
				Delegate[] invocationList564 = this.OnMultiplayerUpdateBuildRegionRequestEvent.GetInvocationList();
				foreach (Delegate obj564 in invocationList564)
				{
					if (object.ReferenceEquals(obj564.Target, instance))
					{
						this.OnMultiplayerUpdateBuildRegionRequestEvent = (PlayFabRequestEvent<UpdateBuildRegionRequest>)Delegate.Remove(this.OnMultiplayerUpdateBuildRegionRequestEvent, (PlayFabRequestEvent<UpdateBuildRegionRequest>)obj564);
					}
				}
			}
			if (this.OnMultiplayerUpdateBuildRegionResultEvent != null)
			{
				Delegate[] invocationList565 = this.OnMultiplayerUpdateBuildRegionResultEvent.GetInvocationList();
				foreach (Delegate obj565 in invocationList565)
				{
					if (object.ReferenceEquals(obj565.Target, instance))
					{
						this.OnMultiplayerUpdateBuildRegionResultEvent = (PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)Delegate.Remove(this.OnMultiplayerUpdateBuildRegionResultEvent, (PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)obj565);
					}
				}
			}
			if (this.OnMultiplayerUpdateBuildRegionsRequestEvent != null)
			{
				Delegate[] invocationList566 = this.OnMultiplayerUpdateBuildRegionsRequestEvent.GetInvocationList();
				foreach (Delegate obj566 in invocationList566)
				{
					if (object.ReferenceEquals(obj566.Target, instance))
					{
						this.OnMultiplayerUpdateBuildRegionsRequestEvent = (PlayFabRequestEvent<UpdateBuildRegionsRequest>)Delegate.Remove(this.OnMultiplayerUpdateBuildRegionsRequestEvent, (PlayFabRequestEvent<UpdateBuildRegionsRequest>)obj566);
					}
				}
			}
			if (this.OnMultiplayerUpdateBuildRegionsResultEvent != null)
			{
				Delegate[] invocationList567 = this.OnMultiplayerUpdateBuildRegionsResultEvent.GetInvocationList();
				foreach (Delegate obj567 in invocationList567)
				{
					if (object.ReferenceEquals(obj567.Target, instance))
					{
						this.OnMultiplayerUpdateBuildRegionsResultEvent = (PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)Delegate.Remove(this.OnMultiplayerUpdateBuildRegionsResultEvent, (PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)obj567);
					}
				}
			}
			if (this.OnMultiplayerUploadCertificateRequestEvent != null)
			{
				Delegate[] invocationList568 = this.OnMultiplayerUploadCertificateRequestEvent.GetInvocationList();
				foreach (Delegate obj568 in invocationList568)
				{
					if (object.ReferenceEquals(obj568.Target, instance))
					{
						this.OnMultiplayerUploadCertificateRequestEvent = (PlayFabRequestEvent<UploadCertificateRequest>)Delegate.Remove(this.OnMultiplayerUploadCertificateRequestEvent, (PlayFabRequestEvent<UploadCertificateRequest>)obj568);
					}
				}
			}
			if (this.OnMultiplayerUploadCertificateResultEvent != null)
			{
				Delegate[] invocationList569 = this.OnMultiplayerUploadCertificateResultEvent.GetInvocationList();
				foreach (Delegate obj569 in invocationList569)
				{
					if (object.ReferenceEquals(obj569.Target, instance))
					{
						this.OnMultiplayerUploadCertificateResultEvent = (PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)Delegate.Remove(this.OnMultiplayerUploadCertificateResultEvent, (PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)obj569);
					}
				}
			}
			if (this.OnProfilesGetGlobalPolicyRequestEvent != null)
			{
				Delegate[] invocationList570 = this.OnProfilesGetGlobalPolicyRequestEvent.GetInvocationList();
				foreach (Delegate obj570 in invocationList570)
				{
					if (object.ReferenceEquals(obj570.Target, instance))
					{
						this.OnProfilesGetGlobalPolicyRequestEvent = (PlayFabRequestEvent<GetGlobalPolicyRequest>)Delegate.Remove(this.OnProfilesGetGlobalPolicyRequestEvent, (PlayFabRequestEvent<GetGlobalPolicyRequest>)obj570);
					}
				}
			}
			if (this.OnProfilesGetGlobalPolicyResultEvent != null)
			{
				Delegate[] invocationList571 = this.OnProfilesGetGlobalPolicyResultEvent.GetInvocationList();
				foreach (Delegate obj571 in invocationList571)
				{
					if (object.ReferenceEquals(obj571.Target, instance))
					{
						this.OnProfilesGetGlobalPolicyResultEvent = (PlayFabResultEvent<GetGlobalPolicyResponse>)Delegate.Remove(this.OnProfilesGetGlobalPolicyResultEvent, (PlayFabResultEvent<GetGlobalPolicyResponse>)obj571);
					}
				}
			}
			if (this.OnProfilesGetProfileRequestEvent != null)
			{
				Delegate[] invocationList572 = this.OnProfilesGetProfileRequestEvent.GetInvocationList();
				foreach (Delegate obj572 in invocationList572)
				{
					if (object.ReferenceEquals(obj572.Target, instance))
					{
						this.OnProfilesGetProfileRequestEvent = (PlayFabRequestEvent<GetEntityProfileRequest>)Delegate.Remove(this.OnProfilesGetProfileRequestEvent, (PlayFabRequestEvent<GetEntityProfileRequest>)obj572);
					}
				}
			}
			if (this.OnProfilesGetProfileResultEvent != null)
			{
				Delegate[] invocationList573 = this.OnProfilesGetProfileResultEvent.GetInvocationList();
				foreach (Delegate obj573 in invocationList573)
				{
					if (object.ReferenceEquals(obj573.Target, instance))
					{
						this.OnProfilesGetProfileResultEvent = (PlayFabResultEvent<GetEntityProfileResponse>)Delegate.Remove(this.OnProfilesGetProfileResultEvent, (PlayFabResultEvent<GetEntityProfileResponse>)obj573);
					}
				}
			}
			if (this.OnProfilesGetProfilesRequestEvent != null)
			{
				Delegate[] invocationList574 = this.OnProfilesGetProfilesRequestEvent.GetInvocationList();
				foreach (Delegate obj574 in invocationList574)
				{
					if (object.ReferenceEquals(obj574.Target, instance))
					{
						this.OnProfilesGetProfilesRequestEvent = (PlayFabRequestEvent<GetEntityProfilesRequest>)Delegate.Remove(this.OnProfilesGetProfilesRequestEvent, (PlayFabRequestEvent<GetEntityProfilesRequest>)obj574);
					}
				}
			}
			if (this.OnProfilesGetProfilesResultEvent != null)
			{
				Delegate[] invocationList575 = this.OnProfilesGetProfilesResultEvent.GetInvocationList();
				foreach (Delegate obj575 in invocationList575)
				{
					if (object.ReferenceEquals(obj575.Target, instance))
					{
						this.OnProfilesGetProfilesResultEvent = (PlayFabResultEvent<GetEntityProfilesResponse>)Delegate.Remove(this.OnProfilesGetProfilesResultEvent, (PlayFabResultEvent<GetEntityProfilesResponse>)obj575);
					}
				}
			}
			if (this.OnProfilesGetTitlePlayersFromMasterPlayerAccountIdsRequestEvent != null)
			{
				Delegate[] invocationList576 = this.OnProfilesGetTitlePlayersFromMasterPlayerAccountIdsRequestEvent.GetInvocationList();
				foreach (Delegate obj576 in invocationList576)
				{
					if (object.ReferenceEquals(obj576.Target, instance))
					{
						this.OnProfilesGetTitlePlayersFromMasterPlayerAccountIdsRequestEvent = (PlayFabRequestEvent<GetTitlePlayersFromMasterPlayerAccountIdsRequest>)Delegate.Remove(this.OnProfilesGetTitlePlayersFromMasterPlayerAccountIdsRequestEvent, (PlayFabRequestEvent<GetTitlePlayersFromMasterPlayerAccountIdsRequest>)obj576);
					}
				}
			}
			if (this.OnProfilesGetTitlePlayersFromMasterPlayerAccountIdsResultEvent != null)
			{
				Delegate[] invocationList577 = this.OnProfilesGetTitlePlayersFromMasterPlayerAccountIdsResultEvent.GetInvocationList();
				foreach (Delegate obj577 in invocationList577)
				{
					if (object.ReferenceEquals(obj577.Target, instance))
					{
						this.OnProfilesGetTitlePlayersFromMasterPlayerAccountIdsResultEvent = (PlayFabResultEvent<GetTitlePlayersFromMasterPlayerAccountIdsResponse>)Delegate.Remove(this.OnProfilesGetTitlePlayersFromMasterPlayerAccountIdsResultEvent, (PlayFabResultEvent<GetTitlePlayersFromMasterPlayerAccountIdsResponse>)obj577);
					}
				}
			}
			if (this.OnProfilesSetGlobalPolicyRequestEvent != null)
			{
				Delegate[] invocationList578 = this.OnProfilesSetGlobalPolicyRequestEvent.GetInvocationList();
				foreach (Delegate obj578 in invocationList578)
				{
					if (object.ReferenceEquals(obj578.Target, instance))
					{
						this.OnProfilesSetGlobalPolicyRequestEvent = (PlayFabRequestEvent<SetGlobalPolicyRequest>)Delegate.Remove(this.OnProfilesSetGlobalPolicyRequestEvent, (PlayFabRequestEvent<SetGlobalPolicyRequest>)obj578);
					}
				}
			}
			if (this.OnProfilesSetGlobalPolicyResultEvent != null)
			{
				Delegate[] invocationList579 = this.OnProfilesSetGlobalPolicyResultEvent.GetInvocationList();
				foreach (Delegate obj579 in invocationList579)
				{
					if (object.ReferenceEquals(obj579.Target, instance))
					{
						this.OnProfilesSetGlobalPolicyResultEvent = (PlayFabResultEvent<SetGlobalPolicyResponse>)Delegate.Remove(this.OnProfilesSetGlobalPolicyResultEvent, (PlayFabResultEvent<SetGlobalPolicyResponse>)obj579);
					}
				}
			}
			if (this.OnProfilesSetProfileLanguageRequestEvent != null)
			{
				Delegate[] invocationList580 = this.OnProfilesSetProfileLanguageRequestEvent.GetInvocationList();
				foreach (Delegate obj580 in invocationList580)
				{
					if (object.ReferenceEquals(obj580.Target, instance))
					{
						this.OnProfilesSetProfileLanguageRequestEvent = (PlayFabRequestEvent<SetProfileLanguageRequest>)Delegate.Remove(this.OnProfilesSetProfileLanguageRequestEvent, (PlayFabRequestEvent<SetProfileLanguageRequest>)obj580);
					}
				}
			}
			if (this.OnProfilesSetProfileLanguageResultEvent != null)
			{
				Delegate[] invocationList581 = this.OnProfilesSetProfileLanguageResultEvent.GetInvocationList();
				foreach (Delegate obj581 in invocationList581)
				{
					if (object.ReferenceEquals(obj581.Target, instance))
					{
						this.OnProfilesSetProfileLanguageResultEvent = (PlayFabResultEvent<SetProfileLanguageResponse>)Delegate.Remove(this.OnProfilesSetProfileLanguageResultEvent, (PlayFabResultEvent<SetProfileLanguageResponse>)obj581);
					}
				}
			}
			if (this.OnProfilesSetProfilePolicyRequestEvent != null)
			{
				Delegate[] invocationList582 = this.OnProfilesSetProfilePolicyRequestEvent.GetInvocationList();
				foreach (Delegate obj582 in invocationList582)
				{
					if (object.ReferenceEquals(obj582.Target, instance))
					{
						this.OnProfilesSetProfilePolicyRequestEvent = (PlayFabRequestEvent<SetEntityProfilePolicyRequest>)Delegate.Remove(this.OnProfilesSetProfilePolicyRequestEvent, (PlayFabRequestEvent<SetEntityProfilePolicyRequest>)obj582);
					}
				}
			}
			if (this.OnProfilesSetProfilePolicyResultEvent == null)
			{
				return;
			}
			Delegate[] invocationList583 = this.OnProfilesSetProfilePolicyResultEvent.GetInvocationList();
			foreach (Delegate obj583 in invocationList583)
			{
				if (object.ReferenceEquals(obj583.Target, instance))
				{
					this.OnProfilesSetProfilePolicyResultEvent = (PlayFabResultEvent<SetEntityProfilePolicyResponse>)Delegate.Remove(this.OnProfilesSetProfilePolicyResultEvent, (PlayFabResultEvent<SetEntityProfilePolicyResponse>)obj583);
				}
			}
		}

		private void OnProcessingErrorEvent(PlayFabRequestCommon request, PlayFabError error)
		{
			if (_instance.OnGlobalErrorEvent != null)
			{
				_instance.OnGlobalErrorEvent(request, error);
			}
		}

		private void OnProcessingEvent(ApiProcessingEventArgs e)
		{
			if (e.EventType == ApiProcessingEventType.Pre)
			{
				Type type = e.Request.GetType();
				if (type == typeof(AcceptTradeRequest) && _instance.OnAcceptTradeRequestEvent != null)
				{
					_instance.OnAcceptTradeRequestEvent((AcceptTradeRequest)e.Request);
				}
				else if (type == typeof(AddFriendRequest) && _instance.OnAddFriendRequestEvent != null)
				{
					_instance.OnAddFriendRequestEvent((AddFriendRequest)e.Request);
				}
				else if (type == typeof(AddGenericIDRequest) && _instance.OnAddGenericIDRequestEvent != null)
				{
					_instance.OnAddGenericIDRequestEvent((AddGenericIDRequest)e.Request);
				}
				else if (type == typeof(AddOrUpdateContactEmailRequest) && _instance.OnAddOrUpdateContactEmailRequestEvent != null)
				{
					_instance.OnAddOrUpdateContactEmailRequestEvent((AddOrUpdateContactEmailRequest)e.Request);
				}
				else if (type == typeof(AddSharedGroupMembersRequest) && _instance.OnAddSharedGroupMembersRequestEvent != null)
				{
					_instance.OnAddSharedGroupMembersRequestEvent((AddSharedGroupMembersRequest)e.Request);
				}
				else if (type == typeof(AddUsernamePasswordRequest) && _instance.OnAddUsernamePasswordRequestEvent != null)
				{
					_instance.OnAddUsernamePasswordRequestEvent((AddUsernamePasswordRequest)e.Request);
				}
				else if (type == typeof(AddUserVirtualCurrencyRequest) && _instance.OnAddUserVirtualCurrencyRequestEvent != null)
				{
					_instance.OnAddUserVirtualCurrencyRequestEvent((AddUserVirtualCurrencyRequest)e.Request);
				}
				else if (type == typeof(AndroidDevicePushNotificationRegistrationRequest) && _instance.OnAndroidDevicePushNotificationRegistrationRequestEvent != null)
				{
					_instance.OnAndroidDevicePushNotificationRegistrationRequestEvent((AndroidDevicePushNotificationRegistrationRequest)e.Request);
				}
				else if (type == typeof(AttributeInstallRequest) && _instance.OnAttributeInstallRequestEvent != null)
				{
					_instance.OnAttributeInstallRequestEvent((AttributeInstallRequest)e.Request);
				}
				else if (type == typeof(CancelTradeRequest) && _instance.OnCancelTradeRequestEvent != null)
				{
					_instance.OnCancelTradeRequestEvent((CancelTradeRequest)e.Request);
				}
				else if (type == typeof(ConfirmPurchaseRequest) && _instance.OnConfirmPurchaseRequestEvent != null)
				{
					_instance.OnConfirmPurchaseRequestEvent((ConfirmPurchaseRequest)e.Request);
				}
				else if (type == typeof(ConsumeItemRequest) && _instance.OnConsumeItemRequestEvent != null)
				{
					_instance.OnConsumeItemRequestEvent((ConsumeItemRequest)e.Request);
				}
				else if (type == typeof(ConsumeMicrosoftStoreEntitlementsRequest) && _instance.OnConsumeMicrosoftStoreEntitlementsRequestEvent != null)
				{
					_instance.OnConsumeMicrosoftStoreEntitlementsRequestEvent((ConsumeMicrosoftStoreEntitlementsRequest)e.Request);
				}
				else if (type == typeof(ConsumePSNEntitlementsRequest) && _instance.OnConsumePSNEntitlementsRequestEvent != null)
				{
					_instance.OnConsumePSNEntitlementsRequestEvent((ConsumePSNEntitlementsRequest)e.Request);
				}
				else if (type == typeof(ConsumeXboxEntitlementsRequest) && _instance.OnConsumeXboxEntitlementsRequestEvent != null)
				{
					_instance.OnConsumeXboxEntitlementsRequestEvent((ConsumeXboxEntitlementsRequest)e.Request);
				}
				else if (type == typeof(CreateSharedGroupRequest) && _instance.OnCreateSharedGroupRequestEvent != null)
				{
					_instance.OnCreateSharedGroupRequestEvent((CreateSharedGroupRequest)e.Request);
				}
				else if (type == typeof(ExecuteCloudScriptRequest) && _instance.OnExecuteCloudScriptRequestEvent != null)
				{
					_instance.OnExecuteCloudScriptRequestEvent((ExecuteCloudScriptRequest)e.Request);
				}
				else if (type == typeof(GetAccountInfoRequest) && _instance.OnGetAccountInfoRequestEvent != null)
				{
					_instance.OnGetAccountInfoRequestEvent((GetAccountInfoRequest)e.Request);
				}
				else if (type == typeof(GetAdPlacementsRequest) && _instance.OnGetAdPlacementsRequestEvent != null)
				{
					_instance.OnGetAdPlacementsRequestEvent((GetAdPlacementsRequest)e.Request);
				}
				else if (type == typeof(ListUsersCharactersRequest) && _instance.OnGetAllUsersCharactersRequestEvent != null)
				{
					_instance.OnGetAllUsersCharactersRequestEvent((ListUsersCharactersRequest)e.Request);
				}
				else if (type == typeof(GetCatalogItemsRequest) && _instance.OnGetCatalogItemsRequestEvent != null)
				{
					_instance.OnGetCatalogItemsRequestEvent((GetCatalogItemsRequest)e.Request);
				}
				else if (type == typeof(GetCharacterDataRequest) && _instance.OnGetCharacterDataRequestEvent != null)
				{
					_instance.OnGetCharacterDataRequestEvent((GetCharacterDataRequest)e.Request);
				}
				else if (type == typeof(GetCharacterInventoryRequest) && _instance.OnGetCharacterInventoryRequestEvent != null)
				{
					_instance.OnGetCharacterInventoryRequestEvent((GetCharacterInventoryRequest)e.Request);
				}
				else if (type == typeof(GetCharacterLeaderboardRequest) && _instance.OnGetCharacterLeaderboardRequestEvent != null)
				{
					_instance.OnGetCharacterLeaderboardRequestEvent((GetCharacterLeaderboardRequest)e.Request);
				}
				else if (type == typeof(GetCharacterDataRequest) && _instance.OnGetCharacterReadOnlyDataRequestEvent != null)
				{
					_instance.OnGetCharacterReadOnlyDataRequestEvent((GetCharacterDataRequest)e.Request);
				}
				else if (type == typeof(GetCharacterStatisticsRequest) && _instance.OnGetCharacterStatisticsRequestEvent != null)
				{
					_instance.OnGetCharacterStatisticsRequestEvent((GetCharacterStatisticsRequest)e.Request);
				}
				else if (type == typeof(GetContentDownloadUrlRequest) && _instance.OnGetContentDownloadUrlRequestEvent != null)
				{
					_instance.OnGetContentDownloadUrlRequestEvent((GetContentDownloadUrlRequest)e.Request);
				}
				else if (type == typeof(CurrentGamesRequest) && _instance.OnGetCurrentGamesRequestEvent != null)
				{
					_instance.OnGetCurrentGamesRequestEvent((CurrentGamesRequest)e.Request);
				}
				else if (type == typeof(GetFriendLeaderboardRequest) && _instance.OnGetFriendLeaderboardRequestEvent != null)
				{
					_instance.OnGetFriendLeaderboardRequestEvent((GetFriendLeaderboardRequest)e.Request);
				}
				else if (type == typeof(GetFriendLeaderboardAroundPlayerRequest) && _instance.OnGetFriendLeaderboardAroundPlayerRequestEvent != null)
				{
					_instance.OnGetFriendLeaderboardAroundPlayerRequestEvent((GetFriendLeaderboardAroundPlayerRequest)e.Request);
				}
				else if (type == typeof(GetFriendsListRequest) && _instance.OnGetFriendsListRequestEvent != null)
				{
					_instance.OnGetFriendsListRequestEvent((GetFriendsListRequest)e.Request);
				}
				else if (type == typeof(GameServerRegionsRequest) && _instance.OnGetGameServerRegionsRequestEvent != null)
				{
					_instance.OnGetGameServerRegionsRequestEvent((GameServerRegionsRequest)e.Request);
				}
				else if (type == typeof(GetLeaderboardRequest) && _instance.OnGetLeaderboardRequestEvent != null)
				{
					_instance.OnGetLeaderboardRequestEvent((GetLeaderboardRequest)e.Request);
				}
				else if (type == typeof(GetLeaderboardAroundCharacterRequest) && _instance.OnGetLeaderboardAroundCharacterRequestEvent != null)
				{
					_instance.OnGetLeaderboardAroundCharacterRequestEvent((GetLeaderboardAroundCharacterRequest)e.Request);
				}
				else if (type == typeof(GetLeaderboardAroundPlayerRequest) && _instance.OnGetLeaderboardAroundPlayerRequestEvent != null)
				{
					_instance.OnGetLeaderboardAroundPlayerRequestEvent((GetLeaderboardAroundPlayerRequest)e.Request);
				}
				else if (type == typeof(GetLeaderboardForUsersCharactersRequest) && _instance.OnGetLeaderboardForUserCharactersRequestEvent != null)
				{
					_instance.OnGetLeaderboardForUserCharactersRequestEvent((GetLeaderboardForUsersCharactersRequest)e.Request);
				}
				else if (type == typeof(GetPaymentTokenRequest) && _instance.OnGetPaymentTokenRequestEvent != null)
				{
					_instance.OnGetPaymentTokenRequestEvent((GetPaymentTokenRequest)e.Request);
				}
				else if (type == typeof(GetPhotonAuthenticationTokenRequest) && _instance.OnGetPhotonAuthenticationTokenRequestEvent != null)
				{
					_instance.OnGetPhotonAuthenticationTokenRequestEvent((GetPhotonAuthenticationTokenRequest)e.Request);
				}
				else if (type == typeof(GetPlayerCombinedInfoRequest) && _instance.OnGetPlayerCombinedInfoRequestEvent != null)
				{
					_instance.OnGetPlayerCombinedInfoRequestEvent((GetPlayerCombinedInfoRequest)e.Request);
				}
				else if (type == typeof(GetPlayerProfileRequest) && _instance.OnGetPlayerProfileRequestEvent != null)
				{
					_instance.OnGetPlayerProfileRequestEvent((GetPlayerProfileRequest)e.Request);
				}
				else if (type == typeof(GetPlayerSegmentsRequest) && _instance.OnGetPlayerSegmentsRequestEvent != null)
				{
					_instance.OnGetPlayerSegmentsRequestEvent((GetPlayerSegmentsRequest)e.Request);
				}
				else if (type == typeof(GetPlayerStatisticsRequest) && _instance.OnGetPlayerStatisticsRequestEvent != null)
				{
					_instance.OnGetPlayerStatisticsRequestEvent((GetPlayerStatisticsRequest)e.Request);
				}
				else if (type == typeof(GetPlayerStatisticVersionsRequest) && _instance.OnGetPlayerStatisticVersionsRequestEvent != null)
				{
					_instance.OnGetPlayerStatisticVersionsRequestEvent((GetPlayerStatisticVersionsRequest)e.Request);
				}
				else if (type == typeof(GetPlayerTagsRequest) && _instance.OnGetPlayerTagsRequestEvent != null)
				{
					_instance.OnGetPlayerTagsRequestEvent((GetPlayerTagsRequest)e.Request);
				}
				else if (type == typeof(GetPlayerTradesRequest) && _instance.OnGetPlayerTradesRequestEvent != null)
				{
					_instance.OnGetPlayerTradesRequestEvent((GetPlayerTradesRequest)e.Request);
				}
				else if (type == typeof(GetPlayFabIDsFromFacebookIDsRequest) && _instance.OnGetPlayFabIDsFromFacebookIDsRequestEvent != null)
				{
					_instance.OnGetPlayFabIDsFromFacebookIDsRequestEvent((GetPlayFabIDsFromFacebookIDsRequest)e.Request);
				}
				else if (type == typeof(GetPlayFabIDsFromFacebookInstantGamesIdsRequest) && _instance.OnGetPlayFabIDsFromFacebookInstantGamesIdsRequestEvent != null)
				{
					_instance.OnGetPlayFabIDsFromFacebookInstantGamesIdsRequestEvent((GetPlayFabIDsFromFacebookInstantGamesIdsRequest)e.Request);
				}
				else if (type == typeof(GetPlayFabIDsFromGameCenterIDsRequest) && _instance.OnGetPlayFabIDsFromGameCenterIDsRequestEvent != null)
				{
					_instance.OnGetPlayFabIDsFromGameCenterIDsRequestEvent((GetPlayFabIDsFromGameCenterIDsRequest)e.Request);
				}
				else if (type == typeof(GetPlayFabIDsFromGenericIDsRequest) && _instance.OnGetPlayFabIDsFromGenericIDsRequestEvent != null)
				{
					_instance.OnGetPlayFabIDsFromGenericIDsRequestEvent((GetPlayFabIDsFromGenericIDsRequest)e.Request);
				}
				else if (type == typeof(GetPlayFabIDsFromGoogleIDsRequest) && _instance.OnGetPlayFabIDsFromGoogleIDsRequestEvent != null)
				{
					_instance.OnGetPlayFabIDsFromGoogleIDsRequestEvent((GetPlayFabIDsFromGoogleIDsRequest)e.Request);
				}
				else if (type == typeof(GetPlayFabIDsFromKongregateIDsRequest) && _instance.OnGetPlayFabIDsFromKongregateIDsRequestEvent != null)
				{
					_instance.OnGetPlayFabIDsFromKongregateIDsRequestEvent((GetPlayFabIDsFromKongregateIDsRequest)e.Request);
				}
				else if (type == typeof(GetPlayFabIDsFromNintendoSwitchDeviceIdsRequest) && _instance.OnGetPlayFabIDsFromNintendoSwitchDeviceIdsRequestEvent != null)
				{
					_instance.OnGetPlayFabIDsFromNintendoSwitchDeviceIdsRequestEvent((GetPlayFabIDsFromNintendoSwitchDeviceIdsRequest)e.Request);
				}
				else if (type == typeof(GetPlayFabIDsFromPSNAccountIDsRequest) && _instance.OnGetPlayFabIDsFromPSNAccountIDsRequestEvent != null)
				{
					_instance.OnGetPlayFabIDsFromPSNAccountIDsRequestEvent((GetPlayFabIDsFromPSNAccountIDsRequest)e.Request);
				}
				else if (type == typeof(GetPlayFabIDsFromSteamIDsRequest) && _instance.OnGetPlayFabIDsFromSteamIDsRequestEvent != null)
				{
					_instance.OnGetPlayFabIDsFromSteamIDsRequestEvent((GetPlayFabIDsFromSteamIDsRequest)e.Request);
				}
				else if (type == typeof(GetPlayFabIDsFromTwitchIDsRequest) && _instance.OnGetPlayFabIDsFromTwitchIDsRequestEvent != null)
				{
					_instance.OnGetPlayFabIDsFromTwitchIDsRequestEvent((GetPlayFabIDsFromTwitchIDsRequest)e.Request);
				}
				else if (type == typeof(GetPlayFabIDsFromXboxLiveIDsRequest) && _instance.OnGetPlayFabIDsFromXboxLiveIDsRequestEvent != null)
				{
					_instance.OnGetPlayFabIDsFromXboxLiveIDsRequestEvent((GetPlayFabIDsFromXboxLiveIDsRequest)e.Request);
				}
				else if (type == typeof(GetPublisherDataRequest) && _instance.OnGetPublisherDataRequestEvent != null)
				{
					_instance.OnGetPublisherDataRequestEvent((GetPublisherDataRequest)e.Request);
				}
				else if (type == typeof(GetPurchaseRequest) && _instance.OnGetPurchaseRequestEvent != null)
				{
					_instance.OnGetPurchaseRequestEvent((GetPurchaseRequest)e.Request);
				}
				else if (type == typeof(GetSharedGroupDataRequest) && _instance.OnGetSharedGroupDataRequestEvent != null)
				{
					_instance.OnGetSharedGroupDataRequestEvent((GetSharedGroupDataRequest)e.Request);
				}
				else if (type == typeof(GetStoreItemsRequest) && _instance.OnGetStoreItemsRequestEvent != null)
				{
					_instance.OnGetStoreItemsRequestEvent((GetStoreItemsRequest)e.Request);
				}
				else if (type == typeof(GetTimeRequest) && _instance.OnGetTimeRequestEvent != null)
				{
					_instance.OnGetTimeRequestEvent((GetTimeRequest)e.Request);
				}
				else if (type == typeof(GetTitleDataRequest) && _instance.OnGetTitleDataRequestEvent != null)
				{
					_instance.OnGetTitleDataRequestEvent((GetTitleDataRequest)e.Request);
				}
				else if (type == typeof(GetTitleNewsRequest) && _instance.OnGetTitleNewsRequestEvent != null)
				{
					_instance.OnGetTitleNewsRequestEvent((GetTitleNewsRequest)e.Request);
				}
				else if (type == typeof(GetTitlePublicKeyRequest) && _instance.OnGetTitlePublicKeyRequestEvent != null)
				{
					_instance.OnGetTitlePublicKeyRequestEvent((GetTitlePublicKeyRequest)e.Request);
				}
				else if (type == typeof(GetTradeStatusRequest) && _instance.OnGetTradeStatusRequestEvent != null)
				{
					_instance.OnGetTradeStatusRequestEvent((GetTradeStatusRequest)e.Request);
				}
				else if (type == typeof(GetUserDataRequest) && _instance.OnGetUserDataRequestEvent != null)
				{
					_instance.OnGetUserDataRequestEvent((GetUserDataRequest)e.Request);
				}
				else if (type == typeof(GetUserInventoryRequest) && _instance.OnGetUserInventoryRequestEvent != null)
				{
					_instance.OnGetUserInventoryRequestEvent((GetUserInventoryRequest)e.Request);
				}
				else if (type == typeof(GetUserDataRequest) && _instance.OnGetUserPublisherDataRequestEvent != null)
				{
					_instance.OnGetUserPublisherDataRequestEvent((GetUserDataRequest)e.Request);
				}
				else if (type == typeof(GetUserDataRequest) && _instance.OnGetUserPublisherReadOnlyDataRequestEvent != null)
				{
					_instance.OnGetUserPublisherReadOnlyDataRequestEvent((GetUserDataRequest)e.Request);
				}
				else if (type == typeof(GetUserDataRequest) && _instance.OnGetUserReadOnlyDataRequestEvent != null)
				{
					_instance.OnGetUserReadOnlyDataRequestEvent((GetUserDataRequest)e.Request);
				}
				else if (type == typeof(GetWindowsHelloChallengeRequest) && _instance.OnGetWindowsHelloChallengeRequestEvent != null)
				{
					_instance.OnGetWindowsHelloChallengeRequestEvent((GetWindowsHelloChallengeRequest)e.Request);
				}
				else if (type == typeof(GrantCharacterToUserRequest) && _instance.OnGrantCharacterToUserRequestEvent != null)
				{
					_instance.OnGrantCharacterToUserRequestEvent((GrantCharacterToUserRequest)e.Request);
				}
				else if (type == typeof(LinkAndroidDeviceIDRequest) && _instance.OnLinkAndroidDeviceIDRequestEvent != null)
				{
					_instance.OnLinkAndroidDeviceIDRequestEvent((LinkAndroidDeviceIDRequest)e.Request);
				}
				else if (type == typeof(LinkAppleRequest) && _instance.OnLinkAppleRequestEvent != null)
				{
					_instance.OnLinkAppleRequestEvent((LinkAppleRequest)e.Request);
				}
				else if (type == typeof(LinkCustomIDRequest) && _instance.OnLinkCustomIDRequestEvent != null)
				{
					_instance.OnLinkCustomIDRequestEvent((LinkCustomIDRequest)e.Request);
				}
				else if (type == typeof(LinkFacebookAccountRequest) && _instance.OnLinkFacebookAccountRequestEvent != null)
				{
					_instance.OnLinkFacebookAccountRequestEvent((LinkFacebookAccountRequest)e.Request);
				}
				else if (type == typeof(LinkFacebookInstantGamesIdRequest) && _instance.OnLinkFacebookInstantGamesIdRequestEvent != null)
				{
					_instance.OnLinkFacebookInstantGamesIdRequestEvent((LinkFacebookInstantGamesIdRequest)e.Request);
				}
				else if (type == typeof(LinkGameCenterAccountRequest) && _instance.OnLinkGameCenterAccountRequestEvent != null)
				{
					_instance.OnLinkGameCenterAccountRequestEvent((LinkGameCenterAccountRequest)e.Request);
				}
				else if (type == typeof(LinkGoogleAccountRequest) && _instance.OnLinkGoogleAccountRequestEvent != null)
				{
					_instance.OnLinkGoogleAccountRequestEvent((LinkGoogleAccountRequest)e.Request);
				}
				else if (type == typeof(LinkIOSDeviceIDRequest) && _instance.OnLinkIOSDeviceIDRequestEvent != null)
				{
					_instance.OnLinkIOSDeviceIDRequestEvent((LinkIOSDeviceIDRequest)e.Request);
				}
				else if (type == typeof(LinkKongregateAccountRequest) && _instance.OnLinkKongregateRequestEvent != null)
				{
					_instance.OnLinkKongregateRequestEvent((LinkKongregateAccountRequest)e.Request);
				}
				else if (type == typeof(LinkNintendoServiceAccountRequest) && _instance.OnLinkNintendoServiceAccountRequestEvent != null)
				{
					_instance.OnLinkNintendoServiceAccountRequestEvent((LinkNintendoServiceAccountRequest)e.Request);
				}
				else if (type == typeof(LinkNintendoSwitchDeviceIdRequest) && _instance.OnLinkNintendoSwitchDeviceIdRequestEvent != null)
				{
					_instance.OnLinkNintendoSwitchDeviceIdRequestEvent((LinkNintendoSwitchDeviceIdRequest)e.Request);
				}
				else if (type == typeof(LinkOpenIdConnectRequest) && _instance.OnLinkOpenIdConnectRequestEvent != null)
				{
					_instance.OnLinkOpenIdConnectRequestEvent((LinkOpenIdConnectRequest)e.Request);
				}
				else if (type == typeof(LinkPSNAccountRequest) && _instance.OnLinkPSNAccountRequestEvent != null)
				{
					_instance.OnLinkPSNAccountRequestEvent((LinkPSNAccountRequest)e.Request);
				}
				else if (type == typeof(LinkSteamAccountRequest) && _instance.OnLinkSteamAccountRequestEvent != null)
				{
					_instance.OnLinkSteamAccountRequestEvent((LinkSteamAccountRequest)e.Request);
				}
				else if (type == typeof(LinkTwitchAccountRequest) && _instance.OnLinkTwitchRequestEvent != null)
				{
					_instance.OnLinkTwitchRequestEvent((LinkTwitchAccountRequest)e.Request);
				}
				else if (type == typeof(LinkWindowsHelloAccountRequest) && _instance.OnLinkWindowsHelloRequestEvent != null)
				{
					_instance.OnLinkWindowsHelloRequestEvent((LinkWindowsHelloAccountRequest)e.Request);
				}
				else if (type == typeof(LinkXboxAccountRequest) && _instance.OnLinkXboxAccountRequestEvent != null)
				{
					_instance.OnLinkXboxAccountRequestEvent((LinkXboxAccountRequest)e.Request);
				}
				else if (type == typeof(LoginWithAndroidDeviceIDRequest) && _instance.OnLoginWithAndroidDeviceIDRequestEvent != null)
				{
					_instance.OnLoginWithAndroidDeviceIDRequestEvent((LoginWithAndroidDeviceIDRequest)e.Request);
				}
				else if (type == typeof(LoginWithAppleRequest) && _instance.OnLoginWithAppleRequestEvent != null)
				{
					_instance.OnLoginWithAppleRequestEvent((LoginWithAppleRequest)e.Request);
				}
				else if (type == typeof(LoginWithCustomIDRequest) && _instance.OnLoginWithCustomIDRequestEvent != null)
				{
					_instance.OnLoginWithCustomIDRequestEvent((LoginWithCustomIDRequest)e.Request);
				}
				else if (type == typeof(LoginWithEmailAddressRequest) && _instance.OnLoginWithEmailAddressRequestEvent != null)
				{
					_instance.OnLoginWithEmailAddressRequestEvent((LoginWithEmailAddressRequest)e.Request);
				}
				else if (type == typeof(LoginWithFacebookRequest) && _instance.OnLoginWithFacebookRequestEvent != null)
				{
					_instance.OnLoginWithFacebookRequestEvent((LoginWithFacebookRequest)e.Request);
				}
				else if (type == typeof(LoginWithFacebookInstantGamesIdRequest) && _instance.OnLoginWithFacebookInstantGamesIdRequestEvent != null)
				{
					_instance.OnLoginWithFacebookInstantGamesIdRequestEvent((LoginWithFacebookInstantGamesIdRequest)e.Request);
				}
				else if (type == typeof(LoginWithGameCenterRequest) && _instance.OnLoginWithGameCenterRequestEvent != null)
				{
					_instance.OnLoginWithGameCenterRequestEvent((LoginWithGameCenterRequest)e.Request);
				}
				else if (type == typeof(LoginWithGoogleAccountRequest) && _instance.OnLoginWithGoogleAccountRequestEvent != null)
				{
					_instance.OnLoginWithGoogleAccountRequestEvent((LoginWithGoogleAccountRequest)e.Request);
				}
				else if (type == typeof(LoginWithIOSDeviceIDRequest) && _instance.OnLoginWithIOSDeviceIDRequestEvent != null)
				{
					_instance.OnLoginWithIOSDeviceIDRequestEvent((LoginWithIOSDeviceIDRequest)e.Request);
				}
				else if (type == typeof(LoginWithKongregateRequest) && _instance.OnLoginWithKongregateRequestEvent != null)
				{
					_instance.OnLoginWithKongregateRequestEvent((LoginWithKongregateRequest)e.Request);
				}
				else if (type == typeof(LoginWithNintendoServiceAccountRequest) && _instance.OnLoginWithNintendoServiceAccountRequestEvent != null)
				{
					_instance.OnLoginWithNintendoServiceAccountRequestEvent((LoginWithNintendoServiceAccountRequest)e.Request);
				}
				else if (type == typeof(LoginWithNintendoSwitchDeviceIdRequest) && _instance.OnLoginWithNintendoSwitchDeviceIdRequestEvent != null)
				{
					_instance.OnLoginWithNintendoSwitchDeviceIdRequestEvent((LoginWithNintendoSwitchDeviceIdRequest)e.Request);
				}
				else if (type == typeof(LoginWithOpenIdConnectRequest) && _instance.OnLoginWithOpenIdConnectRequestEvent != null)
				{
					_instance.OnLoginWithOpenIdConnectRequestEvent((LoginWithOpenIdConnectRequest)e.Request);
				}
				else if (type == typeof(LoginWithPlayFabRequest) && _instance.OnLoginWithPlayFabRequestEvent != null)
				{
					_instance.OnLoginWithPlayFabRequestEvent((LoginWithPlayFabRequest)e.Request);
				}
				else if (type == typeof(LoginWithPSNRequest) && _instance.OnLoginWithPSNRequestEvent != null)
				{
					_instance.OnLoginWithPSNRequestEvent((LoginWithPSNRequest)e.Request);
				}
				else if (type == typeof(LoginWithSteamRequest) && _instance.OnLoginWithSteamRequestEvent != null)
				{
					_instance.OnLoginWithSteamRequestEvent((LoginWithSteamRequest)e.Request);
				}
				else if (type == typeof(LoginWithTwitchRequest) && _instance.OnLoginWithTwitchRequestEvent != null)
				{
					_instance.OnLoginWithTwitchRequestEvent((LoginWithTwitchRequest)e.Request);
				}
				else if (type == typeof(LoginWithWindowsHelloRequest) && _instance.OnLoginWithWindowsHelloRequestEvent != null)
				{
					_instance.OnLoginWithWindowsHelloRequestEvent((LoginWithWindowsHelloRequest)e.Request);
				}
				else if (type == typeof(LoginWithXboxRequest) && _instance.OnLoginWithXboxRequestEvent != null)
				{
					_instance.OnLoginWithXboxRequestEvent((LoginWithXboxRequest)e.Request);
				}
				else if (type == typeof(MatchmakeRequest) && _instance.OnMatchmakeRequestEvent != null)
				{
					_instance.OnMatchmakeRequestEvent((MatchmakeRequest)e.Request);
				}
				else if (type == typeof(OpenTradeRequest) && _instance.OnOpenTradeRequestEvent != null)
				{
					_instance.OnOpenTradeRequestEvent((OpenTradeRequest)e.Request);
				}
				else if (type == typeof(PayForPurchaseRequest) && _instance.OnPayForPurchaseRequestEvent != null)
				{
					_instance.OnPayForPurchaseRequestEvent((PayForPurchaseRequest)e.Request);
				}
				else if (type == typeof(PurchaseItemRequest) && _instance.OnPurchaseItemRequestEvent != null)
				{
					_instance.OnPurchaseItemRequestEvent((PurchaseItemRequest)e.Request);
				}
				else if (type == typeof(RedeemCouponRequest) && _instance.OnRedeemCouponRequestEvent != null)
				{
					_instance.OnRedeemCouponRequestEvent((RedeemCouponRequest)e.Request);
				}
				else if (type == typeof(RefreshPSNAuthTokenRequest) && _instance.OnRefreshPSNAuthTokenRequestEvent != null)
				{
					_instance.OnRefreshPSNAuthTokenRequestEvent((RefreshPSNAuthTokenRequest)e.Request);
				}
				else if (type == typeof(RegisterForIOSPushNotificationRequest) && _instance.OnRegisterForIOSPushNotificationRequestEvent != null)
				{
					_instance.OnRegisterForIOSPushNotificationRequestEvent((RegisterForIOSPushNotificationRequest)e.Request);
				}
				else if (type == typeof(RegisterPlayFabUserRequest) && _instance.OnRegisterPlayFabUserRequestEvent != null)
				{
					_instance.OnRegisterPlayFabUserRequestEvent((RegisterPlayFabUserRequest)e.Request);
				}
				else if (type == typeof(RegisterWithWindowsHelloRequest) && _instance.OnRegisterWithWindowsHelloRequestEvent != null)
				{
					_instance.OnRegisterWithWindowsHelloRequestEvent((RegisterWithWindowsHelloRequest)e.Request);
				}
				else if (type == typeof(RemoveContactEmailRequest) && _instance.OnRemoveContactEmailRequestEvent != null)
				{
					_instance.OnRemoveContactEmailRequestEvent((RemoveContactEmailRequest)e.Request);
				}
				else if (type == typeof(RemoveFriendRequest) && _instance.OnRemoveFriendRequestEvent != null)
				{
					_instance.OnRemoveFriendRequestEvent((RemoveFriendRequest)e.Request);
				}
				else if (type == typeof(RemoveGenericIDRequest) && _instance.OnRemoveGenericIDRequestEvent != null)
				{
					_instance.OnRemoveGenericIDRequestEvent((RemoveGenericIDRequest)e.Request);
				}
				else if (type == typeof(RemoveSharedGroupMembersRequest) && _instance.OnRemoveSharedGroupMembersRequestEvent != null)
				{
					_instance.OnRemoveSharedGroupMembersRequestEvent((RemoveSharedGroupMembersRequest)e.Request);
				}
				else if (type == typeof(ReportAdActivityRequest) && _instance.OnReportAdActivityRequestEvent != null)
				{
					_instance.OnReportAdActivityRequestEvent((ReportAdActivityRequest)e.Request);
				}
				else if (type == typeof(DeviceInfoRequest) && _instance.OnReportDeviceInfoRequestEvent != null)
				{
					_instance.OnReportDeviceInfoRequestEvent((DeviceInfoRequest)e.Request);
				}
				else if (type == typeof(ReportPlayerClientRequest) && _instance.OnReportPlayerRequestEvent != null)
				{
					_instance.OnReportPlayerRequestEvent((ReportPlayerClientRequest)e.Request);
				}
				else if (type == typeof(RestoreIOSPurchasesRequest) && _instance.OnRestoreIOSPurchasesRequestEvent != null)
				{
					_instance.OnRestoreIOSPurchasesRequestEvent((RestoreIOSPurchasesRequest)e.Request);
				}
				else if (type == typeof(RewardAdActivityRequest) && _instance.OnRewardAdActivityRequestEvent != null)
				{
					_instance.OnRewardAdActivityRequestEvent((RewardAdActivityRequest)e.Request);
				}
				else if (type == typeof(SendAccountRecoveryEmailRequest) && _instance.OnSendAccountRecoveryEmailRequestEvent != null)
				{
					_instance.OnSendAccountRecoveryEmailRequestEvent((SendAccountRecoveryEmailRequest)e.Request);
				}
				else if (type == typeof(SetFriendTagsRequest) && _instance.OnSetFriendTagsRequestEvent != null)
				{
					_instance.OnSetFriendTagsRequestEvent((SetFriendTagsRequest)e.Request);
				}
				else if (type == typeof(SetPlayerSecretRequest) && _instance.OnSetPlayerSecretRequestEvent != null)
				{
					_instance.OnSetPlayerSecretRequestEvent((SetPlayerSecretRequest)e.Request);
				}
				else if (type == typeof(StartGameRequest) && _instance.OnStartGameRequestEvent != null)
				{
					_instance.OnStartGameRequestEvent((StartGameRequest)e.Request);
				}
				else if (type == typeof(StartPurchaseRequest) && _instance.OnStartPurchaseRequestEvent != null)
				{
					_instance.OnStartPurchaseRequestEvent((StartPurchaseRequest)e.Request);
				}
				else if (type == typeof(SubtractUserVirtualCurrencyRequest) && _instance.OnSubtractUserVirtualCurrencyRequestEvent != null)
				{
					_instance.OnSubtractUserVirtualCurrencyRequestEvent((SubtractUserVirtualCurrencyRequest)e.Request);
				}
				else if (type == typeof(UnlinkAndroidDeviceIDRequest) && _instance.OnUnlinkAndroidDeviceIDRequestEvent != null)
				{
					_instance.OnUnlinkAndroidDeviceIDRequestEvent((UnlinkAndroidDeviceIDRequest)e.Request);
				}
				else if (type == typeof(UnlinkAppleRequest) && _instance.OnUnlinkAppleRequestEvent != null)
				{
					_instance.OnUnlinkAppleRequestEvent((UnlinkAppleRequest)e.Request);
				}
				else if (type == typeof(UnlinkCustomIDRequest) && _instance.OnUnlinkCustomIDRequestEvent != null)
				{
					_instance.OnUnlinkCustomIDRequestEvent((UnlinkCustomIDRequest)e.Request);
				}
				else if (type == typeof(UnlinkFacebookAccountRequest) && _instance.OnUnlinkFacebookAccountRequestEvent != null)
				{
					_instance.OnUnlinkFacebookAccountRequestEvent((UnlinkFacebookAccountRequest)e.Request);
				}
				else if (type == typeof(UnlinkFacebookInstantGamesIdRequest) && _instance.OnUnlinkFacebookInstantGamesIdRequestEvent != null)
				{
					_instance.OnUnlinkFacebookInstantGamesIdRequestEvent((UnlinkFacebookInstantGamesIdRequest)e.Request);
				}
				else if (type == typeof(UnlinkGameCenterAccountRequest) && _instance.OnUnlinkGameCenterAccountRequestEvent != null)
				{
					_instance.OnUnlinkGameCenterAccountRequestEvent((UnlinkGameCenterAccountRequest)e.Request);
				}
				else if (type == typeof(UnlinkGoogleAccountRequest) && _instance.OnUnlinkGoogleAccountRequestEvent != null)
				{
					_instance.OnUnlinkGoogleAccountRequestEvent((UnlinkGoogleAccountRequest)e.Request);
				}
				else if (type == typeof(UnlinkIOSDeviceIDRequest) && _instance.OnUnlinkIOSDeviceIDRequestEvent != null)
				{
					_instance.OnUnlinkIOSDeviceIDRequestEvent((UnlinkIOSDeviceIDRequest)e.Request);
				}
				else if (type == typeof(UnlinkKongregateAccountRequest) && _instance.OnUnlinkKongregateRequestEvent != null)
				{
					_instance.OnUnlinkKongregateRequestEvent((UnlinkKongregateAccountRequest)e.Request);
				}
				else if (type == typeof(UnlinkNintendoServiceAccountRequest) && _instance.OnUnlinkNintendoServiceAccountRequestEvent != null)
				{
					_instance.OnUnlinkNintendoServiceAccountRequestEvent((UnlinkNintendoServiceAccountRequest)e.Request);
				}
				else if (type == typeof(UnlinkNintendoSwitchDeviceIdRequest) && _instance.OnUnlinkNintendoSwitchDeviceIdRequestEvent != null)
				{
					_instance.OnUnlinkNintendoSwitchDeviceIdRequestEvent((UnlinkNintendoSwitchDeviceIdRequest)e.Request);
				}
				else if (type == typeof(UnlinkOpenIdConnectRequest) && _instance.OnUnlinkOpenIdConnectRequestEvent != null)
				{
					_instance.OnUnlinkOpenIdConnectRequestEvent((UnlinkOpenIdConnectRequest)e.Request);
				}
				else if (type == typeof(UnlinkPSNAccountRequest) && _instance.OnUnlinkPSNAccountRequestEvent != null)
				{
					_instance.OnUnlinkPSNAccountRequestEvent((UnlinkPSNAccountRequest)e.Request);
				}
				else if (type == typeof(UnlinkSteamAccountRequest) && _instance.OnUnlinkSteamAccountRequestEvent != null)
				{
					_instance.OnUnlinkSteamAccountRequestEvent((UnlinkSteamAccountRequest)e.Request);
				}
				else if (type == typeof(UnlinkTwitchAccountRequest) && _instance.OnUnlinkTwitchRequestEvent != null)
				{
					_instance.OnUnlinkTwitchRequestEvent((UnlinkTwitchAccountRequest)e.Request);
				}
				else if (type == typeof(UnlinkWindowsHelloAccountRequest) && _instance.OnUnlinkWindowsHelloRequestEvent != null)
				{
					_instance.OnUnlinkWindowsHelloRequestEvent((UnlinkWindowsHelloAccountRequest)e.Request);
				}
				else if (type == typeof(UnlinkXboxAccountRequest) && _instance.OnUnlinkXboxAccountRequestEvent != null)
				{
					_instance.OnUnlinkXboxAccountRequestEvent((UnlinkXboxAccountRequest)e.Request);
				}
				else if (type == typeof(UnlockContainerInstanceRequest) && _instance.OnUnlockContainerInstanceRequestEvent != null)
				{
					_instance.OnUnlockContainerInstanceRequestEvent((UnlockContainerInstanceRequest)e.Request);
				}
				else if (type == typeof(UnlockContainerItemRequest) && _instance.OnUnlockContainerItemRequestEvent != null)
				{
					_instance.OnUnlockContainerItemRequestEvent((UnlockContainerItemRequest)e.Request);
				}
				else if (type == typeof(UpdateAvatarUrlRequest) && _instance.OnUpdateAvatarUrlRequestEvent != null)
				{
					_instance.OnUpdateAvatarUrlRequestEvent((UpdateAvatarUrlRequest)e.Request);
				}
				else if (type == typeof(UpdateCharacterDataRequest) && _instance.OnUpdateCharacterDataRequestEvent != null)
				{
					_instance.OnUpdateCharacterDataRequestEvent((UpdateCharacterDataRequest)e.Request);
				}
				else if (type == typeof(UpdateCharacterStatisticsRequest) && _instance.OnUpdateCharacterStatisticsRequestEvent != null)
				{
					_instance.OnUpdateCharacterStatisticsRequestEvent((UpdateCharacterStatisticsRequest)e.Request);
				}
				else if (type == typeof(UpdatePlayerStatisticsRequest) && _instance.OnUpdatePlayerStatisticsRequestEvent != null)
				{
					_instance.OnUpdatePlayerStatisticsRequestEvent((UpdatePlayerStatisticsRequest)e.Request);
				}
				else if (type == typeof(UpdateSharedGroupDataRequest) && _instance.OnUpdateSharedGroupDataRequestEvent != null)
				{
					_instance.OnUpdateSharedGroupDataRequestEvent((UpdateSharedGroupDataRequest)e.Request);
				}
				else if (type == typeof(UpdateUserDataRequest) && _instance.OnUpdateUserDataRequestEvent != null)
				{
					_instance.OnUpdateUserDataRequestEvent((UpdateUserDataRequest)e.Request);
				}
				else if (type == typeof(UpdateUserDataRequest) && _instance.OnUpdateUserPublisherDataRequestEvent != null)
				{
					_instance.OnUpdateUserPublisherDataRequestEvent((UpdateUserDataRequest)e.Request);
				}
				else if (type == typeof(UpdateUserTitleDisplayNameRequest) && _instance.OnUpdateUserTitleDisplayNameRequestEvent != null)
				{
					_instance.OnUpdateUserTitleDisplayNameRequestEvent((UpdateUserTitleDisplayNameRequest)e.Request);
				}
				else if (type == typeof(ValidateAmazonReceiptRequest) && _instance.OnValidateAmazonIAPReceiptRequestEvent != null)
				{
					_instance.OnValidateAmazonIAPReceiptRequestEvent((ValidateAmazonReceiptRequest)e.Request);
				}
				else if (type == typeof(ValidateGooglePlayPurchaseRequest) && _instance.OnValidateGooglePlayPurchaseRequestEvent != null)
				{
					_instance.OnValidateGooglePlayPurchaseRequestEvent((ValidateGooglePlayPurchaseRequest)e.Request);
				}
				else if (type == typeof(ValidateIOSReceiptRequest) && _instance.OnValidateIOSReceiptRequestEvent != null)
				{
					_instance.OnValidateIOSReceiptRequestEvent((ValidateIOSReceiptRequest)e.Request);
				}
				else if (type == typeof(ValidateWindowsReceiptRequest) && _instance.OnValidateWindowsStoreReceiptRequestEvent != null)
				{
					_instance.OnValidateWindowsStoreReceiptRequestEvent((ValidateWindowsReceiptRequest)e.Request);
				}
				else if (type == typeof(WriteClientCharacterEventRequest) && _instance.OnWriteCharacterEventRequestEvent != null)
				{
					_instance.OnWriteCharacterEventRequestEvent((WriteClientCharacterEventRequest)e.Request);
				}
				else if (type == typeof(WriteClientPlayerEventRequest) && _instance.OnWritePlayerEventRequestEvent != null)
				{
					_instance.OnWritePlayerEventRequestEvent((WriteClientPlayerEventRequest)e.Request);
				}
				else if (type == typeof(WriteTitleEventRequest) && _instance.OnWriteTitleEventRequestEvent != null)
				{
					_instance.OnWriteTitleEventRequestEvent((WriteTitleEventRequest)e.Request);
				}
				else if (type == typeof(GetEntityTokenRequest) && _instance.OnAuthenticationGetEntityTokenRequestEvent != null)
				{
					_instance.OnAuthenticationGetEntityTokenRequestEvent((GetEntityTokenRequest)e.Request);
				}
				else if (type == typeof(ValidateEntityTokenRequest) && _instance.OnAuthenticationValidateEntityTokenRequestEvent != null)
				{
					_instance.OnAuthenticationValidateEntityTokenRequestEvent((ValidateEntityTokenRequest)e.Request);
				}
				else if (type == typeof(ExecuteEntityCloudScriptRequest) && _instance.OnCloudScriptExecuteEntityCloudScriptRequestEvent != null)
				{
					_instance.OnCloudScriptExecuteEntityCloudScriptRequestEvent((ExecuteEntityCloudScriptRequest)e.Request);
				}
				else if (type == typeof(ExecuteFunctionRequest) && _instance.OnCloudScriptExecuteFunctionRequestEvent != null)
				{
					_instance.OnCloudScriptExecuteFunctionRequestEvent((ExecuteFunctionRequest)e.Request);
				}
				else if (type == typeof(ListFunctionsRequest) && _instance.OnCloudScriptListFunctionsRequestEvent != null)
				{
					_instance.OnCloudScriptListFunctionsRequestEvent((ListFunctionsRequest)e.Request);
				}
				else if (type == typeof(ListFunctionsRequest) && _instance.OnCloudScriptListHttpFunctionsRequestEvent != null)
				{
					_instance.OnCloudScriptListHttpFunctionsRequestEvent((ListFunctionsRequest)e.Request);
				}
				else if (type == typeof(ListFunctionsRequest) && _instance.OnCloudScriptListQueuedFunctionsRequestEvent != null)
				{
					_instance.OnCloudScriptListQueuedFunctionsRequestEvent((ListFunctionsRequest)e.Request);
				}
				else if (type == typeof(PostFunctionResultForEntityTriggeredActionRequest) && _instance.OnCloudScriptPostFunctionResultForEntityTriggeredActionRequestEvent != null)
				{
					_instance.OnCloudScriptPostFunctionResultForEntityTriggeredActionRequestEvent((PostFunctionResultForEntityTriggeredActionRequest)e.Request);
				}
				else if (type == typeof(PostFunctionResultForFunctionExecutionRequest) && _instance.OnCloudScriptPostFunctionResultForFunctionExecutionRequestEvent != null)
				{
					_instance.OnCloudScriptPostFunctionResultForFunctionExecutionRequestEvent((PostFunctionResultForFunctionExecutionRequest)e.Request);
				}
				else if (type == typeof(PostFunctionResultForPlayerTriggeredActionRequest) && _instance.OnCloudScriptPostFunctionResultForPlayerTriggeredActionRequestEvent != null)
				{
					_instance.OnCloudScriptPostFunctionResultForPlayerTriggeredActionRequestEvent((PostFunctionResultForPlayerTriggeredActionRequest)e.Request);
				}
				else if (type == typeof(PostFunctionResultForScheduledTaskRequest) && _instance.OnCloudScriptPostFunctionResultForScheduledTaskRequestEvent != null)
				{
					_instance.OnCloudScriptPostFunctionResultForScheduledTaskRequestEvent((PostFunctionResultForScheduledTaskRequest)e.Request);
				}
				else if (type == typeof(RegisterHttpFunctionRequest) && _instance.OnCloudScriptRegisterHttpFunctionRequestEvent != null)
				{
					_instance.OnCloudScriptRegisterHttpFunctionRequestEvent((RegisterHttpFunctionRequest)e.Request);
				}
				else if (type == typeof(RegisterQueuedFunctionRequest) && _instance.OnCloudScriptRegisterQueuedFunctionRequestEvent != null)
				{
					_instance.OnCloudScriptRegisterQueuedFunctionRequestEvent((RegisterQueuedFunctionRequest)e.Request);
				}
				else if (type == typeof(UnregisterFunctionRequest) && _instance.OnCloudScriptUnregisterFunctionRequestEvent != null)
				{
					_instance.OnCloudScriptUnregisterFunctionRequestEvent((UnregisterFunctionRequest)e.Request);
				}
				else if (type == typeof(AbortFileUploadsRequest) && _instance.OnDataAbortFileUploadsRequestEvent != null)
				{
					_instance.OnDataAbortFileUploadsRequestEvent((AbortFileUploadsRequest)e.Request);
				}
				else if (type == typeof(DeleteFilesRequest) && _instance.OnDataDeleteFilesRequestEvent != null)
				{
					_instance.OnDataDeleteFilesRequestEvent((DeleteFilesRequest)e.Request);
				}
				else if (type == typeof(FinalizeFileUploadsRequest) && _instance.OnDataFinalizeFileUploadsRequestEvent != null)
				{
					_instance.OnDataFinalizeFileUploadsRequestEvent((FinalizeFileUploadsRequest)e.Request);
				}
				else if (type == typeof(GetFilesRequest) && _instance.OnDataGetFilesRequestEvent != null)
				{
					_instance.OnDataGetFilesRequestEvent((GetFilesRequest)e.Request);
				}
				else if (type == typeof(GetObjectsRequest) && _instance.OnDataGetObjectsRequestEvent != null)
				{
					_instance.OnDataGetObjectsRequestEvent((GetObjectsRequest)e.Request);
				}
				else if (type == typeof(InitiateFileUploadsRequest) && _instance.OnDataInitiateFileUploadsRequestEvent != null)
				{
					_instance.OnDataInitiateFileUploadsRequestEvent((InitiateFileUploadsRequest)e.Request);
				}
				else if (type == typeof(SetObjectsRequest) && _instance.OnDataSetObjectsRequestEvent != null)
				{
					_instance.OnDataSetObjectsRequestEvent((SetObjectsRequest)e.Request);
				}
				else if (type == typeof(WriteEventsRequest) && _instance.OnEventsWriteEventsRequestEvent != null)
				{
					_instance.OnEventsWriteEventsRequestEvent((WriteEventsRequest)e.Request);
				}
				else if (type == typeof(WriteEventsRequest) && _instance.OnEventsWriteTelemetryEventsRequestEvent != null)
				{
					_instance.OnEventsWriteTelemetryEventsRequestEvent((WriteEventsRequest)e.Request);
				}
				else if (type == typeof(CreateExclusionGroupRequest) && _instance.OnExperimentationCreateExclusionGroupRequestEvent != null)
				{
					_instance.OnExperimentationCreateExclusionGroupRequestEvent((CreateExclusionGroupRequest)e.Request);
				}
				else if (type == typeof(CreateExperimentRequest) && _instance.OnExperimentationCreateExperimentRequestEvent != null)
				{
					_instance.OnExperimentationCreateExperimentRequestEvent((CreateExperimentRequest)e.Request);
				}
				else if (type == typeof(DeleteExclusionGroupRequest) && _instance.OnExperimentationDeleteExclusionGroupRequestEvent != null)
				{
					_instance.OnExperimentationDeleteExclusionGroupRequestEvent((DeleteExclusionGroupRequest)e.Request);
				}
				else if (type == typeof(DeleteExperimentRequest) && _instance.OnExperimentationDeleteExperimentRequestEvent != null)
				{
					_instance.OnExperimentationDeleteExperimentRequestEvent((DeleteExperimentRequest)e.Request);
				}
				else if (type == typeof(GetExclusionGroupsRequest) && _instance.OnExperimentationGetExclusionGroupsRequestEvent != null)
				{
					_instance.OnExperimentationGetExclusionGroupsRequestEvent((GetExclusionGroupsRequest)e.Request);
				}
				else if (type == typeof(GetExclusionGroupTrafficRequest) && _instance.OnExperimentationGetExclusionGroupTrafficRequestEvent != null)
				{
					_instance.OnExperimentationGetExclusionGroupTrafficRequestEvent((GetExclusionGroupTrafficRequest)e.Request);
				}
				else if (type == typeof(GetExperimentsRequest) && _instance.OnExperimentationGetExperimentsRequestEvent != null)
				{
					_instance.OnExperimentationGetExperimentsRequestEvent((GetExperimentsRequest)e.Request);
				}
				else if (type == typeof(GetLatestScorecardRequest) && _instance.OnExperimentationGetLatestScorecardRequestEvent != null)
				{
					_instance.OnExperimentationGetLatestScorecardRequestEvent((GetLatestScorecardRequest)e.Request);
				}
				else if (type == typeof(GetTreatmentAssignmentRequest) && _instance.OnExperimentationGetTreatmentAssignmentRequestEvent != null)
				{
					_instance.OnExperimentationGetTreatmentAssignmentRequestEvent((GetTreatmentAssignmentRequest)e.Request);
				}
				else if (type == typeof(StartExperimentRequest) && _instance.OnExperimentationStartExperimentRequestEvent != null)
				{
					_instance.OnExperimentationStartExperimentRequestEvent((StartExperimentRequest)e.Request);
				}
				else if (type == typeof(StopExperimentRequest) && _instance.OnExperimentationStopExperimentRequestEvent != null)
				{
					_instance.OnExperimentationStopExperimentRequestEvent((StopExperimentRequest)e.Request);
				}
				else if (type == typeof(UpdateExclusionGroupRequest) && _instance.OnExperimentationUpdateExclusionGroupRequestEvent != null)
				{
					_instance.OnExperimentationUpdateExclusionGroupRequestEvent((UpdateExclusionGroupRequest)e.Request);
				}
				else if (type == typeof(UpdateExperimentRequest) && _instance.OnExperimentationUpdateExperimentRequestEvent != null)
				{
					_instance.OnExperimentationUpdateExperimentRequestEvent((UpdateExperimentRequest)e.Request);
				}
				else if (type == typeof(InsightsEmptyRequest) && _instance.OnInsightsGetDetailsRequestEvent != null)
				{
					_instance.OnInsightsGetDetailsRequestEvent((InsightsEmptyRequest)e.Request);
				}
				else if (type == typeof(InsightsEmptyRequest) && _instance.OnInsightsGetLimitsRequestEvent != null)
				{
					_instance.OnInsightsGetLimitsRequestEvent((InsightsEmptyRequest)e.Request);
				}
				else if (type == typeof(InsightsGetOperationStatusRequest) && _instance.OnInsightsGetOperationStatusRequestEvent != null)
				{
					_instance.OnInsightsGetOperationStatusRequestEvent((InsightsGetOperationStatusRequest)e.Request);
				}
				else if (type == typeof(InsightsGetPendingOperationsRequest) && _instance.OnInsightsGetPendingOperationsRequestEvent != null)
				{
					_instance.OnInsightsGetPendingOperationsRequestEvent((InsightsGetPendingOperationsRequest)e.Request);
				}
				else if (type == typeof(InsightsSetPerformanceRequest) && _instance.OnInsightsSetPerformanceRequestEvent != null)
				{
					_instance.OnInsightsSetPerformanceRequestEvent((InsightsSetPerformanceRequest)e.Request);
				}
				else if (type == typeof(InsightsSetStorageRetentionRequest) && _instance.OnInsightsSetStorageRetentionRequestEvent != null)
				{
					_instance.OnInsightsSetStorageRetentionRequestEvent((InsightsSetStorageRetentionRequest)e.Request);
				}
				else if (type == typeof(AcceptGroupApplicationRequest) && _instance.OnGroupsAcceptGroupApplicationRequestEvent != null)
				{
					_instance.OnGroupsAcceptGroupApplicationRequestEvent((AcceptGroupApplicationRequest)e.Request);
				}
				else if (type == typeof(AcceptGroupInvitationRequest) && _instance.OnGroupsAcceptGroupInvitationRequestEvent != null)
				{
					_instance.OnGroupsAcceptGroupInvitationRequestEvent((AcceptGroupInvitationRequest)e.Request);
				}
				else if (type == typeof(AddMembersRequest) && _instance.OnGroupsAddMembersRequestEvent != null)
				{
					_instance.OnGroupsAddMembersRequestEvent((AddMembersRequest)e.Request);
				}
				else if (type == typeof(ApplyToGroupRequest) && _instance.OnGroupsApplyToGroupRequestEvent != null)
				{
					_instance.OnGroupsApplyToGroupRequestEvent((ApplyToGroupRequest)e.Request);
				}
				else if (type == typeof(BlockEntityRequest) && _instance.OnGroupsBlockEntityRequestEvent != null)
				{
					_instance.OnGroupsBlockEntityRequestEvent((BlockEntityRequest)e.Request);
				}
				else if (type == typeof(ChangeMemberRoleRequest) && _instance.OnGroupsChangeMemberRoleRequestEvent != null)
				{
					_instance.OnGroupsChangeMemberRoleRequestEvent((ChangeMemberRoleRequest)e.Request);
				}
				else if (type == typeof(CreateGroupRequest) && _instance.OnGroupsCreateGroupRequestEvent != null)
				{
					_instance.OnGroupsCreateGroupRequestEvent((CreateGroupRequest)e.Request);
				}
				else if (type == typeof(CreateGroupRoleRequest) && _instance.OnGroupsCreateRoleRequestEvent != null)
				{
					_instance.OnGroupsCreateRoleRequestEvent((CreateGroupRoleRequest)e.Request);
				}
				else if (type == typeof(DeleteGroupRequest) && _instance.OnGroupsDeleteGroupRequestEvent != null)
				{
					_instance.OnGroupsDeleteGroupRequestEvent((DeleteGroupRequest)e.Request);
				}
				else if (type == typeof(DeleteRoleRequest) && _instance.OnGroupsDeleteRoleRequestEvent != null)
				{
					_instance.OnGroupsDeleteRoleRequestEvent((DeleteRoleRequest)e.Request);
				}
				else if (type == typeof(GetGroupRequest) && _instance.OnGroupsGetGroupRequestEvent != null)
				{
					_instance.OnGroupsGetGroupRequestEvent((GetGroupRequest)e.Request);
				}
				else if (type == typeof(InviteToGroupRequest) && _instance.OnGroupsInviteToGroupRequestEvent != null)
				{
					_instance.OnGroupsInviteToGroupRequestEvent((InviteToGroupRequest)e.Request);
				}
				else if (type == typeof(IsMemberRequest) && _instance.OnGroupsIsMemberRequestEvent != null)
				{
					_instance.OnGroupsIsMemberRequestEvent((IsMemberRequest)e.Request);
				}
				else if (type == typeof(ListGroupApplicationsRequest) && _instance.OnGroupsListGroupApplicationsRequestEvent != null)
				{
					_instance.OnGroupsListGroupApplicationsRequestEvent((ListGroupApplicationsRequest)e.Request);
				}
				else if (type == typeof(ListGroupBlocksRequest) && _instance.OnGroupsListGroupBlocksRequestEvent != null)
				{
					_instance.OnGroupsListGroupBlocksRequestEvent((ListGroupBlocksRequest)e.Request);
				}
				else if (type == typeof(ListGroupInvitationsRequest) && _instance.OnGroupsListGroupInvitationsRequestEvent != null)
				{
					_instance.OnGroupsListGroupInvitationsRequestEvent((ListGroupInvitationsRequest)e.Request);
				}
				else if (type == typeof(ListGroupMembersRequest) && _instance.OnGroupsListGroupMembersRequestEvent != null)
				{
					_instance.OnGroupsListGroupMembersRequestEvent((ListGroupMembersRequest)e.Request);
				}
				else if (type == typeof(ListMembershipRequest) && _instance.OnGroupsListMembershipRequestEvent != null)
				{
					_instance.OnGroupsListMembershipRequestEvent((ListMembershipRequest)e.Request);
				}
				else if (type == typeof(ListMembershipOpportunitiesRequest) && _instance.OnGroupsListMembershipOpportunitiesRequestEvent != null)
				{
					_instance.OnGroupsListMembershipOpportunitiesRequestEvent((ListMembershipOpportunitiesRequest)e.Request);
				}
				else if (type == typeof(RemoveGroupApplicationRequest) && _instance.OnGroupsRemoveGroupApplicationRequestEvent != null)
				{
					_instance.OnGroupsRemoveGroupApplicationRequestEvent((RemoveGroupApplicationRequest)e.Request);
				}
				else if (type == typeof(RemoveGroupInvitationRequest) && _instance.OnGroupsRemoveGroupInvitationRequestEvent != null)
				{
					_instance.OnGroupsRemoveGroupInvitationRequestEvent((RemoveGroupInvitationRequest)e.Request);
				}
				else if (type == typeof(RemoveMembersRequest) && _instance.OnGroupsRemoveMembersRequestEvent != null)
				{
					_instance.OnGroupsRemoveMembersRequestEvent((RemoveMembersRequest)e.Request);
				}
				else if (type == typeof(UnblockEntityRequest) && _instance.OnGroupsUnblockEntityRequestEvent != null)
				{
					_instance.OnGroupsUnblockEntityRequestEvent((UnblockEntityRequest)e.Request);
				}
				else if (type == typeof(UpdateGroupRequest) && _instance.OnGroupsUpdateGroupRequestEvent != null)
				{
					_instance.OnGroupsUpdateGroupRequestEvent((UpdateGroupRequest)e.Request);
				}
				else if (type == typeof(UpdateGroupRoleRequest) && _instance.OnGroupsUpdateRoleRequestEvent != null)
				{
					_instance.OnGroupsUpdateRoleRequestEvent((UpdateGroupRoleRequest)e.Request);
				}
				else if (type == typeof(GetLanguageListRequest) && _instance.OnLocalizationGetLanguageListRequestEvent != null)
				{
					_instance.OnLocalizationGetLanguageListRequestEvent((GetLanguageListRequest)e.Request);
				}
				else if (type == typeof(CancelAllMatchmakingTicketsForPlayerRequest) && _instance.OnMultiplayerCancelAllMatchmakingTicketsForPlayerRequestEvent != null)
				{
					_instance.OnMultiplayerCancelAllMatchmakingTicketsForPlayerRequestEvent((CancelAllMatchmakingTicketsForPlayerRequest)e.Request);
				}
				else if (type == typeof(CancelAllServerBackfillTicketsForPlayerRequest) && _instance.OnMultiplayerCancelAllServerBackfillTicketsForPlayerRequestEvent != null)
				{
					_instance.OnMultiplayerCancelAllServerBackfillTicketsForPlayerRequestEvent((CancelAllServerBackfillTicketsForPlayerRequest)e.Request);
				}
				else if (type == typeof(CancelMatchmakingTicketRequest) && _instance.OnMultiplayerCancelMatchmakingTicketRequestEvent != null)
				{
					_instance.OnMultiplayerCancelMatchmakingTicketRequestEvent((CancelMatchmakingTicketRequest)e.Request);
				}
				else if (type == typeof(CancelServerBackfillTicketRequest) && _instance.OnMultiplayerCancelServerBackfillTicketRequestEvent != null)
				{
					_instance.OnMultiplayerCancelServerBackfillTicketRequestEvent((CancelServerBackfillTicketRequest)e.Request);
				}
				else if (type == typeof(CreateBuildAliasRequest) && _instance.OnMultiplayerCreateBuildAliasRequestEvent != null)
				{
					_instance.OnMultiplayerCreateBuildAliasRequestEvent((CreateBuildAliasRequest)e.Request);
				}
				else if (type == typeof(CreateBuildWithCustomContainerRequest) && _instance.OnMultiplayerCreateBuildWithCustomContainerRequestEvent != null)
				{
					_instance.OnMultiplayerCreateBuildWithCustomContainerRequestEvent((CreateBuildWithCustomContainerRequest)e.Request);
				}
				else if (type == typeof(CreateBuildWithManagedContainerRequest) && _instance.OnMultiplayerCreateBuildWithManagedContainerRequestEvent != null)
				{
					_instance.OnMultiplayerCreateBuildWithManagedContainerRequestEvent((CreateBuildWithManagedContainerRequest)e.Request);
				}
				else if (type == typeof(CreateBuildWithProcessBasedServerRequest) && _instance.OnMultiplayerCreateBuildWithProcessBasedServerRequestEvent != null)
				{
					_instance.OnMultiplayerCreateBuildWithProcessBasedServerRequestEvent((CreateBuildWithProcessBasedServerRequest)e.Request);
				}
				else if (type == typeof(CreateMatchmakingTicketRequest) && _instance.OnMultiplayerCreateMatchmakingTicketRequestEvent != null)
				{
					_instance.OnMultiplayerCreateMatchmakingTicketRequestEvent((CreateMatchmakingTicketRequest)e.Request);
				}
				else if (type == typeof(CreateRemoteUserRequest) && _instance.OnMultiplayerCreateRemoteUserRequestEvent != null)
				{
					_instance.OnMultiplayerCreateRemoteUserRequestEvent((CreateRemoteUserRequest)e.Request);
				}
				else if (type == typeof(CreateServerBackfillTicketRequest) && _instance.OnMultiplayerCreateServerBackfillTicketRequestEvent != null)
				{
					_instance.OnMultiplayerCreateServerBackfillTicketRequestEvent((CreateServerBackfillTicketRequest)e.Request);
				}
				else if (type == typeof(CreateServerMatchmakingTicketRequest) && _instance.OnMultiplayerCreateServerMatchmakingTicketRequestEvent != null)
				{
					_instance.OnMultiplayerCreateServerMatchmakingTicketRequestEvent((CreateServerMatchmakingTicketRequest)e.Request);
				}
				else if (type == typeof(DeleteAssetRequest) && _instance.OnMultiplayerDeleteAssetRequestEvent != null)
				{
					_instance.OnMultiplayerDeleteAssetRequestEvent((DeleteAssetRequest)e.Request);
				}
				else if (type == typeof(DeleteBuildRequest) && _instance.OnMultiplayerDeleteBuildRequestEvent != null)
				{
					_instance.OnMultiplayerDeleteBuildRequestEvent((DeleteBuildRequest)e.Request);
				}
				else if (type == typeof(DeleteBuildAliasRequest) && _instance.OnMultiplayerDeleteBuildAliasRequestEvent != null)
				{
					_instance.OnMultiplayerDeleteBuildAliasRequestEvent((DeleteBuildAliasRequest)e.Request);
				}
				else if (type == typeof(DeleteBuildRegionRequest) && _instance.OnMultiplayerDeleteBuildRegionRequestEvent != null)
				{
					_instance.OnMultiplayerDeleteBuildRegionRequestEvent((DeleteBuildRegionRequest)e.Request);
				}
				else if (type == typeof(DeleteCertificateRequest) && _instance.OnMultiplayerDeleteCertificateRequestEvent != null)
				{
					_instance.OnMultiplayerDeleteCertificateRequestEvent((DeleteCertificateRequest)e.Request);
				}
				else if (type == typeof(DeleteContainerImageRequest) && _instance.OnMultiplayerDeleteContainerImageRepositoryRequestEvent != null)
				{
					_instance.OnMultiplayerDeleteContainerImageRepositoryRequestEvent((DeleteContainerImageRequest)e.Request);
				}
				else if (type == typeof(DeleteRemoteUserRequest) && _instance.OnMultiplayerDeleteRemoteUserRequestEvent != null)
				{
					_instance.OnMultiplayerDeleteRemoteUserRequestEvent((DeleteRemoteUserRequest)e.Request);
				}
				else if (type == typeof(EnableMultiplayerServersForTitleRequest) && _instance.OnMultiplayerEnableMultiplayerServersForTitleRequestEvent != null)
				{
					_instance.OnMultiplayerEnableMultiplayerServersForTitleRequestEvent((EnableMultiplayerServersForTitleRequest)e.Request);
				}
				else if (type == typeof(GetAssetUploadUrlRequest) && _instance.OnMultiplayerGetAssetUploadUrlRequestEvent != null)
				{
					_instance.OnMultiplayerGetAssetUploadUrlRequestEvent((GetAssetUploadUrlRequest)e.Request);
				}
				else if (type == typeof(GetBuildRequest) && _instance.OnMultiplayerGetBuildRequestEvent != null)
				{
					_instance.OnMultiplayerGetBuildRequestEvent((GetBuildRequest)e.Request);
				}
				else if (type == typeof(GetBuildAliasRequest) && _instance.OnMultiplayerGetBuildAliasRequestEvent != null)
				{
					_instance.OnMultiplayerGetBuildAliasRequestEvent((GetBuildAliasRequest)e.Request);
				}
				else if (type == typeof(GetContainerRegistryCredentialsRequest) && _instance.OnMultiplayerGetContainerRegistryCredentialsRequestEvent != null)
				{
					_instance.OnMultiplayerGetContainerRegistryCredentialsRequestEvent((GetContainerRegistryCredentialsRequest)e.Request);
				}
				else if (type == typeof(GetMatchRequest) && _instance.OnMultiplayerGetMatchRequestEvent != null)
				{
					_instance.OnMultiplayerGetMatchRequestEvent((GetMatchRequest)e.Request);
				}
				else if (type == typeof(GetMatchmakingQueueRequest) && _instance.OnMultiplayerGetMatchmakingQueueRequestEvent != null)
				{
					_instance.OnMultiplayerGetMatchmakingQueueRequestEvent((GetMatchmakingQueueRequest)e.Request);
				}
				else if (type == typeof(GetMatchmakingTicketRequest) && _instance.OnMultiplayerGetMatchmakingTicketRequestEvent != null)
				{
					_instance.OnMultiplayerGetMatchmakingTicketRequestEvent((GetMatchmakingTicketRequest)e.Request);
				}
				else if (type == typeof(GetMultiplayerServerDetailsRequest) && _instance.OnMultiplayerGetMultiplayerServerDetailsRequestEvent != null)
				{
					_instance.OnMultiplayerGetMultiplayerServerDetailsRequestEvent((GetMultiplayerServerDetailsRequest)e.Request);
				}
				else if (type == typeof(GetMultiplayerServerLogsRequest) && _instance.OnMultiplayerGetMultiplayerServerLogsRequestEvent != null)
				{
					_instance.OnMultiplayerGetMultiplayerServerLogsRequestEvent((GetMultiplayerServerLogsRequest)e.Request);
				}
				else if (type == typeof(GetMultiplayerSessionLogsBySessionIdRequest) && _instance.OnMultiplayerGetMultiplayerSessionLogsBySessionIdRequestEvent != null)
				{
					_instance.OnMultiplayerGetMultiplayerSessionLogsBySessionIdRequestEvent((GetMultiplayerSessionLogsBySessionIdRequest)e.Request);
				}
				else if (type == typeof(GetQueueStatisticsRequest) && _instance.OnMultiplayerGetQueueStatisticsRequestEvent != null)
				{
					_instance.OnMultiplayerGetQueueStatisticsRequestEvent((GetQueueStatisticsRequest)e.Request);
				}
				else if (type == typeof(GetRemoteLoginEndpointRequest) && _instance.OnMultiplayerGetRemoteLoginEndpointRequestEvent != null)
				{
					_instance.OnMultiplayerGetRemoteLoginEndpointRequestEvent((GetRemoteLoginEndpointRequest)e.Request);
				}
				else if (type == typeof(GetServerBackfillTicketRequest) && _instance.OnMultiplayerGetServerBackfillTicketRequestEvent != null)
				{
					_instance.OnMultiplayerGetServerBackfillTicketRequestEvent((GetServerBackfillTicketRequest)e.Request);
				}
				else if (type == typeof(GetTitleEnabledForMultiplayerServersStatusRequest) && _instance.OnMultiplayerGetTitleEnabledForMultiplayerServersStatusRequestEvent != null)
				{
					_instance.OnMultiplayerGetTitleEnabledForMultiplayerServersStatusRequestEvent((GetTitleEnabledForMultiplayerServersStatusRequest)e.Request);
				}
				else if (type == typeof(GetTitleMultiplayerServersQuotasRequest) && _instance.OnMultiplayerGetTitleMultiplayerServersQuotasRequestEvent != null)
				{
					_instance.OnMultiplayerGetTitleMultiplayerServersQuotasRequestEvent((GetTitleMultiplayerServersQuotasRequest)e.Request);
				}
				else if (type == typeof(JoinMatchmakingTicketRequest) && _instance.OnMultiplayerJoinMatchmakingTicketRequestEvent != null)
				{
					_instance.OnMultiplayerJoinMatchmakingTicketRequestEvent((JoinMatchmakingTicketRequest)e.Request);
				}
				else if (type == typeof(ListMultiplayerServersRequest) && _instance.OnMultiplayerListArchivedMultiplayerServersRequestEvent != null)
				{
					_instance.OnMultiplayerListArchivedMultiplayerServersRequestEvent((ListMultiplayerServersRequest)e.Request);
				}
				else if (type == typeof(ListAssetSummariesRequest) && _instance.OnMultiplayerListAssetSummariesRequestEvent != null)
				{
					_instance.OnMultiplayerListAssetSummariesRequestEvent((ListAssetSummariesRequest)e.Request);
				}
				else if (type == typeof(MultiplayerEmptyRequest) && _instance.OnMultiplayerListBuildAliasesRequestEvent != null)
				{
					_instance.OnMultiplayerListBuildAliasesRequestEvent((MultiplayerEmptyRequest)e.Request);
				}
				else if (type == typeof(ListBuildSummariesRequest) && _instance.OnMultiplayerListBuildSummariesRequestEvent != null)
				{
					_instance.OnMultiplayerListBuildSummariesRequestEvent((ListBuildSummariesRequest)e.Request);
				}
				else if (type == typeof(ListCertificateSummariesRequest) && _instance.OnMultiplayerListCertificateSummariesRequestEvent != null)
				{
					_instance.OnMultiplayerListCertificateSummariesRequestEvent((ListCertificateSummariesRequest)e.Request);
				}
				else if (type == typeof(ListContainerImagesRequest) && _instance.OnMultiplayerListContainerImagesRequestEvent != null)
				{
					_instance.OnMultiplayerListContainerImagesRequestEvent((ListContainerImagesRequest)e.Request);
				}
				else if (type == typeof(ListContainerImageTagsRequest) && _instance.OnMultiplayerListContainerImageTagsRequestEvent != null)
				{
					_instance.OnMultiplayerListContainerImageTagsRequestEvent((ListContainerImageTagsRequest)e.Request);
				}
				else if (type == typeof(ListMatchmakingQueuesRequest) && _instance.OnMultiplayerListMatchmakingQueuesRequestEvent != null)
				{
					_instance.OnMultiplayerListMatchmakingQueuesRequestEvent((ListMatchmakingQueuesRequest)e.Request);
				}
				else if (type == typeof(ListMatchmakingTicketsForPlayerRequest) && _instance.OnMultiplayerListMatchmakingTicketsForPlayerRequestEvent != null)
				{
					_instance.OnMultiplayerListMatchmakingTicketsForPlayerRequestEvent((ListMatchmakingTicketsForPlayerRequest)e.Request);
				}
				else if (type == typeof(ListMultiplayerServersRequest) && _instance.OnMultiplayerListMultiplayerServersRequestEvent != null)
				{
					_instance.OnMultiplayerListMultiplayerServersRequestEvent((ListMultiplayerServersRequest)e.Request);
				}
				else if (type == typeof(ListPartyQosServersRequest) && _instance.OnMultiplayerListPartyQosServersRequestEvent != null)
				{
					_instance.OnMultiplayerListPartyQosServersRequestEvent((ListPartyQosServersRequest)e.Request);
				}
				else if (type == typeof(ListQosServersForTitleRequest) && _instance.OnMultiplayerListQosServersForTitleRequestEvent != null)
				{
					_instance.OnMultiplayerListQosServersForTitleRequestEvent((ListQosServersForTitleRequest)e.Request);
				}
				else if (type == typeof(ListServerBackfillTicketsForPlayerRequest) && _instance.OnMultiplayerListServerBackfillTicketsForPlayerRequestEvent != null)
				{
					_instance.OnMultiplayerListServerBackfillTicketsForPlayerRequestEvent((ListServerBackfillTicketsForPlayerRequest)e.Request);
				}
				else if (type == typeof(ListVirtualMachineSummariesRequest) && _instance.OnMultiplayerListVirtualMachineSummariesRequestEvent != null)
				{
					_instance.OnMultiplayerListVirtualMachineSummariesRequestEvent((ListVirtualMachineSummariesRequest)e.Request);
				}
				else if (type == typeof(RemoveMatchmakingQueueRequest) && _instance.OnMultiplayerRemoveMatchmakingQueueRequestEvent != null)
				{
					_instance.OnMultiplayerRemoveMatchmakingQueueRequestEvent((RemoveMatchmakingQueueRequest)e.Request);
				}
				else if (type == typeof(RequestMultiplayerServerRequest) && _instance.OnMultiplayerRequestMultiplayerServerRequestEvent != null)
				{
					_instance.OnMultiplayerRequestMultiplayerServerRequestEvent((RequestMultiplayerServerRequest)e.Request);
				}
				else if (type == typeof(RolloverContainerRegistryCredentialsRequest) && _instance.OnMultiplayerRolloverContainerRegistryCredentialsRequestEvent != null)
				{
					_instance.OnMultiplayerRolloverContainerRegistryCredentialsRequestEvent((RolloverContainerRegistryCredentialsRequest)e.Request);
				}
				else if (type == typeof(SetMatchmakingQueueRequest) && _instance.OnMultiplayerSetMatchmakingQueueRequestEvent != null)
				{
					_instance.OnMultiplayerSetMatchmakingQueueRequestEvent((SetMatchmakingQueueRequest)e.Request);
				}
				else if (type == typeof(ShutdownMultiplayerServerRequest) && _instance.OnMultiplayerShutdownMultiplayerServerRequestEvent != null)
				{
					_instance.OnMultiplayerShutdownMultiplayerServerRequestEvent((ShutdownMultiplayerServerRequest)e.Request);
				}
				else if (type == typeof(UntagContainerImageRequest) && _instance.OnMultiplayerUntagContainerImageRequestEvent != null)
				{
					_instance.OnMultiplayerUntagContainerImageRequestEvent((UntagContainerImageRequest)e.Request);
				}
				else if (type == typeof(UpdateBuildAliasRequest) && _instance.OnMultiplayerUpdateBuildAliasRequestEvent != null)
				{
					_instance.OnMultiplayerUpdateBuildAliasRequestEvent((UpdateBuildAliasRequest)e.Request);
				}
				else if (type == typeof(UpdateBuildRegionRequest) && _instance.OnMultiplayerUpdateBuildRegionRequestEvent != null)
				{
					_instance.OnMultiplayerUpdateBuildRegionRequestEvent((UpdateBuildRegionRequest)e.Request);
				}
				else if (type == typeof(UpdateBuildRegionsRequest) && _instance.OnMultiplayerUpdateBuildRegionsRequestEvent != null)
				{
					_instance.OnMultiplayerUpdateBuildRegionsRequestEvent((UpdateBuildRegionsRequest)e.Request);
				}
				else if (type == typeof(UploadCertificateRequest) && _instance.OnMultiplayerUploadCertificateRequestEvent != null)
				{
					_instance.OnMultiplayerUploadCertificateRequestEvent((UploadCertificateRequest)e.Request);
				}
				else if (type == typeof(GetGlobalPolicyRequest) && _instance.OnProfilesGetGlobalPolicyRequestEvent != null)
				{
					_instance.OnProfilesGetGlobalPolicyRequestEvent((GetGlobalPolicyRequest)e.Request);
				}
				else if (type == typeof(GetEntityProfileRequest) && _instance.OnProfilesGetProfileRequestEvent != null)
				{
					_instance.OnProfilesGetProfileRequestEvent((GetEntityProfileRequest)e.Request);
				}
				else if (type == typeof(GetEntityProfilesRequest) && _instance.OnProfilesGetProfilesRequestEvent != null)
				{
					_instance.OnProfilesGetProfilesRequestEvent((GetEntityProfilesRequest)e.Request);
				}
				else if (type == typeof(GetTitlePlayersFromMasterPlayerAccountIdsRequest) && _instance.OnProfilesGetTitlePlayersFromMasterPlayerAccountIdsRequestEvent != null)
				{
					_instance.OnProfilesGetTitlePlayersFromMasterPlayerAccountIdsRequestEvent((GetTitlePlayersFromMasterPlayerAccountIdsRequest)e.Request);
				}
				else if (type == typeof(SetGlobalPolicyRequest) && _instance.OnProfilesSetGlobalPolicyRequestEvent != null)
				{
					_instance.OnProfilesSetGlobalPolicyRequestEvent((SetGlobalPolicyRequest)e.Request);
				}
				else if (type == typeof(SetProfileLanguageRequest) && _instance.OnProfilesSetProfileLanguageRequestEvent != null)
				{
					_instance.OnProfilesSetProfileLanguageRequestEvent((SetProfileLanguageRequest)e.Request);
				}
				else if (type == typeof(SetEntityProfilePolicyRequest) && _instance.OnProfilesSetProfilePolicyRequestEvent != null)
				{
					_instance.OnProfilesSetProfilePolicyRequestEvent((SetEntityProfilePolicyRequest)e.Request);
				}
			}
			else
			{
				Type type2 = e.Result.GetType();
				if (type2 == typeof(LoginResult) && _instance.OnLoginResultEvent != null)
				{
					_instance.OnLoginResultEvent((LoginResult)e.Result);
				}
				else if (type2 == typeof(AcceptTradeResponse) && _instance.OnAcceptTradeResultEvent != null)
				{
					_instance.OnAcceptTradeResultEvent((AcceptTradeResponse)e.Result);
				}
				else if (type2 == typeof(AddFriendResult) && _instance.OnAddFriendResultEvent != null)
				{
					_instance.OnAddFriendResultEvent((AddFriendResult)e.Result);
				}
				else if (type2 == typeof(AddGenericIDResult) && _instance.OnAddGenericIDResultEvent != null)
				{
					_instance.OnAddGenericIDResultEvent((AddGenericIDResult)e.Result);
				}
				else if (type2 == typeof(AddOrUpdateContactEmailResult) && _instance.OnAddOrUpdateContactEmailResultEvent != null)
				{
					_instance.OnAddOrUpdateContactEmailResultEvent((AddOrUpdateContactEmailResult)e.Result);
				}
				else if (type2 == typeof(AddSharedGroupMembersResult) && _instance.OnAddSharedGroupMembersResultEvent != null)
				{
					_instance.OnAddSharedGroupMembersResultEvent((AddSharedGroupMembersResult)e.Result);
				}
				else if (type2 == typeof(AddUsernamePasswordResult) && _instance.OnAddUsernamePasswordResultEvent != null)
				{
					_instance.OnAddUsernamePasswordResultEvent((AddUsernamePasswordResult)e.Result);
				}
				else if (type2 == typeof(ModifyUserVirtualCurrencyResult) && _instance.OnAddUserVirtualCurrencyResultEvent != null)
				{
					_instance.OnAddUserVirtualCurrencyResultEvent((ModifyUserVirtualCurrencyResult)e.Result);
				}
				else if (type2 == typeof(AndroidDevicePushNotificationRegistrationResult) && _instance.OnAndroidDevicePushNotificationRegistrationResultEvent != null)
				{
					_instance.OnAndroidDevicePushNotificationRegistrationResultEvent((AndroidDevicePushNotificationRegistrationResult)e.Result);
				}
				else if (type2 == typeof(AttributeInstallResult) && _instance.OnAttributeInstallResultEvent != null)
				{
					_instance.OnAttributeInstallResultEvent((AttributeInstallResult)e.Result);
				}
				else if (type2 == typeof(CancelTradeResponse) && _instance.OnCancelTradeResultEvent != null)
				{
					_instance.OnCancelTradeResultEvent((CancelTradeResponse)e.Result);
				}
				else if (type2 == typeof(ConfirmPurchaseResult) && _instance.OnConfirmPurchaseResultEvent != null)
				{
					_instance.OnConfirmPurchaseResultEvent((ConfirmPurchaseResult)e.Result);
				}
				else if (type2 == typeof(ConsumeItemResult) && _instance.OnConsumeItemResultEvent != null)
				{
					_instance.OnConsumeItemResultEvent((ConsumeItemResult)e.Result);
				}
				else if (type2 == typeof(ConsumeMicrosoftStoreEntitlementsResponse) && _instance.OnConsumeMicrosoftStoreEntitlementsResultEvent != null)
				{
					_instance.OnConsumeMicrosoftStoreEntitlementsResultEvent((ConsumeMicrosoftStoreEntitlementsResponse)e.Result);
				}
				else if (type2 == typeof(ConsumePSNEntitlementsResult) && _instance.OnConsumePSNEntitlementsResultEvent != null)
				{
					_instance.OnConsumePSNEntitlementsResultEvent((ConsumePSNEntitlementsResult)e.Result);
				}
				else if (type2 == typeof(ConsumeXboxEntitlementsResult) && _instance.OnConsumeXboxEntitlementsResultEvent != null)
				{
					_instance.OnConsumeXboxEntitlementsResultEvent((ConsumeXboxEntitlementsResult)e.Result);
				}
				else if (type2 == typeof(CreateSharedGroupResult) && _instance.OnCreateSharedGroupResultEvent != null)
				{
					_instance.OnCreateSharedGroupResultEvent((CreateSharedGroupResult)e.Result);
				}
				else if (type2 == typeof(PlayFab.ClientModels.ExecuteCloudScriptResult) && _instance.OnExecuteCloudScriptResultEvent != null)
				{
					_instance.OnExecuteCloudScriptResultEvent((PlayFab.ClientModels.ExecuteCloudScriptResult)e.Result);
				}
				else if (type2 == typeof(GetAccountInfoResult) && _instance.OnGetAccountInfoResultEvent != null)
				{
					_instance.OnGetAccountInfoResultEvent((GetAccountInfoResult)e.Result);
				}
				else if (type2 == typeof(GetAdPlacementsResult) && _instance.OnGetAdPlacementsResultEvent != null)
				{
					_instance.OnGetAdPlacementsResultEvent((GetAdPlacementsResult)e.Result);
				}
				else if (type2 == typeof(ListUsersCharactersResult) && _instance.OnGetAllUsersCharactersResultEvent != null)
				{
					_instance.OnGetAllUsersCharactersResultEvent((ListUsersCharactersResult)e.Result);
				}
				else if (type2 == typeof(GetCatalogItemsResult) && _instance.OnGetCatalogItemsResultEvent != null)
				{
					_instance.OnGetCatalogItemsResultEvent((GetCatalogItemsResult)e.Result);
				}
				else if (type2 == typeof(GetCharacterDataResult) && _instance.OnGetCharacterDataResultEvent != null)
				{
					_instance.OnGetCharacterDataResultEvent((GetCharacterDataResult)e.Result);
				}
				else if (type2 == typeof(GetCharacterInventoryResult) && _instance.OnGetCharacterInventoryResultEvent != null)
				{
					_instance.OnGetCharacterInventoryResultEvent((GetCharacterInventoryResult)e.Result);
				}
				else if (type2 == typeof(GetCharacterLeaderboardResult) && _instance.OnGetCharacterLeaderboardResultEvent != null)
				{
					_instance.OnGetCharacterLeaderboardResultEvent((GetCharacterLeaderboardResult)e.Result);
				}
				else if (type2 == typeof(GetCharacterDataResult) && _instance.OnGetCharacterReadOnlyDataResultEvent != null)
				{
					_instance.OnGetCharacterReadOnlyDataResultEvent((GetCharacterDataResult)e.Result);
				}
				else if (type2 == typeof(GetCharacterStatisticsResult) && _instance.OnGetCharacterStatisticsResultEvent != null)
				{
					_instance.OnGetCharacterStatisticsResultEvent((GetCharacterStatisticsResult)e.Result);
				}
				else if (type2 == typeof(GetContentDownloadUrlResult) && _instance.OnGetContentDownloadUrlResultEvent != null)
				{
					_instance.OnGetContentDownloadUrlResultEvent((GetContentDownloadUrlResult)e.Result);
				}
				else if (type2 == typeof(CurrentGamesResult) && _instance.OnGetCurrentGamesResultEvent != null)
				{
					_instance.OnGetCurrentGamesResultEvent((CurrentGamesResult)e.Result);
				}
				else if (type2 == typeof(GetLeaderboardResult) && _instance.OnGetFriendLeaderboardResultEvent != null)
				{
					_instance.OnGetFriendLeaderboardResultEvent((GetLeaderboardResult)e.Result);
				}
				else if (type2 == typeof(GetFriendLeaderboardAroundPlayerResult) && _instance.OnGetFriendLeaderboardAroundPlayerResultEvent != null)
				{
					_instance.OnGetFriendLeaderboardAroundPlayerResultEvent((GetFriendLeaderboardAroundPlayerResult)e.Result);
				}
				else if (type2 == typeof(GetFriendsListResult) && _instance.OnGetFriendsListResultEvent != null)
				{
					_instance.OnGetFriendsListResultEvent((GetFriendsListResult)e.Result);
				}
				else if (type2 == typeof(GameServerRegionsResult) && _instance.OnGetGameServerRegionsResultEvent != null)
				{
					_instance.OnGetGameServerRegionsResultEvent((GameServerRegionsResult)e.Result);
				}
				else if (type2 == typeof(GetLeaderboardResult) && _instance.OnGetLeaderboardResultEvent != null)
				{
					_instance.OnGetLeaderboardResultEvent((GetLeaderboardResult)e.Result);
				}
				else if (type2 == typeof(GetLeaderboardAroundCharacterResult) && _instance.OnGetLeaderboardAroundCharacterResultEvent != null)
				{
					_instance.OnGetLeaderboardAroundCharacterResultEvent((GetLeaderboardAroundCharacterResult)e.Result);
				}
				else if (type2 == typeof(GetLeaderboardAroundPlayerResult) && _instance.OnGetLeaderboardAroundPlayerResultEvent != null)
				{
					_instance.OnGetLeaderboardAroundPlayerResultEvent((GetLeaderboardAroundPlayerResult)e.Result);
				}
				else if (type2 == typeof(GetLeaderboardForUsersCharactersResult) && _instance.OnGetLeaderboardForUserCharactersResultEvent != null)
				{
					_instance.OnGetLeaderboardForUserCharactersResultEvent((GetLeaderboardForUsersCharactersResult)e.Result);
				}
				else if (type2 == typeof(GetPaymentTokenResult) && _instance.OnGetPaymentTokenResultEvent != null)
				{
					_instance.OnGetPaymentTokenResultEvent((GetPaymentTokenResult)e.Result);
				}
				else if (type2 == typeof(GetPhotonAuthenticationTokenResult) && _instance.OnGetPhotonAuthenticationTokenResultEvent != null)
				{
					_instance.OnGetPhotonAuthenticationTokenResultEvent((GetPhotonAuthenticationTokenResult)e.Result);
				}
				else if (type2 == typeof(GetPlayerCombinedInfoResult) && _instance.OnGetPlayerCombinedInfoResultEvent != null)
				{
					_instance.OnGetPlayerCombinedInfoResultEvent((GetPlayerCombinedInfoResult)e.Result);
				}
				else if (type2 == typeof(GetPlayerProfileResult) && _instance.OnGetPlayerProfileResultEvent != null)
				{
					_instance.OnGetPlayerProfileResultEvent((GetPlayerProfileResult)e.Result);
				}
				else if (type2 == typeof(GetPlayerSegmentsResult) && _instance.OnGetPlayerSegmentsResultEvent != null)
				{
					_instance.OnGetPlayerSegmentsResultEvent((GetPlayerSegmentsResult)e.Result);
				}
				else if (type2 == typeof(GetPlayerStatisticsResult) && _instance.OnGetPlayerStatisticsResultEvent != null)
				{
					_instance.OnGetPlayerStatisticsResultEvent((GetPlayerStatisticsResult)e.Result);
				}
				else if (type2 == typeof(GetPlayerStatisticVersionsResult) && _instance.OnGetPlayerStatisticVersionsResultEvent != null)
				{
					_instance.OnGetPlayerStatisticVersionsResultEvent((GetPlayerStatisticVersionsResult)e.Result);
				}
				else if (type2 == typeof(GetPlayerTagsResult) && _instance.OnGetPlayerTagsResultEvent != null)
				{
					_instance.OnGetPlayerTagsResultEvent((GetPlayerTagsResult)e.Result);
				}
				else if (type2 == typeof(GetPlayerTradesResponse) && _instance.OnGetPlayerTradesResultEvent != null)
				{
					_instance.OnGetPlayerTradesResultEvent((GetPlayerTradesResponse)e.Result);
				}
				else if (type2 == typeof(GetPlayFabIDsFromFacebookIDsResult) && _instance.OnGetPlayFabIDsFromFacebookIDsResultEvent != null)
				{
					_instance.OnGetPlayFabIDsFromFacebookIDsResultEvent((GetPlayFabIDsFromFacebookIDsResult)e.Result);
				}
				else if (type2 == typeof(GetPlayFabIDsFromFacebookInstantGamesIdsResult) && _instance.OnGetPlayFabIDsFromFacebookInstantGamesIdsResultEvent != null)
				{
					_instance.OnGetPlayFabIDsFromFacebookInstantGamesIdsResultEvent((GetPlayFabIDsFromFacebookInstantGamesIdsResult)e.Result);
				}
				else if (type2 == typeof(GetPlayFabIDsFromGameCenterIDsResult) && _instance.OnGetPlayFabIDsFromGameCenterIDsResultEvent != null)
				{
					_instance.OnGetPlayFabIDsFromGameCenterIDsResultEvent((GetPlayFabIDsFromGameCenterIDsResult)e.Result);
				}
				else if (type2 == typeof(GetPlayFabIDsFromGenericIDsResult) && _instance.OnGetPlayFabIDsFromGenericIDsResultEvent != null)
				{
					_instance.OnGetPlayFabIDsFromGenericIDsResultEvent((GetPlayFabIDsFromGenericIDsResult)e.Result);
				}
				else if (type2 == typeof(GetPlayFabIDsFromGoogleIDsResult) && _instance.OnGetPlayFabIDsFromGoogleIDsResultEvent != null)
				{
					_instance.OnGetPlayFabIDsFromGoogleIDsResultEvent((GetPlayFabIDsFromGoogleIDsResult)e.Result);
				}
				else if (type2 == typeof(GetPlayFabIDsFromKongregateIDsResult) && _instance.OnGetPlayFabIDsFromKongregateIDsResultEvent != null)
				{
					_instance.OnGetPlayFabIDsFromKongregateIDsResultEvent((GetPlayFabIDsFromKongregateIDsResult)e.Result);
				}
				else if (type2 == typeof(GetPlayFabIDsFromNintendoSwitchDeviceIdsResult) && _instance.OnGetPlayFabIDsFromNintendoSwitchDeviceIdsResultEvent != null)
				{
					_instance.OnGetPlayFabIDsFromNintendoSwitchDeviceIdsResultEvent((GetPlayFabIDsFromNintendoSwitchDeviceIdsResult)e.Result);
				}
				else if (type2 == typeof(GetPlayFabIDsFromPSNAccountIDsResult) && _instance.OnGetPlayFabIDsFromPSNAccountIDsResultEvent != null)
				{
					_instance.OnGetPlayFabIDsFromPSNAccountIDsResultEvent((GetPlayFabIDsFromPSNAccountIDsResult)e.Result);
				}
				else if (type2 == typeof(GetPlayFabIDsFromSteamIDsResult) && _instance.OnGetPlayFabIDsFromSteamIDsResultEvent != null)
				{
					_instance.OnGetPlayFabIDsFromSteamIDsResultEvent((GetPlayFabIDsFromSteamIDsResult)e.Result);
				}
				else if (type2 == typeof(GetPlayFabIDsFromTwitchIDsResult) && _instance.OnGetPlayFabIDsFromTwitchIDsResultEvent != null)
				{
					_instance.OnGetPlayFabIDsFromTwitchIDsResultEvent((GetPlayFabIDsFromTwitchIDsResult)e.Result);
				}
				else if (type2 == typeof(GetPlayFabIDsFromXboxLiveIDsResult) && _instance.OnGetPlayFabIDsFromXboxLiveIDsResultEvent != null)
				{
					_instance.OnGetPlayFabIDsFromXboxLiveIDsResultEvent((GetPlayFabIDsFromXboxLiveIDsResult)e.Result);
				}
				else if (type2 == typeof(GetPublisherDataResult) && _instance.OnGetPublisherDataResultEvent != null)
				{
					_instance.OnGetPublisherDataResultEvent((GetPublisherDataResult)e.Result);
				}
				else if (type2 == typeof(GetPurchaseResult) && _instance.OnGetPurchaseResultEvent != null)
				{
					_instance.OnGetPurchaseResultEvent((GetPurchaseResult)e.Result);
				}
				else if (type2 == typeof(GetSharedGroupDataResult) && _instance.OnGetSharedGroupDataResultEvent != null)
				{
					_instance.OnGetSharedGroupDataResultEvent((GetSharedGroupDataResult)e.Result);
				}
				else if (type2 == typeof(GetStoreItemsResult) && _instance.OnGetStoreItemsResultEvent != null)
				{
					_instance.OnGetStoreItemsResultEvent((GetStoreItemsResult)e.Result);
				}
				else if (type2 == typeof(GetTimeResult) && _instance.OnGetTimeResultEvent != null)
				{
					_instance.OnGetTimeResultEvent((GetTimeResult)e.Result);
				}
				else if (type2 == typeof(GetTitleDataResult) && _instance.OnGetTitleDataResultEvent != null)
				{
					_instance.OnGetTitleDataResultEvent((GetTitleDataResult)e.Result);
				}
				else if (type2 == typeof(GetTitleNewsResult) && _instance.OnGetTitleNewsResultEvent != null)
				{
					_instance.OnGetTitleNewsResultEvent((GetTitleNewsResult)e.Result);
				}
				else if (type2 == typeof(GetTitlePublicKeyResult) && _instance.OnGetTitlePublicKeyResultEvent != null)
				{
					_instance.OnGetTitlePublicKeyResultEvent((GetTitlePublicKeyResult)e.Result);
				}
				else if (type2 == typeof(GetTradeStatusResponse) && _instance.OnGetTradeStatusResultEvent != null)
				{
					_instance.OnGetTradeStatusResultEvent((GetTradeStatusResponse)e.Result);
				}
				else if (type2 == typeof(GetUserDataResult) && _instance.OnGetUserDataResultEvent != null)
				{
					_instance.OnGetUserDataResultEvent((GetUserDataResult)e.Result);
				}
				else if (type2 == typeof(GetUserInventoryResult) && _instance.OnGetUserInventoryResultEvent != null)
				{
					_instance.OnGetUserInventoryResultEvent((GetUserInventoryResult)e.Result);
				}
				else if (type2 == typeof(GetUserDataResult) && _instance.OnGetUserPublisherDataResultEvent != null)
				{
					_instance.OnGetUserPublisherDataResultEvent((GetUserDataResult)e.Result);
				}
				else if (type2 == typeof(GetUserDataResult) && _instance.OnGetUserPublisherReadOnlyDataResultEvent != null)
				{
					_instance.OnGetUserPublisherReadOnlyDataResultEvent((GetUserDataResult)e.Result);
				}
				else if (type2 == typeof(GetUserDataResult) && _instance.OnGetUserReadOnlyDataResultEvent != null)
				{
					_instance.OnGetUserReadOnlyDataResultEvent((GetUserDataResult)e.Result);
				}
				else if (type2 == typeof(GetWindowsHelloChallengeResponse) && _instance.OnGetWindowsHelloChallengeResultEvent != null)
				{
					_instance.OnGetWindowsHelloChallengeResultEvent((GetWindowsHelloChallengeResponse)e.Result);
				}
				else if (type2 == typeof(GrantCharacterToUserResult) && _instance.OnGrantCharacterToUserResultEvent != null)
				{
					_instance.OnGrantCharacterToUserResultEvent((GrantCharacterToUserResult)e.Result);
				}
				else if (type2 == typeof(LinkAndroidDeviceIDResult) && _instance.OnLinkAndroidDeviceIDResultEvent != null)
				{
					_instance.OnLinkAndroidDeviceIDResultEvent((LinkAndroidDeviceIDResult)e.Result);
				}
				else if (type2 == typeof(PlayFab.ClientModels.EmptyResult) && _instance.OnLinkAppleResultEvent != null)
				{
					_instance.OnLinkAppleResultEvent((PlayFab.ClientModels.EmptyResult)e.Result);
				}
				else if (type2 == typeof(LinkCustomIDResult) && _instance.OnLinkCustomIDResultEvent != null)
				{
					_instance.OnLinkCustomIDResultEvent((LinkCustomIDResult)e.Result);
				}
				else if (type2 == typeof(LinkFacebookAccountResult) && _instance.OnLinkFacebookAccountResultEvent != null)
				{
					_instance.OnLinkFacebookAccountResultEvent((LinkFacebookAccountResult)e.Result);
				}
				else if (type2 == typeof(LinkFacebookInstantGamesIdResult) && _instance.OnLinkFacebookInstantGamesIdResultEvent != null)
				{
					_instance.OnLinkFacebookInstantGamesIdResultEvent((LinkFacebookInstantGamesIdResult)e.Result);
				}
				else if (type2 == typeof(LinkGameCenterAccountResult) && _instance.OnLinkGameCenterAccountResultEvent != null)
				{
					_instance.OnLinkGameCenterAccountResultEvent((LinkGameCenterAccountResult)e.Result);
				}
				else if (type2 == typeof(LinkGoogleAccountResult) && _instance.OnLinkGoogleAccountResultEvent != null)
				{
					_instance.OnLinkGoogleAccountResultEvent((LinkGoogleAccountResult)e.Result);
				}
				else if (type2 == typeof(LinkIOSDeviceIDResult) && _instance.OnLinkIOSDeviceIDResultEvent != null)
				{
					_instance.OnLinkIOSDeviceIDResultEvent((LinkIOSDeviceIDResult)e.Result);
				}
				else if (type2 == typeof(LinkKongregateAccountResult) && _instance.OnLinkKongregateResultEvent != null)
				{
					_instance.OnLinkKongregateResultEvent((LinkKongregateAccountResult)e.Result);
				}
				else if (type2 == typeof(PlayFab.ClientModels.EmptyResult) && _instance.OnLinkNintendoServiceAccountResultEvent != null)
				{
					_instance.OnLinkNintendoServiceAccountResultEvent((PlayFab.ClientModels.EmptyResult)e.Result);
				}
				else if (type2 == typeof(LinkNintendoSwitchDeviceIdResult) && _instance.OnLinkNintendoSwitchDeviceIdResultEvent != null)
				{
					_instance.OnLinkNintendoSwitchDeviceIdResultEvent((LinkNintendoSwitchDeviceIdResult)e.Result);
				}
				else if (type2 == typeof(PlayFab.ClientModels.EmptyResult) && _instance.OnLinkOpenIdConnectResultEvent != null)
				{
					_instance.OnLinkOpenIdConnectResultEvent((PlayFab.ClientModels.EmptyResult)e.Result);
				}
				else if (type2 == typeof(LinkPSNAccountResult) && _instance.OnLinkPSNAccountResultEvent != null)
				{
					_instance.OnLinkPSNAccountResultEvent((LinkPSNAccountResult)e.Result);
				}
				else if (type2 == typeof(LinkSteamAccountResult) && _instance.OnLinkSteamAccountResultEvent != null)
				{
					_instance.OnLinkSteamAccountResultEvent((LinkSteamAccountResult)e.Result);
				}
				else if (type2 == typeof(LinkTwitchAccountResult) && _instance.OnLinkTwitchResultEvent != null)
				{
					_instance.OnLinkTwitchResultEvent((LinkTwitchAccountResult)e.Result);
				}
				else if (type2 == typeof(LinkWindowsHelloAccountResponse) && _instance.OnLinkWindowsHelloResultEvent != null)
				{
					_instance.OnLinkWindowsHelloResultEvent((LinkWindowsHelloAccountResponse)e.Result);
				}
				else if (type2 == typeof(LinkXboxAccountResult) && _instance.OnLinkXboxAccountResultEvent != null)
				{
					_instance.OnLinkXboxAccountResultEvent((LinkXboxAccountResult)e.Result);
				}
				else if (type2 == typeof(MatchmakeResult) && _instance.OnMatchmakeResultEvent != null)
				{
					_instance.OnMatchmakeResultEvent((MatchmakeResult)e.Result);
				}
				else if (type2 == typeof(OpenTradeResponse) && _instance.OnOpenTradeResultEvent != null)
				{
					_instance.OnOpenTradeResultEvent((OpenTradeResponse)e.Result);
				}
				else if (type2 == typeof(PayForPurchaseResult) && _instance.OnPayForPurchaseResultEvent != null)
				{
					_instance.OnPayForPurchaseResultEvent((PayForPurchaseResult)e.Result);
				}
				else if (type2 == typeof(PurchaseItemResult) && _instance.OnPurchaseItemResultEvent != null)
				{
					_instance.OnPurchaseItemResultEvent((PurchaseItemResult)e.Result);
				}
				else if (type2 == typeof(RedeemCouponResult) && _instance.OnRedeemCouponResultEvent != null)
				{
					_instance.OnRedeemCouponResultEvent((RedeemCouponResult)e.Result);
				}
				else if (type2 == typeof(PlayFab.ClientModels.EmptyResponse) && _instance.OnRefreshPSNAuthTokenResultEvent != null)
				{
					_instance.OnRefreshPSNAuthTokenResultEvent((PlayFab.ClientModels.EmptyResponse)e.Result);
				}
				else if (type2 == typeof(RegisterForIOSPushNotificationResult) && _instance.OnRegisterForIOSPushNotificationResultEvent != null)
				{
					_instance.OnRegisterForIOSPushNotificationResultEvent((RegisterForIOSPushNotificationResult)e.Result);
				}
				else if (type2 == typeof(RegisterPlayFabUserResult) && _instance.OnRegisterPlayFabUserResultEvent != null)
				{
					_instance.OnRegisterPlayFabUserResultEvent((RegisterPlayFabUserResult)e.Result);
				}
				else if (type2 == typeof(RemoveContactEmailResult) && _instance.OnRemoveContactEmailResultEvent != null)
				{
					_instance.OnRemoveContactEmailResultEvent((RemoveContactEmailResult)e.Result);
				}
				else if (type2 == typeof(RemoveFriendResult) && _instance.OnRemoveFriendResultEvent != null)
				{
					_instance.OnRemoveFriendResultEvent((RemoveFriendResult)e.Result);
				}
				else if (type2 == typeof(RemoveGenericIDResult) && _instance.OnRemoveGenericIDResultEvent != null)
				{
					_instance.OnRemoveGenericIDResultEvent((RemoveGenericIDResult)e.Result);
				}
				else if (type2 == typeof(RemoveSharedGroupMembersResult) && _instance.OnRemoveSharedGroupMembersResultEvent != null)
				{
					_instance.OnRemoveSharedGroupMembersResultEvent((RemoveSharedGroupMembersResult)e.Result);
				}
				else if (type2 == typeof(ReportAdActivityResult) && _instance.OnReportAdActivityResultEvent != null)
				{
					_instance.OnReportAdActivityResultEvent((ReportAdActivityResult)e.Result);
				}
				else if (type2 == typeof(PlayFab.ClientModels.EmptyResponse) && _instance.OnReportDeviceInfoResultEvent != null)
				{
					_instance.OnReportDeviceInfoResultEvent((PlayFab.ClientModels.EmptyResponse)e.Result);
				}
				else if (type2 == typeof(ReportPlayerClientResult) && _instance.OnReportPlayerResultEvent != null)
				{
					_instance.OnReportPlayerResultEvent((ReportPlayerClientResult)e.Result);
				}
				else if (type2 == typeof(RestoreIOSPurchasesResult) && _instance.OnRestoreIOSPurchasesResultEvent != null)
				{
					_instance.OnRestoreIOSPurchasesResultEvent((RestoreIOSPurchasesResult)e.Result);
				}
				else if (type2 == typeof(RewardAdActivityResult) && _instance.OnRewardAdActivityResultEvent != null)
				{
					_instance.OnRewardAdActivityResultEvent((RewardAdActivityResult)e.Result);
				}
				else if (type2 == typeof(SendAccountRecoveryEmailResult) && _instance.OnSendAccountRecoveryEmailResultEvent != null)
				{
					_instance.OnSendAccountRecoveryEmailResultEvent((SendAccountRecoveryEmailResult)e.Result);
				}
				else if (type2 == typeof(SetFriendTagsResult) && _instance.OnSetFriendTagsResultEvent != null)
				{
					_instance.OnSetFriendTagsResultEvent((SetFriendTagsResult)e.Result);
				}
				else if (type2 == typeof(SetPlayerSecretResult) && _instance.OnSetPlayerSecretResultEvent != null)
				{
					_instance.OnSetPlayerSecretResultEvent((SetPlayerSecretResult)e.Result);
				}
				else if (type2 == typeof(StartGameResult) && _instance.OnStartGameResultEvent != null)
				{
					_instance.OnStartGameResultEvent((StartGameResult)e.Result);
				}
				else if (type2 == typeof(StartPurchaseResult) && _instance.OnStartPurchaseResultEvent != null)
				{
					_instance.OnStartPurchaseResultEvent((StartPurchaseResult)e.Result);
				}
				else if (type2 == typeof(ModifyUserVirtualCurrencyResult) && _instance.OnSubtractUserVirtualCurrencyResultEvent != null)
				{
					_instance.OnSubtractUserVirtualCurrencyResultEvent((ModifyUserVirtualCurrencyResult)e.Result);
				}
				else if (type2 == typeof(UnlinkAndroidDeviceIDResult) && _instance.OnUnlinkAndroidDeviceIDResultEvent != null)
				{
					_instance.OnUnlinkAndroidDeviceIDResultEvent((UnlinkAndroidDeviceIDResult)e.Result);
				}
				else if (type2 == typeof(PlayFab.ClientModels.EmptyResponse) && _instance.OnUnlinkAppleResultEvent != null)
				{
					_instance.OnUnlinkAppleResultEvent((PlayFab.ClientModels.EmptyResponse)e.Result);
				}
				else if (type2 == typeof(UnlinkCustomIDResult) && _instance.OnUnlinkCustomIDResultEvent != null)
				{
					_instance.OnUnlinkCustomIDResultEvent((UnlinkCustomIDResult)e.Result);
				}
				else if (type2 == typeof(UnlinkFacebookAccountResult) && _instance.OnUnlinkFacebookAccountResultEvent != null)
				{
					_instance.OnUnlinkFacebookAccountResultEvent((UnlinkFacebookAccountResult)e.Result);
				}
				else if (type2 == typeof(UnlinkFacebookInstantGamesIdResult) && _instance.OnUnlinkFacebookInstantGamesIdResultEvent != null)
				{
					_instance.OnUnlinkFacebookInstantGamesIdResultEvent((UnlinkFacebookInstantGamesIdResult)e.Result);
				}
				else if (type2 == typeof(UnlinkGameCenterAccountResult) && _instance.OnUnlinkGameCenterAccountResultEvent != null)
				{
					_instance.OnUnlinkGameCenterAccountResultEvent((UnlinkGameCenterAccountResult)e.Result);
				}
				else if (type2 == typeof(UnlinkGoogleAccountResult) && _instance.OnUnlinkGoogleAccountResultEvent != null)
				{
					_instance.OnUnlinkGoogleAccountResultEvent((UnlinkGoogleAccountResult)e.Result);
				}
				else if (type2 == typeof(UnlinkIOSDeviceIDResult) && _instance.OnUnlinkIOSDeviceIDResultEvent != null)
				{
					_instance.OnUnlinkIOSDeviceIDResultEvent((UnlinkIOSDeviceIDResult)e.Result);
				}
				else if (type2 == typeof(UnlinkKongregateAccountResult) && _instance.OnUnlinkKongregateResultEvent != null)
				{
					_instance.OnUnlinkKongregateResultEvent((UnlinkKongregateAccountResult)e.Result);
				}
				else if (type2 == typeof(PlayFab.ClientModels.EmptyResponse) && _instance.OnUnlinkNintendoServiceAccountResultEvent != null)
				{
					_instance.OnUnlinkNintendoServiceAccountResultEvent((PlayFab.ClientModels.EmptyResponse)e.Result);
				}
				else if (type2 == typeof(UnlinkNintendoSwitchDeviceIdResult) && _instance.OnUnlinkNintendoSwitchDeviceIdResultEvent != null)
				{
					_instance.OnUnlinkNintendoSwitchDeviceIdResultEvent((UnlinkNintendoSwitchDeviceIdResult)e.Result);
				}
				else if (type2 == typeof(PlayFab.ClientModels.EmptyResponse) && _instance.OnUnlinkOpenIdConnectResultEvent != null)
				{
					_instance.OnUnlinkOpenIdConnectResultEvent((PlayFab.ClientModels.EmptyResponse)e.Result);
				}
				else if (type2 == typeof(UnlinkPSNAccountResult) && _instance.OnUnlinkPSNAccountResultEvent != null)
				{
					_instance.OnUnlinkPSNAccountResultEvent((UnlinkPSNAccountResult)e.Result);
				}
				else if (type2 == typeof(UnlinkSteamAccountResult) && _instance.OnUnlinkSteamAccountResultEvent != null)
				{
					_instance.OnUnlinkSteamAccountResultEvent((UnlinkSteamAccountResult)e.Result);
				}
				else if (type2 == typeof(UnlinkTwitchAccountResult) && _instance.OnUnlinkTwitchResultEvent != null)
				{
					_instance.OnUnlinkTwitchResultEvent((UnlinkTwitchAccountResult)e.Result);
				}
				else if (type2 == typeof(UnlinkWindowsHelloAccountResponse) && _instance.OnUnlinkWindowsHelloResultEvent != null)
				{
					_instance.OnUnlinkWindowsHelloResultEvent((UnlinkWindowsHelloAccountResponse)e.Result);
				}
				else if (type2 == typeof(UnlinkXboxAccountResult) && _instance.OnUnlinkXboxAccountResultEvent != null)
				{
					_instance.OnUnlinkXboxAccountResultEvent((UnlinkXboxAccountResult)e.Result);
				}
				else if (type2 == typeof(UnlockContainerItemResult) && _instance.OnUnlockContainerInstanceResultEvent != null)
				{
					_instance.OnUnlockContainerInstanceResultEvent((UnlockContainerItemResult)e.Result);
				}
				else if (type2 == typeof(UnlockContainerItemResult) && _instance.OnUnlockContainerItemResultEvent != null)
				{
					_instance.OnUnlockContainerItemResultEvent((UnlockContainerItemResult)e.Result);
				}
				else if (type2 == typeof(PlayFab.ClientModels.EmptyResponse) && _instance.OnUpdateAvatarUrlResultEvent != null)
				{
					_instance.OnUpdateAvatarUrlResultEvent((PlayFab.ClientModels.EmptyResponse)e.Result);
				}
				else if (type2 == typeof(UpdateCharacterDataResult) && _instance.OnUpdateCharacterDataResultEvent != null)
				{
					_instance.OnUpdateCharacterDataResultEvent((UpdateCharacterDataResult)e.Result);
				}
				else if (type2 == typeof(UpdateCharacterStatisticsResult) && _instance.OnUpdateCharacterStatisticsResultEvent != null)
				{
					_instance.OnUpdateCharacterStatisticsResultEvent((UpdateCharacterStatisticsResult)e.Result);
				}
				else if (type2 == typeof(UpdatePlayerStatisticsResult) && _instance.OnUpdatePlayerStatisticsResultEvent != null)
				{
					_instance.OnUpdatePlayerStatisticsResultEvent((UpdatePlayerStatisticsResult)e.Result);
				}
				else if (type2 == typeof(UpdateSharedGroupDataResult) && _instance.OnUpdateSharedGroupDataResultEvent != null)
				{
					_instance.OnUpdateSharedGroupDataResultEvent((UpdateSharedGroupDataResult)e.Result);
				}
				else if (type2 == typeof(UpdateUserDataResult) && _instance.OnUpdateUserDataResultEvent != null)
				{
					_instance.OnUpdateUserDataResultEvent((UpdateUserDataResult)e.Result);
				}
				else if (type2 == typeof(UpdateUserDataResult) && _instance.OnUpdateUserPublisherDataResultEvent != null)
				{
					_instance.OnUpdateUserPublisherDataResultEvent((UpdateUserDataResult)e.Result);
				}
				else if (type2 == typeof(UpdateUserTitleDisplayNameResult) && _instance.OnUpdateUserTitleDisplayNameResultEvent != null)
				{
					_instance.OnUpdateUserTitleDisplayNameResultEvent((UpdateUserTitleDisplayNameResult)e.Result);
				}
				else if (type2 == typeof(ValidateAmazonReceiptResult) && _instance.OnValidateAmazonIAPReceiptResultEvent != null)
				{
					_instance.OnValidateAmazonIAPReceiptResultEvent((ValidateAmazonReceiptResult)e.Result);
				}
				else if (type2 == typeof(ValidateGooglePlayPurchaseResult) && _instance.OnValidateGooglePlayPurchaseResultEvent != null)
				{
					_instance.OnValidateGooglePlayPurchaseResultEvent((ValidateGooglePlayPurchaseResult)e.Result);
				}
				else if (type2 == typeof(ValidateIOSReceiptResult) && _instance.OnValidateIOSReceiptResultEvent != null)
				{
					_instance.OnValidateIOSReceiptResultEvent((ValidateIOSReceiptResult)e.Result);
				}
				else if (type2 == typeof(ValidateWindowsReceiptResult) && _instance.OnValidateWindowsStoreReceiptResultEvent != null)
				{
					_instance.OnValidateWindowsStoreReceiptResultEvent((ValidateWindowsReceiptResult)e.Result);
				}
				else if (type2 == typeof(WriteEventResponse) && _instance.OnWriteCharacterEventResultEvent != null)
				{
					_instance.OnWriteCharacterEventResultEvent((WriteEventResponse)e.Result);
				}
				else if (type2 == typeof(WriteEventResponse) && _instance.OnWritePlayerEventResultEvent != null)
				{
					_instance.OnWritePlayerEventResultEvent((WriteEventResponse)e.Result);
				}
				else if (type2 == typeof(WriteEventResponse) && _instance.OnWriteTitleEventResultEvent != null)
				{
					_instance.OnWriteTitleEventResultEvent((WriteEventResponse)e.Result);
				}
				else if (type2 == typeof(GetEntityTokenResponse) && _instance.OnAuthenticationGetEntityTokenResultEvent != null)
				{
					_instance.OnAuthenticationGetEntityTokenResultEvent((GetEntityTokenResponse)e.Result);
				}
				else if (type2 == typeof(ValidateEntityTokenResponse) && _instance.OnAuthenticationValidateEntityTokenResultEvent != null)
				{
					_instance.OnAuthenticationValidateEntityTokenResultEvent((ValidateEntityTokenResponse)e.Result);
				}
				else if (type2 == typeof(PlayFab.CloudScriptModels.ExecuteCloudScriptResult) && _instance.OnCloudScriptExecuteEntityCloudScriptResultEvent != null)
				{
					_instance.OnCloudScriptExecuteEntityCloudScriptResultEvent((PlayFab.CloudScriptModels.ExecuteCloudScriptResult)e.Result);
				}
				else if (type2 == typeof(ExecuteFunctionResult) && _instance.OnCloudScriptExecuteFunctionResultEvent != null)
				{
					_instance.OnCloudScriptExecuteFunctionResultEvent((ExecuteFunctionResult)e.Result);
				}
				else if (type2 == typeof(ListFunctionsResult) && _instance.OnCloudScriptListFunctionsResultEvent != null)
				{
					_instance.OnCloudScriptListFunctionsResultEvent((ListFunctionsResult)e.Result);
				}
				else if (type2 == typeof(ListHttpFunctionsResult) && _instance.OnCloudScriptListHttpFunctionsResultEvent != null)
				{
					_instance.OnCloudScriptListHttpFunctionsResultEvent((ListHttpFunctionsResult)e.Result);
				}
				else if (type2 == typeof(ListQueuedFunctionsResult) && _instance.OnCloudScriptListQueuedFunctionsResultEvent != null)
				{
					_instance.OnCloudScriptListQueuedFunctionsResultEvent((ListQueuedFunctionsResult)e.Result);
				}
				else if (type2 == typeof(PlayFab.CloudScriptModels.EmptyResult) && _instance.OnCloudScriptPostFunctionResultForEntityTriggeredActionResultEvent != null)
				{
					_instance.OnCloudScriptPostFunctionResultForEntityTriggeredActionResultEvent((PlayFab.CloudScriptModels.EmptyResult)e.Result);
				}
				else if (type2 == typeof(PlayFab.CloudScriptModels.EmptyResult) && _instance.OnCloudScriptPostFunctionResultForFunctionExecutionResultEvent != null)
				{
					_instance.OnCloudScriptPostFunctionResultForFunctionExecutionResultEvent((PlayFab.CloudScriptModels.EmptyResult)e.Result);
				}
				else if (type2 == typeof(PlayFab.CloudScriptModels.EmptyResult) && _instance.OnCloudScriptPostFunctionResultForPlayerTriggeredActionResultEvent != null)
				{
					_instance.OnCloudScriptPostFunctionResultForPlayerTriggeredActionResultEvent((PlayFab.CloudScriptModels.EmptyResult)e.Result);
				}
				else if (type2 == typeof(PlayFab.CloudScriptModels.EmptyResult) && _instance.OnCloudScriptPostFunctionResultForScheduledTaskResultEvent != null)
				{
					_instance.OnCloudScriptPostFunctionResultForScheduledTaskResultEvent((PlayFab.CloudScriptModels.EmptyResult)e.Result);
				}
				else if (type2 == typeof(PlayFab.CloudScriptModels.EmptyResult) && _instance.OnCloudScriptRegisterHttpFunctionResultEvent != null)
				{
					_instance.OnCloudScriptRegisterHttpFunctionResultEvent((PlayFab.CloudScriptModels.EmptyResult)e.Result);
				}
				else if (type2 == typeof(PlayFab.CloudScriptModels.EmptyResult) && _instance.OnCloudScriptRegisterQueuedFunctionResultEvent != null)
				{
					_instance.OnCloudScriptRegisterQueuedFunctionResultEvent((PlayFab.CloudScriptModels.EmptyResult)e.Result);
				}
				else if (type2 == typeof(PlayFab.CloudScriptModels.EmptyResult) && _instance.OnCloudScriptUnregisterFunctionResultEvent != null)
				{
					_instance.OnCloudScriptUnregisterFunctionResultEvent((PlayFab.CloudScriptModels.EmptyResult)e.Result);
				}
				else if (type2 == typeof(AbortFileUploadsResponse) && _instance.OnDataAbortFileUploadsResultEvent != null)
				{
					_instance.OnDataAbortFileUploadsResultEvent((AbortFileUploadsResponse)e.Result);
				}
				else if (type2 == typeof(DeleteFilesResponse) && _instance.OnDataDeleteFilesResultEvent != null)
				{
					_instance.OnDataDeleteFilesResultEvent((DeleteFilesResponse)e.Result);
				}
				else if (type2 == typeof(FinalizeFileUploadsResponse) && _instance.OnDataFinalizeFileUploadsResultEvent != null)
				{
					_instance.OnDataFinalizeFileUploadsResultEvent((FinalizeFileUploadsResponse)e.Result);
				}
				else if (type2 == typeof(GetFilesResponse) && _instance.OnDataGetFilesResultEvent != null)
				{
					_instance.OnDataGetFilesResultEvent((GetFilesResponse)e.Result);
				}
				else if (type2 == typeof(GetObjectsResponse) && _instance.OnDataGetObjectsResultEvent != null)
				{
					_instance.OnDataGetObjectsResultEvent((GetObjectsResponse)e.Result);
				}
				else if (type2 == typeof(InitiateFileUploadsResponse) && _instance.OnDataInitiateFileUploadsResultEvent != null)
				{
					_instance.OnDataInitiateFileUploadsResultEvent((InitiateFileUploadsResponse)e.Result);
				}
				else if (type2 == typeof(SetObjectsResponse) && _instance.OnDataSetObjectsResultEvent != null)
				{
					_instance.OnDataSetObjectsResultEvent((SetObjectsResponse)e.Result);
				}
				else if (type2 == typeof(WriteEventsResponse) && _instance.OnEventsWriteEventsResultEvent != null)
				{
					_instance.OnEventsWriteEventsResultEvent((WriteEventsResponse)e.Result);
				}
				else if (type2 == typeof(WriteEventsResponse) && _instance.OnEventsWriteTelemetryEventsResultEvent != null)
				{
					_instance.OnEventsWriteTelemetryEventsResultEvent((WriteEventsResponse)e.Result);
				}
				else if (type2 == typeof(CreateExclusionGroupResult) && _instance.OnExperimentationCreateExclusionGroupResultEvent != null)
				{
					_instance.OnExperimentationCreateExclusionGroupResultEvent((CreateExclusionGroupResult)e.Result);
				}
				else if (type2 == typeof(CreateExperimentResult) && _instance.OnExperimentationCreateExperimentResultEvent != null)
				{
					_instance.OnExperimentationCreateExperimentResultEvent((CreateExperimentResult)e.Result);
				}
				else if (type2 == typeof(PlayFab.ExperimentationModels.EmptyResponse) && _instance.OnExperimentationDeleteExclusionGroupResultEvent != null)
				{
					_instance.OnExperimentationDeleteExclusionGroupResultEvent((PlayFab.ExperimentationModels.EmptyResponse)e.Result);
				}
				else if (type2 == typeof(PlayFab.ExperimentationModels.EmptyResponse) && _instance.OnExperimentationDeleteExperimentResultEvent != null)
				{
					_instance.OnExperimentationDeleteExperimentResultEvent((PlayFab.ExperimentationModels.EmptyResponse)e.Result);
				}
				else if (type2 == typeof(GetExclusionGroupsResult) && _instance.OnExperimentationGetExclusionGroupsResultEvent != null)
				{
					_instance.OnExperimentationGetExclusionGroupsResultEvent((GetExclusionGroupsResult)e.Result);
				}
				else if (type2 == typeof(GetExclusionGroupTrafficResult) && _instance.OnExperimentationGetExclusionGroupTrafficResultEvent != null)
				{
					_instance.OnExperimentationGetExclusionGroupTrafficResultEvent((GetExclusionGroupTrafficResult)e.Result);
				}
				else if (type2 == typeof(GetExperimentsResult) && _instance.OnExperimentationGetExperimentsResultEvent != null)
				{
					_instance.OnExperimentationGetExperimentsResultEvent((GetExperimentsResult)e.Result);
				}
				else if (type2 == typeof(GetLatestScorecardResult) && _instance.OnExperimentationGetLatestScorecardResultEvent != null)
				{
					_instance.OnExperimentationGetLatestScorecardResultEvent((GetLatestScorecardResult)e.Result);
				}
				else if (type2 == typeof(GetTreatmentAssignmentResult) && _instance.OnExperimentationGetTreatmentAssignmentResultEvent != null)
				{
					_instance.OnExperimentationGetTreatmentAssignmentResultEvent((GetTreatmentAssignmentResult)e.Result);
				}
				else if (type2 == typeof(PlayFab.ExperimentationModels.EmptyResponse) && _instance.OnExperimentationStartExperimentResultEvent != null)
				{
					_instance.OnExperimentationStartExperimentResultEvent((PlayFab.ExperimentationModels.EmptyResponse)e.Result);
				}
				else if (type2 == typeof(PlayFab.ExperimentationModels.EmptyResponse) && _instance.OnExperimentationStopExperimentResultEvent != null)
				{
					_instance.OnExperimentationStopExperimentResultEvent((PlayFab.ExperimentationModels.EmptyResponse)e.Result);
				}
				else if (type2 == typeof(PlayFab.ExperimentationModels.EmptyResponse) && _instance.OnExperimentationUpdateExclusionGroupResultEvent != null)
				{
					_instance.OnExperimentationUpdateExclusionGroupResultEvent((PlayFab.ExperimentationModels.EmptyResponse)e.Result);
				}
				else if (type2 == typeof(PlayFab.ExperimentationModels.EmptyResponse) && _instance.OnExperimentationUpdateExperimentResultEvent != null)
				{
					_instance.OnExperimentationUpdateExperimentResultEvent((PlayFab.ExperimentationModels.EmptyResponse)e.Result);
				}
				else if (type2 == typeof(InsightsGetDetailsResponse) && _instance.OnInsightsGetDetailsResultEvent != null)
				{
					_instance.OnInsightsGetDetailsResultEvent((InsightsGetDetailsResponse)e.Result);
				}
				else if (type2 == typeof(InsightsGetLimitsResponse) && _instance.OnInsightsGetLimitsResultEvent != null)
				{
					_instance.OnInsightsGetLimitsResultEvent((InsightsGetLimitsResponse)e.Result);
				}
				else if (type2 == typeof(InsightsGetOperationStatusResponse) && _instance.OnInsightsGetOperationStatusResultEvent != null)
				{
					_instance.OnInsightsGetOperationStatusResultEvent((InsightsGetOperationStatusResponse)e.Result);
				}
				else if (type2 == typeof(InsightsGetPendingOperationsResponse) && _instance.OnInsightsGetPendingOperationsResultEvent != null)
				{
					_instance.OnInsightsGetPendingOperationsResultEvent((InsightsGetPendingOperationsResponse)e.Result);
				}
				else if (type2 == typeof(InsightsOperationResponse) && _instance.OnInsightsSetPerformanceResultEvent != null)
				{
					_instance.OnInsightsSetPerformanceResultEvent((InsightsOperationResponse)e.Result);
				}
				else if (type2 == typeof(InsightsOperationResponse) && _instance.OnInsightsSetStorageRetentionResultEvent != null)
				{
					_instance.OnInsightsSetStorageRetentionResultEvent((InsightsOperationResponse)e.Result);
				}
				else if (type2 == typeof(PlayFab.GroupsModels.EmptyResponse) && _instance.OnGroupsAcceptGroupApplicationResultEvent != null)
				{
					_instance.OnGroupsAcceptGroupApplicationResultEvent((PlayFab.GroupsModels.EmptyResponse)e.Result);
				}
				else if (type2 == typeof(PlayFab.GroupsModels.EmptyResponse) && _instance.OnGroupsAcceptGroupInvitationResultEvent != null)
				{
					_instance.OnGroupsAcceptGroupInvitationResultEvent((PlayFab.GroupsModels.EmptyResponse)e.Result);
				}
				else if (type2 == typeof(PlayFab.GroupsModels.EmptyResponse) && _instance.OnGroupsAddMembersResultEvent != null)
				{
					_instance.OnGroupsAddMembersResultEvent((PlayFab.GroupsModels.EmptyResponse)e.Result);
				}
				else if (type2 == typeof(ApplyToGroupResponse) && _instance.OnGroupsApplyToGroupResultEvent != null)
				{
					_instance.OnGroupsApplyToGroupResultEvent((ApplyToGroupResponse)e.Result);
				}
				else if (type2 == typeof(PlayFab.GroupsModels.EmptyResponse) && _instance.OnGroupsBlockEntityResultEvent != null)
				{
					_instance.OnGroupsBlockEntityResultEvent((PlayFab.GroupsModels.EmptyResponse)e.Result);
				}
				else if (type2 == typeof(PlayFab.GroupsModels.EmptyResponse) && _instance.OnGroupsChangeMemberRoleResultEvent != null)
				{
					_instance.OnGroupsChangeMemberRoleResultEvent((PlayFab.GroupsModels.EmptyResponse)e.Result);
				}
				else if (type2 == typeof(CreateGroupResponse) && _instance.OnGroupsCreateGroupResultEvent != null)
				{
					_instance.OnGroupsCreateGroupResultEvent((CreateGroupResponse)e.Result);
				}
				else if (type2 == typeof(CreateGroupRoleResponse) && _instance.OnGroupsCreateRoleResultEvent != null)
				{
					_instance.OnGroupsCreateRoleResultEvent((CreateGroupRoleResponse)e.Result);
				}
				else if (type2 == typeof(PlayFab.GroupsModels.EmptyResponse) && _instance.OnGroupsDeleteGroupResultEvent != null)
				{
					_instance.OnGroupsDeleteGroupResultEvent((PlayFab.GroupsModels.EmptyResponse)e.Result);
				}
				else if (type2 == typeof(PlayFab.GroupsModels.EmptyResponse) && _instance.OnGroupsDeleteRoleResultEvent != null)
				{
					_instance.OnGroupsDeleteRoleResultEvent((PlayFab.GroupsModels.EmptyResponse)e.Result);
				}
				else if (type2 == typeof(GetGroupResponse) && _instance.OnGroupsGetGroupResultEvent != null)
				{
					_instance.OnGroupsGetGroupResultEvent((GetGroupResponse)e.Result);
				}
				else if (type2 == typeof(InviteToGroupResponse) && _instance.OnGroupsInviteToGroupResultEvent != null)
				{
					_instance.OnGroupsInviteToGroupResultEvent((InviteToGroupResponse)e.Result);
				}
				else if (type2 == typeof(IsMemberResponse) && _instance.OnGroupsIsMemberResultEvent != null)
				{
					_instance.OnGroupsIsMemberResultEvent((IsMemberResponse)e.Result);
				}
				else if (type2 == typeof(ListGroupApplicationsResponse) && _instance.OnGroupsListGroupApplicationsResultEvent != null)
				{
					_instance.OnGroupsListGroupApplicationsResultEvent((ListGroupApplicationsResponse)e.Result);
				}
				else if (type2 == typeof(ListGroupBlocksResponse) && _instance.OnGroupsListGroupBlocksResultEvent != null)
				{
					_instance.OnGroupsListGroupBlocksResultEvent((ListGroupBlocksResponse)e.Result);
				}
				else if (type2 == typeof(ListGroupInvitationsResponse) && _instance.OnGroupsListGroupInvitationsResultEvent != null)
				{
					_instance.OnGroupsListGroupInvitationsResultEvent((ListGroupInvitationsResponse)e.Result);
				}
				else if (type2 == typeof(ListGroupMembersResponse) && _instance.OnGroupsListGroupMembersResultEvent != null)
				{
					_instance.OnGroupsListGroupMembersResultEvent((ListGroupMembersResponse)e.Result);
				}
				else if (type2 == typeof(ListMembershipResponse) && _instance.OnGroupsListMembershipResultEvent != null)
				{
					_instance.OnGroupsListMembershipResultEvent((ListMembershipResponse)e.Result);
				}
				else if (type2 == typeof(ListMembershipOpportunitiesResponse) && _instance.OnGroupsListMembershipOpportunitiesResultEvent != null)
				{
					_instance.OnGroupsListMembershipOpportunitiesResultEvent((ListMembershipOpportunitiesResponse)e.Result);
				}
				else if (type2 == typeof(PlayFab.GroupsModels.EmptyResponse) && _instance.OnGroupsRemoveGroupApplicationResultEvent != null)
				{
					_instance.OnGroupsRemoveGroupApplicationResultEvent((PlayFab.GroupsModels.EmptyResponse)e.Result);
				}
				else if (type2 == typeof(PlayFab.GroupsModels.EmptyResponse) && _instance.OnGroupsRemoveGroupInvitationResultEvent != null)
				{
					_instance.OnGroupsRemoveGroupInvitationResultEvent((PlayFab.GroupsModels.EmptyResponse)e.Result);
				}
				else if (type2 == typeof(PlayFab.GroupsModels.EmptyResponse) && _instance.OnGroupsRemoveMembersResultEvent != null)
				{
					_instance.OnGroupsRemoveMembersResultEvent((PlayFab.GroupsModels.EmptyResponse)e.Result);
				}
				else if (type2 == typeof(PlayFab.GroupsModels.EmptyResponse) && _instance.OnGroupsUnblockEntityResultEvent != null)
				{
					_instance.OnGroupsUnblockEntityResultEvent((PlayFab.GroupsModels.EmptyResponse)e.Result);
				}
				else if (type2 == typeof(UpdateGroupResponse) && _instance.OnGroupsUpdateGroupResultEvent != null)
				{
					_instance.OnGroupsUpdateGroupResultEvent((UpdateGroupResponse)e.Result);
				}
				else if (type2 == typeof(UpdateGroupRoleResponse) && _instance.OnGroupsUpdateRoleResultEvent != null)
				{
					_instance.OnGroupsUpdateRoleResultEvent((UpdateGroupRoleResponse)e.Result);
				}
				else if (type2 == typeof(GetLanguageListResponse) && _instance.OnLocalizationGetLanguageListResultEvent != null)
				{
					_instance.OnLocalizationGetLanguageListResultEvent((GetLanguageListResponse)e.Result);
				}
				else if (type2 == typeof(CancelAllMatchmakingTicketsForPlayerResult) && _instance.OnMultiplayerCancelAllMatchmakingTicketsForPlayerResultEvent != null)
				{
					_instance.OnMultiplayerCancelAllMatchmakingTicketsForPlayerResultEvent((CancelAllMatchmakingTicketsForPlayerResult)e.Result);
				}
				else if (type2 == typeof(CancelAllServerBackfillTicketsForPlayerResult) && _instance.OnMultiplayerCancelAllServerBackfillTicketsForPlayerResultEvent != null)
				{
					_instance.OnMultiplayerCancelAllServerBackfillTicketsForPlayerResultEvent((CancelAllServerBackfillTicketsForPlayerResult)e.Result);
				}
				else if (type2 == typeof(CancelMatchmakingTicketResult) && _instance.OnMultiplayerCancelMatchmakingTicketResultEvent != null)
				{
					_instance.OnMultiplayerCancelMatchmakingTicketResultEvent((CancelMatchmakingTicketResult)e.Result);
				}
				else if (type2 == typeof(CancelServerBackfillTicketResult) && _instance.OnMultiplayerCancelServerBackfillTicketResultEvent != null)
				{
					_instance.OnMultiplayerCancelServerBackfillTicketResultEvent((CancelServerBackfillTicketResult)e.Result);
				}
				else if (type2 == typeof(BuildAliasDetailsResponse) && _instance.OnMultiplayerCreateBuildAliasResultEvent != null)
				{
					_instance.OnMultiplayerCreateBuildAliasResultEvent((BuildAliasDetailsResponse)e.Result);
				}
				else if (type2 == typeof(CreateBuildWithCustomContainerResponse) && _instance.OnMultiplayerCreateBuildWithCustomContainerResultEvent != null)
				{
					_instance.OnMultiplayerCreateBuildWithCustomContainerResultEvent((CreateBuildWithCustomContainerResponse)e.Result);
				}
				else if (type2 == typeof(CreateBuildWithManagedContainerResponse) && _instance.OnMultiplayerCreateBuildWithManagedContainerResultEvent != null)
				{
					_instance.OnMultiplayerCreateBuildWithManagedContainerResultEvent((CreateBuildWithManagedContainerResponse)e.Result);
				}
				else if (type2 == typeof(CreateBuildWithProcessBasedServerResponse) && _instance.OnMultiplayerCreateBuildWithProcessBasedServerResultEvent != null)
				{
					_instance.OnMultiplayerCreateBuildWithProcessBasedServerResultEvent((CreateBuildWithProcessBasedServerResponse)e.Result);
				}
				else if (type2 == typeof(CreateMatchmakingTicketResult) && _instance.OnMultiplayerCreateMatchmakingTicketResultEvent != null)
				{
					_instance.OnMultiplayerCreateMatchmakingTicketResultEvent((CreateMatchmakingTicketResult)e.Result);
				}
				else if (type2 == typeof(CreateRemoteUserResponse) && _instance.OnMultiplayerCreateRemoteUserResultEvent != null)
				{
					_instance.OnMultiplayerCreateRemoteUserResultEvent((CreateRemoteUserResponse)e.Result);
				}
				else if (type2 == typeof(CreateServerBackfillTicketResult) && _instance.OnMultiplayerCreateServerBackfillTicketResultEvent != null)
				{
					_instance.OnMultiplayerCreateServerBackfillTicketResultEvent((CreateServerBackfillTicketResult)e.Result);
				}
				else if (type2 == typeof(CreateMatchmakingTicketResult) && _instance.OnMultiplayerCreateServerMatchmakingTicketResultEvent != null)
				{
					_instance.OnMultiplayerCreateServerMatchmakingTicketResultEvent((CreateMatchmakingTicketResult)e.Result);
				}
				else if (type2 == typeof(PlayFab.MultiplayerModels.EmptyResponse) && _instance.OnMultiplayerDeleteAssetResultEvent != null)
				{
					_instance.OnMultiplayerDeleteAssetResultEvent((PlayFab.MultiplayerModels.EmptyResponse)e.Result);
				}
				else if (type2 == typeof(PlayFab.MultiplayerModels.EmptyResponse) && _instance.OnMultiplayerDeleteBuildResultEvent != null)
				{
					_instance.OnMultiplayerDeleteBuildResultEvent((PlayFab.MultiplayerModels.EmptyResponse)e.Result);
				}
				else if (type2 == typeof(PlayFab.MultiplayerModels.EmptyResponse) && _instance.OnMultiplayerDeleteBuildAliasResultEvent != null)
				{
					_instance.OnMultiplayerDeleteBuildAliasResultEvent((PlayFab.MultiplayerModels.EmptyResponse)e.Result);
				}
				else if (type2 == typeof(PlayFab.MultiplayerModels.EmptyResponse) && _instance.OnMultiplayerDeleteBuildRegionResultEvent != null)
				{
					_instance.OnMultiplayerDeleteBuildRegionResultEvent((PlayFab.MultiplayerModels.EmptyResponse)e.Result);
				}
				else if (type2 == typeof(PlayFab.MultiplayerModels.EmptyResponse) && _instance.OnMultiplayerDeleteCertificateResultEvent != null)
				{
					_instance.OnMultiplayerDeleteCertificateResultEvent((PlayFab.MultiplayerModels.EmptyResponse)e.Result);
				}
				else if (type2 == typeof(PlayFab.MultiplayerModels.EmptyResponse) && _instance.OnMultiplayerDeleteContainerImageRepositoryResultEvent != null)
				{
					_instance.OnMultiplayerDeleteContainerImageRepositoryResultEvent((PlayFab.MultiplayerModels.EmptyResponse)e.Result);
				}
				else if (type2 == typeof(PlayFab.MultiplayerModels.EmptyResponse) && _instance.OnMultiplayerDeleteRemoteUserResultEvent != null)
				{
					_instance.OnMultiplayerDeleteRemoteUserResultEvent((PlayFab.MultiplayerModels.EmptyResponse)e.Result);
				}
				else if (type2 == typeof(EnableMultiplayerServersForTitleResponse) && _instance.OnMultiplayerEnableMultiplayerServersForTitleResultEvent != null)
				{
					_instance.OnMultiplayerEnableMultiplayerServersForTitleResultEvent((EnableMultiplayerServersForTitleResponse)e.Result);
				}
				else if (type2 == typeof(GetAssetUploadUrlResponse) && _instance.OnMultiplayerGetAssetUploadUrlResultEvent != null)
				{
					_instance.OnMultiplayerGetAssetUploadUrlResultEvent((GetAssetUploadUrlResponse)e.Result);
				}
				else if (type2 == typeof(GetBuildResponse) && _instance.OnMultiplayerGetBuildResultEvent != null)
				{
					_instance.OnMultiplayerGetBuildResultEvent((GetBuildResponse)e.Result);
				}
				else if (type2 == typeof(BuildAliasDetailsResponse) && _instance.OnMultiplayerGetBuildAliasResultEvent != null)
				{
					_instance.OnMultiplayerGetBuildAliasResultEvent((BuildAliasDetailsResponse)e.Result);
				}
				else if (type2 == typeof(GetContainerRegistryCredentialsResponse) && _instance.OnMultiplayerGetContainerRegistryCredentialsResultEvent != null)
				{
					_instance.OnMultiplayerGetContainerRegistryCredentialsResultEvent((GetContainerRegistryCredentialsResponse)e.Result);
				}
				else if (type2 == typeof(GetMatchResult) && _instance.OnMultiplayerGetMatchResultEvent != null)
				{
					_instance.OnMultiplayerGetMatchResultEvent((GetMatchResult)e.Result);
				}
				else if (type2 == typeof(GetMatchmakingQueueResult) && _instance.OnMultiplayerGetMatchmakingQueueResultEvent != null)
				{
					_instance.OnMultiplayerGetMatchmakingQueueResultEvent((GetMatchmakingQueueResult)e.Result);
				}
				else if (type2 == typeof(GetMatchmakingTicketResult) && _instance.OnMultiplayerGetMatchmakingTicketResultEvent != null)
				{
					_instance.OnMultiplayerGetMatchmakingTicketResultEvent((GetMatchmakingTicketResult)e.Result);
				}
				else if (type2 == typeof(GetMultiplayerServerDetailsResponse) && _instance.OnMultiplayerGetMultiplayerServerDetailsResultEvent != null)
				{
					_instance.OnMultiplayerGetMultiplayerServerDetailsResultEvent((GetMultiplayerServerDetailsResponse)e.Result);
				}
				else if (type2 == typeof(GetMultiplayerServerLogsResponse) && _instance.OnMultiplayerGetMultiplayerServerLogsResultEvent != null)
				{
					_instance.OnMultiplayerGetMultiplayerServerLogsResultEvent((GetMultiplayerServerLogsResponse)e.Result);
				}
				else if (type2 == typeof(GetMultiplayerServerLogsResponse) && _instance.OnMultiplayerGetMultiplayerSessionLogsBySessionIdResultEvent != null)
				{
					_instance.OnMultiplayerGetMultiplayerSessionLogsBySessionIdResultEvent((GetMultiplayerServerLogsResponse)e.Result);
				}
				else if (type2 == typeof(GetQueueStatisticsResult) && _instance.OnMultiplayerGetQueueStatisticsResultEvent != null)
				{
					_instance.OnMultiplayerGetQueueStatisticsResultEvent((GetQueueStatisticsResult)e.Result);
				}
				else if (type2 == typeof(GetRemoteLoginEndpointResponse) && _instance.OnMultiplayerGetRemoteLoginEndpointResultEvent != null)
				{
					_instance.OnMultiplayerGetRemoteLoginEndpointResultEvent((GetRemoteLoginEndpointResponse)e.Result);
				}
				else if (type2 == typeof(GetServerBackfillTicketResult) && _instance.OnMultiplayerGetServerBackfillTicketResultEvent != null)
				{
					_instance.OnMultiplayerGetServerBackfillTicketResultEvent((GetServerBackfillTicketResult)e.Result);
				}
				else if (type2 == typeof(GetTitleEnabledForMultiplayerServersStatusResponse) && _instance.OnMultiplayerGetTitleEnabledForMultiplayerServersStatusResultEvent != null)
				{
					_instance.OnMultiplayerGetTitleEnabledForMultiplayerServersStatusResultEvent((GetTitleEnabledForMultiplayerServersStatusResponse)e.Result);
				}
				else if (type2 == typeof(GetTitleMultiplayerServersQuotasResponse) && _instance.OnMultiplayerGetTitleMultiplayerServersQuotasResultEvent != null)
				{
					_instance.OnMultiplayerGetTitleMultiplayerServersQuotasResultEvent((GetTitleMultiplayerServersQuotasResponse)e.Result);
				}
				else if (type2 == typeof(JoinMatchmakingTicketResult) && _instance.OnMultiplayerJoinMatchmakingTicketResultEvent != null)
				{
					_instance.OnMultiplayerJoinMatchmakingTicketResultEvent((JoinMatchmakingTicketResult)e.Result);
				}
				else if (type2 == typeof(ListMultiplayerServersResponse) && _instance.OnMultiplayerListArchivedMultiplayerServersResultEvent != null)
				{
					_instance.OnMultiplayerListArchivedMultiplayerServersResultEvent((ListMultiplayerServersResponse)e.Result);
				}
				else if (type2 == typeof(ListAssetSummariesResponse) && _instance.OnMultiplayerListAssetSummariesResultEvent != null)
				{
					_instance.OnMultiplayerListAssetSummariesResultEvent((ListAssetSummariesResponse)e.Result);
				}
				else if (type2 == typeof(ListBuildAliasesForTitleResponse) && _instance.OnMultiplayerListBuildAliasesResultEvent != null)
				{
					_instance.OnMultiplayerListBuildAliasesResultEvent((ListBuildAliasesForTitleResponse)e.Result);
				}
				else if (type2 == typeof(ListBuildSummariesResponse) && _instance.OnMultiplayerListBuildSummariesResultEvent != null)
				{
					_instance.OnMultiplayerListBuildSummariesResultEvent((ListBuildSummariesResponse)e.Result);
				}
				else if (type2 == typeof(ListCertificateSummariesResponse) && _instance.OnMultiplayerListCertificateSummariesResultEvent != null)
				{
					_instance.OnMultiplayerListCertificateSummariesResultEvent((ListCertificateSummariesResponse)e.Result);
				}
				else if (type2 == typeof(ListContainerImagesResponse) && _instance.OnMultiplayerListContainerImagesResultEvent != null)
				{
					_instance.OnMultiplayerListContainerImagesResultEvent((ListContainerImagesResponse)e.Result);
				}
				else if (type2 == typeof(ListContainerImageTagsResponse) && _instance.OnMultiplayerListContainerImageTagsResultEvent != null)
				{
					_instance.OnMultiplayerListContainerImageTagsResultEvent((ListContainerImageTagsResponse)e.Result);
				}
				else if (type2 == typeof(ListMatchmakingQueuesResult) && _instance.OnMultiplayerListMatchmakingQueuesResultEvent != null)
				{
					_instance.OnMultiplayerListMatchmakingQueuesResultEvent((ListMatchmakingQueuesResult)e.Result);
				}
				else if (type2 == typeof(ListMatchmakingTicketsForPlayerResult) && _instance.OnMultiplayerListMatchmakingTicketsForPlayerResultEvent != null)
				{
					_instance.OnMultiplayerListMatchmakingTicketsForPlayerResultEvent((ListMatchmakingTicketsForPlayerResult)e.Result);
				}
				else if (type2 == typeof(ListMultiplayerServersResponse) && _instance.OnMultiplayerListMultiplayerServersResultEvent != null)
				{
					_instance.OnMultiplayerListMultiplayerServersResultEvent((ListMultiplayerServersResponse)e.Result);
				}
				else if (type2 == typeof(ListPartyQosServersResponse) && _instance.OnMultiplayerListPartyQosServersResultEvent != null)
				{
					_instance.OnMultiplayerListPartyQosServersResultEvent((ListPartyQosServersResponse)e.Result);
				}
				else if (type2 == typeof(ListQosServersForTitleResponse) && _instance.OnMultiplayerListQosServersForTitleResultEvent != null)
				{
					_instance.OnMultiplayerListQosServersForTitleResultEvent((ListQosServersForTitleResponse)e.Result);
				}
				else if (type2 == typeof(ListServerBackfillTicketsForPlayerResult) && _instance.OnMultiplayerListServerBackfillTicketsForPlayerResultEvent != null)
				{
					_instance.OnMultiplayerListServerBackfillTicketsForPlayerResultEvent((ListServerBackfillTicketsForPlayerResult)e.Result);
				}
				else if (type2 == typeof(ListVirtualMachineSummariesResponse) && _instance.OnMultiplayerListVirtualMachineSummariesResultEvent != null)
				{
					_instance.OnMultiplayerListVirtualMachineSummariesResultEvent((ListVirtualMachineSummariesResponse)e.Result);
				}
				else if (type2 == typeof(RemoveMatchmakingQueueResult) && _instance.OnMultiplayerRemoveMatchmakingQueueResultEvent != null)
				{
					_instance.OnMultiplayerRemoveMatchmakingQueueResultEvent((RemoveMatchmakingQueueResult)e.Result);
				}
				else if (type2 == typeof(RequestMultiplayerServerResponse) && _instance.OnMultiplayerRequestMultiplayerServerResultEvent != null)
				{
					_instance.OnMultiplayerRequestMultiplayerServerResultEvent((RequestMultiplayerServerResponse)e.Result);
				}
				else if (type2 == typeof(RolloverContainerRegistryCredentialsResponse) && _instance.OnMultiplayerRolloverContainerRegistryCredentialsResultEvent != null)
				{
					_instance.OnMultiplayerRolloverContainerRegistryCredentialsResultEvent((RolloverContainerRegistryCredentialsResponse)e.Result);
				}
				else if (type2 == typeof(SetMatchmakingQueueResult) && _instance.OnMultiplayerSetMatchmakingQueueResultEvent != null)
				{
					_instance.OnMultiplayerSetMatchmakingQueueResultEvent((SetMatchmakingQueueResult)e.Result);
				}
				else if (type2 == typeof(PlayFab.MultiplayerModels.EmptyResponse) && _instance.OnMultiplayerShutdownMultiplayerServerResultEvent != null)
				{
					_instance.OnMultiplayerShutdownMultiplayerServerResultEvent((PlayFab.MultiplayerModels.EmptyResponse)e.Result);
				}
				else if (type2 == typeof(PlayFab.MultiplayerModels.EmptyResponse) && _instance.OnMultiplayerUntagContainerImageResultEvent != null)
				{
					_instance.OnMultiplayerUntagContainerImageResultEvent((PlayFab.MultiplayerModels.EmptyResponse)e.Result);
				}
				else if (type2 == typeof(BuildAliasDetailsResponse) && _instance.OnMultiplayerUpdateBuildAliasResultEvent != null)
				{
					_instance.OnMultiplayerUpdateBuildAliasResultEvent((BuildAliasDetailsResponse)e.Result);
				}
				else if (type2 == typeof(PlayFab.MultiplayerModels.EmptyResponse) && _instance.OnMultiplayerUpdateBuildRegionResultEvent != null)
				{
					_instance.OnMultiplayerUpdateBuildRegionResultEvent((PlayFab.MultiplayerModels.EmptyResponse)e.Result);
				}
				else if (type2 == typeof(PlayFab.MultiplayerModels.EmptyResponse) && _instance.OnMultiplayerUpdateBuildRegionsResultEvent != null)
				{
					_instance.OnMultiplayerUpdateBuildRegionsResultEvent((PlayFab.MultiplayerModels.EmptyResponse)e.Result);
				}
				else if (type2 == typeof(PlayFab.MultiplayerModels.EmptyResponse) && _instance.OnMultiplayerUploadCertificateResultEvent != null)
				{
					_instance.OnMultiplayerUploadCertificateResultEvent((PlayFab.MultiplayerModels.EmptyResponse)e.Result);
				}
				else if (type2 == typeof(GetGlobalPolicyResponse) && _instance.OnProfilesGetGlobalPolicyResultEvent != null)
				{
					_instance.OnProfilesGetGlobalPolicyResultEvent((GetGlobalPolicyResponse)e.Result);
				}
				else if (type2 == typeof(GetEntityProfileResponse) && _instance.OnProfilesGetProfileResultEvent != null)
				{
					_instance.OnProfilesGetProfileResultEvent((GetEntityProfileResponse)e.Result);
				}
				else if (type2 == typeof(GetEntityProfilesResponse) && _instance.OnProfilesGetProfilesResultEvent != null)
				{
					_instance.OnProfilesGetProfilesResultEvent((GetEntityProfilesResponse)e.Result);
				}
				else if (type2 == typeof(GetTitlePlayersFromMasterPlayerAccountIdsResponse) && _instance.OnProfilesGetTitlePlayersFromMasterPlayerAccountIdsResultEvent != null)
				{
					_instance.OnProfilesGetTitlePlayersFromMasterPlayerAccountIdsResultEvent((GetTitlePlayersFromMasterPlayerAccountIdsResponse)e.Result);
				}
				else if (type2 == typeof(SetGlobalPolicyResponse) && _instance.OnProfilesSetGlobalPolicyResultEvent != null)
				{
					_instance.OnProfilesSetGlobalPolicyResultEvent((SetGlobalPolicyResponse)e.Result);
				}
				else if (type2 == typeof(SetProfileLanguageResponse) && _instance.OnProfilesSetProfileLanguageResultEvent != null)
				{
					_instance.OnProfilesSetProfileLanguageResultEvent((SetProfileLanguageResponse)e.Result);
				}
				else if (type2 == typeof(SetEntityProfilePolicyResponse) && _instance.OnProfilesSetProfilePolicyResultEvent != null)
				{
					_instance.OnProfilesSetProfilePolicyResultEvent((SetEntityProfilePolicyResponse)e.Result);
				}
			}
		}
	}
}
