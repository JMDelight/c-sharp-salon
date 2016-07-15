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

    [Fact]
    public void Test_Find_FindsClientInDatabase()
    {
      //Arrange
      Client testClient = new Client("Bob", 1);
      testClient.Save();

      //Act
      Client foundClient = Client.Find(testClient.GetId());

      //Assert
      Assert.Equal(testClient, foundClient);
    }

    [Fact]
    public void Test_Delete_DeletesASingleClientById()
    {
      //Arrange
      Client firstClient = new Client("Bob", 1);
      Client secondClient = new Client("Linda", 1);
      firstClient.Save();
      secondClient.Save();
      List<Client> expectedResult = new List<Client> {firstClient};

      //Act
      secondClient.Delete();
      List<Client> result = Client.GetAll();

      //Assert
      Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void Test_Update_UpdatesNameInDatabase()
    {
      //Arrange
      Client firstClient = new Client("Bob", 1);
      firstClient.Save();

      //Act
      firstClient.Update("Bobette", 3);
      Client resultClient = Client.Find(firstClient.GetId());

      //Assert
      Assert.Equal("Bobette", firstClient.GetName());
      Assert.Equal("Bobette", resultClient.GetName());
      Assert.Equal(3 , firstClient.GetStylistId());
      Assert.Equal(3 , resultClient.GetStylistId());
    }



    public void Dispose()
    {
      Client.DeleteAll();
    }
  }
}
