using System.Net;
using Listener;
using Listener.Properties;

var listener = new HttpListener();
listener.Prefixes.Add("http://localhost:8888/");
listener.Start();
Console.WriteLine("Listening...");

while (true)
{
    var context = await listener.GetContextAsync();
    var request = context.Request;
    var response = context.Response;

    var url = ParseRequest(request);
    var responseString = CreateResponse(url, request, response);
    var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
    response.ContentLength64 = buffer.Length;

    await using (var output = response.OutputStream)
    {
        output.Write(buffer, 0, buffer.Length);
    }
}

string[] ParseRequest(HttpListenerRequest request)
{
    var url = request.RawUrl?.Split('/');
    if (url is null || url.Length < 2)
    {
        return new []{ string.Empty };
    }

    return url[1..];
}

string CreateResponse(string[] url, HttpListenerRequest request, HttpListenerResponse response)
{
    var method = url[0];

    if (method.EqualsTo(Resources.MyNameUrl))
    {
        if (url.Length > 1)
        {
            var name = url[1];
            response.StatusCode = 200;
            return name;
        }
    }
    if (method.EqualsTo(Resources.MyNameByHeaderUrl))
    {
        var nameFromHeader = GetMyNameByHeader(request);
        if (nameFromHeader is not null)
        {
            response.StatusCode = 200;
            return nameFromHeader;
        }
    }
    if (method.EqualsTo(Resources.MyNameByCookiesUrl))
    {
        var nameFromCookie = GetMyNameByCookie(request);
        if (nameFromCookie is not null)
        {
            response.StatusCode = 200;
            return nameFromCookie;
        }
    }
    if (method.EqualsTo(Resources.InformationUrl))
    {
        response.StatusCode = 100;
        return string.Empty;
    }
    if (method.EqualsTo(Resources.SuccessUrl))
    {
        response.StatusCode = 200;
        return string.Empty;
    }
    if (method.EqualsTo(Resources.RedirectionUrl))
    {
        response.StatusCode = 300;
        return string.Empty;
    }
    if (method.EqualsTo(Resources.ClientErrorUrl))
    {
        response.StatusCode = 400;
        return string.Empty;
    }
    if (method.EqualsTo(Resources.ServerErrorUrl))
    {
        response.StatusCode = 500;
        return string.Empty;
    }

    response.StatusCode = 404;
    return Resources.NotFound;
}

string GetMyNameByHeader(HttpListenerRequest request)
{
    return request.Headers.Get(Resources.NameHeader);
}

string GetMyNameByCookie(HttpListenerRequest request)
{
    return request.Cookies.FirstOrDefault(c => c.Name.EqualsTo("MyName"))?.Value;
}