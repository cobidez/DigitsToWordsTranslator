using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Globalization;

namespace DigitsToWordsTranslator;

internal class StringNumber
{
    private List<string> _parsedDigitIntegerPartOnIndexesList = []; // Хранит индексы целой части
    private string _parsedDigitPartialPart = string.Empty; // Хранит дробную часть
    private Dictionary<int,NumberStringByGenderCase> _numberStringByGenderCasesList = [];    
    private const int _indexSize = 3;
    private bool _isNegative;

    public StringNumber(int integerValue)
    {
        InitNumberSign(integerValue);
        InitIntegerPart(integerValue);
    }

    public StringNumber(double doubleValue) : this((int)doubleValue)
    {
        InitPartialPart(doubleValue);
    }

    public void ShowStructure ()
    {
        Console.WriteLine($"_isNegative = {_isNegative}");
        Console.WriteLine("IntegerPart");
        foreach (String index in _parsedDigitIntegerPartOnIndexesList)
        {
            Console.WriteLine(index);
        }

        Console.WriteLine("PartialPart");
        Console.WriteLine(_parsedDigitPartialPart);
    }

    public string GetTextString()
    {
        var stringBuilder  = new StringBuilder();
        return "";
    }

    private void InitNumberSign(int integerValue)
    {
        // Обрабатываем знак числа
        if (integerValue < 0)
        {
            _isNegative = true;
        }
    }

    private void InitIntegerPart(int integerValue)
    {
        if (integerValue < 0)
        {
            integerValue *= -1; // Меняем знак, чтобы можно было нормально порезать на символы.
        }

        // Разделяем число на массив символов по разряду числа
        string stringIntegerValue = integerValue.ToString();            ;

        int greaterIndexDigitsCountMod = stringIntegerValue.Length % _indexSize;
        int greaterIndexDigitsCount = greaterIndexDigitsCountMod == 0 ? _indexSize : greaterIndexDigitsCountMod; // Определяем кол-во цифр в старшем разряде

        // Определяем кол-во итераций цикла, для 9 символов это будет 3 итерации, для 10, 11, 12 символов это будет 4 итерации.
        int loopCount = stringIntegerValue.Length / _indexSize + (greaterIndexDigitsCount == 3 ? 0 : 1);

        for (int i = 0; i < loopCount; i++)
        {
            int substrFromIndex;
            int substrLength;

            // Если это последняя итерация цикла и кол-во цифр в старшем разряде не равно 3
            if (i + 1 == loopCount)
            {
                substrFromIndex = 0; //Старший разряд всегда будет резаться с первого элемента
                substrLength = greaterIndexDigitsCount;
            }
            else
            {
                substrFromIndex = stringIntegerValue.Length - _indexSize * (i + 1);
                substrLength = _indexSize;
            }

            string substredString = stringIntegerValue.Substring(substrFromIndex, substrLength);

            _parsedDigitIntegerPartOnIndexesList.Add(substredString);
        }
    }

    private void InitPartialPart(double doubleValue)
    {
        if (doubleValue < 0)
        {
            doubleValue *= -1;
        }
        
        string stringPartialValue = doubleValue.ToString();
        int separatorIndex = stringPartialValue.IndexOf(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator, 0);

        _parsedDigitPartialPart = stringPartialValue.Substring(separatorIndex + 1);
    }

    private void AddValueToNumberStringByGenderCaseList(int index,string maleCase, string femaleCase = null, string middleCase = null) 
    {
        _numberStringByGenderCasesList.Add(index,new NumberStringByGenderCase(maleCase, femaleCase, middleCase));
    }

    private void InitNumberStringByGenderCaseList()
    {
        AddValueToNumberStringByGenderCaseList(1,"один", "одна", "одно");
        AddValueToNumberStringByGenderCaseList(2,"два", "две");
        AddValueToNumberStringByGenderCaseList(3,"три");
        AddValueToNumberStringByGenderCaseList(4,"четыре");
        AddValueToNumberStringByGenderCaseList(5, "пять");
        AddValueToNumberStringByGenderCaseList(6, "шесть");
        AddValueToNumberStringByGenderCaseList(7, "семь");
        AddValueToNumberStringByGenderCaseList(8, "восемь");
        AddValueToNumberStringByGenderCaseList(9, "девять");
        AddValueToNumberStringByGenderCaseList(10, "десять");
        AddValueToNumberStringByGenderCaseList(11, "одиннадцать");
        AddValueToNumberStringByGenderCaseList(12, "двенадцать");
        AddValueToNumberStringByGenderCaseList(13, "тринадцать");
        AddValueToNumberStringByGenderCaseList(14, "четырнадцать");
        AddValueToNumberStringByGenderCaseList(15, "пятнадцать");
        AddValueToNumberStringByGenderCaseList(16, "шетнадцать");
        AddValueToNumberStringByGenderCaseList(17, "семнадцать");
        AddValueToNumberStringByGenderCaseList(18, "вомемнадцать");
        AddValueToNumberStringByGenderCaseList(19, "девятнадцать");
        AddValueToNumberStringByGenderCaseList(20, "двадцать");
        AddValueToNumberStringByGenderCaseList(30, "тридцать");
        AddValueToNumberStringByGenderCaseList(40, "сорок");
        AddValueToNumberStringByGenderCaseList(50, "пятьдесят");
        AddValueToNumberStringByGenderCaseList(60, "шестьдесят");
        AddValueToNumberStringByGenderCaseList(70, "семьдесят");
        AddValueToNumberStringByGenderCaseList(80, "восемьдесят");
        AddValueToNumberStringByGenderCaseList(90, "девяносто");
        AddValueToNumberStringByGenderCaseList(100, "сто");
        AddValueToNumberStringByGenderCaseList(200, "двести");
        AddValueToNumberStringByGenderCaseList(300, "триста");
        AddValueToNumberStringByGenderCaseList(400, "четыреста");
        AddValueToNumberStringByGenderCaseList(500, "пятьсот");
        AddValueToNumberStringByGenderCaseList(600, "шестьсот");
        AddValueToNumberStringByGenderCaseList(700, "семьсот");
        AddValueToNumberStringByGenderCaseList(800, "восемьсот");
        AddValueToNumberStringByGenderCaseList(900, "девятьсот");
    }

    private record NumberStringByGenderCase(string? maleCase, string? femaleCase, string? middleCase) 
    { 
        string GetMaleCase() => maleCase;
        string GetFemaleCase() => femaleCase is null ? GetMaleCase() : femaleCase;
        string GetMiddleCase() => middleCase is null ? GetMaleCase() : middleCase;
    };

}
