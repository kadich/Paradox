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

            for (int i = 0; i < 100; i++) //цикл для многократного повторения стратегий
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

                Thread.Sleep(2); //приостановка потока для более случайного Random

                //создание экземпляра класса Random и симуляции случайного выбора номера "двери" участником
                Random rnd2 = new Random((int)DateTime.Now.Ticks);
                int choice1 = rnd2.Next(0, 3);

                int shown = 0; //переменная для записи номер показанной "двери"
                //симуляция открытия "двери", за которой стоит "коза"
                for (int k = 0; k < 3; k++)
                {
                    //условия: за "дверью" не должна быть "машина" и "дверь" не должна быть выбрана участником
                    if (!doors[k] & k != choice1)
                    {
                        shown = k;
                        break;
                    }
                }

                //симуляция смены выбора "двери" участником
                int choice2 = 0;
                for (int j = 0; j < 3; j++)
                {
                    //условия: "дверь" не должна быть выбрана и "дверь" не должна быть открыта
                    if (j != choice1 & j != shown)
                    {
                        choice2 = j;
                        break;
                    }
                }

                //подсчет количества верных выборов
                if (doors[choice1])
                {
                    result[0].WinRate1++;
                }
                else
                {
                    result[0].WinRate2++;
                }
            }

            return View("ResultPage", result);
        }
    }
}