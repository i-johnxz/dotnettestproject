using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TagHelperDemo.TagHelpers
{
    public class GreeterTagHelper : TagHelper
    {
        [HtmlAttributeName("name")]
        public string Name { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "p";
            output.Content.SetContent($"Hello {Name}");
            base.Process(context, output);
        }
    }
}
