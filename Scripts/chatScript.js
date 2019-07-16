console.log("Holaaaaaa desde js");

var condicion = true;
/*while (condicion == true) {
    console.log("Entro al while");
    setInterval(function () {
        console.log("entro al set interval")
        function httpGet() {
            console.log("entro al get http");
            var xmlHttp = new XMLHttpRequest();
            xmlHttp.open("GET", "https://localhost:44338/User/SendMessage", false); // false for synchronous request
            xmlHttp.send(null);
            return xmlHttp.responseText;
        }
        httpGet();
        console.log("prueba de repeticion 2 segundos");
    }, 2000);
}*/

var counter = 0;
function loopGet() {
    console.log("hola");
    theNum = Math.floor(Math.random() * 6) + 1;
    counter = counter + 1;
    /*var xmlHttp = new XMLHttpRequest();
    xmlHttp.open("GET", "https://localhost:44338/User/SendMessage", false); // false for synchronous request
    xmlHttp.send(null);
    xmlHttp.responseText;
    */
    $.ajax({
        url: '/User/SendMessage',
        contentType: 'application/json',
        async: true,
        success: function (result) {
            //console.log("result:   ", result); 
            //$("#PartialContainer").load("/User/SendMessage");
           // window.location.reload();
            console.log("Se ejecuto:");
            //$(#listTelNumbers).load(location.href + "#listTelNumbers");
           $(document.body).load(location.href);
        }
    });

    if (counter < 100000) {
        setTimeout(loopGet, 5000);
    }
}

loopGet();
