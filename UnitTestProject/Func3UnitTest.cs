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
    public class Func3UnitTest
    {
        /// <summary>
        /// Тест Function3 для x=2, b=1.
        /// Ожидаемый результат: ≈ 0.0540 (допуск 1e-4).
        /// </summary>
        [TestMethod]
        public void TestFunction3_BasicValues()
        {
            double result = Core.Function3(2, 1);
            Assert.AreEqual(0.0540, result, 1e-4,
                "F3(2, 1) должна быть ≈ 0.0540");
        }

        /// <summary>
        /// Тест Function3 для x=5, b=3.
        /// Ожидаемый результат: ≈ 0.6946 (допуск 1e-4).
        /// </summary>
        [TestMethod]
        public void TestFunction3_AnotherValues()
        {
            double result = Core.Function3(5, 3);
            Assert.AreEqual(0.6946, result, 1e-4,
                "F3(5, 3) должна быть ≈ 0.6946");
        }

        /// <summary>
        /// Тест Function3: при x == b функция не определена (ln(0) и деление на 0).
        /// Результат должен быть -Infinity или NaN.
        /// </summary>
        [TestMethod]
        public void TestFunction3_DivisionByZero_WhenXEqualsB()
        {
            double result = Core.Function3(2, 2);
            Assert.IsTrue(double.IsNegativeInfinity(result) || double.IsNaN(result),
                "F3(2, 2) должна быть -Infinity или NaN, т.к. x==b");
        }
    }
}
