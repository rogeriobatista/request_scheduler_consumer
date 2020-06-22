using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using request_scheduler_consumer.Domain.MauticForms.Dtos;

namespace request_scheduler_consumer.Generics.Http
{
    public class Client
    {
        private string Url { get; set; }

        private string Body { get; set; }

        private string ContentType { get; set; }

        private readonly HttpClient HttpClient;

        public Client(string url, string contentType, string headers, string body)
        {
            HttpClient = new HttpClient();
            var httpHeaders = JsonConvert.DeserializeObject<List<MauticFormHeaderDto>>(headers);
            foreach (var item in httpHeaders)
            {
                HttpClient.DefaultRequestHeaders.Add(item.Name, item.Value);
            }
            Url = url;
            ContentType = contentType;
            Body = body;
        }

        public void Post()
        {
            HttpClient.PostAsync(Url, new StringContent(Body, Encoding.UTF8, ContentType));
        }

        public void Get()
        {
            HttpClient.GetAsync(Url);
        }
    }
}
