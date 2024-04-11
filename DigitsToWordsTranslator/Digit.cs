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
    }

    private void InitDigit(int integerValue)
    {
        string stringIntegerValue = Convert.ToString(integerValue);

        int greaterIndexDigitsCountMod = stringIntegerValue.Length % _indexSize;
        int greaterIndexDigitsCount = greaterIndexDigitsCountMod == 0 ? _indexSize : greaterIndexDigitsCountMod; // Определяем кол-во цифр в старшем разряде

        // Определяем кол-во итераций цикла, для 9 символов это будет 3 итерации, для 10, 11, 12 символов это будет 4 итерации.
        int loopCount = stringIntegerValue.Length / _indexSize + (greaterIndexDigitsCount == 3 ? 0 : 1);

        for (int i = 0; i < loopCount; i++)
        {
            int substrFromIndex = stringIntegerValue.Length - _indexSize * (i + 1);
            // Если это последняя итерация цикла и кол-во цифр в старшем разряде не равно 3
            int substrLength = (i + 1 == loopCount && greaterIndexDigitsCount != _indexSize) ? greaterIndexDigitsCount : _indexSize;
            string substredString = stringIntegerValue.Substring(substrFromIndex, substrLength);


            _parsedDigitOnIndexes.Add(substredString);
        }
    }
}
