using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrezziSubito.Helpers
{
    public static class PageHelpers
    {
        public static int CalculateTotalAds(HtmlDocument htmlDoc)
        {
            var totalAddsElement = htmlDoc.DocumentNode.QuerySelector("p.total-ads").InnerText.ToString();

            var totalAddsFound = totalAddsElement.Substring(0, totalAddsElement.IndexOf(" "));

            int.TryParse(totalAddsFound, out int totalAdds);

            return totalAdds;
        }

        public static List<decimal> FindPrices(HtmlDocument htmlDoc)
        {
            var prices = new List<decimal> { };

            var priceNodes = htmlDoc.DocumentNode.QuerySelectorAll("p.price");

            foreach (var node in priceNodes)
            {
                var positionForTrimming = 0;

                var elementText = node.InnerText;

                positionForTrimming = elementText.IndexOf("€");

                if (positionForTrimming == -1) continue;

                Decimal.TryParse(node.InnerText.Trim().Substring(0, positionForTrimming - 1), out Decimal price);

                prices.Add(price);

            }

            return prices;
        }

        public static HtmlDocument LoadDocument(string url)
        {
            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(url);

            return htmlDoc;
        }
    }
}
