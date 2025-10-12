using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axolotl.Application.Dtos;

public record ProductsDto(
    long Id,
    Guid Uuid,
    string Code,
    string? Name,
    bool Active,
    long? CategoryId
);

