using NUnit.Framework;
using Moq;
using Calculator;
using Calculator.Interfaces;

namespace CalculatorTest
{
    [TestFixture]
    public class Tests
    {
       public enum PresenterTestType
        {
            Number, Operation,Enter, Open, Close
        }



        [SetUp]
        public void Setup()
        {

        }

        [TestCase('(',PresenterTestType.Open)]
        [TestCase(')',PresenterTestType.Close)]
        [TestCase('+',PresenterTestType.Operation)]
        [TestCase('-',PresenterTestType.Operation)]
        [TestCase('/',PresenterTestType.Operation)]
        [TestCase('*',PresenterTestType.Operation)]
        [TestCase('1',PresenterTestType.Number)]
        [TestCase('a',PresenterTestType.Number)]
        [TestCase('A',PresenterTestType.Number)]
        [TestCase('4',PresenterTestType.Number)]
        [TestCase('0',PresenterTestType.Number)]
        [TestCase(',',PresenterTestType.Number)]
        [TestCase('.',PresenterTestType.Number)]
        public void Test(char c,PresenterTestType t)
        {

            var moqView = new Mock<IView>();
            var moqModel = new Mock<IModel>();
            var presenter = new Presenter(moqView.Object, moqModel.Object);


            presenter.OnCharInput(c);

           if(t==PresenterTestType.Open) moqModel.Verify(m => m.OpenBracket(), Times.Once());
           if(t==PresenterTestType.Close) moqModel.Verify(m => m.CloseBracket(), Times.Once());
           if(t==PresenterTestType.Operation) moqModel.Verify(m => m.AddOperator(It.IsAny<Operation>()), Times.Once());
           if(t==PresenterTestType.Number) moqModel.Verify(m => m.AddToNumber(It.IsAny<char>()), Times.Once());
           //if(isEnter) moqModel.Verify(m => m.Calculate(), Times.Once());

        }

    }
}