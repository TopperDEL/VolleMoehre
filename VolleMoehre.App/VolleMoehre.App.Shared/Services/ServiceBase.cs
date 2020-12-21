using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace VolleMoehre.Shared.Services
{
    public abstract class ServiceBase
    {
        protected HttpClient GetClient(string apiKey = "")
        {
#if __WASM__
            var handler = new Uno.UI.Wasm.WasmHttpHandler();
            var client = new HttpClient(handler);
#else
#if DEBUG && !__ANDROID__
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
            HttpClient client = new HttpClient(handler);
#else
            HttpClient client = new HttpClient();
#endif
#endif
#if DEBUG && !__ANDROID__
            //client.BaseAddress = new Uri("https://localhost:44384/api/");
            client.BaseAddress = new Uri("https://api.vollemoehre.de/api/");
#else
            client.BaseAddress = new Uri("https://api.vollemoehre.de/api/");
#endif
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("vm-userkey", apiKey);
            return client;
        }

        public static bool IsOnline()
        {
#if __WASM__
            return true;
#else
            return CrossConnectivity.Current.IsConnected;
#endif
        }
    }
}
