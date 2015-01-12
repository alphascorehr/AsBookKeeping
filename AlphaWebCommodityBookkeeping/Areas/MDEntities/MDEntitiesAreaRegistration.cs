using System.Web.Mvc;

namespace AlphaWebCommodityBookkeeping.Areas.MDEntities
{
    public class MDEntitiesAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "MDEntities";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "MDEntities_default",
                "MDEntities/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
