using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AirTraficMonitoring.Test.Unit
{
    public class Class1
    {

        [Test]

        public void Simple_math_Test()//Got a theory about Jenkins not being able to run the solution because there is no test to run
        {
            Assert.That(2 + 2, Is.EqualTo(4));
        }
    }
}
