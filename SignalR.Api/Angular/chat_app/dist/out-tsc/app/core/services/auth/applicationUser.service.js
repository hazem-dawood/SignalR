"use strict";
exports.__esModule = true;
exports.ApplicationUserService = void 0;
var tslib_1 = require("tslib");
var core_1 = require("@angular/core");
var ApplicationUserService = /** @class */ (function () {
    function ApplicationUserService(httpClient) {
        this.httpClient = httpClient;
        this.usersUrl = 'users/';
    }
    /**
    /// get all users inside db
    /// </summary>
    */
    ApplicationUserService.prototype.getUsers = function () {
        return this.httpClient.get(this.usersUrl + 'getUsers');
    };
    /**
    /// get groups of a user
    /// </summary>
    /// <param name="userId"></param>
    */
    ApplicationUserService.prototype.getUserGroups = function (userId) {
        return this.httpClient.get(this.usersUrl + 'getUserGroups?userId=' + userId);
    };
    /**
    /// log in
    /// </summary>
    /// <param name="model"></param>
    */
    ApplicationUserService.prototype.signIn = function (model) {
        return this.httpClient.post(this.usersUrl + 'signIn', model);
    };
    ApplicationUserService = tslib_1.__decorate([
        core_1.Injectable({
            providedIn: "root"
        })
    ], ApplicationUserService);
    return ApplicationUserService;
}());
exports.ApplicationUserService = ApplicationUserService;
