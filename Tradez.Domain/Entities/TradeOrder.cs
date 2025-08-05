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
        public Guid WalletId { get; private set; }
        public string OwnerId { get; private set; } = string.Empty;

        public TradeTypes Type { get; private set; } // Buy or Sell
        public string Symbol { get; private set; } = string.Empty;
        public decimal Quantity { get; private set; }
        public decimal? LimitPrice { get; private set; } // null = market order

        public decimal? StopLoss { get; private set; }
        public decimal? TakeProfit { get; private set; }

        public TradeOrderStatuses Status { get; private set; } = TradeOrderStatuses.Pending;
        public DateTime PlacedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? ExecutedAt { get; private set; }

        public OrderTypes OrderType => LimitPrice is null ? OrderTypes.Market : OrderTypes.Limit;

        private TradeOrder() { }

        public static TradeOrder Create(
            Guid walletId,
            TradeTypes type,
            string symbol,
            decimal quantity,
            string ownerId,
            decimal? limitPrice = null,
            decimal? stopLoss = null,
            decimal? takeProfit = null)
        {
            if (quantity <= 0)
                throw new InvalidTradeOrderException("Trade quantity must be greater than zero.");

            var order = new TradeOrder
            {
                WalletId = walletId,
                Type = type,
                Symbol = symbol,
                Quantity = quantity,
                OwnerId = ownerId,
                LimitPrice = limitPrice,
                StopLoss = stopLoss,
                TakeProfit = takeProfit,
                Status = TradeOrderStatuses.Pending,
                PlacedAt = DateTime.UtcNow
            };

            order.ValidateRiskRules(limitPrice ?? 0);
            return order;
        }

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
