

using IBKR_Service;
using IBKR_Service.Config;
using IBKR_Service.Handlers;
using IBKR_Service.Interfaces;
using IBKR_Service.Services;
using IBKR_TradeBridge;
using IBKR_TradeBridge.Adapters;
using IBKR_TradeBridge.Config;
using IBKR_TradeBridge.Handlers;
using IBKR_TradeBridge.Interfaces;
using IBKR_TradeBridge.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;

//var builder = Host.CreateApplicationBuilder(args);

////var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


////builder.Services.AddCors(options =>{
////    options.AddPolicy(name: MyAllowSpecificOrigins,
////                  policy =>
////                  {
////                  policy.WithOrigins("",
////                                              "https://www.localhost:5000") // Replace with your client's origin
////                                .AllowAnyHeader()
////                                .AllowAnyMethod();
////                      });
////});

////
////builder.Services.AddHttpClient();
//builder.Services.AddHttpClient("IBKR", client =>
//{
//    client.BaseAddress = new Uri("https://localhost:5000");
//}).ConfigurePrimaryHttpMessageHandler(() =>
//    new HttpClientHandler
//    {
//        ServerCertificateCustomValidationCallback =
//            (sender, cert, chain, sslPolicyErrors) => true
//    });

//builder.Services.AddHostedService<Worker>();

//var host = builder.Build();
//host.Run();

//eyJpZCI6IjYwMzRkNjM5IiwibWFjIjoiMTY6MjE6NjU6RkI6MTQ6MDUifQ


var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHttpClient();

builder.Services.AddSingleton<ApiMessenger>();

// ✅ Register your Worker
builder.Services.Configure<IbkrSettings>(
    builder.Configuration.GetSection("IBKR")
);
//builder.Services.AddHostedService<Worker>();
//builder.Services.AddHostedService<ESocketWorker>();






builder.Services.Configure<BridgeSettings>(builder.Configuration.GetSection("BridgeSettings"));

// Core registrations
builder.Services.AddSingleton<IIbkrClient, IbkrClient>();
builder.Services.AddSingleton<IOrderPipeline, OrderPipeline>();
builder.Services.AddSingleton<IOrderAdapter, CfdToFutureAdapter>();
builder.Services.AddSingleton<IOrderRepository, OrderRepository>();
builder.Services.AddSingleton<IMt5Listener, Mt5FileListener>();
builder.Services.AddSingleton<ApiMessenger>();

// Pipeline handlers (order matters)
builder.Services.AddTransient<IOrderHandler, SymbolValidationHandler>();
builder.Services.AddTransient<IOrderHandler, SymbolMappingHandler>();
builder.Services.AddTransient<IOrderHandler, RiskCheckHandler>();
builder.Services.AddTransient<IOrderHandler, BuildIbkrOrderHandler>();
builder.Services.AddTransient<IOrderHandler, SendToIbkrHandler>();
builder.Services.AddTransient<IOrderHandler, LoggingHandler>();

builder.Services.AddTransient<SuccessfulResponseHanlder>();
builder.Services.AddTransient<ConfirmationResponseHanlder>();
builder.Services.AddTransient<ErrorResponseHanlder>();
builder.Services.AddTransient<CheckPnlResponseHanlder>();



builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();

