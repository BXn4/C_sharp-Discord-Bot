using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using Discord.WebSocket;
using System.Drawing;


namespace BenCMDSdc
{
    public class Parancsok : ModuleBase<SocketCommandContext>
    {
        // // // // // // //
        #region koszones
        [Command("szia")]
        public async Task koszones()
        {
            Emoji integetes = new Emoji("👋");
            await ReplyAsync($"Helló {Context.Message.Author.Mention}! :wave:");
            _ = Context.Message.AddReactionAsync(integetes);
        }
        #endregion
        // // // // // // //

        // // // // // // //
        #region jatekok teszt
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
        #endregion // **
        // // // // // // //

        // // // // // // //
        #region parancs:parancsok
        [Command("parancsok")]
        public async Task parancsok()
        {
            EmbedBuilder parancsokb = new EmbedBuilder();
            parancsokb.WithTitle("Az alábbi parancsok érhetőek el:");
            parancsokb.AddField("A parancs leírása: ", "`.parancsok [.parancs]`", false);
            parancsokb.AddField("Csatornák parancsai", "`.vc`\n`.tc`", false);
            parancsokb.AddField("Játékok", "`.kocka`", true);
            parancsokb.AddField("FUN", "`.szia`\n`.in`\n`.meme`", true);
            parancsokb.WithColor(Discord.Color.Orange);
            parancsokb.WithThumbnailUrl("https://stream.data.hu/get/13237441/fogaskerek.png");
            await Context.User.SendMessageAsync("", false, parancsokb.Build());
            EmbedBuilder elkuldtem = new EmbedBuilder();
            await Context.Message.DeleteAsync();
            elkuldtem.WithAuthor(Context.Message.Author);
            elkuldtem.WithColor(Discord.Color.Green);
            elkuldtem.AddField(":white_check_mark: Az elérhető parancsokat elküldtem privátban.", ".parancsok", false);
            await Context.Channel.SendMessageAsync("", false, elkuldtem.Build());
        }
        #endregion
        // // // // // // //

        // // // // // // //
        #region parancs:voice letrehozasa
        [Command("vc create")]
        public async Task voice_create()
        {
            var csatorna = Context.Guild.Channels.SingleOrDefault(x => x.Name == Context.User.Username + " csatornája");
            if (csatorna == null)
            {
                var csatornanev = Context.User.Username;
                await Context.Guild.CreateVoiceChannelAsync(csatornanev + " csatornája");
                EmbedBuilder sikeres = new EmbedBuilder();
                sikeres.AddField($"Sikeresen létrehoztam a csatornát.\nA csatorna neve: {csatornanev} csatornája.", "További parancsok a csatornához a [.vc] paranccsal érhetőek el.", true);
                sikeres.WithColor(Discord.Color.Green);
                sikeres.WithAuthor(Context.Message.Author);
                await Context.Channel.SendMessageAsync("", false, sikeres.Build());
            }
            else
            {
                var csatornanev = Context.User.Username;
                EmbedBuilder hibas = new EmbedBuilder();
                hibas.AddField($"Neked már van csatornád.\nA csatornád neve: {csatornanev} csatornája.", "Ha törölni szeretnéd, akkor használd a [.vc del] parancsot!", true);
                hibas.WithColor(Discord.Color.Red);
                hibas.WithAuthor(Context.Message.Author);
                await Context.Channel.SendMessageAsync("", false, hibas.Build());
            }

        }
        #endregion
        // // // // // // // 

        // // // // // // //
        #region parancs:voice torlese
        [Command("vc del")]
        public async Task voice_delete()
        {
            var csatorna = Context.Guild.Channels.SingleOrDefault(x => x.Name == Context.User.Username + " csatornája");
            SocketGuildChannel sgc = csatorna;
            if (csatorna != null)
            {
                EmbedBuilder kesz = new EmbedBuilder();
                kesz.AddField($":ok_hand:\nSikeresen töröltem a csatornád.", "Ha szeretnél újat csinálni, akkor használd a [.vc create] parancsot.", true);
                kesz.WithColor(Discord.Color.Green);
                kesz.WithAuthor(Context.Message.Author);
                await Context.Channel.SendMessageAsync("", false, kesz.Build());
                _ = sgc.DeleteAsync();
            }
        }
        #endregion
        // // // // // // //

        // // // // // // //
        #region parancs:szoveges csatorna letrehozasa
        [Command("tc create")]
        public async Task text_create()
        {
            var csatorna = Context.Guild.Channels.SingleOrDefault(x => x.Name == Context.User.Username + "-szöveges-csatornája");
            if (csatorna == null)
            {
                var csatornanev = Context.User.Username;
                await Context.Guild.CreateTextChannelAsync(csatornanev + "-szöveges-csatornája");
                EmbedBuilder sikeres = new EmbedBuilder();
                sikeres.AddField($"Sikeresen létrehoztam a szöveges csatornát.\nA csatorna neve: {csatornanev}-szöveges-csatornája.", "További parancsok a csatornához a [.tc] paranccsal érhetőek el.", true);
                sikeres.WithColor(Discord.Color.Green);
                sikeres.WithAuthor(Context.Message.Author);
                await Context.Channel.SendMessageAsync("", false, sikeres.Build());
            }
            else
            {
                var csatornanev = Context.User.Username;
                EmbedBuilder hibas = new EmbedBuilder();
                hibas.AddField($"Neked már szöveges van csatornád.\nA csatornád neve: {csatornanev} csatornája.", "Ha törölni szeretnéd, akkor használd a [.tc del] parancsot!", true);
                hibas.WithColor(Discord.Color.Red);
                hibas.WithAuthor(Context.Message.Author);
                await Context.Channel.SendMessageAsync("", false, hibas.Build());
            }

        }
        #endregion
        // // // // // // //

        // // // // // // //
        #region parancs:szoveges csatorna torlese
        [Command("tc del")]
        public async Task text_delete()
        {
            var csatorna = Context.Guild.Channels.SingleOrDefault(x => x.Name == Context.User.Username + "-szöveges-csatornája");
            SocketGuildChannel sgc = csatorna;
            if (csatorna != null)
            {
                EmbedBuilder kesz = new EmbedBuilder();
                kesz.AddField($":ok_hand:\nSikeresen töröltem a csatornád.", "Ha szeretnél újat csinálni, akkor használd a [.tc create] parancsot.", true);
                kesz.WithColor(Discord.Color.Green);
                kesz.WithAuthor(Context.Message.Author);
                await Context.Channel.SendMessageAsync("", false, kesz.Build());
                _ = sgc.DeleteAsync();
                
            }
        }
        #endregion
        // // // // // // //

        // // // // // // //
        #region parancsok leirasa
        [Command("vc")]
        public async Task vc_parancsai()
        {
            EmbedBuilder eb = new EmbedBuilder();
            eb.AddField($"Voice csatornákra jellemző parancsok:", "`.vc create`\n`.vc del`", true);
            eb.AddField($"Leírás", "Csatorna létrehozása.\nCsatorna törlése.", true);
            eb.WithColor(Discord.Color.Blue);
            await Context.Channel.SendMessageAsync("", false, eb.Build());
        }
        [Command("tc")]
        public async Task tc_parancsai()
        {
            EmbedBuilder eb = new EmbedBuilder();
            eb.AddField($"Szöveges csatornákra jellemző parancsok:", "`.tc create`\n`.tc del`", true);
            eb.AddField($"Leírás", "Csatorna létrehozása.\nCsatorna törlése.", true);
            eb.WithColor(Discord.Color.Blue);
            await Context.Channel.SendMessageAsync("", false, eb.Build());
        }
        [Command("parancsok .parancsok")]
        public async Task parancsokparancsaxd()
        {
            EmbedBuilder eb = new EmbedBuilder();
            eb.AddField($"A .parancsok parancs leírása:", "Az elérhető parancsok listája.", true);
            eb.WithColor(Discord.Color.Blue);
            await Context.Channel.SendMessageAsync("", false, eb.Build());
        }
        [Command("parancsok .vc")]
        public async Task voicekparancsa()
        {
            EmbedBuilder eb = new EmbedBuilder();
            eb.AddField($"A .vc parancs leírása:", "A voice csatornákra jellemző parancsok listája.\n Használat: `.vc [utasítás]`", true);
            eb.WithColor(Discord.Color.Blue);
            await Context.Channel.SendMessageAsync("", false, eb.Build());
        }
        [Command("parancsok .tc")]
        public async Task textchannelparancsoks()
        {
            EmbedBuilder eb = new EmbedBuilder();
            eb.AddField($"A .tc parancs leírása:", "A szöveges csatornákra jellemző parancsok listája.\n Használat: `.tc [utasítás]`", true);
            eb.WithColor(Discord.Color.Blue);
            await Context.Channel.SendMessageAsync("", false, eb.Build());
        }
        [Command("parancsok .kocka")]
        public async Task kockaparancs()
        {
            EmbedBuilder eb = new EmbedBuilder();
            eb.AddField($"A .kocka parancs leírása:", "Dobj egyszer, vagy négyszer a dobókockával.\n Használat:\n `.kocka` (Ha bot ellen szerenél játszani)\n `.kocka [dobások]` \n `.kocka [megemlített felhasználó]`", true);
            eb.WithColor(Discord.Color.Blue);
            await Context.Channel.SendMessageAsync("", false, eb.Build());
        }
        #endregion
        // // // // // // //

        [Command("uwu")]
        public async Task uwu()
        {
             Random uwu = new Random();
             int uwus = uwu.Next(1, 100);
             if (Context.Message.Author.Username == "kaka89")
             {
                 await ReplyAsync($">>> :regional_indicator_u: :regional_indicator_w: :regional_indicator_u:\n {Context.Message.Author.Mention} 101%-ban uwu :):3");
             }
             else
             {
                 await ReplyAsync($">>> :regional_indicator_u: :regional_indicator_w: :regional_indicator_u:\n {Context.Message.Author.Mention} {uwus}%-ban uwu :):3");
             } 
        }

        [Command("szeretlek")]
        public async Task asd()
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=dQw4w9WgXcQ%22");
        }
        // // // // // // //
        #region meme
        [Command("meme")]
        public async Task meme()
        {
            string elsoszoveg = string.Empty;
            string masodikszoveg = string.Empty;
            string felhasznev = $"{Context.Message.Author.Username}";
            PointF elsohely = new PointF(120f, 350f);
            PointF masodikhely = new PointF(520f, 375f);
            PointF felhaszhely = new PointF(280f, 210f);
            string kephelye = string.Empty;
            Random memerandom = new Random();
            int memeszam = memerandom.Next(1, 3);
            Random memeszovegrandom = new Random();
            if (memeszam == 1)
            {
                elsohely = new PointF(120f, 350f);
                masodikhely = new PointF(520f, 375f);
                felhaszhely = new PointF(280f, 210f);
                kephelye = @"C:\Users\BencePC\source\repos\BenCMDSdc\BenCMDSdc\bin\Debug\kepek\meme1.jpg";
                var elsomeme = new List<string> { "Youtube", "Fortnite", "Játszás", "NFTK", "TikTok", "Videa", "Netflix" };
                var masodikmeme = new List<string> { "TikTok", "Minecraft", "Iskola", "Bitcoin", "Youtube", "Netflix", "Alvás" };
                int i = memerandom.Next(elsomeme.Count);
            elsoszoveg = (elsomeme[i]);
            masodikszoveg = (masodikmeme[i]);
           }
            if (memeszam == 2)
            {
                elsohely = new PointF(230f, 380f);
                masodikhely = new PointF(520f, 375f);
                felhaszhely = new PointF(30f, 150f);
                kephelye = @"C:\Users\BencePC\source\repos\BenCMDSdc\BenCMDSdc\bin\Debug\kepek\meme2.jfif";
                var elsomeme = new List<string> { "\nKoronavírus","Random\nkisgyerek","Matek\ndolgozat","Alkoholmen-\ntes sör","\nMiki egér", "1 éves romlott\nmekis sajtburesz" };
                string masodikmeme = Context.User.Username;
                int i = memerandom.Next(elsomeme.Count);
                elsoszoveg = (elsomeme[i]);
                masodikszoveg = (masodikmeme);
            }

            Bitmap bitmapn;
            using (var bitmap = (Bitmap)System.Drawing.Image.FromFile(kephelye))
            {
                using (Graphics rajzol = Graphics.FromImage(bitmap))
                {
                    using (Font arialFont = new Font("Arial", 35))
                    {
                        rajzol.DrawString(elsoszoveg, arialFont, Brushes.White, elsohely);
                        rajzol.DrawString(masodikszoveg, arialFont, Brushes.White, masodikhely);
                        rajzol.DrawString(felhasznev, arialFont, Brushes.White, felhaszhely);
                    }
                }
                bitmapn = new Bitmap(bitmap);
            }

            bitmapn.Save($@"C:\Users\BencePC\source\repos\BenCMDSdc\BenCMDSdc\bin\Debug\kepek\memek\meme{Context.Message.Author.Username}meme.jpg");
            _ = Context.Message.Channel.SendFileAsync($@"C:\Users\BencePC\source\repos\BenCMDSdc\BenCMDSdc\bin\Debug\kepek\memek\meme{Context.Message.Author.Username}meme.jpg");
            bitmapn.Dispose();
        }
        #endregion
        // // // // // // //

        // // // // // // //
        #region test
        [Command("play", RunMode = RunMode.Async)]
        public async Task hang(IVoiceChannel hang = null)
        {
            hang = hang ?? (Context.User as IGuildUser)?.VoiceChannel;
            if (hang == null)
            {
                await Context.Channel.SendMessageAsync("```Ajjaj! Ahhoz, hogy csatlakozni tudjak a hang csatornához, ahhoz bent kell lenned egy audiócsatornában. Csatlakozz az egyikbe, majd használd újra a parancsot!```");
            }

            var hangkliens = await hang.ConnectAsync();
        }
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
        #endregion
        // // // // // // //
    }
}
