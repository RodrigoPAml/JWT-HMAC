namespace JWT_HMAC.Entities
{
    public class PayloadBase
    {
        public long Exp { get; set; } = 0;

        public void AddExpirationDate(DateTime expiration)
        {
            Exp = new DateTimeOffset(expiration).ToUnixTimeSeconds();
        }
    }
}
