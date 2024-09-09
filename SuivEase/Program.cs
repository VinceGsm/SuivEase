using Business.Services;
using DAL.Data;
using DAL.Repos;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using SuivEase.Components;
using SuivEase.Components.Account;


try
{
    var builder = WebApplication.CreateBuilder(args);

    // Aspire (discovery, resilience, health checks, and OpenTelemetry)
    builder.AddServiceDefaults();
    //builder.AddRedisOutputCache("cache");

    builder.Services.AddMudServices(); // MudBlazor

    // Add services to the container.
    builder.Services.AddRazorComponents()
        .AddInteractiveServerComponents() 
        .AddInteractiveWebAssemblyComponents();

    // Identity
    builder.Services.AddCascadingAuthenticationState();
    builder.Services.AddScoped<IdentityUserAccessor>();
    builder.Services.AddScoped<IdentityRedirectManager>();
    builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();    
    builder.Services.AddAuthorization();
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
        .AddIdentityCookies();

    // Database
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));    

    // Identity 2    
    builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<ApplicationRole>() 
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

    //Service    
    builder.Services.AddTransient<ISuiviService, SuiviService>();
    builder.Services.AddTransient<IContactService, ContactService>();
    builder.Services.AddTransient<IAddressService, AddressService>();

    //Repos
    builder.Services.AddTransient<ISuiviRepository, SuiviRepository>();
    builder.Services.AddTransient<IAddressRepository, AddressRepository>();
    builder.Services.AddTransient<IContactRepository, ContactRepository>();

    builder.Services.AddControllers(); // CTRL

    // Swagger
    builder.Services.AddSwaggerGen(x =>
    {
        x.SchemaFilter<EnumSchemaFilter>();

    }); 

    builder.Services.AddAntiforgery(); // Antiforgery

    // TO DO : comprendre ?
    builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
    
    ///////////////////////////////////// BUILD ////////////////////////////////////////////////////////////
    var webApp = builder.Build();    

    // Configure the HTTP request pipeline.
    if (webApp.Environment.IsDevelopment())
    {
        webApp.UseWebAssemblyDebugging();
        webApp.UseMigrationsEndPoint();
        webApp.UseSwagger();
        webApp.UseSwaggerUI();
    }
    else
    {        
        // Strict-Transport-Security HeaderInforms browsers that the site should only be accessed using HTTPS
        // And that any future attempts to access it using HTTP should automatically be converted to HTTPS
        webApp.UseHsts();
        webApp.UseExceptionHandler("/Error", createScopeForErrors: true);
    }

    // name ?
    webApp.UseHttpsRedirection();
    webApp.UseStaticFiles();
    webApp.MapRazorComponents<App>()
        .AddInteractiveServerRenderMode()
        .AddInteractiveWebAssemblyRenderMode()        
        .AddAdditionalAssemblies(typeof(SuivEase.Client._Imports).Assembly);

    // Add endpoints required by the Identity /Account Razor components.
    webApp.MapAdditionalIdentityEndpoints();

    // name ?
    webApp.UseAuthorization();

    // Use routing and map endpoints
    webApp.UseRouting();
    webApp.UseAntiforgery(); // need to be between Routing + Endpoints
    webApp.MapDefaultEndpoints();
    webApp.MapControllers();
    
    webApp.Run();
}
catch (Exception ex)
{
    // TO DO : log
}
finally
{
    // ?
}