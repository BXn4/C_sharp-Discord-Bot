using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using Discord.Audio;
namespace BenCMDSdc
{
    public class Parancsok : ModuleBase<SocketCommandContext>
    {
        [Command("szia")]
        public async Task koszones()
        {
            Emoji integetes = new Emoji("👋");
            await ReplyAsync($"Helló {Context.Message.Author.Mention}! :wave:");
            Context.Message.AddReactionAsync(integetes);
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
