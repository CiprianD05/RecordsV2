global using RecordsFrontEnd.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Http;
using RecordsFrontEnd;
using RecordsFrontEnd.AuthProviders;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<ICitizenService, CitizenService>();
builder.Services.AddScoped<IDocumentTypeService, DocumentTypeService>();
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<IPsychologicalProfileService, PsychologicalProfileService>();
builder.Services.AddScoped<IPsychologicalProfileSimilaritiesService, PsychologicalProfileSimilaritiesService>();
builder.Services.AddScoped<AuthenticationStateProvider, TestAuthStateProvider>();


builder.Services.AddAuthorizationCore();
builder.Services.AddAutoMapper(typeof(RecordsDTOs.AnchorProfile));
await builder.Build().RunAsync();
