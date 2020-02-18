using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class DateTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void DateTestSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator DateTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }

        [Test]
        public void DateEqualOperator()
        {
            // equal dates
            var equalTrue = false;
            var equalMilliTrue = false;
            var leftEqual = DateTime.Now;
            var rightEqual = DateTime.Now;

            if (leftEqual == rightEqual)
            {
                equalTrue = true;
            }
            
            if (leftEqual.Millisecond == rightEqual.Millisecond)
            {
                equalMilliTrue = true;
            }
            
            Assert.AreEqual(equalTrue, false);
            Assert.True(equalMilliTrue);
        }

        [Test]
        public void DateGreaterOperator()
        {
            var greaterTrue = true;
            var greaterMilli = true;
            var leftGreater = DateTime.Now;
            var rightGreater = DateTime.Now.AddDays(1);

            if (leftGreater > rightGreater)
            {
                greaterTrue = false;
            }

            if (leftGreater.Millisecond > rightGreater.Millisecond)
            {
                greaterMilli = false;
            }
            
            
            Assert.True(greaterTrue);
            Assert.True(greaterMilli);
        }
        
        [Test]
        public void DateLesserOperator()
        {
            var lesserTrue = false;
            var lesserMilli = false;
            var leftLesser = DateTime.Now;
            var rightLesser = DateTime.Now.AddDays(1);

            if (leftLesser < rightLesser)
            {
                lesserTrue = true;
            }

            if (leftLesser.Millisecond < rightLesser.Millisecond)
            {
                lesserMilli = true;
            }
            
            Debug.Log(leftLesser < rightLesser);
            Debug.Log(leftLesser.Millisecond < rightLesser.Millisecond);
            
            Debug.Log(leftLesser.Millisecond);
            Debug.Log(rightLesser.Millisecond);
            
            Debug.Log(leftLesser);
            Debug.Log(rightLesser);
            
            Assert.True(lesserTrue);
            Assert.True(lesserMilli);
        }
    }
}
