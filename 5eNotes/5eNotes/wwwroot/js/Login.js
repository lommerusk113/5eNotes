window.onload = function () {
    var app = new Vue({
        el: '#app',
        data: {
            message: 'Hello Vue!'
        },
        methods: {
            Inventory: function () {
                console.log(this.message)
            },
        },
        mounted() {
            console.log("Mounted")
        }
    })
};