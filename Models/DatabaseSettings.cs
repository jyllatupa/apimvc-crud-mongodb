namespace API_CRUD_MongoDB.Models
{
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; } = null!; //distinto a null
        public string DatabaseName { get; set; } = null!;
        public string CollectionName { get; set; } = null!;
    }
}
