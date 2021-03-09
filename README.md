# [cdcavell.name](https://cdcavell.name)
Personal Website for Christopher D. Cavell
<hr />

[![GitHub license](https://img.shields.io/github/license/cdcavell/cdcavell.name)](https://github.com/cdcavell/cdcavell.name/blob/main/LICENSE)
![GitHub tag (latest by date)](https://img.shields.io/github/v/tag/cdcavell/cdcavell.name)
![GitHub top language](https://img.shields.io/github/languages/top/cdcavell/cdcavell.name)
![GitHub language count](https://img.shields.io/github/languages/count/cdcavell/cdcavell.name)
[![CodeQL Analysis](https://github.com/cdcavell/cdcavell.name/workflows/CodeQL%20Analysis/badge.svg)](https://github.com/cdcavell/cdcavell.name/actions?query=workflow%3A%22CodeQL+Analysis%22)
[![W3C Validation](https://img.shields.io/w3c-validation/default?targetUrl=https%3A%2F%2Fcdcavell.name)](https://validator.nu/?doc=https%3A%2F%2Fcdcavell.name)
[![Security Headers](https://img.shields.io/security-headers?url=https%3A%2F%2Fcdcavell.name)](https://securityheaders.com/?q=https%3A%2F%2Fcdcavell.name)

<hr />

Project incorporates generation of markdown files in Documentation folder, during project builds, from comment syntax of source code, through console application XmlToMarkdown. Documentation changes are maintained in a [wiki submodule](https://brendancleary.com/2013/03/08/including-a-github-wiki-in-a-repository-as-a-submodule/) that is also updated during project build.

Target Frameworks are [ASP.NET Core 5.0](https://dotnet.microsoft.com/download/dotnet/5.0) and [.NET Standard 2.1](https://docs.microsoft.com/en-us/dotnet/standard/net-standard) respectfully. Developed and built in a Windows environment utilizing [Visual Studio Community 2019 ](https://visualstudio.microsoft.com/vs/) source-code editor. Repository is [Git](https://git-scm.com/) utilizing [git-flow](https://danielkummer.github.io/git-flow-cheatsheet/) extention to provide high-level repository operations for [Vincent Driessen's branching model](https://nvie.com/posts/a-successful-git-branching-model/).

This work is [licensed](https://github.com/cdcavell/cdcavell.name/blob/main/LICENSE) under the [MIT License](https://opensource.org/licenses/MIT), with the exception of Duende&trade; IdentityServer software libraries which are licensed under the [Duende&trade; Software License Agreement](https://duendesoftware.com/license/identityserver.pdf). All other software libraries are licensed under their respective license agreements. Source Code documentation is found in repository [Wiki](https://github.com/cdcavell/cdcavell.name/wiki) section. 

<hr />

_If you are cloning this repository, please enter commands as follows:_

```
$ git clone --recurse-submodules https://github.com/cdcavell/cdcavell.name.git

$ cd cdcavell.name

$ git flow init -d
```

<hr />



[__Website__](https://cdcavell.name) deployment 

| Build | Date | Description |
|-------|------|-------------|
| 1.0.3.3 | 03/09/2021 | __Update:__ User Authorization Web Service <br /> Dependency update <br />&nbsp;&nbsp;&nbsp;&nbsp;_Duende.IdentityServer v5.0.5_ <br />&nbsp;&nbsp;&nbsp;&nbsp;_Duende.IdentityServer.Storage v5.0.5_ <br />&nbsp;&nbsp;&nbsp;&nbsp;_Duende.IdentityServer.AspNetIdentity v5.0.5_ |
| 1.0.3.2 | 02/20/2021 | __Update:__ Dependency update <br />&nbsp;&nbsp;&nbsp;&nbsp;_Microsoft.Extensions.Logging.ApplicationInsights v2.17.0_ <br />&nbsp;&nbsp;&nbsp;&nbsp;_Duende.IdentityServer v5.0.4_ <br />&nbsp;&nbsp;&nbsp;&nbsp;_Duende.IdentityServer.AspNetIdentity v5.0.4_ |
| 1.0.3.1 | 02/09/2021 | __Update:__ User Authorization Web Service |
| 1.0.3.0 | 02/06/2021 | __Add:__ Initial build Authorization Service |
| 1.0.2.3 | 01/21/2021 | __Update:__ Update Duende software libraries v5.0.1 |
| 1.0.2.2 | 01/18/2021 | __Update:__ Convert GrantType from Implicit to Pkce |
| 1.0.2.1 | 01/17/2021 | __Update:__ Handle HttpRequestException as http status instead of application exception |
| 1.0.2.0 | 01/16/2021 | __Add:__ Initial build Duende IdentityServer5 |
| 1.0.1.3 | 01/16/2021 | __Update:__ Change wiki submodule branch from main to master <br />  __Update:__ Dependency update<br />&nbsp;&nbsp;&nbsp;&nbsp;_Microsoft.AspNetCore.Authentication.Facebook v5.0.2_<br />&nbsp;&nbsp;&nbsp;&nbsp;_Microsoft.AspNetCore.Authentication.Google v5.0.2_<br />&nbsp;&nbsp;&nbsp;&nbsp;_Microsoft.AspNetCore.Authentication.MicrosoftAccount v5.0.2_<br />&nbsp;&nbsp;&nbsp;&nbsp;_Microsoft.AspNetCore.Authentication.Twitter v5.0.2_<br />&nbsp;&nbsp;&nbsp;&nbsp;_Microsoft.AspNetCore.Identity.EntityFrameworkCore v5.0.2_<br />&nbsp;&nbsp;&nbsp;&nbsp;_Microsoft.EntityFrameworkCore v5.0.2_<br />&nbsp;&nbsp;&nbsp;&nbsp;_Microsoft.EntityFrameworkCore.Sqlite v5.0.2_<br />&nbsp;&nbsp;&nbsp;&nbsp;_Microsoft.EntityFrameworkCore.Tools v5.0.2_<br />&nbsp;&nbsp;&nbsp;&nbsp;_Microsoft.AspNetCore.Authentication.OpenIdConnect v5.0.2_ |
| 1.0.1.2 | 11/29/2020 | __Update:__ Implement Registration/Roles/Permissions [#183](https://github.com/cdcavell/cdcavell.name/issues/183) |
| 1.0.1.1 | 11/27/2020 | __Update:__ Convert master repository to main |
| 1.0.1.0 | 11/27/2020 | __Update:__ Target Framework netcoreapp3.1 to net5.0 <br /> __Update:__ use cdcavell/automerge-action@v0.12.0 |
| 1.0.0.9 | 11/22/2020 | __Add:__ Implement Registration/Roles/Permissions [#183](https://github.com/cdcavell/cdcavell.name/issues/183) <br /> __Add:__ Blog Site link <br /> __Fix:__ Meta description length <br /> __Add:__ Ping Google with the location of sitemap <br /> __Update:__ allowed-branch version |
| 1.0.0.8 | 11/01/2020 | __Update:__ Bing Search APIs will transition from Azure Cognitive Services to Azure Marketplace on 31 October 2023 [#152](https://github.com/cdcavell/cdcavell.name/issues/152) |
| 1.0.0.7 | 10/31/2020 | __Fix:__ Eliminate render-blocking resources [#171](https://github.com/cdcavell/cdcavell.name/issues/171) <br /> __Fix:__ Serve static assets with an efficient cache policy [#172](https://github.com/cdcavell/cdcavell.name/issues/172) <br /> __Add:__ Integrate Bing's Adaptive URL submission API with your website [#144](https://github.com/cdcavell/cdcavell.name/issues/144) |
| 1.0.0.6 | 10/31/2020 | __Update:__ Convert Sitemap class to build sitemap.xml dynamic based on existing controllers in project [#145](https://github.com/cdcavell/cdcavell.name/issues/145) |
| 1.0.0.5 | 10/31/2020 | __Add:__ EU General Data Protection Regulation (GDPR) support in ASP.NET Core [#161](https://github.com/cdcavell/cdcavell.name/issues/161) |
| 1.0.0.4 | 10/30/2020 | __Add:__ Enforce HTTPS in ASP.NET Core [#158](https://github.com/cdcavell/cdcavell.name/issues/158) |
| 1.0.0.3 | 10/30/2020 | __Fix:__ Addressed Issues [#142](https://github.com/cdcavell/cdcavell.name/issues/142) [#143](https://github.com/cdcavell/cdcavell.name/issues/143) [#146](https://github.com/cdcavell/cdcavell.name/issues/146) [#147](https://github.com/cdcavell/cdcavell.name/issues/147) [#150](https://github.com/cdcavell/cdcavell.name/issues/150) |
| 1.0.0.2 | 10/28/2020 | __Fix:__ Change twitter description in layout |
| 1.0.0.1 | 10/28/2020 | __Fix:__ Center Search Pagination |
| 1.0.0.0 | 10/28/2020 | __Initial Development__ |
