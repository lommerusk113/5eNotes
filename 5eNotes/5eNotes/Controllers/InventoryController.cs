using Microsoft.AspNetCore.Mvc;
using _5eNotes.Services;
using _5eNotes.Models;

namespace _5eNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : Controller
    {
        [Route("CreateItem")]
        [HttpPost]
        public int CreateItem(ItemModel item)
        {
            ItemService itemService = new ItemService();
            itemService.InsertItem(item);
            return 1;
        }
        [Route("CreateInventory")]
        [HttpPost]
        public int CreateInventory(InventoryModel inventory)
        {
            ItemService itemService = new ItemService();
            itemService.InsertInventory(inventory);
            return 1;
        }
        [Route("UpdateItem")]
        [HttpPost]
        public int UpdateItem(ItemModel item)
        {
            ItemService itemService = new ItemService();
            itemService.UpdateItem(item);
            return 1;
        }
        [Route("DeleteItem")]
        [HttpPost]
        public int DeleteItem(ItemModel item)
        {
            ItemService itemService = new ItemService();
            itemService.DeleteItem(item);
            return 1;
        }
        [Route("DeleteInventory")]
        [HttpPost]
        public int DeleteInventory(InventoryModel CurrentInventory)
        {
            ItemService itemService = new ItemService();
            itemService.DeleteInventory(CurrentInventory);
            return 1;
        }
        [Route("GetItemList")]
        [HttpPost]
        public List<ItemModel> GetItemList(ItemModel item)
        {
            ItemService itemService = new ItemService();
            List<ItemModel> Items = itemService.GetItemList(item);
            return Items;
        }
        [Route("GetInventoryList")]
        [HttpPost]
        public List<InventoryModel> GetInventoryList(UserModel user)
        {
            ItemService itemService = new ItemService();
            List<InventoryModel> Inventories = itemService.GetInventoryList(user);
            return Inventories;
        }
        [Route("MoveItem")]
        [HttpPost]
        public int MoveItem(ItemModel item)
        {
            ItemService itemService = new ItemService();
            itemService.MoveInventory(item);
            return 1;
        }
    }
}
