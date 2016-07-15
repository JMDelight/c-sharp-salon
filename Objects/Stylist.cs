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
    public int GetId()
    {
        return _id;
    }
    public string GetName()
    {
        return _name;
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
  }
}
