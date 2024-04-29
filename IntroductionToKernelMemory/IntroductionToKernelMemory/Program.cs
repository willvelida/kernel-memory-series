using Microsoft.Extensions.Configuration;
using Microsoft.KernelMemory;

//var config = new ConfigurationBuilder()
//    .AddJsonFile("appsettings.json")
//    .Build();

//string openAiKey = config["OPEN_AI_KEY"];

//var memory = new KernelMemoryBuilder()
//    .WithOpenAIDefaults(openAiKey)
//    .Build<MemoryServerless>();

//await memory.ImportDocumentAsync("sample-KM-Readme.pdf", documentId: "doc001");

//Console.WriteLine("Ask a question about this document: ");
//var question = Console.ReadLine();

//var answer = await memory.AskAsync(question);

//Console.WriteLine($"Question: {question}\n\nAnswer: {answer.Result}");

//Console.WriteLine("Sources:\n");

//foreach (var source in answer.RelevantSources)
//{
//    Console.WriteLine($"{source.SourceName} - {source.Link} [{source.Partitions.First().LastUpdate:D}]");
//}

var memory = new MemoryWebClient("http://127.0.0.1:9001");

await memory.ImportDocumentAsync("sample-KM-Readme.pdf", documentId: "doc001");

Console.WriteLine("Waiting for memory ingestion to complete...");

while (!await memory.IsDocumentReadyAsync("doc001"))
{
    await Task.Delay(TimeSpan.FromMilliseconds(1500));
}

Console.WriteLine("Ask a question about this document: ");
var question = Console.ReadLine();

var answer = await memory.AskAsync(question);

Console.WriteLine($"Question: {question}\n\nAnswer: {answer.Result}");