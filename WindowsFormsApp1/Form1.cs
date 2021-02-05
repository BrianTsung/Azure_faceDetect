using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var client = new HttpClient();
            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "72dfb7d9260c4251a08e12163ef39d16");

            HttpResponseMessage response;

            //detect
            var queryString_detect = HttpUtility.ParseQueryString(string.Empty);
            queryString_detect["returnFaceId"] = "true";
            queryString_detect["returnFaceLandmarks"] = "false";
            queryString_detect["recognitionModel"] = "recognition_03";
            queryString_detect["returnRecognitionModel"] = "false";
            queryString_detect["detectionModel"] = "detection_01";
            queryString_detect["faceIdTimeToLive"] = "86400";
            var uri_detect = "https://southcentralus.api.cognitive.microsoft.com/face/v1.0/detect?" + queryString_detect;

            string result_detect;
            string imgurl = url.Text;
            pictureBox1.Load(imgurl);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            byte[] byteData_detect = Encoding.UTF8.GetBytes("{'url': '"+ imgurl + "'}");

            using (var content = new ByteArrayContent(byteData_detect))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri_detect, content);
                result_detect = await response.Content.ReadAsStringAsync();
            }
            char[] charsToTrim = { '[', ']' };
            result_detect = result_detect.Trim(charsToTrim);
            JObject take_detect = JObject.Parse(result_detect);
            string faceid = take_detect["faceId"].ToString();

            //Identify
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            var uri = "https://southcentralus.api.cognitive.microsoft.com/face/v1.0/identify?" + queryString;
            byte[] byteData = Encoding.UTF8.GetBytes("{'PersonGroupId': 'briangroup','faceIds':["+ "'"+faceid + "'" + "]},'maxNumOfCandidatesReturned':1");
            string result;
            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);
                result = await response.Content.ReadAsStringAsync();
            }
       
            result = result.Trim(charsToTrim);
            result = result.Remove(62, 1);
            result = result.Remove(result.Length - 2, 1);
            JObject take = JObject.Parse(result);
            string personid = take["candidates"]["personId"].ToString();
            string confidence = take["candidates"]["confidence"].ToString();
            label6.Text = confidence;

            //get
            var uri2 = "https://southcentralus.api.cognitive.microsoft.com/face/v1.0/persongroups/briangroup/persons/" + personid + "?" + queryString;
            string result2;
            var response2 = await client.GetAsync(uri2);
            result2 = await response2.Content.ReadAsStringAsync();
            result2 = result2.Replace("'", @"""");
            result2 = result2.Remove((result2.IndexOf("userData")) + 10, 1);
            result2 = result2.Remove(result2.Length - 2, 1);
            JObject take2 = JObject.Parse(result2);
            string userdata = take2["userData"][1].ToString();
            //Console.WriteLine("get Msg: " + userdata);
            label1.Text = take2["userData"][0].ToString();
            label2.Text = take2["userData"][1].ToString();
            label3.Text = take2["userData"][2].ToString();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "72dfb7d9260c4251a08e12163ef39d16");

            var uri = "https://southcentralus.api.cognitive.microsoft.com/face/v1.0/persongroups/briangroup/train?" + queryString;

            HttpResponseMessage response;

            // Request body
            byte[] byteData = Encoding.UTF8.GetBytes("");

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);
            }

            // train status
            var uri_status = "https://southcentralus.api.cognitive.microsoft.com/face/v1.0/persongroups/briangroup/training?" + queryString;

            string result;
            var response_status = await client.GetAsync(uri_status);
            result = await response_status.Content.ReadAsStringAsync();
            JObject take = JObject.Parse(result);
            string status = take["status"].ToString();
            string createdDateTime = take["createdDateTime"].ToString();
            label8.Text = status;
            label9.Text = createdDateTime;
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "72dfb7d9260c4251a08e12163ef39d16");

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
            result2 = result.Remove(0, (result.IndexOf("female")) + 10);
            result = result.Remove((result.IndexOf("female")) + 9);
            JObject take = JObject.Parse(result);
            string persistedFaceIds = take["persistedFaceIds"].ToString();
            JObject take2 = JObject.Parse(result2);
            string persistedFaceIds2 = take2["persistedFaceIds"].ToString();
            richTextBox1.Multiline = true;
            richTextBox1.AppendText(persistedFaceIds);
            richTextBox1.AppendText(persistedFaceIds2);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }

   
}
