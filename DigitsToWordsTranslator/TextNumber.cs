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
    private readonly IndexesTextOption indexesTextOption; // Настройки разрядов
    private NumberTextValueDict numberTextValueDict = new (false); // Не рейзимся, если не будет найдено значение в справочнике

    /// <summary>
    /// Конструктор, инициализирует целую часть
    /// </summary>
    /// <param name="integerValue"></param>
    public TextNumber(
        int integerValue,
        IndexOption unitIndexOption)
    {
        InitNumberSign(integerValue);
        InitIntegerPart(integerValue);
        indexesTextOption = new IndexesTextOption(unitIndexOption);
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
    /// <returns>Строка текста числа</returns>
    public string ToStringText()
    {
        var result = new StringBuilder();

        if (isNegative)
        {
            result.Append( "минус " );
        }

        // Бежим по распаршеным разрядам от больших разрядов к меньшим и формируем текст
        for (int i = parsedIntegerPartOnIndexesList.Length - 1; i >= 0; i--)
        {
            var indexValue = parsedIntegerPartOnIndexesList[i];

            // Определяем настройки
            EGrammarCase grammarCase = GetGrammarCaseForIndexNumber(indexValue);
            IndexOption indexOption = indexesTextOption.GetIndexOption((ENumberIndex)i);

            // Обрабатываем сотни (третья цифра справа)
            if (indexValue >= 100)
            {
                result.Append(
                    numberTextValueDict.GetValue(
                        (indexValue / 100) * 100, // Обрезаем все кроме сотен
                        indexOption.gender) + " ");
            }

            // Обрабатываем, если это десятки от 10 до 19 (первая и вторая цифра справа)
            if (indexValue >= 10 && indexValue <= 19)
            {
                result.Append(
                    numberTextValueDict.GetValue(
                        indexValue,
                        indexOption.gender) + " ");
            }
            // Обрабатываем, если это НЕ десятки от 10 до 19
            else
            {
                int dozenValue = indexValue % 100;
                bool isDozenExists = dozenValue > 9;

                // Если это десятки (вторая цифра справа)
                if (isDozenExists)
                {
                    result.Append(
                        numberTextValueDict.GetValue(
                            (dozenValue / 10) * 10, // Обрезаем все кроме десятков
                            indexOption.gender) + " ");
                }

                int unitsValue = indexValue % 10;
                if (!(unitsValue == 0 && isDozenExists))
                {
                    result.Append(
                        numberTextValueDict.GetValue(
                            unitsValue, // Только единицы
                            indexOption.gender) + " ");
                }
            }

            result.Append(indexOption.numberGramarCase.FirstCase + " ");
        }

        return result.ToString();
    }

    /// <summary>
    /// Отпечатать число
    /// </summary>
    /// <param name="unitIndexGender"></param>
    public void PrintStringText()
    {
        Console.WriteLine(ToStringText());
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

    /// <summary>
    /// Определить грамматический кейс для числа
    /// </summary>
    /// <param name="indexNumber"></param>
    /// <returns></returns>
    private EGrammarCase GetGrammarCaseForIndexNumber(int indexNumber)
    {
        // Смотрим только на часть с единицами и десятками (поэтому остаток от деления на 100)
        int checkingNumber = indexNumber % 100;

        if (checkingNumber >= 11 && checkingNumber <= 19)
        {
            return EGrammarCase.ThirdCase;
        }
        
        // Если это не числа от 11 до 19, то проверяем последнюю цифру (поэтому остаток от деления на 10)
        switch (checkingNumber % 10)
        {
            case 1:
                return EGrammarCase.FirstCase;
            case 2:
            case 3:
            case 4:
                return EGrammarCase.SecondCase;
            default:
                return EGrammarCase.ThirdCase;
        }
    }

}
