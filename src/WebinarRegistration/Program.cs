using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebinarRegistration;
using WebinarRegistration.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
// Registro del servicio de asistentes (localStorage)
builder.Services.AddScoped<IAttendeeService, LocalStorageAttendeeService>();

await builder.Build().RunAsync();
