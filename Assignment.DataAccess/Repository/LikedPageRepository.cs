using Assignment.DataAccess.Repository.IRepository;
using Assignment.Models;
using Newtonsoft.Json;
using Assignment.Utility;
using System.Net.Http.Headers;
using Assignment.Models.AutoMapper;
using System.Web;

namespace Assignment.DataAccess.Repository
{
    public class LikedPageRepository : Repository<LikedPage>, ILikedPageRepository
    {
        private readonly HttpClient _httpClient;
        public LikedPageRepository()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://graph.facebook.com/v16.0/")
            };
            _httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        ///<summary>
        ///Updates Liked Page data and store it in json file on local disk path defined in Assignment.Utility SD class, property FolderPath
        ///</summary>
        public void Update(LikedPage likedPage)
        {
            try
            {
                string jsonFile = JsonConvert.SerializeObject(likedPage);
                string docPath = SD.FolderPath;
                string docName = $"LikeId {likedPage.Id.ToString()}, TimeStamp {DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-fff")}.json ";

                if (!Directory.Exists(docPath))
                {
                    Directory.CreateDirectory(docPath);
                }

                using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, docName)))
                {
                    outputFile.WriteAsync(jsonFile);
                }
            }
            catch (DriveNotFoundException)
            {
                Console.WriteLine("The drive specified in 'path' is invalid.");
                throw new Exception();
            }
            catch (PathTooLongException)
            {
                string message = "\"'path' exceeds the maximum supported path length.\"";
                Console.WriteLine(message);
                throw new Exception(message);
            }
            catch (UnauthorizedAccessException)
            {                
                string message = "You do not have permission to create this file.";
                Console.WriteLine(message);
                throw new Exception(message);
            }           
            catch (IOException ex) when ((ex.HResult & 0x0000FFFF) == 80)
            {
                string message = "The file already exists.";
                Console.WriteLine(message);
                throw new Exception(message);                
            }
            catch (IOException ex)
            {
                string message = $"An exception occurred:\nError code: {ex.HResult & 0x0000FFFF}\nMessage: {ex.Message}";
                Console.WriteLine(message);
                throw new Exception(message);               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        ///<summary>
        ///Retrieves Liked Pages
        ///</summary>
        public List<LikedPage> GetAll(string accessToken)
        {
            try
            {
                List<LikedPage> likedPages = new List<LikedPage>();

                if (!String.IsNullOrEmpty(accessToken))
                {
                    FacebookLikedPagesResponse response = GetAllFacebookLikedPages(_httpClient, accessToken);
                    if (response.Data != null)
                    {
                        var mapper = AutoMapperConfig.InitializeAutomapper();
                        likedPages = mapper.Map<FacebookLikedPagesResponse, List<LikedPage>>(response);
                    }
                }
                return likedPages;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }


        ///<summary>
        ///Retrieves Liked Page by id
        ///</summary>
        public LikedPage Get(string? id, string accessToken)
        {
            try
            {
                List<LikedPage> likedPages = new List<LikedPage>();
                FacebookLikedPagesResponse response = GetFacebookLikedPage(id, accessToken);

                if (response.Data != null)
                {
                    var mapper = AutoMapperConfig.InitializeAutomapper();
                    likedPages = mapper.Map<FacebookLikedPagesResponse, List<LikedPage>>(response);
                }

                return likedPages.FirstOrDefault(u => u.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        ///<summary>
        ///Retrieves all Liked Pages from Facebook
        ///</summary>
        public FacebookLikedPagesResponse GetAllFacebookLikedPages(HttpClient _httpClinet, string accessToken)
        {
            try
            {

                FacebookLikedPagesResponse myDeserializedClass = new FacebookLikedPagesResponse();
                HttpResponseMessage response = _httpClinet.GetAsync($"{_httpClient.BaseAddress}me/likes?fields=id%2Cname%2Cabout%2Cdescription&limit=10&access_token={accessToken}").GetAwaiter().GetResult(); ;

                if (response.IsSuccessStatusCode)
                {
                    var res = response.Content.ReadAsStringAsync().GetAwaiter().GetResult(); ;
                    myDeserializedClass = JsonConvert.DeserializeObject<FacebookLikedPagesResponse>(res);
                    return myDeserializedClass;
                }

                return myDeserializedClass;
            }           
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        ///<summary>
        ///Retrieves Liked Page from Facebook by id
        ///</summary>
        public FacebookLikedPagesResponse GetFacebookLikedPage(string id, string accessToken)
        {
            try
            {
                FacebookLikedPagesResponse myDeserializedClass = new FacebookLikedPagesResponse();
                HttpResponseMessage response = _httpClient.GetAsync($"{_httpClient.BaseAddress}me/likes/{id}?fields=id%2Cname%2Cabout%2Cdescription&access_token={accessToken}").GetAwaiter().GetResult(); ;

                if (response.IsSuccessStatusCode)
                {
                    var res = response.Content.ReadAsStringAsync().GetAwaiter().GetResult(); ;
                    myDeserializedClass = JsonConvert.DeserializeObject<FacebookLikedPagesResponse>(res);
                    return myDeserializedClass;
                }
                return myDeserializedClass;
            }          
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        ///<summary>
        ///Retrieves Facebook access token
        ///</summary>
        public string GetFacebookAccessToken(string? urlWithFacebookCode)
        {
            try
            {
                String? access_token = String.Empty;
                if (!String.IsNullOrEmpty(urlWithFacebookCode))
                {
                    Uri myUri = new Uri(urlWithFacebookCode);
                    String? code = HttpUtility.ParseQueryString(myUri.Query).Get("code");

                    if (!String.IsNullOrEmpty(code))
                    {
                        HttpResponseMessage response = _httpClient.GetAsync($"{_httpClient.BaseAddress}oauth/access_token?redirect_uri={SD.FacebookRedirectPage}& client_id={SD.FacebookClientId}&client_secret={SD.FacebookClientSecret}={code}").GetAwaiter().GetResult(); ;
                        if (response.IsSuccessStatusCode)
                        {
                            var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult().ToString();
                            dynamic responseString = JsonConvert.DeserializeObject(result);
                            access_token = responseString.access_token;
                        }
                        else
                            throw new Exception("Can not obtain access token from Facebook");
                    }
                }
                return access_token;
            }           
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

        }


    }
}
