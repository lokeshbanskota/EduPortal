using EduPortal.DBContext;
using EduPortal.Middleware;
using EduPortal.RabbitMQ;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr")));
builder.Services.AddSingleton<RabbitMQProducer>();
builder.Services.AddHostedService<RabbitMQBackgroundService>();
builder.Services.AddBusiness(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

// Ensure database is created
//using (var scope = app.Services.CreateScope())
//{
// var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//dbContext.Database.EnsureDeleted();//delete existing tables
// dbContext.Database.EnsureCreated();//create new tables
//}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EduPortal v1"));

}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
