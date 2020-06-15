using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HastaneYonetim
{
    public static class WebApiConfig
    {
        public static void Kayit(HttpConfiguration config)
        {
        
            var ayar = GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings;
            ayar.ContractResolver = new CamelCasePropertyNamesContractResolver();
            ayar.Formatting = Formatting.Indented;

           
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            config.Formatters.Remove(config.Formatters.XmlFormatter);




            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
