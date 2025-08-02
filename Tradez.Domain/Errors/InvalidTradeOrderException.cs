using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tradez.Shared.Exceptions;

namespace Tradez.Domain.Errors
{
    internal class InvalidTradeOrderException(string message) : DomainException(message)
    {

    }
}
