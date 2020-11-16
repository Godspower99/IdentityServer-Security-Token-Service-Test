using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Net.Http;
using System;
using IdentityModel;
using IdentityModel.Client;
using System.Threading.Tasks;

namespace DaemonClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = (a, b, c , d) => true;
            var httpclient = new HttpClient(httpHandler);

            var discoveryDocument = await httpclient.GetDiscoveryDocumentAsync(address: "https://localhost:5001");

            if(!discoveryDocument.IsError)
            {
                var clientToken = await httpclient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest{
                    ClientId = "daemon-client",
                    ClientSecret = "daemon-client-secret",
                    Scope = "access-protected-api",
                    Address = discoveryDocument.TokenEndpoint
                });

                if(!clientToken.IsError)
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:6001/identity");
                    httpclient.SetBearerToken(clientToken.AccessToken);
                    var response = await httpclient.GetAsync("https://localhost:6001/identity");
                    if(response.IsSuccessStatusCode)
                        Console.WriteLine(await response.Content.ReadAsStringAsync());
                    Console.WriteLine(response.StatusCode.ToString());
                }

            }
            else{
                Console.WriteLine(discoveryDocument.Error);
            }
        }
    }
}
