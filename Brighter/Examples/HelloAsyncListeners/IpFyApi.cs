﻿using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HelloAsyncListeners
{
    internal struct IpFyApiResult
    {
        public IpFyApiResult(bool success, string message)
            : this()
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; private set; }
        public string Message { get; private set; }
    }

    internal class IpFyApi
    {
        private readonly Uri _endpoint;

        public IpFyApi(Uri endpoint)
        {
            _endpoint = endpoint;
        }

        public async Task<IpFyApiResult> GetAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _endpoint;
                client.DefaultRequestHeaders.Clear();

                var response = await client.GetAsync("");
                string result;
                if (response.IsSuccessStatusCode)
                    result = await response.Content.ReadAsStringAsync();
                else
                    result = "API returned HTTP status " + response.StatusCode;
                return new IpFyApiResult(response.IsSuccessStatusCode, result);
            }
        }
    }
}
