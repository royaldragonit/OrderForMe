#pragma checksum "E:\ProjectBackup\OrderForMeProject\Views\Shared\_Header.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6630f6930e5bce218f3c8b9e7a3e2ddb7fa1e9ae"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__Header), @"mvc.1.0.view", @"/Views/Shared/_Header.cshtml")]
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
#line 1 "E:\ProjectBackup\OrderForMeProject\Views\_ViewImports.cshtml"
using OrderForMeProject;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\ProjectBackup\OrderForMeProject\Views\_ViewImports.cshtml"
using OrderForMeProject.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "E:\ProjectBackup\OrderForMeProject\Views\_ViewImports.cshtml"
using OrderForMeProject.Commons;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "E:\ProjectBackup\OrderForMeProject\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "E:\ProjectBackup\OrderForMeProject\Views\_ViewImports.cshtml"
using Microsoft.EntityFrameworkCore;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "E:\ProjectBackup\OrderForMeProject\Views\Shared\_Header.cshtml"
using OrderForMeProject.Models.Entities;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6630f6930e5bce218f3c8b9e7a3e2ddb7fa1e9ae", @"/Views/Shared/_Header.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9cca0df4e58e365c233060ac6520214fcbfb4d79", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__Header : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "E:\ProjectBackup\OrderForMeProject\Views\Shared\_Header.cshtml"
   
    OrderformeContext _db = new OrderformeContext();
    int userId = User.GetUserId();
    Users user =await _db.Users.FindAsync(userId);


#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <!-- BEGIN: Header-->
<nav class=""header-navbar navbar navbar-expand-lg align-items-center floating-nav navbar-light navbar-shadow"">
    <div class=""navbar-container d-flex content"">
        <ul class=""nav navbar-nav align-items-center ml-auto"">
            <li class=""nav-item dropdown dropdown-language"">
                <a class=""nav-link dropdown-toggle fix-bug-not-show-button"" href=""javascript:void(0);"" data-toggle=""dropdown"" aria-haspopup=""true"" aria-expanded=""false"">
                    <i class=""flag-icon flag-icon-us""></i><span class=""selected-language"">English</span>
                </a>
                <div class=""dropdown-menu dropdown-menu-right fix-bug-not-show"" aria-labelledby=""dropdown-flag"">
                    <a class=""dropdown-item"" href=""javascript:void(0);"" data-language=""en"" onclick=""fChangeLanguage('en-US','English');"">
                        <i class=""flag-icon flag-icon-us""></i> English
                    </a>
                    <a class=""dropdown-item"" href=""javascr");
            WriteLiteral(@"ipt:void(0);"" data-language=""vi"" onclick=""fChangeLanguage('vi-VN',' Vi???t Nam');"">
                        <i class=""flag-icon flag-icon-vn""></i> Vi???t Nam
                    </a>
                </div>
            </li>
            <li class=""nav-item d-none d-lg-block""><a class=""nav-link nav-link-style""><i class=""ficon"" data-feather=""moon""></i></a></li>
            <li class=""nav-item dropdown dropdown-user"">
                <a class=""nav-link dropdown-toggle dropdown-user-link"" id=""dropdown-user"" href=""javascript:void(0);"" data-toggle=""dropdown"" aria-haspopup=""true"" aria-expanded=""false"">
                    <div class=""user-nav d-sm-flex d-none""><span class=""user-name font-weight-bolder"">");
#nullable restore
#line 29 "E:\ProjectBackup\OrderForMeProject\Views\Shared\_Header.cshtml"
                                                                                                 Write(user.Username);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><span class=\"user-status\">");
#nullable restore
#line 29 "E:\ProjectBackup\OrderForMeProject\Views\Shared\_Header.cshtml"
                                                                                                                                                Write(user.Username);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</span></div><span class=""avatar""><img class=""round"" src=""/admin/app-assets/images/portrait/small/avatar-s-11.jpg"" alt=""avatar"" height=""40"" width=""40""><span class=""avatar-status-online""></span></span>
                </a>
                <div class=""dropdown-menu dropdown-menu-right"" aria-labelledby=""dropdown-user"">
                    <a class=""dropdown-item""");
            BeginWriteAttribute("href", " href=\"", 2387, "\"", 2429, 1);
#nullable restore
#line 32 "E:\ProjectBackup\OrderForMeProject\Views\Shared\_Header.cshtml"
WriteAttributeValue("", 2394, Url.Content("/Users/Edit/"+userId), 2394, 35, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@"><i class=""mr-50"" data-feather=""user""></i> Trang c?? nh??n</a>
                    <a class=""dropdown-item"" href=""/Login/Logout""><i class=""mr-50"" data-feather=""power""></i> ????ng xu???t</a>
                </div>
            </li>
        </ul>
    </div>
</nav>
<ul class=""main-search-list-defaultlist-other-list d-none"">
    <li class=""auto-suggestion justify-content-between"">
        <a class=""d-flex align-items-center justify-content-between w-100 py-50"">
            <div class=""d-flex justify-content-start""><span class=""mr-75"" data-feather=""alert-circle""></span><span>No results found.</span></div>
        </a>
    </li>
</ul>
<!-- END: Header-->
<script>
    $(document).ready(function () {
        if (getCookie(""lang"") == ""English"") {
            $("".fix-bug-not-show-button"").empty();
            $("".fix-bug-not-show-button"").append(`<i class=""flag-icon flag-icon-us""></i><span class=""selected-language"">English</span>`);
        } else {
            $("".fix-bug-not-show-button"").empty();
   ");
            WriteLiteral(@"         $("".fix-bug-not-show-button"").append(`<i class=""flag-icon flag-icon-vn""></i><span class=""selected-language""> Vi???t Nam</span>`);
        }
    });
    function fChangeLanguage(culture,lang) {
        $.ajax({
            url: '/Home/Setlanguage?culture=' + culture,
            type: 'GET',
            data: {}
        }).done(function (res) {
            setCookie(""lang"", lang, 30);
            location.reload();
        });
    }
</script>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IViewLocalizer localizer { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
