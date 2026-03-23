using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticalWork4_Smolin_Koskina;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject
{
    [TestClass]
    public class Func1UnitTest
    {
        /// <summary>
        /// Тест Function1 для стандартных допустимых значений x=2, y=1, z=1.
        /// Ожидаемый результат: ≈ 0.5249 (допуск 1e-4).
        /// </summary>
        [TestMethod]
        public void TestFunction1_StandardValues()
        {
            double result = Core.Function1(2, 1, 1);
            Assert.AreEqual(0.5249, result, 1e-4,
                "F1(2, 1, 1) должна быть близка к 0.5249");
        }

        /// <summary>
        /// Тест Function1 для значений x=3, y=2, z=0.5.
        /// Ожидаемый результат: ≈ 1.7312 (допуск 1e-4).
        /// </summary>
        [TestMethod]
        public void TestFunction1_AnotherValues()
        {
            double result = Core.Function1(3, 2, 0.5);
            Assert.AreEqual(1.7312, result, 1e-4,
                "F1(3, 2, 0.5) должна быть близка к 1.7312");
        }

        /// <summary>
        /// Тест Function1: при x == y знаменатель равен нулю, результат должен быть Infinity или NaN.
        /// Функция не определена в этой точке.
        /// </summary>
        [TestMethod]
        public void TestFunction1_DivisionByZero_WhenXEqualsY()
        {
            double result = Core.Function1(3, 3, 1);
            Assert.IsTrue(double.IsInfinity(result) || double.IsNaN(result),
                "F1(3, 3, 1) должна быть Infinity или NaN, т.к. |x-y|=0");
        }
    }
}
