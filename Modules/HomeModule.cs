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

      Get["/clients"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        List<Client> allClients = Client.GetAll();
        List<Stylist> allStylists = Stylist.GetAll();
        model.Add("stylists", allStylists);
        model.Add("clients", allClients);

        return View["clients.cshtml", model];
      };

      Post["/clients"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Client newClient = new Client(Request.Form["client-name"], Request.Form["stylist-id"]);
        newClient.Save();
        List<Stylist> allStylists = Stylist.GetAll();
        List<Client> allClients = Client.GetAll();
        model.Add("stylists", allStylists);
        model.Add("clients", allClients);
        return View["clients.cshtml", model];
      };

      Patch["/clients"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Client editedClient =Client.Find(Request.Form["client-id"]);
        editedClient.Update(Request.Form["client-name"], Request.Form["stylist-id"]);
        List<Client> allClients = Client.GetAll();
        List<Stylist> allStylists = Stylist.GetAll();
        model.Add("stylists", allStylists);
        model.Add("clients", allClients);

        return View["clients.cshtml", model];
      };

      Get["/client/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Client foundClient = Client.Find(parameters.id);
        Stylist clientsStylist = Stylist.Find(foundClient.GetStylistId());
        model.Add("client", foundClient);
        model.Add("stylist", clientsStylist);
        return View["client.cshtml", model];
      };

      Delete["/client/{id}"] = parameters => {
        Client clientToDelete = Client.Find(parameters.id);
        Stylist foundStylist = Stylist.Find(clientToDelete.GetStylistId());
        clientToDelete.Delete();
        Dictionary<string, object> model = new Dictionary<string, object>();
        List<Client> stylistsClients = foundStylist.GetClients();
        model.Add("stylist", foundStylist);
        model.Add("clients", stylistsClients);
        return View["stylist.cshtml", model];
      };

      Get["/client/edit/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Client foundClient = Client.Find(parameters.id);
        List<Stylist> allStylists = Stylist.GetAll();
        model.Add("client", foundClient);
        model.Add("stylists", allStylists);
        return View["client_edit.cshtml", model];
      };

      Get["/stylists"] = _ => {
        List<Stylist> allStylists = Stylist.GetAll();
        return View["stylists.cshtml", allStylists];
      };

      Post["/stylists"] = _ => {
        Stylist newStylist = new Stylist(Request.Form["stylist-name"]);
        newStylist.Save();
        List<Stylist> allStylists = Stylist.GetAll();
        return View["stylists.cshtml", allStylists];
      };

      Patch["/stylists"] = _ => {
        Stylist editedStylist = Stylist.Find(Request.Form["stylist-id"]);
        editedStylist.Update(Request.Form["stylist-name"]);
        List<Stylist> allStylists = Stylist.GetAll();
        return View["stylists.cshtml", allStylists];
      };

      Get["/stylist/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Stylist foundStylist = Stylist.Find(parameters.id);
        List<Client> stylistsClients = foundStylist.GetClients();
        model.Add("stylist", foundStylist);
        model.Add("clients", stylistsClients);
        return View["stylist.cshtml", model];
      };

      Delete["/stylist/{id}"] = parameters => {
        Stylist stylistToDelete = Stylist.Find(parameters.id);
        stylistToDelete.Delete();
        List<Stylist> allStylists = Stylist.GetAll();
        return View["stylists.cshtml", allStylists];
      };

      Get["/stylist/edit/{id}"] = parameters => {
        Stylist foundStylist = Stylist.Find(parameters.id);
        return View["stylist_edit.cshtml", foundStylist];
      };
    }
  }
}
