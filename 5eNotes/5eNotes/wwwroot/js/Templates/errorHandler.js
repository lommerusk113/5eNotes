var errorHandler = {
    createErrorModal: function (body = "An error has occured.", title ="") {
        let modalString = `<div class="modal" id="errorModal" tabindex="-1" role="dialog">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    ${title != "" ?
            `
                    <div class="modal-header">
                        <h5 class="modal-title">${title}</h5>
                        <button type="button" class="close btn" data-dismiss="errorModal" aria-label="Close" onclick="errorHandler.removeErrorModal()">&times;</button>
                    </div>
                    `
            :
            ``
        }
                    <div class="modal-body">
                        <p>${body}</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn-primary btn" data-dismiss="errorModal" onclick="errorHandler.removeErrorModal()">Ok</button>
                    </div>
                </div>
            </div>
        </div>`

    $("body").append(modalString)
    $('#errorModal').modal("show")
    },

    removeErrorModal: function () {
        $('#errorModal').modal("hide")
        $("#errorModal").remove();
    },


}
