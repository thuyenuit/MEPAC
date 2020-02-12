using Newtonsoft.Json;
using SMS.Shared.Shares;
using SMS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Reflection;

namespace SMS.API.Infrastructure.Core
{
    public class HttpCore
    {
        private static string API_URL
        {
            get {
                return "http://localhost:52208/";
            }
        }

        private string GetToken
        {
            get {
                return HttpContext.Current.Session["LOGIN_AUTHOR"] != null ? (string)HttpContext.Current.Session["LOGIN_AUTHOR"] : null;
            }
        }

        public static T ConvertToObject<T>(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return default(T);
            }

            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception)
            {
                return default(T);
            }
        } 

        public static async Task<string> Login(string username, string passowrd)
        {
            try
            {               
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
                //string url = API_URL + "token";

                /*LoginInfo loginInfo = new LoginInfo();
                loginInfo.Password = passowrd;
                loginInfo.UserName = username;
                string strJson = JsonConvert.SerializeObject(loginInfo);
                var content = new StringContent(strJson, Encoding.UTF8, "application/json");*/

                var postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("username", username));
                postData.Add(new KeyValuePair<string, string>("password", passowrd));
                postData.Add(new KeyValuePair<string, string>("grant_type", "password"));


                client.BaseAddress = new Uri(API_URL);
                var content = new FormUrlEncodedContent(postData);
                var response = await client.PostAsync("token", content);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
              
                return null;
                
            }
            catch (HttpRequestException hre)
            {
                return null;
            }

        }

        public static async Task<T> Login<T>(string username, string password)
        {
            T data = default(T);
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded;charset=utf-8");

                var values = new Dictionary<string, string>();
                values.Add("UserName", username);
                values.Add("Password", password);
                var content = new FormUrlEncodedContent(values);

                HttpResponseMessage response = await client.PostAsync(SystemParam.BASE_API + SystemParam.LOGIN_API, content);
                if (response.IsSuccessStatusCode)
                {
                    data = await response.Content.ReadAsAsync<T>();
                };
            }
            return data;
        }

        public static async Task<T> PostAsync<T>(string url, object request, string token = null)
        {
            T data = default(T);
            using (HttpClient client = new HttpClient())
            {
                if(token == null)
                    token = HttpContext.Current.Session["LOGIN_AUTHOR"] != null ? (string)HttpContext.Current.Session["LOGIN_AUTHOR"] : null;
                //client.BaseAddress = new Uri(url);
                BindGlobalInfo(request);
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    data = await response.Content.ReadAsAsync<T>();
                    
                };
            }
            return data;
        }

        public static BaseResponse<A> PostAsync<A, T>(string url, string uri, T request, string token = null)
        {
            BaseResponse<A> response = new BaseResponse<A>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    BindGlobalInfo(request);

                    if (token == null)
                        token = HttpContext.Current.Session["LOGIN_AUTHOR"] != null ? (string)HttpContext.Current.Session["LOGIN_AUTHOR"] : null;
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                    HttpResponseMessage httpResponse = client.PostAsync(uri, content).Result;

                    if (httpResponse.IsSuccessStatusCode)
                    {
                        response = httpResponse.Content.ReadAsAsync<BaseResponse<A>>().Result;
                        return response;
                    };
                }
            }
            catch (Exception ex)
            {
                return response;
            }
            
            return response;
        }

        private static void BindGlobalInfo<T>(T request) {

            if (request is BaseRequest)
            {
                Global _global = new Global();
                BaseRequest _baseRequest = new BaseRequest()
                {
                    UserID = _global.UserID,
                    UserFullName =_global.UserFullName,
                    UserAccount = _global.UserAccount
                };

                BindTo(request, _baseRequest);
            }

            //Type objectType = request.GetType();
            //IList<PropertyInfo> props = new List<PropertyInfo>(objectType.GetProperties());
            //foreach (PropertyInfo prop in props)
            //{
            //    if (prop.Name.Equals("Login_User_ID"))
            //    {
            //        request.GetType().GetRuntimeProperty(prop.Name)?.SetValue(request, (HttpContext.Current.Session["USER_ID"] != null ? (string)HttpContext.Current.Session["USER_ID"] : null));
            //    }
            //    else if (prop.Name.Equals("Login_User_Account"))
            //    {
            //        request.GetType().GetRuntimeProperty(prop.Name)?.SetValue(request, (HttpContext.Current.Session["USER_ACCOUNT"] != null ? (string)HttpContext.Current.Session["USER_ACCOUNT"] : null));
            //    }
            //    else if (prop.Name.Equals("Login_Full_Name"))
            //    {
            //        request.GetType().GetRuntimeProperty(prop.Name)?.SetValue(request, (HttpContext.Current.Session["USER_FULLNAME"] != null ? (string)HttpContext.Current.Session["USER_FULLNAME"] : null));
            //    }
            //}
        }

        private static void BindTo(object source, object dest, bool ExceptVirtual = false, bool NullValue = true)
        {
            Type f = source.GetType();
            PropertyInfo[] fproperties = f.GetProperties();

            Type m = dest.GetType();
            PropertyInfo[] mproperties = m.GetProperties();
            List<string> tmp = new List<string>();
            foreach (PropertyInfo p in mproperties)
            {
                tmp.Add(p.Name);
            }

            foreach (PropertyInfo p in fproperties)
            {
                if (!NullValue)
                {
                    if (p.GetValue(source, null) == null)
                    {
                        continue;
                    }
                }
                if (ExceptVirtual)
                {
                    if (p.GetGetMethod().IsVirtual)
                    {
                        continue;
                    }
                }
                if (tmp.Contains(p.Name))
                {

                    //lay ve value tu thang dest
                    object valueDest = m.GetProperty(p.Name).GetValue(dest, null);

                    if (valueDest == null)
                    {
                        object value = p.GetValue(source, null);
                        m.GetProperty(p.Name).SetValue(dest, value, null);
                    }
                }
            }
        }

        public T PostAsync2<T>(string url, object request, string token = null)
        {
            T data = default(T);
            using (HttpClient client = new HttpClient())
            {
                if (token == null)
                    token = HttpContext.Current.Session["LOGIN_AUTHOR"] != null ? (string)HttpContext.Current.Session["LOGIN_AUTHOR"] : null;
                //client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer ", GetToken);
                var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(url, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = Task.Run(async () =>
                    {
                        return await response.Content.ReadAsAsync<T>();
                    }).Result;
                    data = result;
                };
            }
            return data;
        }

        //public static async Task<T> PostAsync2<T>(string url, object request)
        //{
        //    T data = default(T);
        //    using (HttpClient client = new HttpClient())
        //    {
        //        //client.BaseAddress = new Uri(url);
        //        client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer ", GetToken);
        //        var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        //        HttpResponseMessage response = await client.PostAsJsonAsync(url, content);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            data = await response.Content.ReadAsAsync<T>();

        //        };
        //    }
        //    return data;
        //}
    }

    public class LoginInfo
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string grant_type { get { return "password"; } }
    }
}