using CHBase_FitBit_TestProject.Garmin.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CHBase_FitBit_TestProject.Garmin.Pages
{
    public partial class Activities : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string ActivitiesUrl = "https://healthapi.garmin.com/wellness-api/rest/activities";

            string strNounce = AuthHelper.GenerateNounce();
            string OAuthToken = HttpContext.Current.Session["OAuthToken"].ToString();

            string startTime = AuthHelper.GenerateNounce(DateTime.Now.AddDays(-1));
            string EndTime = strNounce;


            string strParamters = "oauth_consumer_key=37d14781-5529-4a3a-9c55-aad6b835913c&oauth_nonce=" + strNounce + "&oauth_signature_method=HMAC-SHA1&oauth_timestamp=" + strNounce + "&oauth_token=" + OAuthToken + "&oauth_version=1.0&uploadEndTimeInSeconds=" + EndTime + "&uploadStartTimeInSeconds=" + startTime;
            string strRequestUrl = "GET&" + AuthHelper.UpperCaseUrlEncode((ActivitiesUrl).ToLower()) + "&" + AuthHelper.UpperCaseUrlEncode(strParamters);

            string HashKey = "fq07vfP6JodQr0EmgnPYUxKPkNNv8pKoib6" + "&" + HttpContext.Current.Session["OAuthToken_Secret"];

            var signature = AuthHelper.GenerateOAuthSignature(HashKey, strRequestUrl);

            ActivitiesUrl = ActivitiesUrl + "?uploadStartTimeInSeconds={0}&uploadEndTimeInSeconds={1}";

            ActivitiesUrl = string.Format(ActivitiesUrl, startTime, EndTime);
            string AuthHeader = "oauth_version=\"1.0\", oauth_consumer_key=\"37d14781-5529-4a3a-9c55-aad6b835913c\", oauth_timestamp=\"" + strNounce + "\", oauth_nonce=\"" + strNounce + "\", oauth_signature_method=\"HMAC-SHA1\", oauth_signature=\"" + signature + "\", oauth_token=\"" + OAuthToken + "\"";
            try
            {
                HttpClient hc = new HttpClient();
                hc.DefaultRequestHeaders.Add("Authorization", "OAuth " + AuthHeader);

                HttpResponseMessage response = hc.GetAsync(ActivitiesUrl).Result;

                string str = response.Content.ReadAsStringAsync().Result;



            }
            catch { }
        }

        internal string GetBackLogResponse()
        {
            string ActivitiesUrl = "https://healthapi.garmin.com/wellness-api/rest/backfill/activities";

            string strNounce = AuthHelper.GenerateNounce();
            string OAuthToken = HttpContext.Current.Session["OAuthToken"].ToString();

            string startTime = AuthHelper.GenerateNounce(DateTime.Now.AddDays(-90));
            string EndTime = strNounce;


            string strParamters = "oauth_consumer_key=37d14781-5529-4a3a-9c55-aad6b835913c&oauth_nonce=" + strNounce + "&oauth_signature_method=HMAC-SHA1&oauth_timestamp=" + strNounce + "&oauth_token=" + OAuthToken + "&oauth_version=1.0&summaryEndTimeInSeconds=" + EndTime + "&summaryStartTimeInSeconds=" + startTime;
            string strRequestUrl = "GET&" + AuthHelper.UpperCaseUrlEncode((ActivitiesUrl).ToLower()) + "&" + AuthHelper.UpperCaseUrlEncode(strParamters);

            string HashKey = "fq07vfP6JodQr0EmgnPYUxKPkNNv8pKoib6" + "&" + HttpContext.Current.Session["OAuthToken_Secret"];

            var signature = AuthHelper.GenerateOAuthSignature(HashKey, strRequestUrl);

            ActivitiesUrl = ActivitiesUrl + "?summaryStartTimeInSeconds={0}&summaryEndTimeInSeconds={1}";

            ActivitiesUrl = string.Format(ActivitiesUrl, startTime, EndTime);
            string AuthHeader = "oauth_version=\"1.0\", oauth_consumer_key=\"37d14781-5529-4a3a-9c55-aad6b835913c\", oauth_timestamp=\"" + strNounce + "\", oauth_nonce=\"" + strNounce + "\", oauth_signature_method=\"HMAC-SHA1\", oauth_signature=\"" + signature + "\", oauth_token=\"" + OAuthToken + "\"";
            try
            {
                HttpClient hc = new HttpClient();
                hc.DefaultRequestHeaders.Add("Authorization", "OAuth " + AuthHeader);

                HttpResponseMessage response = hc.GetAsync(ActivitiesUrl).Result;

                string str = response.Content.ReadAsStringAsync().Result;
                return str;


            }
            catch { return string.Empty; }

        }

        protected void btnFetchBackLogActivities_Click(object sender, EventArgs e)
        {
            GetBackLogResponse();
        }

        protected void btnGetUserId_Click(object sender, EventArgs e)
        {
            string UserIDUrl = "https://healthapi.garmin.com/wellness-api/rest/user/id";

            string strNounce = AuthHelper.GenerateNounce();
            string OAuthToken = HttpContext.Current.Session["OAuthToken"].ToString();

            string startTime = AuthHelper.GenerateNounce(DateTime.Now.AddDays(-90));
            string EndTime = strNounce;


            string strParamters = "oauth_consumer_key=37d14781-5529-4a3a-9c55-aad6b835913c&oauth_nonce=" + strNounce + "&oauth_signature_method=HMAC-SHA1&oauth_timestamp=" + strNounce + "&oauth_token=" + OAuthToken + "&oauth_version=1.0"; //&summaryEndTimeInSeconds=" + EndTime + "&summaryStartTimeInSeconds=" + startTime
            string strRequestUrl = "GET&" + AuthHelper.UpperCaseUrlEncode((UserIDUrl).ToLower()) + "&" + AuthHelper.UpperCaseUrlEncode(strParamters);

            string HashKey = "fq07vfP6JodQr0EmgnPYUxKPkNNv8pKoib6" + "&" + HttpContext.Current.Session["OAuthToken_Secret"];

            var signature = AuthHelper.GenerateOAuthSignature(HashKey, strRequestUrl);

            // ActivitiesUrl = ActivitiesUrl + "?summaryStartTimeInSeconds={0}&summaryEndTimeInSeconds={1}";

            //ActivitiesUrl = string.Format(ActivitiesUrl, startTime, EndTime);
            string AuthHeader = "oauth_version=\"1.0\", oauth_consumer_key=\"37d14781-5529-4a3a-9c55-aad6b835913c\", oauth_timestamp=\"" + strNounce + "\", oauth_nonce=\"" + strNounce + "\", oauth_signature_method=\"HMAC-SHA1\", oauth_signature=\"" + signature + "\", oauth_token=\"" + OAuthToken + "\"";
            try
            {
                HttpClient hc = new HttpClient();
                hc.DefaultRequestHeaders.Add("Authorization", "OAuth " + AuthHeader);

                HttpResponseMessage response = hc.GetAsync(UserIDUrl).Result;

                string str = response.Content.ReadAsStringAsync().Result;



            }
            catch { }
        }
    }
}