namespace Domain.Exceptions
{
    public sealed class PersonNotFoundExceptions : NotFoundException
    {
        public PersonNotFoundExceptions(int personId)
            : base($"La persona con id {personId} no fue encontrada.")
        {
        }
    }
}

