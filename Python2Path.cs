/* Name: Python2Path
 * Description:   This program adds Python to the System PATH Envronmental Variable.  It
                    automatically removes all previous paths to Python, so it can also
                    be used to update your PATH if you changed Python versions.
 * Author: JamRizzi Technologies
 * Version: 1.0.0
 * Date Created: 18-10-2015
 */

using System;
using System.IO;
using System.Security;
using System.Collections.Generic;

namespace Python2Path {
    class Program {
        static void Main(string[] args) {

            // Define variables
            string path = Environment.GetEnvironmentVariable("Path"),
                input = "", pythonPath = "";
            List<string> paths = path.Parse(';'), pythonInstallations = new List<string>();
            string[] directories = new string[0];
            bool automatic = true;

            // Header
            Console.WriteLine(@"
  Python2Path
  Created by JamRizzi Technologies
  Copyright 2015

  This program adds Python to the System PATH Envronmental Variable.  It
    automatically removes all previous paths to Python, so it can also
    be used to update your PATH if you changed Python versions.

");

            // Sanitize paths
            for(int i = 0; i < paths.Count; i++) {
                if(paths[i] == "") {
                    paths.RemoveAt(i);
                    i -= 1;
                }
            }

            // Scan for python installations
            directories = Directory.GetDirectories(@"C:\");
            for (int i = 0; i < directories.Length; i++) {
                string directory = "";
                if (directories[i].Length > 9) {
                    for (int j = 0; j < 9; j++) {
                        directory += directories[i][j];
                    }
                }
                if (directory == @"C:\Python" && directories[i].Length == 11) {
                    pythonInstallations.Add(directories[i]);
                }
            }
           
            // List detected python installations
            if(pythonInstallations.Count == 0) {
                Console.WriteLine(@"No Python installations have been detected.  Maybe you installed it at a
custom location
");
            } else {
                if(pythonInstallations.Count == 1) {
                    Console.WriteLine("We have detected a Python installation at the following directory:");
                } else {
                    Console.WriteLine("We have detected Python installations at the following directories:");
                }
                for (int i = 0; i < pythonInstallations.Count; i++) {
                    Console.WriteLine("  " + pythonInstallations[i]);
                }
                Console.WriteLine();
            }

            // User instructions
            Console.WriteLine(@"INSTRUCTIONS:
You must enter either a valid Python version or a valid path to a Python
installation.  If your version is Python 2.7.10, you could enter Python 2.7.10,
Python 2.7, Python 27, 2.7.10, 2.7 or 27.  If you installed python at
C:/MySecretLocation/Python, then you would enter C:/MySecretLocation/Python.
");

            // Begin main loop
            for (;;) {

                // Redefine variables
                pythonPath = "";

                // Get user input
                Console.Write(": ");
                input = Console.ReadLine();
                Console.WriteLine();

                // Detect if they are doing it automatically or manually
                for (int i = 0; i < input.Length; i++) {
                    if (input[i] == '/' || input[i] == '\\') {
                        automatic = false;
                        break;
                    } else {
                        automatic = true;
                    }
                }
                    
                // Automatic mode
                if (automatic) {
                    // Sanitize input
                    for (int i = 0; i < input.Length; i++) { // Remove everything but numbers
                        if (Char.IsDigit(input[i])) {
                            pythonPath += input[i];
                        }
                    }
                    input = "";
                    if (pythonPath.Length < 2) { // If not enough digits
                        pythonPath += "///"; // Create impossible directory
                    }
                    for (int i = 0; i < 2; i++) { // Limit to 2 digits
                        input += pythonPath[i];
                    }
                    pythonPath = @"C:\Python" + input;

                // Manuel mode
                } else {
                    // Replace / with \
                    for(int i = 0; i < input.Length; i++) {
                        if(input[i] == '/') {
                            pythonPath += '\\';
                        } else {
                            pythonPath += input[i];
                        }
                    }
                }

                // Verify directory is valid
                if (!Directory.Exists(pythonPath)) {
                    Console.WriteLine(@"The following input is not valid.  Try again.
");
                    continue;
                }

                // Verify the user wants to continue
                Console.Write(@"I want " + pythonPath + @" added to the System PATH Envrionmental Variable.
Enter (Y)es or (n)o.

: ");
                input = Console.ReadLine().ToLower();
                Console.WriteLine();
                if (input == "" || input[0] != 'n') {

                    // Remove python installations from PATH
                    for (int i = 0; i < pythonInstallations.Count; i++) {
                        for (int j = 0; j < paths.Count; j++) {
                            if (pythonInstallations[i].ToLower() == paths[j].ToLower()) {
                                paths.RemoveAt(j);
                            }
                        }
                    }

                    // Rebuild PATH
                    paths.Add(pythonPath);
                    path = paths.Unparse(';');
                    Console.WriteLine(@"Adding Python installation to PATH . . .
");
                    try {
                        Environment.SetEnvironmentVariable("Path", path, EnvironmentVariableTarget.Machine);
                        Console.WriteLine(@"Your Python installation has been successfully added to PATH.
");
                        break;
                    } catch(SecurityException) {
                        Console.WriteLine(@"Adding Python installation to PATH failed.  Try running this
program as Admistrator.
");
                        break;
                    }

                } else {
                    Console.WriteLine(@"Adding Python installation to PATH failed.
");
                    break;
                }

            } // End main loop

            Console.WriteLine(@"Press ENTER to exit . . .
");
            Console.ReadLine();

        }


    }



    public static class MethodExtensions {


        /* Parse a string on delimiter <char> and convert to a List<string>
        ====================================================================== */
        public static List<string> Parse(this string unparsed, char delimiter) {

            // Define variables
            string parsedSection = "";
            List<string> parsedList = new List<string>();

            // Add delimiter to end of string to prevent "System.IndexOutOfRangeException" on last iteration
            unparsed = unparsed + Convert.ToString(delimiter);

            for (int i = 0; i < unparsed.Length; i++) {
                if (unparsed[i] != delimiter) {
                    parsedSection += unparsed[i];
                } else {
                    parsedList.Add(parsedSection);
                    parsedSection = "";
                }
            }

            return parsedList;
        }


        /* Convert a List<string> to a string separated by a delimiter <char>
        ======================================================================= */
        public static string Unparse(this List<string> list, char delimiter) {

            // Define variables
            string unparsed = "";

            for (int i = 0; i < list.Count; i++) {
                if (i + 1 < list.Count) {
                    unparsed += list[i] + delimiter;
                } else {
                    unparsed += list[i];
                }
            }

            return unparsed;
        }


    }
}
