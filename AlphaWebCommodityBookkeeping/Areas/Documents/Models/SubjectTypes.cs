using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlphaWebCommodityBookkeeping.Areas.Documents.Models
{
    public class SubjectTypes : List<SubjectType>
    {
        public SubjectTypes()
        {
            SubjectType item = new SubjectType();
            item.Id = 0;
            item.Name = "Osoba";
            this.Add(item);

            item = new SubjectType();
            item.Id = 1;
            item.Name = "Tvrtka";
            this.Add(item);

            item = new SubjectType();
            item.Id = 2;
            item.Name = "Obrt";
            this.Add(item);

            item = new SubjectType();
            item.Id = 3;
            item.Name = "Slobodna djelatnost";
            this.Add(item);
        }
    }

    public class SubjectType
    {
        public Int32 Id { get; set; }
        public string Name { get; set; }

        public SubjectType()
        {
        }
    }
}