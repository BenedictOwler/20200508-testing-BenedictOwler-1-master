using System;
using System.IO;
using PhoneBookEntry = System.Collections.Generic.KeyValuePair<string, string>;
using PhoneBookList = System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>>;

namespace PhoneBook
{
    public class PhoneBookLib
    {
        static PhoneBookList _storage = new PhoneBookList();

        static public void loadPhoneBook(string datapath)
        {
            // IMPLEMENT HERE

            string line = "";
            StreamReader file = new StreamReader(datapath);
            while ((line = file.ReadLine()) != null)
            {
                var result = line.Split(": ");
                _storage.Add(new PhoneBookEntry(result[0], result[1]));
                Console.WriteLine(result[0]);
                Console.WriteLine(result[1]);
            }

        }

        static public void savePhoneBook(string datapath)
        {
            // IMPLEMENT HERE

           
        }

        static public void addNumber(string name, string number)
        {
            // IMPLEMENT HERE
            _storage.Add(new PhoneBookEntry(name, number));
        }

        static public string toString()
        {
            string return_value = "";
            // IMPLEMENT HERE
            return return_value;
        }

        static public PhoneBookList searchName(string search_string)
        {
            PhoneBookList return_value = new PhoneBookList();
            foreach (var item in _storage)
            {

                if (item.Key.Contains(search_string)
                {

                    return_value.Add(item);
                }
            }

            // IMPLEMENT HERE
            return return_value;
        }

        static public int count {
            get { 
                return _storage.Count; 
            } 
        }
    }
}
