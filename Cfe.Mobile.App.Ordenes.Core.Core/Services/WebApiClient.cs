using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Cfe.Mobile.App.Ordenes.Core.Core.Services {
    public class WebApiClient : HttpClient {
        public WebApiClient() {
            BaseAddress = new Uri(UrlBaseWebApi);
            InitHeaders();
        }

        public WebApiClient(string urlController) {
            UrlController = urlController;
            InitHeaders();
        }

        public string UrlBaseWebApi { get; set; } = "https://appssureste.azurewebsites.net/api";
            //"http://10.18.144.180/sistemas/OrdenesWeb/api/";
            //"http://localhost:55803/api";

        string urlController;
        protected string UrlController {
            get => urlController;
            set {
                urlController = !value.EndsWith("/") ? value + "/" : value;
                UrlBaseWebApi += (!UrlBaseWebApi.EndsWith("/") ? "/" : string.Empty) + value;
                BaseAddress = new Uri(UrlBaseWebApi);
            }
        }

        void InitHeaders() {
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
            //    Convert.ToBase64String(Encoding.UTF8.GetBytes(
            //        string.Format("{0}:{1}", "android", "p4zzM0biil3"))));
        }

        public async Task<TResponse> CallPostAsync<TRequest, TResponse>(string url, TRequest req) {
            var res = await PostAsync(url, new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json")).ConfigureAwait(false);
            if (res.IsSuccessStatusCode) {
                var s = res.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<TResponse>(s);
                return response;
            } else {
                return default(TResponse);
            }
        }

        public async Task<TResponse> CallGetAsync<TResponse>(string url) {
            var res = await GetAsync(url).ConfigureAwait(false);
            if (res.IsSuccessStatusCode) {
                var s = res.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<TResponse>(s);
                return response;
            } else {
                return default(TResponse);
            }
        }

        public async Task<TResponse> CallPostFileAsync<TResponse>(string url, byte[] file, string contentName, string fileName, string mediaType, HttpContent extraContent = null, string extraName = "") {
            //http://stackoverflow.com/questions/16416601/c-sharp-httpclient-4-5-multipart-form-data-upload
            var requestContent = new MultipartFormDataContent();
            var imageContent = new ByteArrayContent(file);
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse(mediaType);
            requestContent.Add(imageContent, contentName, fileName);
            if (extraContent != null)
                requestContent.Add(extraContent, extraName);
            var res = await PostAsync(url, requestContent);
            if (res.IsSuccessStatusCode) {
                var s = res.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<TResponse>(s);
                return response;
            } else {
                return default(TResponse);
            }
        }
    }
}
