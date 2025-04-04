if (typeof window.isSending === 'undefined') {
    window.isSending = false;
}

async function startConnection() {
    try {
        const chatId = parseInt(document.getElementById("chatId").value);
        if (connection.state === signalR.HubConnectionState.Disconnected) {
            await connection.start();
            console.log("SignalR připojen.");

            await connection.invoke("JoinChat", parseInt(chatId));
            console.log("Připojen do skupiny chatId:", chatId);
        } else {
            console.log("SignalR už je připojený.");
        }

        await loadOldMessages(chatId);

    } catch (err) {
        console.error("Chyba při připojení k SignalR:", err);
        setTimeout(startConnection, 5000);
    }
}

async function loadOldMessages(chatId) {
    try {
        const response = await fetch(`/GetMessages?chatId=${chatId}`);
        const messages = await response.json();

        const messageBox = document.getElementById("messages");
        messageBox.innerHTML = "";
        const chatColor = document.getElementById("foreign-chat-color").value;

        messages.forEach(msg => {
            appendMessageToChat(msg.senderId, msg.senderNick, msg.content, chatColor, msg.sentAt);
        });

        scrollToBottom();

    } catch (err) {
        console.error("Chyba při načítání zpráv:", err);
    }
}

async function changeChat(chatId) {
    if (connection.state !== signalR.HubConnectionState.Disconnected) {
        await connection.stop();
    }
}
function sendMessage() {
    if (window.isSending) return;
    window.isSending = true;

    const chatId = document.getElementById("chatId").value;
    const message = document.getElementById("messageInput").value;

    const sendButton = document.getElementById("send-button");
    sendButton.disabled = true;

    if (connection.state === signalR.HubConnectionState.Connected) {
        connection.invoke("SendMessage", parseInt(chatId), message)
            .then(() => console.log("Zpráva úspěšně odeslána"))
            .catch(err => console.error("Chyba při odesílání zprávy:", err));
    } else {
        console.warn("Nemůžeš poslat zprávu, protože nejsi připojen.");
    }

    sendButton.disabled = false;
    window.isSending = false;
}

function appendMessageToChat(userId, userNick, message, chatColor, time = "Neznámý čas") {
    const messageBox = document.getElementById("messages");
    const msgDiv = document.createElement("div");
    msgDiv.classList.add("row", "message");

    msgDiv.innerHTML = `
        <div class="col-auto message-block" data-bs-toggle="tooltip" title="${time}">
            <span class="user-nick">${userNick}</span>
            <span class="content">${message}</span>
            ${time ? `<span class="time">${time}</span>` : ""}
        </div>
    `;

    const currentUser = window.currentUser
    if (userId == currentUser) {
        msgDiv.classList.add("my-message");
    } else {
        msgDiv.classList.add("foreign-message");
        const contentElement = msgDiv.querySelector('.content')
        if (contentElement) {
            contentElement.style.backgroundColor = chatColor;
        }
    }


    messageBox.appendChild(msgDiv);
    scrollToBottom();

    var tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]');
    var tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl));
}

function scrollToBottom() {
    const messageBox = document.getElementById("messages");
    messageBox.scrollTop = messageBox.scrollHeight;
}

function formatDatetimeNow() {
    const now = new Date();
    const hours = now.getHours().toString().padStart(2, '0');
    const minutes = now.getMinutes().toString().padStart(2, '0');
    const day = now.getDate().toString().padStart(2, '0');
    const month = (now.getMonth() + 1).toString().padStart(2, '0');

    return `${hours}:${minutes} ${day}.${month}`;
}

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").withAutomaticReconnect().configureLogging(signalR.LogLevel.Debug).build();

startConnection();

connection.on("ReceiveMessage", function (user, message) {
    console.log("Příchozí zpráva: ", user, message);

    timeNow = formatDatetimeNow();
    const chatColor = document.getElementById("foreign-chat-color").value;
    appendMessageToChat(window.currentUser, user, message, chatColor, timeNow);
});

connection.onclose(error => {
    console.error("SignalR spojení bylo uzavřeno:", error);
});
