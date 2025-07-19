using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tradez.Domain.Abstractions;

namespace Tradez.Domain.Common.Analysis
{
    public record SocialStats(
        int TwitterFollowers,
        int RedditSubscribers,
        int GithubStars,
        int GithubForks
    ) : ITradeAnalysisComponent
    {
        public static SocialStats Default => new(0, 0, 0, 0);
        public bool IsPlaceholder =>
            TwitterFollowers == 0 &&
            RedditSubscribers == 0 &&
            GithubStars == 0 &&
            GithubForks == 0;
    }
}
