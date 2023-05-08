namespace code_connect_to_sap_sl.Model
{
    public class LoginResponse
    {
        public string odatametadata { get; set; }
        public string SessionId { get; set; }
        public string Version { get; set; }
        public int SessionTimeout { get; set; }
    }
}
