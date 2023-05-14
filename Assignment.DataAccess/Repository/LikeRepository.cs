using Assignment.DataAccess.Repository.IRepository;
using Assignment.Models;
using Newtonsoft.Json;
using Assignment.Utility;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using Assignment.Models.AutoMapper;
using System.Web;

namespace Assignment.DataAccess.Repository
{
    public class LikeRepository : Repository<Like>, ILikeRepository
    {
        private readonly HttpClient _httpClient;        

        public LikeRepository()
        {
            _httpClient = new HttpClient
            {               
                BaseAddress = new Uri("https://graph.facebook.com/v16.0/")
            };
            _httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));            
        }
             
        public void Update(Like entity)
        {
            string jsonFile = JsonConvert.SerializeObject(entity);            
            string docPath = SD.FolderPath;            
            string docName = $"LikeId {entity.Id.ToString()}, TimeStamp {DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-fff")}.json ";            

            if (!Directory.Exists(docPath))
            {
                Directory.CreateDirectory(docPath);
            }
                       
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, docName)))
            {
                outputFile.WriteAsync(jsonFile);
            }
        }

        public List<Like> GetAll(string accessToken)                        
        {          
            List<Like> postList = new List<Like>();     
            
            if (!String.IsNullOrEmpty(accessToken))
            { 
            FacebookLikeResponse response = GetAllFacebookLikes(_httpClient, accessToken);
            if (response.data != null)
            {
                var mapper = AutoMapperConfig.InitializeAutomapper();
                postList = mapper.Map<FacebookLikeResponse, List<Like>>(response);
            }
            }
            return postList;
        }
              
       

        public Like Get(string? id, string accessToken)
        {           
            List<Like> postList = new List<Like>();
            FacebookLikeResponse response = GetFacebookLike(id, accessToken);

            if (response.data != null)
            {
                var mapper = AutoMapperConfig.InitializeAutomapper();
                postList = mapper.Map<FacebookLikeResponse, List<Like>>(response);
            }

            return postList.FirstOrDefault(u => u.Id == id);          
        }

        //private static string baseUrl = "https://graph.facebook.com/v2.8/";

   

        public FacebookLikeResponse GetAllFacebookLikes(HttpClient _httpClinet, string accessToken)
        {
            
            FacebookLikeResponse myDeserializedClass = new FacebookLikeResponse();
            HttpResponseMessage response = _httpClinet.GetAsync($"{_httpClient.BaseAddress}me/likes?fields=about%2Cdescription&access_token={accessToken}").GetAwaiter().GetResult(); ;

            if (response.IsSuccessStatusCode)
            {
                var product2 =  response.Content.ReadAsStringAsync().GetAwaiter().GetResult(); ;
                myDeserializedClass = JsonConvert.DeserializeObject<FacebookLikeResponse>(product2);
                return myDeserializedClass;
            }

            return myDeserializedClass;
        }

        public FacebookLikeResponse GetFacebookLike(string id, string accessToken)
        {
            FacebookLikeResponse myDeserializedClass = new FacebookLikeResponse();
            HttpResponseMessage response = _httpClient.GetAsync($"{_httpClient.BaseAddress}me/likes/{id}?fields=about%2Cdescription&access_token={accessToken}").GetAwaiter().GetResult(); ;

            if (response.IsSuccessStatusCode)
            {
                var product2 = response.Content.ReadAsStringAsync().GetAwaiter().GetResult(); ;
                myDeserializedClass = JsonConvert.DeserializeObject<FacebookLikeResponse>(product2);
                return myDeserializedClass;
            }
            return myDeserializedClass;
        }

        public string GetAccessToken()
        {
            String? access_token = String.Empty;
            if (!String.IsNullOrEmpty(SD.UrllWithFacebookCode))
            {
                Uri myUri = new Uri(SD.UrllWithFacebookCode);
                String? code = HttpUtility.ParseQueryString(myUri.Query).Get("code");


                if (!String.IsNullOrEmpty(code))
                {
                    HttpResponseMessage response = _httpClient.GetAsync($"{_httpClient.BaseAddress}oauth/access_token?redirect_uri=https%3A%2F%2Flocalhost%3A7061%2FHome%2F&client_id=978379916908004&client_secret=6b0f9a4e82f8785a97583ea46eb45d7e&code={code}").GetAwaiter().GetResult(); ;
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult().ToString();
                        dynamic responseString = JsonConvert.DeserializeObject(result);
                        access_token = responseString.access_token;
                    }
                }
            }
            return access_token;
        }


    }
}
