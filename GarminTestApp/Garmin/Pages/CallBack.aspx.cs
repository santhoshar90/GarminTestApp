using CHBase_FitBit_TestProject.Garmin.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CHBase_FitBit_TestProject.Garmin.Pages
{
    public partial class CallBack : System.Web.UI.Page
    {
        public string OAuthToken
        {
            get
            {
                if (!String.IsNullOrEmpty(Request["oauth_token"]))
                {
                    return Request["oauth_token"];
                }
                return string.Empty;
            }
            set { }
        }

        public string OauthVerifier
        {
            get
            {
                if (!String.IsNullOrEmpty(Request["oauth_verifier"]))
                {
                    return Request["oauth_verifier"];
                }
                return string.Empty;
            }
        }
        string strOAuthConsumerKey = ConfigurationManager.AppSettings["oauth_consumer_key"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(OAuthToken) && !string.IsNullOrEmpty(OauthVerifier))
            {
                string strPostUrl = "https://connectapi.garmin.com/oauth-service/oauth/access_token";

                string strNounce = AuthHelper.GenerateNounce();

                string strParamters = "oauth_consumer_key=" + strOAuthConsumerKey + "&oauth_nonce=" + strNounce + "&oauth_signature_method=HMAC-SHA1&oauth_timestamp=" + strNounce + "&oauth_token="+ OAuthToken + "&oauth_verifier="+OauthVerifier+"&oauth_version=1.0";
                string strRequestUrl = "POST&" + AuthHelper.UpperCaseUrlEncode(("https://connectapi.garmin.com/oauth-service/oauth/access_token").ToLower()) + "&" + AuthHelper.UpperCaseUrlEncode(strParamters);

                string HashKey = ConfigurationManager.AppSettings["hashkey"] + "&" + HttpContext.Current.Session["OAuthToken_Secret"];

                var signature = AuthHelper.GenerateOAuthSignature(HashKey, strRequestUrl);

                string AuthHeader = "oauth_version=\"1.0\",oauth_consumer_key=\"" + strOAuthConsumerKey + "\", oauth_timestamp=\"" + strNounce+"\", oauth_nonce=\"" + strNounce + "\", oauth_signature_method=\"HMAC-SHA1\", oauth_signature=\"" + signature + "\", oauth_verifier=\""+ OauthVerifier+"\", oauth_token=\""+ OAuthToken+ "\"";
                try
                {
                    HttpClient hc = new HttpClient();
                    hc.DefaultRequestHeaders.Add("Authorization", "OAuth " + AuthHeader);

                    HttpResponseMessage response = hc.PostAsync(strPostUrl, null).Result;

                    string str = response.Content.ReadAsStringAsync().Result;

                    string strOAuthToken = str.Split('&')[0];
                    string strOAuthTokenSecret = str.Split('&')[1];
                    strOAuthToken = strOAuthToken.Split('=')[1];
                    strOAuthTokenSecret = strOAuthTokenSecret.Split('=')[1];

                    HttpContext.Current.Session["OAuthToken_Secret"] = strOAuthTokenSecret;
                    HttpContext.Current.Session["OAuthToken"] = strOAuthToken;

                    string UserIDUrl = "https://healthapi.garmin.com/wellness-api/rest/user/id";

                     strNounce = AuthHelper.GenerateNounce();
                    string OAuthToken = HttpContext.Current.Session["OAuthToken"].ToString();

                    string startTime = AuthHelper.GenerateNounce(DateTime.Now.AddDays(-90));
                    string EndTime = strNounce;


                     strParamters = "oauth_consumer_key=" + strOAuthConsumerKey + "&oauth_nonce=" + strNounce + "&oauth_signature_method=HMAC-SHA1&oauth_timestamp=" + strNounce + "&oauth_token=" + OAuthToken + "&oauth_version=1.0"; //&summaryEndTimeInSeconds=" + EndTime + "&summaryStartTimeInSeconds=" + startTime
                     strRequestUrl = "GET&" + AuthHelper.UpperCaseUrlEncode((UserIDUrl).ToLower()) + "&" + AuthHelper.UpperCaseUrlEncode(strParamters);

                     HashKey = ConfigurationManager.AppSettings["hashkey"] + "&" + HttpContext.Current.Session["OAuthToken_Secret"];

                     signature = AuthHelper.GenerateOAuthSignature(HashKey, strRequestUrl);

                    // ActivitiesUrl = ActivitiesUrl + "?summaryStartTimeInSeconds={0}&summaryEndTimeInSeconds={1}";

                    //ActivitiesUrl = string.Format(ActivitiesUrl, startTime, EndTime);
                     AuthHeader = "oauth_version=\"1.0\", oauth_consumer_key=\"" + strOAuthConsumerKey + "\", oauth_timestamp=\"" + strNounce + "\", oauth_nonce=\"" + strNounce + "\", oauth_signature_method=\"HMAC-SHA1\", oauth_signature=\"" + signature + "\", oauth_token=\"" + OAuthToken + "\"";
                    try
                    {
                        hc = new HttpClient();
                        hc.DefaultRequestHeaders.Add("Authorization", "OAuth " + AuthHeader);

                        response = hc.GetAsync(UserIDUrl).Result;

                        str = response.Content.ReadAsStringAsync().Result;



                    }
                    catch
                    {
                        throw;
                    }


                    Response.Redirect("Activities.aspx");


                }
                catch { throw; }


            }
        }

        protected void lnkActivities_ServerClick(object sender, EventArgs e)
        {
           


        }
    }
}