using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HatWorks.Tools
{
    public class QueryHelper
    {
        public static string GetQuery(string query)
        {
            string result = "";
            switch (query)
            {
                case "tbl_carisinifQuery":
                    result = string.Format(@"(SELECT sinif.idd,sinif.Ad,sinif.ID,sinif.GrupID,sinif.FirmaID, grup.Ad GrupAd FROM tbl_carisinif sinif  JOIN tbl_carigrup grup  ON sinif.GrupID=grup.ID) t");
                    break;
                case "storelistQuery":
                    result = string.Format(@"(SELECT m.Ad AS Ad,hrk.Depo AS Depo,SUM(hrk.Giren - hrk.Cikan) AS Miktar,m.ID AS ID FROM (tbl_marketstokhareket hrk LEFT JOIN tbl_marketstok m ON (m.ID = hrk.StokID)) GROUP BY m.ID , hrk.DepoID) t");
                    break;
                case "tbl_marketstokQuery":
                    result = string.Format(@"(SELECT * FROM tbl_marketstok ORDER BY kayitTarihi Desc) t");
                    break;
            }

            if (result == "")
                result = string.Format("(SELECT * FROM {0}) t", query);

            return result;
        }

        public static string GetFilter(string query, List<string> additionalvalues)
        {
            string result = "";
            switch (query)
            {
                case "tbl_carihareket":
                    result = "t.ID=" + additionalvalues.First();
                    break;
            }
            return result;

        }
    }
}
