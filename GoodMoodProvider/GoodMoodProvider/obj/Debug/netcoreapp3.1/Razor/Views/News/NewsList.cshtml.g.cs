#pragma checksum "C:\Users\Lenovo\Documents\GitHub\SAU-TestRepository\GoodMoodProvider\GoodMoodProvider\Views\News\NewsList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "150ad473c77c12d680a14d38810426a3bfc3532e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_News_NewsList), @"mvc.1.0.view", @"/Views/News/NewsList.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Lenovo\Documents\GitHub\SAU-TestRepository\GoodMoodProvider\GoodMoodProvider\Views\_ViewImports.cshtml"
using GoodMoodProvider;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Lenovo\Documents\GitHub\SAU-TestRepository\GoodMoodProvider\GoodMoodProvider\Views\_ViewImports.cshtml"
using ModelsLibrary;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"150ad473c77c12d680a14d38810426a3bfc3532e", @"/Views/News/NewsList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1dfea078ce36ea26a590daf207b95f2f54fbe7b5", @"/Views/_ViewImports.cshtml")]
    public class Views_News_NewsList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<News>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n");
#nullable restore
#line 4 "C:\Users\Lenovo\Documents\GitHub\SAU-TestRepository\GoodMoodProvider\GoodMoodProvider\Views\News\NewsList.cshtml"
  
    ViewData["Title"] = "NewsList";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<h2>NewsList</h2>\r\n<br>\r\n<br>\r\n");
#nullable restore
#line 13 "C:\Users\Lenovo\Documents\GitHub\SAU-TestRepository\GoodMoodProvider\GoodMoodProvider\Views\News\NewsList.cshtml"
 foreach (News news in Model)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <h3>");
#nullable restore
#line 15 "C:\Users\Lenovo\Documents\GitHub\SAU-TestRepository\GoodMoodProvider\GoodMoodProvider\Views\News\NewsList.cshtml"
   Write(news.Article);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n    <br>\r\n");
#nullable restore
#line 18 "C:\Users\Lenovo\Documents\GitHub\SAU-TestRepository\GoodMoodProvider\GoodMoodProvider\Views\News\NewsList.cshtml"
Write(Html.Raw(System.Web.HttpUtility.HtmlDecode(@news.Body)));

#line default
#line hidden
#nullable disable
            WriteLiteral("    <br>\r\n");
#nullable restore
#line 21 "C:\Users\Lenovo\Documents\GitHub\SAU-TestRepository\GoodMoodProvider\GoodMoodProvider\Views\News\NewsList.cshtml"
     if (news.Source != null)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("<i> from: </i> <a");
            BeginWriteAttribute("href", " href=\"", 357, "\"", 376, 1);
#nullable restore
#line 22 "C:\Users\Lenovo\Documents\GitHub\SAU-TestRepository\GoodMoodProvider\GoodMoodProvider\Views\News\NewsList.cshtml"
WriteAttributeValue("", 364, news.Source, 364, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("></a>");
#nullable restore
#line 22 "C:\Users\Lenovo\Documents\GitHub\SAU-TestRepository\GoodMoodProvider\GoodMoodProvider\Views\News\NewsList.cshtml"
                                               }

#line default
#line hidden
#nullable disable
            WriteLiteral("    <br>\r\n");
#nullable restore
#line 25 "C:\Users\Lenovo\Documents\GitHub\SAU-TestRepository\GoodMoodProvider\GoodMoodProvider\Views\News\NewsList.cshtml"
Write(news.DatePosted);

#line default
#line hidden
#nullable disable
            WriteLiteral("    <a");
            BeginWriteAttribute("href", " href=\"", 427, "\"", 457, 2);
            WriteAttributeValue("", 434, "/News/EditNews/", 434, 15, true);
#nullable restore
#line 27 "C:\Users\Lenovo\Documents\GitHub\SAU-TestRepository\GoodMoodProvider\GoodMoodProvider\Views\News\NewsList.cshtml"
WriteAttributeValue("", 449, news.ID, 449, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">     Edit</a>\r\n    <a");
            BeginWriteAttribute("href", " href=\"", 480, "\"", 512, 2);
            WriteAttributeValue("", 487, "/News/DeleteNews/", 487, 17, true);
#nullable restore
#line 28 "C:\Users\Lenovo\Documents\GitHub\SAU-TestRepository\GoodMoodProvider\GoodMoodProvider\Views\News\NewsList.cshtml"
WriteAttributeValue("", 504, news.ID, 504, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">     Delete</a>     \r\n");
            WriteLiteral("    <hr>\r\n");
            WriteLiteral("    <hr>\r\n");
            WriteLiteral("    <br>\r\n");
#nullable restore
#line 36 "C:\Users\Lenovo\Documents\GitHub\SAU-TestRepository\GoodMoodProvider\GoodMoodProvider\Views\News\NewsList.cshtml"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<News>> Html { get; private set; }
    }
}
#pragma warning restore 1591
