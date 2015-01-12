using System.Web.Mvc;

namespace AlphaWebCommodityBookkeeping.Areas.MDSubjects
{
    public class MDSubjectsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "MDSubjects";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "MDSubjects_default",
                "MDSubjects/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
