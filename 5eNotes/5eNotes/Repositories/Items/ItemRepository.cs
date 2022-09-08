using System.Data.SqlClient;
using Dapper;
using _5eNotes.Models;

namespace _5eNotes.Repositories.Items
{
    public class ItemRepository
    {
        public string ConnectionString = "Server=LOCALHOST; Database=DNDNOTES; Trusted_connection = True; MultipleActiveResultSets = true";
        public List<ItemModel> GetItemList(ItemModel items)
        {
            long InventoryId = items.InventoryId;

            string ItemName = items.ItemName;
            int Value = items.Value;
            int Rarity = items.Rarity;
            int OrderBy = items.OrderBy;
            string Direction = items.OrderByDirection == 1 ? "DESC" : "ASC";

            string SQL = $"SELECT * FROM Items ";

            List<string> searchParams = new List<string>();
            searchParams.Add("InventoryId = @InventoryId ");
            if (ItemName != "") searchParams.Add("ItemName LIKE '%' + @ItemName + '%' ");
            if (Value != 0) searchParams.Add("Value >= @Value ");
            if (Rarity != 0) searchParams.Add("Rarity = @Rarity ");

            SQL += " WHERE  ";
            SQL += string.Join(" AND ", searchParams);

            SQL += "ORDER BY";
            SQL += $" CASE WHEN @OrderBy = 1 THEN ItemName END {Direction},";
            SQL += $" CASE WHEN @OrderBy = 2 THEN [Value] END {Direction},";
            SQL += $" CASE WHEN @OrderBy = 3 THEN Rarity END {Direction},";
            SQL += $" CASE WHEN @OrderBy = 4 THEN Quantity END {Direction}";

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var result = connection.Query<ItemModel>(SQL, new { InventoryId, ItemName, Value, Rarity, OrderBy, Direction});
                List<ItemModel> resultList = new List<ItemModel>();

                foreach (var person in result)
                {
                    resultList.Add(person);
                }
                return resultList;
            }
        }
        public void Insert(ItemModel item)
        {
            long InventoryID = item.InventoryId;
            string ItemName = item.ItemName;
            int Value = item.Value;
            string Description = item.Description;
            int Rarity = item.Rarity;
            int Quantity = item.Quantity;
            int OrderBy = item.OrderBy;


            string SQL = $"Insert Into Items(InventoryId, ItemName, Value, Description, Rarity, Quantity) VALUES (@InventoryId, @ItemName, @Value, @Description, @Rarity, @Quantity); ";
            

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var result = connection.Query<UserModel>(SQL, new { InventoryID, ItemName, Value, Description, Rarity, Quantity, OrderBy });
            }
        }
        public void Delete(ItemModel item)
        {
            long ItemId = item.ItemId;

            string SQL = $"Delete FROM Items WHERE ItemId = @ItemId ";
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var result = connection.Query<UserModel>(SQL, new { ItemId });
            }
        }
        public void Update(ItemModel item)
        {
            long InventoryId = item.InventoryId;
            string ItemName = item.ItemName;
            int Value = item.Value;
            string Description = item.Description;
            int Rarity = item.Rarity;
            int Quantity = item.Quantity;
            long ItemId = item.ItemId;

            string SQL = $"UPDATE Items SET ItemName = @ItemName, Value = @Value, Description = @Description, Rarity = @Rarity, Quantity = @Quantity WHERE ItemId = @ItemId";
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var result = connection.Query<UserModel>(SQL, new { ItemId, InventoryId, Value, Description, Rarity, Quantity, ItemName });
            }
        }
        public void MoveInventory(ItemModel item)
        {
            long ItemId = item.ItemId;
            long InventoryId = item.ItemId;

            string SQL = $"UPDATE Items SET InventoryId = @InventoryId where ItemId = @ItemId";
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var result = connection.Query<UserModel>(SQL, new { ItemId, InventoryId });
            }
        }
    }
}
