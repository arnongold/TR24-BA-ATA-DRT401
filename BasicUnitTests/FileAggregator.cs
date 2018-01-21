using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicUnitTests
{
    public class FileAggregator
    {
        public int GetFileAggregation(string fileName)
        {
            var numbers = GetNumbersFromFile(fileName);
            return numbers.Sum();
        }

        private List<int> GetNumbersFromFile(string fileName)
        {
            var numbersList = new List<int>();
            using (StreamReader inputFile = new StreamReader(fileName))
            {
                string numbers = inputFile.ReadToEnd();
                var numbersAsArray = numbers.Split(',');
                foreach (var number in numbersAsArray)
                {
                    numbersList.Add(Convert.ToInt32(number));
                }
            }
            return numbersList;
        }
    }
}
