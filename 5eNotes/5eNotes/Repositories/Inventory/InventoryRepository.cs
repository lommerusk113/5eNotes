
using System.Data.SqlClient;
using Dapper;
using _5eNotes.Models;

namespace _5eNotes.Repositories.Inventory
{
    public class InventoryRepository
    {
   
        //public string ConnectionString = "Server=192.168.60.47; Database=STUDENT; Trusted_connection = True; MultipleActiveResultSets = true";
        public string ConnectionString = "Server=LOCALHOST; Database=DNDNOTES; Trusted_connection = True; MultipleActiveResultSets = true";
 

        public List<InventoryModel> GetInventoryList(UserModel user)
        {
            long UserId = user.UserId;

            string SQL = $"SELECT * FROM Inventory WHERE UserId = @UserId";
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var result = connection.Query<InventoryModel>(SQL, new { UserId });
                List<InventoryModel> resultList = new List<InventoryModel>();

                foreach (var person in result)
                {
                    resultList.Add(person);
                }
                return resultList;
            }
        }
        public void Insert(InventoryModel inventory)
        {
            long UserId = inventory.UserId;
            string InventoryName =  inventory.InventoryName;

            string SQL = $"Insert Into Inventory(UserId, InventoryName) VALUES (@UserId, @InventoryName); ";
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var result = connection.Query<UserModel>(SQL, new { UserId, InventoryName });
            }
        }
        public void Delete(InventoryModel inventory)
        {
            long InventoryId = inventory.InventoryId;

            string SQL = $"Delete FROM Inventory WHERE InventoryId = @InventoryId; ";
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var result = connection.Query<UserModel>(SQL, new { InventoryId });
            }
        }


    }
}
