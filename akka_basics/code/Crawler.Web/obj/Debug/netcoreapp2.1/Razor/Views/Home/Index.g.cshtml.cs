#pragma checksum "C:\projects\brownbags\akka_basics\code\Crawler.Web\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3bbcfbc843c5bce1f5db17912fd5444d6ddf6214"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Index.cshtml", typeof(AspNetCore.Views_Home_Index))]
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
#line 1 "C:\projects\brownbags\akka_basics\code\Crawler.Web\Views\_ViewImports.cshtml"
using Crawler.Web;

#line default
#line hidden
#line 2 "C:\projects\brownbags\akka_basics\code\Crawler.Web\Views\_ViewImports.cshtml"
using Crawler.Web.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3bbcfbc843c5bce1f5db17912fd5444d6ddf6214", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"73cba8fe2a804f89a56a6bde131228c70477c469", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "C:\projects\brownbags\akka_basics\code\Crawler.Web\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
            BeginContext(45, 462, true);
            WriteLiteral(@"
<div class=""jumbotron"">
    <h1>Demo Web Scraper</h1>
    <p>Akka.NET Spyfu brownbag</p>
</div>

<div class=""row"">
    <div class=""input-group"">
        <span class=""input-group-btn"">
            <button class=""btn btn-default"" type=""button"" id=""btnQuery"">Query</button>
        </span>
        <input type=""text"" class=""form-control"" placeholder=""Crawl domain..."" id=""crawlDomain"">
    </div>
    <div class=""container"" id=""app""></div>
</div>

");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(526, 6751, true);
                WriteLiteral(@"
    <script src=""lib/signalr/dist/browser/signalr.js""></script>
    <script src=""lib/date.format.js""></script>
    <script src=""lib/vue/dist/vue.min.js""></script>

    <script>
        // global state (yes, I know, I'm terrible)
        window.jobs = [];
        window.messages = [];

        var Jobs = {
            template: `
            <div class='jobs container'>
                <button v-on:click='startAll' v-if='jobs.length >= 1' type='button' class='btn btn-danger' style='margin-bottom: 15px; margin-top: 15px;'>Start All</button>
                <div class='row' style='margin-bottom: 15px; margin-top: 15px;' v-for='job in jobs'>
                    <div class='col'>
                        <div class='card shadow' style='background-color: goldenrod;' :style='{ backgroundColor: (job.status == ""error"") ? ""FireBrick"" : (job.status == ""done"") ? ""LightGreen"" : (job.status == ""todo"") ? ""LightSteelBlue"" : ""PaleGoldenrod"" }'>
                            <div class='card-header'>
          ");
                WriteLiteral(@"                      {{ job.uri }}
                                <button type='button' class='close' v-on:click='remove(job)'>&times;</button>
                            </div>
                            <div class='card-body'>
                                <h5>Internal Links</h5>
                                <ul>
                                    <li v-for='link in job.internalLinks'>{{ link }}</li>
                                </ul>
                                <h5>External Links</h5>
                                <ul>
                                    <li v-for='link in job.externalLinks'>{{ link }}</li>
                                </ul>
                            </div>
                            <div class='card-footer text-right'>
                                <button type='button' class='btn btn-info' v-on:click='start(job)'>Start</button>
                            </div>
                        </div>
                    </div>
                </div>
");
                WriteLiteral(@"            </div>
            `,
            props: {
                jobs: { default: [] }
            },
            computed: {
                rows: function() {
                    return this.jobs.reduce((accumulator, job, index) => {
                            if (index % 4 === 0) {
                                accumulator.push([job]);
                            } else {
                                accumulator[accumulator.length - 1].push(job);
                            }

                            return accumulator;
                        },
                        []);
                },
            },
            methods: {
                remove: function(domain) {
                    var index = window.jobs.indexOf(domain);
                    if (index !== -1) {
                        window.jobs.splice(index, 1);
                    }
                },
                start: function(domain) {
                    window.chat.send('StartCrawl', domain.");
                WriteLiteral(@"uri);
                },
                startAll: function() {
                    this.jobs.forEach(job => window.chat.send('StartCrawl', job.uri));
                }
            }
        };

        window.app = new Vue({
            el: '#app',
            components: {
                jobs: Jobs,
            },
            data() {
                return {
                    uris: window.jobs,
                }
            },
            template: `<jobs :jobs='uris'></jobs>`,
        });

        $(function() {
            // Reference the auto-generated proxy for the hub.
            var chat = new signalR.HubConnectionBuilder().withUrl(""/hubs/crawlHub"").build();
            window.chat = chat;
            // Create a function that the hub can call back to display messages.
            chat.on(""writeStatus"",
                function(message) {
                    addMessage(message);
                });

            chat.on(""startCrawl"",
                function(domain");
                WriteLiteral(@") {
                    addJob(domain);
                });

            chat.on(""setLinks"",
                function(domain, links, isInternal) {
                    console.log('setLinks received: ', links);
                    var job = window.jobs.find(job => job.uri == domain);
                    console.log('job: ', job);
                    if (job) {
                        if (isInternal) {
                            job.internalLinks = links;
                        } else {
                            job.externalLinks = links;
                        }
                    }
                });

            chat.on(""setStatus"",
                function(domain, status) {
                    console.log('setStatus received: ', status, domain);
                    var job = window.jobs.find(job => job.uri == domain);
                    if (job) {
                        job.status = status.toLowerCase();
                    }
                });

            chat.on(""setE");
                WriteLiteral(@"rror"",
                function(domain, error) {
                    console.log('setError received: ', domain, error);
                    var job = window.jobs.find(job => job.uri == domain);
                    if (job) {
                        job.status = 'error';
                        job.error = error;
                    }
                });


            chat.start().then(function() {
                $('#btnQuery').click(formHandler);
                $('#crawlDomain').keypress(function(e) {
                    if (e.which == 13) {
                        formHandler();
                        return false;
                    }
                });

                function formHandler() {
                    // add job
                    var domain = $('#crawlDomain').val();
                    addJob(domain);
                    //call the StartCrawl method on the hub
                    //chat.send(""StartCrawl"", domain);

                    $('#crawlDomain').val('')");
                WriteLiteral(@".focus();
                };
            });

            function addJob(domain, status) {
                status = status ? status : 'todo';
                console.log('adding domain: ', domain);
                window.jobs.push({
                    uri: domain,
                    internalLinks: [],
                    externalLinks: [],
                    status: status,
                    errorMessage: ''
                });
            }

            function addMessage(message) {
                window.messages.unshift(message);
            }
        });
    </script>
");
                EndContext();
            }
            );
            BeginContext(7280, 4, true);
            WriteLiteral("\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
