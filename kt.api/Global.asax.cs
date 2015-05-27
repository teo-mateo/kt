using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Configuration;

namespace kt.api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            using(var db = new Models.KTContext())
            {
                var cnt = db.Decks.Count();
            }
        }

        public static string GetConnectionString(){
            var azureKey = "SQLAZURECONNSTR_ktContext";
            var connectionString = Environment.GetEnvironmentVariable(azureKey);

            if (connectionString == null)
            {
                connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ktConnectionString"].ConnectionString;
            }

            return connectionString;
        }
    }
}
