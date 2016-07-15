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

      Get["/clients/new"] = _ => View["clients_form.cshtml"];

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

      Patch["/stylist/success"] = _ => {
        Stylist editedStylist = Stylist.Find(Request.Form["stylist-id"]);
        editedStylist.Update(Request.Form["stylist-name"]);
        List<Stylist> allStylists = Stylist.GetAll();
        return View["stylists.cshtml", allStylists];
      };

      Get["/stylists/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Stylist foundStylist = Stylist.Find(parameters.id);
        List<Client> stylistsClients = foundStylist.GetClients();
        model.Add("stylist", foundStylist);
        model.Add("clients", stylistsClients);
        return View["stylist.cshtml", model];
      };

      Get["/stylists/edit/{id}"] = parameters => {
        Stylist foundStylist = Stylist.Find(parameters.id);
        return View["stylists_edit.cshtml", foundStylist];
      };


    }
  }
}
