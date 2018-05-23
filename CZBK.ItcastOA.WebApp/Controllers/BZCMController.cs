using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.ItcastOA.WebApp.Controllers
{
    public class BZCMController : BaseController
    {
        //
        // GET: /BZCM/
        IBLL.IBzcmText_FanChanService BzcmText_FanChanService { get; set; }
        IBLL.IIsFristItemService IsFristItemService { get; set; }
        IBLL.IWx__BzcmTextService Wx__BzcmTextService { get; set; }

        public ActionResult Index()
        {
            return View();
        }

    }
}
