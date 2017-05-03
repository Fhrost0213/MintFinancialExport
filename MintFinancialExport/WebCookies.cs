using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Security;
using System.Threading.Tasks;

namespace MintFinancialExport
{
    class WebCookies
    {
        public void GetCookies()
        {
            CookieContainer cookies = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = cookies;

            HttpClient client = new HttpClient(handler);
            HttpResponseMessage response = client.GetAsync("http://mint.com").Result;

            Uri uri = new Uri("http://mint.com");
            IEnumerable<Cookie> responseCookies = cookies.GetCookies(uri).Cast<Cookie>();

            uri = new Uri("http://intuit.com");
            responseCookies = cookies.GetCookies(uri).Cast<Cookie>();

            uri = new Uri("http://accounts.intuit.com");
            responseCookies = cookies.GetCookies(uri).Cast<Cookie>();

            uri = new Uri("http://mint.intuit.com");
            responseCookies = cookies.GetCookies(uri).Cast<Cookie>();
            //foreach (Cookie cookie in responseCookies)
            //    Console.WriteLine(cookie.Name + ": " + cookie.Value);

            //Console.ReadLine();

            HttpWebRequest request = null;
            request = HttpWebRequest.Create("http://mint.com") as HttpWebRequest;
            HttpWebResponse TheRespone = (HttpWebResponse)request.GetResponse();
            String setCookieHeader = TheRespone.Headers[HttpResponseHeader.SetCookie];

        }

        public Tuple<string, string> GetSessionAndGuidFromCookies()
        {
            string session = "";
            string guid = "";

            ChromeCookieReader wb = new ChromeCookieReader();
            var cookies = wb.ReadCookies("mint.intuit.com");
            var test = wb.ReadCookies("pf.intuit.com");

            foreach (var cookie in cookies)
            {
                if (cookie.Item1 == "MINTJSESSIONID") session = cookie.Item2;
                if (cookie.Item1 == "userguid") guid = cookie.Item2;

            }

            return new Tuple<string, string>(session, guid);
        }

    }

    public class ChromeCookieReader
    {
        public IEnumerable<Tuple<string, string>> ReadCookies(string hostName)
        {
            if (hostName == null) throw new ArgumentNullException("hostName");

            var dbPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Google\Chrome\User Data\Default\Cookies";
            if (!System.IO.File.Exists(dbPath)) throw new System.IO.FileNotFoundException("Cant find cookie store", dbPath); // race condition, but i'll risk it

            var connectionString = "Data Source=" + dbPath + ";pooling=false";

            using (var conn = new System.Data.SQLite.SQLiteConnection(connectionString))
            using (var cmd = conn.CreateCommand())
            {
                var prm = cmd.CreateParameter();
                prm.ParameterName = "hostName";
                prm.Value = hostName;
                cmd.Parameters.Add(prm);

                cmd.CommandText = "SELECT name,encrypted_value FROM cookies WHERE host_key = @hostName";

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var encryptedData = (byte[])reader[1];
                        var decodedData = System.Security.Cryptography.ProtectedData.Unprotect(encryptedData, null, System.Security.Cryptography.DataProtectionScope.CurrentUser);
                        var plainText = Encoding.ASCII.GetString(decodedData); // Looks like ASCII

                        yield return Tuple.Create(reader.GetString(0), plainText);
                    }
                }
                conn.Close();
            }
        }
    }
}
