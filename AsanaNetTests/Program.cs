using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using AsanaNet;

namespace AsanaNetTests
{
    class Program
    {
        private static string YOUR_API_KEY  = @"";
        private static Int64 YOUR_WORKSPACE = 0; 
        private static Asana Asana;
        static async Task TestGetMe()
        {
            var me = await Asana.GetMe();

            Console.WriteLine(me.Name);
            Console.ReadLine();
        }

        private static async Task TestCreateTask(AsanaWorkspace workspace)
        {
            var me = await Asana.GetMe();

            var taskParent = new AsanaTask { Name = "Parent Test", Assignee = me };
            await workspace.CreateTask(taskParent);
//            taskParent = await taskParent.Save(Asana);

            Console.WriteLine(taskParent.Name);
            Console.ReadLine();

            var taskChild = new AsanaTask { Name = "Subtask Test" };
            await taskParent.CreateSubtask(taskChild);

            Console.WriteLine(taskChild.Name);
            Console.WriteLine("Task and subtask added.");
            Console.ReadLine();

            // clean-up
            await taskChild.Delete();
            await taskParent.Delete();
            Console.WriteLine("Tasks deleted.");
            Console.ReadLine();
        }

        private static async Task TestCacheFilling(AsanaWorkspace workspace)
        {
            Console.WriteLine("Fetching only team IDs from Asana.");
            var teams = await workspace.GetTeams("id");

            foreach (var team in workspace.Teams)
            {
                Console.WriteLine(object.ReferenceEquals(team.Name, null) ? team.ID.ToString() : team.Name);
            }

            Console.WriteLine("Fetching projects together with their respective team names.");

            var projects = await Asana.GetProjectsInWorkspace(workspace, "name,team,team.name");
            /*
            foreach (var project in projects)
            {
                Console.WriteLine("{1}: {0}", project.Name, project.Team);
            }
             */
            Console.WriteLine("Now, all the teams that had at least one project should display with names instead of IDs.");
            foreach (var team in workspace.Teams)
            {
                Console.WriteLine(object.ReferenceEquals(team.Name, null) ? team.ID.ToString() : team.Name);
            }
        }
        private static async Task TestTeamsFetchingViaProjects(AsanaWorkspace workspace)
        {
            Console.WriteLine("Fetching projects together with their respective teams.");

            var projects = await Asana.GetProjectsInWorkspace(workspace, "name,team,team.name,workspace");

            /*
            foreach (var project in projects)
            {
                Console.WriteLine("{1}: {0}", project.Name, project.Team);
            }
             */

            foreach (var team in workspace.Teams)
            {
                Console.WriteLine(object.ReferenceEquals(team.Name, null) ? team.ID.ToString() : team.Name);
            }
        }

        private static async Task TestGetProjects(AsanaWorkspace workspace)
        {
            Console.WriteLine("Fetching from Asana.");
            var projects = await workspace.GetProjects("name,team,team.name");
//            var projects = await Asana.GetProjectsInWorkspace(workspace, "name,team,team.name");
            foreach (var project in projects)
            {
                Console.WriteLine("{1}: {0}", project.Name, object.ReferenceEquals(project.Team.Name, null) ? project.Team.ID.ToString() : project.Team.Name);
            }
            Console.ReadLine();

            Console.WriteLine("Fetching from cache.");
            projects = await workspace.GetProjects();
            foreach (var project in projects)
            {
                Console.WriteLine("{1}: {0}", project.Name, object.ReferenceEquals(project.Team.Name, null) ? project.Team.ID.ToString() : project.Team.Name);
            }

            Console.ReadLine();

            Console.WriteLine("Only updating the fetched data.");
            projects = await workspace.GetProjects(null, AsanaCacheLevel.FillExisting);
            foreach (var project in projects)
            {
                Console.WriteLine("{1}: {0}", project.Name, object.ReferenceEquals(project.Team.Name, null) ? project.Team.ID.ToString() : project.Team.Name);
            }

            Console.ReadLine();

            Console.WriteLine("Resetting everything and updating from Asana (team name shouldn't be visible).");
            projects = await workspace.GetProjects(null, AsanaCacheLevel.Ignore);
            foreach (var project in projects)
            {
                Console.WriteLine("{1}: {0}", project.Name, object.ReferenceEquals(project.Team.Name, null) ? project.Team.ID.ToString() : project.Team.Name);
            }

            Console.ReadLine();
        }

        private static async Task TestSyncProjectLoop(AsanaWorkspace workspace, string optFields)
        {
            await workspace.Sync(optFields);

            Print(workspace.Projects);
            Print(workspace.Tags);
            Console.WriteLine("omg project tasks:");
            
            Print(workspace.Projects.First(project => project.Name == "omg").GetTasks().Result);

            Console.WriteLine("Please add/rename/delete a project, tag or task from the web.");
            Console.ReadKey();
        }

//        private static void Print<what>(AsanaWorkspace workspace)
        private static void Print<T>(AsanaObjectCollection<T> collection) where T: AsanaObject
        {
            if (!object.ReferenceEquals(collection, null))
            {
                if (collection.GetType().GetGenericArguments().First() == typeof (AsanaProject))
                {
                    Console.WriteLine("\n[PROJECTS]");
                    foreach (T obj in collection)
                    {
                        Console.WriteLine("{1}\t {0}", (obj as AsanaProject).Name, obj.ID);
                    }
                }
                if (collection.GetType().GetGenericArguments().First() == typeof(AsanaTag))
                {
                    Console.WriteLine("\n[TAGS]");
                    foreach (T obj in collection)
                    {
                        Console.WriteLine("{1}\t {0}", (obj as AsanaTag).Name, obj.ID);
                    }
                }
                if (collection.GetType().GetGenericArguments().First() == typeof(AsanaTask))
                {
                    Console.WriteLine("\n[TASKS]");
                    foreach (T obj in collection)
                    {
                        Console.WriteLine("{1}\t {0}", (obj as AsanaTask).Name, obj.ID);
                        foreach (var subObj in (obj as AsanaTask).Tasks)
                        {
                            Console.WriteLine("\t\t[{1}\t {0}]", subObj.Name, subObj.ID);
                        }
                    }
                }
                if (collection.GetType().GetGenericArguments().First() == typeof(AsanaTeam))
                {
                    Console.WriteLine("\n[TEAMS]");
                    foreach (T obj in collection)
                    {
                        Console.WriteLine("{1}\t {0}", (obj as AsanaTeam).Name, obj.ID);
                    }
                }
                if (collection.GetType().GetGenericArguments().First() == typeof(AsanaWorkspace))
                {
                    Console.WriteLine("\n[WORKSPACES]");
                    foreach (T obj in collection)
                    {
                        Console.WriteLine("{1}\t {0} (isOrganization: {2})", (obj as AsanaWorkspace).Name, obj.ID, (obj as AsanaWorkspace).IsOrganization);
                    }
                }
            }
            Console.WriteLine(String.Concat(Enumerable.Repeat("-", Console.WindowWidth)));
        }

        private static async Task AdvancedTest(AsanaWorkspace workspace)
        {
//            await workspace.GetProjects();
//            await workspace.GetTags();

            await workspace.Sync();

            Print(workspace.Projects);
            Print(workspace.Tags);

            AsanaTag newtag;
            AsanaProject newproject;
            AsanaTask newtask;
//            await workspace.CreateTag(newtag = new AsanaTag { Name = "test-tag" });
            await workspace.CreateProject(newproject = new AsanaProject { Name = "test-project" });
            await workspace.CreateTask(newtask = new AsanaTask { Name = "test-task" });
            await newtask.AddProject(newproject);
//            await newtask.AddTag(newtag);

            Print(workspace.Projects);
            Print(workspace.Tags);
            Print(workspace.FetchedTasks);

            Console.ReadKey();
            Console.WriteLine("Trying to sync...\n");
            await workspace.Sync();

            Print(workspace.Projects);
            Print(workspace.Tags);
            Print(workspace.FetchedTasks);

            Console.ReadKey();
            Console.WriteLine("Deleting...\n");

//            await newtask.Delete();
            await newproject.Delete();
//            await newtag.Delete();

            Print(workspace.Projects);
            Print(workspace.Tags);
            Print(workspace.FetchedTasks);

            Console.ReadKey();
            Console.WriteLine("Trying to sync...\n");
            await workspace.Sync();

            Print(workspace.Projects);
            Print(workspace.Tags);
            Print(workspace.FetchedTasks);
        }

        private static async Task TestSyncChanges(AsanaWorkspace workspace)
        {
            var optFields =
                "created_at,type,resource,resource.name,resource.tags,resource.tags.name,resource.tags.workspace,resource.followers,resource.target,resource.text,parent,parent.name";

            //var events = workspace.GetEventLists();
            await workspace.GetProjects();
            await workspace.GetTags();

            ////

            while (true)
            {
                await TestSyncProjectLoop(workspace, optFields);
            }

            Console.WriteLine("\nPlease add/rename/delete a task from the web.");
            Console.ReadKey();

            await workspace.Sync(optFields);
            Console.WriteLine("\n");
            foreach (var task in workspace.FetchedTasks)
            {
                Console.WriteLine("{0}", task.Name);
            }
            Console.WriteLine("\nPlease add/rename/delete a task from the web.");
            Console.ReadKey();

            await workspace.Sync(optFields);
            Console.WriteLine("\n");
            foreach (var task in workspace.FetchedTasks)
            {
                Console.WriteLine("{0}", task.Name);
            }
            Console.ReadKey();
            await workspace.Sync(optFields);
            Console.WriteLine("\nSee if it fixes sync.");
            foreach (var task in workspace.FetchedTasks)
            {
                Console.WriteLine("{0}", task.Name);
            }
            Console.ReadKey();

        }

        private static async Task TestCreateProject(AsanaWorkspace workspace)
        {
            await workspace.GetProjects();
            foreach (var project in workspace.Projects)
            {
                Console.WriteLine("{0}", project.Name);
            }

            var newProject = new AsanaProject { Name = "Test Project" };

            await workspace.CreateProject(newProject);
            foreach (var project in workspace.Projects)
            {
                Console.WriteLine("{0}", project.Name);
            }
            Console.ReadKey();

            await newProject.Delete();

            foreach (var project in workspace.Projects)
            {
                Console.WriteLine("{0}", project.Name);
            }
            Console.ReadKey();
        }
        private static async Task TestUpdatingTask(Asana asana)
        {
            var task = await asana.GetTaskById(10359694701285);
            Console.WriteLine("Name: {0}\nNotes: {1}\n", task.Name, task.Notes);
            Console.ReadKey();
            task.Notes = task.Notes + "\nTEST " + DateTime.Now;
            await task.Save();
            Console.WriteLine("Name and notes for the given task should now reflect this:");
            Console.WriteLine("Name: {0}\nNotes: {1}\n", task.Name, task.Notes);
            Console.ReadKey();
            task.Notes = "";
            await task.Save();
            Console.WriteLine("Name and notes for the given task should now reflect this:");
            Console.WriteLine("Name: {0}\nNotes: {1}\n", task.Name, task.Notes);
        }
        static void Main(string[] args)
        {
            Asana = new Asana(YOUR_API_KEY, AuthenticationType.Basic, ErrorCallback);

            var workspaces = Asana.GetWorkspaces("name,is_organization").GetAwaiter().GetResult();
            Print(workspaces);
            
//            var workspace = Asana.GetWorkspaceById(YOUR_WORKSPACE).Result;
//            Console.WriteLine(workspace.Name);

//            TestGetMe().GetAwaiter().GetResult();
//            TestCreateTask(workspace).GetAwaiter().GetResult();
//            TestGetProjects(workspace).GetAwaiter().GetResult();
//            TestCreateProject(workspace).GetAwaiter().GetResult();
//            TestSyncChanges(workspace).GetAwaiter().GetResult();
//            TestCacheFilling(workspace).GetAwaiter().GetResult();
//            TestTeamsFetchingViaProjects(workspace).GetAwaiter().GetResult();
//            AdvancedTest(workspace).GetAwaiter().GetResult();
            TestUpdatingTask(Asana).GetAwaiter().GetResult();

            Console.ReadKey();
        }
        private static void ErrorCallback(string method, string requestString, HttpStatusCode statusCode, string error, int retryNo)
        {
            Console.WriteLine("[AsanaRequest-{0}-{4}]\n{1}\n{2}, {3}\n", method, requestString, statusCode, error, retryNo);
        }
    }
}
