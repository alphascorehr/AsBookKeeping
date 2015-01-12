using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace AlphaWebCommodityBookkeeping.Models
{
    using DalEf;
    using DevExpress.Web.ASPxEditors;
    using DevExpress.Web.Mvc;
    using System.Web.Mvc;

    public static class MjestaComboProvider
    {
        const string MDPlacesEntitiestKey = "MDPlacesEntitiestDataContext";

        public static int? MjestoId { get; set; }
        public static MDPlacesEntities DB
        {
            get
            {
                if (HttpContext.Current.Items[MDPlacesEntitiestKey] == null)
                    HttpContext.Current.Items[MDPlacesEntitiestKey] = new MDPlacesEntities();
                return (MDPlacesEntities)HttpContext.Current.Items[MDPlacesEntitiestKey];
            }
        }

        public static object GetPersonsRange(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            List<int?> listForGrid;
            if (System.Web.HttpContext.Current.Session["listMjesta"] != null)
                listForGrid = (List<int?>)System.Web.HttpContext.Current.Session["listMjesta"];
            else
                listForGrid = new List<int?>();

            if (args.Filter == "" && (MjestoId ?? 0) > 0  )
            {
                string mj = DB.MDPlaces_Enums_Geo_Place.Single(p => p.Id == MjestoId).Name;
                var skip = args.BeginIndex;
                var take = args.EndIndex - args.BeginIndex + 1;

                MjestoId = null;
                return (from mjesta in DB.MDPlaces_Enums_Geo_Place
                        where (string.Compare(mjesta.Name, mj) == 0 && mjesta.Name != "")
                        orderby mjesta.Name
                        select new { Id = mjesta.Id, Name = mjesta.Name, Zip = mjesta.ZIPCode }
                        ).Skip(skip).Take(take);
                //var rez = (from mjesta in DB.MDPlaces_Enums_Geo_Place
                //           where mjesta.Id == MjestoId
                //           orderby mjesta.Name
                //           select new { Id = mjesta.Id, Name = mjesta.Name }).Skip(skip).Take(take);

                        
            }
            else if (args.Filter == "" && listForGrid.Count > 0)
            {
                var skip = args.BeginIndex;
                var take = args.EndIndex - args.BeginIndex + 1;
                var rez = (from mjesta in DB.MDPlaces_Enums_Geo_Place
                           where listForGrid.Contains(mjesta.Id)
                           orderby mjesta.Name
                           select new { Id = mjesta.Id, Name = mjesta.Name, Zip = mjesta.ZIPCode }
                      ).Skip(skip).Take(take);

                System.Web.HttpContext.Current.Session["listMjesta"] = null;
                return rez;

                
            }
            else
            {
                var skip = args.BeginIndex;
                var take = args.EndIndex - args.BeginIndex + 1;
                return (from mjesta in DB.MDPlaces_Enums_Geo_Place
                        where mjesta.Name.StartsWith(args.Filter)
                        orderby mjesta.Name
                        select new { Id = mjesta.Id, Name = mjesta.Name, Zip = mjesta.ZIPCode }
                        ).Skip(skip).Take(take);
            }
        }
        public static object GetPersonByID(ListEditItemRequestedByValueEventArgs args)
        {
            if (args.Value != null)
            {
                int id = (int)args.Value;
                return (from mjesta in DB.MDPlaces_Enums_Geo_Place
                        where mjesta.Id == id
                        select new { Id = mjesta.Id, Name = mjesta.Name, Zip = mjesta.ZIPCode }).SingleOrDefault();
            }
            return null;
        }

        public static JsonResult SearchMjestaById(int? id)
        {
            if (id != null)
            {
                int? mjestoId = (int?)id;
                var result = (from mjesta in DB.MDPlaces_Enums_Geo_Place
                        where mjesta.Id == mjestoId
                        select new { Id = mjesta.Id, Name = mjesta.Name, Zip = mjesta.ZIPCode }).SingleOrDefault();

                if (result != null)
                    return new JsonResult { Data = new { Success = true, Results = result } };
                else
                    return new JsonResult { Data = new { Success = false } };
            }
            else
                return new JsonResult { Data = new { Success = false } };

           
        }

    }
}