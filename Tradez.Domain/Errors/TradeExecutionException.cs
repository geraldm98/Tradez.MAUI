using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tradez.Shared.Exceptions;

namespace Tradez.Domain.Errors
{
    internal class TradeExecutionException(string message) : DomainException(message)
    {

    }
}
