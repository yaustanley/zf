using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Code;

namespace unittest
{
    [TestClass]
    public class UnitTest1
    {
        decimal[] data = new decimal[] { -1, 1, (decimal)1.1, (decimal)1.1, (decimal)2.8 };

        [TestMethod]
        public void TestFindMedian()
        {
            decimal result = Zenfolio.FindMedian(this.data);
            Assert.AreEqual((decimal)1.1, result);

            result = Zenfolio.FindMedian(new decimal[] { -1, 1, (decimal)1.1, (decimal)1.1 });
            Assert.AreEqual((decimal)1.05, result);
        }

        [TestMethod]
        public void TestFindMode()
        {
            decimal mode;
            bool uniqueMode = Zenfolio.FindMode(this.data, out mode);
            Assert.IsTrue(uniqueMode && (decimal)1.1 == mode);

            // Method returns false when mode isn't unique.
            Assert.IsFalse(Zenfolio.FindMode(new decimal[] { 1, 2, 2, 3, 3 }, out mode));
        }

        [TestMethod]
        public void TestCharCountMap()
        {
            SortedDictionary<char, int> charCountMap = new SortedDictionary<char, int>
            {
                {'b', 1 }, {'a', 2 }
            };

            Zenfolio.CountChar(charCountMap, 'b');
            Assert.AreEqual(2, charCountMap['b']);

            Zenfolio.CountChar(charCountMap, 'c');
            Assert.AreEqual(1, charCountMap['c']);

            Zenfolio.CountChar(charCountMap, 'a');
            Assert.AreEqual(3, charCountMap['a']);
        }
    }
}
