using Scalar.AspNetCore;
using SingHub.Application.Extensions;
using SingHub.Persistence.Extensions;
using SingHub.WebAPI.CustomMiddlewares;
using SingHub.WebAPI.Registration;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AppApplicationSetting(builder.Configuration);
builder.Services.AppPersistenceSetting(builder.Configuration);
builder.Services.AddOpenApi();

var app = builder.Build();
await app.UseDbSeederAsync();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.UseMiddleware<CustomExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGroup("/api").RegisterEndpoints();
app.Run();
