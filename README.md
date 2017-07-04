# MicoServiceTest
基于ASP.NET WebAPI OWIN实现Self-Host项目实战</br>
====  
# 引用</br>
寄宿ASP.NET Web API 不一定需要IIS 的支持，我们可以采用Self Host 的方式使用任意类型的应用程序（控制台、Windows Forms 应用、WPF 应用甚至是Windows Service）作为宿主。</br>

# Host和Self—Host分别是什么意思？</br>
Host：中文解释是“宿主”的意思。Self—Host中文解释是“自我宿主”的意思。Host宿主一词我们不会陌生，它可以看作是一个基础设施，它为一些服务和功能提供最底层的支持，如你的web应用程序可以运行在iis或者apache上，而这两个东西就是web应用程序的宿主，而今天说的自主宿主SelfHost就是说，它可以自己去监听自己的服务，如你可以把一个web应用程序宿主到一个console控制台程序上，或者把一个webApi宿主到一个控制台应用程序、桌面应用程序或者windowService上，这都是可以的。</br>

# OWIN到底是神马东西？

OWIN的英文全称是Open Web Interface for .NET。

OWIN是针对.NET平台的开放Web接口，那Web接口是谁和谁之间的接口呢？OWIN就是.NET Web应用程序与Web服务器之间的接口。

OWIN定义了.NET web服务器和web应用程序间的抽象。OWIN通过将web服务器从应用程序解耦，使得为.NET web开发创建中间件和移植web应用程序到其他托管——比如，Window服务或其他进程的自我托管——变得更加容易。
# 基于OWIN规范实现的HTTP服务器有哪些？

支持OWIN标准的WEB应用的高性能的跨平台HTTP服务器，比如:TinyFox、Jexus Web Server 等等。

# 实现WEB应用程序的Self-Host解决方案

通过HttpListener实现简单的Http服务（.NET 2.0+）

基于WCF堆栈的自宿主SelfHosting

采用ASP.NET HttpSelfHost来承载WebAPI服务 (.NET 4.0+)

采用ASP.NET OWIN来承载WebAPI服务(.NET4.5 微软推荐使用)

## 程序结构描述
HTTPRequestConsole 模拟HTTP请求</br>
WebApplication1 模拟网页AJAX请求</br>
MicoServiceTest WebAPI基于OWIN的控制台微服务</br>

## 参考网站
https://damienbod.com/2014/03/28/web-api-file-upload-single-or-multiple-files/
https://www.strathweb.com/2012/08/a-guide-to-asynchronous-file-uploads-in-asp-net-web-api-rtm/
## Git提交解决
https://my.oschina.net/tearlight/blog/193913
