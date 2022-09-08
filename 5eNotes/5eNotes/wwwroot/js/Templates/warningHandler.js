var warningHandler = {
    createWarningModal: function ({ title = "", body = "Are you sure you want continue?" }) {
        let modalString = `<div class="modal" id="errorModal" tabindex="-1" role="dialog">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    ${title != "" ?
                `
                    <div class="modal-header">
                        <h5 class="modal-title">${title}</h5>
                        <button type="button" class="close btn" data-dismiss="modal" aria-label="Close" onclick="removeErrorModal(0)">&times;</button>
                    </div>
                    `
                :
                ``
            }
                    <div class="modal-body">
                        <p>${body}</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn-success btn" data-dismiss="inventoryModal" onclick="removeErrorModal(1)">Yes</button>
                        <button type="button" class="btn-danger btn" data-dismiss="inventoryModal" onclick="removeErrorModal(0)">No</button>
                    </div>
                </div>
            </div>
        </div>`

        $("body").append(modalString)
        $('#errorModal').modal("show")
    },

    removeWarningModal: function (num) {
        $('#errorModal').modal("show")
        $("#errorModal").remove();
        return num;
    },


}