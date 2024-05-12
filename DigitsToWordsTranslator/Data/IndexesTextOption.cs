using DigitsToWordsTranslator.ENum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitsToWordsTranslator.Data;

internal class IndexesTextOption
{
    private static Dictionary<ENumberIndex, EGender> indexGenderLink = GetIndexGenderOption();
    private EGender unitIndexGender;

    /// <summary>
    /// Получить справочник родов разрядов
    /// </summary>
    /// <returns></returns>
    private static Dictionary<ENumberIndex, EGender> GetIndexGenderOption()
    {
        var result  = new Dictionary<ENumberIndex, EGender>();

        // Добавляем информацию по тысячам, миллионам и миллиардам
        result.Add(ENumberIndex.THOUSAND, EGender.FEMALE);
        result.Add(ENumberIndex.MILLION, EGender.MALE);
        result.Add(ENumberIndex.BILLION, EGender.MALE);

        return result;
    }

    /// <summary>
    /// Конструктор объекта IndexesTextOption.
    /// </summary>
    /// <param name="unitIndexGender">Род разряда единиц</param>
    public IndexesTextOption(EGender unitIndexGender)
    {
        this.unitIndexGender = unitIndexGender;
    }

    /// <summary>
    /// Получить род разряда
    /// </summary>
    /// <param name="numberIndex">Разряд</param>
    /// <returns></returns>
    public EGender GetGender(ENumberIndex numberIndex)
    {
        return numberIndex == ENumberIndex.UNIT ?
            unitIndexGender :
            indexGenderLink.GetValueOrDefault(numberIndex);
    }

    
}

