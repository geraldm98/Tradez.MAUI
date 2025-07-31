using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tradez.Domain.Common.ValueObjects
{

    public record WalletBalance(string Symbol, decimal Amount);
}
