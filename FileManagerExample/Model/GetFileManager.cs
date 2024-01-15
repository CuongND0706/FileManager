namespace FileManagerExample.Model
{
    public class GetFileManager
    {
        public class ErrorInfo
        {
            public string code { get; set; }
            public string description { get; set; }
            public InvalidParams[] invalidParams { get; set; }
            public string reason { get; set; }
        }

        public class InvalidParams
        {
            public string names { get; set; }
        }

        public string fileName { get; set; }
        public string fileType { get; set; }
        public string updatedByUserName { get; set; }
        public string updatedAt { get; set; }
        public string fileManagerURL { get; set; }
        public ErrorInfo[] errorInfoList { get; set; }
    }

    public class Profile
    {
        public string display_name { get; set; }
        public string website { get; set; }
    }

    public class ResourceOwner
    {
        public string id { get; set; }
        public string group_id { get; set; }
        public string user_id { get; set; }
        public string author_id { get; set; }
        public Profile profile { get; set; }
        public string Type { get; set; }
    }

    public class GetResoureResponse
    {
        public int Total { get; set; }
        public List<ResourceOwner> resource_owners { get; set; }
    }

    public class CreatedBy
    {
        public User User { get; set; }
        public Client Client { get; set; }
    }

    public class UpdatedBy
    {
        public User User { get; set; }
        public Client Client { get; set; }
    }

    public class User
    {
        public string Id { get; set; }
    }

    public class Client
    {
        public string ClientId { get; set; }
        public bool IsDevice { get; set; }
    }

    public class Files
    {
        public string TotalCount { get; set; }
        public string TotalSize { get; set; }
    }

    public class Bucket
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ResourceOwnerId { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public CreatedBy CreatedBy { get; set; }
        public UpdatedBy UpdatedBy { get; set; }
        public Files Files { get; set; }
    }

    public class BucketResponse
    {
        public List<Bucket> Buckets { get; set; }
        public int Total { get; set; }
    }
}
