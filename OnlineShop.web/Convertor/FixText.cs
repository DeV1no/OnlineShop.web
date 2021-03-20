namespace OnlineShop.web.Convertor
{
    public class FixText
    {
        public static string FixEmail(string email)
        {
            return email.Trim().ToLower();
        }
    }
}