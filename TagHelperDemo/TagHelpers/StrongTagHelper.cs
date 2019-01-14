using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TagHelperDemo.TagHelpers
{
    [HtmlTargetElement(Attributes = "strong")]
    public class StrongTagHelper : TagHelper
    {
        public string Color { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.RemoveAll("strong");

            output.Attributes.Add("style", "font-weight:bold;");
            if (!String.IsNullOrWhiteSpace(Color))
            {
                output.Attributes.RemoveAll("style");
                output.Attributes.Add("style", $"font-weight:bold;color:{Color};");
            }

            base.Process(context, output);
        }
    }
}
