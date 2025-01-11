"use strict";
exports.__esModule = true;
exports.setTokenInterceptor = void 0;
var http_1 = require("@angular/common/http");
var rxjs_1 = require("rxjs");
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var authenticate_service_1 = require("../services/auth/authenticate.service");
var setTokenInterceptor = function (request, next) {
    var authenticateService = core_1.inject(authenticate_service_1.AuthenticateService);
    var router = core_1.inject(router_1.Router);
    var token = '';
    if (authenticateService.isUserAuthenticated()) {
        token = authenticateService.getToken();
    }
    if (token) {
        request = request.clone({
            setHeaders: {
                'authorization': 'Bearer ' + token
            }
        });
    }
    else {
        request = request.clone({
            setHeaders: {}
        });
    }
    return next(request).pipe(rxjs_1.tap(function (event) {
        if (event instanceof http_1.HttpResponse) {
            var to = event.headers.get('token');
            authenticateService.setLastAntToken(to);
        }
    }, function (error) {
        if (error.status === 401) {
            authenticateService.removeAuthentication();
            router.navigate(['/login']);
        }
        else {
        }
    }));
};
exports.setTokenInterceptor = setTokenInterceptor;
