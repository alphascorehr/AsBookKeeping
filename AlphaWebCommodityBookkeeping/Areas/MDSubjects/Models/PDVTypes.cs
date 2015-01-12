using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlphaWebCommodityBookkeeping.Areas.MDSubjects.Models
{
    public class PDVTypes : List<PDVType>
    {
        public PDVTypes()
        {
            PDVType item = new PDVType();
            item.Id = 0;
            item.Name = "R1";
            this.Add(item);

            item = new PDVType();
            item.Id = 1;
            item.Name = "R2";
            this.Add(item);
        }
        
        
        
    }

    public class PDVType
    {
        public byte Id { get; set; }
        public string Name { get; set; }

        public PDVType()
        {
        }
    }
}