using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace HairSalon
{
  public class Stylist
  {
    private int _id;
    private string _name;

    public Stylist(string name, int id=0)
    {
      _name = name;
      _id = id;
    }

    public override bool Equals(System.Object otherStylist)
    {
      if(!(otherStylist is Stylist)) return false;
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        bool nameEquality = this.GetName() == newStylist.GetName();
        bool idEquality = this.GetId() == newStylist.GetId();
        return(nameEquality && idEquality);
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

    public void Update(string newName)
    {
      _name = newName;
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE stylists SET stylist_name = @stylistName where id = @id;", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@stylistName";
      nameParameter.Value = newName;

      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@id";
      idParameter.Value = this.GetId();

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(idParameter);
      rdr = cmd.ExecuteReader();

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO stylists (stylist_name) OUTPUT INSERTED.id VALUES (@stylistName);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@stylistName";
      nameParameter.Value = this.GetName();

      cmd.Parameters.Add(nameParameter);
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

    public List<Client> GetClients()
  {
    List<Client> allClientsMatchingStylist = new List<Client>{};
    SqlConnection conn = DB.Connection();
    SqlDataReader rdr = null;
    conn.Open();

    SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE stylist_id = @stylistId;", conn);

    SqlParameter stylistIdParameter = new SqlParameter();
    stylistIdParameter.ParameterName = "@stylistId";
    stylistIdParameter.Value = this.GetId().ToString();

    cmd.Parameters.Add(stylistIdParameter);
    rdr = cmd.ExecuteReader();
    while(rdr.Read())
    {
      int clientId = rdr.GetInt32(0);
      string clientName = rdr.GetString(1);
      int clientStylistId = rdr.GetInt32(2);
      Client newClient = new Client(clientName, clientStylistId, clientId);
      allClientsMatchingStylist.Add(newClient);
    }
    if (rdr != null)
    {
      rdr.Close();
    }
    if (conn != null)
    {
      conn.Close();
    }
    return allClientsMatchingStylist;
  }

    public static List<Stylist> GetAll()
    {
      List<Stylist> allStylists = new List<Stylist>{};
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM stylists;", conn);
      rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);

        Stylist newStylist = new Stylist(stylistName, stylistId);
        allStylists.Add(newStylist);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allStylists;
    }

    public static Stylist Find(int searchId)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM stylists WHERE id = @stylistId;", conn);

      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@stylistId";
      stylistIdParameter.Value = searchId.ToString();

      int foundStylistId = 0;
      string foundStylistName = null;
      rdr = cmd.ExecuteReader();

      cmd.Parameters.Add(stylistIdParameter);

      while(rdr.Read())
      {
        foundStylistId = rdr.GetInt32(0);
        foundStylistName = rdr.GetString(1);
      }
      Stylist foundStylist = new Stylist(foundStylistName, foundStylistId);
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundStylist;
    }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM stylists WHERE id = @stylistId;", conn);

      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@stylistId";
      stylistIdParameter.Value = this.GetId();

      cmd.Parameters.Add(stylistIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM stylists;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
