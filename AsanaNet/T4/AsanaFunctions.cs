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
			AddAFollower,
			RemoveAFollower,
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
			public async Task<AsanaObjectCollection<AsanaUser>> GetUsers(string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetUsers)); 
				
                AsanaObjectCollection<AsanaUser> cachedObject = null;
                
                cachedObject = (AsanaObjectCollection<AsanaUser>) _objectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if (cachedObject != null || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
			    Uri uri;
                if (optFields != null)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetUsers), new Dictionary<string,object>{{"opt_fields", optFields}});
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetUsers));

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetUsers), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, cacheLevel >= AsanaCacheLevel.FillExisting);
				_objectCache.Set(cachePath, output);
				return output;
			}

			public async Task<AsanaUser> GetMe(string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetMe)); 
				
                AsanaUser cachedObject = null;
                if (cacheLevel >= AsanaCacheLevel.FillExisting)
                cachedObject = (AsanaUser) _objectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if (cachedObject != null || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
			    Uri uri;
                if (optFields != null)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetMe), new Dictionary<string,object>{{"opt_fields", optFields}});
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetMe));

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetMe), uri);
                var output = AsanaRequest.GetResponse(response, this, cachedObject);
				_objectCache.Set(cachePath, output);
				return output;
			}

			public async Task<AsanaUser> GetUserById(Int64 arg1, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = string.Format(new PropertyFormatProvider(), "{0}", arg1);
                AsanaUser cachedObject = null;
                if (cacheLevel >= AsanaCacheLevel.FillExisting)
                cachedObject = (AsanaUser) _objectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if (cachedObject != null || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
			    Uri uri;
                if (optFields != null)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetUserById), new Dictionary<string,object>{{"opt_fields", optFields}}, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetUserById), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetUserById), uri);
                var output = AsanaRequest.GetResponse(response, this, cachedObject);
				_objectCache.Set(cachePath, output);
				return output;
			}

			public async Task<AsanaObjectCollection<AsanaWorkspace>> GetWorkspaces(string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetWorkspaces)); 
				
                AsanaObjectCollection<AsanaWorkspace> cachedObject = null;
                
                cachedObject = (AsanaObjectCollection<AsanaWorkspace>) _objectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if (cachedObject != null || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
			    Uri uri;
                if (optFields != null)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetWorkspaces), new Dictionary<string,object>{{"opt_fields", optFields}});
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetWorkspaces));

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetWorkspaces), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, cacheLevel >= AsanaCacheLevel.FillExisting);
				_objectCache.Set(cachePath, output);
				return output;
			}

			public async Task<AsanaWorkspace> GetWorkspaceById(Int64 arg1, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = string.Format(new PropertyFormatProvider(), "{0}", arg1);
                AsanaWorkspace cachedObject = null;
                if (cacheLevel >= AsanaCacheLevel.FillExisting)
                cachedObject = (AsanaWorkspace) _objectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if (cachedObject != null || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
			    Uri uri;
                if (optFields != null)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetWorkspaceById), new Dictionary<string,object>{{"opt_fields", optFields}}, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetWorkspaceById), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetWorkspaceById), uri);
                var output = AsanaRequest.GetResponse(response, this, cachedObject);
				_objectCache.Set(cachePath, output);
				return output;
			}

			public async Task<AsanaObjectCollection<AsanaUser>> GetUsersInWorkspace(AsanaWorkspace arg1, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetUsersInWorkspace), arg1); 
				
                AsanaObjectCollection<AsanaUser> cachedObject = null;
                
                cachedObject = (AsanaObjectCollection<AsanaUser>) _objectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if (cachedObject != null || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
			    Uri uri;
                if (optFields != null)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetUsersInWorkspace), new Dictionary<string,object>{{"opt_fields", optFields}}, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetUsersInWorkspace), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetUsersInWorkspace), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, cacheLevel >= AsanaCacheLevel.FillExisting);
				_objectCache.Set(cachePath, output);
				return output;
			}

			public async Task<AsanaObjectCollection<AsanaTask>> GetTasksInWorkspace(AsanaWorkspace arg1,  AsanaUser arg2, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetTasksInWorkspace), arg1, arg2); 
				
                AsanaObjectCollection<AsanaTask> cachedObject = null;
                
                cachedObject = (AsanaObjectCollection<AsanaTask>) _objectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if (cachedObject != null || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
			    Uri uri;
                if (optFields != null)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetTasksInWorkspace), new Dictionary<string,object>{{"opt_fields", optFields}}, arg1, arg2);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetTasksInWorkspace), arg1, arg2);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetTasksInWorkspace), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, cacheLevel >= AsanaCacheLevel.FillExisting);
				_objectCache.Set(cachePath, output);
				return output;
			}

			public async Task<AsanaObjectCollection<AsanaProject>> GetProjectsInWorkspace(AsanaWorkspace arg1, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetProjectsInWorkspace), arg1); 
				
                AsanaObjectCollection<AsanaProject> cachedObject = null;
                
                cachedObject = (AsanaObjectCollection<AsanaProject>) _objectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if (cachedObject != null || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
			    Uri uri;
                if (optFields != null)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetProjectsInWorkspace), new Dictionary<string,object>{{"opt_fields", optFields}}, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetProjectsInWorkspace), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetProjectsInWorkspace), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, cacheLevel >= AsanaCacheLevel.FillExisting);
				_objectCache.Set(cachePath, output);
				return output;
			}

			public async Task<AsanaObjectCollection<AsanaTag>> GetTagsInWorkspace(AsanaWorkspace arg1, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetTagsInWorkspace), arg1); 
				
                AsanaObjectCollection<AsanaTag> cachedObject = null;
                
                cachedObject = (AsanaObjectCollection<AsanaTag>) _objectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if (cachedObject != null || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
			    Uri uri;
                if (optFields != null)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetTagsInWorkspace), new Dictionary<string,object>{{"opt_fields", optFields}}, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetTagsInWorkspace), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetTagsInWorkspace), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, cacheLevel >= AsanaCacheLevel.FillExisting);
				_objectCache.Set(cachePath, output);
				return output;
			}

			public async Task<AsanaTask> GetTaskById(Int64 arg1, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = string.Format(new PropertyFormatProvider(), "{0}", arg1);
                AsanaTask cachedObject = null;
                if (cacheLevel >= AsanaCacheLevel.FillExisting)
                cachedObject = (AsanaTask) _objectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if (cachedObject != null || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
			    Uri uri;
                if (optFields != null)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetTaskById), new Dictionary<string,object>{{"opt_fields", optFields}}, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetTaskById), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetTaskById), uri);
                var output = AsanaRequest.GetResponse(response, this, cachedObject);
				_objectCache.Set(cachePath, output);
				return output;
			}

			public async Task<AsanaObjectCollection<AsanaTask>> GetSubtasksInTask(AsanaTask arg1, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetSubtasksInTask), arg1); 
				
                AsanaObjectCollection<AsanaTask> cachedObject = null;
                
                cachedObject = (AsanaObjectCollection<AsanaTask>) _objectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if (cachedObject != null || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
			    Uri uri;
                if (optFields != null)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetSubtasksInTask), new Dictionary<string,object>{{"opt_fields", optFields}}, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetSubtasksInTask), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetSubtasksInTask), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, cacheLevel >= AsanaCacheLevel.FillExisting);
				_objectCache.Set(cachePath, output);
				return output;
			}

			public async Task<AsanaObjectCollection<AsanaStory>> GetStoriesInTask(AsanaTask arg1, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetStoriesInTask), arg1); 
				
                AsanaObjectCollection<AsanaStory> cachedObject = null;
                
                cachedObject = (AsanaObjectCollection<AsanaStory>) _objectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if (cachedObject != null || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
			    Uri uri;
                if (optFields != null)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetStoriesInTask), new Dictionary<string,object>{{"opt_fields", optFields}}, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetStoriesInTask), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetStoriesInTask), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, cacheLevel >= AsanaCacheLevel.FillExisting);
				_objectCache.Set(cachePath, output);
				return output;
			}

			public async Task<AsanaObjectCollection<AsanaProject>> GetProjectsOnATask(AsanaTask arg1, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetProjectsOnATask), arg1); 
				
                AsanaObjectCollection<AsanaProject> cachedObject = null;
                
                cachedObject = (AsanaObjectCollection<AsanaProject>) _objectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if (cachedObject != null || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
			    Uri uri;
                if (optFields != null)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetProjectsOnATask), new Dictionary<string,object>{{"opt_fields", optFields}}, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetProjectsOnATask), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetProjectsOnATask), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, cacheLevel >= AsanaCacheLevel.FillExisting);
				_objectCache.Set(cachePath, output);
				return output;
			}

			public async Task<AsanaObjectCollection<AsanaTag>> GetTagsOnATask(AsanaTask arg1, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetTagsOnATask), arg1); 
				
                AsanaObjectCollection<AsanaTag> cachedObject = null;
                
                cachedObject = (AsanaObjectCollection<AsanaTag>) _objectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if (cachedObject != null || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
			    Uri uri;
                if (optFields != null)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetTagsOnATask), new Dictionary<string,object>{{"opt_fields", optFields}}, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetTagsOnATask), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetTagsOnATask), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, cacheLevel >= AsanaCacheLevel.FillExisting);
				_objectCache.Set(cachePath, output);
				return output;
			}

			public async Task<AsanaObjectCollection<AsanaTask>> GetTasksByTag(AsanaTag arg1, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetTasksByTag), arg1); 
				
                AsanaObjectCollection<AsanaTask> cachedObject = null;
                
                cachedObject = (AsanaObjectCollection<AsanaTask>) _objectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if (cachedObject != null || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
			    Uri uri;
                if (optFields != null)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetTasksByTag), new Dictionary<string,object>{{"opt_fields", optFields}}, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetTasksByTag), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetTasksByTag), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, cacheLevel >= AsanaCacheLevel.FillExisting);
				_objectCache.Set(cachePath, output);
				return output;
			}

			public async Task<AsanaStory> GetStoryById(Int64 arg1, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = string.Format(new PropertyFormatProvider(), "{0}", arg1);
                AsanaStory cachedObject = null;
                if (cacheLevel >= AsanaCacheLevel.FillExisting)
                cachedObject = (AsanaStory) _objectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if (cachedObject != null || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
			    Uri uri;
                if (optFields != null)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetStoryById), new Dictionary<string,object>{{"opt_fields", optFields}}, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetStoryById), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetStoryById), uri);
                var output = AsanaRequest.GetResponse(response, this, cachedObject);
				_objectCache.Set(cachePath, output);
				return output;
			}

			public async Task<AsanaProject> GetProjectById(Int64 arg1, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = string.Format(new PropertyFormatProvider(), "{0}", arg1);
                AsanaProject cachedObject = null;
                if (cacheLevel >= AsanaCacheLevel.FillExisting)
                cachedObject = (AsanaProject) _objectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if (cachedObject != null || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
			    Uri uri;
                if (optFields != null)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetProjectById), new Dictionary<string,object>{{"opt_fields", optFields}}, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetProjectById), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetProjectById), uri);
                var output = AsanaRequest.GetResponse(response, this, cachedObject);
				_objectCache.Set(cachePath, output);
				return output;
			}

			public async Task<AsanaObjectCollection<AsanaTask>> GetTasksInAProject(AsanaProject arg1, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetTasksInAProject), arg1); 
				
                AsanaObjectCollection<AsanaTask> cachedObject = null;
                
                cachedObject = (AsanaObjectCollection<AsanaTask>) _objectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if (cachedObject != null || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
			    Uri uri;
                if (optFields != null)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetTasksInAProject), new Dictionary<string,object>{{"opt_fields", optFields}}, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetTasksInAProject), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetTasksInAProject), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, cacheLevel >= AsanaCacheLevel.FillExisting);
				_objectCache.Set(cachePath, output);
				return output;
			}

			public async Task<AsanaTag> GetTagById(Int64 arg1, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = string.Format(new PropertyFormatProvider(), "{0}", arg1);
                AsanaTag cachedObject = null;
                if (cacheLevel >= AsanaCacheLevel.FillExisting)
                cachedObject = (AsanaTag) _objectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if (cachedObject != null || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
			    Uri uri;
                if (optFields != null)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetTagById), new Dictionary<string,object>{{"opt_fields", optFields}}, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetTagById), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetTagById), uri);
                var output = AsanaRequest.GetResponse(response, this, cachedObject);
				_objectCache.Set(cachePath, output);
				return output;
			}

			public async Task<AsanaObjectCollection<AsanaTeam>> GetTeamsInWorkspace(AsanaWorkspace arg1, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetTeamsInWorkspace), arg1); 
				
                AsanaObjectCollection<AsanaTeam> cachedObject = null;
                
                cachedObject = (AsanaObjectCollection<AsanaTeam>) _objectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if (cachedObject != null || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
			    Uri uri;
                if (optFields != null)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetTeamsInWorkspace), new Dictionary<string,object>{{"opt_fields", optFields}}, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetTeamsInWorkspace), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetTeamsInWorkspace), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, cacheLevel >= AsanaCacheLevel.FillExisting);
				_objectCache.Set(cachePath, output);
				return output;
			}

			public async Task<AsanaEventList> GetEvents(AsanaEventedObject arg1,  string arg2, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default)
			{
                if (cacheLevel == AsanaCacheLevel.Default) cacheLevel = this.DefaultCacheLevel;

				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetEvents), arg1, arg2); 
				
                AsanaEventList cachedObject = null;
                if (cacheLevel >= AsanaCacheLevel.FillExisting)
                cachedObject = (AsanaEventList) _objectCache.Get(cachePath);
                if (cacheLevel >= AsanaCacheLevel.UseExisting)
                {
                    if (cachedObject != null || cacheLevel == AsanaCacheLevel.UseExistingOrNull)
                        return cachedObject;
                }
			    Uri uri;
                if (optFields != null)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetEvents), new Dictionary<string,object>{{"opt_fields", optFields}}, arg1, arg2);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetEvents), arg1, arg2);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetEvents), uri);
                var output = AsanaRequest.GetResponse(response, this, cachedObject);
				_objectCache.Set(cachePath, output);
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
				Functions.Add(Function.AddAFollower, new AsanaFunction("/tasks/{0:ID}/addFollowers", "POST"));
				Functions.Add(Function.RemoveAFollower, new AsanaFunction("/tasks/{0:ID}/removeFollowers", "POST"));
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
        if (!object.ReferenceEquals(Host, null))
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
        if (!object.ReferenceEquals(Host, null))
            await Host.GetWorkspaceById(this.ID, optFields, AsanaCacheLevel.Ignore);
    }
}

public partial class AsanaWorkspace // : AsanaObject, IAsanaData
{
	[AsanaDataAttribute     ("users",        SerializationFlags.Optional, "ID")]
    public AsanaObjectCollection<AsanaUser> Users
    {
        get 
        {
            var task = GetUsers(null, AsanaCacheLevel.UseExistingOrNull);
            return !object.ReferenceEquals(task, null) ? task.Result : null;
        }
		private set 
		{
            if (!object.ReferenceEquals(value, null))
            {
			    string cachePath = Asana.GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetUsersInWorkspace), this);
			    Host._objectCache.Set(cachePath, value);
            }
		}
    }
    public Task<AsanaObjectCollection<AsanaUser>> GetUsers(string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default)
    {
        if (!object.ReferenceEquals(Host, null))
            return Host.GetUsersInWorkspace(this, optFields, cacheLevel);
        return null;
    }
}

public partial class AsanaWorkspace // : AsanaObject, IAsanaData
{
    public Task<AsanaObjectCollection<AsanaTask>> GetTasks( AsanaUser arg2, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default)
    {
        if (!object.ReferenceEquals(Host, null))
            return Host.GetTasksInWorkspace(this, arg2, optFields, cacheLevel);
        return null;
    }
}

public partial class AsanaWorkspace // : AsanaObject, IAsanaData
{
	[AsanaDataAttribute     ("projects",        SerializationFlags.Optional, "ID")]
    public AsanaObjectCollection<AsanaProject> Projects
    {
        get 
        {
            var task = GetProjects(null, AsanaCacheLevel.UseExistingOrNull);
            return !object.ReferenceEquals(task, null) ? task.Result : null;
        }
		private set 
		{
            if (!object.ReferenceEquals(value, null))
            {
			    string cachePath = Asana.GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetProjectsInWorkspace), this);
			    Host._objectCache.Set(cachePath, value);
            }
		}
    }
    public Task<AsanaObjectCollection<AsanaProject>> GetProjects(string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default)
    {
        if (!object.ReferenceEquals(Host, null))
            return Host.GetProjectsInWorkspace(this, optFields, cacheLevel);
        return null;
    }
}

public partial class AsanaWorkspace // : AsanaObject, IAsanaData
{
	[AsanaDataAttribute     ("tags",        SerializationFlags.Optional, "ID")]
    public AsanaObjectCollection<AsanaTag> Tags
    {
        get 
        {
            var task = GetTags(null, AsanaCacheLevel.UseExistingOrNull);
            return !object.ReferenceEquals(task, null) ? task.Result : null;
        }
		private set 
		{
            if (!object.ReferenceEquals(value, null))
            {
			    string cachePath = Asana.GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetTagsInWorkspace), this);
			    Host._objectCache.Set(cachePath, value);
            }
		}
    }
    public Task<AsanaObjectCollection<AsanaTag>> GetTags(string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default)
    {
        if (!object.ReferenceEquals(Host, null))
            return Host.GetTagsInWorkspace(this, optFields, cacheLevel);
        return null;
    }
}

public partial class AsanaTask // : AsanaObject, IAsanaData
{
    public async override Task Refresh(string optFields = null)
    {
        if (!object.ReferenceEquals(Host, null))
            await Host.GetTaskById(this.ID, optFields, AsanaCacheLevel.Ignore);
    }
}

public partial class AsanaTask // : AsanaObject, IAsanaData
{
	[AsanaDataAttribute     ("tasks",        SerializationFlags.Optional, "ID")]
    public AsanaObjectCollection<AsanaTask> Tasks
    {
        get 
        {
            var task = GetTasks(null, AsanaCacheLevel.UseExistingOrNull);
            return !object.ReferenceEquals(task, null) ? task.Result : null;
        }
		private set 
		{
            if (!object.ReferenceEquals(value, null))
            {
			    string cachePath = Asana.GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetSubtasksInTask), this);
			    Host._objectCache.Set(cachePath, value);
            }
		}
    }
    public Task<AsanaObjectCollection<AsanaTask>> GetTasks(string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default)
    {
        if (!object.ReferenceEquals(Host, null))
            return Host.GetSubtasksInTask(this, optFields, cacheLevel);
        return null;
    }
}

public partial class AsanaTask // : AsanaObject, IAsanaData
{
	[AsanaDataAttribute     ("stories",        SerializationFlags.Optional, "ID")]
    public AsanaObjectCollection<AsanaStory> Stories
    {
        get 
        {
            var task = GetStories(null, AsanaCacheLevel.UseExistingOrNull);
            return !object.ReferenceEquals(task, null) ? task.Result : null;
        }
		private set 
		{
            if (!object.ReferenceEquals(value, null))
            {
			    string cachePath = Asana.GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetStoriesInTask), this);
			    Host._objectCache.Set(cachePath, value);
            }
		}
    }
    public Task<AsanaObjectCollection<AsanaStory>> GetStories(string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default)
    {
        if (!object.ReferenceEquals(Host, null))
            return Host.GetStoriesInTask(this, optFields, cacheLevel);
        return null;
    }
}

public partial class AsanaTask // : AsanaObject, IAsanaData
{
	[AsanaDataAttribute     ("projects",        SerializationFlags.Optional, "ID")]
    public AsanaObjectCollection<AsanaProject> Projects
    {
        get 
        {
            var task = GetProjects(null, AsanaCacheLevel.UseExistingOrNull);
            return !object.ReferenceEquals(task, null) ? task.Result : null;
        }
		private set 
		{
            if (!object.ReferenceEquals(value, null))
            {
			    string cachePath = Asana.GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetProjectsOnATask), this);
			    Host._objectCache.Set(cachePath, value);
            }
		}
    }
    public Task<AsanaObjectCollection<AsanaProject>> GetProjects(string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default)
    {
        if (!object.ReferenceEquals(Host, null))
            return Host.GetProjectsOnATask(this, optFields, cacheLevel);
        return null;
    }
}

public partial class AsanaTask // : AsanaObject, IAsanaData
{
	[AsanaDataAttribute     ("tags",        SerializationFlags.Optional, "ID")]
    public AsanaObjectCollection<AsanaTag> Tags
    {
        get 
        {
            var task = GetTags(null, AsanaCacheLevel.UseExistingOrNull);
            return !object.ReferenceEquals(task, null) ? task.Result : null;
        }
		private set 
		{
            if (!object.ReferenceEquals(value, null))
            {
			    string cachePath = Asana.GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetTagsOnATask), this);
			    Host._objectCache.Set(cachePath, value);
            }
		}
    }
    public Task<AsanaObjectCollection<AsanaTag>> GetTags(string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default)
    {
        if (!object.ReferenceEquals(Host, null))
            return Host.GetTagsOnATask(this, optFields, cacheLevel);
        return null;
    }
}

public partial class AsanaTag // : AsanaObject, IAsanaData
{
	[AsanaDataAttribute     ("tasks",        SerializationFlags.Optional, "ID")]
    public AsanaObjectCollection<AsanaTask> Tasks
    {
        get 
        {
            var task = GetTasks(null, AsanaCacheLevel.UseExistingOrNull);
            return !object.ReferenceEquals(task, null) ? task.Result : null;
        }
		private set 
		{
            if (!object.ReferenceEquals(value, null))
            {
			    string cachePath = Asana.GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetTasksByTag), this);
			    Host._objectCache.Set(cachePath, value);
            }
		}
    }
    public Task<AsanaObjectCollection<AsanaTask>> GetTasks(string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default)
    {
        if (!object.ReferenceEquals(Host, null))
            return Host.GetTasksByTag(this, optFields, cacheLevel);
        return null;
    }
}

public partial class AsanaStory // : AsanaObject, IAsanaData
{
    public async override Task Refresh(string optFields = null)
    {
        if (!object.ReferenceEquals(Host, null))
            await Host.GetStoryById(this.ID, optFields, AsanaCacheLevel.Ignore);
    }
}

public partial class AsanaProject // : AsanaObject, IAsanaData
{
    public async override Task Refresh(string optFields = null)
    {
        if (!object.ReferenceEquals(Host, null))
            await Host.GetProjectById(this.ID, optFields, AsanaCacheLevel.Ignore);
    }
}

public partial class AsanaProject // : AsanaObject, IAsanaData
{
	[AsanaDataAttribute     ("tasks",        SerializationFlags.Optional, "ID")]
    public AsanaObjectCollection<AsanaTask> Tasks
    {
        get 
        {
            var task = GetTasks(null, AsanaCacheLevel.UseExistingOrNull);
            return !object.ReferenceEquals(task, null) ? task.Result : null;
        }
		private set 
		{
            if (!object.ReferenceEquals(value, null))
            {
			    string cachePath = Asana.GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetTasksInAProject), this);
			    Host._objectCache.Set(cachePath, value);
            }
		}
    }
    public Task<AsanaObjectCollection<AsanaTask>> GetTasks(string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default)
    {
        if (!object.ReferenceEquals(Host, null))
            return Host.GetTasksInAProject(this, optFields, cacheLevel);
        return null;
    }
}

public partial class AsanaTag // : AsanaObject, IAsanaData
{
    public async override Task Refresh(string optFields = null)
    {
        if (!object.ReferenceEquals(Host, null))
            await Host.GetTagById(this.ID, optFields, AsanaCacheLevel.Ignore);
    }
}

public partial class AsanaWorkspace // : AsanaObject, IAsanaData
{
	[AsanaDataAttribute     ("teams",        SerializationFlags.Optional, "ID")]
    public AsanaObjectCollection<AsanaTeam> Teams
    {
        get 
        {
            var task = GetTeams(null, AsanaCacheLevel.UseExistingOrNull);
            return !object.ReferenceEquals(task, null) ? task.Result : null;
        }
		private set 
		{
            if (!object.ReferenceEquals(value, null))
            {
			    string cachePath = Asana.GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetTeamsInWorkspace), this);
			    Host._objectCache.Set(cachePath, value);
            }
		}
    }
    public Task<AsanaObjectCollection<AsanaTeam>> GetTeams(string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default)
    {
        if (!object.ReferenceEquals(Host, null))
            return Host.GetTeamsInWorkspace(this, optFields, cacheLevel);
        return null;
    }
}

public partial class AsanaEventedObject // : AsanaObject, IAsanaData
{
    public Task<AsanaEventList> GetEventLists( string arg2, string optFields = null, AsanaCacheLevel cacheLevel = AsanaCacheLevel.Default)
    {
        if (!object.ReferenceEquals(Host, null))
            return Host.GetEvents(this, arg2, optFields, cacheLevel);
        return null;
    }
}

public partial class AsanaWorkspace // : AsanaObject, IAsanaData
{
    public Task CreateTask(AsanaTask arg)
    {
        arg.Host = Host;
        arg.Workspace = this;
        return Host.Save(arg, AsanaFunction.GetFunction(Function.CreateTask));
    }
}

public partial class AsanaWorkspace // : AsanaObject, IAsanaData
{
    public Task CreateProject(AsanaProject arg)
    {
        arg.Host = Host;
        arg.Workspace = this;
        return Host.Save(arg, AsanaFunction.GetFunction(Function.CreateProject)).ContinueWith(
            (prevTask) =>
                {
                    if (!ReferenceEquals(Projects, null))
                        Projects.Add(arg);
                }, TaskContinuationOptions.NotOnFaulted);
    }
}

public partial class AsanaWorkspace // : AsanaObject, IAsanaData
{
    public Task CreateTag(AsanaTag arg)
    {
        arg.Host = Host;
        arg.Workspace = this;
        return Host.Save(arg, AsanaFunction.GetFunction(Function.CreateTag)).ContinueWith(
            (prevTask) =>
                {
                    if (!ReferenceEquals(Tags, null))
                        Tags.Add(arg);
                }, TaskContinuationOptions.NotOnFaulted);
    }
}

public partial class AsanaTask // : AsanaObject, IAsanaData
{
    public Task CreateStory(AsanaStory arg)
    {
        arg.Host = Host;
        arg.Target = this;
        return Host.Save(arg, AsanaFunction.GetFunction(Function.CreateStory)).ContinueWith(
            (prevTask) =>
                {
                    if (!ReferenceEquals(Stories, null))
                        Stories.Add(arg);
                }, TaskContinuationOptions.NotOnFaulted);
    }
}

public partial class AsanaTask // : AsanaObject, IAsanaData
{
    public Task CreateSubtask(AsanaTask arg)
    {
        arg.Host = Host;
        arg.Parent = this;
        return Host.Save(arg, AsanaFunction.GetFunction(Function.CreateSubtask)).ContinueWith(
            (prevTask) =>
                {
                    if (!ReferenceEquals(Tasks, null))
                        Tasks.Add(arg);
                }, TaskContinuationOptions.NotOnFaulted);
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
    public Task AddAFollower(AsanaUser arg)
    {
        Dictionary<string, object> param = new Dictionary<string, object> {{"user", arg.ID}};
        return Host.Save(this, AsanaFunction.GetFunction(Function.AddAFollower), param).ContinueWith(
            (prevTask) =>
                Followers.Add(arg), TaskContinuationOptions.NotOnFaulted);
    }
}
public partial class AsanaTask // : AsanaObject, IAsanaData
{
    public Task RemoveAFollower(AsanaUser arg)
    {
        Dictionary<string, object> param = new Dictionary<string, object> {{"user", arg.ID}};
        return Host.Save(this, AsanaFunction.GetFunction(Function.RemoveAFollower), param).ContinueWith(
            (prevTask) =>
                Followers.Remove(arg), TaskContinuationOptions.NotOnFaulted);
    }
}
public partial class AsanaTask // : AsanaObject, IAsanaData
{
    public Task Delete()
    {
        return Host.Save(this, AsanaFunction.GetFunction(Function.DeleteTask), new Dictionary<string, object>(0)).ContinueWith(
            (prevTask) =>
            {
                IsRemoved = true; // invokes destroying all of the references to this object
/*
                //Asana.RemoveFromAllCacheListsOfType<AsanaTask>(this, Host);
                var listsPossiblyContainingThis = Host._objectCache.GetAllOfType<AsanaObjectCollection<AsanaTask>>("/");
                foreach (var list in listsPossiblyContainingThis)
                {
                    list.Remove(this);
                }
                //if (!ReferenceEquals(Parent.Tasks, null))
                //    Parent.Tasks.Remove(this);
                ID = (Int64) AsanaExistance.Deleted;
*/
            }, TaskContinuationOptions.NotOnFaulted);

    }
}

public partial class AsanaProject // : AsanaObject, IAsanaData
{
    public Task Delete()
    {
        return Host.Save(this, AsanaFunction.GetFunction(Function.DeleteProject), new Dictionary<string, object>(0)).ContinueWith(
            (prevTask) =>
            {
                IsRemoved = true; // invokes destroying all of the references to this object
/*
                //Asana.RemoveFromAllCacheListsOfType<AsanaProject>(this, Host);
                var listsPossiblyContainingThis = Host._objectCache.GetAllOfType<AsanaObjectCollection<AsanaProject>>("/");
                foreach (var list in listsPossiblyContainingThis)
                {
                    list.Remove(this);
                }
                //if (!ReferenceEquals(Workspace.Projects, null))
                //    Workspace.Projects.Remove(this);
                ID = (Int64) AsanaExistance.Deleted;
*/
            }, TaskContinuationOptions.NotOnFaulted);

    }
}

public partial class AsanaStory // : AsanaObject, IAsanaData
{
    public Task Delete()
    {
        return Host.Save(this, AsanaFunction.GetFunction(Function.DeleteStory), new Dictionary<string, object>(0)).ContinueWith(
            (prevTask) =>
            {
                IsRemoved = true; // invokes destroying all of the references to this object
/*
                //Asana.RemoveFromAllCacheListsOfType<AsanaStory>(this, Host);
                var listsPossiblyContainingThis = Host._objectCache.GetAllOfType<AsanaObjectCollection<AsanaStory>>("/");
                foreach (var list in listsPossiblyContainingThis)
                {
                    list.Remove(this);
                }
                //if (!ReferenceEquals(Target.Stories, null))
                //    Target.Stories.Remove(this);
                ID = (Int64) AsanaExistance.Deleted;
*/
            }, TaskContinuationOptions.NotOnFaulted);

    }
}


}
