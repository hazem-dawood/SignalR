"use strict";
exports.__esModule = true;
exports.AuthenticateService = void 0;
var tslib_1 = require("tslib");
var core_1 = require("@angular/core");
var AuthenticateService = /** @class */ (function () {
    function AuthenticateService() {
    }
    AuthenticateService.prototype.getLastAntToken = function () {
        return localStorage.getItem('antiToken');
    };
    AuthenticateService.prototype.setLastAntToken = function (to) {
        return localStorage.setItem('antiToken', to !== null && to !== void 0 ? to : '');
    };
    AuthenticateService.prototype.getToken = function () {
        return localStorage.getItem('token');
    };
    AuthenticateService.prototype.isUserAuthenticated = function () {
        return localStorage.getItem('token') != null;
    };
    AuthenticateService.prototype.authenticateUser = function (token) {
        localStorage.setItem('token', token);
    };
    AuthenticateService.prototype.removeAuthentication = function () {
        localStorage.removeItem('token');
    };
    AuthenticateService = tslib_1.__decorate([
        core_1.Injectable({
            providedIn: 'root'
        })
    ], AuthenticateService);
    return AuthenticateService;
}());
exports.AuthenticateService = AuthenticateService;
