using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System;

namespace PhoneBookTest
{
    [TestClass]
    public class UnitTest1
    {
        private const string Expected = "Hello World!";
        private const string phone_book_file = "phonebook.dat";
        private const string phone_book_test_file = "phonebook_test.dat";

        private const string name1 = "Adelmann Gerlinde";
        private const string number1 = "+43 669 0192837465";
        private const string name2 = "Ulmer Robert";
        private const string number2 = "+43 316 1234567890";
        private const string name3 = "Zotter Bernd";
        private const string number3 = "+43 316 0987654321";
        private const string name4 = "Beyer Claudia";
        private const string number4 = "+43 676 54545454";

        string[] arguments = { phone_book_file };
        [TestMethod]
        public void TestMethod00()
        {
            FileStream fs = File.OpenWrite(phone_book_file);
            string phone_book_string = "";
            phone_book_string += name1 + ": " + number1 + Environment.NewLine;
            phone_book_string += name2 + ": " + number2 + Environment.NewLine;
            phone_book_string += name3 + ": " + number3 + Environment.NewLine;
            byte[] bytes = System.Text.UTF8Encoding.UTF8.GetBytes(phone_book_string);
            fs.Write(bytes);
            fs.Close();
        }

        [TestMethod]
        public void TestMethod01()
        {
            string line = "";
            StreamReader file = new StreamReader(phone_book_file);
            while((line = file.ReadLine()) != null)
            {
                var result = line.Split(":");
                Console.WriteLine(result[0]);
                Console.WriteLine(result[1]);
            }
        }

        [TestMethod]
        public void TestMethod02()
        {
            PhoneBook.PhoneBookLib.loadPhoneBook(phone_book_file);
            Assert.AreEqual(3, PhoneBook.PhoneBookLib.count);

        }

        [TestMethod]
        public void TestMethod03()
        {
            // PhoneBook.PhoneBookLib.loadPhoneBook(phone_book_file);
            Assert.AreEqual(3, PhoneBook.PhoneBookLib.count);
            PhoneBook.PhoneBookLib.addNumber(name4, number4);
            Assert.AreEqual(4, PhoneBook.PhoneBookLib.count);

        }
        [TestMethod]
        public void TestMethod04()
        {
            // PhoneBook.PhoneBookLib.loadPhoneBook(phone_book_file);
            var result = PhoneBook.PhoneBookLib.searchName("Ulmer Robert");
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(name2, result[0].Key);
            Assert.AreEqual(number2, result[0].Value);
        }

        [TestMethod]
        public void TestMethod05()
        {
            // We test for partial name
            var result = PhoneBook.PhoneBookLib.searchName("Ulmer");
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(name2, result[0].Key);
            Assert.AreEqual(number2, result[0].Value);
        }

        [TestMethod]
        public void TestMethod06()
        {
            // We test for case insensitifity
            var result = PhoneBook.PhoneBookLib.searchName("ULMER");
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(name2, result[0].Key);
            Assert.AreEqual(number2, result[0].Value);
        }

        [TestMethod]
        public void TestMethod07()
        {
            // We test for case insensitifity
            var result = PhoneBook.PhoneBookLib.searchName("XAVER");
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestMethod08()
        {
            // We test for a result with more then one result
            var result = PhoneBook.PhoneBookLib.searchName("BE");
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(name4, result[0].Key);
            Assert.AreEqual(number4, result[0].Value);
            Assert.AreEqual(name2, result[1].Key);
            Assert.AreEqual(number2, result[1].Value);
            Assert.AreEqual(name3, result[2].Key);
            Assert.AreEqual(number3, result[2].Value);
        }

        [TestMethod]
        public void TestMethod09()
        {
            // Test to string
            string expected = "";
            expected += "PhoneBook:" + Environment.NewLine;
            expected += "======================================================================" + Environment.NewLine;
            expected += name1 + ": " + number1 + Environment.NewLine;
            expected += name4 + ": " + number4 + Environment.NewLine;
            expected += name2 + ": " + number2 + Environment.NewLine;
            expected += name3 + ": " + number3 + Environment.NewLine;
            expected += "======================================================================" + Environment.NewLine;

            var result = PhoneBook.PhoneBookLib.toString();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestMethod10()
        {
            // Test save a file
            if (File.Exists(phone_book_test_file)) {
                File.Delete(phone_book_test_file);
            }
            PhoneBook.PhoneBookLib.savePhoneBook(phone_book_test_file);
            string expected = "";
            expected += name1 + ": " + number1 + Environment.NewLine;
            expected += name4 + ": " + number4 + Environment.NewLine;
            expected += name2 + ": " + number2 + Environment.NewLine;
            expected += name3 + ": " + number3 + Environment.NewLine;
            StreamReader sr = new StreamReader(phone_book_test_file);
            string result = sr.ReadToEnd();
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void TestMethod99()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                PhoneBook.Program.Main(arguments);

                var result = sw.ToString().Trim();
                Assert.AreEqual(Expected, result);
            }
        }
    }
}
