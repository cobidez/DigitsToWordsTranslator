using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DigitsToWordsTranslator;

internal class StringNumber
{
    private static readonly Dictionary<int, NumberStringByGenderCase> _numberStringByGenderCasesList = GetNumberStringByGenderCaseDict(); // Хранит шаблоны чисел для преобразования в текст
    public static readonly Dictionary<int, IndexStringByGrammarCase> _indexStringByGrammarCaseList = GetIndexStringByGrammarCaseDict(); // Хранит шаблоны разрядов типа "тысяча", "миллион" и тд
    private static readonly int _indexSize = 3; // Кол-во символов в разряде: 123 456 789
    
    private int _indexCount;
    private Dictionary<int,string> _parsedDigitIntegerPartOnIndexesList = []; // Хранит индексы целой части
    private string _parsedDigitPartialPart = string.Empty; // Хранит дробную часть
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

    public void ShowStructure()
    {
        Console.WriteLine($"_isNegative = {_isNegative}");
        Console.WriteLine("IntegerPart");


        Console.WriteLine("PartialPart");
        Console.WriteLine(_parsedDigitPartialPart);
    }

    public string GetTextString()
    {
        var stringBuilder = new StringBuilder();
        for (int i = _parsedDigitIntegerPartOnIndexesList.Count - 1; i >= 0; i--)
        {
            var indexStringByGrammarCase = _indexStringByGrammarCaseList.GetValueOrDefault(i);
            stringBuilder.Append(GetIndexValueText(_parsedDigitIntegerPartOnIndexesList.GetValueOrDefault(i), indexStringByGrammarCase.Gender));
            stringBuilder.Append(" ");
        }

        return stringBuilder.ToString();
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
        string stringIntegerValue = integerValue.ToString(); ;

        int greaterIndexDigitsCountMod = stringIntegerValue.Length % _indexSize;
        int greaterIndexDigitsCount = greaterIndexDigitsCountMod == 0 ? _indexSize : greaterIndexDigitsCountMod; // Определяем кол-во цифр в старшем разряде

        // Определяем кол-во итераций цикла, для 9 символов это будет 3 итерации, для 10, 11, 12 символов это будет 4 итерации.
        _indexCount = stringIntegerValue.Length / _indexSize + (greaterIndexDigitsCount == 3 ? 0 : 1);

        for (int i = 0; i < _indexCount; i++)
        {
            int substrFromIndex;
            int substrLength;

            // Если это последняя итерация цикла и кол-во цифр в старшем разряде не равно 3
            if (i + 1 == _indexCount)
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

            _parsedDigitIntegerPartOnIndexesList.Add(i,substredString);
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

        _parsedDigitPartialPart = stringPartialValue
            .Substring(separatorIndex + 1);
    }

    private static void AddValueToNumberStringByGenderCaseDict(
        ref Dictionary<int,NumberStringByGenderCase> dict,
        int index,
        string maleCase,
        string? femaleCase = null,
        string? middleCase = null)
    {
        dict.Add(index, new NumberStringByGenderCase(maleCase, femaleCase, middleCase));
    }

    private static Dictionary<int, NumberStringByGenderCase> GetNumberStringByGenderCaseDict()
    {
        var result = new Dictionary<int, NumberStringByGenderCase>();

        AddValueToNumberStringByGenderCaseDict(ref result, 1, "один", "одна", "одно");
        AddValueToNumberStringByGenderCaseDict(ref result, 2, "два", "две");
        AddValueToNumberStringByGenderCaseDict(ref result, 3, "три");
        AddValueToNumberStringByGenderCaseDict(ref result, 4, "четыре");
        AddValueToNumberStringByGenderCaseDict(ref result, 5, "пять");
        AddValueToNumberStringByGenderCaseDict(ref result, 6, "шесть");
        AddValueToNumberStringByGenderCaseDict(ref result, 7, "семь");
        AddValueToNumberStringByGenderCaseDict(ref result, 8, "восемь");
        AddValueToNumberStringByGenderCaseDict(ref result, 9, "девять");
        AddValueToNumberStringByGenderCaseDict(ref result, 10, "десять");
        AddValueToNumberStringByGenderCaseDict(ref result, 11, "одиннадцать");
        AddValueToNumberStringByGenderCaseDict(ref result, 12, "двенадцать");
        AddValueToNumberStringByGenderCaseDict(ref result, 13, "тринадцать");
        AddValueToNumberStringByGenderCaseDict(ref result, 14, "четырнадцать");
        AddValueToNumberStringByGenderCaseDict(ref result, 15, "пятнадцать");
        AddValueToNumberStringByGenderCaseDict(ref result, 16, "шетнадцать");
        AddValueToNumberStringByGenderCaseDict(ref result, 17, "семнадцать");
        AddValueToNumberStringByGenderCaseDict(ref result, 18, "вомемнадцать");
        AddValueToNumberStringByGenderCaseDict(ref result, 19, "девятнадцать");
        AddValueToNumberStringByGenderCaseDict(ref result, 20, "двадцать");
        AddValueToNumberStringByGenderCaseDict(ref result, 30, "тридцать");
        AddValueToNumberStringByGenderCaseDict(ref result, 40, "сорок");
        AddValueToNumberStringByGenderCaseDict(ref result, 50, "пятьдесят");
        AddValueToNumberStringByGenderCaseDict(ref result, 60, "шестьдесят");
        AddValueToNumberStringByGenderCaseDict(ref result, 70, "семьдесят");
        AddValueToNumberStringByGenderCaseDict(ref result, 80, "восемьдесят");
        AddValueToNumberStringByGenderCaseDict(ref result, 90, "девяносто");
        AddValueToNumberStringByGenderCaseDict(ref result, 100, "сто");
        AddValueToNumberStringByGenderCaseDict(ref result, 200, "двести");
        AddValueToNumberStringByGenderCaseDict(ref result, 300, "триста");
        AddValueToNumberStringByGenderCaseDict(ref result, 400, "четыреста");
        AddValueToNumberStringByGenderCaseDict(ref result, 500, "пятьсот");
        AddValueToNumberStringByGenderCaseDict(ref result, 600, "шестьсот");
        AddValueToNumberStringByGenderCaseDict(ref result, 700, "семьсот");
        AddValueToNumberStringByGenderCaseDict(ref result, 800, "восемьсот");
        AddValueToNumberStringByGenderCaseDict(ref result, 900, "девятьсот");

        return result;
    }

    private record NumberStringByGenderCase(string MaleCase, string? FemaleCase, string? MiddleCase)
    {
        public string GetMaleCase() => MaleCase;
        public string GetFemaleCase() => FemaleCase is null ? GetMaleCase() : FemaleCase;
        public string GetMiddleCase() => MiddleCase is null ? GetMaleCase() : MiddleCase;
    };

    protected static void AddValueToIndexStringByGrammarCaseDict(
        ref Dictionary<int, IndexStringByGrammarCase> dict,
        int index,
        string firstCase,
        string secondCase,
        string thirdCase,
        EGender gender)
    {
        dict.Add(index,new IndexStringByGrammarCase(firstCase, secondCase, thirdCase, gender));
    }

    protected static Dictionary<int, IndexStringByGrammarCase> GetIndexStringByGrammarCaseDict()
    {
        var result = new Dictionary<int, IndexStringByGrammarCase>();

        AddValueToIndexStringByGrammarCaseDict(ref result,1,"тысяча","тысячи","тысяч",EGender.FEMALE);
        AddValueToIndexStringByGrammarCaseDict(ref result, 2, "миллион", "миллиона", "миллионов",EGender.MALE);
        AddValueToIndexStringByGrammarCaseDict(ref result, 3, "миллиард", "миллиарда", "миллиардов",EGender.MALE);

        return result;
    }

    public record IndexStringByGrammarCase(string FirstCase, string SecondCase, string ThirdCase, EGender Gender)
    {
        string GetFirstCase() => FirstCase;
        string GetSecondCase() => SecondCase;
        string GetThirdCase() => ThirdCase;
        EGender GetEGender() => Gender;
    };

    private string GetOneNumberWord(int value, EGender gender)
    {
        // переделать на GetValueOrDefault
        var number = _numberStringByGenderCasesList.GetValueOrDefault(value);

        switch (gender)
        {
            case EGender.MALE:
                return number.GetMaleCase();
            case EGender.FEMALE:
                return number.GetFemaleCase();
            case EGender.MIDDLE:
                return number.GetMiddleCase();
        }
        // Мы сюда не должны дойти, как красиво написать я не знаю.
        return string.Empty;
    }

    private string GetIndexValueText(string indexValue, EGender gender = EGender.MALE)
    {
        var stringBuilder = new StringBuilder();
        int indexValueLength = indexValue.Length;
        bool isTeenAge = false; // Это флаг, что имеется значение от 10 до 19

        if (indexValueLength == 3)
        {
            stringBuilder.Append(GetOneNumberWord(int.Parse(indexValue.Substring(0, 1)) * 100, gender));
            stringBuilder.Append(" ");
        }

        if (indexValueLength > 1) 
        { 
            if (indexValue.Substring(1, 1) == "1")
            {
                stringBuilder.Append(GetOneNumberWord(int.Parse(indexValue.Substring(1, 2)), gender));
                stringBuilder.Append(" ");
                isTeenAge = true;
            } 
            else
            {
                stringBuilder.Append(GetOneNumberWord(int.Parse(indexValue.Substring(1, 1)) * 10, gender));
                stringBuilder.Append(" ");
            }
        }

        if (!isTeenAge)
        {
            stringBuilder.Append(GetOneNumberWord(int.Parse(indexValue.Substring(2, 1)) * 10, gender));
            stringBuilder.Append(" ");
        }

        return stringBuilder.ToString();
    }

    private string GetIndexNameGrammarCase(string indexValue, int indexNum)
    {
        int ;
    }

}
