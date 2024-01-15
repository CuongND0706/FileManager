using FileManagerExample.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FileManagerExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileManager : ControllerBase
    {
        public FileManager(ILogger<FileManager> logger)
        {
        }

        [HttpGet(Name = "GetResource")]
        public async Task<List<ResourceOwner>> Get(string siteId)
        {
            string apiUrl = "https://api.pre.earthbrain-pf.com/v2/resource_owners?type=Site&siteId="+siteId+"";

            using HttpClient client = new();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6ImF0K2p3dCIsImtpZCI6Ijl2d1VZZUhYZ0hIYlctUG9xVVhuaE1yYjZZWVhRX1N0VzhXR0RwQlphLWcifQ.eyJhdWQiOlsiaHR0cHM6Ly9hcGktcHJlLmxhbmRsb2cuaW5mbyJdLCJzdWIiOiI2NDg3ZjliNS1kZmZhLTQxYjgtOWM5Yy01NzQzNDI2NTE1OTUiLCJncmFudF90eXBlIjoiYXV0aG9yaXphdGlvbl9jb2RlIiwiYXpwIjoiOTcxNTFlZmYtNzRjMS00YTU3LTlkOTAtNTViNmNiMmUzMzg1Iiwic2NvcGUiOiJ1c2VyLnByb2ZpbGUgZmlsZV9zdG9yYWdlLndyaXRlIGNvcnBvcmF0aW9uLmdyb3VwLnJlYWQgY29ycG9yYXRpb24ucmVhZCBvcGVuaWQgZmlsZV9zdG9yYWdlLnVwbG9hZCBsaWNlbnNlLnJlYWQgcmVzb3VyY2Vfb3duZXIucmVhZCBmaWxlX3N0b3JhZ2UuZG93bmxvYWQgc2l0ZS5tZW1iZXIucmVhZCBmaWxlX3N0b3JhZ2UucmVhZCBkZXZpY2Uud3JpdGUgZ3JvdXAucmVhZCBjb2xsZWN0aW9uLmRhdGEud3JpdGUgY29sbGVjdGlvbi5kYXRhLnJlYWQgY29ycG9yYXRpb24udXNlci5yZWFkIGRldmljZS5yZWFkIHNpdGUubWVtYmVyLndyaXRlIHNpdGUucmVhZCIsImlzcyI6Imh0dHBzOi8vYXV0aC1wcmUubGFuZGxvZy5pbmZvIiwiYXBwbGljYXRpb25fdHlwZXMiOlsid2ViIl0sImV4cCI6MTcwNTM3Njk5OCwiaWF0IjoxNzA1MjkwNTk4LCJjbGllbnRfaWQiOiI5NzE1MWVmZi03NGMxLTRhNTctOWQ5MC01NWI2Y2IyZTMzODUiLCJqdGkiOiI0MHdqc196Q1puV3RYVXZrUmtyWTc4TV9kZVdadmJJaDg0REN5YzNhUzRjIn0.WEn2tZmXHi7QdNs_5itWqF2DDsbQHj6GoWVnP3OVc6iC4gt8v6akMbkAImCHycF8Vhr6JJX-kNEnrssJY2OTAssQE_fKeo3BYhLf_HC4cy8dbxpZNcjn2rX6EKCpeK9G311Pj4sHVONVv6Hk1QB4UC80B6kXtk4nC5zziZyMuXm1PFJsgXZwI1u8d_M93UveaWQpk2TnVQ6PhDn0zDFZWGCV3iAQ_0P38U9wq1d4jrKVTsFmOb9GJliDt-etEjWm2qBAdXlxXAIdbC86JrCxTnmMHrjQYCyAjE0wZ3OA9T9ustxTXhOmOC98TNUo87CZVwPSnk6qE3ejLYFaETlZ9Q");
            // Thực hiện yêu cầu GET
            HttpResponseMessage response = await client.GetAsync(apiUrl);

            // Kiểm tra xem yêu cầu có thành công không
            if (response.IsSuccessStatusCode)
            {
                // Đọc nội dung kết quả
                string result = await response.Content.ReadAsStringAsync();

                // Chuyển đổi JSON thành đối tượng ApiResponse
                GetResoureResponse apiResponse = JsonConvert.DeserializeObject<GetResoureResponse>(result);



                // Lấy danh sách ResourceOwners
                List<ResourceOwner> resourceOwners = apiResponse.resource_owners;

                foreach (var item in resourceOwners)
                {
                    GetBucketsAsync(item.id);
                }

                return resourceOwners;
            }
            else
            {
                // Xử lý khi yêu cầu không thành công
                Console.WriteLine($"Lỗi: {response.StatusCode}");
                return null;
            }
        }

        private async Task<List<Bucket>> GetBucketsAsync(string resouce_owner_id)
        {
            string apiUrl = "https://api.pre.earthbrain-pf.com/v2/file_storage/buckets?resource_owner_id=" + resouce_owner_id + "";

            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6ImF0K2p3dCIsImtpZCI6Ijl2d1VZZUhYZ0hIYlctUG9xVVhuaE1yYjZZWVhRX1N0VzhXR0RwQlphLWcifQ.eyJhdWQiOlsiaHR0cHM6Ly9hcGktcHJlLmxhbmRsb2cuaW5mbyJdLCJzdWIiOiI2NDg3ZjliNS1kZmZhLTQxYjgtOWM5Yy01NzQzNDI2NTE1OTUiLCJncmFudF90eXBlIjoiYXV0aG9yaXphdGlvbl9jb2RlIiwiYXpwIjoiOTcxNTFlZmYtNzRjMS00YTU3LTlkOTAtNTViNmNiMmUzMzg1Iiwic2NvcGUiOiJ1c2VyLnByb2ZpbGUgZmlsZV9zdG9yYWdlLndyaXRlIGNvcnBvcmF0aW9uLmdyb3VwLnJlYWQgY29ycG9yYXRpb24ucmVhZCBvcGVuaWQgZmlsZV9zdG9yYWdlLnVwbG9hZCBsaWNlbnNlLnJlYWQgcmVzb3VyY2Vfb3duZXIucmVhZCBmaWxlX3N0b3JhZ2UuZG93bmxvYWQgc2l0ZS5tZW1iZXIucmVhZCBmaWxlX3N0b3JhZ2UucmVhZCBkZXZpY2Uud3JpdGUgZ3JvdXAucmVhZCBjb2xsZWN0aW9uLmRhdGEud3JpdGUgY29sbGVjdGlvbi5kYXRhLnJlYWQgY29ycG9yYXRpb24udXNlci5yZWFkIGRldmljZS5yZWFkIHNpdGUubWVtYmVyLndyaXRlIHNpdGUucmVhZCIsImlzcyI6Imh0dHBzOi8vYXV0aC1wcmUubGFuZGxvZy5pbmZvIiwiYXBwbGljYXRpb25fdHlwZXMiOlsid2ViIl0sImV4cCI6MTcwNTM3Njk5OCwiaWF0IjoxNzA1MjkwNTk4LCJjbGllbnRfaWQiOiI5NzE1MWVmZi03NGMxLTRhNTctOWQ5MC01NWI2Y2IyZTMzODUiLCJqdGkiOiI0MHdqc196Q1puV3RYVXZrUmtyWTc4TV9kZVdadmJJaDg0REN5YzNhUzRjIn0.WEn2tZmXHi7QdNs_5itWqF2DDsbQHj6GoWVnP3OVc6iC4gt8v6akMbkAImCHycF8Vhr6JJX-kNEnrssJY2OTAssQE_fKeo3BYhLf_HC4cy8dbxpZNcjn2rX6EKCpeK9G311Pj4sHVONVv6Hk1QB4UC80B6kXtk4nC5zziZyMuXm1PFJsgXZwI1u8d_M93UveaWQpk2TnVQ6PhDn0zDFZWGCV3iAQ_0P38U9wq1d4jrKVTsFmOb9GJliDt-etEjWm2qBAdXlxXAIdbC86JrCxTnmMHrjQYCyAjE0wZ3OA9T9ustxTXhOmOC98TNUo87CZVwPSnk6qE3ejLYFaETlZ9Q");
            // Thực hiện yêu cầu GET
            HttpResponseMessage response = await client.GetAsync(apiUrl);

            // Kiểm tra xem yêu cầu có thành công không
            if (response.IsSuccessStatusCode)
            {
                // Đọc nội dung kết quả
                string result = await response.Content.ReadAsStringAsync();

                // Chuyển đổi JSON thành đối tượng ApiResponse
                BucketResponse apiResponse = JsonConvert.DeserializeObject<BucketResponse>(result);

                // Lấy danh sách ResourceOwners
                List<Bucket> resourceOwners = apiResponse.Buckets;

                return resourceOwners;
            }
            else
            {
                // Xử lý khi yêu cầu không thành công
                Console.WriteLine($"Lỗi: {response.StatusCode}");
                return null;
            }
        }
    }
}
