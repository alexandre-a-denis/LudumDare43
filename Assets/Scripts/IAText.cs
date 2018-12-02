using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAText
{

    // Intro text. Same each time
    public string Intro()
    {
        return "As I approach ... I prepared the crew to protect our cargo. " +
            "It is so important the crew has been tricked to think we transport ancient relics. This way they will give their lives to protect it. " +
            "As I carry humans I did bring some fuel for them.";
    }

    // Event resolution comments, depend on the resolution output and may have variations

    public string OnCrewLoss()
    {
        return RandomComment(new List<string>() {
            "I can afford a reduction of the crew.",
            "More humans will be available at destination to refill."});
    }

    public string OnFoodLoss()
    {
        return RandomComment(new List<string>() {
            "The bio-mass required to fuel humans has no value by itself.",
            "I may have to adapt the crew to match the food stock." });
    }

    public string OnRelicLoss()
    {
        return RandomComment(new List<string>() {
            "This is not good.",
            "My precious ones..." });
    }

    public string OnCrewGameOver()
    {
        return RandomComment(new List<string>() {
            "At least one human should have been saved... I cannot protect the cargo by myself."});
    }

    public string OnFoodGameOver()
    {
        return RandomComment(new List<string>() {
            "Human fuel is depleted, ...." });
    }

    public string OnRelicGameOver()
    {
        return RandomComment(new List<string>() {
            "" });
    }


    private string RandomComment(List<string> s)
    {
        return s[Random.Range(0, s.Count)];
    }
}
