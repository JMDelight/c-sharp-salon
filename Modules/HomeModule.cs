using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;
using System;

namespace HairSalon
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => View["index.cshtml"];

      Get["/stylists"] = _ => {
        List<Stylist> allStylists = Stylist.GetAll();
        return View["stylists.cshtml", allStylists];
      };

      Get["/stylists/new"] = _ => View["stylists_form.cshtml"];

      Post["/stylist/success"] = _ => {
        Stylist newStylist = new Stylist(Request.Form["stylist-name"]);
        newStylist.Save();
        List<Stylist> allStylists = Stylist.GetAll();
        return View["stylists.cshtml", allStylists];
      };

      Get["/stylists/{id}"] = parameters => {
        Stylist foundStylist = Stylist.Find(parameters.id);
        return View["stylist.cshtml", foundStylist];
      };


    }
  }
}
