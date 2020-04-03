using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace APITester
{
    public class indexModel : PageModel
    {
        public string Message { get; set; }

        [BindProperty]
        public string sUrl { get; set; }

        [BindProperty]
        public string sController { get; set; }

        [BindProperty]
        public int lValue { get; set; }

        public async Task OnPostAsync()
        {
            HttpResponseMessage resp;

            //clean up if I botch a copy/paste
            sUrl = sUrl.Trim();

            //clean up if I botch a copy/paste
            if (sUrl[sUrl.Length - 1] != '/')
                sUrl += "/";

            using (HttpClient client = new HttpClient())
            {
                using (HttpRequestMessage req = new HttpRequestMessage())
                {
                    // the api end point
                    req.RequestUri = new Uri(sUrl + sController);
                    // we are getting data versus posting data
                    req.Method = HttpMethod.Get;
                    //header API key
                    req.Headers.Add("api_key", "1234");
                    //sending it all off
                    resp = await client.SendAsync(req);
                }
            }

            //display the response
            if (resp != null && resp.IsSuccessStatusCode)
            {
                var foo = resp.Content.ReadAsStringAsync();

                Message = $"Success! {foo.Result}";
            }
            else
            {
                Message = $"fail: {resp.StatusCode}";
            }
        }


        public async Task OnPostSendValueAsync()
        {
            HttpResponseMessage resp;

            //clean up if I botch a copy/paste
            sUrl = sUrl.Trim();

            //clean up if I botch a copy/paste
            if (sUrl[sUrl.Length - 1] != '/')
                sUrl += "/";

            //Convert the value to some JSON formatting.
            var jsonDat = new
            {
                id = $"{lValue}"
            };

            //serialize the data to a JSON string.
            string json = JsonConvert.SerializeObject(jsonDat);

            using (HttpClient http = new HttpClient())
            {
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, sUrl + sController))
                {
                    //ship the JSON over to the API.
                    request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                    resp = await http.SendAsync(request);
                };
            }


            if (resp.IsSuccessStatusCode)
            {
                Message = $"Success! {resp.StatusCode}";
            }
            else
            {
                Message = $"fail: {resp.StatusCode}";
            }









            //using (HttpClient client = new HttpClient())
            //{
            //    string aaa = "";

            //    var jsonDat = new
            //    {
            //        id = @"foo"
            //    };

            //    aaa = JsonConvert.SerializeObject(jsonDat);

            //    client.PostAsJsonAsync("", "");

            //    HttpContent foo = new StringContent(aaa, Encoding.UTF8, "application/json");
            //    resp = await client.PostAsync(new Uri(sUrl + sController), foo);
            //}

            //display the response
            //if (resp != null && resp.IsSuccessStatusCode)
            //{
            //    var foo = resp.Content.ReadAsStringAsync();

            //    Message = $"Success! {foo.Result}";
            //}
            //else
            //{
            //    Message = $"fail: {resp.StatusCode}";
            //}
            //   return Redirect("Index");
        }

    }
}