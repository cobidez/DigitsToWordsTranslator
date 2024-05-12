using DigitsToWordsTranslator.ENum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitsToWordsTranslator.Data;

/// <summary>
/// Рекорд с опциями индекса
/// </summary>
internal record IndexOption
{
    /// <summary>
    /// Род индекса
    /// </summary>
    public readonly EGender gender;

    /// <summary>
    /// Словарь с кейсами падежей слов в зависимости от чисел.
    /// </summary>
    public readonly Dictionary<EGrammarCase, string> numberGramarCase = [];

    public IndexOption() : this(EGender.MALE,"", "", "")
    {
    }

    /// <summary>
    /// Инициализирует объект IndexOption
    /// </summary>
    /// <param name="gender"></param>
    /// <param name="firstCaseValue"></param>
    /// <param name="secondCaseValue"></param>
    /// <param name="thirdCaseValue"></param>
    public IndexOption(
        EGender gender, 
        string firstCaseValue,
        string secondCaseValue,
        string thirdCaseValue)
    {
        this.gender = gender;
        numberGramarCase.Add(EGrammarCase.FirstCase, firstCaseValue);
        numberGramarCase.Add(EGrammarCase.SecondCase, secondCaseValue);
        numberGramarCase.Add(EGrammarCase.ThirdCase, thirdCaseValue);
    }

    /// <summary>
    /// Получить информацию о кейсе числа
    /// </summary>
    /// <param name="grammarCase">Кейс</param>
    /// <returns>Значение</returns>
    public string GetGrammarCaseValue(EGrammarCase grammarCase) 
        => numberGramarCase.GetValueOrDefault(grammarCase);
}
