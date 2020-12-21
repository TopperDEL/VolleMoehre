using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace VolleMoehre.Web.Helper
{
    public class MoehreImageTagHelper : TagHelper
    {
        public string Spieler { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string spieler = Spieler.Replace("Göksen", "Goeksen").Replace("Philipp", "PhilippH").Replace("PhilippHW", "PhilippW").Replace("MichaelM", "Michael");
            output.TagName = "img";
            output.Attributes.Add("src", "/images/Fotos2013/klein" + spieler + ".png");
            output.Attributes.Add("alt", Spieler);
        }
    }
}
