﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Activity;

namespace Midterm
{
    class Deposit
    {
        
        public static void InsertMoney(string email)
        {
            Connection connection = new Connection();
            if (Midterm.User.isLoggedIn)
            {

                Console.Write(@"
Enter Deposit Amount (USD). It will be converted into USDT
>>");
                float money = float.Parse(Console.ReadLine());
                
                connection.InsertBalance(money, email);
            }

        }
    }
}
