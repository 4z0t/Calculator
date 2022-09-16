using Calculator.Interfaces;
using Calculator;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorTest
{
    [TestFixture]
    class ModelTest
    {





        [SetUp]
        public void Setup()
        {

        }

         const string err = "Incorrect input";
        [TestCase("1+2","3")]
        [TestCase("1/2","0,5")]
        [TestCase("0.5","0,5")]
        [TestCase("0.5+9,5","10")]
        [TestCase("2/1","2")]
        [TestCase("(1+2)*3","9")]
        [TestCase("(1+2)/(2-1)","3")]
        [TestCase("sadsadsad",err)]
        [TestCase("sadsad+2",err)]
        [TestCase("qwq+",err)]
        [TestCase("2+",err)]
        [TestCase("(2",err)]
        [TestCase("4+4)",err)]
        public void Test(string input, string output)
        {
            var view = Mock.Of<IView>();
            var model = new Model();
            var presenter = new Presenter(view, model);

            foreach (char c in input)
            {
                presenter.OnCharInput(c);
            }
            model.Calculate(out string r);
            Assert.AreEqual(output, r);



        }

    }

}

