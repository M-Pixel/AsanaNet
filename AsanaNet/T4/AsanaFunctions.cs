using System;
using System.Threading.Tasks;
using System.Collections.Generic;
/*
* THIS FILE IS GENERATED! DO NOT EDIT!
* REFER TO AsanaFunctionDefinitions.xml
*/
namespace AsanaNet
{
		// Enums for all functions
		public enum Function
		{
			GetUsers,
			GetMe,
			GetUserById,
			GetWorkspaces,
			GetWorkspaceById,
			GetUsersInWorkspace,
			GetMyTasks,
			GetTasksInWorkspace,
			GetProjectsInWorkspace,
			GetTagsInWorkspace,
			GetTaskById,
			GetSubtasksInTask,
			GetStoriesInTask,
			GetProjectsOnATask,
			GetTagsOnATask,
			GetTasksByTag,
			GetStoryById,
			GetProjectById,
			GetTasksInAProject,
			GetTagById,
			GetTeamsInWorkspace,
			GetTeamById,
			GetEvents,
			CreateTask,
			CreateProject,
			CreateTag,
			CreateStory,
			CreateSubtask,
			SetParent,
			AddProject,
			RemoveProject,
			AddTag,
			RemoveTag,
			AddFollowers,
			RemoveFollowers,
			UpdateTask,
			UpdateTag,
			UpdateProject,
			UpdateWorkspace,
			DeleteTask,
			DeleteProject,
			DeleteStory,
		}

		// Function definitions specifically for the GET functions.
		public partial class Asana
		{
			// just returns cached response
			public AsanaObjectCollection<AsanaUser> GetUsersCachedOrNull()
			{
				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetUsers)); 
				
                AsanaObjectCollection<AsanaUser> cachedObject = (AsanaObjectCollection<AsanaUser>) ObjectCache.Get(cachePath);
                if (cachedObject != null && cachedObject.Initialized)
                    return cachedObject;
				return null;
			}
			// real one:
			public async Task<AsanaObjectCollection<AsanaUser>> GetUsers(string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default, Dictionary<string, object> extraCallArguments = null)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetUsers)); 
				
                AsanaObjectCollection<AsanaUser> cachedObject = null;
                
                cachedObject = (AsanaObjectCollection<AsanaUser>) ObjectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if ((cachedObject != null && cachedObject.Initialized) || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
                if (extraCallArguments == null)
                    extraCallArguments = new Dictionary<string, object>();
                if (optFields != null)
                    extraCallArguments.Add("opt_fields", optFields);
			    Uri uri;
                if (extraCallArguments.Count > 0)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetUsers), extraCallArguments);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetUsers));

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetUsers), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, cacheLevel >= AsanaCacheLevel.FillExisting);
                //even if empty, we fetched it and there's nothing - we can cache it next time
                output.Initialized = true;
				ObjectCache.Set(cachePath, output);
				return output;
			}

			// just returns cached response
			public AsanaUser GetMeCachedOrNull()
			{
				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetMe)); 
				
                AsanaUser cachedObject = (AsanaUser) ObjectCache.Get(cachePath);
                if (cachedObject != null)
                    return cachedObject;
				return null;
			}
			// real one:
			public async Task<AsanaUser> GetMe(string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default, Dictionary<string, object> extraCallArguments = null)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetMe)); 
				
                AsanaUser cachedObject = null;
                if (cacheLevel >= AsanaCacheLevel.FillExisting)
                cachedObject = (AsanaUser) ObjectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if ((cachedObject != null) || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
                if (extraCallArguments == null)
                    extraCallArguments = new Dictionary<string, object>();
                if (optFields != null)
                    extraCallArguments.Add("opt_fields", optFields);
			    Uri uri;
                if (extraCallArguments.Count > 0)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetMe), extraCallArguments);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetMe));

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetMe), uri);
                var output = AsanaRequest.GetResponse(response, this, cachedObject);
				ObjectCache.Set(cachePath, output);
				return output;
			}

			// just returns cached response
			public AsanaUser GetUserByIdCachedOrNull(Int64 arg1)
			{
				string cachePath = string.Format(new PropertyFormatProvider(), "{0}", arg1);
                AsanaUser cachedObject = (AsanaUser) ObjectCache.Get(cachePath);
                if (cachedObject != null)
                    return cachedObject;
				return null;
			}
			// real one:
			public async Task<AsanaUser> GetUserById(Int64 arg1, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default, Dictionary<string, object> extraCallArguments = null)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = string.Format(new PropertyFormatProvider(), "{0}", arg1);
                AsanaUser cachedObject = null;
                if (cacheLevel >= AsanaCacheLevel.FillExisting)
                cachedObject = (AsanaUser) ObjectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if ((cachedObject != null) || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
                if (extraCallArguments == null)
                    extraCallArguments = new Dictionary<string, object>();
                if (optFields != null)
                    extraCallArguments.Add("opt_fields", optFields);
			    Uri uri;
                if (extraCallArguments.Count > 0)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetUserById), extraCallArguments, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetUserById), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetUserById), uri);
                var output = AsanaRequest.GetResponse(response, this, cachedObject);
				ObjectCache.Set(cachePath, output);
				return output;
			}

			// just returns cached response
			public AsanaObjectCollection<AsanaWorkspace> GetWorkspacesCachedOrNull()
			{
				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetWorkspaces)); 
				
                AsanaObjectCollection<AsanaWorkspace> cachedObject = (AsanaObjectCollection<AsanaWorkspace>) ObjectCache.Get(cachePath);
                if (cachedObject != null && cachedObject.Initialized)
                    return cachedObject;
				return null;
			}
			// real one:
			public async Task<AsanaObjectCollection<AsanaWorkspace>> GetWorkspaces(string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default, Dictionary<string, object> extraCallArguments = null)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetWorkspaces)); 
				
                AsanaObjectCollection<AsanaWorkspace> cachedObject = null;
                
                cachedObject = (AsanaObjectCollection<AsanaWorkspace>) ObjectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if ((cachedObject != null && cachedObject.Initialized) || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
                if (extraCallArguments == null)
                    extraCallArguments = new Dictionary<string, object>();
                if (optFields != null)
                    extraCallArguments.Add("opt_fields", optFields);
			    Uri uri;
                if (extraCallArguments.Count > 0)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetWorkspaces), extraCallArguments);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetWorkspaces));

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetWorkspaces), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, cacheLevel >= AsanaCacheLevel.FillExisting);
                //even if empty, we fetched it and there's nothing - we can cache it next time
                output.Initialized = true;
				ObjectCache.Set(cachePath, output);
				return output;
			}

			// just returns cached response
			public AsanaWorkspace GetWorkspaceByIdCachedOrNull(Int64 arg1)
			{
				string cachePath = string.Format(new PropertyFormatProvider(), "{0}", arg1);
                AsanaWorkspace cachedObject = (AsanaWorkspace) ObjectCache.Get(cachePath);
                if (cachedObject != null)
                    return cachedObject;
				return null;
			}
			// real one:
			public async Task<AsanaWorkspace> GetWorkspaceById(Int64 arg1, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default, Dictionary<string, object> extraCallArguments = null)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = string.Format(new PropertyFormatProvider(), "{0}", arg1);
                AsanaWorkspace cachedObject = null;
                if (cacheLevel >= AsanaCacheLevel.FillExisting)
                cachedObject = (AsanaWorkspace) ObjectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if ((cachedObject != null) || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
                if (extraCallArguments == null)
                    extraCallArguments = new Dictionary<string, object>();
                if (optFields != null)
                    extraCallArguments.Add("opt_fields", optFields);
			    Uri uri;
                if (extraCallArguments.Count > 0)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetWorkspaceById), extraCallArguments, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetWorkspaceById), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetWorkspaceById), uri);
                var output = AsanaRequest.GetResponse(response, this, cachedObject);
				ObjectCache.Set(cachePath, output);
				return output;
			}

			// just returns cached response
			public AsanaObjectCollection<AsanaUser> GetUsersInWorkspaceCachedOrNull(AsanaWorkspace arg1)
			{
				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetUsersInWorkspace), arg1); 
				
                AsanaObjectCollection<AsanaUser> cachedObject = (AsanaObjectCollection<AsanaUser>) ObjectCache.Get(cachePath);
                if (cachedObject != null && cachedObject.Initialized)
                    return cachedObject;
				return null;
			}
			// real one:
			public async Task<AsanaObjectCollection<AsanaUser>> GetUsersInWorkspace(AsanaWorkspace arg1, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default, Dictionary<string, object> extraCallArguments = null)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetUsersInWorkspace), arg1); 
				
                AsanaObjectCollection<AsanaUser> cachedObject = null;
                
                cachedObject = (AsanaObjectCollection<AsanaUser>) ObjectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if ((cachedObject != null && cachedObject.Initialized) || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
                if (extraCallArguments == null)
                    extraCallArguments = new Dictionary<string, object>();
                if (optFields != null)
                    extraCallArguments.Add("opt_fields", optFields);
			    Uri uri;
                if (extraCallArguments.Count > 0)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetUsersInWorkspace), extraCallArguments, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetUsersInWorkspace), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetUsersInWorkspace), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, cacheLevel >= AsanaCacheLevel.FillExisting);
                //even if empty, we fetched it and there's nothing - we can cache it next time
                output.Initialized = true;
				ObjectCache.Set(cachePath, output);
				return output;
			}

			// just returns cached response
			public AsanaObjectCollection<AsanaTask> GetMyTasksCachedOrNull(AsanaWorkspace arg1)
			{
				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetMyTasks), arg1); 
				
                AsanaObjectCollection<AsanaTask> cachedObject = (AsanaObjectCollection<AsanaTask>) ObjectCache.Get(cachePath);
                if (cachedObject != null && cachedObject.Initialized)
                    return cachedObject;
				return null;
			}
			// real one:
			public async Task<AsanaObjectCollection<AsanaTask>> GetMyTasks(AsanaWorkspace arg1, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default, Dictionary<string, object> extraCallArguments = null)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetMyTasks), arg1); 
				
                AsanaObjectCollection<AsanaTask> cachedObject = null;
                
                cachedObject = (AsanaObjectCollection<AsanaTask>) ObjectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if ((cachedObject != null && cachedObject.Initialized) || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
                if (extraCallArguments == null)
                    extraCallArguments = new Dictionary<string, object>();
                if (optFields != null)
                    extraCallArguments.Add("opt_fields", optFields);
			    Uri uri;
                if (extraCallArguments.Count > 0)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetMyTasks), extraCallArguments, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetMyTasks), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetMyTasks), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, cacheLevel >= AsanaCacheLevel.FillExisting);
                //even if empty, we fetched it and there's nothing - we can cache it next time
                output.Initialized = true;
				ObjectCache.Set(cachePath, output);
				return output;
			}

			// just returns cached response
			public AsanaObjectCollection<AsanaTask> GetTasksInWorkspaceCachedOrNull(AsanaWorkspace arg1,  AsanaUser arg2)
			{
				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetTasksInWorkspace), arg1, arg2); 
				
                AsanaObjectCollection<AsanaTask> cachedObject = (AsanaObjectCollection<AsanaTask>) ObjectCache.Get(cachePath);
                if (cachedObject != null && cachedObject.Initialized)
                    return cachedObject;
				return null;
			}
			// real one:
			public async Task<AsanaObjectCollection<AsanaTask>> GetTasksInWorkspace(AsanaWorkspace arg1,  AsanaUser arg2, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default, Dictionary<string, object> extraCallArguments = null)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetTasksInWorkspace), arg1, arg2); 
				
                AsanaObjectCollection<AsanaTask> cachedObject = null;
                
                cachedObject = (AsanaObjectCollection<AsanaTask>) ObjectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if ((cachedObject != null && cachedObject.Initialized) || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
                if (extraCallArguments == null)
                    extraCallArguments = new Dictionary<string, object>();
                if (optFields != null)
                    extraCallArguments.Add("opt_fields", optFields);
			    Uri uri;
                if (extraCallArguments.Count > 0)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetTasksInWorkspace), extraCallArguments, arg1, arg2);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetTasksInWorkspace), arg1, arg2);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetTasksInWorkspace), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, cacheLevel >= AsanaCacheLevel.FillExisting);
                //even if empty, we fetched it and there's nothing - we can cache it next time
                output.Initialized = true;
				ObjectCache.Set(cachePath, output);
				return output;
			}

			// just returns cached response
			public AsanaObjectCollection<AsanaProject> GetProjectsInWorkspaceCachedOrNull(AsanaWorkspace arg1)
			{
				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetProjectsInWorkspace), arg1); 
				
                AsanaObjectCollection<AsanaProject> cachedObject = (AsanaObjectCollection<AsanaProject>) ObjectCache.Get(cachePath);
                if (cachedObject != null && cachedObject.Initialized)
                    return cachedObject;
				return null;
			}
			// real one:
			public async Task<AsanaObjectCollection<AsanaProject>> GetProjectsInWorkspace(AsanaWorkspace arg1, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default, Dictionary<string, object> extraCallArguments = null)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetProjectsInWorkspace), arg1); 
				
                AsanaObjectCollection<AsanaProject> cachedObject = null;
                
                cachedObject = (AsanaObjectCollection<AsanaProject>) ObjectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if ((cachedObject != null && cachedObject.Initialized) || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
                if (extraCallArguments == null)
                    extraCallArguments = new Dictionary<string, object>();
                if (optFields != null)
                    extraCallArguments.Add("opt_fields", optFields);
			    Uri uri;
                if (extraCallArguments.Count > 0)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetProjectsInWorkspace), extraCallArguments, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetProjectsInWorkspace), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetProjectsInWorkspace), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, cacheLevel >= AsanaCacheLevel.FillExisting);
                //even if empty, we fetched it and there's nothing - we can cache it next time
                output.Initialized = true;
				ObjectCache.Set(cachePath, output);
				return output;
			}

			// just returns cached response
			public AsanaObjectCollection<AsanaTag> GetTagsInWorkspaceCachedOrNull(AsanaWorkspace arg1)
			{
				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetTagsInWorkspace), arg1); 
				
                AsanaObjectCollection<AsanaTag> cachedObject = (AsanaObjectCollection<AsanaTag>) ObjectCache.Get(cachePath);
                if (cachedObject != null && cachedObject.Initialized)
                    return cachedObject;
				return null;
			}
			// real one:
			public async Task<AsanaObjectCollection<AsanaTag>> GetTagsInWorkspace(AsanaWorkspace arg1, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default, Dictionary<string, object> extraCallArguments = null)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetTagsInWorkspace), arg1); 
				
                AsanaObjectCollection<AsanaTag> cachedObject = null;
                
                cachedObject = (AsanaObjectCollection<AsanaTag>) ObjectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if ((cachedObject != null && cachedObject.Initialized) || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
                if (extraCallArguments == null)
                    extraCallArguments = new Dictionary<string, object>();
                if (optFields != null)
                    extraCallArguments.Add("opt_fields", optFields);
			    Uri uri;
                if (extraCallArguments.Count > 0)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetTagsInWorkspace), extraCallArguments, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetTagsInWorkspace), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetTagsInWorkspace), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, cacheLevel >= AsanaCacheLevel.FillExisting);
                //even if empty, we fetched it and there's nothing - we can cache it next time
                output.Initialized = true;
				ObjectCache.Set(cachePath, output);
				return output;
			}

			// just returns cached response
			public AsanaTask GetTaskByIdCachedOrNull(Int64 arg1)
			{
				string cachePath = string.Format(new PropertyFormatProvider(), "{0}", arg1);
                AsanaTask cachedObject = (AsanaTask) ObjectCache.Get(cachePath);
                if (cachedObject != null)
                    return cachedObject;
				return null;
			}
			// real one:
			public async Task<AsanaTask> GetTaskById(Int64 arg1, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default, Dictionary<string, object> extraCallArguments = null)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = string.Format(new PropertyFormatProvider(), "{0}", arg1);
                AsanaTask cachedObject = null;
                if (cacheLevel >= AsanaCacheLevel.FillExisting)
                cachedObject = (AsanaTask) ObjectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if ((cachedObject != null) || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
                if (extraCallArguments == null)
                    extraCallArguments = new Dictionary<string, object>();
                if (optFields != null)
                    extraCallArguments.Add("opt_fields", optFields);
			    Uri uri;
                if (extraCallArguments.Count > 0)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetTaskById), extraCallArguments, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetTaskById), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetTaskById), uri);
                var output = AsanaRequest.GetResponse(response, this, cachedObject);
				ObjectCache.Set(cachePath, output);
				return output;
			}

			// just returns cached response
			public AsanaObjectCollection<AsanaTask> GetSubtasksInTaskCachedOrNull(AsanaTask arg1)
			{
				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetSubtasksInTask), arg1); 
				
                AsanaObjectCollection<AsanaTask> cachedObject = (AsanaObjectCollection<AsanaTask>) ObjectCache.Get(cachePath);
                if (cachedObject != null && cachedObject.Initialized)
                    return cachedObject;
				return null;
			}
			// real one:
			public async Task<AsanaObjectCollection<AsanaTask>> GetSubtasksInTask(AsanaTask arg1, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default, Dictionary<string, object> extraCallArguments = null)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetSubtasksInTask), arg1); 
				
                AsanaObjectCollection<AsanaTask> cachedObject = null;
                
                cachedObject = (AsanaObjectCollection<AsanaTask>) ObjectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if ((cachedObject != null && cachedObject.Initialized) || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
                if (extraCallArguments == null)
                    extraCallArguments = new Dictionary<string, object>();
                if (optFields != null)
                    extraCallArguments.Add("opt_fields", optFields);
			    Uri uri;
                if (extraCallArguments.Count > 0)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetSubtasksInTask), extraCallArguments, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetSubtasksInTask), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetSubtasksInTask), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, cacheLevel >= AsanaCacheLevel.FillExisting);
                //even if empty, we fetched it and there's nothing - we can cache it next time
                output.Initialized = true;
				ObjectCache.Set(cachePath, output);
				return output;
			}

			// just returns cached response
			public AsanaObjectCollection<AsanaStory> GetStoriesInTaskCachedOrNull(AsanaTask arg1)
			{
				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetStoriesInTask), arg1); 
				
                AsanaObjectCollection<AsanaStory> cachedObject = (AsanaObjectCollection<AsanaStory>) ObjectCache.Get(cachePath);
                if (cachedObject != null && cachedObject.Initialized)
                    return cachedObject;
				return null;
			}
			// real one:
			public async Task<AsanaObjectCollection<AsanaStory>> GetStoriesInTask(AsanaTask arg1, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default, Dictionary<string, object> extraCallArguments = null)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetStoriesInTask), arg1); 
				
                AsanaObjectCollection<AsanaStory> cachedObject = null;
                
                cachedObject = (AsanaObjectCollection<AsanaStory>) ObjectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if ((cachedObject != null && cachedObject.Initialized) || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
                if (extraCallArguments == null)
                    extraCallArguments = new Dictionary<string, object>();
                if (optFields != null)
                    extraCallArguments.Add("opt_fields", optFields);
			    Uri uri;
                if (extraCallArguments.Count > 0)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetStoriesInTask), extraCallArguments, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetStoriesInTask), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetStoriesInTask), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, cacheLevel >= AsanaCacheLevel.FillExisting);
                //even if empty, we fetched it and there's nothing - we can cache it next time
                output.Initialized = true;
				ObjectCache.Set(cachePath, output);
				return output;
			}

			// just returns cached response
			public AsanaObjectCollection<AsanaProject> GetProjectsOnATaskCachedOrNull(AsanaTask arg1)
			{
				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetProjectsOnATask), arg1); 
				
                AsanaObjectCollection<AsanaProject> cachedObject = (AsanaObjectCollection<AsanaProject>) ObjectCache.Get(cachePath);
                if (cachedObject != null && cachedObject.Initialized)
                    return cachedObject;
				return null;
			}
			// real one:
			public async Task<AsanaObjectCollection<AsanaProject>> GetProjectsOnATask(AsanaTask arg1, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default, Dictionary<string, object> extraCallArguments = null)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetProjectsOnATask), arg1); 
				
                AsanaObjectCollection<AsanaProject> cachedObject = null;
                
                cachedObject = (AsanaObjectCollection<AsanaProject>) ObjectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if ((cachedObject != null && cachedObject.Initialized) || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
                if (extraCallArguments == null)
                    extraCallArguments = new Dictionary<string, object>();
                if (optFields != null)
                    extraCallArguments.Add("opt_fields", optFields);
			    Uri uri;
                if (extraCallArguments.Count > 0)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetProjectsOnATask), extraCallArguments, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetProjectsOnATask), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetProjectsOnATask), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, cacheLevel >= AsanaCacheLevel.FillExisting);
                //even if empty, we fetched it and there's nothing - we can cache it next time
                output.Initialized = true;
				ObjectCache.Set(cachePath, output);
				return output;
			}

			// just returns cached response
			public AsanaObjectCollection<AsanaTag> GetTagsOnATaskCachedOrNull(AsanaTask arg1)
			{
				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetTagsOnATask), arg1); 
				
                AsanaObjectCollection<AsanaTag> cachedObject = (AsanaObjectCollection<AsanaTag>) ObjectCache.Get(cachePath);
                if (cachedObject != null && cachedObject.Initialized)
                    return cachedObject;
				return null;
			}
			// real one:
			public async Task<AsanaObjectCollection<AsanaTag>> GetTagsOnATask(AsanaTask arg1, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default, Dictionary<string, object> extraCallArguments = null)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetTagsOnATask), arg1); 
				
                AsanaObjectCollection<AsanaTag> cachedObject = null;
                
                cachedObject = (AsanaObjectCollection<AsanaTag>) ObjectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if ((cachedObject != null && cachedObject.Initialized) || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
                if (extraCallArguments == null)
                    extraCallArguments = new Dictionary<string, object>();
                if (optFields != null)
                    extraCallArguments.Add("opt_fields", optFields);
			    Uri uri;
                if (extraCallArguments.Count > 0)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetTagsOnATask), extraCallArguments, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetTagsOnATask), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetTagsOnATask), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, cacheLevel >= AsanaCacheLevel.FillExisting);
                //even if empty, we fetched it and there's nothing - we can cache it next time
                output.Initialized = true;
				ObjectCache.Set(cachePath, output);
				return output;
			}

			// just returns cached response
			public AsanaObjectCollection<AsanaTask> GetTasksByTagCachedOrNull(AsanaTag arg1)
			{
				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetTasksByTag), arg1); 
				
                AsanaObjectCollection<AsanaTask> cachedObject = (AsanaObjectCollection<AsanaTask>) ObjectCache.Get(cachePath);
                if (cachedObject != null && cachedObject.Initialized)
                    return cachedObject;
				return null;
			}
			// real one:
			public async Task<AsanaObjectCollection<AsanaTask>> GetTasksByTag(AsanaTag arg1, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default, Dictionary<string, object> extraCallArguments = null)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetTasksByTag), arg1); 
				
                AsanaObjectCollection<AsanaTask> cachedObject = null;
                
                cachedObject = (AsanaObjectCollection<AsanaTask>) ObjectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if ((cachedObject != null && cachedObject.Initialized) || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
                if (extraCallArguments == null)
                    extraCallArguments = new Dictionary<string, object>();
                if (optFields != null)
                    extraCallArguments.Add("opt_fields", optFields);
			    Uri uri;
                if (extraCallArguments.Count > 0)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetTasksByTag), extraCallArguments, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetTasksByTag), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetTasksByTag), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, cacheLevel >= AsanaCacheLevel.FillExisting);
                //even if empty, we fetched it and there's nothing - we can cache it next time
                output.Initialized = true;
				ObjectCache.Set(cachePath, output);
				return output;
			}

			// just returns cached response
			public AsanaStory GetStoryByIdCachedOrNull(Int64 arg1)
			{
				string cachePath = string.Format(new PropertyFormatProvider(), "{0}", arg1);
                AsanaStory cachedObject = (AsanaStory) ObjectCache.Get(cachePath);
                if (cachedObject != null)
                    return cachedObject;
				return null;
			}
			// real one:
			public async Task<AsanaStory> GetStoryById(Int64 arg1, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default, Dictionary<string, object> extraCallArguments = null)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = string.Format(new PropertyFormatProvider(), "{0}", arg1);
                AsanaStory cachedObject = null;
                if (cacheLevel >= AsanaCacheLevel.FillExisting)
                cachedObject = (AsanaStory) ObjectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if ((cachedObject != null) || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
                if (extraCallArguments == null)
                    extraCallArguments = new Dictionary<string, object>();
                if (optFields != null)
                    extraCallArguments.Add("opt_fields", optFields);
			    Uri uri;
                if (extraCallArguments.Count > 0)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetStoryById), extraCallArguments, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetStoryById), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetStoryById), uri);
                var output = AsanaRequest.GetResponse(response, this, cachedObject);
				ObjectCache.Set(cachePath, output);
				return output;
			}

			// just returns cached response
			public AsanaProject GetProjectByIdCachedOrNull(Int64 arg1)
			{
				string cachePath = string.Format(new PropertyFormatProvider(), "{0}", arg1);
                AsanaProject cachedObject = (AsanaProject) ObjectCache.Get(cachePath);
                if (cachedObject != null)
                    return cachedObject;
				return null;
			}
			// real one:
			public async Task<AsanaProject> GetProjectById(Int64 arg1, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default, Dictionary<string, object> extraCallArguments = null)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = string.Format(new PropertyFormatProvider(), "{0}", arg1);
                AsanaProject cachedObject = null;
                if (cacheLevel >= AsanaCacheLevel.FillExisting)
                cachedObject = (AsanaProject) ObjectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if ((cachedObject != null) || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
                if (extraCallArguments == null)
                    extraCallArguments = new Dictionary<string, object>();
                if (optFields != null)
                    extraCallArguments.Add("opt_fields", optFields);
			    Uri uri;
                if (extraCallArguments.Count > 0)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetProjectById), extraCallArguments, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetProjectById), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetProjectById), uri);
                var output = AsanaRequest.GetResponse(response, this, cachedObject);
				ObjectCache.Set(cachePath, output);
				return output;
			}

			// just returns cached response
			public AsanaObjectCollection<AsanaTask> GetTasksInAProjectCachedOrNull(AsanaProject arg1)
			{
				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetTasksInAProject), arg1); 
				
                AsanaObjectCollection<AsanaTask> cachedObject = (AsanaObjectCollection<AsanaTask>) ObjectCache.Get(cachePath);
                if (cachedObject != null && cachedObject.Initialized)
                    return cachedObject;
				return null;
			}
			// real one:
			public async Task<AsanaObjectCollection<AsanaTask>> GetTasksInAProject(AsanaProject arg1, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default, Dictionary<string, object> extraCallArguments = null)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetTasksInAProject), arg1); 
				
                AsanaObjectCollection<AsanaTask> cachedObject = null;
                
                cachedObject = (AsanaObjectCollection<AsanaTask>) ObjectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if ((cachedObject != null && cachedObject.Initialized) || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
                if (extraCallArguments == null)
                    extraCallArguments = new Dictionary<string, object>();
                if (optFields != null)
                    extraCallArguments.Add("opt_fields", optFields);
			    Uri uri;
                if (extraCallArguments.Count > 0)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetTasksInAProject), extraCallArguments, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetTasksInAProject), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetTasksInAProject), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, cacheLevel >= AsanaCacheLevel.FillExisting);
                //even if empty, we fetched it and there's nothing - we can cache it next time
                output.Initialized = true;
				ObjectCache.Set(cachePath, output);
				return output;
			}

			// just returns cached response
			public AsanaTag GetTagByIdCachedOrNull(Int64 arg1)
			{
				string cachePath = string.Format(new PropertyFormatProvider(), "{0}", arg1);
                AsanaTag cachedObject = (AsanaTag) ObjectCache.Get(cachePath);
                if (cachedObject != null)
                    return cachedObject;
				return null;
			}
			// real one:
			public async Task<AsanaTag> GetTagById(Int64 arg1, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default, Dictionary<string, object> extraCallArguments = null)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = string.Format(new PropertyFormatProvider(), "{0}", arg1);
                AsanaTag cachedObject = null;
                if (cacheLevel >= AsanaCacheLevel.FillExisting)
                cachedObject = (AsanaTag) ObjectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if ((cachedObject != null) || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
                if (extraCallArguments == null)
                    extraCallArguments = new Dictionary<string, object>();
                if (optFields != null)
                    extraCallArguments.Add("opt_fields", optFields);
			    Uri uri;
                if (extraCallArguments.Count > 0)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetTagById), extraCallArguments, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetTagById), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetTagById), uri);
                var output = AsanaRequest.GetResponse(response, this, cachedObject);
				ObjectCache.Set(cachePath, output);
				return output;
			}

			// just returns cached response
			public AsanaObjectCollection<AsanaTeam> GetTeamsInWorkspaceCachedOrNull(AsanaWorkspace arg1)
			{
				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetTeamsInWorkspace), arg1); 
				
                AsanaObjectCollection<AsanaTeam> cachedObject = (AsanaObjectCollection<AsanaTeam>) ObjectCache.Get(cachePath);
                if (cachedObject != null && cachedObject.Initialized)
                    return cachedObject;
				return null;
			}
			// real one:
			public async Task<AsanaObjectCollection<AsanaTeam>> GetTeamsInWorkspace(AsanaWorkspace arg1, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default, Dictionary<string, object> extraCallArguments = null)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetTeamsInWorkspace), arg1); 
				
                AsanaObjectCollection<AsanaTeam> cachedObject = null;
                
                cachedObject = (AsanaObjectCollection<AsanaTeam>) ObjectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if ((cachedObject != null && cachedObject.Initialized) || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
                if (extraCallArguments == null)
                    extraCallArguments = new Dictionary<string, object>();
                if (optFields != null)
                    extraCallArguments.Add("opt_fields", optFields);
			    Uri uri;
                if (extraCallArguments.Count > 0)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetTeamsInWorkspace), extraCallArguments, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetTeamsInWorkspace), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetTeamsInWorkspace), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, cacheLevel >= AsanaCacheLevel.FillExisting);
                //even if empty, we fetched it and there's nothing - we can cache it next time
                output.Initialized = true;
				ObjectCache.Set(cachePath, output);
				return output;
			}

			// just returns cached response
			public AsanaTeam GetTeamByIdCachedOrNull(Int64 arg1)
			{
				string cachePath = string.Format(new PropertyFormatProvider(), "{0}", arg1);
                AsanaTeam cachedObject = (AsanaTeam) ObjectCache.Get(cachePath);
                if (cachedObject != null)
                    return cachedObject;
				return null;
			}
			// real one:
			public async Task<AsanaTeam> GetTeamById(Int64 arg1, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default, Dictionary<string, object> extraCallArguments = null)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = string.Format(new PropertyFormatProvider(), "{0}", arg1);
                AsanaTeam cachedObject = null;
                if (cacheLevel >= AsanaCacheLevel.FillExisting)
                cachedObject = (AsanaTeam) ObjectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if ((cachedObject != null) || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
                if (extraCallArguments == null)
                    extraCallArguments = new Dictionary<string, object>();
                if (optFields != null)
                    extraCallArguments.Add("opt_fields", optFields);
			    Uri uri;
                if (extraCallArguments.Count > 0)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetTeamById), extraCallArguments, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetTeamById), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetTeamById), uri);
                var output = AsanaRequest.GetResponse(response, this, cachedObject);
				ObjectCache.Set(cachePath, output);
				return output;
			}

			// just returns cached response
			public AsanaEventList GetEventsCachedOrNull(AsanaEventedObject arg1,  string arg2)
			{
				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetEvents), arg1, arg2); 
				
                AsanaEventList cachedObject = (AsanaEventList) ObjectCache.Get(cachePath);
                if (cachedObject != null)
                    return cachedObject;
				return null;
			}
			// real one:
			public async Task<AsanaEventList> GetEvents(AsanaEventedObject arg1,  string arg2, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default, Dictionary<string, object> extraCallArguments = null)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetEvents), arg1, arg2); 
				
                AsanaEventList cachedObject = null;
                if (cacheLevel >= AsanaCacheLevel.FillExisting)
                cachedObject = (AsanaEventList) ObjectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if ((cachedObject != null) || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
                if (extraCallArguments == null)
                    extraCallArguments = new Dictionary<string, object>();
                if (optFields != null)
                    extraCallArguments.Add("opt_fields", optFields);
			    Uri uri;
                if (extraCallArguments.Count > 0)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetEvents), extraCallArguments, arg1, arg2);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetEvents), arg1, arg2);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetEvents), uri);
                var output = AsanaRequest.GetResponse(response, this, cachedObject);
				ObjectCache.Set(cachePath, output);
				return output;
			}

		}

		// Binds the enums, formations and methods
		public partial class AsanaFunction
		{
			static internal void InitFunctions()
			{
				Functions.Add(Function.GetUsers, new AsanaFunction("/users", "GET"));
				Functions.Add(Function.GetMe, new AsanaFunction("/users/me", "GET"));
				Functions.Add(Function.GetUserById, new AsanaFunction("/users/{0}", "GET"));
				Functions.Add(Function.GetWorkspaces, new AsanaFunction("/workspaces", "GET"));
				Functions.Add(Function.GetWorkspaceById, new AsanaFunction("/workspaces/{0}", "GET"));
				Functions.Add(Function.GetUsersInWorkspace, new AsanaFunction("/workspaces/{0:ID}/users", "GET"));
				Functions.Add(Function.GetMyTasks, new AsanaFunction("/workspaces/{0:ID}/tasks?assignee=me", "GET"));
				Functions.Add(Function.GetTasksInWorkspace, new AsanaFunction("/workspaces/{0:ID}/tasks?assignee={1:ID}", "GET"));
				Functions.Add(Function.GetProjectsInWorkspace, new AsanaFunction("/workspaces/{0:ID}/projects", "GET"));
				Functions.Add(Function.GetTagsInWorkspace, new AsanaFunction("/workspaces/{0:ID}/tags", "GET"));
				Functions.Add(Function.GetTaskById, new AsanaFunction("/tasks/{0}", "GET"));
				Functions.Add(Function.GetSubtasksInTask, new AsanaFunction("/tasks/{0:ID}/subtasks", "GET"));
				Functions.Add(Function.GetStoriesInTask, new AsanaFunction("/tasks/{0:ID}/stories", "GET"));
				Functions.Add(Function.GetProjectsOnATask, new AsanaFunction("/tasks/{0:ID}/projects", "GET"));
				Functions.Add(Function.GetTagsOnATask, new AsanaFunction("/tasks/{0:ID}/tags", "GET"));
				Functions.Add(Function.GetTasksByTag, new AsanaFunction("/tags/{0:ID}/tasks", "GET"));
				Functions.Add(Function.GetStoryById, new AsanaFunction("/stories/{0}", "GET"));
				Functions.Add(Function.GetProjectById, new AsanaFunction("/projects/{0}", "GET"));
				Functions.Add(Function.GetTasksInAProject, new AsanaFunction("/projects/{0:ID}/tasks", "GET"));
				Functions.Add(Function.GetTagById, new AsanaFunction("/tags/{0}", "GET"));
				Functions.Add(Function.GetTeamsInWorkspace, new AsanaFunction("/organizations/{0:ID}/teams", "GET"));
				Functions.Add(Function.GetTeamById, new AsanaFunction("/teams/{0}", "GET"));
				Functions.Add(Function.GetEvents, new AsanaFunction("/events?resources={0:ID}&sync={1}", "GET"));
				Functions.Add(Function.CreateTask, new AsanaFunction("/tasks", "POST"));
				Functions.Add(Function.CreateProject, new AsanaFunction("/projects", "POST"));
				Functions.Add(Function.CreateTag, new AsanaFunction("/tags", "POST"));
				Functions.Add(Function.CreateStory, new AsanaFunction("/tasks/{0:Target}/stories", "POST"));
				Functions.Add(Function.CreateSubtask, new AsanaFunction("/tasks/{0:Parent}/subtasks", "POST"));
				Functions.Add(Function.SetParent, new AsanaFunction("/tasks/{0:ID}/setParent", "POST"));
				Functions.Add(Function.AddProject, new AsanaFunction("/tasks/{0:ID}/addProject", "POST"));
				Functions.Add(Function.RemoveProject, new AsanaFunction("/tasks/{0:ID}/removeProject", "POST"));
				Functions.Add(Function.AddTag, new AsanaFunction("/tasks/{0:ID}/addTag", "POST"));
				Functions.Add(Function.RemoveTag, new AsanaFunction("/tasks/{0:ID}/removeTag", "POST"));
				Functions.Add(Function.AddFollowers, new AsanaFunction("/tasks/{0:ID}/addFollowers", "POST"));
				Functions.Add(Function.RemoveFollowers, new AsanaFunction("/tasks/{0:ID}/removeFollowers", "POST"));
				Functions.Add(Function.UpdateTask, new AsanaFunction("/tasks/{0:ID}", "PUT"));
				Functions.Add(Function.UpdateTag, new AsanaFunction("/tags/{0:ID}", "PUT"));
				Functions.Add(Function.UpdateProject, new AsanaFunction("/projects/{0:ID}", "PUT"));
				Functions.Add(Function.UpdateWorkspace, new AsanaFunction("/workspaces/{0:ID}", "PUT"));
				Functions.Add(Function.DeleteTask, new AsanaFunction("/tasks/{0:ID}", "DELETE"));
				Functions.Add(Function.DeleteProject, new AsanaFunction("/projects/{0:ID}", "DELETE"));
				Functions.Add(Function.DeleteStory, new AsanaFunction("/projects/{0:ID}", "DELETE"));
				Associations.Add(typeof(AsanaWorkspace), new AsanaFunctionAssociation(null, GetFunction(Function.UpdateWorkspace), null));
				Associations.Add(typeof(AsanaTask), new AsanaFunctionAssociation(GetFunction(Function.CreateTask), GetFunction(Function.UpdateTask), GetFunction(Function.DeleteTask)));
				Associations.Add(typeof(AsanaProject), new AsanaFunctionAssociation(GetFunction(Function.CreateProject), GetFunction(Function.UpdateProject), GetFunction(Function.DeleteProject)));
				Associations.Add(typeof(AsanaTag), new AsanaFunctionAssociation(GetFunction(Function.CreateTag), GetFunction(Function.UpdateTag), null));
				Associations.Add(typeof(AsanaStory), new AsanaFunctionAssociation(GetFunction(Function.CreateStory), null, null));
		
			}
		}


        // add function to the objects

public partial class AsanaUser // : AsanaObject, IAsanaData
{
}

public partial class AsanaUser // : AsanaObject, IAsanaData
{
}

public partial class AsanaUser // : AsanaObject, IAsanaData
{
    public async override Task Refresh(string optFields = null)
    {
        if (!IsObjectLocal && !object.ReferenceEquals(Host, null))
            await Host.GetUserById(this.ID, optFields, AsanaCacheLevel.Ignore);
    }
}

public partial class AsanaWorkspace // : AsanaObject, IAsanaData
{
}

public partial class AsanaWorkspace // : AsanaObject, IAsanaData
{
    public async override Task Refresh(string optFields = null)
    {
        if (!IsObjectLocal && !object.ReferenceEquals(Host, null))
            await Host.GetWorkspaceById(this.ID, optFields, AsanaCacheLevel.Ignore);
    }
}

public partial class AsanaWorkspace // : AsanaObject, IAsanaData
{
    private AsanaObjectCollection<AsanaUser> bufferedUsersObject;

	[AsanaDataAttribute     ("users", SerializationFlags.Omit, int.MaxValue, "ID")]
    public AsanaObjectCollection<AsanaUser> Users
    {
        get 
        {
            if (!ReferenceEquals(bufferedUsersObject, null)) return bufferedUsersObject;
            if (IsObjectLocal) return bufferedUsersObject = new AsanaObjectCollection<AsanaUser>();
            var obj = Host.GetUsersInWorkspaceCachedOrNull(this);
            if (obj != null) return obj;
			// lazy initialization + caching
            Users = bufferedUsersObject = new AsanaObjectCollection<AsanaUser>();
            return bufferedUsersObject;
        }
		private set 
		{
            if (!ReferenceEquals(value, null))
            {
			    string cachePath = Asana.GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetUsersInWorkspace), this);
			    Host.ObjectCache.Set(cachePath, value);
            }
		}
    }
    public Task<AsanaObjectCollection<AsanaUser>> GetUsers(string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default, Dictionary<string, object> extraCallArguments = null)
    {
        if (!IsObjectLocal && !object.ReferenceEquals(Host, null))
        {
            var task = Host.GetUsersInWorkspace(this, optFields, cacheLevel, extraCallArguments);
            return task;
        }
        return null;
    }
}

public partial class AsanaWorkspace // : AsanaObject, IAsanaData
{
    private AsanaObjectCollection<AsanaTask> bufferedTasksObject;

	[AsanaDataAttribute     ("tasks", SerializationFlags.Omit, int.MaxValue, "ID")]
    public AsanaObjectCollection<AsanaTask> Tasks
    {
        get 
        {
            if (!ReferenceEquals(bufferedTasksObject, null)) return bufferedTasksObject;
            if (IsObjectLocal) return bufferedTasksObject = new AsanaObjectCollection<AsanaTask>();
            var obj = Host.GetMyTasksCachedOrNull(this);
            if (obj != null) return obj;
			// lazy initialization + caching
            Tasks = bufferedTasksObject = new AsanaObjectCollection<AsanaTask>();
            return bufferedTasksObject;
        }
		private set 
		{
            if (!ReferenceEquals(value, null))
            {
                if (!ReferenceEquals(this, null))
                {
                    foreach (var obj in value)
                    {
                        obj.Workspace = this;
                    }
                }
			    string cachePath = Asana.GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetMyTasks), this);
			    Host.ObjectCache.Set(cachePath, value);
            }
		}
    }
    public Task<AsanaObjectCollection<AsanaTask>> GetTasks(string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default, Dictionary<string, object> extraCallArguments = null)
    {
        if (!IsObjectLocal && !object.ReferenceEquals(Host, null))
        {
            var task = Host.GetMyTasks(this, optFields, cacheLevel, extraCallArguments);
            task.ContinueWith(
                async prevTask =>
                {
                    if (cacheLevel < AsanaCacheLevel.UseExistingOrNull)
                        foreach (var obj in await prevTask)
                        {
                            obj._workspace = this;
                        }
                }, TaskContinuationOptions.NotOnFaulted);
            return task;
        }
        return null;
    }
}

public partial class AsanaWorkspace // : AsanaObject, IAsanaData
{
    public Task<AsanaObjectCollection<AsanaTask>> GetTasks( AsanaUser arg2, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default, Dictionary<string, object> extraCallArguments = null)
    {
        if (!IsObjectLocal && !object.ReferenceEquals(Host, null))
        {
            var task = Host.GetTasksInWorkspace(this, arg2, optFields, cacheLevel, extraCallArguments);
            task.ContinueWith(
                async prevTask =>
                {
                    if (cacheLevel < AsanaCacheLevel.UseExistingOrNull)
                        foreach (var obj in await prevTask)
                        {
                            obj._workspace = this;
                        }
                }, TaskContinuationOptions.NotOnFaulted);
            return task;
        }
        return null;
    }
}

public partial class AsanaWorkspace // : AsanaObject, IAsanaData
{
    private AsanaObjectCollection<AsanaProject> bufferedProjectsObject;

	[AsanaDataAttribute     ("projects", SerializationFlags.Omit, int.MaxValue, "ID")]
    public AsanaObjectCollection<AsanaProject> Projects
    {
        get 
        {
            if (!ReferenceEquals(bufferedProjectsObject, null)) return bufferedProjectsObject;
            if (IsObjectLocal) return bufferedProjectsObject = new AsanaObjectCollection<AsanaProject>();
            var obj = Host.GetProjectsInWorkspaceCachedOrNull(this);
            if (obj != null) return obj;
			// lazy initialization + caching
            Projects = bufferedProjectsObject = new AsanaObjectCollection<AsanaProject>();
            return bufferedProjectsObject;
        }
		private set 
		{
            if (!ReferenceEquals(value, null))
            {
                if (!ReferenceEquals(this, null))
                {
                    foreach (var obj in value)
                    {
                        obj.Workspace = this;
                    }
                }
			    string cachePath = Asana.GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetProjectsInWorkspace), this);
			    Host.ObjectCache.Set(cachePath, value);
            }
		}
    }
    public Task<AsanaObjectCollection<AsanaProject>> GetProjects(string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default, Dictionary<string, object> extraCallArguments = null)
    {
        if (!IsObjectLocal && !object.ReferenceEquals(Host, null))
        {
            var task = Host.GetProjectsInWorkspace(this, optFields, cacheLevel, extraCallArguments);
            task.ContinueWith(
                async prevTask =>
                {
                    if (cacheLevel < AsanaCacheLevel.UseExistingOrNull)
                        foreach (var obj in await prevTask)
                        {
                            obj._workspace = this;
                        }
                }, TaskContinuationOptions.NotOnFaulted);
            return task;
        }
        return null;
    }
}

public partial class AsanaWorkspace // : AsanaObject, IAsanaData
{
    private AsanaObjectCollection<AsanaTag> bufferedTagsObject;

	[AsanaDataAttribute     ("tags", SerializationFlags.Omit, int.MaxValue, "ID")]
    public AsanaObjectCollection<AsanaTag> Tags
    {
        get 
        {
            if (!ReferenceEquals(bufferedTagsObject, null)) return bufferedTagsObject;
            if (IsObjectLocal) return bufferedTagsObject = new AsanaObjectCollection<AsanaTag>();
            var obj = Host.GetTagsInWorkspaceCachedOrNull(this);
            if (obj != null) return obj;
			// lazy initialization + caching
            Tags = bufferedTagsObject = new AsanaObjectCollection<AsanaTag>();
            return bufferedTagsObject;
        }
		private set 
		{
            if (!ReferenceEquals(value, null))
            {
                if (!ReferenceEquals(this, null))
                {
                    foreach (var obj in value)
                    {
                        obj.Workspace = this;
                    }
                }
			    string cachePath = Asana.GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetTagsInWorkspace), this);
			    Host.ObjectCache.Set(cachePath, value);
            }
		}
    }
    public Task<AsanaObjectCollection<AsanaTag>> GetTags(string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default, Dictionary<string, object> extraCallArguments = null)
    {
        if (!IsObjectLocal && !object.ReferenceEquals(Host, null))
        {
            var task = Host.GetTagsInWorkspace(this, optFields, cacheLevel, extraCallArguments);
            task.ContinueWith(
                async prevTask =>
                {
                    if (cacheLevel < AsanaCacheLevel.UseExistingOrNull)
                        foreach (var obj in await prevTask)
                        {
                            obj._workspace = this;
                        }
                }, TaskContinuationOptions.NotOnFaulted);
            return task;
        }
        return null;
    }
}

public partial class AsanaTask // : AsanaObject, IAsanaData
{
    public async override Task Refresh(string optFields = null)
    {
        if (!IsObjectLocal && !object.ReferenceEquals(Host, null))
            await Host.GetTaskById(this.ID, optFields, AsanaCacheLevel.Ignore);
    }
}

public partial class AsanaTask // : AsanaObject, IAsanaData
{
    private AsanaObjectCollection<AsanaTask> bufferedTasksObject;

	[AsanaDataAttribute     ("tasks", SerializationFlags.Omit, int.MaxValue, "ID")]
    public AsanaObjectCollection<AsanaTask> Tasks
    {
        get 
        {
            if (!ReferenceEquals(bufferedTasksObject, null)) return bufferedTasksObject;
            if (IsObjectLocal) return bufferedTasksObject = new AsanaObjectCollection<AsanaTask>();
            var obj = Host.GetSubtasksInTaskCachedOrNull(this);
            if (obj != null) return obj;
			// lazy initialization + caching
            Tasks = bufferedTasksObject = new AsanaObjectCollection<AsanaTask>();
            return bufferedTasksObject;
        }
		private set 
		{
            if (!ReferenceEquals(value, null))
            {
                if (!ReferenceEquals(Workspace, null))
                {
                    foreach (var obj in value)
                    {
                        obj.Workspace = Workspace;
                    }
                }
			    string cachePath = Asana.GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetSubtasksInTask), this);
			    Host.ObjectCache.Set(cachePath, value);
            }
		}
    }
    public Task<AsanaObjectCollection<AsanaTask>> GetTasks(string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default, Dictionary<string, object> extraCallArguments = null)
    {
        if (!IsObjectLocal && !object.ReferenceEquals(Host, null))
        {
            var task = Host.GetSubtasksInTask(this, optFields, cacheLevel, extraCallArguments);
            task.ContinueWith(
                async prevTask =>
                {
                    if (cacheLevel < AsanaCacheLevel.UseExistingOrNull)
                        foreach (var obj in await prevTask)
                        {
                            obj._parent = this;
                        }
                }, TaskContinuationOptions.NotOnFaulted);
            return task;
        }
        return null;
    }
}

public partial class AsanaTask // : AsanaObject, IAsanaData
{
    private AsanaObjectCollection<AsanaStory> bufferedStoriesObject;

	[AsanaDataAttribute     ("stories", SerializationFlags.Omit, int.MaxValue, "ID")]
    public AsanaObjectCollection<AsanaStory> Stories
    {
        get 
        {
            if (!ReferenceEquals(bufferedStoriesObject, null)) return bufferedStoriesObject;
            if (IsObjectLocal) return bufferedStoriesObject = new AsanaObjectCollection<AsanaStory>();
            var obj = Host.GetStoriesInTaskCachedOrNull(this);
            if (obj != null) return obj;
			// lazy initialization + caching
            Stories = bufferedStoriesObject = new AsanaObjectCollection<AsanaStory>();
            return bufferedStoriesObject;
        }
		private set 
		{
            if (!ReferenceEquals(value, null))
            {
			    string cachePath = Asana.GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetStoriesInTask), this);
			    Host.ObjectCache.Set(cachePath, value);
            }
		}
    }
    public Task<AsanaObjectCollection<AsanaStory>> GetStories(string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default, Dictionary<string, object> extraCallArguments = null)
    {
        if (!IsObjectLocal && !object.ReferenceEquals(Host, null))
        {
            var task = Host.GetStoriesInTask(this, optFields, cacheLevel, extraCallArguments);
            task.ContinueWith(
                async prevTask =>
                {
                    if (cacheLevel < AsanaCacheLevel.UseExistingOrNull)
                        foreach (var obj in await prevTask)
                        {
                            obj._target = this;
                        }
                }, TaskContinuationOptions.NotOnFaulted);
            return task;
        }
        return null;
    }
}

public partial class AsanaTask // : AsanaObject, IAsanaData
{
    private AsanaObjectCollection<AsanaProject> bufferedProjectsObject;

	[AsanaDataAttribute     ("projects", SerializationFlags.Omit, int.MaxValue, "ID")]
    public AsanaObjectCollection<AsanaProject> Projects
    {
        get 
        {
            if (!ReferenceEquals(bufferedProjectsObject, null)) return bufferedProjectsObject;
            if (IsObjectLocal) return bufferedProjectsObject = new AsanaObjectCollection<AsanaProject>();
            var obj = Host.GetProjectsOnATaskCachedOrNull(this);
            if (obj != null) return obj;
			// lazy initialization + caching
            Projects = bufferedProjectsObject = new AsanaObjectCollection<AsanaProject>();
            return bufferedProjectsObject;
        }
		private set 
		{
            if (!ReferenceEquals(value, null))
            {
                if (!ReferenceEquals(Workspace, null))
                {
                    foreach (var obj in value)
                    {
                        obj.Workspace = Workspace;
                    }
                }
                // accessibleFrom
                foreach (var obj in value)
                {
                    if (!obj.Tasks.Contains(this))
                        obj.Tasks.Add(this);
                }
			    string cachePath = Asana.GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetProjectsOnATask), this);
			    Host.ObjectCache.Set(cachePath, value);
            }
		}
    }
    public Task<AsanaObjectCollection<AsanaProject>> GetProjects(string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default, Dictionary<string, object> extraCallArguments = null)
    {
        if (!IsObjectLocal && !object.ReferenceEquals(Host, null))
        {
            var task = Host.GetProjectsOnATask(this, optFields, cacheLevel, extraCallArguments);
            return task;
        }
        return null;
    }
}

public partial class AsanaTask // : AsanaObject, IAsanaData
{
    private AsanaObjectCollection<AsanaTag> bufferedTagsObject;

	[AsanaDataAttribute     ("tags", SerializationFlags.Omit, int.MaxValue, "ID")]
    public AsanaObjectCollection<AsanaTag> Tags
    {
        get 
        {
            if (!ReferenceEquals(bufferedTagsObject, null)) return bufferedTagsObject;
            if (IsObjectLocal) return bufferedTagsObject = new AsanaObjectCollection<AsanaTag>();
            var obj = Host.GetTagsOnATaskCachedOrNull(this);
            if (obj != null) return obj;
			// lazy initialization + caching
            Tags = bufferedTagsObject = new AsanaObjectCollection<AsanaTag>();
            return bufferedTagsObject;
        }
		private set 
		{
            if (!ReferenceEquals(value, null))
            {
                if (!ReferenceEquals(Workspace, null))
                {
                    foreach (var obj in value)
                    {
                        obj.Workspace = Workspace;
                    }
                }
                // accessibleFrom
                foreach (var obj in value)
                {
                    if (!obj.Tasks.Contains(this))
                        obj.Tasks.Add(this);
                }
			    string cachePath = Asana.GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetTagsOnATask), this);
			    Host.ObjectCache.Set(cachePath, value);
            }
		}
    }
    public Task<AsanaObjectCollection<AsanaTag>> GetTags(string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default, Dictionary<string, object> extraCallArguments = null)
    {
        if (!IsObjectLocal && !object.ReferenceEquals(Host, null))
        {
            var task = Host.GetTagsOnATask(this, optFields, cacheLevel, extraCallArguments);
            return task;
        }
        return null;
    }
}

public partial class AsanaTag // : AsanaObject, IAsanaData
{
    private AsanaObjectCollection<AsanaTask> bufferedTasksObject;

	[AsanaDataAttribute     ("tasks", SerializationFlags.Omit, int.MaxValue, "ID")]
    public AsanaObjectCollection<AsanaTask> Tasks
    {
        get 
        {
            if (!ReferenceEquals(bufferedTasksObject, null)) return bufferedTasksObject;
            if (IsObjectLocal) return bufferedTasksObject = new AsanaObjectCollection<AsanaTask>();
            var obj = Host.GetTasksByTagCachedOrNull(this);
            if (obj != null) return obj;
			// lazy initialization + caching
            Tasks = bufferedTasksObject = new AsanaObjectCollection<AsanaTask>();
            return bufferedTasksObject;
        }
		private set 
		{
            if (!ReferenceEquals(value, null))
            {
                if (!ReferenceEquals(Workspace, null))
                {
                    foreach (var obj in value)
                    {
                        obj.Workspace = Workspace;
                    }
                }
			    string cachePath = Asana.GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetTasksByTag), this);
			    Host.ObjectCache.Set(cachePath, value);
            }
		}
    }
    public Task<AsanaObjectCollection<AsanaTask>> GetTasks(string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default, Dictionary<string, object> extraCallArguments = null)
    {
        if (!IsObjectLocal && !object.ReferenceEquals(Host, null))
        {
            var task = Host.GetTasksByTag(this, optFields, cacheLevel, extraCallArguments);
            return task;
        }
        return null;
    }
}

public partial class AsanaStory // : AsanaObject, IAsanaData
{
    public async override Task Refresh(string optFields = null)
    {
        if (!IsObjectLocal && !object.ReferenceEquals(Host, null))
            await Host.GetStoryById(this.ID, optFields, AsanaCacheLevel.Ignore);
    }
}

public partial class AsanaProject // : AsanaObject, IAsanaData
{
    public async override Task Refresh(string optFields = null)
    {
        if (!IsObjectLocal && !object.ReferenceEquals(Host, null))
            await Host.GetProjectById(this.ID, optFields, AsanaCacheLevel.Ignore);
    }
}

public partial class AsanaProject // : AsanaObject, IAsanaData
{
    private AsanaObjectCollection<AsanaTask> bufferedTasksObject;

	[AsanaDataAttribute     ("tasks", SerializationFlags.Omit, int.MaxValue, "ID")]
    public AsanaObjectCollection<AsanaTask> Tasks
    {
        get 
        {
            if (!ReferenceEquals(bufferedTasksObject, null)) return bufferedTasksObject;
            if (IsObjectLocal) return bufferedTasksObject = new AsanaObjectCollection<AsanaTask>();
            var obj = Host.GetTasksInAProjectCachedOrNull(this);
            if (obj != null) return obj;
			// lazy initialization + caching
            Tasks = bufferedTasksObject = new AsanaObjectCollection<AsanaTask>();
            return bufferedTasksObject;
        }
		private set 
		{
            if (!ReferenceEquals(value, null))
            {
                if (!ReferenceEquals(Workspace, null))
                {
                    foreach (var obj in value)
                    {
                        obj.Workspace = Workspace;
                    }
                }
			    string cachePath = Asana.GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetTasksInAProject), this);
			    Host.ObjectCache.Set(cachePath, value);
            }
		}
    }
    public Task<AsanaObjectCollection<AsanaTask>> GetTasks(string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default, Dictionary<string, object> extraCallArguments = null)
    {
        if (!IsObjectLocal && !object.ReferenceEquals(Host, null))
        {
            var task = Host.GetTasksInAProject(this, optFields, cacheLevel, extraCallArguments);
            return task;
        }
        return null;
    }
}

public partial class AsanaTag // : AsanaObject, IAsanaData
{
    public async override Task Refresh(string optFields = null)
    {
        if (!IsObjectLocal && !object.ReferenceEquals(Host, null))
            await Host.GetTagById(this.ID, optFields, AsanaCacheLevel.Ignore);
    }
}

public partial class AsanaWorkspace // : AsanaObject, IAsanaData
{
    private AsanaObjectCollection<AsanaTeam> bufferedTeamsObject;

	[AsanaDataAttribute     ("teams", SerializationFlags.Omit, int.MaxValue, "ID")]
    public AsanaObjectCollection<AsanaTeam> Teams
    {
        get 
        {
            if (!ReferenceEquals(bufferedTeamsObject, null)) return bufferedTeamsObject;
            if (IsObjectLocal) return bufferedTeamsObject = new AsanaObjectCollection<AsanaTeam>();
            var obj = Host.GetTeamsInWorkspaceCachedOrNull(this);
            if (obj != null) return obj;
			// lazy initialization + caching
            Teams = bufferedTeamsObject = new AsanaObjectCollection<AsanaTeam>();
            return bufferedTeamsObject;
        }
		private set 
		{
            if (!ReferenceEquals(value, null))
            {
                if (!ReferenceEquals(this, null))
                {
                    foreach (var obj in value)
                    {
                        obj.Workspace = this;
                    }
                }
			    string cachePath = Asana.GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetTeamsInWorkspace), this);
			    Host.ObjectCache.Set(cachePath, value);
            }
		}
    }
    public Task<AsanaObjectCollection<AsanaTeam>> GetTeams(string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default, Dictionary<string, object> extraCallArguments = null)
    {
        if (!IsObjectLocal && !object.ReferenceEquals(Host, null))
        {
            var task = Host.GetTeamsInWorkspace(this, optFields, cacheLevel, extraCallArguments);
            task.ContinueWith(
                async prevTask =>
                {
                    if (cacheLevel < AsanaCacheLevel.UseExistingOrNull)
                        foreach (var obj in await prevTask)
                        {
                            obj._organization = this;
                        }
                }, TaskContinuationOptions.NotOnFaulted);
            return task;
        }
        return null;
    }
}

public partial class AsanaTeam // : AsanaObject, IAsanaData
{
    public async override Task Refresh(string optFields = null)
    {
        if (!IsObjectLocal && !object.ReferenceEquals(Host, null))
            await Host.GetTeamById(this.ID, optFields, AsanaCacheLevel.Ignore);
    }
}

public partial class AsanaEventedObject // : AsanaObject, IAsanaData
{
    public Task<AsanaEventList> GetEventLists( string arg2, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default, Dictionary<string, object> extraCallArguments = null)
    {
        if (!IsObjectLocal && !object.ReferenceEquals(Host, null))
        {
            var task = Host.GetEvents(this, arg2, optFields, cacheLevel, extraCallArguments);
            return task;
        }
        return null;
    }
}

public partial class AsanaWorkspace // : AsanaObject, IAsanaData
{
    public Task<AsanaWorkspace> CreateTask(AsanaTask arg)
    {
        arg.Host = Host;
        arg.Workspace = this;
        var task = Host.Save(arg, AsanaFunction.GetFunction(Function.CreateTask));
        task.ContinueWith(
            (prevTask) =>
                {
                    //if (!ReferenceEquals(FetchedTasks, null))
                        if (!FetchedTasks.Contains(arg)) FetchedTasks.Add(arg);
                    //return this;
                }, TaskContinuationOptions.NotOnFaulted);
        return task.ContinueWith(prevTask => this);
    }
}

public partial class AsanaWorkspace // : AsanaObject, IAsanaData
{
    public Task<AsanaWorkspace> CreateProject(AsanaProject arg)
    {
        arg.Host = Host;
        arg.Workspace = this;
        var task = Host.Save(arg, AsanaFunction.GetFunction(Function.CreateProject));
        task.ContinueWith(
            (prevTask) =>
                {
                    //if (!ReferenceEquals(Projects, null))
                        if (!Projects.Contains(arg)) Projects.Add(arg);
                    //return this;
                }, TaskContinuationOptions.NotOnFaulted);
        return task.ContinueWith(prevTask => this);
    }
}

public partial class AsanaWorkspace // : AsanaObject, IAsanaData
{
    public Task<AsanaWorkspace> CreateTag(AsanaTag arg)
    {
        arg.Host = Host;
        arg.Workspace = this;
        var task = Host.Save(arg, AsanaFunction.GetFunction(Function.CreateTag));
        task.ContinueWith(
            (prevTask) =>
                {
                    //if (!ReferenceEquals(Tags, null))
                        if (!Tags.Contains(arg)) Tags.Add(arg);
                    //return this;
                }, TaskContinuationOptions.NotOnFaulted);
        return task.ContinueWith(prevTask => this);
    }
}

public partial class AsanaTask // : AsanaObject, IAsanaData
{
    public Task<AsanaTask> CreateStory(AsanaStory arg)
    {
        arg.Host = Host;
        arg.Target = this;
        var task = Host.Save(arg, AsanaFunction.GetFunction(Function.CreateStory));
        task.ContinueWith(
            (prevTask) =>
                {
                    //if (!ReferenceEquals(Stories, null))
                        if (!Stories.Contains(arg)) Stories.Add(arg);
                    //return this;
                }, TaskContinuationOptions.NotOnFaulted);
        return task.ContinueWith(prevTask => this);
    }
}

public partial class AsanaTask // : AsanaObject, IAsanaData
{
    public Task<AsanaTask> CreateSubtask(AsanaTask arg)
    {
        arg.Host = Host;
        arg.Parent = this;
        var task = Host.Save(arg, AsanaFunction.GetFunction(Function.CreateSubtask));
        task.ContinueWith(
            (prevTask) =>
                {
                    //if (!ReferenceEquals(Tasks, null))
                        if (!Tasks.Contains(arg)) Tasks.Add(arg);
                    //return this;
                }, TaskContinuationOptions.NotOnFaulted);
        return task.ContinueWith(prevTask => this);
    }
}

public partial class AsanaTask // : AsanaObject, IAsanaData
{
    public Task SetParent(AsanaTask arg)
    {
        Dictionary<string, object> param = new Dictionary<string, object> {{"parent", arg.ID}};
        return Host.Save(this, AsanaFunction.GetFunction(Function.SetParent), param);
        //Dictionary<string, object> param = Parsing.Serialize(arg, true, false);
        //param.Add("task", ID);
        //return Host.Save(this, AsanaFunction.GetFunction(Function.SetParent), param);
    }
}
public partial class AsanaTask // : AsanaObject, IAsanaData
{
    public Task AddProject(AsanaProject arg)
    {
        Dictionary<string, object> param = new Dictionary<string, object> {{"project", arg.ID}};
        return Host.Save(this, AsanaFunction.GetFunction(Function.AddProject), param).ContinueWith(
            (prevTask) =>
                Projects.Add(arg), TaskContinuationOptions.NotOnFaulted);
    }
}
public partial class AsanaTask // : AsanaObject, IAsanaData
{
    public Task RemoveProject(AsanaProject arg)
    {
        Dictionary<string, object> param = new Dictionary<string, object> {{"project", arg.ID}};
        return Host.Save(this, AsanaFunction.GetFunction(Function.RemoveProject), param).ContinueWith(
            (prevTask) =>
                Projects.Remove(arg), TaskContinuationOptions.NotOnFaulted);
    }
}
public partial class AsanaTask // : AsanaObject, IAsanaData
{
    public Task AddTag(AsanaTag arg)
    {
        Dictionary<string, object> param = new Dictionary<string, object> {{"tag", arg.ID}};
        return Host.Save(this, AsanaFunction.GetFunction(Function.AddTag), param).ContinueWith(
            (prevTask) =>
                Tags.Add(arg), TaskContinuationOptions.NotOnFaulted);
    }
}
public partial class AsanaTask // : AsanaObject, IAsanaData
{
    public Task RemoveTag(AsanaTag arg)
    {
        Dictionary<string, object> param = new Dictionary<string, object> {{"tag", arg.ID}};
        return Host.Save(this, AsanaFunction.GetFunction(Function.RemoveTag), param).ContinueWith(
            (prevTask) =>
                Tags.Remove(arg), TaskContinuationOptions.NotOnFaulted);
    }
}
public partial class AsanaTask // : AsanaObject, IAsanaData
{
    public Task AddFollowers(AsanaUser arg)
    {
        Dictionary<string, object> param = new Dictionary<string, object> {{"user", arg.ID}};
        return Host.Save(this, AsanaFunction.GetFunction(Function.AddFollowers), param).ContinueWith(
            (prevTask) =>
                Followers.Add(arg), TaskContinuationOptions.NotOnFaulted);
    }
}
public partial class AsanaTask // : AsanaObject, IAsanaData
{
    public Task RemoveFollowers(AsanaUser arg)
    {
        Dictionary<string, object> param = new Dictionary<string, object> {{"user", arg.ID}};
        return Host.Save(this, AsanaFunction.GetFunction(Function.RemoveFollowers), param).ContinueWith(
            (prevTask) =>
                Followers.Remove(arg), TaskContinuationOptions.NotOnFaulted);
    }
}
public partial class AsanaTask // : AsanaObject, IAsanaData
{
    public Task Delete()
    {
        var task = Host.Save(this, AsanaFunction.GetFunction(Function.DeleteTask), new Dictionary<string, object>(0));
        task.ContinueWith(
            (prevTask) =>
            {
                IsRemoved = true; // invokes destroying all of the references to this object
/*
                //Asana.RemoveFromAllCacheListsOfType<AsanaTask>(this, Host);
                var listsPossiblyContainingThis = Host.ObjectCache.GetAllOfType<AsanaObjectCollection<AsanaTask>>("/");
                foreach (var list in listsPossiblyContainingThis)
                {
                    list.Remove(this);
                }
                //if (!ReferenceEquals(Parent.Tasks, null))
                //    Parent.Tasks.Remove(this);
                ID = (Int64) AsanaExistance.Deleted;
*/
            }, TaskContinuationOptions.NotOnFaulted);
        return task;
    }
}

public partial class AsanaProject // : AsanaObject, IAsanaData
{
    public Task Delete()
    {
        var task = Host.Save(this, AsanaFunction.GetFunction(Function.DeleteProject), new Dictionary<string, object>(0));
        task.ContinueWith(
            (prevTask) =>
            {
                IsRemoved = true; // invokes destroying all of the references to this object
/*
                //Asana.RemoveFromAllCacheListsOfType<AsanaProject>(this, Host);
                var listsPossiblyContainingThis = Host.ObjectCache.GetAllOfType<AsanaObjectCollection<AsanaProject>>("/");
                foreach (var list in listsPossiblyContainingThis)
                {
                    list.Remove(this);
                }
                //if (!ReferenceEquals(Workspace.Projects, null))
                //    Workspace.Projects.Remove(this);
                ID = (Int64) AsanaExistance.Deleted;
*/
            }, TaskContinuationOptions.NotOnFaulted);
        return task;
    }
}

public partial class AsanaStory // : AsanaObject, IAsanaData
{
    public Task Delete()
    {
        var task = Host.Save(this, AsanaFunction.GetFunction(Function.DeleteStory), new Dictionary<string, object>(0));
        task.ContinueWith(
            (prevTask) =>
            {
                IsRemoved = true; // invokes destroying all of the references to this object
/*
                //Asana.RemoveFromAllCacheListsOfType<AsanaStory>(this, Host);
                var listsPossiblyContainingThis = Host.ObjectCache.GetAllOfType<AsanaObjectCollection<AsanaStory>>("/");
                foreach (var list in listsPossiblyContainingThis)
                {
                    list.Remove(this);
                }
                //if (!ReferenceEquals(Target.Stories, null))
                //    Target.Stories.Remove(this);
                ID = (Int64) AsanaExistance.Deleted;
*/
            }, TaskContinuationOptions.NotOnFaulted);
        return task;
    }
}


}
