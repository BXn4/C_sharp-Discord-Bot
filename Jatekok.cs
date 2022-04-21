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
using MySql.Data;
using MySql.Data.MySqlClient;

namespace BenCMDSdc
{
    public class Jatekok : ModuleBase<SocketCommandContext>
    {
        [Command("szam")]
        public async Task szamkitalalo()
        {
            Random szam = new Random();
            int gondoltszam = szam.Next(1, 25);
            if (gondoltszam % 2 == 0)
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
        [Command("kocka")]
        public async Task dobokocka()
        {
            var dobasok = new SelectMenuBuilder().WithPlaceholder("Kérlek válassz!").WithCustomId("dobas").WithMinValues(1).WithMaxValues(1).AddOption("Egy", "1", "Egy kockával szeretnék dobni.").AddOption("Kettő", "2", "Két kockával szeretnék dobni.").AddOption("Három", "3", "Három kockával szeretnék dobni").AddOption("Négy", "4", "Négy kockával szeretnék dobni");
            var dobasselect = new ComponentBuilder().WithSelectMenu(dobasok);
            await ReplyAsync($">>> Hány kockával szeretnél dobni? {Context.Message.Author.Mention}", components: dobasselect.Build());
            Context.Client.SelectMenuExecuted += dobas;
        }
        public async Task dobas(SocketMessageComponent arg)
        {
            var dobasokszama = string.Join(", ", arg.Data.Values);
            _ = arg.Message.DeleteAsync();
            Random kocka = new Random();
            int elsodobas = kocka.Next(1, 7);
            int masodikdobas = kocka.Next(1, 7);
            int harmadikdobas = kocka.Next(1, 7);
            int negyedikdobas = kocka.Next(1, 7);
            int botelsodobas = kocka.Next(1, 7);
            int botmasodikdobas = kocka.Next(1, 7);
            int botharmadikdobas = kocka.Next(1, 7);
            int botnegyedikdobas = kocka.Next(1, 7);
            if (dobasokszama == "1")
            {
                var dobasok = new SelectMenuBuilder().WithPlaceholder("Kérlek válassz!").WithCustomId("dobas").WithMinValues(1).WithMaxValues(1).AddOption("Egy", "1", "Egy kockával szeretnék dobni.").AddOption("Kettő", "2", "Két kockával szeretnék dobni.").AddOption("Három", "3", "Három kockával szeretnék dobni").AddOption("Négy", "4", "Négy kockával szeretnék dobni");
                var dobasselect = new ComponentBuilder().WithSelectMenu(dobasok);
                //parancs = "insert into student values";
                dobasokszama = "0";
                if (elsodobas > botelsodobas)
                {
                        /*MySqlConnection csatlakozas = new MySqlConnection("server=localhost;database=bencmds;username=root;password=root");
                        MySqlCommand cmd = null;
                        string parancs = "";
                        csatlakozas.Open();
                        //MySqlCommand kereses = new MySqlCommand($"SELECT COUNT(felhasznalo) FROM pontok WHERE felhasznalo LIKE '{Context.Message.Author.Username}';");
                        string kereses = $"SELECT COUNT(felhasznalo) FROM pontok AS db WHERE felhasznalo LIKE '{Context.Message.Author.Username}';";
                        string van = "";
                        MySqlCommand par = new MySqlCommand(kereses, csatlakozas);
                        using (MySqlDataReader olvas = par.ExecuteReader())
                        {
                            MySqlDataReader olvas2 = cmd.ExecuteReader();
                            olvas2.Read();
                            van = olvas["db"].ToString();
                            if (van == "0")
                                {
                                    Console.WriteLine("nincs még");
                                    await Context.Channel.SendMessageAsync("nincs");
                                }
                                else if (van == "1")
                                {

                                }
                                csatlakozas.Close();
                                */
                                await arg.RespondAsync($">>> :game_die:\nA dobott számod:  **{elsodobas}**\nAz én dobásom: \t**{botelsodobas}**\nEzt a kört te nyerted.");
                    
                }
                if (elsodobas < botelsodobas)
                {
                    await arg.RespondAsync($">>> :game_die:\nA dobott számod:  **{elsodobas}**\nAz én dobásom: \t**{botelsodobas}**\nEzt a kört én nyertem. Sok szerencsét a következő játékhoz!");
                }
                if (elsodobas == botelsodobas)
                {
                    await arg.RespondAsync($">>> :game_die:\nA dobott számod:  **{elsodobas}**\nAz én dobásom: \t**{botelsodobas}**\nEgyenlő. Dobsz megint?", components: dobasselect.Build());
                }
            }
            else if (dobasokszama == "2")
            {
                var dobasok = new SelectMenuBuilder().WithPlaceholder("Kérlek válassz!").WithCustomId("dobas").WithMinValues(1).WithMaxValues(1).AddOption("Egy", "1", "Egy kockával szeretnék dobni.").AddOption("Kettő", "2", "Két kockával szeretnék dobni.").AddOption("Három", "3", "Három kockával szeretnék dobni").AddOption("Négy", "4", "Négy kockával szeretnék dobni");
                var dobasselect = new ComponentBuilder().WithSelectMenu(dobasok);
                dobasokszama = "0";
                int eredmeny = elsodobas + masodikdobas;
                int boteredmeny = botelsodobas + botmasodikdobas;
                if (eredmeny > boteredmeny)
                {
                    await arg.RespondAsync($">>> :game_die: :game_die:\nA dobott számaid: **{elsodobas}, {masodikdobas}**\nÖsszege: **{eredmeny}**\nAz én dobásaim: **{botelsodobas}, {botmasodikdobas}**\nÖsszege: **{boteredmeny}**\nEzt a kört te nyerted.");
                }
                if (eredmeny < boteredmeny)
                {
                    await arg.RespondAsync($">>> :game_die: :game_die:\nA dobott számaid: **{elsodobas}, {masodikdobas}**\nÖsszege: **{eredmeny}**\nAz én dobásaim: **{botelsodobas}, {botmasodikdobas}**\nÖsszege: **{boteredmeny}**\nEzt a kört én nyertem. Sok szerencsét a következő játékhoz!");
                }
                if (eredmeny == boteredmeny)
                {
                    await arg.RespondAsync($">>> :game_die: :game_die:\nA dobott számaid: **{elsodobas}, {masodikdobas}**\nÖsszege: **{eredmeny}**\nAz én dobásaim: **{botelsodobas}, {botmasodikdobas}**\nÖsszege: **{boteredmeny}**\nEgyenlő. Dobsz megint?", components: dobasselect.Build());

                }
            }
            else if (dobasokszama == "3")
            {
                var dobasok = new SelectMenuBuilder().WithPlaceholder("Kérlek válassz!").WithCustomId("dobas").WithMinValues(1).WithMaxValues(1).AddOption("Egy", "1", "Egy kockával szeretnék dobni.").AddOption("Kettő", "2", "Két kockával szeretnék dobni.").AddOption("Három", "3", "Három kockával szeretnék dobni").AddOption("Négy", "4", "Négy kockával szeretnék dobni");
                var dobasselect = new ComponentBuilder().WithSelectMenu(dobasok);
                dobasokszama = "0";
                int eredmeny = elsodobas + masodikdobas + harmadikdobas;
                int boteredmeny = botelsodobas + botmasodikdobas + botharmadikdobas;
                if (eredmeny > boteredmeny)
                {
                    await arg.RespondAsync($">>> :game_die: :game_die: :game_die:\nA dobott számaid: **{elsodobas}, {masodikdobas}, {harmadikdobas}**\nÖsszege: **{eredmeny}**\nAz én dobásaim: **{botelsodobas}, {botmasodikdobas}, {botharmadikdobas}**\nÖsszege: **{boteredmeny}**\nEzt a kört te nyerted.");
                }
                if (eredmeny < boteredmeny)
                {
                    await arg.RespondAsync($">>> :game_die: :game_die: :game_die:\nA dobott számaid: **{elsodobas}, {masodikdobas}, {harmadikdobas}**\nÖsszege: **{eredmeny}**\nAz én dobásaim: **{botelsodobas}, {botmasodikdobas}, {botharmadikdobas}**\nÖsszege: **{boteredmeny}**\nEzt a kört én nyertem. Sok szerencsét a következő játékhoz!");
                }
                if (eredmeny == boteredmeny)
                {
                    await arg.RespondAsync($">>> :game_die: :game_die: :game_die:\nA dobott számaid: **{elsodobas}, {masodikdobas}, {harmadikdobas}**\nÖsszege: **{eredmeny}**\nAz én dobásaim: **{botelsodobas}, {botmasodikdobas}, {botharmadikdobas}**\nÖsszege: **{boteredmeny}**\nEgyenlő. Dobsz megint?", components: dobasselect.Build());

                }
            }
            else if (dobasokszama == "4")
            {
                var dobasok = new SelectMenuBuilder().WithPlaceholder("Kérlek válassz!").WithCustomId("dobas").WithMinValues(1).WithMaxValues(1).AddOption("Egy", "1", "Egy kockával szeretnék dobni.").AddOption("Kettő", "2", "Két kockával szeretnék dobni.").AddOption("Három", "3", "Három kockával szeretnék dobni").AddOption("Négy", "4", "Négy kockával szeretnék dobni");
                var dobasselect = new ComponentBuilder().WithSelectMenu(dobasok);
                dobasokszama = "0";
                int eredmeny = elsodobas + masodikdobas + harmadikdobas + negyedikdobas;
                int boteredmeny = botelsodobas + botmasodikdobas + botharmadikdobas + botnegyedikdobas;
                if (eredmeny > boteredmeny)
                {
                    await arg.RespondAsync($">>> :game_die: :game_die: :game_die: :game_die:\nA dobott számaid: **{elsodobas}, {masodikdobas}, {harmadikdobas}, {negyedikdobas}**\nÖsszege: **{eredmeny}**\nAz én dobásaim: **{botelsodobas}, {botmasodikdobas}, {botharmadikdobas}, {botnegyedikdobas}**\nÖsszege: **{boteredmeny}**\nEzt a kört te nyerted.");
                }
                if (eredmeny < boteredmeny)
                {
                    await arg.RespondAsync($">>> :game_die: :game_die: :game_die: :game_die:\nA dobott számaid: **{elsodobas}, {masodikdobas}, {harmadikdobas}, {negyedikdobas}**\nÖsszege: **{eredmeny}**\nAz én dobásaim: **{botelsodobas}, {botmasodikdobas}, {botharmadikdobas}, {botnegyedikdobas}**\nÖsszege: **{boteredmeny}**\nEzt a kört én nyertem. Sok szerencsét a következő játékhoz!");
                }
                if (eredmeny == boteredmeny)
                {
                    await arg.RespondAsync($">>> :game_die: :game_die: :game_die: :game_die:\nA dobott számaid: **{elsodobas}, {masodikdobas}, {harmadikdobas}**\nÖsszege: **{eredmeny}**\nAz én dobásaim: **{botelsodobas}, {botmasodikdobas}, {botharmadikdobas}, {botnegyedikdobas}**\nÖsszege: **{boteredmeny}**\nEgyenlő. Dobsz megint?", components: dobasselect.Build());
                }
            }
        }
    }
}
