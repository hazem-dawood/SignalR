"use strict";
exports.__esModule = true;
exports.GroupService = void 0;
var tslib_1 = require("tslib");
var core_1 = require("@angular/core");
var GroupService = /** @class */ (function () {
    function GroupService(httpClient) {
        this.httpClient = httpClient;
        this.groupUrl = 'group/';
    }
    /**
     * add new group
     * @param model
     * @returns
     */
    GroupService.prototype.Add = function (model) {
        return this.httpClient.post(this.groupUrl + 'add', model);
    };
    /**
    * get the groups that the current user is a member of it.
    * </summary>
    */
    GroupService.prototype.getCurrentUserGroups = function () {
        return this.httpClient.get(this.groupUrl + 'getCurrentUserGroups');
    };
    /**
    * pagination group messages by group id
    */
    GroupService.prototype.getGroupMessages = function (model) {
        var url = this.groupUrl + 'getGroupMessages?';
        url += 'groupId=' + model.groupId + '&length=' + model.length + '&pageNumber=' + model.pageNumber;
        return this.httpClient.get(url);
    };
    /**
    * send a message to a group
   */
    GroupService.prototype.sendMessage = function (model) {
        return this.httpClient.post(this.groupUrl + 'sendMessage', model);
    };
    GroupService = tslib_1.__decorate([
        core_1.Injectable({
            providedIn: "root"
        })
    ], GroupService);
    return GroupService;
}());
exports.GroupService = GroupService;
