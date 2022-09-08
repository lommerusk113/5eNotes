namespace _5eNotes.Models
{
    public class ItemModel
    {
        public long ItemId { get; set; }
        public long InventoryId { get; set; }
        public string? ItemName { get; set; }
        public int Value { get; set; }
        public string? Description { get; set; }
        public int Rarity { get; set; }
        public int Quantity { get; set; }
        public int IsEditing { get; set; }
        public int OrderBy { get; set; }
        public int OrderByDirection { get; set; }
    }
}
