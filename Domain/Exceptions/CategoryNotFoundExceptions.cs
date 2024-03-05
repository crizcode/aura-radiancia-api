using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
public sealed class CategoryNotFoundExceptions : NotFoundException
{
    public CategoryNotFoundExceptions(int categoryId)
        : base($"La categoria con id {categoryId} no fue encontrado.")
    {
    }
}
}