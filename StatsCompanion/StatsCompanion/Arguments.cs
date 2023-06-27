﻿using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace StatsCompanion
{
    /// <summary>
    /// Handles the final JSON export.
    /// </summary>
    internal class Arguments
    {
        public string runTime { get; set; }
        public string runDate { get; set; }
        public string kefkaTowerUnlockTime { get; set; }
        public bool skip { get; set; }
        public string ktSkipUnlockTime { get; set; }
        public string ktStartTime { get; set; }
        public string kefkaTime { get; set; }
        public int countResets { get; set; }
        public string timeSpentOnMenus { get; set; }
        public int countTimesMenuWasOpened { get; set; }
        public string timeSpentOnShops { get; set; }
        public int countTimesShopsWereVisited { get; set; }
        public string totalMenuTime { get; set; }
        public string timeSpentDrivingAirship { get; set; }
        public int countTimesAirshipWasUsed { get; set; }
        public string timeSpentonBattles { get; set; }
        public int countBattlesFought { get; set; }
        public int gpSpent { get; set; }
        public List<string> chars { get; set; }
        public List<string> abilities { get; set; }
        public bool disableAbilityCheck { get; set; }
        public int numOfChars { get; set; }
        public int numOfEspers { get; set; }
        public int numOfChecks { get; set; }
        public int numOfPeekedChecks { get; set; }
        public int numOfBosses { get; set; }
        public int numOfChests { get; set; }
        public List<string> dragons { get; set; }
        public List<string> finalBattle { get; set; }
        public int highestLevel { get; set; }
        public string superBalls { get; set; }
        public string egg { get; set; }
        public string auction { get; set; }
        public string thiefPeek { get; set; }
        public string thiefReward { get; set; }
        public string coliseum { get; set; }
        public string userId { get; set; }
        public string race { get; set; }
        public string mood { get; set; }
        public string seed { get; set; }
        public string raceId { get; set; }
        public string flagset { get; set; }
        public string otherFlagset { get; set; }
        public List<string> checksCompleted { get; set; }
        public List<string> checksPeeked { get; set; }
        public Character[] finalBattleCharacters { get; set; }
        public List<string> knownSwdTechs { get; set; }
        public List<string> knownBlitzes { get; set; }
        public List<string> knownLores { get; set; }
        public List<string> route { get; set; }
        
        private const string TIME_FORMAT = "hh\\:mm\\:ss";
        private const string DATE_FORMAT = "yyyy-MM-ddTHH:mm:ss";

        public Arguments(Run run)
        {
            runTime = (run.EndTime - run.StartTime - WCData.TimeFromKefkaFlashToAnimation).ToString(@TIME_FORMAT);
            runDate = run.StartTime.ToString(DATE_FORMAT);
            flagset = "";
            otherFlagset = "";
            ktStartTime = (run.KefkaTowerStartTime - run.StartTime).ToString(@TIME_FORMAT);
            kefkaTime = (run.KefkaStartTime - run.StartTime).ToString(@TIME_FORMAT);
            userId = "";
            chars = run.StartingCharacters;
            abilities = run.StartingCommands;
            disableAbilityCheck = false;
            numOfChars = run.CharacterCount;
            numOfEspers = run.EsperCount;
            numOfChecks = run.CheckCount;
            numOfPeekedChecks = run.ChecksPeeked.Count;
            numOfBosses = run.BossCount;
            numOfChests = run.ChestCount;
            kefkaTowerUnlockTime = (run.KefkaTowerUnlockTime - run.StartTime).ToString(@TIME_FORMAT);
            skip = run.IsKTSkipUnlocked;
            ktSkipUnlockTime = run.KtSkipUnlockTimeString;
            dragons = run.DragonsKilled;
            finalBattle = run.FinalBattlePrep;
            highestLevel = run.CharacterMaxLevel;
            superBalls = run.HasSuperBall;
            egg = run.HasExpEgg;
            auction = run.AuctionHouseEsperCountText;
            thiefPeek = run.TzenThief;
            thiefReward = run.TzenThiefReward;
            coliseum = run.ColiseumVisit;
            race = "No_Race";
            mood = "Not_recorded";
            raceId = "";
            countResets = run.ResetCount;
            timeSpentOnMenus = run.TimeSpentOnMenus.ToString(@TIME_FORMAT);
            countTimesMenuWasOpened = run.MenuOpenCounter;
            timeSpentOnShops = run.TimeSpentOnShops.ToString(@TIME_FORMAT);
            countTimesShopsWereVisited = run.ShopOpenCounter;
            totalMenuTime = (run.TimeSpentOnMenus + run.TimeSpentOnShops).ToString(@TIME_FORMAT);
            timeSpentDrivingAirship = run.TimeSpentOnAirship.ToString(@TIME_FORMAT);
            countTimesAirshipWasUsed = run.AirshipCounter;
            timeSpentonBattles = run.TimeSpentOnBattles.ToString(@TIME_FORMAT);
            countBattlesFought = run.BattlesFought;
            gpSpent = run.GPSpent;
            checksCompleted = run.ChecksCompleted;
            checksPeeked = run.ChecksPeeked;
            finalBattleCharacters = run.FinalBattleCharacters;
            route = run.RouteJson;
            knownSwdTechs = run.KnownSwdTechs;
            knownBlitzes = run.KnownBlitzes;
            knownLores = run.KnownLores;
            seed = "";
        }


        /// <summary>
        /// Replaces certain characters for the JSON to be compatible with StatsCollide.
        /// </summary>
        /// <param name="jsonString">The string to replace.</param>
        /// <returns>A string with replaced characters for underscores.</returns>
        public static string ReplaceCharacters(string jsonString)
        {
            // Replace " - " with "__" in values
            string replacedCharacters = Regex.Replace(jsonString, @"""(.*?)""", match =>
            {
                string value = match.Groups[1].Value;
                string replacedValue = value.Replace(" - ", "__");
                replacedValue = replacedValue.Replace(" ", "_");
                return "\"" + replacedValue + "\"";
            });

            return replacedCharacters;
        }
    }
}