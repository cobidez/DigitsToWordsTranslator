using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitsToWordsTranslator;

internal class TextNumber
{
   // public static readonly Dictionary<int, IndexTextByGrammarCase> indexTextByGrammarCaseList = GetTextStringByGrammarCaseDict(); // Хранит шаблоны разрядов типа "тысяча", "миллион" и тд
    private static readonly int indexSize = 3; // Кол-во символов в разряде: 123 456 789

    private int indexesCount; // Кол-во разрядов (под разрядами подразумеваются: единицы, тысячи, миллионы, миллиарды)
    private Dictionary<int, int> parsedIntegerPartOnIndexesList = []; // Хранит индексы целой части
    private bool isNegative; // Негативное ли число

    public TextNumber(int integerValue)
    {
        InitNumberSign(integerValue);
        InitIntegerPart(integerValue);
    }

    public void ShowStructure()
    {
        Console.WriteLine($"_isNegative = {isNegative}");
        Console.WriteLine("IntegerPart");

        foreach (var item in parsedIntegerPartOnIndexesList)
        {
            Console.WriteLine(item.Value);
        }
    }

    private void InitIntegerPart(int integerValue)
    {
        if (integerValue < 0)
        {
            integerValue *= -1; // Меняем знак, чтобы можно было нормально порезать на символы.
        }

        int numberLength = integerValue.ToString().Length;

        // Кол-во разрядов определяется как длина числа, деленная на кол-во цифр в разряде. + Необходимо добавить еще один разряд, если длина числа не кратна 3.
        indexesCount = numberLength / indexSize + (numberLength % indexSize == 0 ? 0 : 1);

        for (int i = 0; i < indexesCount; i++)
        {
            parsedIntegerPartOnIndexesList.Add(
                i,
                (integerValue / (int)Math.Pow( 10.0, i * indexSize)) % 1000); // Формула: Число / 10^i*3 % 1000
        }
    }

    private void InitNumberSign(int integerValue)
    {
        // Обрабатываем знак числа
        if (integerValue < 0)
        {
            isNegative = true;
        }
    }
}
