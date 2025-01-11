"use strict";
exports.__esModule = true;
exports.reqHandler = void 0;
var tslib_1 = require("tslib");
var node_1 = require("@angular/ssr/node");
var express_1 = tslib_1.__importDefault(require("express"));
var node_path_1 = require("node:path");
var node_url_1 = require("node:url");
var serverDistFolder = node_path_1.dirname(node_url_1.fileURLToPath(import.meta.url));
var browserDistFolder = node_path_1.resolve(serverDistFolder, '../browser');
var app = express_1["default"]();
var angularApp = new node_1.AngularNodeAppEngine();
/**
 * Example Express Rest API endpoints can be defined here.
 * Uncomment and define endpoints as necessary.
 *
 * Example:
 * ```ts
 * app.get('/api/**', (req, res) => {
 *   // Handle API request
 * });
 * ```
 */
/**
 * Serve static files from /browser
 */
app.use(express_1["default"].static(browserDistFolder, {
    maxAge: '1y',
    index: false,
    redirect: false
}));
/**
 * Handle all other requests by rendering the Angular application.
 */
app.use('/**', function (req, res, next) {
    angularApp
        .handle(req)
        .then(function (response) {
        return response ? node_1.writeResponseToNodeResponse(response, res) : next();
    })["catch"](next);
});
/**
 * Start the server if this module is the main entry point.
 * The server listens on the port defined by the `PORT` environment variable, or defaults to 4000.
 */
if (node_1.isMainModule(import.meta.url)) {
    var port_1 = process.env['PORT'] || 4000;
    app.listen(port_1, function () {
        console.log("Node Express server listening on http://localhost:" + port_1);
    });
}
/**
 * The request handler used by the Angular CLI (dev-server and during build).
 */
exports.reqHandler = node_1.createNodeRequestHandler(app);
