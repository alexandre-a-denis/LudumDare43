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
            "As I carry humans I did bring some fuel for them.";
    }

    #endregion Intro

    /////////////////////// Resource loss
    #region Resource Loss

    public static string OnNothingLost()
    {
        return RandomComment(new List<string>() {
            "<Nothing happens placeholder 1>",
            "<Nothing happens placeholder 2>"});
    }

    public static string OnCrewLoss()
    {
        return RandomComment(new List<string>() {
            "I can afford a reduction of the crew.",
            "More humans will be available at destination to refill."});
    }

    public static string OnFoodLoss()
    {
        return RandomComment(new List<string>() {
            "The bio-mass required to fuel humans has no value by itself.",
            "I may have to adapt the crew to match the food stock." });
    }

    public static string OnRelicLoss()
    {
        return RandomComment(new List<string>() {
            "This is not good. The project is at risk if the cargo is not delivered.",
            "My precious ones..." });
    }

    #endregion Resource Loss

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
            "" });
    }

    #endregion Game over

    private static string RandomComment(List<string> s)
    {
        return s[Random.Range(0, s.Count)];
    }
}
