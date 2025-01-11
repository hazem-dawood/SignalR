"use strict";
exports.__esModule = true;
exports.serverRoutes = void 0;
var ssr_1 = require("@angular/ssr");
exports.serverRoutes = [
    {
        path: '**',
        renderMode: ssr_1.RenderMode.Prerender
    }
];
