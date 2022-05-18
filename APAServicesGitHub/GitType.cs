using Direct.Shared;

namespace Direct.APAServicesGit.Library
{
    [DirectDom("Git Type", "Predefined Git Type", false)]
    [IsCatalog]
    [TableName("Git Type")]
    public class GitType : BaseEntity
    {
        protected PropertyHolder<string> _UserName = new PropertyHolder<string>(nameof(UserName));
        protected PropertyHolder<string> _UserEmail = new PropertyHolder<string>(nameof(UserEmail));
        protected PropertyHolder<string> _LocalRepository = new PropertyHolder<string>(nameof(LocalRepository));
        protected PropertyHolder<string> _NewLocalRepository = new PropertyHolder<string>(nameof(NewLocalRepository));
        protected PropertyHolder<string> _RemoteRepository = new PropertyHolder<string>(nameof(RemoteRepository));
        protected PropertyHolder<string> _GPGKey = new PropertyHolder<string>(nameof(GPGKey));      

        public GitType()
        {

        }

        public GitType(IProject project) : base(project)
        {

        }



        [DirectDom("UserName")]
        [DesignTimeInfo("User Name")]
        [Collectable]
        public string UserName
        {
            get
            {
                return this._UserName.TypedValue;
            }
            set
            {
                this._UserName.TypedValue = value;
            }
        }

        [DirectDom("UserEmail")]
        [DesignTimeInfo("User Email")]
        [Collectable]
        public string UserEmail
        {
            get
            {
                return this._UserEmail.TypedValue;
            }
            set
            {
                this._UserEmail.TypedValue = value;
            }
        }

        [DirectDom("LocalRepository")]
        [DesignTimeInfo("Local Repository")]
        [Collectable]
        public string LocalRepository
        {
            get
            {
                return this._LocalRepository.TypedValue;
            }
            set
            {
                this._LocalRepository.TypedValue = value;
            }
        }

        [DirectDom("NewLocalRepository")]
        [DesignTimeInfo("New Local Repository")]
        [Collectable]
        public string NewLocalRepository
        {
            get
            {
                return this._NewLocalRepository.TypedValue;
            }
            set
            {
                this._NewLocalRepository.TypedValue = value;
            }
        }

        [DirectDom("RemoteRepository")]
        [DesignTimeInfo("RemoteRepository")]
        [Collectable]
        public string RemoteRepository
        {
            get
            {
                return this._RemoteRepository.TypedValue;
            }
            set
            {
                this._RemoteRepository.TypedValue = value;
            }
        }

        [DirectDom("GPGKey")]
        [DesignTimeInfo("GPGKey")]
        [Collectable]
        public string GPGKey
        {
            get
            {
                return this._GPGKey.TypedValue;
            }
            set
            {
                this._GPGKey.TypedValue = value;
            }
        }


    }
}
