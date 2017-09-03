using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetSorter
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            string assetFolder = Console.ReadLine();

            string championsFolder = Path.Combine(assetFolder, "champions");

            foreach (string file in Directory.GetFiles(championsFolder)) {

                if (file.Contains("_")) {
                    string championName = file.Split('_')[0];
                    string championFolder = Path.Combine(championsFolder, championName);
                    Directory.CreateDirectory(championFolder);

                    var fi = new FileInfo(file);

                    File.Move(file, Path.Combine(championFolder, fi.Name));
                }
            }

            foreach (string championFolder in Directory.GetDirectories(championsFolder)) {
                foreach (string file in Directory.GetFiles(championFolder)) {

                    var fi = new FileInfo(file);
                    string[] fileSplitName = file.ToLower().Split('_');

                    Directory.CreateDirectory(Path.Combine(championFolder, "splash"));
                    Directory.CreateDirectory(Path.Combine(championFolder, "splash_centered"));
                    Directory.CreateDirectory(Path.Combine(championFolder, "splash_tile"));
                    Directory.CreateDirectory(Path.Combine(championFolder, "square"));
                    Directory.CreateDirectory(Path.Combine(championFolder, "banner"));

                    switch (fileSplitName[1]) {
                        case "splash":
                            if (fileSplitName[2] == "centered" || fileSplitName[2] == "tile") {
                                File.Move(file, Path.Combine(championFolder, "splash_" + fileSplitName[2] + "//" + fi.Name));
                            } else {
                                File.Move(file, Path.Combine(championFolder, "splash//" + fi.Name));
                            }
                            break;
                        case "square":
                            File.Move(file, Path.Combine(championFolder, "square//" + fi.Name));
                            break;
                        default:
                            File.Move(file, Path.Combine(championFolder, "banner//" + fi.Name));
                            break;
                    }
                }
            }
        }
    }


    public class Champion
    {
        public string Name;
    }
}
