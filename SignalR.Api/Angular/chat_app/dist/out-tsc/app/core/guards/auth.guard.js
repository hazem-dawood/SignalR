"use strict";
exports.__esModule = true;
exports.authGuard = void 0;
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var authenticate_service_1 = require("../services/auth/authenticate.service");
var authGuard = function (route, state) {
    var authenticateService = core_1.inject(authenticate_service_1.AuthenticateService);
    var isUserAuth = authenticateService.isUserAuthenticated();
    if (isUserAuth) {
        return true;
    }
    var router = core_1.inject(router_1.Router);
    router.navigate(['/login']);
    return false;
};
exports.authGuard = authGuard;
