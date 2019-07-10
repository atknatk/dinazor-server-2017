using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Dto.Crm;
using Newtonsoft.Json;

namespace Dinazor.Core.RestClient
{
    //TODO make it generic
    public class DinazorRestClient
    {
        private string BaseUrl = "http://localhost:5001";

        public async Task<DinazorResult<List<OrganizationLicenceDto>>> Post(ClientDto clientDto)
        {
            /*clientDto.BiosVersion = "1.1.1";
            clientDto.ClientIdentifier = "1.1.11uinan123";
            clientDto.HddSerialNo = "1";
            clientDto.Password = "123";
            clientDto.Username = "uinan";*/
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // var clientDtoJson = JsonConvert.SerializeObject(clientDto);
                //var content = new StringContent(clientDtoJson, Encoding.UTF8, "application/json");
                HttpContent content = new FormUrlEncodedContent(GetContent(clientDto));
                HttpResponseMessage responseMessage = await client.PostAsync("api/Licence", content).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
                var res = await responseMessage.Content.ReadAsStringAsync();

                DinazorResult<List<OrganizationLicenceDto>> result = new DinazorResult<List<OrganizationLicenceDto>>();
                result = JsonConvert.DeserializeObject<DinazorResult<List<OrganizationLicenceDto>>>(res);
                // result.Data= JsonConvert.DeserializeObject<List<OrganizationLicenceDto>>(res);
                //result.Success();
                return result;

            }

        }

        private List<KeyValuePair<string, string>> GetContent(ClientDto dto)
        {
            var postData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("BiosVersion", dto.BiosVersion),
                new KeyValuePair<string, string>("ClientIdentifier", dto.ClientIdentifier),
                new KeyValuePair<string, string>("HddSerialNo", dto.HddSerialNo),
                new KeyValuePair<string, string>("Password", dto.Password),
                new KeyValuePair<string, string>("Username", dto.Username)
            };
            return postData;
        }



    }
}
