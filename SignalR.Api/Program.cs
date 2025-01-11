const string allowAngularApp = "AllowAngularApp";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder
    .AddJwtAuthentication()
    .BuildPersistence()
    .Services
    .AddControllers()
    .Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(options =>
    {
        //hub methods
        options.AddSignalRSwaggerGen(a =>
        {
            a.AutoDiscover = AutoDiscover.MethodsAndParams;
            a.ScanAssembly(typeof(SignalR.Application.Startup).Assembly);
        });
    }).AddCors(options =>
    {
        options.AddPolicy(allowAngularApp, policy =>
        {
            policy.WithOrigins("http://localhost:4200") // Replace with your Angular app's URL
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.UseSwagger().UseSwaggerUI();

app.UsePersistence()
    .UseCors(allowAngularApp)
    .UseHttpsRedirection()
    .UseRouting()
    .UseAuthentication()
    .UseAuthorization();
app.MapControllers();
app.Run();
