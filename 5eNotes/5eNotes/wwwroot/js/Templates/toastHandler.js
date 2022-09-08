var counter = 0;
var toastHandler = {
    createToast: function ( body = "Task Complete", title = "") {
        counter += 1
        let toastString =
            `
            <div class="toast ms-auto row bg-success text-white me-3" id="toast${counter}" role="alert" aria-live="assertive" aria-atomic="true" data-autohide="false">
                ${ title != "" ?
                `
                <div class="toast-header">
                    <img src="..." class="rounded mr-2" alt="...">
                    <strong class="mr-auto">${title}</strong>
                    <small class="text-muted">11 mins ago</small>
                    <button type="button" class="ml-2 mb-1 close" data-dismiss="toast" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                `
                :
                ""
                }
                <div class="toast-body">
                    ${body}
                </div>
            </div>
            `


        if ($('#toastContainer').length == 0) {
            $("body").append("<div id='toastContainer' class='fixed-top '></div>")
        }
        
        $("#toastContainer").append(toastString)
        $('.toast').toast("show")
        console.log("showing toast")

        setTimeout(() => {
            $('#toast' + counter).hide()
        }, 5000)
    },
    removeToast: function () {

    }
}