using System.Net;

namespace PP_ERP.DTO.BaseDTO
{
    public class RESULT_REST_RESPONSE<T>
    {
        public bool IS_SUCCESS { get; set; }
        public HttpStatusCode STATUS_CODE { get; set; }
        public HttpContent? RESULT_CONTENT { get; set; }
        public T? DATA { get; set; }
    }
}
