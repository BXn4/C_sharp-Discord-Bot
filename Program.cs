using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using Discord.Audio;
using System.Threading;
using System.Timers;

namespace BenCMDSdc
{
    class Program
    {
        static void Main(string[] args) => new Program().bot_indit().GetAwaiter().GetResult();
        private DiscordSocketClient kliens;
        private CommandService parancsok;
        private IServiceProvider szolgaltatasok;
        public string prefix = string.Empty;
        public string token = string.Empty;
        public string botneve = string.Empty;
        int i = 0;
        private List<string> statuslista = new List<string>() { ".parancsok || .vc create", ".parancsok || .tc create", ".parancsok || .kocka" };
        public struct config
        {

            [JsonProperty("token")]
            public string Token { get; set; }
            [JsonProperty("prefix")]
            public string Prefix { get; set; }
            [JsonProperty("botneve")]
            public string Botneve { get; set; }
        }
        public async Task bot_indit()
        {
            var json = string.Empty;
            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);
            var configjson = JsonConvert.DeserializeObject<config>(json);
            kliens = new DiscordSocketClient();
            parancsok = new CommandService();
            szolgaltatasok = new ServiceCollection()
                .AddSingleton(kliens)
                .AddSingleton(parancsok).BuildServiceProvider();
            await kliens.SetStatusAsync(UserStatus.Online);
            await kliens.SetGameAsync("Elakadtál? .parancsok", "", ActivityType.Watching);
            System.Timers.Timer statusztimer = new System.Timers.Timer();
            statusztimer.Elapsed += new ElapsedEventHandler(statusztimertick);
            statusztimer.Interval = 10000;
            statusztimer.Enabled = true;
            var config = new config()
            {
                Token = configjson.Token,
                Prefix = configjson.Prefix,
                Botneve = configjson.Botneve

            };
            prefix = config.Prefix;
            token = config.Token;
            botneve = config.Botneve;
            Console.WriteLine($"A {botneve} éppen indul. A parancsok használatához használd a: {prefix} karaktert a parancs elején.");
            //Console.ReadKey();
            kliens.Log += kliens_Log;
            await parancsokregisztral();
            await kliens.LoginAsync(TokenType.Bot, token);
            await kliens.StartAsync();
            await Task.Delay(-1);
        }

        private Task kliens_Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        public async Task parancsokregisztral()
        {
            kliens.MessageReceived += parancsasyncs;
            await parancsok.AddModulesAsync(Assembly.GetEntryAssembly(), szolgaltatasok);
        }

        private async Task parancsasyncs(SocketMessage arg)
        {
            var uzenet = arg as SocketUserMessage;
            var context = new SocketCommandContext(kliens, uzenet);
            if (uzenet.Author.IsBot)
            {
                return;
            }
            int argx = 0;
            if (uzenet.HasStringPrefix(prefix, ref argx))
            {
                var eredmeny = await parancsok.ExecuteAsync(context, argx, szolgaltatasok);
                if (!eredmeny.IsSuccess) Console.WriteLine(eredmeny.ErrorReason);
            }
        }
        private void statusztimertick(object source, ElapsedEventArgs e)
        {
            i++;
            if(i == 1)
            {
                kliens.SetGameAsync("Text létrehozása: .tc create", "", ActivityType.Watching);
            }
            if (i == 2)
            {
                kliens.SetGameAsync("Unatkozol? .kocka", "", ActivityType.Watching);
            }
            if(i == 3)
            {
                kliens.SetGameAsync("Voice létrehozása: .vc create", "", ActivityType.Watching);
            }
            if(i == 4)
            {
                kliens.SetGameAsync($"{kliens.Guilds.Count} szerver", "", ActivityType.Watching);
            }
            if(i == 5)
            {
                kliens.SetGameAsync("Elakadtál? .parancsok", "", ActivityType.Watching);
            }
            if(i == 6)
            {
                i = 0;
            }
            //Console.WriteLine(i);
        }
    }
}
