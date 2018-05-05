using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using HairSalonApp;

namespace HairSalonApp.Controllers
{
    public class HairSalonController : Controller
    {
      [HttpGet("/home")]
      public ActionResult Index()
      {
          List<Stylist> allStylists = Stylist.GetAllStylists();
          List<Client> allClients = Client.GetAllClients();
          Dictionary<string,object> all = new Dictionary<string,object> ();
          all.Add("stylists", allStylists);
          all.Add("clients", allClients);
          return View(all);
      }
    }
}
