using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using StringInspector.Core.Helpers;
using StringInspector.Core.Models;

namespace StringInspector.Test
{
    [TestClass]
    public class StringHelperTests
    {
        [TestMethod]
        public void TestCalculateMostCommonCharacter()
        {
            var expected = 'a';
            var expectedCount = 2;
            Article a = new Article()
            {
                Content = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%^&*()_+a"
            };
            var result = StringHelper.CalculateMostCommonCharacter(a);

            Assert.AreEqual(expected, result.MostCommonCharacter, "The most common character should be found in the input string.");
            Assert.AreEqual(expectedCount, result.MostCommonCharacterCount, "The most common character found in the input string should be counted accurately.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullArticle()
        {
            Article a = null;
            var result = StringHelper.CalculateMostCommonCharacter(a);

            Assert.IsTrue(true, "This test should throw an exception before this line executes.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullArticleContent()
        {
            Article a = new Article();
            var result = StringHelper.CalculateMostCommonCharacter(a);

            Assert.IsTrue(true, "This test should throw an exception before this line executes.");
        }

        [TestMethod]
        public void TestEmptyArticleContent()
        {
            var NULL_TERMINATOR = '\0';
            Article a = new Article() 
            {
                Content = ""
            };
            var result = StringHelper.CalculateMostCommonCharacter(a);

            Assert.AreEqual(NULL_TERMINATOR, result.MostCommonCharacter, "Empty article contents should return the null terminator as the most common character.");
            Assert.AreEqual(0, result.MostCommonCharacterCount, "Empty article contents should return the 0 as the most common character count.");
        }

        [TestMethod]
        public void TestTies()
        {
            var expected = 'a';
            var expectedCount = 2;
            Article a = new Article()
            {
                Content = "aabb"
            };
            var result = StringHelper.CalculateMostCommonCharacter(a);

            Assert.AreEqual(expected, result.MostCommonCharacter, "Ties should go to the character found first.");
            Assert.AreEqual(expectedCount, result.MostCommonCharacterCount, "Ties should be counted accurately.");
        }

        [TestMethod]
        public void TestPunctuation()
        {
            var expected = '&';
            var expectedCount = 7;
            Article a = new Article()
            {
                Content = "!@@###$$$$%%%%%^^^^^^&&&&&&&"
            };
            var result = StringHelper.CalculateMostCommonCharacter(a);

            Assert.AreEqual(expected, result.MostCommonCharacter, "Punctuation should count as a character.");
            Assert.AreEqual(expectedCount, result.MostCommonCharacterCount, "Punctuation should be counted accurately.");
        }

        [TestMethod]
        public void TestIgnoreSpaces()
        {
            var expected = 'b';
            var expectedCount = 1;
            Article a = new Article()
            {
                Content = "h e l l o   "
            };
            var result = StringHelper.CalculateMostCommonCharacter(a, new StringHelperOptions() { IgnoreSpaces = true });

            Assert.AreEqual(expected, result.MostCommonCharacter, "Punctuation should count as a character.");
            Assert.AreEqual(expectedCount, result.MostCommonCharacterCount, "Punctuation should be counted accurately.");
        }
    }
}
