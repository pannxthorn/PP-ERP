using Newtonsoft.Json;
using PP_ERP.DTO.BaseDTO;
using System.Net.Http.Headers;
using System.Text;

namespace PP_ERP.WEB.Services.Base
{
    public class RestCommon
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RestCommon(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private HttpClient InitHttpClient(PARAM_REST_REQUEST args)
        {
            var client = _httpClientFactory.CreateClient();

            if (!string.IsNullOrEmpty(args.ACCESS_TOKEN))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", args.ACCESS_TOKEN);
            }

            foreach (var header in args.HEADER)
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
            }

            return client;
        }

        private string UrlCombine(string domain, string route)
        {
            return $"{domain.TrimEnd('/')}/{route.TrimStart('/')}";
        }

        public async Task<RESULT_REST_RESPONSE<T>> GetAsync<T>(PARAM_REST_REQUEST args)
        {
            var result = new RESULT_REST_RESPONSE<T>();
            try
            {
                using var client = InitHttpClient(args);
                var url = UrlCombine(args.DOMAIN_URL, args.ROUTE);

                HttpResponseMessage res;

                if (args.BODY == null)
                {
                    res = await client.GetAsync(url);
                }
                else
                {
                    string payload = args.AUTO_SERIALIZE_OBJECT_REQUEST_BODY ? JsonConvert.SerializeObject(args.BODY) : (string)(args.BODY ?? string.Empty);

                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(url),
                        Content = new StringContent(payload, Encoding.UTF8, "application/json"),
                    };

                    res = await client.SendAsync(request).ConfigureAwait(false);
                }

                result.IS_SUCCESS = res.IsSuccessStatusCode;
                result.STATUS_CODE = res.StatusCode;
                result.RESULT_CONTENT = res.Content;

                if (res.IsSuccessStatusCode)
                {
                    var content = await res.Content.ReadAsStringAsync();
                    result.DATA = JsonConvert.DeserializeObject<T>(content);
                }

                return result;
            }
            catch (Exception)
            {
                result.IS_SUCCESS = false;
                return result;
            }
        }

        public async Task<RESULT_REST_RESPONSE<T>> PostAsync<T>(PARAM_REST_REQUEST args)
        {
            var result = new RESULT_REST_RESPONSE<T>();
            try
            {
                using var client = InitHttpClient(args);
                var url = UrlCombine(args.DOMAIN_URL, args.ROUTE);

                string payload = args.AUTO_SERIALIZE_OBJECT_REQUEST_BODY
                    ? JsonConvert.SerializeObject(args.BODY)
                    : (string)(args.BODY ?? string.Empty);

                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var res = await client.PostAsync(url, content);

                result.IS_SUCCESS = res.IsSuccessStatusCode;
                result.STATUS_CODE = res.StatusCode;
                result.RESULT_CONTENT = res.Content;

                if (res.IsSuccessStatusCode)
                {
                    var resContent = await res.Content.ReadAsStringAsync();
                    result.DATA = JsonConvert.DeserializeObject<T>(resContent);
                }

                return result;
            }
            catch (Exception)
            {
                result.IS_SUCCESS = false;
                return result;
            }
        }

        public async Task<RESULT_REST_RESPONSE<T>> PutAsync<T>(PARAM_REST_REQUEST args)
        {
            var result = new RESULT_REST_RESPONSE<T>();
            try
            {
                using var client = InitHttpClient(args);
                var url = UrlCombine(args.DOMAIN_URL, args.ROUTE);

                string payload = args.AUTO_SERIALIZE_OBJECT_REQUEST_BODY
                    ? JsonConvert.SerializeObject(args.BODY)
                    : (string)(args.BODY ?? string.Empty);

                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var res = await client.PutAsync(url, content);

                result.IS_SUCCESS = res.IsSuccessStatusCode;
                result.STATUS_CODE = res.StatusCode;
                result.RESULT_CONTENT = res.Content;

                if (res.IsSuccessStatusCode)
                {
                    var resContent = await res.Content.ReadAsStringAsync();
                    result.DATA = JsonConvert.DeserializeObject<T>(resContent);
                }

                return result;
            }
            catch (Exception)
            {
                result.IS_SUCCESS = false;
                return result;
            }
        }

        public async Task<RESULT_REST_RESPONSE<T>> DeleteAsync<T>(PARAM_REST_REQUEST args)
        {
            var result = new RESULT_REST_RESPONSE<T>();
            try
            {
                using var client = InitHttpClient(args);
                var url = UrlCombine(args.DOMAIN_URL, args.ROUTE);

                var res = await client.DeleteAsync(url);

                result.IS_SUCCESS = res.IsSuccessStatusCode;
                result.STATUS_CODE = res.StatusCode;
                result.RESULT_CONTENT = res.Content;

                if (res.IsSuccessStatusCode)
                {
                    var resContent = await res.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(resContent))
                    {
                        result.DATA = JsonConvert.DeserializeObject<T>(resContent);
                    }
                }

                return result;
            }
            catch (Exception)
            {
                result.IS_SUCCESS = false;
                return result;
            }
        }
    }
}
