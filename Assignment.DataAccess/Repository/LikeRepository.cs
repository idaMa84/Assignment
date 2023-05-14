using Assignment.DataAccess.Repository.IRepository;
using Assignment.Models;
using Newtonsoft.Json;
using Assignment.Utility;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using Assignment.Models.AutoMapper;

namespace Assignment.DataAccess.Repository
{
    public class LikeRepository : Repository<Like>, ILikeRepository
    {
        private readonly HttpClient _httpClient;
        public LikeRepository()
        {
            _httpClient = new HttpClient
            {
               // BaseAddress = new Uri("https://graph.facebook.com/v2.8/")
                BaseAddress = new Uri("https://graph.facebook.com/v16.0/")
            };
            _httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void Save(Like obj)
        {
            throw new NotImplementedException();
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

        public List<Like> GetAll()
        {

            List<Like> postList = new List<Like>(); 
           
            FacebookLikeResponse response = GetAllFacebookLikes(_httpClient);

            if (response != null)
            {
                var mapper = AutoMapperConfig.InitializeAutomapper();
                postList = mapper.Map<FacebookLikeResponse, List<Like>>(response);
            }

            return postList;


           

        }



      



        public Like Get(string? id)
        {           
           List<Like> postList = new List<Like>();

            FacebookLikeResponse response = GetFacebookLike(_httpClient,id);

            if (response != null)
            {
                var mapper = AutoMapperConfig.InitializeAutomapper();
                postList = mapper.Map<FacebookLikeResponse, List<Like>>(response);
            }

            return postList.FirstOrDefault(u => u.Id == id);
          
        }

        private static string baseUrl = "https://graph.facebook.com/v2.8/";

   

        public FacebookLikeResponse GetAllFacebookLikes(HttpClient _httpClinet)
        {
            FacebookLikeResponse myDeserializedClass = new FacebookLikeResponse();
            HttpResponseMessage response = _httpClinet.GetAsync(baseUrl + "1391050235075158/likes?fields=about%2Cdescription&access_token=EAAN51NNnFeQBAC573O7NXEf6JZA52vG6SwqsckWWe3MisViKZBZBYtBiVkCbOEyfIfPUW6rsgVVCikRt8HKC99rhZAvOeryen0Db6M4C40ZAwk2LIlpE5DgRZBybgcKScFF77WXPB6ZBVE39OxDka4abC5jq1xiuNm2yKZAfhPnvVITGYILriOW3FDWHabQRZBbNqKd69L9VAkQjuB0XqXLM4ifiZCWpSgeWWcWGcHrA2YbrqYNeWZBaYZC8").GetAwaiter().GetResult(); ;

            if (response.IsSuccessStatusCode)
            {
                var product2 =  response.Content.ReadAsStringAsync().GetAwaiter().GetResult(); ;
                myDeserializedClass = JsonConvert.DeserializeObject<FacebookLikeResponse>(product2);
                return myDeserializedClass;
            }

            return myDeserializedClass;
        }

        public FacebookLikeResponse GetFacebookLike(HttpClient _httpClinet, string id)
        {
            FacebookLikeResponse myDeserializedClass = new FacebookLikeResponse();
            HttpResponseMessage response = _httpClinet.GetAsync($"{baseUrl}1391050235075158/likes/{id}?fields=about%2Cdescription&access_token=EAAN51NNnFeQBAC573O7NXEf6JZA52vG6SwqsckWWe3MisViKZBZBYtBiVkCbOEyfIfPUW6rsgVVCikRt8HKC99rhZAvOeryen0Db6M4C40ZAwk2LIlpE5DgRZBybgcKScFF77WXPB6ZBVE39OxDka4abC5jq1xiuNm2yKZAfhPnvVITGYILriOW3FDWHabQRZBbNqKd69L9VAkQjuB0XqXLM4ifiZCWpSgeWWcWGcHrA2YbrqYNeWZBaYZC8").GetAwaiter().GetResult(); ;

            if (response.IsSuccessStatusCode)
            {
                var product2 = response.Content.ReadAsStringAsync().GetAwaiter().GetResult(); ;
                myDeserializedClass = JsonConvert.DeserializeObject<FacebookLikeResponse>(product2);
                return myDeserializedClass;
            }

            return myDeserializedClass;
        }


    }
}
