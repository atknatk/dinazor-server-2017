using System;

namespace Dinazor.Core.Interfaces.Databases
{
    public interface IEntity<TId> where TId : IComparable
    {
        TId Id { get; set; }
    }

    public interface IEntity : IEntity<long>
    {

    }
}
