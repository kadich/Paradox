using Paradox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Paradox.Controllers
{
    public class CheckController : Controller
    {
        public ActionResult StartPage()
        {
            return View();
        }

        public ActionResult Check()
        {
            var result = new List<ResultModel> //коллекция для записи результатов 
            {
                new ResultModel() {WinRate1 = 0, WinRate2 = 0}
            };

            bool[] doors = new bool[3]; //создание массива размером [3], представляющий собой 3 "двери"

            const int repetitions = 1000;
            for (int i = 0; i < repetitions; i++) //цикл для многократного повторения стратегий
            {
                //очистка массива перед очередным проходом цикла
                for (int d = 0; d < doors.Length; d++)
                {
                    doors[d] = false;
                }

                //создание экземпляра класса Random и присвоения случайной "двери" (ячейке массива)
                //значение "машина" (значение true)
                Random rnd1 = new Random((int)DateTime.Now.Ticks);
                doors[rnd1.Next(0, 3)] = true;

                Thread.Sleep(1); //приостановка потока для более случайного Random

                //создание экземпляра класса Random и симуляции случайного выбора номера "двери" участником
                Random rnd2 = new Random((int)DateTime.Now.Ticks);
                int choice1 = rnd2.Next(0, 3);

                // вторая стратегия автоматически становится выигрышной, в случае неудачи первой стартегии,
                // а значит логика подсчета результатов будет выглядить следующим образом:
                
                if (doors[choice1])
                {
                    result[0].WinRate1++;
                }
                else
                {
                    result[0].WinRate2++;
                }
            }

            result[0].WinRate1 = result[0].WinRate1 / (repetitions / 100);
            result[0].WinRate2 = result[0].WinRate2 / (repetitions / 100);

            return View("ResultPage", result);
        }
    }
}