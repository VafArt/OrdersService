namespace OrdersService.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public string Entity { get; set; }

        public string Key { get; set; }

        public NotFoundException(string entity, string key)
            : base($"\"{entity}\" ({key}) not found.")
        {
            Entity = entity;
            Key = key;
        }
    }
}
