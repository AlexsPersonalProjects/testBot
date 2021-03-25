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
            command = message.Content;

            if (message.Author.IsBot) //This ignores all commands from bots
                return Task.CompletedTask;



            //Console.WriteLine(command);

            //Commands begin here

            string[] strCommands = { "hello", "age", "poll", "help"};


            //Test hello command
            if (command.Equals("/hello"))
            {
                message.Channel.SendMessageAsync($@"Hello {message.Author.Mention}");
            }
            //Test Age command
            else if (command.Equals("/age"))
            {
                message.Channel.SendMessageAsync($@"Your account was created at {message.Author.CreatedAt.DateTime.Date}");
            }
            //Huge Alligator
            else if (command.Equals("/alligator"))
            {
                message.Channel.SendFileAsync("memes/alligator.jpeg");
            }
            //like bi'ish people are real
            else if (command.Equals("/british") || command.Equals("/British"))
            {
                message.Channel.SendFileAsync("memes/floppa_british.png");
            }
            //Simple poll function, will be used to replace poll bot
            else if (command.StartsWith("poll: "))
            {
                //Thumbs up will always go first, but if more emojis are added, then they will appear randomly, as it is asynchronous
                message.AddReactionAsync(new Emoji("👍"));
                message.AddReactionAsync(new Emoji("👎"));                
            }
            else if (command.Equals("/help"))    //Help command to list all commands
            {
                string strListCommands = "These are the available commands: ";
                foreach (string strListedCommand in strCommands)
                {
                    strListCommands += strListedCommand + " ";
                }
                message.Channel.SendMessageAsync(strListCommands);
            }

            //Dababy memes
            else if (command.Contains("dababy"))
            {
                //Picks a random dababy file
                int rnd = RandomNumber(0, 5);
                if (rnd == 0)
                {
                    message.Channel.SendFileAsync("memes/yeah_yeah.wav");
                }
                else if (rnd == 1)
                {
                    message.Channel.SendFileAsync("memes/i_pull_up.mp3");
                }
                else if (rnd ==2)
                {
                    message.Channel.SendFileAsync("memes/lets_gooo.mp3");
                }
                else if (rnd == 3)
                {
                    //weighted x2 cuz its the best one
                    message.Channel.SendFileAsync("memes/lets_gooo.mp3");
                }
                else if (rnd == 4)
                {
                    message.Channel.SendFileAsync("memes/Ha.mp3");
                }
                else
                {
                    Console.Write("This should not run lol");
                }
            }
            else
            {

            }

            return Task.CompletedTask;
        }
        // Instantiate random number generator.  
        private readonly Random _random = new Random();

        // Generates a random number within a range.      
        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
    }
}
