using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tradez.Domain.Common.ValueObjects;
using Tradez.Domain.Events;

namespace Tradez.Domain.Entities
{
    public class Wallet : Entity
    {
        private string _address = string.Empty;

        public string Address => _address; // read-only outside
        public string OwnerId { get; private set; } = string.Empty;
        public string Provider { get; private set; } = string.Empty;

        public List<WalletBalance> Balances { get; private set; } = [];
        public List<TradeOrder> TradeOrders { get; private set; } = [];

        public DateTime? LastSynced { get; private set; }

        // Factory method to ensure validation
        public static Wallet Create(string address, string ownerId, string provider)
        {
            if (string.IsNullOrWhiteSpace(address)) throw new ArgumentException("Wallet address is required.");
            if (string.IsNullOrWhiteSpace(ownerId)) throw new ArgumentException("Owner ID is required.");
            if (string.IsNullOrWhiteSpace(provider)) throw new ArgumentException("Provider is required.");

            var wallet = new Wallet
            {
                _address = address.Trim(),
                OwnerId = ownerId,
                Provider = provider
            };

            wallet.AddDomainEvent(new WalletCreated(wallet.Id));

            return wallet;

        }
    }
}
