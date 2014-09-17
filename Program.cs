using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV402.S1.L02C
{
    class Program
    {
        static void Main(string[] args)
        {
            // Declare variables
            const byte MAX_VALUE = 79;
            bool continueProgram = true;
            byte parsedByte;

            // Do until user presses Esc key.
            do
            {
                // Get and parse user input.
                parsedByte = ReadOddByte(Properties.Resources.enterNumberMsg, MAX_VALUE);

                // Render diamond.
                RenderDiamond(parsedByte);

                // Find out if we should continue this program, if user has pressed the Esc key.
                continueProgram = IsContinuing();
            }
            while (continueProgram); // Loop until user presses the Esc key.
        }

        /// <summary>
        /// Asks the user if he wants to continue the program.
        /// </summary>
        /// <returns>Bool answer, yes = true, no = false</returns>
        static bool IsContinuing()
        {
            // Set white textcolor and dark green background color
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkGreen;

            // Display message for user.
            Console.WriteLine("\n{0}\n", Properties.Resources.escapeOrResumeMsg);

            // Reset terminal colors.
            Console.ResetColor();

            // Check if user presses Esc key.
            if (Console.ReadKey(true).Key == ConsoleKey.Escape)
            {
                // User pressed the Esc key. Return false
                return false;
            }
            else
            {
                // User Presed other key. Return true
                return true;
            }

        }

        /// <summary>
        /// Get and parse user input.
        /// </summary>
        /// <param name="prompt">Message displayed in prompt for user</param>
        /// <param name="maxValue">The maximum value for user input</param>
        /// <returns>Parsed byte in an odd number</returns>
        static byte ReadOddByte(string prompt = null, byte maxValue = 255)
        {
            // Declare variables
            byte parsedByte;
            string promtedValue;

            // Loop until the user gets the input right.
            while (true)
            {
                // Try to get user input.
                try
                {
                    // Ask the user to enter input for sum.
                    Console.Write(String.Format("\n {0,-20} {1,-2}", prompt, ":"));

                    // Get user input.
                    promtedValue = Console.ReadLine();

                    // Try to parse user input.
                    if (!byte.TryParse(promtedValue, out parsedByte))
                    {
                        // Parse failed. Throw exception with a custom message.
                        throw new FormatException(String.Format("\"{0}\" {1}", promtedValue, Properties.Resources.userInputCouldNotParseMsg));
                    }

                    // Check that the parsed value is lower than the highest acceptable value.
                    else if (parsedByte > maxValue)
                    {
                        // Throw exception with a custom message.
                        throw new OverflowException(String.Format("{0} {1}", parsedByte, Properties.Resources.userInputValueTooLargeMsg));
                    }
                    // Check if the parsed value is not an odd value
                    else if (parsedByte % 2 == 0)
                    {
                        // Throw exception with a custom message.
                        throw new FormatException(String.Format("{0} {1}", parsedByte, Properties.Resources.userInputValueNotOddMsg));
                    }

                    // All seems right. Break loop.
                    break;
                }
                // Catch errors. If there was something wrong with the users input. Display a message for the user.
                catch (Exception exception)
                {
                    // Set white textcolor and dark green background color
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Red;

                    // Display message for user.
                    Console.WriteLine("\n {0} {1} ", Properties.Resources.userInputErrorMsg, exception.Message);

                    // Reset terminal colors.
                    Console.ResetColor();
                }
            }

            // Return value
            return parsedByte;
        }

        /// <summary>
        /// Renders a diamond with asterisks and spaces.
        /// </summary>
        /// <param name="maxCount">Count of maximum numbers of asterisks in row, this determines the width of the diamond.</param>
        static void RenderDiamond(byte maxCount)
        {
            // Declare variables
            int asteriskCount = 0;

            // Loop for rows
            for (int rowNum = 0; rowNum <= maxCount; rowNum++)
            {
                // Render row
                RenderRow(maxCount, asteriskCount);

                // Keep track of asterisk count over rows. Increase or decrease depending on if half is passed.
                if (rowNum > maxCount / 2)
                {
                    // The rowcount has passed half, start to decrease asteriskCount.
                    asteriskCount--;
                }
                else
                {
                    // The rowcount has NOT passed half, increase asteriskCount.
                    asteriskCount++;
                }
            }
        }

        /// <summary>
        /// Renders a row in a diamond.
        /// </summary>
        /// <param name="maxCount">Maximum totalt number of chars in a row</param>
        /// <param name="asteriskCount">The current count on asterisks</param>
        static void RenderRow(int maxCount, int asteriskCount)
        {
            // Loop for characters
            for (byte charNum = 0; charNum < maxCount; charNum++)
            {
                // Figure out if we should write asterisks or not.
                if (charNum <= (maxCount / 2 - asteriskCount) || charNum >= (maxCount / 2 + asteriskCount))
                {
                    // Write space
                    Console.Write(" ");
                }
                else
                {
                    // Write asterisk
                    Console.Write("*");
                }
            }

            // Write new Line
            Console.WriteLine();
        }
    }
}
