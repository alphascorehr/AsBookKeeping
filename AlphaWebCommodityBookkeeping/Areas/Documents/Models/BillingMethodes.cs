using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlphaWebCommodityBookkeeping.Areas.Documents.Models
{
    public class BillingMethodes : List<BillingMethode>
    {
        public BillingMethodes()
        {
            BillingMethode newItem = new BillingMethode();
            newItem.Id = 0;
            newItem.Name = "Hourly staff rate";

            this.Add(newItem);

            newItem = new BillingMethode();
            newItem.Id = 1;
            newItem.Name = "Hourly task rate";

            this.Add(newItem);

            newItem = new BillingMethode();
            newItem.Id = 2;
            newItem.Name = "Hourly project rate";

            this.Add(newItem);

            newItem = new BillingMethode();
            newItem.Id = 3;
            newItem.Name = "Flat project amount";

            this.Add(newItem);

        }
    }
   public class BillingMethode
    {
        public byte Id { get; set; }
        public string Name { get; set; }

        public BillingMethode()
        {
        }
    }
}