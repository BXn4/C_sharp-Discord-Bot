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
using System.Diagnostics;

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
                .AddSingleton(new AudioService())
                .AddSingleton(parancsok).BuildServiceProvider();    
            await kliens.SetStatusAsync(UserStatus.Online);
            await kliens.SetGameAsync("Elakadtál? .parancsok", "", ActivityType.Watching);
            System.Timers.Timer statusztimer = new System.Timers.Timer();
            statusztimer.Elapsed += new ElapsedEventHandler(statusztimertick);
            statusztimer.Interval = 50000;
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
            string szoveg = context.Message.Content;
            string masodikresz = context.Message.Content;
            string elsoszam = context.Message.Content;
            string muvelet = context.Message.Content;
            string masodikszam = context.Message.Content;
            string[] tomb = szoveg.Split(' ');
            string parancs = tomb[0];
            EmbedBuilder eb = new EmbedBuilder();
            if(parancs == ".calc")
            {
                try
                {
                    elsoszam = tomb[1];
                    muvelet = tomb[2];
                    masodikszam = tomb[3];
                    if(muvelet == "+")
                    {
                        await context.Channel.SendMessageAsync($"{Convert.ToInt32(elsoszam)} {muvelet} {Convert.ToInt32(masodikszam)} = {Convert.ToInt32(elsoszam) + Convert.ToInt32(masodikszam)}");
                    }
                    if (muvelet == "-")
                    {
                        await context.Channel.SendMessageAsync($"{Convert.ToInt32(elsoszam)} {muvelet} {Convert.ToInt32(masodikszam)} = {Convert.ToInt32(elsoszam) - Convert.ToInt32(masodikszam)}");
                    }
                    if (muvelet == "/")
                    {
                        await context.Channel.SendMessageAsync($"{Convert.ToInt32(elsoszam)} {muvelet} {Convert.ToInt32(masodikszam)} = {Convert.ToInt32(elsoszam) / Convert.ToInt32(masodikszam)}");
                    }
                    if (muvelet == "*")
                    {
                        await context.Channel.SendMessageAsync($"{Convert.ToInt32(elsoszam)} {muvelet} {Convert.ToInt32(masodikszam)} = {Convert.ToInt32(elsoszam) * Convert.ToInt32(masodikszam)}");
                    }
                    if (muvelet == "x")
                    {
                        await context.Channel.SendMessageAsync($"{Convert.ToInt32(elsoszam)} {muvelet} {Convert.ToInt32(masodikszam)} = {Convert.ToInt32(elsoszam) * Convert.ToInt32(masodikszam)}");
                    }
                }
                catch
                { }
            }
            if (parancs == ".kocka")
            {
                try
                {
                    masodikresz = tomb[1];
                    Random r = new Random();
                    List<string> szamok = new List<string>();
                    for (int i = 0; i < int.Parse(masodikresz); i++)
                    {
                        int dobottszam = r.Next(1, 7);
                        szamok.Add(dobottszam.ToString());
                    }
                    if (int.Parse(masodikresz) > 10)
                    {
                        eb.WithColor(Discord.Color.Red);
                        eb.WithAuthor(context.Message.Author);
                        eb.AddField(":game_die: Hiba!", "A dobások száma maximum 10 lehet!", false);
                        await context.Channel.SendMessageAsync("", false, eb.Build());
                    }
                    else if (int.Parse(masodikresz) == 1)
                    {
                        eb.WithAuthor(context.Message.Author);
                        eb.WithColor(Discord.Color.Green);
                        eb.AddField($":game_die: A dobott számod: **{string.Join(", ", szamok)}**", $"Dobások száma: {masodikresz}", false);
                        await context.Channel.SendMessageAsync("", false, eb.Build());
                    }
                    else
                    {
                        eb.WithAuthor(context.Message.Author);
                        eb.WithColor(Discord.Color.Green);
                        eb.AddField($":game_die: A dobott számaid: **{string.Join(", ", szamok)}**", $"Dobások száma: {masodikresz}", false);
                        await context.Channel.SendMessageAsync("", false, eb.Build());
                    }
                }
                catch
                {

                }
            }
            if (parancs == ".meno")
            {
                try
                {
                    masodikresz = tomb[1];
                    Random meno = new Random();
                    int menos = meno.Next(1, 100);
                    //bool ures = string.IsNullOrEmpty(masodikresz);
                    if (szoveg == ".meno <@!790265348053532732>")
                    {
                        _ = context.Message.Channel.SendMessageAsync($">>> {masodikresz} 100%-ban menő :sunglasses:");
                    }
                    else
                    {
                        Console.WriteLine($"{masodikresz}");
                        _ = context.Message.Channel.SendMessageAsync($">>> {masodikresz} {menos}%-ban menő :sunglasses:");
                    }

                }
                catch
                {

                }
            }
            if (parancs == ".in")
            {
                try
                {
                    masodikresz = tomb[1];
                    Random i = new Random();
                    int ii = i.Next(1, 20);
                    if (ii <= 10)
                    {
                        await arg.Channel.SendMessageAsync($">>> Nem");
                    }
                    else
                    {
                        await arg.Channel.SendMessageAsync($">>> Igen");
                    }

                }
                catch
                {

                }
            }
            if (parancs == ".kocka")
            {
                try
                {
                    masodikresz = tomb[1];
                    Random i = new Random();
                    int dobas = i.Next(1, 7);
                    string kihivott = string.Empty;
                    string kihivastkezdte = string.Empty;
                    string user = string.Empty;
                    if (masodikresz.Length != 0 && masodikresz.Contains("@"))
                    {
                        File.WriteAllText("kockakihivas.txt", $"{context.Message.Author.Username},{masodikresz}");
                        List<adatok> lista = new List<adatok>();
                        foreach (var item in File.ReadAllLines("kickakihivas.txt"))
                        {
                            adatok Adatok = new adatok(item);
                            lista.Add(Adatok);
                            kihivott = Adatok.kihivott;
                            kihivastkezdte = Adatok.elso;
                            user = $"@{context.Message.Author.Username}";
                        }
                        await context.Channel.SendMessageAsync($">>> :game_die:\nSikeresen kihívtad {masodikresz}-t.\n A te dobásod: {dobas}\nVárom {masodikresz} dobását.");
                        await context.Channel.SendMessageAsync($"{kihivastkezdte}, {masodikresz}");
                    }
                }
                catch
                {

                }

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
        }
    }
}
