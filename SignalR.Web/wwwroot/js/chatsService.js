
const chatsService = (function () {
    var apiUrls = {
        getChats: "/Home/GetChats"
    };

    var /**ul of chats*/
        ulChats = $("#ulChats"),
        /** div that contains message text box and button to send message*/
        chatMessageButtonText = $(".chat-message"),
        /** */
        divChatUserInfo = $("#divChatUserInfo"),
        userInfoImage = $("#userInfoImage"),
        userInfoName = $("#userInfoName"),
        userInfoLastSeen = $("#userInfoLastSeen");

    /**search the box for a user by name*/
    const enableSearchForUsers = function () {
        $("#txtSearch").keyup(function () {
            var s = $(this).val();
            $(".user-name").each(function () {
                if ($(this).text().toLowerCase().includes(s.toLowerCase())) {
                    $(this).closest("li").show();
                } else {
                    $(this).closest("li").hide();
                }
            });
        });
    }

    const getActiveChat = function () {
        const activeLi = ulChats.find("li.active");
        if (activeLi.length === 0)
            return null;

        return {
            userChatId: activeLi.attr("data-id"),
            userId: activeLi.attr("data-user-id")
        };
    }

    /**
     * if user was not authenticated and he logged-in call it
     * also call it to update chats
     */
    const loadUserChats = function () {
        chatBoxService.waitDivChats(true);
        var activeLi = getActiveChat();
        $.get(apiUrls.getChats,
            (res) => {
                chatBoxService.waitDivChats(false);
                ulChats.html(res);
                if (activeLi != null) {
                    $(`li[data-user-id=${activeLi.userId}]`).addClass("active");
                }
            });
    }

    /**
     * happens when click in li (any chat)
     * @@param {HtmlLiElement} el
     * @@param {number} id
     * @@param {boolean} isGroup
     */
    const onClickGetChat = function (el, id, isGroup) {
        chatBoxService.waitDivChats(true);
        //if id 0 then new chat
        el = $(el);
        //remove active from chats
        el.parent().find(">li").each(function () {
            $(this).removeClass("active");
        });

        //active clicked chat
        el.addClass("active");
        //set name
        userInfoName.text(el.find(".user-name").text());
        //set image
        userInfoImage.attr("src", el.find("img").attr("src"));

        userInfoLastSeen.html(el.find(".status").html());
        //show full div
        divChatUserInfo.removeClass("d-none");
        chatMessageButtonText.removeClass("d-none");
        $(".chat-history ul li").remove();
        if (id === 0) {
            //new chat
            chatBoxService.waitDivChats(false);
        } else {
            if (isGroup) {
                //
            } else {
                chatBoxService.loadChat({ userChatId: id, pageNumber: 1, length: 20 });
            }
        }
    }

    /**
    * notify user of online user
    * @param {number} userId
    */
    const newOnlineUser = function (userId) {
        const userLi = $(`li[data-user-id='${userId}']`);
        if (userLi != null) {
            const divStatus = userLi.find(".status");
            divStatus.html('<i class="fa fa-circle online "></i>');
        }
    }
    /**
    * notify user of online user
    * @param {number} userId
    */
    const offlineUser = function (userId) {
        const userLi = $(`li[data-user-id='${userId}']`);
        if (userLi != null) {
            const divStatus = userLi.find(".status");
            divStatus.html('<i class="fa fa-circle offline "></i>');
        }
    }

    /**
     *
     * @param {GetGroupDto} model
     */
    const addedToGroup = function (model) {
        // 
    }

    const messageAdded = function (model) {
        chatsService.loadUserChats();
        //append  li
    }

    return {
        enableSearchForUsers: enableSearchForUsers,
        loadUserChats: loadUserChats,
        onClickGetChat: onClickGetChat,
        getActiveChat: getActiveChat,
        addedToGroup: addedToGroup,
        newOnlineUser: newOnlineUser,
        messageAdded: messageAdded,
        onOfflineUser: offlineUser
    }
})();

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

/**
* @typedef {Object} IdNameDto
* @property {number} id - The unique identifier.
* @property {string} name - The name of the entity.
*/

/**
 * @typedef {Object} LastMessageDto
 * @property {string} message - The content of the last message.
 * @property {Date} createdDate - The creation date of the message.
 */