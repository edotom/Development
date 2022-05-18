using LibGit2Sharp;
using LibGit2Sharp.Handlers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASGitHub
{
    // public delegate void RepoChanged;
    public class ASGitRepository
    {
        private string _path;
        private readonly Repository repo;


        public ASGitRepository(string path)
        {
            _path = path;
            try
            {
                repo = new Repository(_path);
            }
            catch (Exception ex)
            {
                if (ex.Data.Count == 0)
                {
                    GitInit();
                    repo = new Repository(_path);
                }
            }


        }
        public void InitilizeRepository()
        {
            Repository.Init(_path);
        }
        private bool ContainsGitDir(string path)
        {
            string GitRepoDirectory = path;
            if (null == GitRepoDirectory)
                return false;
            else
                return Directory.Exists(GitRepoDirectory + Path.DirectorySeparatorChar + ".git");
        }

        public Repository GetRepository(string path)
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
        public void GitInit()
        {
            try
            {
                Repository.Init(_path);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public string CommitFile(string message, string userName, string userEmail)
        {
            string result = string.Empty;
            try
            {
                using (var repo = new Repository(_path))
                {
                    repo.Commit(string.IsNullOrEmpty(message) ? "default" : message, new Signature(string.IsNullOrEmpty(userName) ? "no user" : userName, string.IsNullOrEmpty(userEmail) ? "no email" : userEmail, DateTimeOffset.Now),
                    new Signature(string.IsNullOrEmpty(userName) ? "no user" : userName, string.IsNullOrEmpty(userEmail) ? "no email" : userEmail, DateTimeOffset.Now));
                }
                result = "Successful";
            }
            catch (Exception ex)
            {
                result = "Error: " + ex.Message;
            }
            return result;
        }

        public void AddAllFilesToRepo()
        {
            try
            {
                using (var repo = new Repository(_path))
                {
                    Commands.Stage(repo, "*");
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public string AddSingleFileToRepo(string fileName)
        {
            string result = string.Empty;
            try
            {   
                GetRepository(_path);
                using (var repo = new Repository(_path))
                    {
                        Commands.Stage(repo, fileName);
                    }
                result = "Successful";
            }
            catch (Exception ex)
            {
                result = result = "Error: " + ex.Message;
            }
            return result;            
        }

        public List<Commit> GetCommits()
        {
            //using (var repo = new Repository(_path))
            //{
            return repo.Commits.ToList();
            //}
        }

        public TagCollection GetTags()
        {
            using (var repo = new Repository(_path))
            {
                return repo.Tags;
            }
        }

        public List<LogEntry> GetLogForFile(string fileName)
        {
            using (var repo = new Repository(_path))
            {
                var oLi = repo.Commits.QueryBy(fileName).ToList();
                return oLi;
            }
        }

        //public void RestoreFile(string path, string fileName, string ID)
        //{
        //    using (var repo = new Repository(path))
        //    {
        //        foreach (LogEntry entry in repo.Commits.QueryBy(fileName).ToList())
        //        {
        //            if (entry.Commit.Id.ToString().Equals(ID))
        //            {
        //                Commands.Checkout(repo, entry.Commit);    
        //            }
        //        }
        //    }            
        //}

        public void ChangeRemote(string url, string path)
        {
            using (var repo = new Repository(path))
            {
                repo.Network.Remotes.Add("origin", url);
            }

        }



        public string PushChanges(string userName, string passWord, string email, string localPath, string url)
        {
            string result = string.Empty;
            try
            {
                using (var repo = new Repository(localPath))
                {


                    string name = "origin";
                    if (CheckRemote(name, localPath, url))
                    {
                        var remote = repo.Network.Remotes.FirstOrDefault(r => r.Name == name);
                        var options = new PushOptions()
                        {
                            CredentialsProvider = (_url, _user, _cred) => new UsernamePasswordCredentials
                            {
                                Username = "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",
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
                result = "Error: " + ex.Message;
            }
            return result;
        }
        public string CheckoutChanges(string userName, string passWord, string email, string localPath, string url)
        {
            string result = string.Empty;
            try
            {
                using (var repo = new Repository(localPath))
                {
                    var myCommits = repo.Branches["remotes/origin/master"].Commits;
                    var trackingBranch = repo.Branches["remotes/origin/work-btls"];
                    if (trackingBranch.IsRemote)
                    {
                        var branch = repo.CreateBranch("SomeLocalBranchName", trackingBranch.Tip);
                        repo.Branches.Update(branch, b => b.TrackedBranch = trackingBranch.CanonicalName);
                       // repo.Checkout(new Tree {  }  branch, new CheckoutOptions { CheckoutModifiers = CheckoutModifiers.Force });
                    }
                }
                result = "Successful";
            }
            catch (Exception ex)
            {
                result = "Error: " + ex.Message;
            }
            return result;
        }
        public string FecthChanges(string userName, string passWord, string email, string localPath, string url)
        {
            string result = string.Empty;
            try
            {
                using (var repo = new Repository(localPath))
                {
                    var remote = repo.Network.Remotes["origin"];
                    repo.Network.Fetch("origin", new[] { "+refs/*:refs/*" });
                    // "origin" is the default name given by a Clone operation
                    // to the created remote
                    

                }
                result = "Successful";
            }
            catch (Exception ex)
            {
                result = "Error: " + ex.Message;
            }
            return result;
        }
        private bool CheckRemote(string remoteName, string path, string url)
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
        public string Clone(string newLocalPath, string currentRepository, string remotePath)
        {
            string result = string.Empty;
            try
            {
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
                result = "Error:" + ex.Message;

            }
            return result;
        }

    }
}
