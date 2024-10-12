using dotenv.net;
using gerenciamento_pedidos.api.Data;
using gerenciamento_pedidos.api.Services;
using Microsoft.EntityFrameworkCore;

DotEnv.Load();
var env = DotEnv.Read();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddDbContext<AppDbContext>(opts => 
    opts.UseSqlServer(env["CONNECTION_STRING"])
   .UseLazyLoadingProxies()
);

builder.Services.AddTransient<CategoryService>();
builder.Services.AddTransient<ProductService>();
builder.Services.AddTransient<TableService>();
builder.Services.AddTransient<ClientService>();
builder.Services.AddTransient<OrderService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
