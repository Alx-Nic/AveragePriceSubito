using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
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
                var elementText = node.InnerText;

                if (string.IsNullOrWhiteSpace(elementText)) continue;

                string charsToTrim = ".€ ";

                string parsedPrice = CleanString(charsToTrim, elementText).Trim();

                var numberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = ","};

                var parseToDecimal = Decimal.Parse(parsedPrice, numberFormatInfo);

                prices.Add(parseToDecimal);

            }

            return prices;
        }

        public static HtmlDocument LoadDocument(string url)
        {
            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(url);

            return htmlDoc;
        }

        public static string CleanString(string charsToClean, string stringToBeClened)
        {
            string result = stringToBeClened;

            foreach (char c in charsToClean)
            {
                result = result.Replace(c.ToString(), string.Empty);
            }

            return result;
        }
    }
}
