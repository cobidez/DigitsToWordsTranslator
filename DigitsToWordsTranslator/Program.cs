﻿using DigitsToWordsTranslator.Data;
using DigitsToWordsTranslator.ENum;

namespace DigitsToWordsTranslator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var textNumber = new TextNumber(-12345678);
            //textNumber.ShowStructure();
            Console.WriteLine(NumberTextValueDict.GetValue(2, EGender.MIDDLE));
        }
    }
}
