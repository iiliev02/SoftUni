﻿using MiniServer.Application.Controllers;
using MiniServer.Server.Contracts;
using MiniServer.Server.Handlers;
using MiniServer.Server.Routing.Contracts;

namespace MiniServer.Application
{
    public class MainApplication : IApplication
    {
        public void Start(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.AddRoute("/", new GETHandler(httpContext => new HomeController().Index()));

            appRouteConfig.AddRoute("/about", new GETHandler(httpContext => new HomeController().AboutUs()));

            appRouteConfig.AddRoute("/search", new GETHandler(httpContext => new HomeController().SearchGet(httpContext.Request)));

            appRouteConfig.AddRoute(
                "/add",
                new POSTHandler(
                    httpContext => new HomeController()
                    .AddPost(httpContext.Request.FormData["name"], double.Parse(httpContext.Request.FormData["price"]), httpContext.Request.FormData["url"])));
            appRouteConfig.AddRoute(
                "/add",
                new GETHandler(httpContext => new HomeController().AddGet()));

            appRouteConfig.AddRoute(
                "/login",
                new POSTHandler(
                    httpContext => new UserController()
                    .LoginPost(httpContext.Request)));
            appRouteConfig.AddRoute(
                "/login",
                new GETHandler(httpContext => new UserController().LoginGet()));

            appRouteConfig.AddRoute(
                 "/register",
                 new POSTHandler(
                     httpContext => new UserController()
                     .RegisterPost(httpContext.Request)));
            appRouteConfig.AddRoute(
                "/register",
                new GETHandler(httpContext => new UserController().RegisterGet()));

            appRouteConfig.AddRoute(
                "/profile",
                new GETHandler(httpContext => new UserController().ProfileGet(httpContext.Request.Session)));

            appRouteConfig.AddRoute(
                "/cakeDetails/{(?<id>[0-9]+)}",
                new GETHandler(httpContext => new HomeController()
                .CakeDetails(httpContext.Request.UrlParameters["id"])));

            appRouteConfig.AddRoute(
                 "/order",
                 new GETHandler(httpContext => new ShoppingController().Order(httpContext.Request)));

            appRouteConfig.AddRoute(
                 "/orders",
                 new GETHandler(httpContext => new ShoppingController().Orders(httpContext.Request)));

            appRouteConfig.AddRoute(
                "/orderDetails/{(?<id>[0-9]+)}",
                new GETHandler(httpContext => new ShoppingController()
                .OrderDetails(httpContext.Request.UrlParameters["id"])));

            appRouteConfig.AddRoute(
                 "/cart",
                 new GETHandler(httpContext => new ShoppingController().CartGet(httpContext.Request)));
            appRouteConfig.AddRoute(
                 "/cart",
                 new POSTHandler(httpContext => new ShoppingController().CartPost(httpContext.Request)));

            appRouteConfig.AddRoute("/success", new GETHandler(httpContext => new ShoppingController().Success(httpContext.Request)));

            appRouteConfig.AddRoute("/logout", new GETHandler(httpContext => new UserController().Logout(httpContext.Request)));

            appRouteConfig.AddRoute(
                @"/Images/{(?<imagePath>[a-zA-Z0-9_]+\.(jpg|png))}",
                new GETHandler(httpContext => new HomeController()
                .Image(httpContext.Request.UrlParameters["imagePath"])));

            appRouteConfig.AddRoute("/test", new GETHandler(httpContext => new HomeController().SessionTest(httpContext.Request)));
        }
    }
}