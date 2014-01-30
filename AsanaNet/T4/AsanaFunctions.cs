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
			GetTasksByTag,
			GetStoryById,
			GetProjectById,
			GetTasksInAProject,
			GetTagById,
			GetTeamsInWorkspace,
			CreateWorkspaceTask,
			CreateTaskSubtask,
			SetParentTask,
			AddProjectToTask,
			RemoveProjectFromTask,
			AddStoryToTask,
			AddTagToTask,
			RemoveTagFromTask,
			CreateWorkspaceProject,
			CreateWorkspaceTag,
			UpdateTask,
			UpdateTag,
			UpdateProject,
			UpdateWorkspace,
			DeleteTask,
			DeleteProject,
		}

		// Function definitions specifically for the GET functions.
		public partial class Asana
		{
			public async Task<AsanaObjectCollection<AsanaUser>> GetUsers(string optFields = null, bool missCache = false, bool missCollectionElementsCache = false)
			{
				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetUsers)); 
				
                var cachedObject = (AsanaObjectCollection<AsanaUser>) _objectCache.Get(cachePath);
                if (!missCache)
                {
                    if (cachedObject != null)
                        return cachedObject;
                }
			    Uri uri;
                if (optFields != null)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetUsers), new Dictionary<string,object>{{"opt_fields", optFields}});
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetUsers));

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetUsers), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, !missCollectionElementsCache);
				_objectCache.Set(cachePath, output);
				return output;
			}

			public async Task<AsanaUser> GetMe(string optFields = null, bool missCache = false)
			{
				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetMe)); 
				
                var cachedObject = (AsanaUser) _objectCache.Get(cachePath);
                if (!missCache)
                {
                    if (cachedObject != null)
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

			public async Task<AsanaUser> GetUserById(Int64 arg1, string optFields = null, bool missCache = false)
			{
				string cachePath = string.Format(new PropertyFormatProvider(), "{0}", arg1);
                var cachedObject = (AsanaUser) _objectCache.Get(cachePath);
                if (!missCache)
                {
                    if (cachedObject != null)
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

			public async Task<AsanaObjectCollection<AsanaWorkspace>> GetWorkspaces(string optFields = null, bool missCache = false, bool missCollectionElementsCache = false)
			{
				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetWorkspaces)); 
				
                var cachedObject = (AsanaObjectCollection<AsanaWorkspace>) _objectCache.Get(cachePath);
                if (!missCache)
                {
                    if (cachedObject != null)
                        return cachedObject;
                }
			    Uri uri;
                if (optFields != null)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetWorkspaces), new Dictionary<string,object>{{"opt_fields", optFields}});
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetWorkspaces));

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetWorkspaces), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, !missCollectionElementsCache);
				_objectCache.Set(cachePath, output);
				return output;
			}

			public async Task<AsanaWorkspace> GetWorkspaceById(Int64 arg1, string optFields = null, bool missCache = false)
			{
				string cachePath = string.Format(new PropertyFormatProvider(), "{0}", arg1);
                var cachedObject = (AsanaWorkspace) _objectCache.Get(cachePath);
                if (!missCache)
                {
                    if (cachedObject != null)
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

			public async Task<AsanaObjectCollection<AsanaUser>> GetUsersInWorkspace(AsanaWorkspace arg1, string optFields = null, bool missCache = false, bool missCollectionElementsCache = false)
			{
				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetUsersInWorkspace), arg1); 
				
                var cachedObject = (AsanaObjectCollection<AsanaUser>) _objectCache.Get(cachePath);
                if (!missCache)
                {
                    if (cachedObject != null)
                        return cachedObject;
                }
			    Uri uri;
                if (optFields != null)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetUsersInWorkspace), new Dictionary<string,object>{{"opt_fields", optFields}}, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetUsersInWorkspace), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetUsersInWorkspace), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, !missCollectionElementsCache);
				_objectCache.Set(cachePath, output);
				return output;
			}

			public async Task<AsanaObjectCollection<AsanaTask>> GetTasksInWorkspace(AsanaWorkspace arg1,  AsanaUser arg2, string optFields = null, bool missCache = false, bool missCollectionElementsCache = false)
			{
				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetTasksInWorkspace), arg1, arg2); 
				
                var cachedObject = (AsanaObjectCollection<AsanaTask>) _objectCache.Get(cachePath);
                if (!missCache)
                {
                    if (cachedObject != null)
                        return cachedObject;
                }
			    Uri uri;
                if (optFields != null)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetTasksInWorkspace), new Dictionary<string,object>{{"opt_fields", optFields}}, arg1, arg2);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetTasksInWorkspace), arg1, arg2);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetTasksInWorkspace), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, !missCollectionElementsCache);
				_objectCache.Set(cachePath, output);
				return output;
			}

			public async Task<AsanaObjectCollection<AsanaProject>> GetProjectsInWorkspace(AsanaWorkspace arg1, string optFields = null, bool missCache = false, bool missCollectionElementsCache = false)
			{
				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetProjectsInWorkspace), arg1); 
				
                var cachedObject = (AsanaObjectCollection<AsanaProject>) _objectCache.Get(cachePath);
                if (!missCache)
                {
                    if (cachedObject != null)
                        return cachedObject;
                }
			    Uri uri;
                if (optFields != null)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetProjectsInWorkspace), new Dictionary<string,object>{{"opt_fields", optFields}}, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetProjectsInWorkspace), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetProjectsInWorkspace), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, !missCollectionElementsCache);
				_objectCache.Set(cachePath, output);
				return output;
			}

			public async Task<AsanaObjectCollection<AsanaTag>> GetTagsInWorkspace(AsanaWorkspace arg1, string optFields = null, bool missCache = false, bool missCollectionElementsCache = false)
			{
				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetTagsInWorkspace), arg1); 
				
                var cachedObject = (AsanaObjectCollection<AsanaTag>) _objectCache.Get(cachePath);
                if (!missCache)
                {
                    if (cachedObject != null)
                        return cachedObject;
                }
			    Uri uri;
                if (optFields != null)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetTagsInWorkspace), new Dictionary<string,object>{{"opt_fields", optFields}}, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetTagsInWorkspace), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetTagsInWorkspace), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, !missCollectionElementsCache);
				_objectCache.Set(cachePath, output);
				return output;
			}

			public async Task<AsanaTask> GetTaskById(Int64 arg1, string optFields = null, bool missCache = false)
			{
				string cachePath = string.Format(new PropertyFormatProvider(), "{0}", arg1);
                var cachedObject = (AsanaTask) _objectCache.Get(cachePath);
                if (!missCache)
                {
                    if (cachedObject != null)
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

			public async Task<AsanaObjectCollection<AsanaTask>> GetSubtasksInTask(AsanaTask arg1, string optFields = null, bool missCache = false, bool missCollectionElementsCache = false)
			{
				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetSubtasksInTask), arg1); 
				
                var cachedObject = (AsanaObjectCollection<AsanaTask>) _objectCache.Get(cachePath);
                if (!missCache)
                {
                    if (cachedObject != null)
                        return cachedObject;
                }
			    Uri uri;
                if (optFields != null)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetSubtasksInTask), new Dictionary<string,object>{{"opt_fields", optFields}}, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetSubtasksInTask), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetSubtasksInTask), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, !missCollectionElementsCache);
				_objectCache.Set(cachePath, output);
				return output;
			}

			public async Task<AsanaObjectCollection<AsanaStory>> GetStoriesInTask(AsanaTask arg1, string optFields = null, bool missCache = false, bool missCollectionElementsCache = false)
			{
				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetStoriesInTask), arg1); 
				
                var cachedObject = (AsanaObjectCollection<AsanaStory>) _objectCache.Get(cachePath);
                if (!missCache)
                {
                    if (cachedObject != null)
                        return cachedObject;
                }
			    Uri uri;
                if (optFields != null)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetStoriesInTask), new Dictionary<string,object>{{"opt_fields", optFields}}, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetStoriesInTask), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetStoriesInTask), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, !missCollectionElementsCache);
				_objectCache.Set(cachePath, output);
				return output;
			}

			public async Task<AsanaObjectCollection<AsanaProject>> GetProjectsOnATask(AsanaTask arg1, string optFields = null, bool missCache = false, bool missCollectionElementsCache = false)
			{
				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetProjectsOnATask), arg1); 
				
                var cachedObject = (AsanaObjectCollection<AsanaProject>) _objectCache.Get(cachePath);
                if (!missCache)
                {
                    if (cachedObject != null)
                        return cachedObject;
                }
			    Uri uri;
                if (optFields != null)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetProjectsOnATask), new Dictionary<string,object>{{"opt_fields", optFields}}, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetProjectsOnATask), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetProjectsOnATask), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, !missCollectionElementsCache);
				_objectCache.Set(cachePath, output);
				return output;
			}

			public async Task<AsanaObjectCollection<AsanaTask>> GetTasksByTag(AsanaTag arg1, string optFields = null, bool missCache = false, bool missCollectionElementsCache = false)
			{
				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetTasksByTag), arg1); 
				
                var cachedObject = (AsanaObjectCollection<AsanaTask>) _objectCache.Get(cachePath);
                if (!missCache)
                {
                    if (cachedObject != null)
                        return cachedObject;
                }
			    Uri uri;
                if (optFields != null)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetTasksByTag), new Dictionary<string,object>{{"opt_fields", optFields}}, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetTasksByTag), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetTasksByTag), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, !missCollectionElementsCache);
				_objectCache.Set(cachePath, output);
				return output;
			}

			public async Task<AsanaStory> GetStoryById(Int64 arg1, string optFields = null, bool missCache = false)
			{
				string cachePath = string.Format(new PropertyFormatProvider(), "{0}", arg1);
                var cachedObject = (AsanaStory) _objectCache.Get(cachePath);
                if (!missCache)
                {
                    if (cachedObject != null)
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

			public async Task<AsanaProject> GetProjectById(Int64 arg1, string optFields = null, bool missCache = false)
			{
				string cachePath = string.Format(new PropertyFormatProvider(), "{0}", arg1);
                var cachedObject = (AsanaProject) _objectCache.Get(cachePath);
                if (!missCache)
                {
                    if (cachedObject != null)
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

			public async Task<AsanaObjectCollection<AsanaTask>> GetTasksInAProject(AsanaProject arg1, string optFields = null, bool missCache = false, bool missCollectionElementsCache = false)
			{
				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetTasksInAProject), arg1); 
				
                var cachedObject = (AsanaObjectCollection<AsanaTask>) _objectCache.Get(cachePath);
                if (!missCache)
                {
                    if (cachedObject != null)
                        return cachedObject;
                }
			    Uri uri;
                if (optFields != null)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetTasksInAProject), new Dictionary<string,object>{{"opt_fields", optFields}}, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetTasksInAProject), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetTasksInAProject), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, !missCollectionElementsCache);
				_objectCache.Set(cachePath, output);
				return output;
			}

			public async Task<AsanaTag> GetTagById(Int64 arg1, string optFields = null, bool missCache = false)
			{
				string cachePath = string.Format(new PropertyFormatProvider(), "{0}", arg1);
                var cachedObject = (AsanaTag) _objectCache.Get(cachePath);
                if (!missCache)
                {
                    if (cachedObject != null)
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

			public async Task<AsanaObjectCollection<AsanaTeam>> GetTeamsInWorkspace(AsanaWorkspace arg1, string optFields = null, bool missCache = false, bool missCollectionElementsCache = false)
			{
				string cachePath = GetAsanaPartUri(AsanaFunction.GetFunction(Function.GetTeamsInWorkspace), arg1); 
				
                var cachedObject = (AsanaObjectCollection<AsanaTeam>) _objectCache.Get(cachePath);
                if (!missCache)
                {
                    if (cachedObject != null)
                        return cachedObject;
                }
			    Uri uri;
                if (optFields != null)
                    uri = GetBaseUriWithParams(AsanaFunction.GetFunction(Function.GetTeamsInWorkspace), new Dictionary<string,object>{{"opt_fields", optFields}}, arg1);
                else
                    uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetTeamsInWorkspace), arg1);

				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetTeamsInWorkspace), uri);
                var output = AsanaRequest.GetResponseCollection(response, this, cachedObject, !missCollectionElementsCache);
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
				Functions.Add(Function.GetTasksByTag, new AsanaFunction("/tags/{0:ID}/tasks", "GET"));
				Functions.Add(Function.GetStoryById, new AsanaFunction("/stories/{0}", "GET"));
				Functions.Add(Function.GetProjectById, new AsanaFunction("/projects/{0}", "GET"));
				Functions.Add(Function.GetTasksInAProject, new AsanaFunction("/projects/{0:ID}/tasks", "GET"));
				Functions.Add(Function.GetTagById, new AsanaFunction("/tags/{0}", "GET"));
				Functions.Add(Function.GetTeamsInWorkspace, new AsanaFunction("/organizations/{0:ID}/teams", "GET"));
				Functions.Add(Function.CreateWorkspaceTask, new AsanaFunction("/tasks", "POST"));
				Functions.Add(Function.CreateTaskSubtask, new AsanaFunction("/tasks/{0:Parent}/subtasks", "POST"));
				Functions.Add(Function.SetParentTask, new AsanaFunction("/tasks/{0:ID}/setParent", "POST"));
				Functions.Add(Function.AddProjectToTask, new AsanaFunction("/tasks/{0:ID}/addProject", "POST"));
				Functions.Add(Function.RemoveProjectFromTask, new AsanaFunction("/tasks/{0:ID}/removeProject", "POST"));
				Functions.Add(Function.AddStoryToTask, new AsanaFunction("/tasks/{0:Target}/stories", "POST"));
				Functions.Add(Function.AddTagToTask, new AsanaFunction("/tasks/{0:ID}/addTag", "POST"));
				Functions.Add(Function.RemoveTagFromTask, new AsanaFunction("/tasks/{0:ID}/removeTag", "POST"));
				Functions.Add(Function.CreateWorkspaceProject, new AsanaFunction("/projects", "POST"));
				Functions.Add(Function.CreateWorkspaceTag, new AsanaFunction("/tags", "POST"));
				Functions.Add(Function.UpdateTask, new AsanaFunction("/tasks/{0:ID}", "PUT"));
				Functions.Add(Function.UpdateTag, new AsanaFunction("/tags/{0:ID}", "PUT"));
				Functions.Add(Function.UpdateProject, new AsanaFunction("/projects/{0:ID}", "PUT"));
				Functions.Add(Function.UpdateWorkspace, new AsanaFunction("/workspaces/{0:ID}", "PUT"));
				Functions.Add(Function.DeleteTask, new AsanaFunction("/tasks/{0:ID}", "DELETE"));
				Functions.Add(Function.DeleteProject, new AsanaFunction("/projects/{0:ID}", "DELETE"));
		

				Associations.Add(typeof(AsanaWorkspace), new AsanaFunctionAssociation(null, GetFunction(Function.UpdateWorkspace), null));
				Associations.Add(typeof(AsanaTask), new AsanaFunctionAssociation(GetFunction(Function.CreateWorkspaceTask), GetFunction(Function.UpdateTask), GetFunction(Function.DeleteTask)));
				Associations.Add(typeof(AsanaSubtask), new AsanaFunctionAssociation(GetFunction(Function.CreateTaskSubtask), GetFunction(Function.UpdateTask), GetFunction(Function.DeleteTask)));
				Associations.Add(typeof(AsanaProject), new AsanaFunctionAssociation(GetFunction(Function.CreateWorkspaceProject), GetFunction(Function.UpdateProject), GetFunction(Function.DeleteProject)));
				Associations.Add(typeof(AsanaTag), new AsanaFunctionAssociation(GetFunction(Function.CreateWorkspaceTag), GetFunction(Function.UpdateTag), null));
				Associations.Add(typeof(AsanaStory), new AsanaFunctionAssociation(GetFunction(Function.AddStoryToTask), null, null));
		
			}
		}
}