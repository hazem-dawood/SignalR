"use strict";
exports.__esModule = true;
exports.UserChatMessageService = void 0;
var tslib_1 = require("tslib");
var core_1 = require("@angular/core");
var UserChatMessageService = /** @class */ (function () {
    function UserChatMessageService(httpClient) {
        this.httpClient = httpClient;
        this.userChatMessageUrl = 'userChatMessage/';
    }
    /**
    * send new message
    * </summary>
    * <param name="model"></param>
    */
    UserChatMessageService.prototype.add = function (model) {
        return this.httpClient.post(this.userChatMessageUrl + 'add', model);
    };
    UserChatMessageService = tslib_1.__decorate([
        core_1.Injectable({
            providedIn: "root"
        })
    ], UserChatMessageService);
    return UserChatMessageService;
}());
exports.UserChatMessageService = UserChatMessageService;
