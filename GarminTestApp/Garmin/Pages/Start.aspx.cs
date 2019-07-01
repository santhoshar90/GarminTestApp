using CHBase_FitBit_TestProject.Garmin.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CHBase_FitBit_TestProject.Garmin.Pages
{
    public partial class Start : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        string strOAuthConsumerKey = ConfigurationManager.AppSettings["oauth_consumer_key"];
        string strTimeStamp = AuthHelper.GenerateNounce();
        protected void aStart_ServerClick(object sender, EventArgs e)
        {
            //    var signature = GenerateOAuthSignature("GET&" + UpperCaseUrlEncode((BASE_URL + PATH_REQUEST_TOKEN).ToLower()) +
            //        "&" + UpperCaseUrlEncode(""));


            string strOAuthToken = string.Empty;
            //string strParamters = "oauth_consumer_key=37d14781-5529-4a3a-9c55-aad6b835913c&oauth_nonce=kbki9sCGRwU&oauth_signature_method=HMAC-SHA1&oauth_timestamp=1543562643&oauth_version=1.0";
            string strParamters = "oauth_consumer_key="+strOAuthConsumerKey+"&oauth_nonce=kbki9sCGRwU&oauth_signature_method=HMAC-SHA1&oauth_timestamp="+ strTimeStamp + "&oauth_version=1.0";

            string strRequestUrl = "POST&" + AuthHelper.UpperCaseUrlEncode(("https://connectapi.garmin.com/oauth-service/oauth/request_token").ToLower()) + "&" + AuthHelper.UpperCaseUrlEncode(strParamters);


            //string HashKey = "fq07vfP6JodQr0EmgnPYUxKPkNNv8pKoib6" + "&";
            string HashKey = ConfigurationManager.AppSettings["hashkey"] + "&";

            var signature = AuthHelper.GenerateOAuthSignature(HashKey,strRequestUrl);

            strRequestUrl = strRequestUrl + AuthHelper.UpperCaseUrlEncode(signature);


            string PostUrl = "https://connectapi.garmin.com/oauth-service/oauth/request_token";
            Hashtable htKeys = GenerateHasTable(signature);

            //string AuthHeader = "oauth_version=\"1.0\", oauth_consumer_key=\"37d14781-5529-4a3a-9c55-aad6b835913c\", oauth_timestamp=\"1543562643\", oauth_nonce=\"kbki9sCGRwU\", oauth_signature_method=\"HMAC-SHA1\", oauth_signature=\"" + signature+"\"";

            string AuthHeader = "oauth_version=\"1.0\", oauth_consumer_key=\""+ strOAuthConsumerKey + "\", oauth_timestamp=\""+ strTimeStamp + "\", oauth_nonce=\"kbki9sCGRwU\", oauth_signature_method=\"HMAC-SHA1\", oauth_signature=\"" + signature + "\"";
            try
            {
                HttpClient hc = new HttpClient();
                hc.DefaultRequestHeaders.Add("Authorization", "OAuth " + AuthHeader);

                HttpResponseMessage response = hc.PostAsync(PostUrl, null).Result;

                string str = response.Content.ReadAsStringAsync().Result;

                strOAuthToken = str.Split('&')[0];
                string strOAuthTokenSecret = str.Split('&')[1];

                strOAuthToken = strOAuthToken.Split('=')[1];
                strOAuthTokenSecret = strOAuthTokenSecret.Split('=')[1];

                HttpContext.Current.Session["OAuthToken_Secret"] = strOAuthTokenSecret;

            
                



            }          
           
            catch (Exception ex)
            {
                throw;
            }
            string AuthCallBack = ConfigurationManager.AppSettings["AuthCallBackPage"];//"http://dit.dev.grcdemo.com/Garmin/Pages/Callback.aspx";

            Response.Redirect("https://connect.garmin.com/oauthConfirm?oauth_token=" + strOAuthToken + "&oauth_callback=" + AuthCallBack + "&state=hahaha");


        }

        private Hashtable GenerateHasTable(string signature)
        {
            Hashtable htKeys = new Hashtable();
            //htKeys.Add("oauth_consumer_key", "37d14781-5529-4a3a-9c55-aad6b835913c");
            htKeys.Add("oauth_consumer_key", strOAuthConsumerKey);
            htKeys.Add("oauth_nonce", "kbki9sCGRwU");
            htKeys.Add("oauth_signature_method", "HMACSHA1");
            htKeys.Add("oauth_timestamp", strTimeStamp);
            htKeys.Add("oauth_version", "1.0");
            htKeys.Add("oauth_signature", signature);

            return htKeys;

        }
    }
}