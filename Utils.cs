using System.Text;

namespace JWT_HMAC
{
    public static class Utils
    {
        public static string CleanBase64(string b64)
        {
            return b64.TrimEnd('=')
                .Replace('+', '-')
                .Replace('/', '_');
        }

        public static string BuildOriginal64(string input)
        {
            string output = input
                .Replace('-', '+')
                .Replace('_', '/');

            switch (output.Length % 4)
            {
                case 2: output += "=="; break;
                case 3: output += "="; break;
            }

            var bytes = Convert.FromBase64String(output);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
