using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using mystocks.Models;

namespace mystocks.Services
{
    class API
    {
        private static String apiKey = "927b9ce7f79dbd46e93658885f59e868";
        private static String baseUrl = "https://marketdata.websol.barchart.com/getHistory.json?";

        public static void getHistory(string code, String date, AsyncCallback callback)
        {
            var uri = string.Format(baseUrl+"apikey={0}&symbol={1}&type=daily&startDate={2}", apiKey, code,date);
            var state = new State();
            CallWebAsync(uri, state, callback);
        }

        private static void CallWebAsync(string uri, State state, AsyncCallback callback)
        {
            var request = HttpWebRequest.Create(uri);
            request.Method = "GET";
            state.Request = request;
            request.BeginGetResponse(callback, state);
        }

        public static void CallHandler(IAsyncResult asyncResult)
        {
            var state = (State)asyncResult.AsyncState;
            var request = state.Request;

            try
            {
                using (HttpWebResponse response = request.EndGetResponse(asyncResult) as HttpWebResponse)
                {
                    state.Status = response.StatusCode;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            state.Response = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                state.Response = e.Message;
            }
        }
    }
}
