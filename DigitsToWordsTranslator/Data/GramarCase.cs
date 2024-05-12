using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitsToWordsTranslator.Data;

internal record NumberGramarCase
{
    /// <summary>
    /// Первый кейс, когда в единственном числе. 
    /// Один кот.
    /// </summary>
    public readonly string FirstCase;

    /// <summary>
    /// Второй кейс, когда значение от 2 до 4.
    /// Два кота, тридцать три кота
    /// </summary>
    public readonly string SecondCase;

    /// <summary>
    /// Третий кейс, все остальные варианты, плюс от 10 до 19.
    /// Девятнадцать котов, тридцать шесть котов.
    /// </summary>
    public readonly string ThirdCase;

    public NumberGramarCase(string firstCase,string secondCase,string thirdCase)
    {
        FirstCase = firstCase;
        SecondCase = secondCase;
        ThirdCase = thirdCase;
    }
}
