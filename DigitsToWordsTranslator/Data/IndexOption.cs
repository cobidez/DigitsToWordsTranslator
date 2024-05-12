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
    /// Рекорд с кейсами падежей слов в зависимости от чисел.
    /// </summary>
    public readonly NumberGramarCase numberGramarCase;

    public IndexOption(EGender gender, NumberGramarCase numberGramarCase)
    {
        this.gender = gender;
        this.numberGramarCase = numberGramarCase;
    }

    /// <summary>
    /// Инициирует новый объект IndexOption по входным параметрам.
    /// </summary>
    /// <param name="gender">Род разряда</param>
    /// <param name="firstCase">Текст разряда в 1 кейсе</param>
    /// <param name="secondCase">Текст разряда во 2 кейсе</param>
    /// <param name="thirdCase">Текст разряда в 3 кейсе</param>
    /// <returns>Объект типа IndexOption</returns>
    public static IndexOption GetIndexOptionObjByParams(
        EGender gender,
        string firstCase,
        string secondCase,
        string thirdCase)
    {
        return new IndexOption(
            gender,
            new NumberGramarCase(
                firstCase,
                secondCase,
                thirdCase));
    }

    /// <summary>
    /// Рекорд с кейсами падежей слов в зависимости от чисел.
    /// </summary>
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

        public NumberGramarCase(string firstCase, string secondCase, string thirdCase)
        {
            FirstCase = firstCase;
            SecondCase = secondCase;
            ThirdCase = thirdCase;
        }
    }
}
