var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder
    .BuildPersistence()
    .Services
    .AddControllersWithViews()
    .Services
    .AddAntiforgery(op =>
    {
        op.FormFieldName = op.HeaderName = "AToken";
    });

builder.Services.AddAuthentication(op =>
{
    op.DefaultAuthenticateScheme = op.DefaultScheme
        = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/Account/Login"; // Path to the login page
    options.LogoutPath = "/Account/Logout"; // Path to the logout page
})
.Services
.AddAuthorization(a =>
{
    a.DefaultPolicy = new AuthorizationPolicyBuilder(CookieAuthenticationDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error")
        .UseHsts();
}

app.UsePersistence()
    .UseHttpsRedirection()
    .UseStaticFiles()
    .UseRouting()
    .UseAuthorization()
    .UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
