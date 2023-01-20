using DSD605Ass2MVC.AuthorizationRequirements;
using DSD605Ass2MVC.Data;

using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var CORSAllowSpecificOrigins = "_CORSAllowed";

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CORSAllowSpecificOrigins,
                         policy =>
                         {
                             policy.WithOrigins("http://localhost:3000", "http://www.contoso.com");
                         });
});


// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//the default identity of the user
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
     .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();


//builder.Services.AddSingleton<IAuthorizationHandler, IsInRoleHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ViewAgeRequirement>();
builder.Services.AddSingleton<IAuthorizationHandler, ViewRolesRequirement>();


builder.Services.AddControllersWithViews();

builder.Services.Configure<PasswordHasherOptions>(options => { options.IterationCount = 310000; });




builder.Services.AddRazorPages();


builder.Services.AddAuthorization(options =>
{

    //1 staff with over 6 months of service and permission View Roles can view roles
    options.AddPolicy("ViewRolesPolicy", policyBuilder => policyBuilder.RequireAssertion(context =>
    {
        var joiningDateClaim = context.User.FindFirst(c => c.Type == "Joining Date")?.Value;
        var joiningDate = Convert.ToDateTime(joiningDateClaim);
        return context.User.HasClaim("Permission", "View Roles") && joiningDate > DateTime.MinValue && joiningDate < DateTime.Now.AddMonths(-6);
    }));

    //2 staff with over 6 months of service and permission View Claims can view claims folder
    options.AddPolicy("ViewClaimsPolicy", policyBuilder => policyBuilder.RequireAssertion(context =>
    {
        var joiningDateClaim = context.User.FindFirst(c => c.Type == "Joining Date")?.Value;
        var joiningDate = Convert.ToDateTime(joiningDateClaim);
        return context.User.HasClaim("Permission", "View Claims") && joiningDate > DateTime.MinValue && joiningDate < DateTime.Now.AddMonths(-6);
    }));



    //3 staff with permission Delete Stock can Delete Stock
    options.AddPolicy("DeleteStockPolicy", policyBuilder => policyBuilder.RequireAssertion(context =>
    {
        return context.User.HasClaim("Permission", "Delete Stock");
    }));

    //4 staff with permission Edit Stock can Edit Stock
    options.AddPolicy("EditStockPolicy", policyBuilder => policyBuilder.RequireAssertion(context =>
    {
        return context.User.HasClaim("Permission", "Edit Stock");
    }));

    //5 staff over 18 can add stock
    options.AddPolicy("AddStockPolicy", policyBuilder => policyBuilder.RequireAssertion(context =>
    {

        var dateOfBirthClaim = context.User.FindFirst(c => c.Type == "DateOfBirth");

        if (dateOfBirthClaim is null)
        {
            return false;
        }

        //convert to date
        var dateOfBirthUser = Convert.ToDateTime(dateOfBirthClaim.Value);

        //get users age
        int calculatedAgeUser = DateTime.Today.Year - dateOfBirthUser.Year;

        if (calculatedAgeUser >= 18)
        {
            return true;
        }

        return false;
    }));



});

//Having configured the policy named AdminPolicy, we can apply it to the AuthorizeFolder method to ensure that only members of the Admin role can access the content: 
builder.Services.AddRazorPages(options =>
{

    options.Conventions.AuthorizeFolder("/RolesManager", "ViewRolesPolicy");
    options.Conventions.AuthorizeFolder("/ClaimsManager", "ViewClaimsPolicy");

});



//=======NEW SECURITY============

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
    options.SignIn.RequireConfirmedEmail = false;


    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;

});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});

//=============END NEW SECURITY================

builder.Services.AddSwaggerGen();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseSwagger();
    app.UseSwaggerUI();

}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors(CORSAllowSpecificOrigins);


app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
