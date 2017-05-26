using MintFinancialExport.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MintFinancialExport.Core
{
    public class PreciousMetalsPriceApi
    {
        public decimal? GetPreciousMetalsPrice(Enums.PreciousMetalsTypes metalsType)
        {
            string value = "0";

            WebClient client = new WebClient();
            string file = client.DownloadString("http://www.kitco.com/charts/live" + metalsType.ToString() + ".html");

            MatchCollection m1 = Regex.Matches(file, @"<span[^>].*?>([^<]*)<\/span>",
            RegexOptions.Singleline);

            foreach (Match m in m1)
            {
                if (m.Value.Contains("sp-bid"))
                {
                    value = m.Value.Replace("<span id=\"sp-bid\">", "").Replace("</span>", "");
                }
            }

            return Convert.ToDecimal(value);
        }
    }
}
