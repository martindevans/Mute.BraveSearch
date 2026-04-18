using Microsoft.Extensions.DependencyInjection;
using Mute.BraveSearch;
using Spectre.Console;

AnsiConsole.Write(new FigletText("Brave Search").Color(Color.Orange1));

// 1. Get API key
var apiKey = Environment.GetEnvironmentVariable("BRAVE_API_KEY");
if (string.IsNullOrWhiteSpace(apiKey))
{
    apiKey = AnsiConsole.Ask<string>("Enter your [orange1]Brave API Key[/]:");
}

// 2. Setup DI
var services = new ServiceCollection();
services.AddBraveSearch(apiKey);
var serviceProvider = services.BuildServiceProvider();

var client = serviceProvider.GetRequiredService<IBraveSearchClient>();

// 3. Enter loop
while (true)
{
    var query = AnsiConsole.Ask<string>("[bold blue]Search[/] (or 'exit' to quit):");
    
    if (string.Equals(query, "exit", StringComparison.OrdinalIgnoreCase))
        break;

    if (string.IsNullOrWhiteSpace(query))
        continue;

    await AnsiConsole.Status()
        .StartAsync("Searching Brave...", async ctx =>
        {
            try
            {
                var newsResponse = await client.NewsSearchAsync(new NewsSearchRequest(query)
                {
                    Country = "GB",
                });

                if (newsResponse.Results is { Count: > 0 })
                {
                    AnsiConsole.WriteLine();
                    foreach (var result in newsResponse.Results)
                    {
                        AnsiConsole.Write(new Panel(result.Description ?? "Null Description")
                        {
                            Header = new PanelHeader($"[link={result.Url}]{Markup.Escape(result.Title)}[/] (from {Markup.Escape(result.Profile?.Name ?? "null")})"),
                            Border = BoxBorder.Rounded,
                            Padding = new Padding(1, 0, 1, 0)
                        });

                        if (result.ExtraSnippets is { Count: > 0 })
                        {
                            foreach (var snippet in result.ExtraSnippets)
                                AnsiConsole.MarkupLineInterpolated($" - {snippet}");
                            AnsiConsole.WriteLine();
                        }
                    }
                }
                else
                {
                    AnsiConsole.MarkupLine("[yellow]No news results found.[/]");
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex);
            }
        });

    AnsiConsole.WriteLine();
}
