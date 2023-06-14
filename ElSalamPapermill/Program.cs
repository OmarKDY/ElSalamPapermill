using ElSalamPapermill.Data;
using ElSalamPapermill.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;

    // Configure the password complexity requirements
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddSingleton<IEmailSender, SendGridEmailSender>();
builder.Services.Configure<SendGridSettings>(
     builder.Configuration.GetSection("SendGrid")
);
builder.Services.AddResponseCaching();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
    //app.UseHsts();
}

app.Use(async (context, next) =>
{
    await next().ConfigureAwait(true);
    if (context.Response.StatusCode == 404)
    {
        context.Request.Path = "/index.html";
        await next().ConfigureAwait(true);
    }
});

app.UseStaticFiles();
//app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseResponseCaching();
app.MapControllers();

app.Run();
