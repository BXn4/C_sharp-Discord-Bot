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

namespace BenCMDSdc
{
    public class Parancsok : ModuleBase<SocketCommandContext>
    {
        int voicekszama = 0;
        int i = 0;
        private Timer _timer = null;
        [Command("szia")]
        public async Task koszones()
        {
            Emoji integetes = new Emoji("👋");
            await ReplyAsync($"Helló {Context.Message.Author.Mention}! :wave:");
            _ = Context.Message.AddReactionAsync(integetes);
        }
        [Command("xdrang")]
        public async Task xdrang()
        {
            Emote fortnite = Emote.Parse("<:fortnite:952991572360847420>");
            Emote lol = Emote.Parse("<:lol:952998853433511986>");
            Emote osu = Emote.Parse("<:osu:952998814296453221>");
            Emote pubg = Emote.Parse("<:pubg:952999671612178472>");
            Emote minecraft = Emote.Parse("<:minecraft:952997746162425886>");
            Emote mw = Emote.Parse("<:mw:952999071696707715>");
            Emote roblox = Emote.Parse("<:roblox:953000756255014912>");
            Emote warthunder = Emote.Parse("<:war_thunder:953000769639030784>");
            Emote csgo = Emote.Parse("<:csgo:953000789490688090>");
            var gomb = new ComponentBuilder();
            string uzenet = $"```Milyen játékokkal szoktál játszani?\n(Reagálj és hozzáférést kapsz a kiválaszott játék csatornájához!).```";
            var elkuld = await Context.Channel.SendMessageAsync(uzenet);
            await elkuld.AddReactionAsync(fortnite);
            await elkuld.AddReactionAsync(lol);
            await elkuld.AddReactionAsync(minecraft);
            await elkuld.AddReactionAsync(roblox);
            await elkuld.AddReactionAsync(mw);
            await elkuld.AddReactionAsync(csgo);
            await elkuld.AddReactionAsync(pubg);
            await elkuld.AddReactionAsync(osu);
            await elkuld.AddReactionAsync(warthunder);


        }
        [Command("parancsok")]
        public async Task parancsok()
        {
            await Context.User.SendMessageAsync(">>> **Hangcsatornák:**\n`.vc create\n.vc delete\nés még stb... `\n\n**Szöveges csatornák:**\n`.tc create\n.tc del\nés még stb... `\n\n**FUN parancsok:**\n`.szia ` \n\n `Frissítve: 2022.03.17 || 18:54`");
        }
        [Command("vc create")]
        public async Task voice_create()
        {
            //File.AppendAllText("voicek.txt", $"{Context.User.Username},{voicekszama}\n");
            //List<adatok> lista = new List<adatok>();
            /*foreach (var item in File.ReadAllLines("voicek.txt"))
            {
                adatok adatok = new adatok(item);
                lista.Add(adatok);
                if(Context.User.Username == adatok.nevtxt)
                {
                    voicekszama = adatok.voicektxt;
                    if(voicekszama == 0)
                    {
                        voicekszama++;
                        File.AppendAllText("voicek.txt", $"{Context.User.Username},{voicekszama}\n");
                        var csatornanev = Context.User.Username;
                        await Context.Guild.CreateVoiceChannelAsync(csatornanev + " csatornája");
                        await ReplyAsync($"Sikeresen létrehoztam a csatornát. A csatorna neve: ```{ csatornanev} csatornája```\n További parancsok a csatornához a .voice paranccsal érhetőek el.");
                    }
                    else
                    {
                        var csatornanev = Context.User.Username;
                        await ReplyAsync($"Ajaj! Neked már van voice csatornád a szerveren. A csatornád neve ```{csatornanev} csatornája```. Ha törölni szeretnéd, akkor használd a ```.vc del``` parancsot!");
                    }
                }
                
            }
            */
            var csatorna = Context.Guild.Channels.SingleOrDefault(x => x.Name == Context.User.Username +" csatornája");
            if (csatorna == null)
            {
                var csatornanev = Context.User.Username;
                await Context.Guild.CreateVoiceChannelAsync(csatornanev + " csatornája");
                await ReplyAsync($">>> :white_check_mark: {Context.Message.Author.Mention}```Sikeresen létrehoztam a csatornát.\nA csatorna neve: {csatornanev} csatornája.\nTovábbi parancsok a csatornához a [.voice] paranccsal érhetőek el. ```");
            }
            else
            {
                var csatornanev = Context.User.Username;
                await ReplyAsync($">>> :bangbang: {Context.Message.Author.Mention}```Neked már van csatornád.\nA csatornád neve: {csatorna}.\nHa törölni szeretnéd, akkor használd a [.vc del] parancsot!```");
            }

        }
        [Command("vc del")]
        public async Task voice_delete()
        {
            var csatorna = Context.Guild.Channels.SingleOrDefault(x => x.Name == Context.User.Username + " csatornája");
            SocketGuildChannel sgc = csatorna;
            if (csatorna != null)
            {
                await ReplyAsync($">>> :ok_hand: {Context.Message.Author.Mention}```Sikeresen töröltem a {csatorna} nevű csatornát.```");
                _ = sgc.DeleteAsync(); 
            }
        }
        [Command("tc create")]
        public async Task text_create()
        {
            var csatorna = Context.Guild.Channels.SingleOrDefault(x => x.Name == Context.User.Username + "-szöveges-csatornája");
            if (csatorna == null)
            {
                var csatornanev = Context.User.Username;
                await Context.Guild.CreateTextChannelAsync(csatornanev + "-szöveges-csatornája");
                await ReplyAsync($">>> :white_check_mark: {Context.Message.Author.Mention}```Sikeresen létrehoztam a szöveges csatornát.\nA csatorna neve: {csatornanev}-szöveges-csatornája.\nTovábbi parancsok a csatornához a [.text] paranccsal érhetőek el. ```");
            }
            else
            {
                var csatornanev = Context.User.Username;
                await ReplyAsync($">>> :bangbang: {Context.Message.Author.Mention}```Neked már van szöveges csatornád.\nA csatornád neve: {csatorna}.\nHa törölni szeretnéd, akkor használd a [.tc del] parancsot!```");
            }

        }
        [Command("tc del")]
        public async Task text_delete()
        {
            var csatorna = Context.Guild.Channels.SingleOrDefault(x => x.Name == Context.User.Username + "-szöveges-csatornája");
            SocketGuildChannel sgc = csatorna;
            if (csatorna != null)
            {
                await ReplyAsync($">>> :ok_hand: {Context.Message.Author.Mention}```Sikeresen töröltem a {csatorna} nevű szöveges csatornát.```");
                _ = sgc.DeleteAsync();
            }
        }

        [Command("szam")]
        public async Task szamkitalalo()
        {
            Random szam = new Random();
            int gondoltszam = szam.Next(1, 25);
            if(gondoltszam % 2 == 0)
            {
                //var paroszamok = new SelectMenuBuilder().WithPlaceholder("Kérlek válassz!").WithCustomId("parosok").AddOption("2").AddOption
              //  var paros = new ComponentBuilder().WithSelectMenu(paroszamok);
               // await ReplyAsync($">>> :brain: \n Gondoltam egy páros számra 1 és 25 között.\n Vajon melyik számra gondoltam?", components: paros.Build());
            }
            else
            {
                var paratlanszamok = new SelectMenuBuilder().WithPlaceholder("Kérlek válassz!").WithCustomId("paratlanok").WithMinValues(1).WithMaxValues(3).AddOption("1", "3", "5").AddOption("7", "9", "11").AddOption("13", "15", "17").AddOption("19", "21", "23");
                var paratlan = new ComponentBuilder().WithSelectMenu(paratlanszamok);
                await ReplyAsync($">>> :brain: \n Gondoltam egy páratlan számra 1 és 25 között.\n Vajon melyik számra gondoltam?", components: paratlan.Build());
            }

        }
        [Command("play", RunMode= RunMode.Async)]
        public async Task hang(IVoiceChannel hang = null)
        {
            hang = hang ?? (Context.User as IGuildUser)?.VoiceChannel;
            if (hang == null)
            {
                await Context.Channel.SendMessageAsync("```Ajjaj! Ahhoz, hogy csatlakozni tudjak a hang csatornához, ahhoz bent kell lenned egy audiócsatornában. Csatlakozz az egyikbe, majd használd újra a parancsot!```");
            }

            var hangkliens = await hang.ConnectAsync();
        }
    
    } 

}
