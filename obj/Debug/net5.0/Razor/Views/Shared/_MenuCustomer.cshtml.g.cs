#pragma checksum "E:\ProjectBackup\OrderForMeProject\Views\Shared\_MenuCustomer.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "64406a92f03b498b820d4fb4d5a1387a0e2dbcaa"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__MenuCustomer), @"mvc.1.0.view", @"/Views/Shared/_MenuCustomer.cshtml")]
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
#line 3 "E:\ProjectBackup\OrderForMeProject\Views\_ViewImports.cshtml"
using OrderForMeProject.Models.Entities;

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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"64406a92f03b498b820d4fb4d5a1387a0e2dbcaa", @"/Views/Shared/_MenuCustomer.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9cca0df4e58e365c233060ac6520214fcbfb4d79", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__MenuCustomer : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<int>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<li class=\" nav-item\">\n    <a class=\"d-flex align-items-center\"");
            BeginWriteAttribute("href", " href=\"", 74, "\"", 115, 1);
#nullable restore
#line 3 "E:\ProjectBackup\OrderForMeProject\Views\Shared\_MenuCustomer.cshtml"
WriteAttributeValue("", 81, Url.Content("/Users/Edit/"+Model), 81, 34, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@"><i data-feather=""user""></i><span class=""menu-title text-truncate"" data-i18n=""User"">Tài khoản của tôi</span></a>
</li>
<li class="" nav-item"">
    <a class=""d-flex align-items-center"" href=""/Orders""><i data-feather='shopping-cart'></i><span class=""menu-title text-truncate"" data-i18n=""Invoice"">Quản lý đơn hàng</span></a>
</li>
<li class="" nav-item"">
    <a class=""d-flex align-items-center""");
            BeginWriteAttribute("href", " href=\"", 506, "\"", 561, 1);
#nullable restore
#line 9 "E:\ProjectBackup\OrderForMeProject\Views\Shared\_MenuCustomer.cshtml"
WriteAttributeValue("", 513, Url.Content("/Users/TransactionHistory/"+Model), 513, 48, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("><i data-feather=\"dollar-sign\"></i><span class=\"menu-title text-truncate\" data-i18n=\"Invoice\">Lịch sử nạp tiền</span></a>\n</li>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<int> Html { get; private set; }
    }
}
#pragma warning restore 1591