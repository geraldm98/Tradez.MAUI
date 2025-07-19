using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tradez.Domain.Common.ValueObjects
{
    public record AssetMetadata(
        string Category,
        int DeveloperScore,
        int CommunityScore
    )
    {
        public static AssetMetadata Default => new("Unknown", 0, 0);
    }
}
