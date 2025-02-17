var connection = new signalR.HubConnectionBuilder().withUrl("/chathub").withAutomaticReconnect().build();

connection.start().then(function () {
    connection.invoke("JoinChat", "@Model.Id");
}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("ReceiveMessage", function (user, message) {
    var msg = `<p><strong>${user}:</strong> ${message}</p>`;
    document.getElementById("chat-messages").innerHTML += msg;
});

function sendMessage() {
    var content = document.getElementById("messageInput").value;
    if (connection.state === signalR.HubConnectionState.Disconnected) {
        connection.start().then(function () {
            connection.invoke("JoinChat", "@Model.Id").then(function () {
                if (content) {
                    connection.invoke("SendMessage", "@Model.Id", "@User.Identity.Name", content).catch(function (err) {
                        return console.error(err.toString());
                    });
                    document.getElementById("messageInput").value = "";
                }
            });
        }).catch(function (err) {
            return console.error(err.toString());
        });
    } else if (connection.state === signalR.HubConnectionState.Connected) {
        if (content) {
            connection.invoke("SendMessage", "@Model.Id", "@User.Identity.Name", content).catch(function (err) {
                return console.error(err.toString());
            });
            document.getElementById("messageInput").value = "";
        }
    }
}