global using RecordsFrontEnd.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RecordsFrontEnd;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<ICitizenService, CitizenService>();
builder.Services.AddScoped<IDocumentTypeService, DocumentTypeService>();

builder.Services.AddAutoMapper(typeof(RecordsDTOs.AnchorProfile));
await builder.Build().RunAsync();
