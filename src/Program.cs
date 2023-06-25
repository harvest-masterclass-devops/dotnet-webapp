using System.Diagnostics.Metrics;
using OpenTelemetry;
using OpenTelemetry.Metrics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

Meter myMeter = new("MyCompany.MyProduct.MyLibrary", "1.0");    
builder.Services.AddOpenTelemetry()
    .WithMetrics(builder => builder
        .AddMeter("MyCompany.MyProduct.MyLibrary")
        .AddPrometheusExporter());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
// http://localhost:9006/metrics
app.UseOpenTelemetryPrometheusScrapingEndpoint();


Counter<long> MyFruitCounter = myMeter.CreateCounter<long>("MyFruitCounter");

// http://localhost:9006/add/apple/purple
app.MapGet("/add/{fruit}/{color}", (string fruit, string color) =>
{
    MyFruitCounter.Add(1, new("name", fruit), new("color", color));
    return "increased";
});

MyFruitCounter.Add(1, new("name", "apple"), new("color", "red"));
MyFruitCounter.Add(2, new("name", "lemon"), new("color", "yellow"));
MyFruitCounter.Add(1, new("name", "lemon"), new("color", "yellow"));
MyFruitCounter.Add(2, new("name", "apple"), new("color", "green"));
MyFruitCounter.Add(5, new("name", "apple"), new("color", "red"));
MyFruitCounter.Add(4, new("name", "lemon"), new("color", "yellow"));

app.Run();

