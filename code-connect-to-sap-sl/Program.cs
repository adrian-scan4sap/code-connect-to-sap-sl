using code_connect_to_sap_sl.Model;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace code_connect_to_sap_sl
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Request details
            string url = "http://sap-server-ip:50001/b1s/v1/Login";
            LoginRequest loginRequest = new LoginRequest()
            {
                UserName = "sap-user",
                Password = "sap-password",
                CompanyDB = "company-database"
            };

            // Serialize request body to JSON
            string jsonRequestBody = JsonConvert.SerializeObject(loginRequest);

            // Make the request
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.KeepAlive = true;
            httpWebRequest.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            httpWebRequest.ServicePoint.Expect100Continue = false;

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            { 
                streamWriter.Write(jsonRequestBody); 
            }

            try
            {
                // Call Service Layer
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    Console.WriteLine(result);

                    // Deserialize success login response
                    var responseInstance = JsonConvert.DeserializeObject<LoginResponse>(result);
                }
            }
            catch (Exception ex) 
            {
                // Unauthorized, etc.
                Console.WriteLine("Unexpected: " + ex.Message);
            }

            Console.ReadLine();
        }
    }
}
