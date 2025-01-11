"use strict";
exports.__esModule = true;
exports.config = void 0;
var core_1 = require("@angular/core");
var platform_server_1 = require("@angular/platform-server");
var ssr_1 = require("@angular/ssr");
var app_config_1 = require("./app.config");
var app_routes_server_1 = require("./app.routes.server");
var serverConfig = {
    providers: [
        platform_server_1.provideServerRendering(),
        ssr_1.provideServerRoutesConfig(app_routes_server_1.serverRoutes)
    ]
};
exports.config = core_1.mergeApplicationConfig(app_config_1.appConfig, serverConfig);
