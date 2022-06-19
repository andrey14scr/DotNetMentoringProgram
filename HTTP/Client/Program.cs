using Client.Properties;

namespace Client;

class Program
{
    static async Task Main(string[] args)
    {
        var client = new CustomClient();

        await client.GetName(Resources.TestName);
    }
}