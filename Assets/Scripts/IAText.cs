﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAText
{

    /////////////////////// Intro
    #region Intro

    public static string Intro()
    {
        return
            "Ship AI core:\n" +
            "As we approach the dangerous system I prepared the crew to protect our cargo. " +
            "This cargo is so important the crew has been tricked to think we transport ancient relics. " +
            "This way they will give their lives to protect it if I order so." +
            "As I carry humans I did bring some fuel for them. I figured they don't run for long without food.";
    }

    public static string Help()
    {
        return
            "Your goal is to keep at least 1 Relic at turn 20.\n" +
            "Random events will occur to rooms. Crew in the room will help counter the event. They may die if they could not save the room.\n" +
            "In order to raise probability to counter the event, you can choose to sacrifice crew members.\n" +
            "Food is consumed (1 per crew member) each turn. If no food is left, crew members will die.";
    }

    #endregion Intro

    /////////////////////// Resource loss

    #region Drama outcome
    public static string CommentOnDramaOutcome(DramaReport report)
    {
        return CommentOnDramaOutcome(report.HasRoomBeenDestroyed, report.CrewQtySacrified, report.CrewQtyLoss, report.RoomType);
    }

    public static string CommentOnDramaOutcome(bool roomLost, int nbSacrificedCrew, int nbDeadCrew, RoomType roomType)
    {
        Debug.Log("CommentOnDramaOutcome roomLost=" + roomLost + ", nbSacrificedCrew=" + nbSacrificedCrew + ", nbDeadCrew=" + nbDeadCrew + ", roomType=" + roomType);

        if (roomLost)
        {
            if (nbDeadCrew == 0) // Room destroyed while being unprotected
            {
                switch (roomType)
                {
                    case RoomType.FOOD:
                        return RandomComment(new List<string>() {
                        "No one was looking at the food. Oh well..."});
                    case RoomType.RELICS:
                        return RandomComment(new List<string>() {
                        "My precious ones..."});
                }
            }
            else if (nbSacrificedCrew == 0) //Room destroyed while being protected (no sacrifice)
            {
                switch (roomType)
                {
                    case RoomType.FOOD:
                        return RandomComment(new List<string>() {
                        "Without food my crew will enter a state where they are even more useless.",
                        "I wonder if food is more important than hope for humans... maybe."});
                    case RoomType.RELICS:
                        return RandomComment(new List<string>() {
                        "Useless humans! Why should I keep them alive if they can't save the Relics?",
                        "I need something more efficient than this crew."});
                }
            }
            else // Room destroyed while being protected (with sacrifice)
            {
                switch (roomType)
                {
                    case RoomType.FOOD:
                        return RandomComment(new List<string>() {
                        "Without food my crew will enter a state where they are even more useless.",
                        "I wonder if food is more important than hope for humans... maybe."});
                    case RoomType.RELICS:
                        return RandomComment(new List<string>() {
                        "Useless humans! Why should I keep them alive if they can't save the Relics?",
                        "Humans are so weak. I should use even more of them."});
                }
            }
        }
        else
        {
            if (nbSacrificedCrew == 0) //Room kept while being protected (no sacrifice)
            {
                switch (roomType)
                {
                    case RoomType.FOOD:
                        return RandomComment(new List<string>() {
                        "Keeping food is good for my crew."});
                    case RoomType.RELICS:
                        return RandomComment(new List<string>() {
                         "Perfect!! Relics are saved for the greater good."});
                }
            }
            else // Room kept while being protected (with sacrifice)
            {
                switch (roomType)
                {
                    case RoomType.FOOD:
                        return RandomComment(new List<string>() {
                        "Keeping food while reducing crew is a good optimisation.",
                        "Human fuel is saved at a price of some humans."});
                    case RoomType.RELICS:
                        return RandomComment(new List<string>() {
                        "Perfect!! Relics are saved for a minimal cost.",
                        "The Relics are saved. Wisely spent humans!"});
                }
            }
        }


        return "";
    }

    #endregion Drama outcome

    /////////////////////// Game over
    #region Game over

    public static string OnCrewGameOver()
    {
        return RandomComment(new List<string>() {
            "At least one human should have been saved... I cannot fully protect the cargo by myself."});
    }

    public static string OnFoodGameOver()
    {
        return RandomComment(new List<string>() {
            "Humans need so much fuel to function... Now they're all dead and unable to protect the cargo." });
    }

    public static string OnRelicGameOver()
    {
        return RandomComment(new List<string>() {
            "All relics are lost, I may dump the now useless crew." });
    }

    #endregion Game over

    private static string RandomComment(List<string> s)
    {
        return s[Random.Range(0, s.Count)];
    }
}
