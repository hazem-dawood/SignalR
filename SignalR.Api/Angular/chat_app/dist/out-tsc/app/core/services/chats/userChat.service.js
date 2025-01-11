"use strict";
exports.__esModule = true;
exports.UserChatService = void 0;
var tslib_1 = require("tslib");
var core_1 = require("@angular/core");
var UserChatService = /** @class */ (function () {
    function UserChatService(httpClient) {
        this.httpClient = httpClient;
        this.userChatUrl = 'userChat/';
    }
    /**
    *get user chats or groups that he is a member of it.
    *</summary>
    */
    UserChatService.prototype.getUserChatsWithGroups = function () {
        return this.httpClient.get(this.userChatUrl + 'getUserChatsWithGroups');
    };
    /**
    *pagination chat messages by id
    *</summary>
    *<param name="model"></param>
    */
    UserChatService.prototype.getUserChatMessages = function (model) {
        return this.httpClient.get(this.userChatUrl + 'getUserChatMessages');
    };
    /**
    *set message as seen
    *</summary>
    *<param name="userChatId"></param>
    */
    UserChatService.prototype.messagesSeen = function (userChatId) {
        return this.httpClient.post(this.userChatUrl + 'messagesSeen?userChatId=' + userChatId, {});
    };
    UserChatService = tslib_1.__decorate([
        core_1.Injectable({
            providedIn: "root"
        })
    ], UserChatService);
    return UserChatService;
}());
exports.UserChatService = UserChatService;
