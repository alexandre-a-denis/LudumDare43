using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAText
{

    /////////////////////// Intro
    #region Intro

    public static string Intro()
    {
        return
            "As I approach the dangerous system I prepared the crew to protect our cargo. " +
            "It is so important the crew has been tricked to think we transport ancient relics. This way they will give their lives to protect it. " +
            "As I carry humans I did bring some fuel for them. I figured they don't run for long without food. " +
            "Humans also tend to have their mental state degraded when they see other humans die. ";
    }

    #endregion Intro

    /////////////////////// Resource loss
    // Drama results can be one of:
    // - room is saved thanks to crew sacrifice. IA is happy
    // - room is lost while crew was trying to save it. IA is angry at those stupids humans not even able to save a room.
    // - room is lost while no crew was affected at its defense. IA is neutral

    #region Drama outcome
    public static string CommentOnDramaOutcome(DramaReport report)
    {
        return CommentOnDramaOutcome(report.HasRoomBeenDestroyed, report.CrewQtyLoss, report.RoomType);
    }

    public static string CommentOnDramaOutcome(bool roomLost, int nbCrewSacrificed, RoomType roomType)
    {
        Debug.Log("CommentOnDramaOutcome roomLost=" + roomLost + ", nbCrewSacrificed=" + nbCrewSacrificed + ", roomType=" + roomType);

        if (!roomLost) // Room is saved.
        {
            switch (roomType)
            {
                case RoomType.FOOD:
                    return RandomComment(new List<string>() {
                        "Keeping food while reducing crew is a good optimisation.",
                        "Human fuel is saved at a price of some humans."});
                case RoomType.RELICS:
                    return RandomComment(new List<string>() {
                        "Perfect!! Relics are saved for the greater good.",
                        "The Relics are saved. Wisely spent humans!",
                        "Losing hope and keeping Relics, good move."});
            }
        }

        if (roomLost & nbCrewSacrificed > 0) // We did fail to save the room. 
        {
            switch (roomType)
            {
                case RoomType.FOOD:
                    return RandomComment(new List<string>() {
                        "Losing food is not the end of the world but humans want some.",
                        "Without food my crew will enter a state where they are even more useless.",
                        "I wonder if food is more important than hope for humans... maybe."});
                case RoomType.RELICS:
                    return RandomComment(new List<string>() {
                        "Useless humans! Why should I keep them alive if they can't save the Relics?",
                        "I need something more efficient than this crew."});
            }
        }

        if (roomLost & nbCrewSacrificed == 0) // We did not even try to save the room.
        {
            switch (roomType)
            {
                case RoomType.FOOD:
                    return RandomComment(new List<string>() {
                        "Keeping them alive to protect really important things."});
                case RoomType.RELICS:
                    return RandomComment(new List<string>() {
                        "This is not good. The project is at risk if the cargo is not delivered.",
                        "My precious ones..."});
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
