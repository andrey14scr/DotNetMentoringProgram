using Client.Properties;

namespace Client;

class Program
{
    static async Task Main(string[] args)
    {
        var client = new CustomClient("http://localhost:8888");

        await client.GetName(Resources.TestName);
        await client.MyNameByHeader(Resources.TestName + "2");
        await client.MyNameByCookies(Resources.TestName + "3");

        //await client.Get(Resources.InformationUrl);
        await client.Get(Resources.SuccessUrl);
        await client.Get(Resources.RedirectionUrl);
        await client.Get(Resources.ClientErrorUrl);
        await client.Get(Resources.ServerErrorUrl);
    }
}