using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace VolleMoehre.Web.Helper
{
    public class MoehreContentTagHelper : TagHelper
    {
        private static string BaseUrl = "https://contentnew.vollemoehre.de/";
        public string Url { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            WebRequest.DefaultWebProxy = null;
            output.TagName = "p";

            try
            {
                output.Content.SetHtmlContent(new WebClient() { Encoding = System.Text.Encoding.UTF8 }.DownloadString(BaseUrl + Url).Replace("/media/", BaseUrl + "media/"));
            }
            catch (Exception ex)
            {
                output.Content.SetContent("Fehler beim Laden von " + Url);
            }
        }
    }
}
