using Discord;
using Discord.WebSocket;
using System;
using System.IO;
using System.Threading.Tasks;

namespace testBot
{
    class Program
    {
        public static void Main(string[] args)
        => new Program().MainAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _client.MessageReceived += CommandHandler;
            _client.Log += Log;

            //  You can assign your bot token to a string, and pass that in to connect.
            //  This is, however, insecure, particularly if you plan to have your code hosted in a public repository.
            var token = File.ReadAllText("C:/Users/Alex/token.txt");

            // Some alternative options would be to keep your token in an Environment Variable or a standalone file.
            // var token = Environment.GetEnvironmentVariable("NameOfYourEnvironmentVariable");
            // var token = File.ReadAllText("token.txt");
            // var token = JsonConvert.DeserializeObject<AConfigurationClass>(File.ReadAllText("config.json")).Token;

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private Task CommandHandler(SocketMessage message)
        {
            //variables
            string command = "";
            int lengthOfCommand = -1;
            //filtering messages begin here
            if (!message.Content.StartsWith('/')) //This is your prefix
                if (message.Content.StartsWith("poll:"))
                {
                    
                }
                else
                {
                    return Task.CompletedTask;
                }
                

            if (message.Author.IsBot) //This ignores all commands from bots
                return Task.CompletedTask;

            if (message.Content.Contains(' '))
                lengthOfCommand = message.Content.IndexOf(' ');
            else
                lengthOfCommand = message.Content.Length;
            if (message.Content.StartsWith("poll:"))
            {
                command = message.Content.Substring(0, lengthOfCommand - 1).ToLower();
            }
            else
            {
                command = message.Content.Substring(1, lengthOfCommand - 1).ToLower();
            }
                
            Console.WriteLine(command);

            //Commands begin here

            var thumbsup = 0xD83D;

            if (command.Equals("hello"))
            {
                message.Channel.SendMessageAsync($@"Hello {message.Author.Mention}");
            }
            else if (command.Equals("age"))
            {
                message.Channel.SendMessageAsync($@"Your account was created at {message.Author.CreatedAt.DateTime.Date}");
            }
            else if (command.Equals("alligator"))
            {
                message.Channel.SendFileAsync("memes/alligator.jpeg");
            }
            else if (command.Equals("british") || command.Equals("British"))
            {
                message.Channel.SendFileAsync("memes/floppa_british.png");
            }
            else if (command.Equals("poll"))
            {
                message.AddReactionAsync(new Emoji("👍"));
                message.AddReactionAsync(new Emoji("👎"));                
            }
            else
            {

            }




            return Task.CompletedTask;
        }
    }
}
