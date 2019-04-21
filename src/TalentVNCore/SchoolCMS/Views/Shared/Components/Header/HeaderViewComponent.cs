using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCMS.Views.Shared.Components.Header
{
    public class HeaderViewComponent : ViewComponent
    {
        public HeaderViewComponent()
        {

        }

        public IViewComponentResult Invoke(string strHeader)
        {
            ViewData["Header"] = strHeader;

            return View();
        }
    }
}
