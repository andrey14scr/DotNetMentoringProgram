using System;

namespace HelloLibrary
{
    public class HelloService
    {
        public string GetHelloString(string username)
        {
            return $"{DateTime.Now} Hello, {username}!";
        }
    }
}
