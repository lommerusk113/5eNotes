window.onload = function () {
    var app = new Vue({
        el: '#app',
        data: {
            message: 'Hello Vue!'
        },
        methods: {
            Inventory: function () {
                window.location.replace("https://localhost:7170/Inventory");
            },
        },
        mounted() {
            console.log("Mounted")
        }
    })
};