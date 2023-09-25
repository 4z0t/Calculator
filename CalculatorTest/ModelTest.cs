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
        [TestCase("1+2", 1d + 2)]
        [TestCase("1/2", 0.5)]
        [TestCase("0.5", 0.5)]
        [TestCase(".5", 0.5)]
        [TestCase("0.5+9,5", 0.5 + 9.5)]
        [TestCase("2/1", 2)]
        [TestCase("-(-1)", 1)]
        [TestCase("(1+2)*3", 9)]
        [TestCase("(1+2)/(2-1)", 3)]
        [TestCase("sadsadsad", 0, true)]
        [TestCase("sadsad+2", 0, true)]
        [TestCase("qwq+", 0, true)]
        [TestCase("2+", 0, true)]
        [TestCase("(2", 0, true)]
        [TestCase("4+4)", 0, true)]
        [TestCase("4+4+", 0, true)]
        [TestCase("4/5*3", 4d / 5 * 3)]
        [TestCase("4^3", 64)]
        public void Test(string input, double output, bool isError = false)
        {
            var view = Mock.Of<IView>();
            var model = new Model();
            var presenter = new Presenter(view, model);

            var (r, err) = presenter.ProcessString(input);
            if (isError)
                Assert.AreEqual(isError, err);
            else
                Assert.AreEqual(output, r);



        }

    }

}

