namespace OrdersService.Application.Common.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        public string Entity { get; set; }

        public string Key { get; set; }

        public AlreadyExistsException(string entity, string key)
            : base($"\"{entity}\" ({key}) already exists")
        {
            Entity = entity;
            Key = key;
        }
    }
}
