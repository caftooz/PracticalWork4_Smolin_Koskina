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
    public class Func2unitTest
    {
        /// <summary>
        /// Тест Function2 для случая x == y (первая ветвь).
        /// f(x) = sh(x), x=3, y=3. Ожидаемый результат: ≈ 109.4989 (допуск 1e-4).
        /// </summary>
        [TestMethod]
        public void TestFunction2_XEqualsY()
        {
            Func<double, double> f = x => Math.Sinh(x);
            double result = Core.Function2(3, 3, f);
            Assert.AreEqual(109.4989, result, 1e-4,
                "F2(3, 3, sinh) при x==y должна быть ≈ 109.4989");
        }

        /// <summary>
        /// Тест Function2 для случая x &gt; y (вторая ветвь).
        /// f(x) = x², x=5, y=3. Ожидаемый результат: ≈ 483.0100 (допуск 1e-4).
        /// </summary>
        [TestMethod]
        public void TestFunction2_XGreaterThanY()
        {
            Func<double, double> f = x => Math.Pow(x, 2);
            double result = Core.Function2(5, 3, f);
            Assert.AreEqual(483.0100, result, 1e-4,
                "F2(5, 3, x²) при x>y должна быть ≈ 483.0100");
        }

        /// <summary>
        /// Тест Function2 для случая x &lt; y (третья ветвь).
        /// f(x) = sh(x), x=1, y=3. Ожидаемый результат: ≈ 3.1873 (допуск 1e-4).
        /// </summary>
        [TestMethod]
        public void TestFunction2_XLessThanY()
        {
            Func<double, double> f = x => Math.Sinh(x);
            double result = Core.Function2(1, 3, f);
            Assert.AreEqual(3.1873, result, 1e-4,
                "F2(1, 3, sinh) при x<y должна быть ≈ 3.1873");
        }
    }
}
