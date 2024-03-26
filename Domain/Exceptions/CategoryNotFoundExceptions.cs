namespace Core.Domain.Exceptions
{
public sealed class CategoryNotFoundExceptions : NotFoundException
{
    public CategoryNotFoundExceptions(int categoryId)
        : base($"La categoria con id {categoryId} no fue encontrada.")
    {
    }
}
}