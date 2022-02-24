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
            var gomb = new ComponentBuilder().WithButton("+", "0", ButtonStyle.Primary);
            await ReplyAsync($"Helló {Context.Message.Author.Mention}! :wave:", components: gomb.Build());
        }
        [Command("play", RunMode= RunMode.Async)]
        public async Task hang(IVoiceChannel hang = null)
        {
            hang = hang ?? (Context.User as IGuildUser)?.VoiceChannel;
            if (hang == null)
            {
                await Context.Channel.SendMessageAsync("Ajjaj! Ahhoz, hogy csatlakozni tudjak a hang csatornához, ahhoz bent kell lenned egy audiócsatornában. Csatlakozz az egyikbe, majd használd újra a parancsot!");
            }

            var hangkliens = await hang.ConnectAsync();
        }
    
    } 

}
