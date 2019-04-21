using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalentVN.SchoolCMS.Views.Shared.Components.Nav
{
    public class NavViewComponent : ViewComponent
    {
        public NavViewComponent()
        {

        }

        public IViewComponentResult Invoke(int selectedIndex)
        {
            return View();
        }
    }
}
