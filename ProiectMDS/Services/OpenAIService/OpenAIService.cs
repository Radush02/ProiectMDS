﻿using Azure.Core;
using OpenAI;
using OpenAI.Assistants;
using OpenAI.Models;
using OpenAI.Chat;
using ProiectMDS.Exceptions;
using ProiectMDS.Models;
namespace ProiectMDS.Services
{
    public class OpenAIService:IOpenAIService
    {

        OpenAIClient client;

        public OpenAIService(){
            client = new OpenAIClient();
        }

        public async Task<OpenAIDTO> GetDescription(OpenAIDTO prompt)  
        {
            var messages = new List<Message>
            {
                new Message(Role.System, "You are a helpful assistant that will help users better format their content. " +
                "The user wants to advertise their car rental and wants to enhance their description. " +
                "Reply with just the description the user might want to use. Enhance the description in the requested language." +
                " Make the description as presentable as possible, including bullet points (where possible)," +
                " new lines, missing details you think the owner might want to include, or any other enhancements you can think of."+
                "If the user specifies just the car and the year, come up with details of the car buyers might want to know of."),
                new Message(Role.User, prompt.prompt)
            };
            var chatRequest = new ChatRequest(messages, Model.GPT3_5_Turbo);
            var response = await client.ChatEndpoint.GetCompletionAsync(chatRequest);
            var choice = response.FirstChoice;
            if (choice == null)
            {
                throw new NotFoundException("No response from OpenAI");
            }
            return new OpenAIDTO {prompt = choice.Message};

        }
    }
}


/*            
 *            
 *            var model = new CreateAssistantRequest(
                name: "Car salesman",
                instructions: "You are a helpful assistant that will help users better format their content. " +
                "The user wants to advertise their car rental and wants to enhance their description." +
                " Reply with just the description the user might want to use. " +
                "A user may speak other language than English, so make sure to reply in the requested language."+
                "Make the description as presentable as possible, including bullet points (where possible), " +
                "new lines, missing details you think the user might want to include, or any other enhancements you can think of.",
                model: Model.GPT3_5_Turbo);
            var assistant = await client.AssistantsEndpoint.CreateAssistantAsync(model);
            var thread = await client.ThreadsEndpoint.CreateThreadAsync();
            var message = await thread.CreateMessageAsync(prompt);
            var run = await thread.CreateRunAsync(assistant);
            return ($"[{run.Id}] {run.Status} | {run.CreatedAt}");
*/