﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS_WPF.Model
{
    public sealed record Country
    {
        public required string Name { get; init; }

        public override string ToString() => Name;
    }
}
