using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Domain.Aggregates
{
    public interface IAggregateRoot
    {
        // This is a marker interface, so it doesn't need any methods or properties.
        // It signals that a class is the root of an aggregate.
    }
}
