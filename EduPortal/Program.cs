using EduPortal.Authentication;
using EduPortal.DBContext;
using EduPortal.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr")));
// For Identity--AuthenticateController
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
//for authentication JWT token
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
// Add the middleware to the pipeline
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
// Ensure database is created
//using (var scope = app.Services.CreateScope())
//{
// var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//dbContext.Database.EnsureDeleted();//delete existing tables
// dbContext.Database.EnsureCreated();//create new tables
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
