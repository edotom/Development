using Direct.Shared;
using LibGit2Sharp;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Direct.APAServicesGit.Library
{

    [DirectSealed]
    [DirectDom("APA Services GitHub Helper", "APA Services GitHub", false)]
    [ParameterType(false)]
    public class APAGitHubHelper
    {
        //private static string _path;
        //private readonly Repository repo;
        private static readonly ILog logArchitect = LogManager.GetLogger(Loggers.LibraryObjects);
        //public APAGitHubHelper(string path)
        //{
        //    _path = path;

        //    try
        //    {
        //        repo = new Repository(_path);
        //    }
        //    catch (Exception ex)
        //    {

        //        if (ex.Data.Count == 0)
        //        {
        //            GitInit();
        //            repo = new Repository(_path);
        //        }
        //    }


        //}
        //public void InitilizeRepository()
        //{
        //    Repository.Init(_path);
        //}
        private static bool ContainsGitDir(string path)
        {
            string GitRepoDirectory = path;
            if (null == GitRepoDirectory)
                return false;
            else
                return Directory.Exists(GitRepoDirectory + Path.DirectorySeparatorChar + ".git");
        }

        private static Repository GetRepository(string path)
        {
            string GitRepoDirectory = path;
            if (null == GitRepoDirectory)
                return null;
            if (!ContainsGitDir(path))
            {
                Repository.Init(GitRepoDirectory);
            }
            return new Repository(GitRepoDirectory);
        }
        //private void GitInit()
        //{
        //    try
        //    {
        //        Repository.Init(_path);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }

        //}

        [MethodDescriptionAttribute("Commit Changes to Local Repository")]
        [DirectDom("Commit local changes", "APA Github helper", false)]
        [DirectDomMethod("Enter commit comment {String}, Enter User Name {String}, Enter User Email {String}, Enter Repository path {String}")]
        public static string CommitFile(string message, string userName, string userEmail, string path)
        {
            string result = string.Empty;
            try
            {
                if (logArchitect.IsDebugEnabled)
                {
                    logArchitect.DebugFormat("APAGitHubHelper CommitFile - Started");
                }
                using (var repo = new Repository(path))
                {
                    repo.Commit(string.IsNullOrEmpty(message) ? "default" : message, new Signature(string.IsNullOrEmpty(userName) ? "no user" : userName, string.IsNullOrEmpty(userEmail) ? "no email" : userEmail, DateTimeOffset.Now),
                    new Signature(string.IsNullOrEmpty(userName) ? "no user" : userName, string.IsNullOrEmpty(userEmail) ? "no email" : userEmail, DateTimeOffset.Now));
                }
                result = "Successful";
            }
            catch (Exception ex)
            {
                logArchitect.Error("APAGitHubHelper Constructor - failed with Exception", ex);
                result = "Error: " + ex.Message;
            }
            return result;

        }

        //public void AddAllFilesToRepo()
        //{
        //    try
        //    {
        //        using (var repo = new Repository(_path))
        //        {
        //            Commands.Stage(repo, "*");
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}
        [MethodDescriptionAttribute("Add Single File to Local Repository")]
        [DirectDom("Add Single File to Local Repository", "APA Github Helper", false)]
        [DirectDomMethod("Enter Filename{String}, Enter Repository path {String}")]
        public static string AddSingleFileToRepo(string fileName, string path)
        {
            string result = string.Empty;
            try
            {
                if (logArchitect.IsDebugEnabled)
                {
                    logArchitect.DebugFormat("APAGitHubHelper AddSingleFileToRepo - Started");
                }
                GetRepository(path);
                using (var repo = new Repository(path))
                {
                    Commands.Stage(repo, fileName);
                }
                result = "Successful";
            }
            catch (Exception ex)
            {
                logArchitect.Error("APAGitHubHelper AddSingleFileToRepo - failed with Exception", ex);
                result = "Error: " + ex.Message;
            }
            return result;
        }
        //public List<Commit> GetCommits()
        //{
        //    //using (var repo = new Repository(_path))
        //    //{
        //    return repo.Commits.ToList();
        //    //}
        //}

        //public TagCollection GetTags()
        //{
        //    using (var repo = new Repository(_path))
        //    {
        //        return repo.Tags;
        //    }
        //}

        //public List<LogEntry> GetLogForFile(string fileName)
        //{
        //    using (var repo = new Repository(_path))
        //    {
        //        var oLi = repo.Commits.QueryBy(fileName).ToList();
        //        return oLi;
        //    }
        //}


        private static void ChangeRemote(string url, string path)
        {
            using (var repo = new Repository(path))
            {
                repo.Network.Remotes.Add("origin", url);
            }

        }

        [MethodDescriptionAttribute("Push Changes to Remote Repository")]
        [DirectDom("Push Changes to Remote Repository", "APA Github Helper", false)]
        [DirectDomMethod("Enter Repository Key{String}, Enter user Email{String}, Enter Local Path {String}, Enter remote url{String}")]
        public static string PushChanges(string gpKey, string email, string localPath, string url)
        {
            string result = string.Empty;
            try
            {
                if (logArchitect.IsDebugEnabled)
                {
                    logArchitect.DebugFormat("APAGitHubHelper PushChanges - Started");
                }
                using (var repo = new Repository(localPath))
                {
                    string name = "origin";
                    if (CheckRemoteExists(name, localPath, url))
                    {
                        var remote = repo.Network.Remotes.FirstOrDefault(r => r.Name == name);
                        var options = new PushOptions()
                        {
                            CredentialsProvider = (_url, _user, _cred) => new UsernamePasswordCredentials
                            {
                                Username = gpKey,
                                Password = ""
                            }
                        };
                        string pushRefSpec = @"refs/heads/master";
                        repo.Network.Push(remote, pushRefSpec, options);

                    }
                }
                result = "Successful";
            }
            catch (Exception ex)
            {
                logArchitect.Error("APAGitHubHelper PushChanges - failed with Exception", ex);
                result = "Error: "+ ex.Message;
            }
            return result;
        }
        private static bool CheckRemoteExists(string remoteName, string path, string url)
        {
            try
            {
                using (var repo = new Repository(path))
                {
                    string name = "origin";
                    var remote = repo.Network.Remotes.FirstOrDefault(r => r.Name == remoteName);
                    if (remote == null)
                    {
                        repo.Network.Remotes.Add(name, url);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [MethodDescriptionAttribute("Clone Remote Repository to a local folder")]
        [DirectDom("Clone Remote Repository to a local folder", "APA Github Helper", false)]
        [DirectDomMethod("Enter New Local Folder{String}, Enter Current Repository {string}, Enter Remote Repository {string}")]
        public static string Clone(string newLocalPath, string currentRepository, string remotePath)
        {
            string result = string.Empty;
            try
            {
                if (logArchitect.IsDebugEnabled)
                {
                    logArchitect.DebugFormat("APAGitHubHelper Clone - Started");
                }
                using (var repo = new Repository(currentRepository))
                {
                    string name = "origin";
                    //repo.Network.Remotes.Add(name, url);
                    var remote = repo.Network.Remotes.FirstOrDefault(r => r.Name == name);
                    //Remote remote = repo.Network.Remotes["origin"];                    
                    var options = new CloneOptions()
                    {
                        CredentialsProvider = (_url, _user, _cred) => new UsernamePasswordCredentials
                        {
                            //this section is related to the gpg key extracted from github settings for the account
                            Username = "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",
                            Password = ""
                        }
                    };
                    // string pushRefSpec = @"refs/heads/master";
                    Repository.Clone(remotePath, newLocalPath, options);
                }
                result = "Successful";
            }
            catch (Exception ex)
            {
                logArchitect.Error("APAGitHubHelper Clone - failed with Exception", ex);
                result = "Error: " + ex.Message;
            }
            return result;
        }
    }
}


