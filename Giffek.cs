using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using Discord.Audio;
using System.IO;
using Discord.WebSocket;
using System.Threading;
using System.IO;
using System.Net;
using System.Web.Helpers;
using Newtonsoft.Json;
using Microsoft.Ajax.Utilities;
using Microsoft.Extensions.Configuration;

namespace BenCMDSdc
{
    public class Giffek : ModuleBase<SocketCommandContext>
    {
        [Command("gif")]
        public async Task gif()
        {
            string url = ($"https://api.tenor.com/v1/search?q=alma&key=3&ContentFilter=high");
            var json = new WebClient().DownloadString($"{url}");
            File.WriteAllText("giftarolo.json", $"{json}");
            read();

        }
        public struct config
        {

            [JsonProperty("url")]
            public string url { get; set; }
        }
        public async Task read()
        {
            var json = string.Empty;
            using (var fs = File.OpenRead("giftarolo.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
            json = await sr.ReadToEndAsync().ConfigureAwait(false);
            var configjson = JsonConvert.DeserializeObject<config>(json);
            var config = new config()
            {
                url = configjson.url,

            };
            string gifurl = string.Empty;
            gifurl = config.url;
            Console.WriteLine($"{gifurl}");
        }

    }
}