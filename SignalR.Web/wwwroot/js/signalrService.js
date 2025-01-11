
var signalrService = (function() {
    //#region vars
    var connection;
    //#endregion

    const onConnectionClosing = function() {
        // Handle disconnection
        connection.onclose((error) => {
            $("#plist .connected-p i")
                .addClass("offline")
                .removeClass("online")
                .text("DisConnected");
            console.warn("Connection lost. Attempting to reconnect...");
            //retryConnection();
        });
    }
    const connect = function() {
        // Create a connection to the SignalR hub
        connection = new window.signalR.HubConnectionBuilder()
            //you can find signalrConfiguration in _Layout
            .withUrl(window.signalrConfiguration.route)
            .withAutomaticReconnect()
            //.withHubProtocol(new signalR.JsonHubProtocol())
            .build();
        onConnectionClosing();
        // Start the connection
        return connection.start()
            .then(() => {
                setInterval(() => {
                        //update user status every 50 seconds
                        connection.invoke("UpdateUserStatus");
                    },
                    50000);
            })
            .catch(function(err) { console.error(err.toString()); });
    }

    /**
      * @param {function(GetGroupDto): void} func - The callback function that processes the group data.
      */
    const onAddedToGroup = function(func) {
        //receiving a message from a group
        connection.on(window.signalrConfiguration.addedToGroup, func);
    }

    /**
      * @param {function(NotifyUserMessageDto): void} func - The callback function that processes the group data.
      */
    const onMessageAdded = function(func) {
        //receiving a message from a group
        connection.on(window.signalrConfiguration.messageAdded, func);
    }

    /**
     *
     * @param {function(number):void} func
     */
    const onNewOnlineUser = function(func) {
        //who's become online ?
        connection.on(window.signalrConfiguration.newOnlineUser, func);
    }
    /**
     *
     * @param {function(number):void} func
     */
    const onOfflineUser = function(func) {
        //who's become online ?
        connection.on(window.signalrConfiguration.offlineUser, func);
    }

    return {
        startConnection: connect,
        onAddedToGroup: onAddedToGroup,
        onMessageAdded: onMessageAdded,
        onNewOnlineUser: onNewOnlineUser,
        onOfflineUser: onOfflineUser
    }
})();


//#region declarations

/**
* @typedef {Object} NotifyUserMessageDto
* @property {number} userChatId - The unique identifier for the user's chat.
* @property {number} messageId - The unique identifier for the message.
* @property {string} message - The content of the message.
* @property {string} fromFullName - The full name of the user sending the message.
* @property {number} toUserId - The unique identifier for the recipient user.
* @property {string} createdDate - Created Date
*/
/**
 * @typedef {Object} GetGroupDto
 * @property {number} id - The unique identifier.
 * @property {string} name - The name of the entity.
 * @property {IdNameDto[]} members - The list of members in the group.
 * @property {LastMessageDto} lastMessage - The details of the last message in the group.
 * @property {boolean} isGroup - Indicates if this is a group.
 * @property {boolean} isGroup - Indicates if user online or not.
 * @property {string} LastSeen - Last Seen
 */

//#endregion