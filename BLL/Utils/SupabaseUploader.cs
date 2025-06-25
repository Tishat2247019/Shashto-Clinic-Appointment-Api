using RestSharp;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BLL.Utils
{
    public class SupabaseUploader
    {
        private static string supabaseUrl = "https://xzqhltkwariwyxptggkt.supabase.co";
        private static string bucketName = "appointment-tokens";
        private static string serviceRoleKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Inh6cWhsdGt3YXJpd3l4cHRnZ2t0Iiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTc1MDg0MDgwMywiZXhwIjoyMDY2NDE2ODAzfQ.rojyhFW5t-zQw0SHWcxCZQsVZfaYifTOG_mBky84s6s";

        private static readonly HttpClient httpClient = new HttpClient();

        public static async Task<string> UploadPDF(byte[] fileBytes, string fileName)
        {
            var url = $"{supabaseUrl}/storage/v1/object/{bucketName}/{fileName}";

             var content = new ByteArrayContent(fileBytes);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");

             var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", serviceRoleKey);
            request.Content = content;

            var response = await httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Upload failed: {error}");
            }

            return $"{supabaseUrl}/storage/v1/object/public/{bucketName}/{fileName}";
        }

    }
}
