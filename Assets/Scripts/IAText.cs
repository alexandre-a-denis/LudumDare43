using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAText
{

    /////////////////////// Intro
    #region Intro

    public string Intro()
    {
        return "As I approach ... I prepared the crew to protect our cargo. " +
            "It is so important the crew has been tricked to think we transport ancient relics. This way they will give their lives to protect it. " +
            "As I carry humans I did bring some fuel for them. I figured they don't run for long without food.";
    }

    #endregion Intro

    /////////////////////// Resource loss
    #region Drama outcome

    // Drama results can be one of:
    // - room is saved thanks to crew sacrifice. IA is happy
    // - room is lost while crew was trying to save it. IA is angry at those stupids humans not even able to save a room.
    // - room is lost while no crew was affected at its defense. IA is neutral
    public static string CommentOnDramaOutcome(DramaReport report)
    {
        return CommentOnDramaOutcome(report.HasRoomBeenDestroyed, report.CrewQtyLoss, report.RoomType);
    }

    public static string CommentOnDramaOutcome(bool roomLost, int nbCrewSacrificed, RoomType roomType)
    {
        if (!roomLost)
        {
            // Room can only be saved if some crew was sacrificed. Let's be happy for the room and not really care for the humans.
            switch (roomType)
            {
                case RoomType.FOOD:
                    return RandomComment(new List<string>() {
                        "Keeping food while reducing crew is a good optimisation.",
                        "Human fuel is saved at a price of some humans."});
                case RoomType.RELICS:
                    return RandomComment(new List<string>() {
                        "Perfect!! Relics are saved for the greater good.",
                        "The Relics are saved. The crew was used wisely"});
            }
        }

        if (roomLost & nbCrewSacrificed > 0)
        {
            // We did fail to save the room. 
            switch (roomType)
            {
                case RoomType.FOOD:
                    return RandomComment(new List<string>() {
                        "PH <Lost food while trying to defend it>"});
                case RoomType.RELICS:
                    return RandomComment(new List<string>() {
                        "PH <Lost relics while trying to defend it>"});
            }
        }

        if (roomLost & nbCrewSacrificed == 0)
        {
            // We did not even try to save the room, the outcome is not a surprise.
            switch (roomType)
            {
                case RoomType.FOOD:
                    return RandomComment(new List<string>() {
                        "PH <Lost food while not trying to defend it>"});
                case RoomType.RELICS:
                    return RandomComment(new List<string>() {
                        "PH <Lost relics while not trying to defend it>"});
            }
        }

        return "";
    }

    // Deprecated
    private string OnCrewLoss()
    {
        return RandomComment(new List<string>() {
            "I can afford to manage less crew.",
            "More humans will be available at destination to refill."});
    }

    // Deprecated
    private string OnFoodLoss()
    {
        return RandomComment(new List<string>() {
            "The bio-mass required to fuel humans has no value by itself.",
            "I may have to adapt the crew to match the food stock." });
    }

    // Deprecated
    private string OnRelicLoss()
    {
        return RandomComment(new List<string>() {
            "This is not good. The project is at risk if the cargo is not delivered.",
            "My precious ones..." });
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
