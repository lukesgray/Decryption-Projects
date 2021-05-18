using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

//Project 1
//Luke Gray
//02/15/2021
//This project will decrypt encypted messages from files
namespace Project_1
{
    class Program
    {
        static void Main(string[] args)
        {



            //*****************************************************************************************************************************************************************
            // Variable Definitions
            //*****************************************************************************************************************************************************************

            // This variable stores the user's input
            string userInput = "";

            // Creates an instance of the StreamReader
            StreamReader fileStream = null;

            // This will store the file path to the file which the user selected in userInput
            string path = "";

            // This int variable will store a number to represent the shift value for decryption
            int i = 0;

            // This bool variable will be checked in the testing while loop.  
            bool ask = false;

            // This string variable will store each line of the user defined file
            string line = "";

            // This string variable will store the response of the user that indicates whether the method has found the correct shift value for decryption
            string response = "";

            // This will store the decrypted line
            string decryptedLine = "";



            //*****************************************************************************************************************************************************************
            // This section set up the StreamReader by prompting the user and recording their responses
            //*****************************************************************************************************************************************************************
            
            // Explains the purpose of this program to the user
            Console.WriteLine("The purpose of this program is to decrypt text in some files that have been encrypted.");

            // Prompts the user to enter the name of the file that they would like to decrypt
            Console.WriteLine("Enter the name of the file from the \"encrypted_files\" folder that you would like to decrypt: ");

            // This line sets the user's input equal to the string variable userInput using Console.ReadLine()
            userInput = Console.ReadLine();

            // This line sets the filepath of the "path" variable to be the the user's desired file from the "enrypted_files" folder
            // Notice that the file type extention is included in the the "path" variable for user convenience
            path = "C:\\Users\\Test\\source\\repos\\comp_sci _coy\\Project 1\\encrypted_files\\" + userInput + ".txt";

            // Connects the filePath to the streamReader and if this connection is impossible, it will catch the exception and write an error message
            try
            {
                fileStream = new StreamReader(path);
            }
            catch (IOException e)
            {
                // Prints these two error messages to the console
                Console.WriteLine("The file could not be read");
                Console.WriteLine(e.Message);
                Console.ReadLine();
                // Ends the program
                return;
            }



            //*****************************************************************************************************************************************************************
            // This section tests decryption for each shift value until the correct shift value is found
            //*****************************************************************************************************************************************************************

            // The variable "line" stores the first line of the stream reader which is stored in "fileStream" 
            line = fileStream.ReadLine();

            // This while loop will run while bool variable "ask" is equal to false
            while (ask == false)
            {
                // This will call the "decrypt" method which has a string return value that is equal to the line shifted by a value of "i" and store it in "decryptedLine"
                decryptedLine = decrypt(line, i);

                // Prints the string variable "decryptedLine" to the console for the user to identify
                Console.WriteLine(decryptedLine);

                // The user will now be prompted to read the first line that has been shifted by "i" and determine if it is in english be typing "y" for yes or "y" for no
                Console.WriteLine("If the decryption looks correct, type \"y\", if not, type \"n\" or type \"q\" to quit:");

                // This response variable will store the user's answer
                response = Console.ReadLine();

                // This if statement will check the value stored in the "response" variable
                // If "response" is storing "y", the statement store the value of true to the bool variable "ask" thus ending the loop without changing the value of "i"
                // If "response" is storing "n", it will increase the value of "i" by 1 and by default the loop will run again using the updated shift value
                // If "response" is storing "q", it will quit the entire program
                if (response == "y")
                {
                    ask = true;
                }
                else if (response == "n")
                {
                    i++;
                }
                else if (response == "q")
                {
                    Environment.Exit(0);
                }
            }



            //*****************************************************************************************************************************************************************
            // Now that "i" contains the correct shift value for decryption, the decrpyt method prints out the correct text
            //*****************************************************************************************************************************************************************

            // This prints the first line that we used to test the decryption
            Console.WriteLine(decryptedLine);

            // This while loop prints out each sucessive line using the decrypt method until the stream reader returns a value of "null"
            while ((line = fileStream.ReadLine()) != null)
            {
                Console.WriteLine(decrypt(line, i));
            }

            // This command closes the StreamReader which is stored in fileStream
            fileStream.Close();

            Console.ReadLine();

        }// Main

        static string decrypt(string fileLine, int shift)
        {
            //*****************************************************************************************************************************************************************
            // Variable Definitions
            //*****************************************************************************************************************************************************************

            // This int will store 
            int num = 0;

            // This string variable will store the line to be returned
            string line = "";



            //*****************************************************************************************************************************************************************
            // This is the beginning of the method
            //*****************************************************************************************************************************************************************

            // Loops through each character of the string passed into this method
            foreach (char letter in fileLine)
            {
                // Converts each character in the string "text" to it's ascii value
                num = letter;

                // Captures all lowercase letters by setting lowercase letter ascii range as parameter in the if statement
                // All other characters go throught the "else"
                // In both cases, the new character is added onto the string variable "line" be concatenating the updated character and the string variable "line"
                // To the variable "line" itself thus increasing the encrypted line length by 1 until all encrypted characters have been been concatenated and stored 
                if (letter > 96 && letter < 123)
                {
                    // Increases the ascii number by the shift value
                    num = num + shift;

                    // Subtracts 26 from the new number if the new number is out of range
                    // This operation turns the shift into a loop of numbers for the cypher
                    if (num > 122)
                    {
                        num = num - 26;
                    }
                    // Converts int variable "num" to character data and concatenates it to the string variable "line"
                    line = string.Concat(line, (char)num);
                }
                else
                {
                    // Concatenates special charcter to the string variable "line"
                    line = string.Concat(line, letter);
                }
            }

            // Returns the string variable "line" through the "decrypt" method
            return line;
        }// decrypt

    }// class Program

}// namespace