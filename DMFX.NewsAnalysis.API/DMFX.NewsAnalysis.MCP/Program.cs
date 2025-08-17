using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol;
using System.Net.Http.Headers;

namespace DMFX.NewsAnalysis.MCP
{

    public class Program
    {
        public static async void Main(string[] args)
        {

            var builder = Host.CreateApplicationBuilder(args);

            builder.Logging.AddConsole(consoleLogOptions =>
            {
                // Configure all logs to go to stderr
                consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Trace;
            });

            builder .Services
                    .AddMcpServer()
                    .WithStdioServerTransport()
                    .WithTools<AddTool>()
        }
    }

}
