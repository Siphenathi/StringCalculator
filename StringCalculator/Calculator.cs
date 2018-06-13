using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class Calculator
    {
        public int Add(string inputString)
        {
            var delimiter = GetDelimiter(inputString);
            inputString = GetDelimitedNumbers(delimiter, inputString);
            var numbers = inputString.Split(new[] { '\n', delimiter }, StringSplitOptions.RemoveEmptyEntries);
            HandleExceptions(numbers);
            return GetSumOfNumbers(numbers);
        }

        private void HandleExceptions(string[] stringNumbers)
        {
            var listOfStringNumbers = new List<string>(stringNumbers);
            var negetiveNumbers = listOfStringNumbers.Where(number => int.Parse(number) < 0);
            
            if (negetiveNumbers.Any())
            {
                var negetives = String.Join(" ", negetiveNumbers.ToArray());
                throw new Exception($"Negatives not allowed {negetives}");
            }
        }

        public string GetDelimitedNumbers(char delimeter, string inputString)
        {
            inputString = !delimeter.Equals(',') ?
                inputString.Substring(3, inputString.Length - 3) : inputString;
            return inputString;
        }

        private char GetDelimiter(string inputString)
        {
            var delimiter = inputString.StartsWith("//") ? inputString[2] : ',';
            return delimiter;
        }

        private int GetSumOfNumbers(string[] numbers)
        {
            var listOfNumbers = new List<string>(numbers);
            return listOfNumbers
                .Where(number=>int.Parse(number)<=1000)
                .Sum(number => int.Parse(number));
        }
    }
}