using DigitsToWordsTranslator.Data;
using DigitsToWordsTranslator.ENum;

namespace DigitsToWordsTranslator;

internal class Program
{
    static void Main(string[] args)
    {
        bool isParsedInteger;

        do
        {
            Console.Write("Введите число: ");

            isParsedInteger = int.TryParse(Console.ReadLine(), out int enteredInteger);

            if (!isParsedInteger)
            {
                Console.WriteLine("Введенный текст не является целым числом!");
                continue;
            }

            char enteredAddObjFlag;

            while (true)
            {
                Console.Write("Хотите ли Вы добавить объект для преобразования числа? (1 - да, 2 - нет): ");
                enteredAddObjFlag = Console.ReadKey().KeyChar;
                Console.WriteLine();

                if ((new char[] { '1', '2' }.Contains(enteredAddObjFlag)))
                {
                    break;
                } 
                else
                {
                    Console.WriteLine($"Вы ввели {enteredAddObjFlag}, необходимо ввести 1 или 2!") ;
                }
            }

            IndexOption indexOption;

            // Если требуется указать информацию об объекте.
            if (enteredAddObjFlag == '1')
            {
                char enteredGenderFlag;

                while (true)
                {
                    Console.Write("Введите грамматический род объекта (1 - мужской, 2 - женский, 3 - средний): ");
                    enteredGenderFlag = Console.ReadKey().KeyChar;
                    Console.WriteLine();

                    if ((new char[] { '1', '2', '3' }.Contains(enteredGenderFlag)))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Вы ввели {enteredGenderFlag}, необходимо ввести 1, 2 или 3!");
                    }
                }

                EGender gender;
                switch (enteredGenderFlag)
                {
                    case '1':
                        gender = EGender.MALE;
                        break;
                    case '2':
                        gender = EGender.FEMALE;
                        break;
                    case '3':
                        gender = EGender.MIDDLE;
                        break;
                    default:
                        gender = EGender.MALE; // Просто потому что иначе ниже не исполняется.
                        break;
                }

                Console.Write("Введите наименование объекта с числительным 1 (например \"кот\"): ");
                string firstCase = Console.ReadLine();

                Console.Write("Введите наименование объекта с числительным 2 (например \"кота\"): ");
                string secondCase = Console.ReadLine();

                Console.Write("Введите наименование объекта с числительным 5 (например \"котов\"): ");
                string thirdCase = Console.ReadLine();

                indexOption = new IndexOption(gender, firstCase, secondCase, thirdCase);
            } 
            // Если не требуется информация об объекте
            else
            {
                indexOption = new IndexOption();
            }

            var textNumber = new TextNumber(enteredInteger, indexOption);
            textNumber.PrintStringText();

            Console.Write("Для выхода нажмите \"Q\", для повтора нажмите любую кнопку: ");
            ConsoleKey enteredRepeatFlag = Console.ReadKey().Key;
            Console.WriteLine();

            if (enteredRepeatFlag == ConsoleKey.Q)
            {        
                break;
            } 
            else
            {
                Console.WriteLine("-------------------");
            }

        } while (isParsedInteger);

    }
}
