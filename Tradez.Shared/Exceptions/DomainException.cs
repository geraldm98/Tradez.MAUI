using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tradez.Shared.Exceptions
{
    public abstract class DomainException(string message) : Exception(message)
    {

    }
}
