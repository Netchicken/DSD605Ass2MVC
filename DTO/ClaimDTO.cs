namespace DSD605Ass2MVC.DTO
{
    public class ClaimDTO
    {


        public ClaimDTO(string user, string type, string value, string issuer)
        {
            User = user;
            Type = type;
            Value = value;
            Issuer = issuer;
        }


        public string Type { get; set; }
        public string Issuer { get; set; }
        public string Value { get; set; }
        public string User { get; set; }
    }
}
