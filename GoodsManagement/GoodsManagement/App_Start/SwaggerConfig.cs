using System.Web.Http;
using WebActivatorEx;
using GoodsManagement;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace GoodsManagement
{
    public class SwaggerConfig
    {
        private static string[] GetXmlCommentsPath()
        {
            string[] xmls = new string[] {
                string.Format("{0}/bin/GoodsManagement.XML", System.AppDomain.CurrentDomain.BaseDirectory)
                 };
            return xmls;
        }
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "GoodsManagement");
                        var xmls = GetXmlCommentsPath();

                        foreach (var item in xmls)
                        {
                            c.IncludeXmlComments(item);
                        }
                    })
                .EnableSwaggerUi();
        }
    }
}
