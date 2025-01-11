"use strict";
exports.__esModule = true;
exports.SignalRService = void 0;
var tslib_1 = require("tslib");
var core_1 = require("@angular/core");
var signalR = tslib_1.__importStar(require("@microsoft/signalr"));
var http_client_service_1 = require("./http-client.service");
var SignalRService = /** @class */ (function () {
    function SignalRService() {
    }
    SignalRService.prototype.startConnection = function () {
        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl(http_client_service_1.HttpClientService.domain + 'chatHub')
            .build();
        this.hubConnection
            .start()
            .then(function () { return console.log('Connection started'); })["catch"](function (err) { return console.log('Error while starting connection: ' + err); });
    };
    SignalRService.prototype.addReceiveMessageListener = function () {
        this.hubConnection.on('ReceiveMessage', function (user, message) {
            console.log(user + ": " + message);
        });
    };
    SignalRService.prototype.sendMessage = function (user, message) {
        this.hubConnection.invoke('SendMessage', user, message)["catch"](function (err) { return console.error(err); });
    };
    SignalRService = tslib_1.__decorate([
        core_1.Injectable({
            providedIn: 'root'
        })
    ], SignalRService);
    return SignalRService;
}());
exports.SignalRService = SignalRService;
