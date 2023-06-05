using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name : MyAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins("http://localhost", "https://localhost", "https://localhost:7237/",
            "http://localhost:7237/", "http://localhost:5241", "https://localhost:5241", "*")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

//builder.Services.InitApplicationModules( new MenUiApiModule());
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "MenIU API" });
    swagger.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        In = ParameterLocation.Header
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    { {
        new OpenApiSecurityScheme { Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme, Id = "bearerAuth"
        } }, new string [] { } }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseDefaultFiles();

app.UseStaticFiles();
app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
