using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using TextModell.Classes;

namespace TextModell
{
    class Program
    {
       
               static void Main(string[] args)
            {
                using (TextReader textReader = new StreamReader(ConfigurationManager.AppSettings["sourceTextFIleURL"]))
                {
                    SeparatorContainer sp = new SeparatorContainer();
                    Parser parser = new Parser(sp);
                    Text text = parser.Parse(textReader);

                    // Отображать все предложения заданного текста в порядке возрастания количества слов в каждом из них.

                    text.SentancesOrderedByWordCount.ForEach(s =>
                    {
                        Console.WriteLine(s.Chars);
                        Console.WriteLine("=={0}==----------------------------------", s.WordCount);
                    });

                    // Во всех вопросительных предложениях текст печатает слово без повторения заданной длины.
                    int length = 3;
                    Console.WriteLine(
                        "In all interrogative sentences of the text print the word without repetition of a given length = {0}.",
                        length);
                    text.InterrogativeSentancesWordsDistinct(length).ForEach(w =>
                    {
                        Console.WriteLine(w.Chars);
                    });

                    // Из текста удалить все слова заданной длины, начиная с согласного письма
                    text.RemoveConsonantWords(3);

                    Console.WriteLine("----------------------------");

                    Console.WriteLine(
                        "In all interrogative sentences of the text print the word without repetition of a given length = {0}.",
                        length);
                    text.InterrogativeSentancesWordsDistinct(length).ForEach(w =>
                    {
                        Console.WriteLine(w.Chars);
                    });

                    Console.WriteLine("----------------------------");

                    //В предложении слова заданной длины заменяются указанной подстрокой,
                    // длина которых может не совпадать с длиной слова.
                    text.Content[2].Replace(6, "всего хорошего,друг ");


                    // Сохранить измененный текст по умолчанию out.text
                    text.ToFile(ConfigurationManager.AppSettings["targetTextFIleURL"]);
                    Console.ReadLine();
                }
            }
        }
    }
