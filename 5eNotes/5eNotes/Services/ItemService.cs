using _5eNotes.Models;
using _5eNotes.Repositories.Inventory;
using _5eNotes.Repositories.Items;

namespace _5eNotes.Services
{
    public class ItemService
    {
        public List<InventoryModel> GetInventoryList(UserModel user)
        {
            InventoryRepository InventoryRepo = new InventoryRepository();
            List<InventoryModel> InventoryList = InventoryRepo.GetInventoryList(user);

            return InventoryList;
        }
        public List<ItemModel> GetItemList(ItemModel item)
        {
            ItemRepository ItemRepo = new ItemRepository();
            List<ItemModel> ItemList = ItemRepo.GetItemList(item);

            return ItemList;

        }
        public void DeleteItem(ItemModel item)
        {
            ItemRepository ItemRepo = new ItemRepository();
            ItemRepo.Delete(item);
        }
        public void DeleteInventory(InventoryModel inventory)
        {
            InventoryRepository InventoryRepo = new InventoryRepository();
            InventoryRepo.Delete(inventory);
        }
        public void UpdateItem(ItemModel item)
        {
            ItemRepository ItemRepo = new ItemRepository();
            if (item.Quantity < 1)
            {
                ItemRepo.Delete(item);
            }
            else
            {
                ItemModel SearchItem = new ItemModel();
                SearchItem.InventoryId = item.InventoryId;
                SearchItem.ItemName = item.ItemName;

                ItemRepo.Update(item);

                List<ItemModel> itemList = ItemRepo.GetItemList(SearchItem);
                if (itemList.Count > 1)
                {
                    itemList[0].Quantity += itemList[1].Quantity;
                    ItemRepo.Update(itemList[0]);
                    ItemRepo.Delete(itemList[1]);
                }
            }
        }
        public void MoveInventory(ItemModel item)
        {
          ItemRepository itemRepo = new ItemRepository();
            itemRepo.MoveInventory(item);

        }
        public void InsertInventory(InventoryModel inventory)
        {
            InventoryRepository InventoryRepo = new InventoryRepository();
            InventoryRepo.Insert(inventory);
        }
        public void InsertItem(ItemModel item)
        {
            item.ItemName = item.ItemName.Substring(0, 1).ToUpper() + item.ItemName.Substring(1);
            ItemRepository ItemRepo = new ItemRepository();
            if (item.Quantity > 0)
            {
                //MERGE SAME ITEMS
                ItemModel SearchItem = new ItemModel();
                SearchItem.InventoryId = item.InventoryId;
                SearchItem.ItemName = item.ItemName;

                List<ItemModel> inventoryList = ItemRepo.GetItemList(SearchItem);
                bool exists = false;
                foreach (ItemModel element in inventoryList)
                {
                    if (element.ItemName == item.ItemName)
                    {
                        element.Quantity += item.Quantity;
                        UpdateItem(element);
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    ItemRepo.Insert(item);
                }
            }
        }
    }
}
