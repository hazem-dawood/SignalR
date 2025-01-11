/**
 * Connect To Signalr r hub
 */
function initSignalR() {
    signalrService.startConnection().then(() => {

        //add green online icon with text connected
        $("#plist .connected-p i")
            .removeClass("offline")
            .addClass("online")
            .text("Connected");

        signalrService.onAddedToGroup(chatsService.addedToGroup);
        signalrService.onNewOnlineUser(chatsService.newOnlineUser);
        signalrService.onOfflineUser(chatsService.onOfflineUser);
        signalrService.onMessageAdded((model) => {

            //add to chat if online
            chatBoxService.messageAdded(model);
            chatsService.messageAdded(model);

            //browser notification8
            notificationService.notify(
                model.fromFullName,
                model.message,
                "icon-url.png",
                ""
            );
        });
    });
}

/**
 * format date time into MM/dd/yyyy HH:mm p
 * @param {string} date
 */
function formatDate(dateString) {
    const date = new Date(dateString);
    return date.toLocaleDateString("en-US", {
        month: "2-digit",
        day: "2-digit",
        year: "numeric",
        hour: "numeric",
        minute: "numeric"
    });
}


$(() => {
    // request permission
    notificationService.requestPermission();

    /**contains logic for log in */
    logInService.logIn();

    /**
     * search for a user
     */
    chatsService.enableSearchForUsers();
    chatBoxService.onClickSendingButton();
});
