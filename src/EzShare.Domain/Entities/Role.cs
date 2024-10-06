using EzShare.Domain.Common;

namespace EzShare.Domain.Entities;

public class Role : BaseEntity
{
    public string Name { get; init; }
}