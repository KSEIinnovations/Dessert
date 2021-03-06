﻿// 
// EventTests.cs
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

namespace DIBRIS.Dessert.Tests.Events
{
    using System;
    using Dessert.Events;
    using NUnit.Framework;
    using SimEvents = System.Collections.Generic.IEnumerable<SimEvent>;

    sealed class EventTests : TestBase
    {
        SimEvents Triggerer_UnusedInCondition(SimEvent<object> token, Action<SimEvent<object>> triggerer)
        {
            var ev = Env.Event();
            Env.Process(Triggerer_UnusedInCondition_Aux(ev));
            yield return Env.AnyOf(token, ev);
            triggerer(token);
        }

        SimEvents Triggerer_UnusedInCondition_Aux(SimEvent<object> token)
        {
            yield return Env.Timeout(5);
            token.Succeed();
        }

        [Test]
        public void Triggerer_UnusedInCondition_Fail()
        {
            var ev = Env.Event();
            Env.Process(Triggerer_UnusedInCondition(ev, e => e.Fail()));
            Env.Run();
        }

        [Test]
        public void Triggerer_UnusedInCondition_FailWithValue()
        {
            var ev = Env.Event();
            Env.Process(Triggerer_UnusedInCondition(ev, e => e.Fail(new Exception())));
            Env.Run();
        }

        [Test]
        public void Triggerer_UnusedInCondition_Succeed()
        {
            var ev = Env.Event();
            Env.Process(Triggerer_UnusedInCondition(ev, e => e.Succeed()));
            Env.Run();
        }

        [Test]
        public void Triggerer_UnusedInCondition_SucceedWithValue()
        {
            var ev = Env.Event();
            Env.Process(Triggerer_UnusedInCondition(ev, e => e.Succeed(5)));
            Env.Run();
        }
    }
}