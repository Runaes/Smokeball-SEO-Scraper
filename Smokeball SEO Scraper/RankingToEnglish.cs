using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smokeball_SEO_Scraper
{
    static class RankingToEnglish
    {
        public static string RankingToEnglishConverter(this int ranking)
        {
            var lastDigit = ranking % 10;
            switch (lastDigit)
            {
                case 1:
                    return $"{ranking}st";
                case 2:
                    return $"{ranking}nd";
                case 3:
                    return $"{ranking}rd";
                default:
                    return $"{ranking}th";
            }
        }
    }
}
