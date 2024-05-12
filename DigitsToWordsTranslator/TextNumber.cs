using DigitsToWordsTranslator.Data;
using DigitsToWordsTranslator.ENum;
using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitsToWordsTranslator;

internal class TextNumber
{
    private static readonly int indexSize = 3; // Кол-во символов в разряде: 123 456 789

    private int[] parsedIntegerPartOnIndexesList; // Хранит индексы целой части
    private bool isNegative; // Негативное ли число

    /// <summary>
    /// Конструктор, инициализирует целую часть
    /// </summary>
    /// <param name="integerValue"></param>
    public TextNumber(int integerValue)
    {
        InitNumberSign(integerValue);
        InitIntegerPart(integerValue);
    }

    /// <summary>
    /// Отпечатать структуру числа
    /// </summary>
    public void PrintStructure()
    {
        Console.WriteLine($"_isNegative = {isNegative}");
        Console.WriteLine("IntegerPart:");
        
        for (int i = 0; i < parsedIntegerPartOnIndexesList.Length; i++)
        {
            Console.WriteLine($"\t{Enum.GetName(typeof(ENumberIndex),i)} index = {parsedIntegerPartOnIndexesList[i]}");
        }
    }

    /// <summary>
    /// Получить текст числа.
    /// </summary>
    /// <returns></returns>
    public string ToStringText(EGender unitIndexGender)
    {
        var numberTextValueDict = new NumberTextValueDict(false);
        var indexesTextOption = new IndexesTextOption(unitIndexGender);

        StringBuilder result = new StringBuilder();

        if (isNegative)
        {
            result.Append( "минус " );
        }

        // Бежим по распаршеным разрядам от больших разрядов к меньшим и формируем текст
        for (int i = parsedIntegerPartOnIndexesList.Length; i > 0; i--)
        {



            result.Append(
                numberTextValueDict.GetValue(
                    parsedIntegerPartOnIndexesList[i],
                    indexesTextOption.GetGender((ENumberIndex)i)
                    ) + " ");
        }

        return result.ToString();
    }

    /// <summary>
    /// Отпечатать число
    /// </summary>
    /// <param name="unitIndexGender"></param>
    public void PrintStringText(EGender unitIndexGender)
    {
        Console.WriteLine(ToStringText(unitIndexGender));
    }

    /// <summary>
    /// Проинициализировать целую часть
    /// </summary>
    /// <param name="integerValue"></param>
    private void InitIntegerPart(int integerValue)
    {
        if (integerValue < 0)
        {
            integerValue *= -1; // Меняем знак, чтобы можно было нормально порезать на символы.
        }

        int numberLength = integerValue.ToString().Length;

        // Кол-во разрядов определяется как длина числа, деленная на кол-во цифр в разряде. + Необходимо добавить еще один разряд, если длина числа не кратна 3.
        int indexesCount = numberLength / indexSize + (numberLength % indexSize == 0 ? 0 : 1);

        // Инициализируем массив
        parsedIntegerPartOnIndexesList = new int[indexesCount];

        for (int i = 0; i < indexesCount; i++)
        {
            // Формула: Число / 10^i*3 % 1000
            parsedIntegerPartOnIndexesList[i] = (integerValue / (int)Math.Pow( 10.0, i * indexSize)) % 1000; 
        }
    }

    /// <summary>
    /// Проинициализировать знак
    /// </summary>
    /// <param name="integerValue"></param>
    private void InitNumberSign(int integerValue)
    {
        // Обрабатываем знак числа
        if (integerValue < 0)
        {
            isNegative = true;
        }
    }

    private 

}
