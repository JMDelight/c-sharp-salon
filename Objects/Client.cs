using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace HairSalon
{
  public class Client
  {
    private int _id;
    private string _name;
    private int _stylistId;

    public Client(string name, int stylistId, int id=0)
    {
      _name = name;
      _id = id;
      _stylistId = stylistId;
    }

    public override bool Equals(System.Object otherClient)
    {
      if(!(otherClient is Client)) return false;
      else
      {
        Client newClient = (Client) otherClient;
        bool nameEquality = this.GetName() == newClient.GetName();
        bool idEquality = this.GetId() == newClient.GetId();
        bool stylistEquality = this.GetStylistId() == newClient.GetStylistId();
        return(nameEquality && idEquality && stylistEquality);
      }
    }

    public int GetId()
    {
        return _id;
    }
    public string GetName()
    {
        return _name;
    }
    public int GetStylistId()
    {
      return _stylistId;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO clients (client_name, stylist_id) OUTPUT INSERTED.id VALUES (@clientName, @stylistId);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@clientName";
      nameParameter.Value = this.GetName();

      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@stylistId";
      stylistIdParameter.Value = this.GetStylistId();

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(stylistIdParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client>{};
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        int stylistId = rdr.GetInt32(2);

        Client newClient = new Client(clientName, stylistId, clientId);
        allClients.Add(newClient);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allClients;
    }

    public static Client Find(int searchId)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE id = @clientId;", conn);

      SqlParameter clientIdParameter = new SqlParameter();
      clientIdParameter.ParameterName = "@clientId";
      clientIdParameter.Value = searchId.ToString();

      cmd.Parameters.Add(clientIdParameter);

      int foundClientId = 0;
      string foundClientName = null;
      int foundStylistId = 0;
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        foundClientId = rdr.GetInt32(0);
        foundClientName = rdr.GetString(1);
        foundStylistId = rdr.GetInt32(2);
      }
      Client foundClient = new Client(foundClientName, foundStylistId, foundClientId);
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundClient;
    }


    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM clients;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
