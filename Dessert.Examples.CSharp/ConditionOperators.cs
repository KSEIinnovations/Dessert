﻿//
// ConditionOperators.cs
//
// Author(s):
//       Alessio Parma <alessio.parma@gmail.com>
//
// Copyright (c) 2012-2016 Alessio Parma <alessio.parma@gmail.com>
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

namespace DIBRIS.Dessert.Examples.CSharp
{
    using System;
    using System.Collections.Generic;

    public static class ConditionOperators
    {
        static IEnumerable<SimEvent> Process(SimEnvironment env)
        {
            var c1 = env.Timeout(3).And(env.Timeout(5)).And(env.Timeout(7));
            yield return c1;
            Console.WriteLine(env.Now); // 7
            Console.WriteLine(c1.Value.Contains(c1.Ev1)); // True
            Console.WriteLine(c1.Value.Contains(c1.Ev2)); // True
            Console.WriteLine(c1.Value.Contains(c1.Ev3)); // True

            var c2 = env.Timeout(7).Or(env.Timeout(5).Or(env.Timeout(3)));
            yield return c2;
            Console.WriteLine(env.Now); // 10
            Console.WriteLine(c2.Value.Contains(c2.Ev1)); // False
            Console.WriteLine(c2.Value.Contains(c2.Ev2)); // False
            Console.WriteLine(c2.Value.Contains(c2.Ev3)); // True
        }

        public static void Run()
        {
            var env = Sim.Environment();
            env.Process(Process(env));
            env.Run();
        }
    }
}