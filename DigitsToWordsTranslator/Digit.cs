using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitsToWordsTranslator;

internal class Digit
{
    private List<String> _parsedDigitOnIndexes = new List<String>();
    private const int _indexSize = 3;
    private bool _isNegative;

    public Digit(int integerValue)
    {
        InitDigit(integerValue);
    }

    public void Show ()
    {
        foreach (String index in _parsedDigitOnIndexes)
        {
            Console.WriteLine(index);
        }
        Console.WriteLine($"_isNegative = {_isNegative}");
    }

    private void InitDigit(int integerValue)
    {
        // Обрабатываем знак числа
        if (integerValue < 0)
        {
            _isNegative = true;
            integerValue *= -1; // Меняем знак, чтобы можно было нормально порезать на символы.
        }

        // Разделяем число на массив символов по разряду числа
        string stringIntegerValue = Convert.ToString(integerValue);

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

            _parsedDigitOnIndexes.Add(substredString);
        }
    }
}
