using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using PoGo.NecroBot.Logic.Common;
using PoGo.NecroBot.Logic.Event;
using PoGo.NecroBot.Logic.Logging;
using PoGo.NecroBot.Logic.State;
using POGOProtos.Enums;
using POGOProtos.Inventory.Item;
using POGOProtos.Networking.Responses;
using Slack.Webhooks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoGo.NecroBot.CLI
{
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    public class slackLoggerListener
    {

        private static string botName = "Francis";
        private static string webhookURL,channel;
        public slackLoggerListener()
        {
            webhookURL = "https://hooks.slack.com/services/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";
            channel = "#nightbot";
            botName = "MrNITE";

        }
        static void TestPostMessage(string message, LogLevel level = LogLevel.Info, ConsoleColor color = ConsoleColor.Black, bool force = false)
        {
            //var context = parseLogContext(level);
            var slackMessage = new SlackMessage
            {
                Channel = channel,
                Text = message,
                IconEmoji = ":nerd_face:",
                Username = botName + "Info"
            };

            SlackAttachment slackAttachment;

            switch (level)
            {
                case LogLevel.Error:
                    slackMessage.Text = "uh oh! I'm in trouble!";
                    slackMessage.IconEmoji = Emoji.Scream;
                    slackMessage.Username = botName + "Error";
                    slackAttachment = new SlackAttachment
                    {
                        Text = message,
                        Color = "#C0392B"
                    };
                    slackMessage.Attachments = new List<SlackAttachment> { slackAttachment };
                    break;
                case LogLevel.Warning:
                    slackMessage.Text = "this is a warning...";
                    slackMessage.IconEmoji = ":thinking_face:";
                    slackMessage.Username = botName + "Warning";
                    slackAttachment = new SlackAttachment
                    {
                        Text = message,
                        Color = "#F4D03F"
                    };
                    slackMessage.Attachments = new List<SlackAttachment> { slackAttachment };
                    break;
                case LogLevel.Info:
                    //Console.ForegroundColor = color == ConsoleColor.Black ? ConsoleColor.DarkCyan : color;
                    //Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] ({LoggingStrings.Info}) {message}");
                    break;
                case LogLevel.Pokestop:
                    slackMessage.Text = "made it to another pokestop!";
                    slackMessage.IconEmoji = Emoji.Blush;
                    slackMessage.Username = botName + "LootingStop";
                    slackAttachment = new SlackAttachment
                    {
                        Text = message,
                        Color = "#3498DB"
                    };
                    slackMessage.Attachments = new List<SlackAttachment> { slackAttachment };
                    break;
                case LogLevel.Farming:
                    //Console.ForegroundColor = color == ConsoleColor.Black ? ConsoleColor.Magenta : color;
                    //Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] ({LoggingStrings.Farming}) {message}");
                    break;
                case LogLevel.Sniper:
                    //Console.ForegroundColor = color == ConsoleColor.Black ? ConsoleColor.White : color;
                    //Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] ({LoggingStrings.Sniper}) {message}");
                    break;
                case LogLevel.Recycling:
                    slackMessage.Text = "";
                    slackMessage.IconEmoji = ":recycle:";
                    slackMessage.Username = botName + "Recycling";
                    slackAttachment = new SlackAttachment
                    {
                        Text = message,
                        Color = "#8E44AD"
                    };
                    slackMessage.Attachments = new List<SlackAttachment> { slackAttachment };
                    break;
                case LogLevel.Caught:
                    slackMessage.Text = "I caught the guy!";
                    slackMessage.IconEmoji = Emoji.Laughing;
                    slackMessage.Username = botName + "Caught";
                    slackAttachment = new SlackAttachment
                    {
                        Text = message,
                        Color = "#2ECC71"
                    };
                    slackMessage.Attachments = new List<SlackAttachment> { slackAttachment };
                    break;
                case LogLevel.Flee:
                    slackMessage.Text = "he's getting away from me!";
                    slackMessage.IconEmoji = ":anguished:";
                    slackMessage.Username = botName + "Struggling";
                    slackAttachment = new SlackAttachment
                    {
                        Text = message,
                        Color = "#F4D03F"
                    };
                    slackMessage.Attachments = new List<SlackAttachment> { slackAttachment };
                    break;
                case LogLevel.Transfer:
                    slackMessage.Text = @"I'm /'transfering/' pokemon. Not putting them in a blender";
                    slackMessage.IconEmoji = Emoji.Smirk;
                    slackMessage.Username = botName + "Transfering";
                    slackAttachment = new SlackAttachment
                    {
                        Text = message,
                        Color = "#186A3B"
                    };
                    slackMessage.Attachments = new List<SlackAttachment> { slackAttachment };
                    break;
                case LogLevel.Evolve:
                    slackMessage.Text = @"Oh shit dat evolve";
                    slackMessage.IconEmoji = Emoji.Frog;
                    slackMessage.Username = botName + "Evolving";
                    slackAttachment = new SlackAttachment
                    {
                        Text = message,
                        Color = "#186A3B"
                    };
                    slackMessage.Attachments = new List<SlackAttachment> { slackAttachment };
                    break;

                case LogLevel.Berry:
                    slackMessage.Text = "Deploying tasty berries";
                    slackMessage.IconEmoji = Emoji.Cherries;
                    slackMessage.Username = botName + "Berries";
                    slackAttachment = new SlackAttachment
                    {
                        Text = message,
                        Color = "#F4D03F"
                    };
                    slackMessage.Attachments = new List<SlackAttachment> { slackAttachment };
                    break;
                case LogLevel.Egg:
                    slackMessage.Text = "I <3 eggs";
                    slackMessage.IconEmoji = Emoji.KissingSmilingEyes;
                    slackMessage.Username = botName + "EggKeeping";
                    slackAttachment = new SlackAttachment
                    {
                        Text = message,
                        Color = "#F4D03F"
                    };
                    slackMessage.Attachments = new List<SlackAttachment> { slackAttachment };
                    break;
                case LogLevel.Debug:
                    //Console.ForegroundColor = color == ConsoleColor.Black ? ConsoleColor.Gray : color;
                    //Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] ({LoggingStrings.Debug}) {message}");
                    break;
                case LogLevel.Update:
                    //Console.ForegroundColor = color == ConsoleColor.Black ? ConsoleColor.White : color;
                    //Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] ({LoggingStrings.Update}) {message}");
                    break;
                case LogLevel.New:
                    //Console.ForegroundColor = color == ConsoleColor.Black ? ConsoleColor.Green : color;
                    //Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] ({LoggingStrings.New}) {message}");
                    break;
                case LogLevel.SoftBan:
                    slackMessage.Text = "SOFT BAN DETECTED";
                    slackMessage.IconEmoji = Emoji.Confounded;
                    slackMessage.Username = botName + "SoftBanned";
                    slackAttachment = new SlackAttachment
                    {
                        Text = message,
                        Color = "#C0392B"
                    };
                    slackMessage.Attachments = new List<SlackAttachment> { slackAttachment };
                    break;
                case LogLevel.LevelUp:
                    slackMessage.Text = "The XP must flow";
                    slackMessage.IconEmoji = Emoji.Star;
                    slackMessage.Username = botName + "LeveledUp";
                    slackAttachment = new SlackAttachment
                    {
                        Text = message,
                        Color = "#186A3B"
                    };
                    slackMessage.Attachments = new List<SlackAttachment> { slackAttachment };
                    break;

                default:
                    //Console.ForegroundColor = color == ConsoleColor.Black ? ConsoleColor.White : color;
                    //Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] ({LoggingStrings.Error}) {message}");
                    break;
            }

            var slackClient = new SlackClient(webhookURL);
            slackClient.Post(slackMessage);
        }
        static void TestPostMessage(SlackMessage message, LogLevel level = LogLevel.Info, ConsoleColor color = ConsoleColor.Black, bool force = false)
        {
            var slackClient = new SlackClient(webhookURL);
            slackClient.Post(message);
        }

        private static void HandleEvent(ProfileEvent profileEvent, ISession session)
        {
            TestPostMessage(session.Translation.GetTranslation(TranslationString.EventProfileLogin,
                profileEvent.Profile.PlayerData.Username ?? ""));
        }

        private static void HandleEvent(ErrorEvent errorEvent, ISession session)
        {
            TestPostMessage(errorEvent.ToString(), LogLevel.Error, force: true);
        }

        private static void HandleEvent(NoticeEvent noticeEvent, ISession session)
        {
            TestPostMessage(noticeEvent.ToString());
        }

        private static void HandleEvent(WarnEvent warnEvent, ISession session)
        {
            TestPostMessage(warnEvent.ToString(), LogLevel.Warning);
            // If the event requires no input return.
            if (!warnEvent.RequireInput) return;
            // Otherwise require input.
            TestPostMessage(session.Translation.GetTranslation(TranslationString.RequireInputText));
            Console.ReadKey();
        }

        private static void HandleEvent(UseLuckyEggEvent useLuckyEggEvent, ISession session)
        {
            TestPostMessage(session.Translation.GetTranslation(TranslationString.EventUsedLuckyEgg, useLuckyEggEvent.Count),
                LogLevel.Egg);
        }

        private static void HandleEvent(PokemonEvolveEvent pokemonEvolveEvent, ISession session)
        {
            string strPokemon = session.Translation.GetPokemonTranslation(pokemonEvolveEvent.Id);
            TestPostMessage(pokemonEvolveEvent.Result == EvolvePokemonResponse.Types.Result.Success
                ? session.Translation.GetTranslation(TranslationString.EventPokemonEvolvedSuccess, strPokemon, pokemonEvolveEvent.Exp)
                : session.Translation.GetTranslation(TranslationString.EventPokemonEvolvedFailed, pokemonEvolveEvent.Id, pokemonEvolveEvent.Result,
                    strPokemon),
                LogLevel.Evolve);
        }

        private static void HandleEvent(TransferPokemonEvent transferPokemonEvent, ISession session)
        {
            TestPostMessage(
                session.Translation.GetTranslation(TranslationString.EventPokemonTransferred,
                session.Translation.GetPokemonTranslation(transferPokemonEvent.Id),
                transferPokemonEvent.Cp.ToString(),
                transferPokemonEvent.Perfection.ToString("0.00"),
                transferPokemonEvent.BestCp.ToString(),
                transferPokemonEvent.BestPerfection.ToString("0.00"),
                transferPokemonEvent.FamilyCandies),
                LogLevel.Transfer);
        }

        private static void HandleEvent(ItemRecycledEvent itemRecycledEvent, ISession session)
        {
            TestPostMessage(session.Translation.GetTranslation(TranslationString.EventItemRecycled, itemRecycledEvent.Count, itemRecycledEvent.Id),
                LogLevel.Recycling);
        }

        private static void HandleEvent(EggIncubatorStatusEvent eggIncubatorStatusEvent, ISession session)
        {
            TestPostMessage(eggIncubatorStatusEvent.WasAddedNow
                ? session.Translation.GetTranslation(TranslationString.IncubatorPuttingEgg, eggIncubatorStatusEvent.KmRemaining)
                : session.Translation.GetTranslation(TranslationString.IncubatorStatusUpdate, eggIncubatorStatusEvent.KmRemaining),
                LogLevel.Egg);
        }

        private static void HandleEvent(EggHatchedEvent eggHatchedEvent, ISession session)
        {
            TestPostMessage(session.Translation.GetTranslation(TranslationString.IncubatorEggHatched,
                session.Translation.GetPokemonTranslation(eggHatchedEvent.PokemonId), eggHatchedEvent.Level, eggHatchedEvent.Cp, eggHatchedEvent.MaxCp, eggHatchedEvent.Perfection),
                LogLevel.Egg);
        }

        private static void HandleEvent(FortUsedEvent fortUsedEvent, ISession session)
        {
            var itemString = fortUsedEvent.InventoryFull
                ? session.Translation.GetTranslation(TranslationString.InvFullPokestopLooting)
                : fortUsedEvent.Items;
            TestPostMessage(
                session.Translation.GetTranslation(TranslationString.EventFortUsed, fortUsedEvent.Name, fortUsedEvent.Exp, fortUsedEvent.Gems,
                    itemString, fortUsedEvent.Latitude, fortUsedEvent.Longitude),
                LogLevel.Pokestop);
        }

        private static void HandleEvent(FortFailedEvent fortFailedEvent, ISession session)
        {
            if (fortFailedEvent.Try != 1 && fortFailedEvent.Looted == false)
            {
                Logger.lineSelect(0, 1); // Replaces the last line to prevent spam.
            }

            if (fortFailedEvent.Looted == true)
            {
                TestPostMessage(
                session.Translation.GetTranslation(TranslationString.SoftBanBypassed),
                LogLevel.SoftBan, ConsoleColor.Green);
            }
            else
            {
                TestPostMessage(
                session.Translation.GetTranslation(TranslationString.EventFortFailed, fortFailedEvent.Name, fortFailedEvent.Try, fortFailedEvent.Max),
                LogLevel.SoftBan);
            }
        }

        private static void HandleEvent(FortTargetEvent fortTargetEvent, ISession session)
        {

            int intTimeForArrival = (int)(fortTargetEvent.Distance / (session.LogicSettings.WalkingSpeedInKilometerPerHour * 0.5));

            TestPostMessage(
                session.Translation.GetTranslation(TranslationString.EventFortTargeted, fortTargetEvent.Name,
                     Math.Round(fortTargetEvent.Distance), intTimeForArrival),
                LogLevel.Info, ConsoleColor.Gray);
        }

        private static void HandleEvent(PokemonCaptureEvent pokemonCaptureEvent, ISession session)
        {

            Func<ItemId, string> returnRealBallName = a =>
            {
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (a)
                {
                    case ItemId.ItemPokeBall:
                        return session.Translation.GetTranslation(TranslationString.Pokeball);
                    case ItemId.ItemGreatBall:
                        return session.Translation.GetTranslation(TranslationString.GreatPokeball);
                    case ItemId.ItemUltraBall:
                        return session.Translation.GetTranslation(TranslationString.UltraPokeball);
                    case ItemId.ItemMasterBall:
                        return session.Translation.GetTranslation(TranslationString.MasterPokeball);
                    default:
                        return session.Translation.GetTranslation(TranslationString.CommonWordUnknown);
                }
            };

            var catchType = pokemonCaptureEvent.CatchType;

            string strStatus;
            switch (pokemonCaptureEvent.Status)
            {
                case CatchPokemonResponse.Types.CatchStatus.CatchError:
                    strStatus = session.Translation.GetTranslation(TranslationString.CatchStatusError);
                    break;
                case CatchPokemonResponse.Types.CatchStatus.CatchEscape:
                    strStatus = session.Translation.GetTranslation(TranslationString.CatchStatusEscape);
                    break;
                case CatchPokemonResponse.Types.CatchStatus.CatchFlee:
                    strStatus = session.Translation.GetTranslation(TranslationString.CatchStatusFlee);
                    break;
                case CatchPokemonResponse.Types.CatchStatus.CatchMissed:
                    strStatus = session.Translation.GetTranslation(TranslationString.CatchStatusMissed);
                    break;
                case CatchPokemonResponse.Types.CatchStatus.CatchSuccess:
                    strStatus = session.Translation.GetTranslation(TranslationString.CatchStatusSuccess);
                    break;
                default:
                    strStatus = pokemonCaptureEvent.Status.ToString();
                    break;
            }

            var catchStatus = pokemonCaptureEvent.Attempt > 1
                ? session.Translation.GetTranslation(TranslationString.CatchStatusAttempt, strStatus, pokemonCaptureEvent.Attempt)
                : session.Translation.GetTranslation(TranslationString.CatchStatus, strStatus);

            var familyCandies = pokemonCaptureEvent.FamilyCandies > 0
                ? session.Translation.GetTranslation(TranslationString.Candies, pokemonCaptureEvent.FamilyCandies)
                : "";

            string message;

            if (pokemonCaptureEvent.Status == CatchPokemonResponse.Types.CatchStatus.CatchSuccess)
            {
                message = session.Translation.GetTranslation(TranslationString.EventPokemonCaptureSuccess, catchStatus, catchType, session.Translation.GetPokemonTranslation(pokemonCaptureEvent.Id),
                pokemonCaptureEvent.Level, pokemonCaptureEvent.Cp, pokemonCaptureEvent.MaxCp, pokemonCaptureEvent.Perfection.ToString("0.00"), pokemonCaptureEvent.Probability,
                pokemonCaptureEvent.Distance.ToString("F2"),
                returnRealBallName(pokemonCaptureEvent.Pokeball), pokemonCaptureEvent.BallAmount,
                pokemonCaptureEvent.Exp, familyCandies, pokemonCaptureEvent.Latitude.ToString("0.000000"), pokemonCaptureEvent.Longitude.ToString("0.000000"));


                TestPostMessage(message, LogLevel.Caught);
            }
            else
            {
                message = session.Translation.GetTranslation(TranslationString.EventPokemonCaptureFailed, catchStatus, catchType, session.Translation.GetPokemonTranslation(pokemonCaptureEvent.Id),
                pokemonCaptureEvent.Level, pokemonCaptureEvent.Cp, pokemonCaptureEvent.MaxCp, pokemonCaptureEvent.Perfection.ToString("0.00"), pokemonCaptureEvent.Probability,
                pokemonCaptureEvent.Distance.ToString("F2"),
                returnRealBallName(pokemonCaptureEvent.Pokeball), pokemonCaptureEvent.BallAmount,
                pokemonCaptureEvent.Latitude.ToString("0.000000"), pokemonCaptureEvent.Longitude.ToString("0.000000"));
                TestPostMessage(message, LogLevel.Flee);
            }
        }

        private static void HandleEvent(NoPokeballEvent noPokeballEvent, ISession session)
        {
            TestPostMessage(session.Translation.GetTranslation(TranslationString.EventNoPokeballs, noPokeballEvent.Id, noPokeballEvent.Cp),
                LogLevel.Caught);
        }

        private static void HandleEvent(UseBerryEvent useBerryEvent, ISession session)
        {
            string strBerry;
            switch (useBerryEvent.BerryType)
            {
                case ItemId.ItemRazzBerry:
                    strBerry = session.Translation.GetTranslation(TranslationString.ItemRazzBerry);
                    break;
                default:
                    strBerry = useBerryEvent.BerryType.ToString();
                    break;
            }

            TestPostMessage(session.Translation.GetTranslation(TranslationString.EventUseBerry, strBerry, useBerryEvent.Count),
                LogLevel.Berry);
        }

        private static void HandleEvent(SnipeEvent snipeEvent, ISession session)
        {
            TestPostMessage(snipeEvent.ToString(), LogLevel.Sniper);
        }

        private static void HandleEvent(SnipeScanEvent snipeScanEvent, ISession session)
        {
            TestPostMessage(snipeScanEvent.PokemonId == PokemonId.Missingno
                ? ((snipeScanEvent.Source != null) ? "(" + snipeScanEvent.Source + ") " : null) + session.Translation.GetTranslation(TranslationString.SnipeScan,
                    $"{snipeScanEvent.Bounds.Latitude},{snipeScanEvent.Bounds.Longitude}")
                : ((snipeScanEvent.Source != null) ? "(" + snipeScanEvent.Source + ") " : null) + session.Translation.GetTranslation(TranslationString.SnipeScanEx, session.Translation.GetPokemonTranslation(snipeScanEvent.PokemonId),
                    snipeScanEvent.Iv > 0 ? snipeScanEvent.Iv.ToString(CultureInfo.InvariantCulture) : session.Translation.GetTranslation(TranslationString.CommonWordUnknown),
                    $"{snipeScanEvent.Bounds.Latitude},{snipeScanEvent.Bounds.Longitude}"), LogLevel.Sniper);
        }

        private static void HandleEvent(DisplayHighestsPokemonEvent displayHighestsPokemonEvent, ISession session)
        {
            if (session.LogicSettings.AmountOfPokemonToDisplayOnStart <= 0)
            {
                return;
            }

            string strHeader;
            //PokemonData | CP | IV | Level | MOVE1 | MOVE2 | Candy
            switch (displayHighestsPokemonEvent.SortedBy)
            {
                case "Level":
                    strHeader = session.Translation.GetTranslation(TranslationString.DisplayHighestsLevelHeader);
                    break;
                case "IV":
                    strHeader = session.Translation.GetTranslation(TranslationString.DisplayHighestsPerfectHeader);
                    break;
                case "CP":
                    strHeader = session.Translation.GetTranslation(TranslationString.DisplayHighestsCpHeader);
                    break;
                case "MOVE1":
                    strHeader = session.Translation.GetTranslation(TranslationString.DisplayHighestMove1Header);
                    break;
                case "MOVE2":
                    strHeader = session.Translation.GetTranslation(TranslationString.DisplayHighestMove2Header);
                    break;
                case "Candy":
                    strHeader = session.Translation.GetTranslation(TranslationString.DisplayHighestCandy);
                    break;
                default:
                    strHeader = session.Translation.GetTranslation(TranslationString.DisplayHighestsHeader);
                    break;
            }
            var strPerfect = session.Translation.GetTranslation(TranslationString.CommonWordPerfect);
            var strName = session.Translation.GetTranslation(TranslationString.CommonWordName).ToUpper();
            var move1 = session.Translation.GetTranslation(TranslationString.DisplayHighestMove1Header);
            var move2 = session.Translation.GetTranslation(TranslationString.DisplayHighestMove2Header);
            var candy = session.Translation.GetTranslation(TranslationString.DisplayHighestCandy);

            TestPostMessage(session.Translation.GetTranslation(TranslationString.HighestsPokemoHeader, strHeader), LogLevel.Info, ConsoleColor.Yellow);
            foreach (var pokemon in displayHighestsPokemonEvent.PokemonList)
            {
                string strMove1 = session.Translation.GetPokemonMovesetTranslation(pokemon.Item5);
                string strMove2 = session.Translation.GetPokemonMovesetTranslation(pokemon.Item6);

                TestPostMessage(
                    session.Translation.GetTranslation(
                        TranslationString.HighestsPokemoCell,
                        pokemon.Item1.Cp.ToString().PadLeft(4, ' '),
                        pokemon.Item2.ToString().PadLeft(4, ' '),
                        pokemon.Item3.ToString("0.00"),
                        strPerfect,
                        pokemon.Item4.ToString("00"),
                        strName,
                        session.Translation.GetPokemonTranslation(pokemon.Item1.PokemonId).PadRight(10, ' '),
                        move1,
                        strMove1.PadRight(20, ' '),
                        move2,
                        strMove2.PadRight(20, ' '),
                        candy,
                        pokemon.Item7
                    ),
                    LogLevel.Info,
                    ConsoleColor.Yellow
                );
            }
        }

        private static void HandleEvent(EvolveCountEvent evolveCountEvent, ISession session)
        {
            TestPostMessage(session.Translation.GetTranslation(TranslationString.PkmPotentialEvolveCount, evolveCountEvent.Evolves), LogLevel.Evolve);
        }

        private static void HandleEvent(UpdateEvent updateEvent, ISession session)
        {
            TestPostMessage(updateEvent.ToString(), LogLevel.Update);
        }

        private static void HandleEvent(HumanWalkingEvent humanWalkingEvent, ISession session)
        {
            if (session.LogicSettings.ShowVariantWalking)
                TestPostMessage(
                    session.Translation.GetTranslation(TranslationString.HumanWalkingVariant,
                    humanWalkingEvent.OldWalkingSpeed,
                    humanWalkingEvent.CurrentWalkingSpeed),
                    LogLevel.Info, ConsoleColor.DarkCyan);
        }

        public void Listen(IEvent evt, ISession session)
        {
            dynamic eve = evt;

            try
            {
                HandleEvent(eve, session);
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch
            {
            }
        }
    }
}
