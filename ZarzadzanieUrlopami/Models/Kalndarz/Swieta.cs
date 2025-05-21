using System;
using System.Collections.Generic;


namespace ZarzadzanieUrlopami.Models.Kalndarz
{
    public static class Swieta
    {
        public static List<DateTime> getSwieta(int year)
        {
            var holidays = new List<DateTime>
        {
            new DateTime(year, 1, 1),   // Nowy Rok
            new DateTime(year, 1, 6),   // Trzech Króli
            new DateTime(year, 5, 1),   // Święto Pracy
            new DateTime(year, 5, 3),   // Święto Konstytucji
            new DateTime(year, 8, 15),  // Wniebowzięcie NMP
            new DateTime(year, 11, 1),  // Wszystkich Świętych
            new DateTime(year, 11, 11), // Święto Niepodległości
            new DateTime(year, 12, 25), // Boże Narodzenie
            new DateTime(year, 12, 26)  // Drugi dzień BN
        };


            DateTime easterSunday = getWielkanoc(year);
            holidays.Add(easterSunday);             // Wielkanoc
            holidays.Add(easterSunday.AddDays(1));  // Poniedziałek Wielkanocny
            holidays.Add(easterSunday.AddDays(49)); // Zielone Świątki
            holidays.Add(easterSunday.AddDays(60)); // Boże Ciało

            return holidays;
        }

        private static DateTime getWielkanoc(int year)
        {
            int a = year % 19;
            int b = year / 100;
            int c = year % 100;
            int d = b / 4;
            int e = b % 4;
            int f = (b + 8) / 25;
            int g = (b - f + 1) / 3;
            int h = (19 * a + b - d - g + 15) % 30;
            int i = c / 4;
            int k = c % 4;
            int l = (32 + 2 * e + 2 * i - h - k) % 7;
            int m = (a + 11 * h + 22 * l) / 451;
            int month = (h + l - 7 * m + 114) / 31;
            int day = ((h + l - 7 * m + 114) % 31) + 1;

            return new DateTime(year, month, day);
        }
    }
}