using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Newtonsoft.Json.Linq;

namespace CloudinaryDotNet
{
	public class Cloudinary
	{
		private class UploadPresetApiParams
		{
			public string Url { get; private set; }

			public UploadPresetParams ParamsCopy { get; private set; }

			public HttpMethod HttpMethod { get; private set; }

			public UploadPresetApiParams(HttpMethod httpMethod, string url, UploadPresetParams paramsCopy)
			{
				Url = url;
				ParamsCopy = paramsCopy;
				HttpMethod = httpMethod;
			}
		}

		internal class UploadLargeParams
		{
			public int BufferSize { get; }

			public string Url { get; }

			public BasicRawUploadParams Parameters { get; }

			public Dictionary<string, string> Headers { get; } = new Dictionary<string, string> { ["X-Unique-Upload-Id"] = RandomPublicId() };

			public UploadLargeParams(BasicRawUploadParams parameters, int bufferSize, Api api)
			{
				parameters.File.Reset(bufferSize);
				Parameters = parameters;
				Url = GetUploadUrl(parameters, api);
				BufferSize = bufferSize;
			}

			private static string RandomPublicId()
			{
				byte[] array = new byte[8];
				new Random().NextBytes(array);
				return string.Concat(array.Select((byte x) => x.ToString("X2", CultureInfo.InvariantCulture)).ToArray());
			}

			private static string GetUploadUrl(BasicRawUploadParams parameters, Api mApi)
			{
				Url apiUrlImgUpV = mApi.ApiUrlImgUpV;
				string name = Enum.GetName(typeof(ResourceType), parameters.ResourceType);
				if (name != null)
				{
					apiUrlImgUpV.ResourceType(name.ToLowerInvariant());
				}
				return apiUrlImgUpV.BuildUrl();
			}
		}

		protected Api m_api;

		protected const string RESOURCE_TYPE_IMAGE = "image";

		protected const string ACTION_GENERATE_ARCHIVE = "generate_archive";

		protected const int DEFAULT_CHUNK_SIZE = 20971520;

		public Api Api => m_api;

		public Search Search()
		{
			return new Search(m_api);
		}

		public Task<ListResourceTypesResult> ListResourceTypesAsync(CancellationToken? cancellationToken = null)
		{
			return CallAdminApiAsync<ListResourceTypesResult>(HttpMethod.GET, GetResourcesUrl().BuildUrl(), null, cancellationToken);
		}

		public ListResourceTypesResult ListResourceTypes()
		{
			return ListResourceTypesAsync().GetAwaiter().GetResult();
		}

		public Task<ListResourcesResult> ListResourcesAsync(string nextCursor = null, bool tags = true, bool context = true, bool moderations = true, CancellationToken? cancellationToken = null)
		{
			ListResourcesParams parameters = new ListResourcesParams
			{
				NextCursor = nextCursor,
				Tags = tags,
				Context = context,
				Moderations = moderations
			};
			return ListResourcesAsync(parameters, cancellationToken);
		}

		public ListResourcesResult ListResources(string nextCursor = null, bool tags = true, bool context = true, bool moderations = true)
		{
			return ListResourcesAsync(nextCursor, tags, context, moderations).GetAwaiter().GetResult();
		}

		public Task<ListResourcesResult> ListResourcesByTypeAsync(string type, string nextCursor = null, CancellationToken? cancellationToken = null)
		{
			ListResourcesParams parameters = new ListResourcesParams
			{
				Type = type,
				NextCursor = nextCursor
			};
			return ListResourcesAsync(parameters, cancellationToken);
		}

		public ListResourcesResult ListResourcesByType(string type, string nextCursor = null)
		{
			return ListResourcesByTypeAsync(type, nextCursor).GetAwaiter().GetResult();
		}

		public Task<ListResourcesResult> ListResourcesByPrefixAsync(string prefix, string type = "upload", string nextCursor = null, CancellationToken? cancellationToken = null)
		{
			ListResourcesByPrefixParams parameters = new ListResourcesByPrefixParams
			{
				Type = type,
				Prefix = prefix,
				NextCursor = nextCursor
			};
			return ListResourcesAsync(parameters, cancellationToken);
		}

		public ListResourcesResult ListResourcesByPrefix(string prefix, string type = "upload", string nextCursor = null)
		{
			return ListResourcesByPrefixAsync(prefix, type, nextCursor).GetAwaiter().GetResult();
		}

		public Task<ListResourcesResult> ListResourcesByPrefixAsync(string prefix, bool tags, bool context, bool moderations, string type = "upload", string nextCursor = null, CancellationToken? cancellationToken = null)
		{
			ListResourcesByPrefixParams parameters = new ListResourcesByPrefixParams
			{
				Tags = tags,
				Context = context,
				Moderations = moderations,
				Type = type,
				Prefix = prefix,
				NextCursor = nextCursor
			};
			return ListResourcesAsync(parameters, cancellationToken);
		}

		public ListResourcesResult ListResourcesByPrefix(string prefix, bool tags, bool context, bool moderations, string type = "upload", string nextCursor = null)
		{
			return ListResourcesByPrefixAsync(prefix, tags, context, moderations, type, nextCursor).GetAwaiter().GetResult();
		}

		public Task<ListResourcesResult> ListResourcesByTagAsync(string tag, string nextCursor = null, CancellationToken? cancellationToken = null)
		{
			ListResourcesByTagParams parameters = new ListResourcesByTagParams
			{
				Tag = tag,
				NextCursor = nextCursor
			};
			return ListResourcesAsync(parameters, cancellationToken);
		}

		public ListResourcesResult ListResourcesByTag(string tag, string nextCursor = null)
		{
			return ListResourcesByTagAsync(tag, nextCursor).GetAwaiter().GetResult();
		}

		public Task<ListResourcesResult> ListResourcesByPublicIdsAsync(IEnumerable<string> publicIds, CancellationToken? cancellationToken = null)
		{
			ListSpecificResourcesParams parameters = new ListSpecificResourcesParams
			{
				PublicIds = new List<string>(publicIds)
			};
			return ListResourcesAsync(parameters, cancellationToken);
		}

		public ListResourcesResult ListResourcesByPublicIds(IEnumerable<string> publicIds)
		{
			return ListResourcesByPublicIdsAsync(publicIds).GetAwaiter().GetResult();
		}

		public Task<ListResourcesResult> ListResourceByPublicIdsAsync(IEnumerable<string> publicIds, bool tags, bool context, bool moderations, CancellationToken? cancellationToken = null)
		{
			ListSpecificResourcesParams parameters = new ListSpecificResourcesParams
			{
				PublicIds = new List<string>(publicIds),
				Tags = tags,
				Context = context,
				Moderations = moderations
			};
			return ListResourcesAsync(parameters, cancellationToken);
		}

		public ListResourcesResult ListResourceByPublicIds(IEnumerable<string> publicIds, bool tags, bool context, bool moderations)
		{
			return ListResourceByPublicIdsAsync(publicIds, tags, context, moderations).GetAwaiter().GetResult();
		}

		public Task<ListResourcesResult> ListResourcesByModerationStatusAsync(string kind, ModerationStatus status, bool tags = true, bool context = true, bool moderations = true, string nextCursor = null, CancellationToken? cancellationToken = null)
		{
			ListResourcesByModerationParams parameters = new ListResourcesByModerationParams
			{
				ModerationKind = kind,
				ModerationStatus = status,
				Tags = tags,
				Context = context,
				Moderations = moderations,
				NextCursor = nextCursor
			};
			return ListResourcesAsync(parameters, cancellationToken);
		}

		public ListResourcesResult ListResourcesByModerationStatus(string kind, ModerationStatus status, bool tags = true, bool context = true, bool moderations = true, string nextCursor = null)
		{
			return ListResourcesByModerationStatusAsync(kind, status, tags, context, moderations, nextCursor).GetAwaiter().GetResult();
		}

		public Task<ListResourcesResult> ListResourcesByContextAsync(string key, string value = "", bool tags = false, bool context = false, string nextCursor = null, CancellationToken? cancellationToken = null)
		{
			ListResourcesByContextParams parameters = new ListResourcesByContextParams
			{
				Key = key,
				Value = value,
				Tags = tags,
				Context = context,
				NextCursor = nextCursor
			};
			return ListResourcesAsync(parameters, cancellationToken);
		}

		public ListResourcesResult ListResourcesByContext(string key, string value = "", bool tags = false, bool context = false, string nextCursor = null)
		{
			return ListResourcesByContextAsync(key, value, tags, context, nextCursor).GetAwaiter().GetResult();
		}

		public Task<ListResourcesResult> ListResourcesAsync(ListResourcesParams parameters, CancellationToken? cancellationToken = null)
		{
			string listResourcesUrl = GetListResourcesUrl(parameters);
			return CallAdminApiAsync<ListResourcesResult>(HttpMethod.GET, listResourcesUrl, parameters, cancellationToken);
		}

		public ListResourcesResult ListResources(ListResourcesParams parameters)
		{
			return ListResourcesAsync(parameters).GetAwaiter().GetResult();
		}

		public Task<PublishResourceResult> PublishResourceByPrefixAsync(string prefix, PublishResourceParams parameters, CancellationToken? cancellationToken)
		{
			return PublishResourceAsync("prefix", prefix, parameters, cancellationToken);
		}

		public PublishResourceResult PublishResourceByPrefix(string prefix, PublishResourceParams parameters)
		{
			return PublishResource("prefix", prefix, parameters);
		}

		public Task<PublishResourceResult> PublishResourceByTagAsync(string tag, PublishResourceParams parameters, CancellationToken? cancellationToken = null)
		{
			return PublishResourceAsync("tag", tag, parameters, cancellationToken);
		}

		public PublishResourceResult PublishResourceByTag(string tag, PublishResourceParams parameters)
		{
			return PublishResource("tag", tag, parameters);
		}

		public Task<PublishResourceResult> PublishResourceByIdsAsync(string tag, PublishResourceParams parameters, CancellationToken? cancellationToken)
		{
			return PublishResourceAsync(string.Empty, string.Empty, parameters, cancellationToken);
		}

		public PublishResourceResult PublishResourceByIds(string tag, PublishResourceParams parameters)
		{
			return PublishResource(string.Empty, string.Empty, parameters);
		}

		public Task<UpdateResourceAccessModeResult> UpdateResourceAccessModeByTagAsync(string tag, UpdateResourceAccessModeParams parameters, CancellationToken? cancellationToken = null)
		{
			return UpdateResourceAccessModeAsync("tag", tag, parameters, cancellationToken);
		}

		public UpdateResourceAccessModeResult UpdateResourceAccessModeByTag(string tag, UpdateResourceAccessModeParams parameters)
		{
			return UpdateResourceAccessMode("tag", tag, parameters);
		}

		public Task<UpdateResourceAccessModeResult> UpdateResourceAccessModeByPrefixAsync(string prefix, UpdateResourceAccessModeParams parameters, CancellationToken? cancellationToken = null)
		{
			return UpdateResourceAccessModeAsync("prefix", prefix, parameters, cancellationToken);
		}

		public UpdateResourceAccessModeResult UpdateResourceAccessModeByPrefix(string prefix, UpdateResourceAccessModeParams parameters)
		{
			return UpdateResourceAccessMode("prefix", prefix, parameters);
		}

		public Task<UpdateResourceAccessModeResult> UpdateResourceAccessModeByIdsAsync(UpdateResourceAccessModeParams parameters, CancellationToken? cancellationToken = null)
		{
			return UpdateResourceAccessModeAsync(string.Empty, string.Empty, parameters, cancellationToken);
		}

		public UpdateResourceAccessModeResult UpdateResourceAccessModeByIds(UpdateResourceAccessModeParams parameters)
		{
			return UpdateResourceAccessMode(string.Empty, string.Empty, parameters);
		}

		public Task<DelDerivedResResult> DeleteDerivedResourcesByTransformAsync(DelDerivedResParams parameters, CancellationToken? cancellationToken = null)
		{
			UrlBuilder urlBuilder = new UrlBuilder(GetApiUrlV().Add("derived_resources").BuildUrl(), parameters.ToParamsDictionary());
			return CallAdminApiAsync<DelDerivedResResult>(HttpMethod.DELETE, urlBuilder.ToString(), parameters, cancellationToken);
		}

		public DelDerivedResResult DeleteDerivedResourcesByTransform(DelDerivedResParams parameters)
		{
			return DeleteDerivedResourcesByTransformAsync(parameters).GetAwaiter().GetResult();
		}

		public Task<GetFoldersResult> RootFoldersAsync(GetFoldersParams parameters = null, CancellationToken? cancellationToken = null)
		{
			return CallAdminApiAsync<GetFoldersResult>(HttpMethod.GET, GetFolderUrl(null, parameters), parameters, cancellationToken);
		}

		public GetFoldersResult RootFolders(GetFoldersParams parameters = null)
		{
			return RootFoldersAsync(parameters).GetAwaiter().GetResult();
		}

		public Task<GetFoldersResult> SubFoldersAsync(string folder, CancellationToken? cancellationToken = null)
		{
			return SubFoldersAsync(folder, null, cancellationToken);
		}

		public Task<GetFoldersResult> SubFoldersAsync(string folder, GetFoldersParams parameters, CancellationToken? cancellationToken = null)
		{
			CheckFolderParameter(folder);
			return CallAdminApiAsync<GetFoldersResult>(HttpMethod.GET, GetFolderUrl(folder, parameters), null, cancellationToken);
		}

		public GetFoldersResult SubFolders(string folder, GetFoldersParams parameters = null)
		{
			return SubFoldersAsync(folder, parameters).GetAwaiter().GetResult();
		}

		public Task<DeleteFolderResult> DeleteFolderAsync(string folder, CancellationToken? cancellationToken = null)
		{
			string folderUrl = GetFolderUrl(folder);
			return CallAdminApiAsync<DeleteFolderResult>(HttpMethod.DELETE, folderUrl, null, cancellationToken);
		}

		public DeleteFolderResult DeleteFolder(string folder)
		{
			return DeleteFolderAsync(folder).GetAwaiter().GetResult();
		}

		public CreateFolderResult CreateFolder(string folder)
		{
			return CreateFolderAsync(folder).GetAwaiter().GetResult();
		}

		public Task<CreateFolderResult> CreateFolderAsync(string folder, CancellationToken? cancellationToken = null)
		{
			CheckIfNotEmpty(folder);
			return CallAdminApiAsync<CreateFolderResult>(HttpMethod.POST, GetFolderUrl(folder), null, cancellationToken);
		}

		public Task<UploadPresetResult> CreateUploadPresetAsync(UploadPresetParams parameters, CancellationToken? cancellationToken = null)
		{
			string url = GetApiUrlV().Add("upload_presets").BuildUrl();
			return CallAdminApiAsync<UploadPresetResult>(HttpMethod.POST, url, parameters, cancellationToken);
		}

		public UploadPresetResult CreateUploadPreset(UploadPresetParams parameters)
		{
			return CreateUploadPresetAsync(parameters).GetAwaiter().GetResult();
		}

		public Task<UploadPresetResult> UpdateUploadPresetAsync(UploadPresetParams parameters, CancellationToken? cancellationToken = null)
		{
			return CallApiAsync<UploadPresetResult>(PrepareUploadPresetApiParams(parameters), cancellationToken);
		}

		public UploadPresetResult UpdateUploadPreset(UploadPresetParams parameters)
		{
			return CallApi<UploadPresetResult>(PrepareUploadPresetApiParams(parameters));
		}

		public Task<GetUploadPresetResult> GetUploadPresetAsync(string name, CancellationToken? cancellationToken = null)
		{
			string url = GetApiUrlV().Add("upload_presets").Add(name).BuildUrl();
			return CallAdminApiAsync<GetUploadPresetResult>(HttpMethod.GET, url, null, cancellationToken);
		}

		public GetUploadPresetResult GetUploadPreset(string name)
		{
			return GetUploadPresetAsync(name).GetAwaiter().GetResult();
		}

		public Task<ListUploadPresetsResult> ListUploadPresetsAsync(string nextCursor = null, CancellationToken? cancellationToken = null)
		{
			return ListUploadPresetsAsync(new ListUploadPresetsParams
			{
				NextCursor = nextCursor
			}, cancellationToken);
		}

		public ListUploadPresetsResult ListUploadPresets(string nextCursor = null)
		{
			return ListUploadPresets(new ListUploadPresetsParams
			{
				NextCursor = nextCursor
			});
		}

		public Task<ListUploadPresetsResult> ListUploadPresetsAsync(ListUploadPresetsParams parameters, CancellationToken? cancellationToken = null)
		{
			UrlBuilder urlBuilder = new UrlBuilder(GetApiUrlV().Add("upload_presets").BuildUrl(), parameters.ToParamsDictionary());
			return CallAdminApiAsync<ListUploadPresetsResult>(HttpMethod.GET, urlBuilder.ToString(), parameters, cancellationToken);
		}

		public ListUploadPresetsResult ListUploadPresets(ListUploadPresetsParams parameters)
		{
			return ListUploadPresetsAsync(parameters).GetAwaiter().GetResult();
		}

		public Task<DeleteUploadPresetResult> DeleteUploadPresetAsync(string name, CancellationToken? cancellationToken = null)
		{
			string url = GetApiUrlV().Add("upload_presets").Add(name).BuildUrl();
			return CallAdminApiAsync<DeleteUploadPresetResult>(HttpMethod.DELETE, url, null, cancellationToken);
		}

		public DeleteUploadPresetResult DeleteUploadPreset(string name)
		{
			return DeleteUploadPresetAsync(name).GetAwaiter().GetResult();
		}

		public Task<UsageResult> GetUsageAsync(DateTime? date, CancellationToken? cancellationToken = null)
		{
			string usageUrl = GetUsageUrl(date);
			return CallAdminApiAsync<UsageResult>(HttpMethod.GET, usageUrl, null, cancellationToken);
		}

		public UsageResult GetUsage(DateTime? date = null)
		{
			return GetUsageAsync(date).GetAwaiter().GetResult();
		}

		public Task<UsageResult> GetUsageAsync(CancellationToken? cancellationToken = null)
		{
			string usageUrl = GetUsageUrl(null);
			return CallAdminApiAsync<UsageResult>(HttpMethod.GET, usageUrl, null, cancellationToken);
		}

		public Task<ListTagsResult> ListTagsAsync(CancellationToken? cancellationToken = null)
		{
			return ListTagsAsync(new ListTagsParams(), cancellationToken);
		}

		public ListTagsResult ListTags()
		{
			return ListTags(new ListTagsParams());
		}

		public Task<ListTagsResult> ListTagsByPrefixAsync(string prefix, CancellationToken? cancellationToken = null)
		{
			return ListTagsAsync(new ListTagsParams
			{
				Prefix = prefix
			}, cancellationToken);
		}

		public ListTagsResult ListTagsByPrefix(string prefix)
		{
			return ListTags(new ListTagsParams
			{
				Prefix = prefix
			});
		}

		public Task<ListTagsResult> ListTagsAsync(ListTagsParams parameters, CancellationToken? cancellationToken = null)
		{
			UrlBuilder urlBuilder = new UrlBuilder(GetApiUrlV().ResourceType("tags").Add(ApiShared.GetCloudinaryParam(parameters.ResourceType)).BuildUrl(), parameters.ToParamsDictionary());
			return CallAdminApiAsync<ListTagsResult>(HttpMethod.GET, urlBuilder.ToString(), parameters, cancellationToken);
		}

		public ListTagsResult ListTags(ListTagsParams parameters)
		{
			return ListTagsAsync(parameters).GetAwaiter().GetResult();
		}

		public Task<ListTransformsResult> ListTransformationsAsync(CancellationToken? cancellationToken = null)
		{
			return ListTransformationsAsync(new ListTransformsParams(), cancellationToken);
		}

		public ListTransformsResult ListTransformations()
		{
			return ListTransformations(new ListTransformsParams());
		}

		public Task<ListTransformsResult> ListTransformationsAsync(ListTransformsParams parameters, CancellationToken? cancellationToken = null)
		{
			UrlBuilder urlBuilder = new UrlBuilder(GetApiUrlV().ResourceType("transformations").BuildUrl(), parameters.ToParamsDictionary());
			return CallAdminApiAsync<ListTransformsResult>(HttpMethod.GET, urlBuilder.ToString(), parameters, cancellationToken);
		}

		public ListTransformsResult ListTransformations(ListTransformsParams parameters)
		{
			return ListTransformationsAsync(parameters).GetAwaiter().GetResult();
		}

		public Task<GetTransformResult> GetTransformAsync(string transform, CancellationToken? cancellationToken = null)
		{
			return GetTransformAsync(new GetTransformParams
			{
				Transformation = transform
			}, cancellationToken);
		}

		public GetTransformResult GetTransform(string transform)
		{
			return GetTransform(new GetTransformParams
			{
				Transformation = transform
			});
		}

		public Task<GetTransformResult> GetTransformAsync(GetTransformParams parameters, CancellationToken? cancellationToken = null)
		{
			UrlBuilder urlBuilder = new UrlBuilder(GetApiUrlV().ResourceType("transformations").BuildUrl(), parameters.ToParamsDictionary());
			return CallAdminApiAsync<GetTransformResult>(HttpMethod.GET, urlBuilder.ToString(), parameters, cancellationToken);
		}

		public GetTransformResult GetTransform(GetTransformParams parameters)
		{
			return GetTransformAsync(parameters).GetAwaiter().GetResult();
		}

		public Task<GetResourceResult> UpdateResourceAsync(string publicId, ModerationStatus moderationStatus, CancellationToken? cancellationToken = null)
		{
			return UpdateResourceAsync(new UpdateParams(publicId)
			{
				ModerationStatus = moderationStatus
			}, cancellationToken);
		}

		public GetResourceResult UpdateResource(string publicId, ModerationStatus moderationStatus)
		{
			return UpdateResource(new UpdateParams(publicId)
			{
				ModerationStatus = moderationStatus
			});
		}

		public Task<GetResourceResult> UpdateResourceAsync(UpdateParams parameters, CancellationToken? cancellationToken = null)
		{
			string url = GetApiUrlV().ResourceType("resources").Add(ApiShared.GetCloudinaryParam(parameters.ResourceType)).Add(parameters.Type)
				.Add(parameters.PublicId)
				.BuildUrl();
			return CallAdminApiAsync<GetResourceResult>(HttpMethod.POST, url, parameters, cancellationToken);
		}

		public GetResourceResult UpdateResource(UpdateParams parameters)
		{
			return UpdateResourceAsync(parameters).GetAwaiter().GetResult();
		}

		public Task<GetResourceResult> GetResourceAsync(string publicId, CancellationToken? cancellationToken = null)
		{
			return GetResourceAsync(new GetResourceParams(publicId), cancellationToken);
		}

		public GetResourceResult GetResource(string publicId)
		{
			return GetResource(new GetResourceParams(publicId));
		}

		public Task<GetResourceResult> GetResourceAsync(GetResourceParams parameters, CancellationToken? cancellationToken = null)
		{
			UrlBuilder urlBuilder = new UrlBuilder(GetApiUrlV().ResourceType("resources").Add(ApiShared.GetCloudinaryParam(parameters.ResourceType)).Add(parameters.Type)
				.Add(parameters.PublicId)
				.BuildUrl(), parameters.ToParamsDictionary());
			return CallAdminApiAsync<GetResourceResult>(HttpMethod.GET, urlBuilder.ToString(), parameters, cancellationToken);
		}

		public GetResourceResult GetResource(GetResourceParams parameters)
		{
			return GetResourceAsync(parameters).GetAwaiter().GetResult();
		}

		public Task<DelDerivedResResult> DeleteDerivedResourcesAsync(params string[] ids)
		{
			DelDerivedResParams delDerivedResParams = new DelDerivedResParams();
			delDerivedResParams.DerivedResources.AddRange(ids);
			return DeleteDerivedResourcesAsync(delDerivedResParams);
		}

		public DelDerivedResResult DeleteDerivedResources(params string[] ids)
		{
			DelDerivedResParams delDerivedResParams = new DelDerivedResParams();
			delDerivedResParams.DerivedResources.AddRange(ids);
			return DeleteDerivedResources(delDerivedResParams);
		}

		public Task<DelDerivedResResult> DeleteDerivedResourcesAsync(DelDerivedResParams parameters, CancellationToken? cancellationToken = null)
		{
			UrlBuilder urlBuilder = new UrlBuilder(GetApiUrlV().Add("derived_resources").BuildUrl(), parameters.ToParamsDictionary());
			return CallAdminApiAsync<DelDerivedResResult>(HttpMethod.DELETE, urlBuilder.ToString(), parameters, cancellationToken);
		}

		public DelDerivedResResult DeleteDerivedResources(DelDerivedResParams parameters)
		{
			return DeleteDerivedResourcesAsync(parameters).GetAwaiter().GetResult();
		}

		public Task<DelResResult> DeleteResourcesAsync(ResourceType type, params string[] publicIds)
		{
			DelResParams delResParams = new DelResParams
			{
				ResourceType = type
			};
			delResParams.PublicIds.AddRange(publicIds);
			return DeleteResourcesAsync(delResParams);
		}

		public DelResResult DeleteResources(ResourceType type, params string[] publicIds)
		{
			DelResParams delResParams = new DelResParams
			{
				ResourceType = type
			};
			delResParams.PublicIds.AddRange(publicIds);
			return DeleteResources(delResParams);
		}

		public Task<DelResResult> DeleteResourcesAsync(params string[] publicIds)
		{
			DelResParams delResParams = new DelResParams();
			delResParams.PublicIds.AddRange(publicIds);
			return DeleteResourcesAsync(delResParams);
		}

		public DelResResult DeleteResources(params string[] publicIds)
		{
			DelResParams delResParams = new DelResParams();
			delResParams.PublicIds.AddRange(publicIds);
			return DeleteResources(delResParams);
		}

		public Task<DelResResult> DeleteResourcesByPrefixAsync(string prefix, CancellationToken? cancellationToken = null)
		{
			DelResParams parameters = new DelResParams
			{
				Prefix = prefix
			};
			return DeleteResourcesAsync(parameters, cancellationToken);
		}

		public DelResResult DeleteResourcesByPrefix(string prefix)
		{
			DelResParams parameters = new DelResParams
			{
				Prefix = prefix
			};
			return DeleteResources(parameters);
		}

		public Task<DelResResult> DeleteResourcesByPrefixAsync(string prefix, bool keepOriginal, string nextCursor, CancellationToken? cancellationToken = null)
		{
			DelResParams parameters = new DelResParams
			{
				Prefix = prefix,
				KeepOriginal = keepOriginal,
				NextCursor = nextCursor
			};
			return DeleteResourcesAsync(parameters, cancellationToken);
		}

		public DelResResult DeleteResourcesByPrefix(string prefix, bool keepOriginal, string nextCursor)
		{
			DelResParams parameters = new DelResParams
			{
				Prefix = prefix,
				KeepOriginal = keepOriginal,
				NextCursor = nextCursor
			};
			return DeleteResources(parameters);
		}

		public Task<DelResResult> DeleteResourcesByTagAsync(string tag, CancellationToken? cancellationToken = null)
		{
			DelResParams parameters = new DelResParams
			{
				Tag = tag
			};
			return DeleteResourcesAsync(parameters, cancellationToken);
		}

		public DelResResult DeleteResourcesByTag(string tag)
		{
			DelResParams parameters = new DelResParams
			{
				Tag = tag
			};
			return DeleteResources(parameters);
		}

		public Task<DelResResult> DeleteResourcesByTagAsync(string tag, bool keepOriginal, string nextCursor, CancellationToken? cancellationToken = null)
		{
			DelResParams parameters = new DelResParams
			{
				Tag = tag,
				KeepOriginal = keepOriginal,
				NextCursor = nextCursor
			};
			return DeleteResourcesAsync(parameters, cancellationToken);
		}

		public DelResResult DeleteResourcesByTag(string tag, bool keepOriginal, string nextCursor)
		{
			DelResParams parameters = new DelResParams
			{
				Tag = tag,
				KeepOriginal = keepOriginal,
				NextCursor = nextCursor
			};
			return DeleteResources(parameters);
		}

		public Task<DelResResult> DeleteAllResourcesAsync(CancellationToken? cancellationToken = null)
		{
			DelResParams parameters = new DelResParams
			{
				All = true
			};
			return DeleteResourcesAsync(parameters, cancellationToken);
		}

		public DelResResult DeleteAllResources()
		{
			DelResParams parameters = new DelResParams
			{
				All = true
			};
			return DeleteResources(parameters);
		}

		public Task<DelResResult> DeleteAllResourcesAsync(bool keepOriginal, string nextCursor, CancellationToken? cancellationToken = null)
		{
			DelResParams parameters = new DelResParams
			{
				All = true,
				KeepOriginal = keepOriginal,
				NextCursor = nextCursor
			};
			return DeleteResourcesAsync(parameters, cancellationToken);
		}

		public DelResResult DeleteAllResources(bool keepOriginal, string nextCursor)
		{
			DelResParams parameters = new DelResParams
			{
				All = true,
				KeepOriginal = keepOriginal,
				NextCursor = nextCursor
			};
			return DeleteResources(parameters);
		}

		public Task<DelResResult> DeleteResourcesAsync(DelResParams parameters, CancellationToken? cancellationToken = null)
		{
			Url url = GetApiUrlV().Add("resources").Add(ApiShared.GetCloudinaryParam(parameters.ResourceType));
			url = (string.IsNullOrEmpty(parameters.Tag) ? url.Add(parameters.Type) : url.Add("tags").Add(parameters.Tag));
			UrlBuilder urlBuilder = new UrlBuilder(url.BuildUrl(), parameters.ToParamsDictionary());
			return CallAdminApiAsync<DelResResult>(HttpMethod.DELETE, urlBuilder.ToString(), parameters, cancellationToken);
		}

		public DelResResult DeleteResources(DelResParams parameters)
		{
			return DeleteResourcesAsync(parameters).GetAwaiter().GetResult();
		}

		public Task<RestoreResult> RestoreAsync(params string[] publicIds)
		{
			RestoreParams restoreParams = new RestoreParams();
			restoreParams.PublicIds.AddRange(publicIds);
			return RestoreAsync(restoreParams);
		}

		public RestoreResult Restore(params string[] publicIds)
		{
			RestoreParams restoreParams = new RestoreParams();
			restoreParams.PublicIds.AddRange(publicIds);
			return Restore(restoreParams);
		}

		public Task<RestoreResult> RestoreAsync(RestoreParams parameters, CancellationToken? cancellationToken = null)
		{
			string url = GetApiUrlV().ResourceType("resources").Add(ApiShared.GetCloudinaryParam(parameters.ResourceType)).Add("upload")
				.Add("restore")
				.BuildUrl();
			return CallAdminApiAsync<RestoreResult>(HttpMethod.POST, url, parameters, cancellationToken);
		}

		public RestoreResult Restore(RestoreParams parameters)
		{
			return RestoreAsync(parameters).GetAwaiter().GetResult();
		}

		public Task<UploadMappingResults> UploadMappingsAsync(UploadMappingParams parameters, CancellationToken? cancellationToken = null)
		{
			return CallUploadMappingsApiAsync(HttpMethod.GET, parameters, cancellationToken);
		}

		public UploadMappingResults UploadMappings(UploadMappingParams parameters)
		{
			return CallUploadMappingsApi(HttpMethod.GET, parameters);
		}

		public Task<UploadMappingResults> UploadMappingAsync(string folder, CancellationToken? cancellationToken = null)
		{
			if (string.IsNullOrEmpty(folder))
			{
				throw new ArgumentException("Folder name is required.", "folder");
			}
			UploadMappingParams parameters = new UploadMappingParams
			{
				Folder = folder
			};
			return CallUploadMappingsApiAsync(HttpMethod.GET, parameters, cancellationToken);
		}

		public UploadMappingResults UploadMapping(string folder)
		{
			if (string.IsNullOrEmpty(folder))
			{
				throw new ArgumentException("Folder must be specified.");
			}
			UploadMappingParams parameters = new UploadMappingParams
			{
				Folder = folder
			};
			return CallUploadMappingsApi(HttpMethod.GET, parameters);
		}

		public Task<UploadMappingResults> CreateUploadMappingAsync(string folder, string template, CancellationToken? cancellationToken = null)
		{
			UploadMappingParams parameters = CreateUploadMappingParams(folder, template);
			return CallUploadMappingsApiAsync(HttpMethod.POST, parameters, cancellationToken);
		}

		public UploadMappingResults CreateUploadMapping(string folder, string template)
		{
			UploadMappingParams parameters = CreateUploadMappingParams(folder, template);
			return CallUploadMappingsApi(HttpMethod.POST, parameters);
		}

		public Task<UploadMappingResults> UpdateUploadMappingAsync(string folder, string newTemplate, CancellationToken? cancellationToken = null)
		{
			UploadMappingParams parameters = CreateUploadMappingParams(folder, newTemplate);
			return CallUploadMappingsApiAsync(HttpMethod.PUT, parameters, cancellationToken);
		}

		public UploadMappingResults UpdateUploadMapping(string folder, string newTemplate)
		{
			UploadMappingParams parameters = CreateUploadMappingParams(folder, newTemplate);
			return CallUploadMappingsApi(HttpMethod.PUT, parameters);
		}

		public Task<UploadMappingResults> DeleteUploadMappingAsync(CancellationToken? cancellationToken = null)
		{
			return DeleteUploadMappingAsync(string.Empty, cancellationToken);
		}

		public UploadMappingResults DeleteUploadMapping()
		{
			return DeleteUploadMapping(string.Empty);
		}

		public Task<UploadMappingResults> DeleteUploadMappingAsync(string folder, CancellationToken? cancellationToken = null)
		{
			UploadMappingParams parameters = new UploadMappingParams
			{
				Folder = folder
			};
			return CallUploadMappingsApiAsync(HttpMethod.DELETE, parameters, cancellationToken);
		}

		public UploadMappingResults DeleteUploadMapping(string folder)
		{
			UploadMappingParams parameters = new UploadMappingParams
			{
				Folder = folder
			};
			return CallUploadMappingsApi(HttpMethod.DELETE, parameters);
		}

		public Task<UpdateTransformResult> UpdateTransformAsync(UpdateTransformParams parameters, CancellationToken? cancellationToken = null)
		{
			HttpMethod httpMethod = HttpMethod.PUT;
			string transformationUrl = GetTransformationUrl(httpMethod, parameters);
			return CallAdminApiAsync<UpdateTransformResult>(httpMethod, transformationUrl, parameters, cancellationToken);
		}

		public UpdateTransformResult UpdateTransform(UpdateTransformParams parameters)
		{
			return UpdateTransformAsync(parameters).GetAwaiter().GetResult();
		}

		public Task<TransformResult> CreateTransformAsync(CreateTransformParams parameters, CancellationToken? cancellationToken = null)
		{
			HttpMethod httpMethod = HttpMethod.POST;
			string transformationUrl = GetTransformationUrl(httpMethod, parameters);
			return CallAdminApiAsync<TransformResult>(httpMethod, transformationUrl, parameters, cancellationToken);
		}

		public TransformResult CreateTransform(CreateTransformParams parameters)
		{
			return CreateTransformAsync(parameters).GetAwaiter().GetResult();
		}

		public Task<TransformResult> DeleteTransformAsync(string transformName, CancellationToken? cancellationToken = null)
		{
			HttpMethod httpMethod = HttpMethod.DELETE;
			string transformationUrl = GetTransformationUrl(httpMethod, new DeleteTransformParams
			{
				Transformation = transformName
			});
			return CallAdminApiAsync<TransformResult>(httpMethod, transformationUrl, null, cancellationToken);
		}

		public TransformResult DeleteTransform(string transformName)
		{
			return DeleteTransformAsync(transformName).GetAwaiter().GetResult();
		}

		private static void CheckIfNotEmpty(string folder)
		{
			if (string.IsNullOrEmpty(folder))
			{
				throw new ArgumentException("Folder must be set.");
			}
		}

		private static void CheckFolderParameter(string folder)
		{
			if (string.IsNullOrEmpty(folder))
			{
				throw new ArgumentException("folder must be set. Please use RootFolders() to get list of folders in root.");
			}
		}

		private static UploadMappingParams CreateUploadMappingParams(string folder, string template)
		{
			if (string.IsNullOrEmpty(folder))
			{
				throw new ArgumentException("Folder property must be specified.");
			}
			if (string.IsNullOrEmpty(template))
			{
				throw new ArgumentException("Template must be specified.");
			}
			return new UploadMappingParams
			{
				Folder = folder,
				Template = template
			};
		}

		private UploadPresetApiParams PrepareUploadPresetApiParams(UploadPresetParams parameters)
		{
			UploadPresetParams uploadPresetParams = (UploadPresetParams)parameters.Copy();
			uploadPresetParams.Name = null;
			string url = GetApiUrlV().Add("upload_presets").Add(parameters.Name).BuildUrl();
			return new UploadPresetApiParams(HttpMethod.PUT, url, uploadPresetParams);
		}

		private string GetFolderUrl(string folder = null, GetFoldersParams parameters = null)
		{
			string text = GetApiUrlV().Add("folders").Add(folder).BuildUrl();
			if (parameters == null)
			{
				return text;
			}
			return new UrlBuilder(text, parameters.ToParamsDictionary()).ToString();
		}

		private string GetUsageUrl(DateTime? date)
		{
			Url url = GetApiUrlV().Action("usage");
			if (date.HasValue)
			{
				url.Add(date.Value.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture));
			}
			return url.BuildUrl();
		}

		private string GetListResourcesUrl(ListResourcesParams parameters)
		{
			Url url = GetResourcesUrl().Add(ApiShared.GetCloudinaryParam(parameters.ResourceType));
			if (!(parameters is ListResourcesByTagParams listResourcesByTagParams))
			{
				if (!(parameters is ListResourcesByModerationParams listResourcesByModerationParams))
				{
					if (parameters is ListResourcesByContextParams)
					{
						url.Add("context");
					}
				}
				else if (!string.IsNullOrEmpty(listResourcesByModerationParams.ModerationKind))
				{
					url.Add("moderations").Add(listResourcesByModerationParams.ModerationKind).Add(ApiShared.GetCloudinaryParam(listResourcesByModerationParams.ModerationStatus));
				}
			}
			else if (!string.IsNullOrEmpty(listResourcesByTagParams.Tag))
			{
				url.Add("tags").Add(listResourcesByTagParams.Tag);
			}
			return new UrlBuilder(url.BuildUrl(), parameters.ToParamsDictionary()).ToString();
		}

		private Task<PublishResourceResult> PublishResourceAsync(string byKey, string value, PublishResourceParams parameters, CancellationToken? cancellationToken)
		{
			if (!string.IsNullOrWhiteSpace(byKey) && !string.IsNullOrWhiteSpace(value))
			{
				parameters.AddCustomParam(byKey, value);
			}
			Url url = GetApiUrlV().Add("resources").Add(parameters.ResourceType.ToString().ToLowerInvariant()).Add("publish_resources");
			return CallAdminApiAsync<PublishResourceResult>(HttpMethod.POST, url.BuildUrl(), parameters, cancellationToken);
		}

		private PublishResourceResult PublishResource(string byKey, string value, PublishResourceParams parameters)
		{
			return PublishResourceAsync(byKey, value, parameters, null).GetAwaiter().GetResult();
		}

		private Task<UpdateResourceAccessModeResult> UpdateResourceAccessModeAsync(string byKey, string value, UpdateResourceAccessModeParams parameters, CancellationToken? cancellationToken = null)
		{
			if (!string.IsNullOrWhiteSpace(byKey) && !string.IsNullOrWhiteSpace(value))
			{
				parameters.AddCustomParam(byKey, value);
			}
			Url url = GetApiUrlV().Add("resources").Add(parameters.ResourceType.ToString().ToLowerInvariant()).Add(parameters.Type)
				.Add("update_access_mode");
			return CallAdminApiAsync<UpdateResourceAccessModeResult>(HttpMethod.POST, url.BuildUrl(), parameters, cancellationToken);
		}

		private UpdateResourceAccessModeResult UpdateResourceAccessMode(string byKey, string value, UpdateResourceAccessModeParams parameters)
		{
			return UpdateResourceAccessModeAsync(byKey, value, parameters).GetAwaiter().GetResult();
		}

		private Url GetResourcesUrl()
		{
			return GetApiUrlV().ResourceType("resources");
		}

		private Task<T> CallAdminApiAsync<T>(HttpMethod httpMethod, string url, BaseParams parameters, CancellationToken? cancellationToken, Dictionary<string, string> extraHeaders = null) where T : BaseResult, new()
		{
			return m_api.CallApiAsync<T>(httpMethod, url, parameters, null, extraHeaders, cancellationToken);
		}

		private string GetTransformationUrl(HttpMethod httpMethod, BaseParams parameters)
		{
			string text = GetApiUrlV().ResourceType("transformations").BuildUrl();
			if (parameters != null && (httpMethod == HttpMethod.GET || httpMethod == HttpMethod.DELETE))
			{
				text = new UrlBuilder(text, parameters.ToParamsDictionary()).ToString();
			}
			return text;
		}

		private UploadMappingResults CallUploadMappingsApi(HttpMethod httpMethod, UploadMappingParams parameters)
		{
			return CallUploadMappingsApiAsync(httpMethod, parameters).GetAwaiter().GetResult();
		}

		private Task<UploadMappingResults> CallUploadMappingsApiAsync(HttpMethod httpMethod, UploadMappingParams parameters, CancellationToken? cancellationToken = null)
		{
			string url = ((httpMethod == HttpMethod.POST || httpMethod == HttpMethod.PUT) ? GetUploadMappingUrl() : GetUploadMappingUrl(parameters));
			return CallAdminApiAsync<UploadMappingResults>(httpMethod, url, parameters, cancellationToken);
		}

		private Task<T> CallApiAsync<T>(UploadPresetApiParams apiParams, CancellationToken? cancellationToken = null) where T : BaseResult, new()
		{
			return CallAdminApiAsync<T>(apiParams.HttpMethod, apiParams.Url, apiParams.ParamsCopy, cancellationToken);
		}

		private T CallApi<T>(UploadPresetApiParams apiParams) where T : BaseResult, new()
		{
			return CallApiAsync<T>(apiParams).GetAwaiter().GetResult();
		}

		private string GetUploadMappingUrl(UploadMappingParams parameters)
		{
			string uploadMappingUrl = GetUploadMappingUrl();
			if (parameters != null)
			{
				return new UrlBuilder(uploadMappingUrl, parameters.ToParamsDictionary()).ToString();
			}
			return uploadMappingUrl;
		}

		private string GetUploadMappingUrl()
		{
			return GetApiUrlV().ResourceType("upload_mappings").BuildUrl();
		}

		public MetadataFieldResult AddMetadataField<T>(MetadataFieldCreateParams<T> parameters)
		{
			string url = m_api.ApiUrlMetadataFieldV.BuildUrl();
			return CallAdminApiAsync<MetadataFieldResult>(HttpMethod.POST, url, parameters, null, PrepareHeaders()).GetAwaiter().GetResult();
		}

		public MetadataFieldListResult ListMetadataFields()
		{
			return CallAdminApiAsync<MetadataFieldListResult>(HttpMethod.GET, m_api.ApiUrlMetadataFieldV.BuildUrl(), null, null).GetAwaiter().GetResult();
		}

		public MetadataFieldResult GetMetadataField(string fieldExternalId)
		{
			string url = m_api.ApiUrlMetadataFieldV.Add(fieldExternalId).BuildUrl();
			return CallAdminApiAsync<MetadataFieldResult>(HttpMethod.GET, url, null, null).GetAwaiter().GetResult();
		}

		public MetadataFieldResult UpdateMetadataField<T>(string fieldExternalId, MetadataFieldUpdateParams<T> parameters)
		{
			if (string.IsNullOrEmpty(fieldExternalId))
			{
				throw new ArgumentNullException("fieldExternalId");
			}
			string url = m_api.ApiUrlMetadataFieldV.Add(fieldExternalId).BuildUrl();
			return CallAdminApiAsync<MetadataFieldResult>(HttpMethod.PUT, url, parameters, null, PrepareHeaders()).GetAwaiter().GetResult();
		}

		public MetadataDataSourceResult UpdateMetadataDataSourceEntries(string fieldExternalId, MetadataDataSourceParams parameters)
		{
			if (string.IsNullOrEmpty(fieldExternalId))
			{
				throw new ArgumentNullException("fieldExternalId");
			}
			string url = m_api.ApiUrlMetadataFieldV.Add(fieldExternalId).Add("datasource").BuildUrl();
			return CallAdminApiAsync<MetadataDataSourceResult>(HttpMethod.PUT, url, parameters, null, PrepareHeaders()).GetAwaiter().GetResult();
		}

		public DelMetadataFieldResult DeleteMetadataField(string fieldExternalId)
		{
			if (string.IsNullOrEmpty(fieldExternalId))
			{
				throw new ArgumentNullException("fieldExternalId");
			}
			string url = m_api.ApiUrlMetadataFieldV.Add(fieldExternalId).BuildUrl();
			return CallAdminApiAsync<DelMetadataFieldResult>(HttpMethod.DELETE, url, null, null).GetAwaiter().GetResult();
		}

		public MetadataDataSourceResult DeleteMetadataDataSourceEntries(string fieldExternalId, List<string> entriesExternalIds)
		{
			string url = PrepareUrlForDatasourceOperation(fieldExternalId, entriesExternalIds, "datasource");
			return CallAdminApiAsync<MetadataDataSourceResult>(HttpMethod.DELETE, url, null, null).GetAwaiter().GetResult();
		}

		public MetadataDataSourceResult RestoreMetadataDataSourceEntries(string fieldExternalId, List<string> entriesExternalIds)
		{
			string url = PrepareUrlForDatasourceOperation(fieldExternalId, entriesExternalIds, "datasource_restore");
			return CallAdminApiAsync<MetadataDataSourceResult>(HttpMethod.POST, url, null, null).GetAwaiter().GetResult();
		}

		public MetadataUpdateResult UpdateMetadata(MetadataUpdateParams parameters)
		{
			string url = GetApiUrlV().Add(ApiShared.GetCloudinaryParam(parameters.ResourceType)).Add("metadata").BuildUrl();
			return CallAdminApiAsync<MetadataUpdateResult>(HttpMethod.POST, url, parameters, null).GetAwaiter().GetResult();
		}

		private static Dictionary<string, string> PrepareHeaders()
		{
			return new Dictionary<string, string> { { "Content-Type", "application/json" } };
		}

		private string PrepareUrlForDatasourceOperation(string fieldExternalId, List<string> entriesExternalIds, string actionName)
		{
			if (string.IsNullOrEmpty(fieldExternalId))
			{
				throw new ArgumentNullException("fieldExternalId");
			}
			DataSourceEntriesParams dataSourceEntriesParams = new DataSourceEntriesParams(entriesExternalIds);
			return new UrlBuilder(m_api.ApiUrlMetadataFieldV.Add(fieldExternalId).Add(actionName).BuildUrl(), dataSourceEntriesParams.ToParamsDictionary()).ToString();
		}

		public Task<StreamingProfileResult> CreateStreamingProfileAsync(StreamingProfileCreateParams parameters, CancellationToken? cancellationToken = null)
		{
			return CallStreamingProfileApiAsync(HttpMethod.POST, parameters, cancellationToken);
		}

		public StreamingProfileResult CreateStreamingProfile(StreamingProfileCreateParams parameters)
		{
			return CallStreamingProfileApi(HttpMethod.POST, parameters);
		}

		public Task<StreamingProfileResult> UpdateStreamingProfileAsync(string name, StreamingProfileUpdateParams parameters, CancellationToken? cancellationToken = null)
		{
			ValidateCallStreamingProfileApiParameters(name, parameters);
			return CallStreamingProfileApiAsync(HttpMethod.PUT, parameters, cancellationToken);
		}

		public StreamingProfileResult UpdateStreamingProfile(string name, StreamingProfileUpdateParams parameters)
		{
			ValidateCallStreamingProfileApiParameters(name, parameters);
			return CallStreamingProfileApi(HttpMethod.PUT, parameters, name);
		}

		public Task<StreamingProfileResult> DeleteStreamingProfileAsync(string name, CancellationToken? cancellationToken = null)
		{
			ValidateNameForCallStreamingProfileApiParameters(name);
			return CallStreamingProfileApiAsync(HttpMethod.DELETE, null, cancellationToken, name);
		}

		public StreamingProfileResult DeleteStreamingProfile(string name)
		{
			ValidateNameForCallStreamingProfileApiParameters(name);
			return CallStreamingProfileApi(HttpMethod.DELETE, null, name);
		}

		public Task<StreamingProfileResult> GetStreamingProfileAsync(string name, CancellationToken? cancellationToken = null)
		{
			ValidateNameForCallStreamingProfileApiParameters(name);
			return CallStreamingProfileApiAsync(HttpMethod.GET, null, cancellationToken, name);
		}

		public StreamingProfileResult GetStreamingProfile(string name)
		{
			ValidateNameForCallStreamingProfileApiParameters(name);
			return CallStreamingProfileApi(HttpMethod.GET, null, name);
		}

		public Task<StreamingProfileListResult> ListStreamingProfilesAsync(CancellationToken? cancellationToken = null)
		{
			return CallAdminApiAsync<StreamingProfileListResult>(HttpMethod.GET, m_api.ApiUrlStreamingProfileV.BuildUrl(), null, cancellationToken);
		}

		public StreamingProfileListResult ListStreamingProfiles()
		{
			return ListStreamingProfilesAsync().GetAwaiter().GetResult();
		}

		private static void ValidateCallStreamingProfileApiParameters(string name, StreamingProfileUpdateParams parameters)
		{
			ValidateNameForCallStreamingProfileApiParameters(name);
			ValidateStreamingProfileUpdateParams(parameters);
		}

		private static void ValidateStreamingProfileUpdateParams(StreamingProfileUpdateParams parameters)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
		}

		private static void ValidateNameForCallStreamingProfileApiParameters(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("Name parameter should be defined", "name");
			}
		}

		private Task<StreamingProfileResult> CallStreamingProfileApiAsync(HttpMethod httpMethod, BaseParams parameters, CancellationToken? cancellationToken, string name = null)
		{
			return CallAdminApiAsync<StreamingProfileResult>(httpMethod, m_api.ApiUrlStreamingProfileV.Add(name).BuildUrl(), parameters, cancellationToken);
		}

		private StreamingProfileResult CallStreamingProfileApi(HttpMethod httpMethod, BaseParams parameters, string name = null)
		{
			return CallStreamingProfileApiAsync(httpMethod, parameters, null, name).GetAwaiter().GetResult();
		}

		public Cloudinary()
		{
			m_api = new Api();
		}

		public Cloudinary(string cloudinaryUrl)
		{
			m_api = new Api(cloudinaryUrl);
		}

		public Cloudinary(Account account)
		{
			m_api = new Api(account);
		}

		public string GetCloudinaryJsConfig(bool directUpload = false, string dir = "")
		{
			if (string.IsNullOrEmpty(dir))
			{
				dir = "/Scripts";
			}
			StringBuilder stringBuilder = new StringBuilder(1000);
			AppendScriptLine(stringBuilder, dir, "jquery.ui.widget.js");
			AppendScriptLine(stringBuilder, dir, "jquery.iframe-transport.js");
			AppendScriptLine(stringBuilder, dir, "jquery.fileupload.js");
			AppendScriptLine(stringBuilder, dir, "jquery.cloudinary.js");
			if (directUpload)
			{
				AppendScriptLine(stringBuilder, dir, "canvas-to-blob.min.js");
				AppendScriptLine(stringBuilder, dir, "jquery.fileupload-image.js");
				AppendScriptLine(stringBuilder, dir, "jquery.fileupload-process.js");
				AppendScriptLine(stringBuilder, dir, "jquery.fileupload-validate.js");
				AppendScriptLine(stringBuilder, dir, "load-image.min.js");
			}
			object[] content = new JProperty[4]
			{
				new JProperty("cloud_name", m_api.Account.Cloud),
				new JProperty("api_key", m_api.Account.ApiKey),
				new JProperty("private_cdn", m_api.UsePrivateCdn),
				new JProperty("cdn_subdomain", m_api.CSubDomain)
			};
			JObject jObject = new JObject(content);
			if (!string.IsNullOrEmpty(m_api.PrivateCdn))
			{
				jObject.Add("secure_distribution", m_api.PrivateCdn);
			}
			stringBuilder.AppendLine("<script type='text/javascript'>");
			stringBuilder.Append("$.cloudinary.config(");
			stringBuilder.Append(jObject.ToString());
			stringBuilder.AppendLine(");");
			stringBuilder.AppendLine("</script>");
			return stringBuilder.ToString();
		}

		private static void AppendScriptLine(StringBuilder sb, string dir, string script)
		{
			sb.Append("<script src=\"");
			sb.Append(dir);
			if (!dir.EndsWith("/", StringComparison.Ordinal) && !dir.EndsWith("\\", StringComparison.Ordinal))
			{
				sb.Append('/');
			}
			sb.Append(script);
			sb.AppendLine("\"></script>");
		}

		private Url GetApiUrlV()
		{
			return m_api.ApiUrlV;
		}

		public Task<ImageUploadResult> UploadAsync(ImageUploadParams parameters, CancellationToken? cancellationToken = null)
		{
			return UploadAsync<ImageUploadResult>(parameters, cancellationToken);
		}

		public ImageUploadResult Upload(ImageUploadParams parameters)
		{
			return Upload<ImageUploadResult, ImageUploadParams>(parameters);
		}

		public Task<VideoUploadResult> UploadAsync(VideoUploadParams parameters, CancellationToken? cancellationToken = null)
		{
			return UploadAsync<VideoUploadResult>(parameters, cancellationToken);
		}

		public VideoUploadResult Upload(VideoUploadParams parameters)
		{
			return Upload<VideoUploadResult, VideoUploadParams>(parameters);
		}

		public Task<RawUploadResult> UploadAsync(string resourceType, IDictionary<string, object> parameters, FileDescription fileDescription, CancellationToken? cancellationToken = null)
		{
			string uploadUrl = GetUploadUrl(resourceType);
			fileDescription.Reset();
			SortedDictionary<string, object> parameters2 = NormalizeParameters(parameters);
			return CallUploadApiAsync(uploadUrl, parameters2, cancellationToken, fileDescription);
		}

		public RawUploadResult Upload(string resourceType, IDictionary<string, object> parameters, FileDescription fileDescription)
		{
			return UploadAsync(resourceType, parameters, fileDescription).GetAwaiter().GetResult();
		}

		public Task<RawUploadResult> UploadAsync(RawUploadParams parameters, string type = "auto", CancellationToken? cancellationToken = null)
		{
			string url = m_api.ApiUrlImgUpV.ResourceType(type).BuildUrl();
			parameters.File.Reset();
			return CallUploadApiAsync<RawUploadResult>(HttpMethod.POST, url, parameters, cancellationToken, parameters.File);
		}

		public RawUploadResult Upload(RawUploadParams parameters, string type = "auto")
		{
			return UploadAsync(parameters, type).GetAwaiter().GetResult();
		}

		public Task<RawUploadResult> UploadLargeRawAsync(BasicRawUploadParams parameters, int bufferSize = 20971520, CancellationToken? cancellationToken = null)
		{
			return UploadLargeAsync<RawUploadResult>(parameters, bufferSize, cancellationToken);
		}

		public RawUploadResult UploadLargeRaw(BasicRawUploadParams parameters, int bufferSize = 20971520)
		{
			return UploadLarge<RawUploadResult>(parameters, bufferSize);
		}

		public Task<RawUploadResult> UploadLargeAsync(RawUploadParams parameters, int bufferSize = 20971520, CancellationToken? cancellationToken = null)
		{
			return UploadLargeAsync<RawUploadResult>(parameters, bufferSize, cancellationToken);
		}

		public RawUploadResult UploadLarge(RawUploadParams parameters, int bufferSize = 20971520)
		{
			return UploadLarge<RawUploadResult>(parameters, bufferSize);
		}

		public Task<ImageUploadResult> UploadLargeAsync(ImageUploadParams parameters, int bufferSize = 20971520, CancellationToken? cancellationToken = null)
		{
			return UploadLargeAsync<ImageUploadResult>(parameters, bufferSize, cancellationToken);
		}

		public ImageUploadResult UploadLarge(ImageUploadParams parameters, int bufferSize = 20971520)
		{
			return UploadLarge<ImageUploadResult>(parameters, bufferSize);
		}

		public Task<VideoUploadResult> UploadLargeAsync(VideoUploadParams parameters, int bufferSize = 20971520, CancellationToken? cancellationToken = null)
		{
			return UploadLargeAsync<VideoUploadResult>(parameters, bufferSize, cancellationToken);
		}

		public VideoUploadResult UploadLarge(VideoUploadParams parameters, int bufferSize = 20971520)
		{
			return UploadLarge<VideoUploadResult>(parameters, bufferSize);
		}

		public Task<RawUploadResult> UploadLargeAsync(AutoUploadParams parameters, int bufferSize = 20971520, CancellationToken? cancellationToken = null)
		{
			return UploadLargeAsync<RawUploadResult>(parameters, bufferSize, cancellationToken);
		}

		public RawUploadResult UploadLarge(AutoUploadParams parameters, int bufferSize = 20971520)
		{
			return UploadLarge<RawUploadResult>(parameters, bufferSize);
		}

		[Obsolete("Use UploadLarge(parameters, bufferSize) instead.")]
		public UploadResult UploadLarge(BasicRawUploadParams parameters, int bufferSize = 20971520, bool isRaw = false)
		{
			if (isRaw)
			{
				return UploadLarge<RawUploadResult>(parameters, bufferSize);
			}
			return UploadLarge<ImageUploadResult>(parameters, bufferSize);
		}

		public async Task<T> UploadLargeAsync<T>(BasicRawUploadParams parameters, int bufferSize = 20971520, CancellationToken? cancellationToken = null) where T : UploadResult, new()
		{
			CheckUploadParameters(parameters);
			if (parameters.File.IsRemote)
			{
				return await UploadAsync<T>(parameters).ConfigureAwait(continueOnCapturedContext: false);
			}
			UploadLargeParams internalParams = new UploadLargeParams(parameters, bufferSize, m_api);
			T result = null;
			while (!parameters.File.Eof)
			{
				UpdateContentRange(internalParams);
				result = await CallUploadApiAsync<T>(HttpMethod.POST, internalParams.Url, parameters, cancellationToken, parameters.File, internalParams.Headers).ConfigureAwait(continueOnCapturedContext: false);
				CheckUploadResult(result);
			}
			return result;
		}

		public T UploadLarge<T>(BasicRawUploadParams parameters, int bufferSize = 20971520) where T : UploadResult, new()
		{
			return UploadLargeAsync<T>(parameters, bufferSize).GetAwaiter().GetResult();
		}

		public Task<RenameResult> RenameAsync(string fromPublicId, string toPublicId, bool overwrite = false, CancellationToken? cancellationToken = null)
		{
			return RenameAsync(new RenameParams(fromPublicId, toPublicId)
			{
				Overwrite = overwrite
			}, cancellationToken);
		}

		public RenameResult Rename(string fromPublicId, string toPublicId, bool overwrite = false)
		{
			RenameParams parameters = new RenameParams(fromPublicId, toPublicId)
			{
				Overwrite = overwrite
			};
			return RenameAsync(parameters).GetAwaiter().GetResult();
		}

		public Task<RenameResult> RenameAsync(RenameParams parameters, CancellationToken? cancellationToken = null)
		{
			string renameUrl = GetRenameUrl(parameters);
			return CallUploadApiAsync<RenameResult>(HttpMethod.POST, renameUrl, parameters, cancellationToken);
		}

		public RenameResult Rename(RenameParams parameters)
		{
			return RenameAsync(parameters).GetAwaiter().GetResult();
		}

		public Task<DeletionResult> DestroyAsync(DeletionParams parameters)
		{
			string url = m_api.ApiUrlImgUpV.ResourceType(ApiShared.GetCloudinaryParam(parameters.ResourceType)).Action("destroy").BuildUrl();
			return CallUploadApiAsync<DeletionResult>(HttpMethod.POST, url, parameters, null);
		}

		public DeletionResult Destroy(DeletionParams parameters)
		{
			return DestroyAsync(parameters).GetAwaiter().GetResult();
		}

		public string DownloadBackedUpAsset(string assetId, string versionId)
		{
			Utils.ShouldNotBeEmpty(() => assetId);
			Utils.ShouldNotBeEmpty(() => versionId);
			SortedDictionary<string, object> parameters = new SortedDictionary<string, object>
			{
				{ "asset_id", assetId },
				{ "version_id", versionId }
			};
			UrlBuilder builder = new UrlBuilder(GetApiUrlV().Action("download_backup").BuildUrl());
			return GetDownloadUrl(builder, parameters);
		}

		public Task<TagResult> TagAsync(TagParams parameters, CancellationToken? cancellationToken = null)
		{
			string url = GetApiUrlV().ResourceType(ApiShared.GetCloudinaryParam(parameters.ResourceType)).Action("tags").BuildUrl();
			return CallUploadApiAsync<TagResult>(HttpMethod.POST, url, parameters, cancellationToken);
		}

		public TagResult Tag(TagParams parameters)
		{
			return TagAsync(parameters).GetAwaiter().GetResult();
		}

		public Task<ContextResult> ContextAsync(ContextParams parameters, CancellationToken? cancellationToken = null)
		{
			string url = m_api.ApiUrlImgUpV.ResourceType(ApiShared.GetCloudinaryParam(parameters.ResourceType)).Action("context").BuildUrl();
			return CallUploadApiAsync<ContextResult>(HttpMethod.POST, url, parameters, cancellationToken);
		}

		public ContextResult Context(ContextParams parameters)
		{
			return ContextAsync(parameters).GetAwaiter().GetResult();
		}

		public Task<ExplicitResult> ExplicitAsync(ExplicitParams parameters, CancellationToken? cancellationToken = null)
		{
			string url = GetApiUrlV().ResourceType(ApiShared.GetCloudinaryParam(parameters.ResourceType)).Action("explicit").BuildUrl();
			return CallUploadApiAsync<ExplicitResult>(HttpMethod.POST, url, parameters, cancellationToken);
		}

		public ExplicitResult Explicit(ExplicitParams parameters)
		{
			return ExplicitAsync(parameters).GetAwaiter().GetResult();
		}

		public Task<ExplodeResult> ExplodeAsync(ExplodeParams parameters, CancellationToken? cancellationToken = null)
		{
			string url = m_api.ApiUrlImgUpV.Action("explode").BuildUrl();
			return CallUploadApiAsync<ExplodeResult>(HttpMethod.POST, url, parameters, cancellationToken);
		}

		public ExplodeResult Explode(ExplodeParams parameters)
		{
			return ExplodeAsync(parameters).GetAwaiter().GetResult();
		}

		public Task<ArchiveResult> CreateZipAsync(ArchiveParams parameters, CancellationToken? cancellationToken = null)
		{
			parameters.TargetFormat(ArchiveFormat.Zip);
			return CreateArchiveAsync(parameters, cancellationToken);
		}

		public ArchiveResult CreateZip(ArchiveParams parameters)
		{
			parameters.TargetFormat(ArchiveFormat.Zip);
			return CreateArchive(parameters);
		}

		public Task<ArchiveResult> CreateArchiveAsync(ArchiveParams parameters, CancellationToken? cancellationToken = null)
		{
			Url url = GetApiUrlV().ResourceType("image").Action("generate_archive");
			if (!string.IsNullOrEmpty(parameters.ResourceType()))
			{
				url.ResourceType(parameters.ResourceType());
			}
			parameters.Mode(ArchiveCallMode.Create);
			return CallUploadApiAsync<ArchiveResult>(HttpMethod.POST, url.BuildUrl(), parameters, cancellationToken);
		}

		public ArchiveResult CreateArchive(ArchiveParams parameters)
		{
			return CreateArchiveAsync(parameters).GetAwaiter().GetResult();
		}

		public string DownloadArchiveUrl(ArchiveParams parameters)
		{
			parameters.Mode(ArchiveCallMode.Download);
			UrlBuilder builder = new UrlBuilder(GetApiUrlV().ResourceType(parameters.ResourceType()).Action("generate_archive").BuildUrl());
			return GetDownloadUrl(builder, parameters.ToParamsDictionary());
		}

		public string DownloadFolder(string folderPath, ArchiveParams parameters = null)
		{
			ArchiveParams archiveParams = parameters ?? new ArchiveParams();
			archiveParams.Prefixes(new List<string> { folderPath });
			archiveParams.ResourceType("all");
			return DownloadArchiveUrl(archiveParams);
		}

		public string DownloadZip(string tag, Transformation transform, string resourceType = "image")
		{
			if (string.IsNullOrEmpty(tag))
			{
				throw new ArgumentException("Tag should be specified!");
			}
			UrlBuilder builder = new UrlBuilder(GetApiUrlV().ResourceType(resourceType).Action("download_tag.zip").BuildUrl());
			SortedDictionary<string, object> sortedDictionary = new SortedDictionary<string, object> { { "tag", tag } };
			if (transform != null)
			{
				sortedDictionary.Add("transformation", transform.Generate());
			}
			return GetDownloadUrl(builder, sortedDictionary);
		}

		public string DownloadPrivate(string publicId, bool? attachment = null, string format = "", string type = "", long? expiresAt = null, string resourceType = "image")
		{
			if (string.IsNullOrEmpty(publicId))
			{
				throw new ArgumentException("The image public ID is missing.");
			}
			UrlBuilder builder = new UrlBuilder(GetApiUrlV().ResourceType(resourceType).Action("download").BuildUrl());
			SortedDictionary<string, object> sortedDictionary = new SortedDictionary<string, object> { { "public_id", publicId } };
			if (!string.IsNullOrEmpty(format))
			{
				sortedDictionary.Add("format", format);
			}
			if (attachment.HasValue)
			{
				sortedDictionary.Add("attachment", attachment.Value ? "true" : "false");
			}
			if (!string.IsNullOrEmpty(type))
			{
				sortedDictionary.Add("type", type);
			}
			if (expiresAt.HasValue)
			{
				sortedDictionary.Add("expires_at", expiresAt);
			}
			return GetDownloadUrl(builder, sortedDictionary);
		}

		public Task<SpriteResult> MakeSpriteAsync(SpriteParams parameters, CancellationToken? cancellationToken = null)
		{
			string url = m_api.ApiUrlImgUpV.Action("sprite").BuildUrl();
			return CallUploadApiAsync<SpriteResult>(HttpMethod.POST, url, parameters, cancellationToken);
		}

		public SpriteResult MakeSprite(SpriteParams parameters)
		{
			return MakeSpriteAsync(parameters).GetAwaiter().GetResult();
		}

		public string DownloadSprite(SpriteParams parameters)
		{
			parameters.Mode = ArchiveCallMode.Download;
			UrlBuilder builder = new UrlBuilder(m_api.ApiUrlImgUpV.Action("sprite").BuildUrl());
			return GetDownloadUrl(builder, parameters.ToParamsDictionary());
		}

		public Task<MultiResult> MultiAsync(MultiParams parameters, CancellationToken? cancellationToken = null)
		{
			string url = m_api.ApiUrlImgUpV.Action("multi").BuildUrl();
			return CallUploadApiAsync<MultiResult>(HttpMethod.POST, url, parameters, cancellationToken);
		}

		public MultiResult Multi(MultiParams parameters)
		{
			return MultiAsync(parameters).GetAwaiter().GetResult();
		}

		public string DownloadMulti(MultiParams parameters)
		{
			parameters.Mode = ArchiveCallMode.Download;
			UrlBuilder builder = new UrlBuilder(m_api.ApiUrlImgUpV.Action("multi").BuildUrl());
			return GetDownloadUrl(builder, parameters.ToParamsDictionary());
		}

		public Task<TextResult> TextAsync(string text, CancellationToken? cancellationToken = null)
		{
			return TextAsync(new TextParams(text), cancellationToken);
		}

		public TextResult Text(string text)
		{
			return Text(new TextParams(text));
		}

		public Task<TextResult> TextAsync(TextParams parameters, CancellationToken? cancellationToken = null)
		{
			string url = m_api.ApiUrlImgUpV.Action("text").BuildUrl();
			return CallUploadApiAsync<TextResult>(HttpMethod.POST, url, parameters, cancellationToken);
		}

		public CreateSlideshowResult CreateSlideshow(CreateSlideshowParams parameters)
		{
			return CreateSlideshowAsync(parameters).GetAwaiter().GetResult();
		}

		public Task<CreateSlideshowResult> CreateSlideshowAsync(CreateSlideshowParams parameters, CancellationToken? cancellationToken = null)
		{
			string url = m_api.ApiUrlVideoUpV.Action("create_slideshow").BuildUrl();
			return CallUploadApiAsync<CreateSlideshowResult>(HttpMethod.POST, url, parameters, cancellationToken);
		}

		public TextResult Text(TextParams parameters)
		{
			return TextAsync(parameters).GetAwaiter().GetResult();
		}

		private static SortedDictionary<string, object> NormalizeParameters(IDictionary<string, object> parameters)
		{
			if (parameters == null)
			{
				return new SortedDictionary<string, object>();
			}
			return (parameters as SortedDictionary<string, object>) ?? new SortedDictionary<string, object>(parameters);
		}

		private static void CheckUploadResult<T>(T result) where T : UploadResult, new()
		{
			if (result.StatusCode != HttpStatusCode.OK)
			{
				string arg = ((result.Error != null) ? result.Error.Message : "Unknown error");
				throw new Exception($"An error has occured while uploading file (status code: {result.StatusCode}). {arg}");
			}
		}

		private static void CheckUploadParameters(BasicRawUploadParams parameters)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters", "Upload parameters should be defined");
			}
			if (parameters.File == null)
			{
				throw new ArgumentException("Parameters.File parameter should be defined");
			}
		}

		private static void UpdateContentRange(UploadLargeParams internalParams)
		{
			FileDescription file = internalParams.Parameters.File;
			long fileLength = file.GetFileLength();
			long bytesSent = file.BytesSent;
			long num = bytesSent + Math.Min(internalParams.BufferSize, fileLength - bytesSent) - 1;
			internalParams.Headers["Content-Range"] = $"bytes {bytesSent}-{num}/{fileLength}";
		}

		private Task<T> CallUploadApiAsync<T>(HttpMethod httpMethod, string url, BaseParams parameters, CancellationToken? cancellationToken, FileDescription fileDescription = null, Dictionary<string, string> extraHeaders = null) where T : BaseResult, new()
		{
			return m_api.CallApiAsync<T>(httpMethod, url, parameters, fileDescription, extraHeaders, cancellationToken);
		}

		private Task<RawUploadResult> CallUploadApiAsync(string url, SortedDictionary<string, object> parameters, CancellationToken? cancellationToken, FileDescription fileDescription = null)
		{
			return m_api.CallAndParseAsync<RawUploadResult>(HttpMethod.POST, url, parameters, fileDescription, null, cancellationToken);
		}

		private string GetUploadUrl(string resourceType)
		{
			return GetApiUrlV().Action("upload").ResourceType(resourceType).BuildUrl();
		}

		private string GetRenameUrl(RenameParams parameters)
		{
			return m_api.ApiUrlImgUpV.ResourceType(ApiShared.GetCloudinaryParam(parameters.ResourceType)).Action("rename").BuildUrl();
		}

		private string CheckUploadParametersAndGetUploadUrl(BasicRawUploadParams parameters)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters", "Upload parameters should be defined");
			}
			string result = GetApiUrlV().Action("upload").ResourceType(ApiShared.GetCloudinaryParam(parameters.ResourceType)).BuildUrl();
			parameters.File.Reset();
			return result;
		}

		private T Upload<T, TP>(TP parameters) where T : UploadResult, new() where TP : BasicRawUploadParams, new()
		{
			return UploadAsync<T>(parameters).GetAwaiter().GetResult();
		}

		private Task<T> UploadAsync<T>(BasicRawUploadParams parameters, CancellationToken? cancellationToken = null) where T : UploadResult, new()
		{
			string url = CheckUploadParametersAndGetUploadUrl(parameters);
			return CallUploadApiAsync<T>(HttpMethod.POST, url, parameters, cancellationToken, parameters.File);
		}

		private string GetDownloadUrl(UrlBuilder builder, IDictionary<string, object> parameters)
		{
			m_api.FinalizeUploadParameters(parameters);
			builder.SetParameters(parameters);
			return builder.ToString();
		}
	}
}
