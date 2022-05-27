#pragma checksum "E:\ProjectBackup\OrderForMeProject\Views\Users\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "056454b54eb230ec641a55e2a5fba0c46f1dd912"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Users_Index), @"mvc.1.0.view", @"/Views/Users/Index.cshtml")]
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
#line 1 "E:\ProjectBackup\OrderForMeProject\Views\Users\Index.cshtml"
using OrderForMeProject.Models.Entities;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\ProjectBackup\OrderForMeProject\Views\Users\Index.cshtml"
using OrderForMeProject.Commons;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"056454b54eb230ec641a55e2a5fba0c46f1dd912", @"/Views/Users/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9cca0df4e58e365c233060ac6520214fcbfb4d79", @"/Views/_ViewImports.cshtml")]
    public class Views_Users_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Users>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-success"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("add-new-record modal-content pt-0"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 5 "E:\ProjectBackup\OrderForMeProject\Views\Users\Index.cshtml"
  
    ViewData["Title"] = "Index";
    bool isAdmin = true;
    if (User.Identity.IsAuthenticated)
    {
        string role = User.GetUserRole();
        //Nếu không phải quyền Administrator
        if (role != null && role.ToInt() != 1)
        {
            isAdmin = false;
        }
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 19 "E:\ProjectBackup\OrderForMeProject\Views\Users\Index.cshtml"
 if (isAdmin)
{


#line default
#line hidden
#nullable disable
            WriteLiteral("    <p>\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "056454b54eb230ec641a55e2a5fba0c46f1dd9125760", async() => {
                WriteLiteral("Tạo mới");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    </p>\r\n");
#nullable restore
#line 25 "E:\ProjectBackup\OrderForMeProject\Views\Users\Index.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<!-- Basic table -->
<section id=""basic-datatable"">
    <div class=""row"">
        <div class=""col-12"">
            <div class=""card"">
                <table class=""dt-groupProduct table"">
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>Tên tài khoản</th>
                            <th>Họ tên</th>
                            <th>Địa chỉ</th>
                            <th>Số điện thoại</th>
                            <th>Email</th>
                            <th>Số dư</th>
                            <th></th>
                        </tr>
                    </thead>
                </table>
            </div>
        </div>
    </div>
    <!-- Modal to add new record -->
    <div class=""modal modal-slide-in fade"" id=""modals-slide-in"">
        <div class=""modal-dialog sidebar-sm"">
            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "056454b54eb230ec641a55e2a5fba0c46f1dd9128119", async() => {
                WriteLiteral(@"
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">×</button>
                <div class=""modal-header mb-1"">
                    <h5 class=""modal-title"" id=""exampleModalLabel"">New Record</h5>
                </div>
                <div class=""modal-body flex-grow-1"">
                    <div class=""form-group"">
                        <label class=""form-label"" for=""basic-icon-default-fullname"">Full Name</label>
                        <input type=""text"" class=""form-control dt-full-name"" id=""basic-icon-default-fullname"" placeholder=""John Doe"" aria-label=""John Doe"" />
                    </div>
                    <div class=""form-group"">
                        <label class=""form-label"" for=""basic-icon-default-post"">Post</label>
                        <input type=""text"" id=""basic-icon-default-post"" class=""form-control dt-post"" placeholder=""Web Developer"" aria-label=""Web Developer"" />
                    </div>
                    <div class=""form-group""");
                WriteLiteral(@">
                        <label class=""form-label"" for=""basic-icon-default-email"">Email</label>
                        <input type=""text"" id=""basic-icon-default-email"" class=""form-control dt-email"" placeholder=""john.doe@example.com"" aria-label=""john.doe@example.com"" />
                        <small class=""form-text text-muted""> You can use letters, numbers & periods </small>
                    </div>
                    <div class=""form-group"">
                        <label class=""form-label"" for=""basic-icon-default-date"">Joining Date</label>
                        <input type=""text"" class=""form-control dt-date"" id=""basic-icon-default-date"" placeholder=""MM/DD/YYYY"" aria-label=""MM/DD/YYYY"" />
                    </div>
                    <div class=""form-group mb-4"">
                        <label class=""form-label"" for=""basic-icon-default-salary"">Salary</label>
                        <input type=""text"" id=""basic-icon-default-salary"" class=""form-control dt-salary"" placeholder=""$12000"" aria-l");
                WriteLiteral(@"abel=""$12000"" />
                    </div>
                    <button type=""button"" class=""btn btn-primary data-submit mr-1"">Submit</button>
                    <button type=""reset"" class=""btn btn-outline-secondary"" data-dismiss=""modal"">Cancel</button>
                </div>
            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n</section>\r\n<!--/ Basic table -->\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n");
#nullable restore
#line 88 "E:\ProjectBackup\OrderForMeProject\Views\Users\Index.cshtml"
      await Html.RenderPartialAsync("_ValidationScriptsPartial");

#line default
#line hidden
#nullable disable
                WriteLiteral(@"    <!-- BEGIN: Page Vendor JS-->
    <script src=""/admin/app-assets/vendors/js/tables/datatable/jquery.dataTables.min.js""></script>
    <script src=""/admin/app-assets/vendors/js/tables/datatable/datatables.bootstrap4.min.js""></script>
    <script src=""/admin/app-assets/vendors/js/tables/datatable/dataTables.responsive.min.js""></script>
    <script src=""/admin/app-assets/vendors/js/tables/datatable/responsive.bootstrap4.js""></script>
    <script src=""/admin/app-assets/vendors/js/tables/datatable/datatables.checkboxes.min.js""></script>
    <script src=""/admin/app-assets/vendors/js/tables/datatable/datatables.buttons.min.js""></script>
    <script src=""/admin/app-assets/vendors/js/tables/datatable/jszip.min.js""></script>
    <script src=""/admin/app-assets/vendors/js/tables/datatable/pdfmake.min.js""></script>
    <script src=""/admin/app-assets/vendors/js/tables/datatable/vfs_fonts.js""></script>
    <script src=""/admin/app-assets/vendors/js/tables/datatable/buttons.html5.min.js""></script>
    <script sr");
                WriteLiteral(@"c=""/admin/app-assets/vendors/js/tables/datatable/buttons.print.min.js""></script>
    <script src=""/admin/app-assets/vendors/js/tables/datatable/dataTables.rowGroup.min.js""></script>
    <script src=""/admin/app-assets/vendors/js/pickers/flatpickr/flatpickr.min.js""></script>
    <!-- END: Page Vendor JS-->
    <script>
        $.validator.addMethod(""__dummy__"", function (value, element) {
            return true;
        });
        $(document).ready(function () {
            //Init data
            var dataOrder =");
#nullable restore
#line 110 "E:\ProjectBackup\OrderForMeProject\Views\Users\Index.cshtml"
                      Write(Json.Serialize((isAdmin ? Model : Model.Where(x => x.UsersId == User.GetUserId()))));

#line default
#line hidden
#nullable disable
                WriteLiteral(@";
            console.log(dataOrder)
            var dtProxy = $("".dt-groupProduct"");
            if (dtProxy.length) {
                dtProxy.dataTable({
                    data: dataOrder,
                    columns: [
                        { data: 'usersId' }, // used for sorting so will hide this column
                        { data: 'username' },
                        { data: 'fullname' },
                        { data: 'address' },
                        { data: 'phone' },
                        { data: 'email' },
                        { data: 'balance' },
                    ],
                    columnDefs: [
                        {
                            targets: 1,
                            visible: true
                        },
                        {
                            targets: 2,
                            visible: true
                        },
                        {
                            targets: 3,
                       ");
                WriteLiteral(@"     visible: true
                        },
                        {
                            targets: 4,
                            visible: true
                        },
                        {
                            targets: 5,
                            visible: true
                        },
                        {
                            targets: 6,
                            visible: true
                        },
                        {
                            // Actions
                            targets: 7,
                            title: 'Hành động',
                            orderable: false,
                            render: function (data, type, full, meta) {
                                return (
                                    '<div class=""d-inline-flex"">' +
                                    '<a class=""pr-1 dropdown-toggle hide-arrow text-primary"" data-toggle=""dropdown"">' +
                                    feather.icons[");
                WriteLiteral(@"'more-vertical'].toSvg({ class: 'font-small-4' }) +
                                    '</a>' +
                                    '<div class=""dropdown-menu dropdown-menu-right"">' +
                                    '<a href=""/Users/Delete/' + full[""usersId""] +'"" class=""dropdown-item delete-record"">' +
                                    feather.icons['trash-2'].toSvg({ class: 'font-small-4 mr-50' }) +
                                    'Xoá</a>' +
                                    '</div>' +
                                    '</div>' +
                                    '<a href=""/Users/Edit/' + full[""usersId""] +'"" title=""Sửa"" class=""item-edit"">' +
                                    feather.icons['edit'].toSvg({ class: 'font-small-4' }) +
                                    '</a>'
                                );
                            }
                        }
                    ],
                });
                $('div.head-label').html('<h6 class=""mb-0"">DataTable ");
                WriteLiteral("with Buttons</h6>\');\r\n            }\r\n\r\n        });\r\n    </script>\r\n    <!-- END: Page JS-->\r\n");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Users>> Html { get; private set; }
    }
}
#pragma warning restore 1591
