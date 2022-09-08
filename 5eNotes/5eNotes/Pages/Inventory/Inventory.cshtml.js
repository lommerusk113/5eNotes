
window.onload = function () {
    var app = new Vue({
        el: '#app',
        data: {
            user: {
                userId: 1,
            },
            message: 'Hello Vue!',
            currentInventory: {
                inventoryName: "",
            },
            items: {},
            inventories: {},
            addRow: false,
            newInventoryName: "",
            searchFilter: {
                hidden: false,
                orderBy: 0,
                orderByDirection: 0,
                itemName: "",
                value: 0,
                rarity: 0,
                inventoryId: 0,
            },
            newItem: {
                quantity: 1,
            },
            editItem: {
                itemName: "",
                value: 0,
                description: "",
                rarity: 0,
                quantity: 1,
            },
        },
        methods: {
            orderBy: function (num) {
                if (this.searchFilter.orderBy == num) {
                    this.searchFilter.orderByDirection = this.searchFilter.orderByDirection == 0 ? 1 : 0
                } else {
                    this.searchFilter.orderByDirection == 0
                }
                this.searchFilter.orderBy = num;
                this.GetItemList()
            },
            search: function () {
                if (!this.searchFilter.hidden) {
                    this.searchFilter.hidden = true

                } else {
                    this.searchFilter.hidden = false
                    this.searchFilter.itemName = ""
                    this.searchFilter.value = 0
                    this.searchFilter.rarity = 0
                }
            },
            changeInventory: function (data, element) {
                this.searchFilter.inventoryId = element.inventoryId;
                this.GetItemList()
                this.currentInventory.inventoryName = element.inventoryName
            },
            createNewItem: function () {
                if (!this.addRow) { this.addRow = true }
                else { this.addRow = false }

            },
            request: function (data) {
                let req = {
                    method: "POST",
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(data)
                }
                return req
            },
            GetInventoryList: function () {
                fetch(`https://localhost:7170/api/Inventory/GetInventoryList`, this.request(this.user))
                    .then(response => response.json())
                    .then(data => {
                        this.inventories = data
                        this.searchFilter.inventoryId = this.inventories[0].inventoryId
                        this.GetItemList();
                        this.currentInventory.inventoryName = this.inventories[0].inventoryName
                    });
            },
            GetItemList: function () {
                if (this.searchFilter.value == "") { this.searchFilter.value = 0 }
                fetch(`https://localhost:7170/api/Inventory/GetItemList`, this.request(this.searchFilter))
                    .then(response => response.json())
                    .then(data => {
                        this.items = data;
                    });
            },
            CreateInventory: function () {
                let postdata = this.user;
                postdata.inventoryName = this.newInventoryName;
                fetch("https://localhost:7170/api/Inventory/CreateInventory", this.request(postdata))
                    .then(response => {
                        this.GetInventoryList()
                        this.closeInventoryModal()
                        toastHandler.createToast("You have successfully created an Inventory.")
                    })
            },
            CreateItem: function () {
                if (!this.newItem.itemName) {
                    this.closeNewItemModal()
                    errorHandler.createErrorModal("An item needs to have a name.", "Naming error")
                    return
                }
                this.newItem.inventoryId = this.searchFilter.inventoryId
                fetch("https://localhost:7170/api/Inventory/CreateItem", this.request(this.newItem))
                    .then(response => {
                        this.GetItemList()
                        this.closeNewItemModal()
                        this.newItem = {
                            quantity: 1,
                        }
                        this.addRow = false
                        toastHandler.createToast("You have successfully added an item.")
                    })
            },
            UpdateItem: function () {
                fetch("https://localhost:7170/api/Inventory/UpdateItem", this.request(this.editItem))
                    .then(response => {
                        this.GetItemList()
                        this.closeEditModal()
                    })
            },
            DeleteItem: function (item) {
                fetch("https://localhost:7170/api/Inventory/DeleteItem", this.request(item))
                    .then(response => {
                        this.GetItemList()
                        toastHandler.createToast("Item has been successfully deleted.")
                    })
            },
            DeleteInventory: function () {
                fetch("https://localhost:7170/api/Inventory/DeleteInventory", this.request(this.searchFilter))
                    .then(response => {
                        this.GetInventoryList()
                        toastHandler.createToast("Inventory has been deleted successfully.")
                    })
            },
            changeQuantity: function (direction, index = -1) {
                if (index == -1) {
                    if (direction == 0) {
                        if (this.newItem.quantity > 0) {
                            this.newItem.quantity -= 1
                        }
                    }
                    else { this.newItem.quantity = parseInt(this.newItem.quantity) + 1 }
                } else {
                    if (direction == 0) {
                        if (this.editItem.quantity > 0) {
                            this.editItem.quantity -= 1
                        }
                    }
                    else { this.editItem.quantity += 1 }
                }
            },
            openInventoryModal: function () {
                this.newInventoryName = ""

                $('#newInventoryModal').modal("show")
            },
            closeInventoryModal: function () {
                $('#newInventoryModal').modal("hide")

            },
            openNewItemModal: function () {
                this.newItem =  {quantity: 1,
            },
                $('#newItemModal').modal("show")
            },
            closeNewItemModal: function () {
                $('#newItemModal').modal("hide")
            },
            openEditItemModal: function (item) {
                this.editItem.itemName = item.itemName;
                this.editItem.value = item.value;
                this.editItem.description = item.description;
                this.editItem.rarity = item.rarity;
                this.editItem.quantity = item.quantity;
                this.editItem.inventoryId = item.inventoryId;
                this.editItem.itemId = item.itemId;

                $('#editItemModal').modal("show")  
            },
            closeEditModal: function () {
                $('#editItemModal').modal("hide")
            },

            getSearchedItemsTimeout: function () {
                setTimeout(() => {
                    this.GetItemList();
                }, 500)
            },
        },
        filters: {
            inventoryNameFilter: function (name) {
                if (name === null) {
                    return "Inventory";
                }
                return name;
            },

            nameFilter: function (name) {
                if (name.length > 20) {
                    return name.slice(0,20) + "..."
                }
                return name
            },
            valueFilter: function (value) {
                if (value == "") {return "NaN"}
                if (value > 999999) { return "Alot" }

                let filteredString = ""

                let gold = Math.round(value / 100);
                let silver = Math.round(value % 100 / 10);
                let copper = value % 100 % 10;

                if (gold != 0) { filteredString += ("Gp: " + gold + " ") }
                if (silver != 0) { filteredString += ("Sp: " + silver + " ") }
                if (copper != 0) { filteredString += ("Cp: " + copper + " ") }

                return filteredString
            },
            rarity: function (value) {
             
                if (value == 0) {
                    return "Not Selected"
                } else if (value == 1) {
                    return "Common"
                } else if (value == 2) {
                    return "Uncommon"
                } else if (value == 3) {
                    return "Rare"
                } else if (value == 4) {
                    return "Very Rare"
                }
            },
            quantityFilter: function (qty) {
                if (qty > 99999) {
                    return "Many"
                }
                return qty
            },
        },
        watch: {
            "searchFilter.itemName": function () {
                this.getSearchedItemsTimeout()
            },
            "searchFilter.value": function () {
                this.getSearchedItemsTimeout()
            },
            "searchFilter.rarity": function () {
                this.getSearchedItemsTimeout()
            }
        },
        mounted() {
            this.GetInventoryList()
        }
    })
};