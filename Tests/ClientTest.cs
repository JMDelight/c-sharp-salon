using Xunit;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace HairSalon
{
  public class ClientTest : IDisposable
  {
    public ClientTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Client.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnTrueIfNamesAreTheSame()
    {
      //Arrange
      Client firstClient = new Client("Bob", 1);
      Client otherFirstClient = new Client("Bob", 1);

      //Act Assert
      Assert.Equal(firstClient, otherFirstClient);
    }

    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      //Arrange
      Client newClient = new Client("Linda", 1);
      List<Client> expectedResult = new List<Client> {newClient};

      //Act
      newClient.Save();
      List<Client> savedClients = Client.GetAll();

      //Assert
      Assert.Equal(expectedResult, savedClients);
    }


    public void Dispose()
    {
      // Client.DeleteAll();
    }
  }
}
