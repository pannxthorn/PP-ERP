namespace PP_ERP.DTO.BaseDTO
{
    public class PARAM_REST_REQUEST
    {
        public string DOMAIN_URL { get; set; } = string.Empty;
        public string ROUTE { get; set; } = string.Empty;
        public List<KeyValuePair<string, string>> HEADER { get; set; } = new();
        public object? BODY { get; set; }
        public string? ACCESS_TOKEN { get; set; }
        public bool AUTO_SERIALIZE_OBJECT_REQUEST_BODY { get; set; } = true;
    }
}
