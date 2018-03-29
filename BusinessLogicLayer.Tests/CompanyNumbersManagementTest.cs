using DataAccessLayer.Models;
using NUnit.Framework;

namespace BusinessLogicLayer.Tests
{
    [TestFixture]
    public class CompanyNumbersManagementTest
    {
        [Test]
        public void RegonIsValid_returns_false_if_regon_is_null_or_empty()
        {
            bool result = CompanyNumbersManagement.RegonIsValid("");

            Assert.IsFalse(result);
        }

        [Test]
        public void RegonIsValid_returns_true_if_regon_is_valid()
        {
            bool resultNineDigitRegon = CompanyNumbersManagement.RegonIsValid("158173413");

            bool resultFourteenDigitRegon = CompanyNumbersManagement.RegonIsValid("1 2 3 4 5 6 7 8 5 1 2 3 4 7");

            Assert.IsTrue(resultNineDigitRegon);
            Assert.IsTrue(resultFourteenDigitRegon);
        }

        [Test]
        public void NipIsValid_returns_false_if_nip_is_null_or_empty()
        {
            bool result = CompanyNumbersManagement.NipIsValid("");

            Assert.IsFalse(result);
        }

        [Test]
        public void NipIsValid_returns_true_if_nip_is_valid()
        {
            bool resultNip1 = CompanyNumbersManagement.NipIsValid("PL3943680458");
            bool resultNip2 = CompanyNumbersManagement.NipIsValid("5661128662");
            bool resultNip3 = CompanyNumbersManagement.NipIsValid("108-96-85-225");

            Assert.IsTrue(resultNip1);
            Assert.IsTrue(resultNip2);
            Assert.IsTrue(resultNip3);
        }

        [Test]
        public void KrsIsValid_returns_false_if_krs_is_null_or_empty()
        {
            bool result = CompanyNumbersManagement.KrsIsValid("");

            Assert.IsFalse(result);
        }

        [Test]
        public void KrsIsValid_returns_false_if_krs_not_starts_with_0000()
        {
            bool result = CompanyNumbersManagement.KrsIsValid("5632132897");

            Assert.IsFalse(result);
        }

        [Test]
        public void KrsIsValid_returns_false_if_krs_length_is_not_equal_to_10()
        {
            bool result = CompanyNumbersManagement.KrsIsValid("0000456");

            Assert.IsFalse(result);
        }

        [Test]
        public void ToOnlyDigitString_removel_all_non_digits_charactes_from_string()
        {
            string testString = "4/*59-ahti";

            string result = testString.ToOnlyDigitString();

            Assert.AreEqual("459", result);
        }

        [Test]
        public void ToCorrectlyFormatedNip_adds_PL_prefix_to_string()
        {
            string testString = "549798421";

            string result = testString.ToCorrectlyFormatedNip();

            Assert.AreEqual("PL549798421", result);
        }

        [Test]
        public void RecognizeNumberType_returns_correct_number_type()
        {
            string validtNip = "3924304381";
            string invalidNip = "3924304386";

            string validRegon = "290217921";
            string invalidRegom = "290217925";

            string validKrs = "0000123456";
            string invalidKrs = "1200041655";

            Assert.AreEqual(NumberType.NIP, CompanyNumbersManagement.RecognizeNumberType(validtNip));
            Assert.AreEqual(NumberType.Unrecognized, CompanyNumbersManagement.RecognizeNumberType(invalidNip));

            Assert.AreEqual(NumberType.REGON, CompanyNumbersManagement.RecognizeNumberType(validRegon));
            Assert.AreEqual(NumberType.Unrecognized, CompanyNumbersManagement.RecognizeNumberType(invalidRegom));

            Assert.AreEqual(NumberType.KRS, CompanyNumbersManagement.RecognizeNumberType(validKrs));
            Assert.AreEqual(NumberType.Unrecognized, CompanyNumbersManagement.RecognizeNumberType(invalidKrs));
        }
    }
}