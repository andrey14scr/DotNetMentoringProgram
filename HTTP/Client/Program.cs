using Client.Properties;

namespace Client;

class Program
{
    static async Task Main(string[] args)
    {
        var client = new CustomClient();

        await client.GetName(Resources.TestName);

        //await client.Get(Resources.InformationUrl);
        await client.Get(Resources.SuccessUrl);
        await client.Get(Resources.RedirectionUrl);
        await client.Get(Resources.ClientErrorUrl);
        await client.Get(Resources.ServerErrorUrl);
    }
}