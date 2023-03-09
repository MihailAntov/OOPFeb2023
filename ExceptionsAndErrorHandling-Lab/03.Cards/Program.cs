using System.Collections.Generic;
using System;

namespace Cards;
public class Program
{
    public static void Main(string[] args)
    {
        string[] cardsInput = Console.ReadLine().Split(",");
        List<string> cards = new List<string>();
        foreach (string cardInput in cardsInput)
        {
            string[] cardArgs = cardInput.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string face = cardArgs[0];
            string suit = cardArgs[1];

            try
            {
                Card card = new Card(face, suit);
                cards.Add(card.ToString());
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        Console.WriteLine(String.Join(" ",cards));
    }





}

public class Card
{
    private string face;
    private string suit;

    public Card(string face, string suit)
    {

        Face = face;
        Suit = suit;
    }

    public string Face
    {
        get { return face; }
        set
        {

            if (!CardHelper.faces.Contains(value))
            {
                throw new ArgumentException("Invalid card!");
            }
            face = value;
        }
    }

    public string Suit
    {
        get { return suit; }
        set
        {
            if (!CardHelper.suits.Contains(value))
            {
                throw new ArgumentException("Invalid card!");
            }
            suit = value;
        }
    }

    public override string ToString()
    {
        return $"[{Face}{CardHelper.GetSymbol(suit)}]";
    }


}

public static class CardHelper
{
    public static readonly List<string> faces = new List<string> { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
    public static readonly List<string> suits = new List<string> { "S", "H", "D", "C" };
    public static string GetSymbol(string suit)
    {
        string result = string.Empty;
        switch (suit)
        {
            case "S":
                result = "\u2660";
                break;
            case "H":
                result = "\u2665";
                break;
            case "D":
                result = "\u2666";
                break;
            case "C":
                result = "\u2663";
                break;
        }
        return result;
    }
}