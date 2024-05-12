using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitsToWordsTranslator.ENum;

namespace DigitsToWordsTranslator.Data;

internal class NumberTextValueDict
{
    private static Dictionary<int, Dictionary<EGender, string>> dict = GetDict();

    public bool IsNeedToRaiseIfNotFound { get; set; }

    /// <summary>
    /// Конструктор объекта NumberTextValueDict
    /// </summary>
    /// <param name="isNeedToRaiseIfNotFound">Нужно ли рейзить ошибку, если не найдено значение</param>
    public NumberTextValueDict(bool isNeedToRaiseIfNotFound)
    {
        IsNeedToRaiseIfNotFound = isNeedToRaiseIfNotFound;
    }

    /// <summary>
    /// Получить значение в виде строки из справочника чисел.
    /// </summary>
    /// <param name="number">Число</param>
    /// <param name="gender">Род слова, по которому выполняется поиск числа</param>
    /// <param name="raiseIfNotFound">Рейзить ошибку, если не найдено значение</param>
    /// <returns>Строка числа</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public string GetValue(
        int number,
        EGender gender, 
        bool raiseIfNotFound = true)
    {
        if (!dict.ContainsKey(number))
        {
            
            if (raiseIfNotFound)
            {
                throw new ArgumentNullException();
            }
            else
            {
                return "";
            }
        }

        return GetValueByGenderOrDefault(
            numberTextValue: dict.GetValueOrDefault(number),
            gender: gender);
    }

    /// <summary>
    /// Получить значение в виде строки из справочника чисел.
    /// Если по данному роду значение не найдено, то выбирается дефолтное - мужского рода.
    /// </summary>
    /// <param name="numberTextValue"></param>
    /// <param name="gender"></param>
    /// <returns></returns>
    private static string GetValueByGenderOrDefault(
        Dictionary<EGender, string> numberTextValue, 
        EGender gender)
    {
        if (numberTextValue.TryGetValue(gender, out var value))
        {
            return value; 
        } 
        else
        {
           return GetDefault(numberTextValue);
        }
    }

    /// <summary>
    /// Получить дефолтное значение.
    /// Дефолтное значение - мужской род
    /// </summary>
    /// <param name="numberTextValue"></param>
    /// <returns></returns>
    private static string GetDefault(Dictionary<EGender, string> numberTextValue)
         => numberTextValue.GetValueOrDefault(EGender.MALE);

    /// <summary>
    /// Получить справочник, используется для инициализации статического Dictionary
    /// </summary>
    /// <returns></returns>
    private static Dictionary<int, Dictionary<EGender, string>> GetDict()
    {
        Dictionary<int, Dictionary<EGender, string>> result = [];

        AddToDict(ref result, 0, "ноль");
        AddToDict(ref result, 1, "один", "одна", "одно");
        AddToDict(ref result, 2, "два", "две");
        AddToDict(ref result, 3, "три");
        AddToDict(ref result, 4, "четыре");
        AddToDict(ref result, 5, "пять");
        AddToDict(ref result, 6, "шесть");
        AddToDict(ref result, 7, "семь");
        AddToDict(ref result, 8, "восемь");
        AddToDict(ref result, 9, "девять");
        AddToDict(ref result, 10, "десять");
        AddToDict(ref result, 11, "одиннадцать");
        AddToDict(ref result, 12, "двенадцать");
        AddToDict(ref result, 13, "тринадцать");
        AddToDict(ref result, 14, "четырнадцать");
        AddToDict(ref result, 15, "пятнадцать");
        AddToDict(ref result, 16, "шетнадцать");
        AddToDict(ref result, 17, "семнадцать");
        AddToDict(ref result, 18, "вомемнадцать");
        AddToDict(ref result, 19, "девятнадцать");
        AddToDict(ref result, 20, "двадцать");
        AddToDict(ref result, 30, "тридцать");
        AddToDict(ref result, 40, "сорок");
        AddToDict(ref result, 50, "пятьдесят");
        AddToDict(ref result, 60, "шестьдесят");
        AddToDict(ref result, 70, "семьдесят");
        AddToDict(ref result, 80, "восемьдесят");
        AddToDict(ref result, 90, "девяносто");
        AddToDict(ref result, 100, "сто");
        AddToDict(ref result, 200, "двести");
        AddToDict(ref result, 300, "триста");
        AddToDict(ref result, 400, "четыреста");
        AddToDict(ref result, 500, "пятьсот");
        AddToDict(ref result, 600, "шестьсот");
        AddToDict(ref result, 700, "семьсот");
        AddToDict(ref result, 800, "восемьсот");
        AddToDict(ref result, 900, "девятьсот");

        return result;
    }

    /// <summary>
    /// Добавить информацию в справочник
    /// </summary>
    /// <param name="dict">Справочник(ссылка)</param>
    /// <param name="number">Число справочника - является ключем</param>
    /// <param name="maleGenderValue">Значение для мужского рода</param>
    /// <param name="femaleGenderValue">Значение для женского рода</param>
    /// <param name="middleGenderValue">Значение для среднего рода</param>
    private static void AddToDict(
        ref Dictionary<int, Dictionary<EGender, string>> dict,
        int number,
        string maleGenderValue,
        string femaleGenderValue = null,
        string middleGenderValue = null)
    {
        Dictionary<EGender, string> fullGenderValue = [];

        // Мужской род есть всегда
        fullGenderValue.Add(EGender.MALE, maleGenderValue);

        // Не храним пустые значения, будем их обрабатывать при получении
        if (femaleGenderValue != null)
        {
            fullGenderValue.Add(EGender.FEMALE, femaleGenderValue);
        }

        if (middleGenderValue != null)
        {
            fullGenderValue.Add(EGender.MIDDLE, middleGenderValue);
        }

        dict.Add(number, fullGenderValue);
    }
}
