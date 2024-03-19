namespace Domain.Exceptions
{
    public sealed class RoleNotFoundExceptions : NotFoundException
    {
        public RoleNotFoundExceptions(int personId)
            : base($"La persona con id {personId} no fue encontrada.")
        {
        }
    }
}

