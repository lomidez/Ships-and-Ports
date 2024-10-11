using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ports;
using ships;
using containers;

namespace ShipsAndPorts
{
    /*
     * DO NOT MODIFY THIS FILE! This class uses the classes you have implemented to verify its functionality 
     * by matching the actual output against expected output.
     */

    /*
     * Main class.
     */
    class Test
    {
        static void Main(string[] args)
        {
            //string baseDirectory = @"..\..\..\testcases\";
            string baseDirectory = @"../../../testcases/";

            for (int i = 0; i < 5; i++)
            {
                string inputFile = Path.Combine(baseDirectory, $"input_{i}.txt");
                string outputFile = Path.Combine(baseDirectory, $"output_{i}.txt");
                string expectedOutputFile = Path.Combine(baseDirectory, $"expected_output_{i}.txt");

                ProcessFile(inputFile, outputFile);

                // Compare the actual output to the expected output line by line
                var actualLines = File.ReadAllLines(outputFile);
                var expectedLines = File.ReadAllLines(expectedOutputFile);

                bool isMatch = true; // Assume the files match

                if (actualLines.Length != expectedLines.Length)
                {
                    Console.WriteLine($"Difference found in {outputFile} and {expectedOutputFile}: Number of lines are different.");
                    isMatch = false;
                }
                else
                {
                    for (int lineNum = 0; lineNum < actualLines.Length; lineNum++)
                    {
                        if (actualLines[lineNum] != expectedLines[lineNum])
                        {
                            isMatch = false;
                            Console.WriteLine($"Difference found in {outputFile} and {expectedOutputFile} at line {lineNum + 1}:");
                            Console.WriteLine($"Expected: {expectedLines[lineNum]}");
                            Console.WriteLine($"Actual:   {actualLines[lineNum]}");
                        }
                    }
                }

                if (isMatch)
                {
                    Console.WriteLine($"Test case {i} passed. \n");
                }
                else
                {
                    Console.WriteLine($"Test case {i} failed. \n");
                }
            }
        }



        static void ProcessFile(string inputFilePath, string outputFilePath)
        {
            var lines = File.ReadAllLines(inputFilePath);
            // Console.WriteLine(string.Join(Environment.NewLine, lines));
            int N = int.Parse(lines[0]);

            List<Container> conts = new List<Container>();
            List<Ship> ships = new List<Ship>();
            List<Port> ports = new List<Port>();

            int currentLine = 1;
            while (currentLine <= N)
            {
                var parts = lines[currentLine].Split(' ');
                int operation_type = int.Parse(parts[0]);

                switch (operation_type)
                {
                    case 1:
                        {
                            int cont_ID = conts.Count;
                            int port_ID = int.Parse(parts[1]);
                            int weight = int.Parse(parts[2]);

                            Container cont;
                            if (parts.Length == 3 && char.IsDigit(parts[2][0]))
                            {
                                if (weight > 3000)
                                    cont = new HeavyContainer(cont_ID, weight);
                                else
                                    cont = new BasicContainer(cont_ID, weight);
                            }
                            else
                            {
                                char special_type = parts[3][0];
                                if (special_type == 'L')
                                    cont = new LiquidContainer(cont_ID, weight);
                                else
                                    cont = new RefrigeratedContainer(cont_ID, weight);
                            }
                            ports[port_ID].GetContainers().Add(cont);
                            conts.Add(cont);
                            break;
                        }
                    case 2:
                        {
                            int ship_ID = ships.Count;
                            int port_ID = int.Parse(parts[1]);
                            int totalWeightCapacity = int.Parse(parts[2]);
                            int maxNumberOfAllContainers = int.Parse(parts[3]);
                            int maxNumberOfHeavyContainers = int.Parse(parts[4]);
                            int maxNumberOfRefrigeratedContainers = int.Parse(parts[5]);
                            int maxNumberOfLiquidContainers = int.Parse(parts[6]);
                            double fuelConsumptionPerKM = double.Parse(parts[7]);



                            ships.Add(new Ship(ship_ID, ports[port_ID], totalWeightCapacity,
                                maxNumberOfAllContainers, maxNumberOfHeavyContainers,
                                maxNumberOfRefrigeratedContainers, maxNumberOfLiquidContainers,
                                fuelConsumptionPerKM));
                            break;
                        }
                    case 3:
                        {
                            int port_ID = ports.Count;
                            double X = double.Parse(parts[1]);
                            double Y = double.Parse(parts[2]);

                            ports.Add(new Port(port_ID, X, Y));
                            break;
                        }
                    case 4:
                        {
                            int ship_ID = int.Parse(parts[1]);
                            int cont_ID = int.Parse(parts[2]);

                            ships[ship_ID].Load(conts[cont_ID]);
                            break;
                        }
                    case 5:
                        {
                            int ship_ID = int.Parse(parts[1]);
                            int cont_ID = int.Parse(parts[2]);

                            ships[ship_ID].Unload(conts[cont_ID]);
                            break;
                        }
                    case 6:
                        {
                            int ship_ID = int.Parse(parts[1]);
                            int port_ID = int.Parse(parts[2]);

                            ships[ship_ID].SailTo(ports[port_ID]);
                            break;
                        }
                    case 7:
                        {
                            int ship_ID = int.Parse(parts[1]);
                            double fuel = double.Parse(parts[2]);

                            ships[ship_ID].Refuel(fuel);
                            break;
                        }
                    default:
                        Console.WriteLine("Invalid operation.");
                        break;
                }

                currentLine++;
            }

            using (StreamWriter sw = new StreamWriter(outputFilePath))
            {
                foreach (Port port in ports)
                {
                    sw.Write(port.ToString());
                }
            }
        }
    }
}