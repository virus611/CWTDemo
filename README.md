# CWTDemo说明

本人就是不喜欢动不动就上JWT这种东西。JWT好是肯定的，但不是无脑的JWT，要看场景。

JWT的缺点也多，具体请看 https://www.cnblogs.com/kklldog/p/should-we-need-jwt-always.html 的描述

我自己撸了一个轻量级的demo，使用方式相同，客户端设置header就行。

# 原理

参考官方文档的模型绑定 https://docs.microsoft.com/zh-cn/aspnet/core/mvc/models/model-binding?view=aspnetcore-3.1



# 使用方式

download下来源码，.net core 3.1的，除了newtownjson外，没有奇奇怪怪的第三方包。当然，全用System.Text.Json也没有问题。

运行后访问Login/Index，demo是假登录。这个时候的输出就是授权后的token。

请求 Test/Index, Header["UID"]=刚才的那一串。如果显示12345，说明OK。否则401错误


