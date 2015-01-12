using System.Web.Mvc;

namespace AlphaWebCommodityBookkeeping.Areas.MDGeneral
{
    public class MDGeneralAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "MDGeneral";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "MDGeneral_default",
                "MDGeneral/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
