using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tradez.Domain.Common.ValueObjects;

namespace Tradez.Domain.Entities
{
    public class Wallet : Entity
    {
        public string Address { get; set; } = string.Empty; // public blockchain address
        public string ExternalId { get; set; } // provider-specific ID
        public string OwnerId { get; set; } = string.Empty; // Keycloak user ID
        public string Provider { get; set; } = string.Empty; // e.g., Kraken, Coinbase
        public DateTime? LastSynced { get; set; }

        public List<WalletBalance> Balances { get; set; } = [];
        public List<TradeOrder> TradeOrders { get; set; } = [];
    }
}
