﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace To_Do_List.ViewModels
{
    public class ToDoViewModel
    {
        public int Id { get; set; }

        public DateTime Date{ get; set; }

        public string Name { get; set; }

        public bool Execution { get; set; }
    }
}
