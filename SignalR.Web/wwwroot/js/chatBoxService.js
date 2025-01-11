
const chatBoxService = (function () {
    var apiUrls = {
        SendChatMessage: "/Home/SendMessage",
        getChatMessages: "/Home/GetChatMessages"
    };
    var
        /**contains all chats with new users chats*/
        divChats = $("#divChats"),
        /**click it to send a message*/
        btnSendMessage = $("#btnSendMessage"),
        /**write message to sent it*/
        txtNewMessage = btnSendMessage.next(),
        /**chat li from template (design) */
        liChatFrom = $("#liChatFrom"),
        /**chat li Me template (design) */
        liChatMe = $("#liChatMe"),
        /** chat history contains all messages*/
        chatHistory = $(".chat-history");

    /**
    * @param {GetUserChatMessageDto} obj
    */
    const appendNewMessage = function (obj) {
        var li;
        if (!obj.isFromMe) {
            li = $(liChatFrom.html());
            li.find("img").attr("src", $("#userInfoImage").attr("src"));
        } else {
            li = $(liChatMe.html());
        }

        li.find(".message").text(obj.message);
        li.find(".message-data-time").text(formatDate(obj.createdDate));
        li.appendTo(chatHistory.find("ul"));
        setTimeout(function () {
            chatHistory.animate({ scrollTop: chatHistory[0].scrollHeight }, 200); // 500ms duration
        }, 100);
    }

    const init = function () {
        /**on clicking enter button send message also*/
        txtNewMessage.on("keydown", function (e) {
            if (e.which === 13)/*Enter Button*/ {
                btnSendMessage.click();
            }
        });
        btnSendMessage.click(function () {
            const message = txtNewMessage.val();
            if (!!!message) {
                //if empty return
                return;
            }
            /**
             * @param {AddMessageDto} obj
            */
            const activeUserChatLi = chatsService.getActiveChat();
            const obj = {
                userChatId: activeUserChatLi.userChatId,
                'toUserId': activeUserChatLi.userId,
                'message': message
            };
            appendNewMessage({
                isFromMe: true,
                createdDate: new Date(),
                message: message,
                from: ""
            });
            $.post(apiUrls.SendChatMessage, obj,
                (res) => {
                    if (res.isSuccess) {
                        txtNewMessage.val(null);
                        chatsService.loadUserChats();
                        if (obj.userChatId === 0) {
                            // it was new chat,but he sent a message so chat added
                            activeUserChatLi.attr("data-id", res.data.userChatId);
                        }
                    } else {
                        //handle error
                    }
                });

        });
    }

    /**
     *
     * receives a message
     * @param {NotifyUserMessageDto} model
     */
    const messageAdded = function (model) {
        const activeChat = chatsService.getActiveChat();
        if (activeChat != null && parseInt(activeChat.userChatId) === model.userChatId) {
            //append only of the chat is opened
            appendNewMessage({
                isFromMe: false,
                createdDate: model.createdDate,
                message: model.message,
                from: model.fromFullName,
                to: ""
            });
        }
    }

    /**
   * show or hide loader
   * @param {boolean} show
   */
    const waitDivChats = function (show) {
        divChats.waitMe(show ? "show" : "hide");
    }

    /**
    * @param {RequestUserChatMessageDto} obj
    */
    const loadChat = function (obj) {
        $.get(`${apiUrls.getChatMessages}?Length=${obj.length}&PageNumber=${obj.pageNumber}&UserChatId=${obj.userChatId}`,
            /**
             * @param {GetUserChatMessageDto[]} res
             */
            (res) => {
                waitDivChats(false);
                if (res != null && res.length > 0) {
                    $.each(res,
                        (i, v) => {
                            appendNewMessage(v);
                        });
                }
            }).fail((r) => {
                waitDivChats(false);
            });
    }


    return {
        onClickSendingButton: init,
        messageAdded: messageAdded,
        appendNewMessage: appendNewMessage,
        waitDivChats: waitDivChats,
        loadChat: loadChat
    }
})();

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
* @typedef {Object} RequestUserChatMessageDto
* @property {number} userChatId - The unique identifier for the user's chat.
* @property {number} pageNumber - Page Number
* @property {number} length - Length
*/

/**
* @typedef {Object} GetUserChatMessageDto
* @property {string} from -
* @property {boolean} isFromMe - is from me?
* @property {string} to -
* @property {string} message -
* @property {string} createdDate -
* @property {boolean} isSeen -
*/
/**
* @typedef {Object} AddMessageDto
* @property {number} userChatId - 0 if first time
* @property {number} toUserId -
* @property {string} message -
*/