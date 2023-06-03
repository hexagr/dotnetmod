using System;
using System.IO;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace test
{
    class dotnetmod
    {
        static void Main(string[] args)
        {
            if (args.Length < 2 || args.Length > 3)
            {
                Console.WriteLine("Usage: dotnetmod.exe <input file> <output file> <search query>");
                return;
            }

            string inputFile = args[0];
            string outputFile = args[1];
            string searchQuery = args.Length == 3 ? args[2].ToLower() : null;

            Console.WriteLine($"[+] Analyzing {inputFile}");

            if (searchQuery != null)
            {
                Console.WriteLine($"[+] Patching functions and methods containing '{searchQuery}'");
            }

            using (ModuleDefMD mod = ModuleDefMD.Load(inputFile))
            {
                foreach (var type in mod.GetTypes())
                {
                    if (searchQuery == null || type.FullName.ToLower().Contains(searchQuery))
                    {
                        if (type.IsNotPublic && !type.Attributes.HasFlag(TypeAttributes.Abstract))
                        {
                            type.Attributes |= TypeAttributes.Public;

                            foreach (var method in type.Methods)
                            {
                                if (method.Attributes.HasFlag(MethodAttributes.Private))
                                {
                                    method.Attributes ^= MethodAttributes.Private;
                                    method.Attributes |= MethodAttributes.Public;
                                }
                                if (method.Attributes.HasFlag(MethodAttributes.PrivateScope))
                                {
                                    method.Attributes ^= MethodAttributes.PrivateScope;
                                }
                                method.Attributes |= MethodAttributes.Public;
                            }

                            foreach (var field in type.Fields)
                            {
                                /* field.Attributes |= FieldAttributes.Public; */
                            }

                            foreach (var nest in type.NestedTypes)
                            {
                                /* nest.Attributes |= TypeAttributes.Public; */
                            }
                        }
                    }
                }

                mod.Write(outputFile);
                Console.WriteLine($"[+] Outputting to: {outputFile}");
            }
        }
    }
}

