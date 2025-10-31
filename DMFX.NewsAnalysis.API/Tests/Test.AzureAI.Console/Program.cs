// NOTE: before running - run in cmd: az login --tenant <tenandid 064edd8d-d78d-47c6-b0fe-75a90ef1b1bb> and re-start MSVS

using Azure;
using Azure.AI.Agents.Persistent;
using Azure.AI.Inference;
using Azure.AI.Projects;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.Identity;
using System;

namespace Test.AzureAI.ConsoleApp
{
    public class Program
    {
        
        private static string AZURE_AI_ENDPOINT = "https://dmfx-test-project-resource02.services.ai.azure.com/models";
        private static string AZURE_AI_MODEL = "gpt-4o";
        private static string AZURE_AI_KEY = "4QPahJhhVOm8Cv0dTvQKDAvWJUT0rqXopnCpu2phhqFN0jCSK27zJQQJ99BIACfhMk5XJ3w3AAAAACOGd9mM";
        public static void Main(string[] args)
        {
            Console.WriteLine("Test.AzureAI.ConsoleApp");

            // ChatAgentExample();
            ChatCompletionExample();
            ChatCompletionWithToolsExample();
        }

        private static void ChatAgentExample()
        {
            var projectEndpoint = AZURE_AI_ENDPOINT;
            var modelDeploymentName = AZURE_AI_MODEL;

            PersistentAgentsClient client = new(projectEndpoint, new DefaultAzureCredential());

            PersistentAgent agent = client.Administration.CreateAgent(
                model: modelDeploymentName,
                name: "SDK Test Agent - Tutor",
                instructions: "You are a personal electronics tutor. Write and run code to answer questions.",
                tools: new List<ToolDefinition> { new CodeInterpreterToolDefinition() });

            PersistentAgentThread thread = client.Threads.CreateThread();

            // Create message to thread
            PersistentThreadMessage messageResponse = client.Messages.CreateMessage(
                thread.Id,
                MessageRole.User,
                "I need to solve the equation `3x + 11 = 14`. Can you help me?");

            // Run the Agent
            ThreadRun run = client.Runs.CreateRun(thread, agent);

            // Wait for the run to complete
            do
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(500));
                run = client.Runs.GetRun(thread.Id, run.Id);
            }
            while (run.Status == RunStatus.Queued
                || run.Status == RunStatus.InProgress);
            Pageable<PersistentThreadMessage> messages = client.Messages.GetMessages(
                threadId: thread.Id,
                order: ListSortOrder.Ascending
            );
            // Print the messages in the thread
            WriteMessages(messages);

            // Delete the thread and agent after use
            client.Threads.DeleteThread(thread.Id);
            client.Administration.DeleteAgent(agent.Id);

            // Temporary function to use a list of messages in the thread and write them to the console.
            static void WriteMessages(IEnumerable<PersistentThreadMessage> messages)
            {
                foreach (PersistentThreadMessage threadMessage in messages)
                {
                    Console.Write($"{threadMessage.CreatedAt:yyyy-MM-dd HH:mm:ss} - {threadMessage.Role,10}: ");
                    foreach (MessageContent contentItem in threadMessage.ContentItems)
                    {
                        if (contentItem is MessageTextContent textItem)
                        {
                            Console.Write(textItem.Text);
                        }
                        else if (contentItem is MessageImageFileContent imageFileItem)
                        {
                            Console.Write($"<image from ID: {imageFileItem.FileId}");
                        }
                        Console.WriteLine();
                    }
                }
            }
        }


        private static void ChatCompletionExample()
        {
            var connstring = new Uri(AZURE_AI_ENDPOINT);
            var modelDeplName = AZURE_AI_MODEL;

            var credential = new AzureKeyCredential(AZURE_AI_KEY);

            ChatCompletionsClient chatCompletionsClient = new ChatCompletionsClient(
                    new Uri(AZURE_AI_ENDPOINT),
                    new AzureKeyCredential(AZURE_AI_KEY)
                );

            var requestOptions = new ChatCompletionsOptions()
            {
                Messages = new List<ChatRequestMessage>(),
                Model = modelDeplName,
            };

            string input = string.Empty;
            while (!input.ToLower().Equals("exit"))
            {
                Console.Write("You: ");
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    var message = new ChatRequestUserMessage(input);
                    requestOptions.Messages.Clear();
                    requestOptions.Messages.Add(message);

                    Response<ChatCompletions> response = chatCompletionsClient.Complete(requestOptions);
                    Console.WriteLine("AI: " + response.Value.Content + "\r\n");
                }
            }
        }

        private static void ChatCompletionWithToolsExample()
        {
            var connstring = new Uri(AZURE_AI_ENDPOINT);
            var modelDeplName = AZURE_AI_MODEL;

            var credential = new AzureKeyCredential(AZURE_AI_KEY);

            ChatCompletionsClient chatCompletionsClient = new ChatCompletionsClient(
                    new Uri(AZURE_AI_ENDPOINT),
                    new AzureKeyCredential(AZURE_AI_KEY)
                );

            var requestOptions = new ChatCompletionsOptions()
            {
                Messages = new List<ChatRequestMessage>(),
                Model = modelDeplName
            };

            

            string input = string.Empty;
            while (!input.ToLower().Equals("exit"))
            {
                Console.Write("You: ");
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    var message = new ChatRequestUserMessage(input);
                    requestOptions.Messages.Clear();
                    requestOptions.Messages.Add(message);

                    Response<ChatCompletions> response = chatCompletionsClient.Complete(requestOptions);
                    Console.WriteLine("AI: " + response.Value.Content + "\r\n");
                }
            }
        }
    }
}
