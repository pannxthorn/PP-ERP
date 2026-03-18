using PP_ERP.DTO.BaseDTO;

namespace PP_ERP.WEB.Services.Base
{
    public class BaseService
    {
        private readonly RestCommon _rest;
        private readonly IConfiguration _configuration;

        public BaseService(RestCommon rest, IConfiguration configuration)
        {
            _rest = rest;
            _configuration = configuration;
        }

        private string GetDomainUrl()
        {
            return _configuration["ApiBaseUrl"] ?? string.Empty;
        }

        private PARAM_REST_REQUEST BuildRequest(string route, object? body = null)
        {
            var req = new PARAM_REST_REQUEST
            {
                DOMAIN_URL = GetDomainUrl(),
                ROUTE = route,
                HEADER = new List<KeyValuePair<string, string>>
                {
                    new("Accept-Encoding", "gzip")
                }
            };

            if (body != null)
            {
                req.BODY = body;
            }

            return req;
        }

        public async Task<RESULT_REST_RESPONSE<T>> Get<T>(string route, object? body = null)
        {
            try
            {
                var req = BuildRequest(route, body);
                return await _rest.GetAsync<T>(req);
            }
            catch (Exception)
            {
                return new RESULT_REST_RESPONSE<T> { IS_SUCCESS = false };
            }
        }

        public async Task<RESULT_REST_RESPONSE<T>> Post<T>(string route, object? body = null)
        {
            try
            {
                var req = BuildRequest(route, body);
                return await _rest.PostAsync<T>(req);
            }
            catch (Exception)
            {
                return new RESULT_REST_RESPONSE<T> { IS_SUCCESS = false };
            }
        }

        public async Task<RESULT_REST_RESPONSE<T>> Put<T>(string route, object? body = null)
        {
            try
            {
                var req = BuildRequest(route, body);
                return await _rest.PutAsync<T>(req);
            }
            catch (Exception)
            {
                return new RESULT_REST_RESPONSE<T> { IS_SUCCESS = false };
            }
        }

        public async Task<RESULT_REST_RESPONSE<T>> Delete<T>(string route)
        {
            try
            {
                var req = BuildRequest(route);
                return await _rest.DeleteAsync<T>(req);
            }
            catch (Exception)
            {
                return new RESULT_REST_RESPONSE<T> { IS_SUCCESS = false };
            }
        }
    }
}
