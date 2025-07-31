using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tradez.Domain.Abstractions;
using Tradez.Domain.Common.Enums;
using Tradez.Domain.Errors;
using Tradez.Shared.Exceptions;

namespace Tradez.Domain.Entities
{
    public class TradeOrder : Entity, ITradeOrder
    {
        public TradeTypes Type { get; set; } // Buy or Sell
        public string Symbol { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal? LimitPrice { get; set; } // null = market order

        public decimal? StopLoss { get; set; }
        public decimal? TakeProfit { get; set; }

        public TradeOrderStatuses Status { get; set; } = TradeOrderStatuses.Pending;
        public DateTime PlacedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ExecutedAt { get; set; }

        public string OwnerId { get; set; } = string.Empty;

        public OrderTypes OrderType => LimitPrice is null ? OrderTypes.Market : OrderTypes.Limit;

        public void ValidateRiskRules(decimal entryPrice)
        {
            if (StopLoss is not null && StopLoss <= 0)
                throw new InvalidTradeOrderException("Stop loss must be greater than 0.");

            if (TakeProfit is not null && TakeProfit <= 0)
                throw new InvalidTradeOrderException("Take profit must be greater than 0.");

            if (Type == TradeTypes.Buy)
            {
                if (StopLoss is not null && StopLoss >= entryPrice)
                    throw new InvalidTradeOrderException("Stop loss must be below entry price for buy orders.");
                if (TakeProfit is not null && TakeProfit <= entryPrice)
                    throw new InvalidTradeOrderException("Take profit must be above entry price for buy orders.");
            }
            else if (Type == TradeTypes.Sell)
            {
                if (StopLoss is not null && StopLoss <= entryPrice)
                    throw new InvalidTradeOrderException("Stop loss must be above entry price for sell orders.");
                if (TakeProfit is not null && TakeProfit >= entryPrice)
                    throw new InvalidTradeOrderException("Take profit must be below entry price for sell orders.");
            }
        }
    }
}
