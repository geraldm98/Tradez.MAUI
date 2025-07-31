using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tradez.Domain.Common.Enums;

namespace Tradez.Domain.Abstractions
{
    public interface ITradeOrder
    {
        Guid Id { get; }
        string Symbol { get; }
        decimal Quantity { get; }
        TradeTypes Type { get; }
        decimal? LimitPrice { get; }
        decimal? StopLoss { get; }
        decimal? TakeProfit { get; }
        OrderTypes OrderType { get; }
        TradeOrderStatuses Status { get; }
        DateTime PlacedAt { get; }
        DateTime? ExecutedAt { get; }
        string OwnerId { get; }

        void ValidateRiskRules(decimal entryPrice);
    }
}
