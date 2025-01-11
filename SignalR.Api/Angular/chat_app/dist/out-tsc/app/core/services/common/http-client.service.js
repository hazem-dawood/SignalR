"use strict";
exports.__esModule = true;
exports.HttpClientService = void 0;
var tslib_1 = require("tslib");
var core_1 = require("@angular/core");
var HttpClientService = /** @class */ (function () {
    function HttpClientService(_httpClient) {
        this._httpClient = _httpClient;
    }
    HttpClientService_1 = HttpClientService;
    HttpClientService.prototype.get = function (url) {
        return this._httpClient.get(HttpClientService_1._baseUrl + url);
    };
    HttpClientService.prototype.post = function (url, model) {
        return this._httpClient.post(HttpClientService_1._baseUrl + url, model);
    };
    var HttpClientService_1;
    HttpClientService.domain = 'https://localhost:7170/';
    HttpClientService._baseUrl = HttpClientService_1.domain + "api/";
    HttpClientService = HttpClientService_1 = tslib_1.__decorate([
        core_1.Injectable({
            providedIn: 'root'
        })
    ], HttpClientService);
    return HttpClientService;
}());
exports.HttpClientService = HttpClientService;
