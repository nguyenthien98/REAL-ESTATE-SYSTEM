#pragma checksum "C:\Users\Thuy Dao Xuan\Desktop\Real_Estate_System\Source Code\.NET Core\RES\RES\Areas\Admin\Views\Home\Search.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5cc4239f9a423bdfeb65741bcc2075e76c2e5f08"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Home_Search), @"mvc.1.0.view", @"/Areas/Admin/Views/Home/Search.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Admin/Views/Home/Search.cshtml", typeof(AspNetCore.Areas_Admin_Views_Home_Search))]
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
#line 1 "C:\Users\Thuy Dao Xuan\Desktop\Real_Estate_System\Source Code\.NET Core\RES\RES\Areas\Admin\Views\_ViewImports.cshtml"
using RES;

#line default
#line hidden
#line 2 "C:\Users\Thuy Dao Xuan\Desktop\Real_Estate_System\Source Code\.NET Core\RES\RES\Areas\Admin\Views\_ViewImports.cshtml"
using RES.Models;

#line default
#line hidden
#line 3 "C:\Users\Thuy Dao Xuan\Desktop\Real_Estate_System\Source Code\.NET Core\RES\RES\Areas\Admin\Views\_ViewImports.cshtml"
using RES.Models.Common;

#line default
#line hidden
#line 1 "C:\Users\Thuy Dao Xuan\Desktop\Real_Estate_System\Source Code\.NET Core\RES\RES\Areas\Admin\Views\Home\Search.cshtml"
using RES.Data.DBModels;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5cc4239f9a423bdfeb65741bcc2075e76c2e5f08", @"/Areas/Admin/Views/Home/Search.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8cdcd4bfcd2d1ebbc40950dcfbe29b8ed6bfd87a", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_Home_Search : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Post>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "~/Areas/Identity/Pages/Account/Manage/_StatusMessage.cshtml", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("target", new global::Microsoft.AspNetCore.Html.HtmlString("_blank"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("margin-right: 3px"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-route-returnUrl", "~/admin/search", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("onsubmit", new global::Microsoft.AspNetCore.Html.HtmlString("return confirm(\'Bạn có chắc chắn muốn ẩn bài đăng này?\');"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(45, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 4 "C:\Users\Thuy Dao Xuan\Desktop\Real_Estate_System\Source Code\.NET Core\RES\RES\Areas\Admin\Views\Home\Search.cshtml"
  
    ViewData["Title"] = "Tất cả bài đăng | Admin BDS Miền Trung";
    Layout = "~/Areas/Admin/Views/Shared/_Layout.cshtml";
    RealEstateSystemContext _context = new RealEstateSystemContext();

    var notice = TempData["Notice"];

    if (notice != null)
    {
        ViewData["Notice"] = notice;
    }

#line default
#line hidden
            BeginContext(370, 103, true);
            WriteLiteral("\r\n<div class=\"animated fadeIn\">\r\n    <div class=\"row\">\r\n\r\n        <div class=\"col-md-12\">\r\n            ");
            EndContext();
            BeginContext(473, 155, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "5cc4239f9a423bdfeb65741bcc2075e76c2e5f087092", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#line 21 "C:\Users\Thuy Dao Xuan\Desktop\Real_Estate_System\Source Code\.NET Core\RES\RES\Areas\Admin\Views\Home\Search.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = (ViewData["Notice"] == null ? null : ViewData["Notice"].ToString());

#line default
#line hidden
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(628, 792, true);
            WriteLiteral(@"
            <div class=""card"">
                <div class=""card-header"">
                    <strong class=""card-title"">Quản lý bài đăng</strong>
                </div>
                <div class=""card-body"">
                    <table id=""bootstrap-data-table"" class=""table table-striped table-bordered"">
                        <thead>
                            <tr>
                                <th>Trạng thái</th>
                                <th>Tiêu đề</th>
                                <th>Loại bài đăng</th>
                                <th>Tác giả</th>
                                <th>Thời gian đăng</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
");
            EndContext();
#line 39 "C:\Users\Thuy Dao Xuan\Desktop\Real_Estate_System\Source Code\.NET Core\RES\RES\Areas\Admin\Views\Home\Search.cshtml"
                             foreach (var post in Model)
                            {

#line default
#line hidden
            BeginContext(1509, 38, true);
            WriteLiteral("                                <tr>\r\n");
            EndContext();
#line 42 "C:\Users\Thuy Dao Xuan\Desktop\Real_Estate_System\Source Code\.NET Core\RES\RES\Areas\Admin\Views\Home\Search.cshtml"
                                      DateTime posttime = post.PostTime ?? DateTime.Now;
                                        string status = _context.Status.Find(post.Status).StatusType;
                                        if (post.Status == 1)
                                        {

#line default
#line hidden
            BeginContext(1846, 66, true);
            WriteLiteral("                                            <td class=\"text-blue\">");
            EndContext();
            BeginContext(1913, 6, false);
#line 46 "C:\Users\Thuy Dao Xuan\Desktop\Real_Estate_System\Source Code\.NET Core\RES\RES\Areas\Admin\Views\Home\Search.cshtml"
                                                             Write(status);

#line default
#line hidden
            EndContext();
            BeginContext(1919, 7, true);
            WriteLiteral("</td>\r\n");
            EndContext();
#line 47 "C:\Users\Thuy Dao Xuan\Desktop\Real_Estate_System\Source Code\.NET Core\RES\RES\Areas\Admin\Views\Home\Search.cshtml"
                                        }
                                        else if (post.Status == 2)
                                        {

#line default
#line hidden
            BeginContext(2080, 67, true);
            WriteLiteral("                                            <td class=\"text-green\">");
            EndContext();
            BeginContext(2148, 6, false);
#line 50 "C:\Users\Thuy Dao Xuan\Desktop\Real_Estate_System\Source Code\.NET Core\RES\RES\Areas\Admin\Views\Home\Search.cshtml"
                                                              Write(status);

#line default
#line hidden
            EndContext();
            BeginContext(2154, 7, true);
            WriteLiteral("</td>\r\n");
            EndContext();
#line 51 "C:\Users\Thuy Dao Xuan\Desktop\Real_Estate_System\Source Code\.NET Core\RES\RES\Areas\Admin\Views\Home\Search.cshtml"
                                        }
                                        else
                                        {

#line default
#line hidden
            BeginContext(2293, 65, true);
            WriteLiteral("                                            <td class=\"text-red\">");
            EndContext();
            BeginContext(2359, 6, false);
#line 54 "C:\Users\Thuy Dao Xuan\Desktop\Real_Estate_System\Source Code\.NET Core\RES\RES\Areas\Admin\Views\Home\Search.cshtml"
                                                            Write(status);

#line default
#line hidden
            EndContext();
            BeginContext(2365, 7, true);
            WriteLiteral("</td>\r\n");
            EndContext();
#line 55 "C:\Users\Thuy Dao Xuan\Desktop\Real_Estate_System\Source Code\.NET Core\RES\RES\Areas\Admin\Views\Home\Search.cshtml"
                                        }
                                    

#line default
#line hidden
            BeginContext(2454, 40, true);
            WriteLiteral("                                    <td>");
            EndContext();
            BeginContext(2495, 11, false);
#line 57 "C:\Users\Thuy Dao Xuan\Desktop\Real_Estate_System\Source Code\.NET Core\RES\RES\Areas\Admin\Views\Home\Search.cshtml"
                                   Write(post.Tittle);

#line default
#line hidden
            EndContext();
            BeginContext(2506, 47, true);
            WriteLiteral("</td>\r\n                                    <td>");
            EndContext();
            BeginContext(2555, 71, false);
#line 58 "C:\Users\Thuy Dao Xuan\Desktop\Real_Estate_System\Source Code\.NET Core\RES\RES\Areas\Admin\Views\Home\Search.cshtml"
                                    Write(post.PostTypeNavigation.Name + " " + post.RealEstaleTypeNavigation.Name);

#line default
#line hidden
            EndContext();
            BeginContext(2627, 47, true);
            WriteLiteral("</td>\r\n                                    <td>");
            EndContext();
            BeginContext(2676, 50, false);
#line 59 "C:\Users\Thuy Dao Xuan\Desktop\Real_Estate_System\Source Code\.NET Core\RES\RES\Areas\Admin\Views\Home\Search.cshtml"
                                    Write(post.Author.LastName + " " + post.Author.Firstname);

#line default
#line hidden
            EndContext();
            BeginContext(2727, 47, true);
            WriteLiteral("</td>\r\n                                    <td>");
            EndContext();
            BeginContext(2776, 40, false);
#line 60 "C:\Users\Thuy Dao Xuan\Desktop\Real_Estate_System\Source Code\.NET Core\RES\RES\Areas\Admin\Views\Home\Search.cshtml"
                                    Write(posttime.ToString("dd/MM/yyyy hh:mm tt"));

#line default
#line hidden
            EndContext();
            BeginContext(2817, 118, true);
            WriteLiteral("</td>\r\n                                    <td style=\"display: inline-flex\">\r\n                                        ");
            EndContext();
            BeginContext(2935, 218, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5cc4239f9a423bdfeb65741bcc2075e76c2e5f0815210", async() => {
                BeginContext(3006, 140, true);
                WriteLiteral("\r\n                                            <input type=\"submit\" class=\"btn-info\" value=\"Xem\" />\r\n                                        ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "action", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 2949, "/", 2949, 1, true);
#line 62 "C:\Users\Thuy Dao Xuan\Desktop\Real_Estate_System\Source Code\.NET Core\RES\RES\Areas\Admin\Views\Home\Search.cshtml"
AddHtmlAttributeValue("", 2950, post.PostId, 2950, 12, false);

#line default
#line hidden
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3153, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 65 "C:\Users\Thuy Dao Xuan\Desktop\Real_Estate_System\Source Code\.NET Core\RES\RES\Areas\Admin\Views\Home\Search.cshtml"
                                         if (post.Status == 1 || post.Status == 2)
                                        {

#line default
#line hidden
            BeginContext(3282, 44, true);
            WriteLiteral("                                            ");
            EndContext();
            BeginContext(3326, 415, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5cc4239f9a423bdfeb65741bcc2075e76c2e5f0817933", async() => {
                BeginContext(3585, 149, true);
                WriteLiteral("\r\n                                                <input type=\"submit\" class=\"btn-danger\" value=\"Ẩn\" />\r\n                                            ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 67 "C:\Users\Thuy Dao Xuan\Desktop\Real_Estate_System\Source Code\.NET Core\RES\RES\Areas\Admin\Views\Home\Search.cshtml"
                                                                                              WriteLiteral(post.PostId);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-returnUrl", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
            }
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["returnUrl"] = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3741, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 71 "C:\Users\Thuy Dao Xuan\Desktop\Real_Estate_System\Source Code\.NET Core\RES\RES\Areas\Admin\Views\Home\Search.cshtml"
                                        }

#line default
#line hidden
            BeginContext(3786, 82, true);
            WriteLiteral("                                    </td>\r\n                                </tr>\r\n");
            EndContext();
#line 74 "C:\Users\Thuy Dao Xuan\Desktop\Real_Estate_System\Source Code\.NET Core\RES\RES\Areas\Admin\Views\Home\Search.cshtml"
                            }

#line default
#line hidden
            BeginContext(3899, 146, true);
            WriteLiteral("                        </tbody>\r\n                    </table>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Post>> Html { get; private set; }
    }
}
#pragma warning restore 1591
