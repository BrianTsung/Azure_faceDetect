using System;
using System.Web;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //create_persongroup();
            //create_person();
            //add_face();
            //train();
            //train_status();
            //detect();
            //Identify();
            //get();
            //delete_person();
            facelist();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        // create persongroup
        static async void create_persongroup()
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "72dfb7d9260c4251a08e12163ef39d16");
            var uri = "https://southcentralus.api.cognitive.microsoft.com/face/v1.0/persongroups/briangroup?" + queryString;

            HttpResponseMessage response;

            // Request body
            byte[] byteData = Encoding.UTF8.GetBytes("{'name':'group1','userData':'brian' ,'recognitionModel':'recognition_03'}");

            string result;
            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PutAsync(uri, content);
                result = await response.Content.ReadAsStringAsync();
            }
            Console.WriteLine("PersonGroup Msg: " + result);
        }

        // create person
        static async void create_person()
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "{{key}}");

            var uri = "https://southcentralus.api.cognitive.microsoft.com/face/v1.0/persongroups/briangroup/persons?" + queryString;

            HttpResponseMessage response;

            // Request body
            string result;
            byte[] byteData = Encoding.UTF8.GetBytes("{\"name\":\"tzuyu\",\"userData\":\"[\'tzuyu\',\'21\',\'female\']\"}");

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);
                result = await response.Content.ReadAsStringAsync();
            }
            Console.WriteLine("create_person Msg: " + result);
        }

        //add face
        static async void add_face()
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "{{key}}");

            // Request parameters
            queryString["detectionModel"] = "detection_01";
            var uri = "https://southcentralus.api.cognitive.microsoft.com/face/v1.0/persongroups/briangroup/persons/3294c82a-b475-41f1-8eda-a3e14558c382/persistedFaces?" + queryString;

            HttpResponseMessage response;

            // Request body
            string result;
            byte[] byteData = Encoding.UTF8.GetBytes("{'url': 'https://pic.pimg.tw/hanzhiyu/1513521505-3334408951_n.jpg'}");

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);
                result = await response.Content.ReadAsStringAsync();
            }
            Console.WriteLine("add_face Msg: " + result);

        }

        //train
        static async void train()
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "{{key}}");

            var uri = "https://southcentralus.api.cognitive.microsoft.com/face/v1.0/persongroups/briangroup/train?" + queryString;

            HttpResponseMessage response;

            // Request body
            string result;
            byte[] byteData = Encoding.UTF8.GetBytes("");

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);
                result = await response.Content.ReadAsStringAsync();
            }
            Console.WriteLine("train Msg: " + result);
        }

        //train status
        static async void train_status()
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "{{key}}");

            var uri = "https://southcentralus.api.cognitive.microsoft.com/face/v1.0/persongroups/briangroup/training?" + queryString;

            string result;
            var response = await client.GetAsync(uri);
            result = await response.Content.ReadAsStringAsync();
            Console.WriteLine("train_status Msg: " + result);
        }

        //detect
        static async void detect()
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "{{key}}");

            // Request parameters
            queryString["returnFaceId"] = "true";
            queryString["returnFaceLandmarks"] = "false";
            queryString["recognitionModel"] = "recognition_03";
            queryString["returnRecognitionModel"] = "false";
            queryString["detectionModel"] = "detection_01";
            queryString["faceIdTimeToLive"] = "86400";
            var uri = "https://southcentralus.api.cognitive.microsoft.com/face/v1.0/detect?" + queryString;

            HttpResponseMessage response;

            // Request body
            string result;
            byte[] byteData = Encoding.UTF8.GetBytes("{'url': 'https://pic.pimg.tw/hanzhiyu/1531128703-2285714373_n.jpg'}");

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);
                result = await response.Content.ReadAsStringAsync();
            }
            char[] charsToTrim = { '[', ']' };
            result = result.Trim(charsToTrim);
            JObject take = JObject.Parse(result);
            string faceid = take["faceId"].ToString();
            Console.WriteLine("detect Msg: " + faceid);

        }

     
        //Identify
        static async void Identify()
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "{{key}}");

            var uri = "https://southcentralus.api.cognitive.microsoft.com/face/v1.0/identify?" + queryString;

            HttpResponseMessage response;

            // Request body
            byte[] byteData = Encoding.UTF8.GetBytes("{'PersonGroupId': 'briangroup','faceIds':['2907c2b7-4080-4882-92d1-24a5dc885527']},'maxNumOfCandidatesReturned':1");
            string result;
            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);
                result = await response.Content.ReadAsStringAsync();
            }
            char[] charsToTrim = {'[', ']'};
            result = result.Trim(charsToTrim);
            result = result.Remove(62, 1);
            result = result.Remove(result.Length - 2,1);
            JObject take = JObject.Parse(result);
            string personid = take["candidates"]["personId"].ToString();
            Console.WriteLine(personid);
            string confidence = take["candidates"]["confidence"].ToString();
            Console.WriteLine(confidence);
            Console.WriteLine("Identify Msg: " + result);

            var uri2 = "https://southcentralus.api.cognitive.microsoft.com/face/v1.0/persongroups/briangroup/persons/"+personid+"?" + queryString;
            string result2;
            var response2 = await client.GetAsync(uri2);
            result2 = await response2.Content.ReadAsStringAsync();
            result2 = result2.Replace("'", @"""");
            result2 = result2.Remove((result2.IndexOf("userData"))+10,1);
            result2 = result2.Remove(result2.Length - 2, 1);
            JObject take2 = JObject.Parse(result2);
            string userdata = take2["userData"][1].ToString();
            Console.WriteLine("get Msg: " + userdata);
            Console.WriteLine(result2);
        }

        //get
        static async void get()
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "{{key}}");

            var uri = "https://southcentralus.api.cognitive.microsoft.com/face/v1.0/persongroups/briangroup/persons/a6ee5ce6-a209-4f79-9d97-1e58dba4f464?" + queryString;
            string result;
            var response = await client.GetAsync(uri);
            result = await response.Content.ReadAsStringAsync();
            Console.WriteLine("get Msg: " + result);
        }

        // delete person
        static async void delete_person()
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "{{key}}");

            var uri = "https://southcentralus.api.cognitive.microsoft.com/face/v1.0/persongroups/briangroup/persons/e312b664-3506-4ccc-8c5d-9cbc7e4767e9?" + queryString;
            string result;
            var response = await client.DeleteAsync(uri);
            result = await response.Content.ReadAsStringAsync();
            Console.WriteLine("get Msg: " + result);
        }

        //get facelist
        static async void facelist()
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "{{key}}");

            // Request parameter
            var uri = "https://southcentralus.api.cognitive.microsoft.com/face/v1.0/persongroups/briangroup/persons?" + queryString;
            string result;
            var response = await client.GetAsync(uri);
            result = await response.Content.ReadAsStringAsync();
            char[] charsToTrim = { '[', ']' };
            result = result.Trim(charsToTrim);    
            result = result.Replace("'", @"""");
            result = result.Remove((result.IndexOf("userData")) + 10, 1);
            result = result.Remove((result.IndexOf("female")) + 8, 1);
            result = result.Remove((result.IndexOf("userData", 180)) + 10, 1);
            result = result.Remove((result.IndexOf("userData", 180)) + 31, 1);
            string result2 = result;
            result2 = result.Remove(0,(result.IndexOf("female")) + 10);
            result = result.Remove((result.IndexOf("female")) + 9);
            JObject take = JObject.Parse(result);
            string persistedFaceIds = take["persistedFaceIds"].ToString();
            JObject take2 = JObject.Parse(result2);
            string persistedFaceIds2 = take2["persistedFaceIds"].ToString();
        }
    }
}
