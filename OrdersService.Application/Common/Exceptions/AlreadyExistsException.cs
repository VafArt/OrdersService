namespace OrdersService.Application.Common.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string name, object key)
            : base($"Сущность \"{name}\" ({key}) уже существует")
        {

        }
    }
}
