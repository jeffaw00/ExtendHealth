using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IoC;

namespace IoCMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository iRepository;
        private readonly IEmailService iEmailService;

        public HomeController(IRepository repository)
        {
            iRepository = repository;
        }

        public HomeController(IRepository repository, IEmailService emailService)
        {
            iRepository = repository;
            iEmailService = emailService;
        }

        public ActionResult Index()
        {
            return View(iRepository.Query);
        }

        public ActionResult Both()
        {
            if (iRepository.Query != null && iEmailService.Query != null)
                return View("Success");
            else
                return View("Failed");
        }
    }
}