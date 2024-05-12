using DigitsToWordsTranslator.ENum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitsToWordsTranslator.Data;

internal class IndexesTextOption
{
    private static Dictionary<ENumberIndex, IndexOption> indexOptionDict = GetIndexOptionDict();
    private IndexOption unitIndexOption;

    /// <summary>
    /// Получить справочник родов разрядов
    /// </summary>
    /// <returns></returns>
    private static Dictionary<ENumberIndex, IndexOption> GetIndexOptionDict()
    {
        var result  = new Dictionary<ENumberIndex, IndexOption>();

        // Добавляем информацию по тысячам
        result.Add(
            ENumberIndex.THOUSAND,
            new IndexOption(
                EGender.FEMALE,
                "тысяча",
                "тысячи",
                "тысяч"));

        // Добавляем информацию о миллионах
        result.Add(
            ENumberIndex.MILLION,
            new IndexOption(
                EGender.MALE,
                "миллион",
                "миллиона",
                "миллионов"));

        // Добавляем информацию о миллиардах
        result.Add(
            ENumberIndex.BILLION,
            new IndexOption(
                EGender.MALE,
                "миллиард",
                "миллиарда",
                "миллиардов"));
        return result;
    }

    /// <summary>
    /// Конструктор объекта IndexesTextOption.
    /// </summary>
    /// <param name="unitIndexOption">Настройка разряда единиц</param>
    public IndexesTextOption(IndexOption unitIndexOption)
    {
        this.unitIndexOption = unitIndexOption;
    }

    /// <summary>
    /// Получить настройку разряда
    /// </summary>
    /// <param name="numberIndex">Разряд</param>
    /// <returns></returns>
    public IndexOption GetIndexOption(ENumberIndex numberIndex)
    {
        return numberIndex == ENumberIndex.UNIT ?
            unitIndexOption :
            indexOptionDict.GetValueOrDefault(numberIndex);
    }
}

