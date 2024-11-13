using Library_Console_App.LibrarySystem.Models;
using System;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Library_Console_App.Utility
{
    public static class UserInput
    {
        // Static method to get validated text input
        public static string ValidateTextInput()
        {
            string? input;
            Regex regex = new Regex("^[a-zA-Z ]+$"); // Allows letters and spaces only

            do
            {
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Input cannot be empty. Please try again:");
                }
                else if (!regex.IsMatch(input))
                {
                    Console.WriteLine("Input must contain only letters. Please try again:");
                    input = null; // Set input to null to force the loop to continue
                }
            }
            while (string.IsNullOrEmpty(input));

            return input;
        }

        // Static method to get validated number input
        public static int ValidateNumberInput()
        {
            string? input;
            int number;

            do
            {
                input = Console.ReadLine();

                if (int.TryParse(input, out number))
                {
                    return number;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number:");
                }
            }
            while (true);
        }

        // Static method to get validated double input
        public static double ValidateDoubleInput()
        {
            string? input;
            double number;

            do
            {
                input = Console.ReadLine();

                if (double.TryParse(input, out number))
                {
                    return number;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid decimal number:");
                }
            }
            while (true);
        }

        public static int ValidateRateInput()
        {
            int ratingInput;
            do
            {
                ratingInput = UserInput.ValidateNumberInput();

                if (ratingInput < 1 || ratingInput > 5)
                {
                    Console.WriteLine("Invalid input, rating should be between 1 and 5");
                }

            } while (ratingInput < 1 || ratingInput > 5);

            return ratingInput;
        }
    }
}
