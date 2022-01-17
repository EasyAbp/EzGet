namespace EasyAbp.EzGet
{
    public static class EzGetErrorCodes
    {
        //Add your business exception error codes here...
        public const string PackageAlreadyExisted = "EasyAbp.EzGet:PackageAlreadyExisted";
        public const string PackageAlreadyUnlisted = "EasyAbp.EzGet:PackageAlreadyUnlisted";
        public const string PackageAlreadyListed = "EasyAbp.EzGet:PackageAlreadyListed";
        public const string FeedCannotAddOtherUserCredential = "EasyAbp.EzGet:FeedCannotAddOtherUserCredential";
        public const string FeedNameAlreadyExisted = "EasyAbp.EzGet:FeedNameAlreadyExisted";
        public const string UserNotFound = "EasyAbp.EzGet:UserNotFound";
        public const string NoAuthorizeHandleThisFeed = "EasyAbp.EzGet:NoAuthorizeHandleThisFeed";
        public const string NoAuthorizeHandleThisCredential = "EasyAbp.EzGet:NoAuthorizeHandleThisCredential";
    }
}
