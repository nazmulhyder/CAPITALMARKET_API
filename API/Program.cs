using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();
builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    options.FallbackPolicy = options.DefaultPolicy;
});
builder.Services.AddControllers();
builder.Services.AddMvc();
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();

//REGULAR SWAGGER FOR WINDOWS AUTHENTICATION
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API_CMBO_IMnTP", Version = "v1" });

});


//SERVICES REGISTER
builder.Services.AddInfrastructure();
builder.Services.AddAuthorization(options => { });




// ADDING API VERSIONING
builder.Services.AddApiVersioning(options =>
{
    //DEFAULT TAKING VERSION 1.O-- IF CONTROLLER IS NOT MAPPED THEN NO NEED TO MENSION VERSION IN THE REQUEST
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader =
    ApiVersionReader.Combine(
       //REQUEST HEADER KEY WITH VERSION
       new HeaderApiVersionReader("api-Version"),
       new QueryStringApiVersionReader("api-version"),
        new MediaTypeApiVersionReader("version"));
    //IF A VERSION IS MAPPED THEN WITHOUT MENSION VERSION RETURN UNSUPPORTEDAPI VERSION ERROR
});

builder.Services.AddVersionedApiExplorer(
    options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

var app = builder.Build();
app.Environment.IsDevelopment();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}
if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
        c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "My API");

    });
}

app.UseHttpsRedirection();

//TOKEN AUTHENTICATION
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
//Enable Static File Middleware
app.UseStaticFiles();
app.MapRazorPages();

app.Run();
