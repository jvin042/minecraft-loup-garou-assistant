using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Windows.Forms;

namespace Minecraft_Loup_Garou_Assistant
{
    public partial class Form1 : Form
    {
        static string version = "1.0";
        static string titre = "Minecraft Loup Garou Assistant";
        static string dossierBuildSpigot = "\\build-spigot\\";
        static string dossierPlugins = "\\plugins\\";
        static string versionMinecraft = "1.15.1";
        static string Spigot = "spigot-" + versionMinecraft + ".jar";
        static string lienSpigot = "https://hub.spigotmc.org/jenkins/job/BuildTools/lastSuccessfulBuild/artifact/target/BuildTools.jar";
        static string lienLoupGarou = "https://github.com/leomelki/LoupGarou/releases/download/1.0.1/LoupGarou-1.0.1.jar";
        static string lienProtocolLib = "https://github.com/dmulloy2/ProtocolLib/releases/download/4.5.0/ProtocolLib.jar";
        static string lienConfigMedieval = "https://raw.githubusercontent.com/jvin042/minecraft-loup-garou-assistant/master/ressources/maps/config-medieval.yml";
        static string lienConfigVillage = "https://raw.githubusercontent.com/jvin042/minecraft-loup-garou-assistant/master/ressources/maps/config-village.yml";
        static string lienMapMedieval = "https://github.com/jvin042/minecraft-loup-garou-assistant/raw/master/ressources/maps/lg_medieval.zip";
        static string lienMapVillage = "https://github.com/jvin042/minecraft-loup-garou-assistant/raw/master/ressources/maps/lg_village.zip";
        static string lienIcone = "https://github.com/jvin042/minecraft-loup-garou-assistant/raw/master/ressources/server-icon.png";
        static string lienServerProperties = "https://github.com/jvin042/minecraft-loup-garou-assistant/raw/master/ressources/server.properties";
        static string lienVersion = "https://github.com/jvin042/minecraft-loup-garou-assistant/raw/master/ressources/VERSION";
        static bool world;

        public Form1()
        {
            InitializeComponent();
            MAJ();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dossierServeur = new FolderBrowserDialog();

            if (dossierServeur.ShowDialog() == DialogResult.OK)
            {
                BuildSpigot(dossierServeur);
                GenerationServeur(dossierServeur);

                var selectedOption1 = MessageBox.Show("Le serveur est OK ! Voulez-vous lancer le serveur ?", titre, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (selectedOption1 == DialogResult.Yes)
                {
                    // Lancement du serveur
                    Process proc = new Process();
                    proc.StartInfo.WorkingDirectory = dossierServeur.SelectedPath;
                    proc.StartInfo.FileName = "launch.bat";
                    proc.StartInfo.CreateNoWindow = false;
                    proc.Start();

                    Application.Exit();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dossierServeur = new FolderBrowserDialog();

            if (dossierServeur.ShowDialog() == DialogResult.OK)
            {
                GenerationServeur(dossierServeur);
                MessageBox.Show("Les fichiers du serveur sont OK ! Il ne reste plus que à les copier sur le serveur !", titre, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Minecraft Loup Garou Assistant by jvin042 (TR1NITY)\n Version " + version, titre, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void download(string URL, string path)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            WebClient wc = new WebClient();
            wc.DownloadFile(URL, path);
        }

        public static void mapVillage(FolderBrowserDialog dossierServeur)
        {
            // Suppresion du fichier de config si il exitste
            if (File.Exists(dossierServeur.SelectedPath + dossierPlugins + "LoupGarou\\config.yml")) File.Delete(dossierServeur.SelectedPath + dossierPlugins + "LoupGarou\\config.yml");

            // Téléchargement de la map et du fichier de config
            download(lienMapVillage, dossierServeur.SelectedPath + "\\lg_village.zip");
            download(lienConfigVillage, dossierServeur.SelectedPath + dossierPlugins + "LoupGarou\\config.yml");

            // Extration du fichier de map
            ZipFile.ExtractToDirectory(dossierServeur.SelectedPath + "\\lg_village.zip", dossierServeur.SelectedPath + "\\world\\");

            // Suppresion du fichier zip
            File.Delete(dossierServeur.SelectedPath + "\\lg_village.zip");
        }

        public static void mapMedieval(FolderBrowserDialog dossierServeur)
        {
            // Suppresion du fichier de config si il exitste
            if (File.Exists(dossierServeur.SelectedPath + dossierPlugins + "LoupGarou\\config.yml")) File.Delete(dossierServeur.SelectedPath + dossierPlugins + "LoupGarou\\config.yml");

            // Téléchargement de la map et du fichier de config
            download(lienMapMedieval, dossierServeur.SelectedPath + "\\lg_medieval.zip");
            download(lienConfigMedieval, dossierServeur.SelectedPath + dossierPlugins + "LoupGarou\\config.yml");

            // Extration du fichier de map
            ZipFile.ExtractToDirectory(dossierServeur.SelectedPath + "\\lg_medieval.zip", dossierServeur.SelectedPath + "\\world\\");

            // Suppresion du fichier zip
            File.Delete(dossierServeur.SelectedPath + "\\lg_medieval.zip");
        }

        public static void BuildSpigot(FolderBrowserDialog dossierServeur)
        {
            // Test si le fichier spigot existe
            if (!File.Exists(dossierServeur.SelectedPath + "\\" + Spigot))
            {
                // Supprime le dossier de build existant si présent
                if (Directory.Exists(dossierServeur.SelectedPath + dossierBuildSpigot))
                {
                    Directory.Delete(dossierServeur.SelectedPath + dossierBuildSpigot, true);
                }

                // Création du dossier de build Spigot
                Directory.CreateDirectory(dossierServeur.SelectedPath + dossierBuildSpigot);

                // Téléchargement du buildtools de Spigot
                download(lienSpigot, dossierServeur.SelectedPath + dossierBuildSpigot + "buildtools.jar");

                // Création du fichier bat pour le build
                File.WriteAllText(dossierServeur.SelectedPath + dossierBuildSpigot + "build.bat", "java -Xmx1G -jar buildtools.jar --rev " + versionMinecraft);

                // Build de Spigot
                Process proc = new Process();
                proc.StartInfo.WorkingDirectory = dossierServeur.SelectedPath + dossierBuildSpigot;
                proc.StartInfo.FileName = "build.bat";
                proc.StartInfo.CreateNoWindow = false;
                proc.Start();
                proc.WaitForExit();

                // Déplacement de la build de Spigot et suppresion du dossier de build
                File.Move(dossierServeur.SelectedPath + dossierBuildSpigot + Spigot, dossierServeur.SelectedPath + "\\" + Spigot);

                // Test si le fichier launch.bat est présent
                if (!File.Exists(dossierServeur.SelectedPath + "\\launch.bat"))
                {
                    // Création du fichier
                    File.WriteAllText(dossierServeur.SelectedPath + "\\launch.bat", "echo off\ncls\ntitle Minecraft Loup Garou Assistant\njava -Xmx1G -jar " + Spigot + " nogui");
                }
            }
        }

        public static void GenerationServeur(FolderBrowserDialog dossierServeur)
        {
            // Test si le dossier plugins existe
            if (!Directory.Exists(dossierServeur.SelectedPath + dossierPlugins))
            {
                // Création du dossier plugins
                Directory.CreateDirectory(dossierServeur.SelectedPath + dossierPlugins);
            }

            // Test si le dossier Loup Garou existe
            if (!Directory.Exists(dossierServeur.SelectedPath + dossierPlugins + "\\LoupGarou"))
            {
                // Création du dossier Loup Garou
                Directory.CreateDirectory(dossierServeur.SelectedPath + dossierPlugins + "\\LoupGarou");
            }

            // Test si le dossier world existe
            if (Directory.Exists(dossierServeur.SelectedPath + "\\world")) world = true;
            else
            {
                // Création du dossier world
                Directory.CreateDirectory(dossierServeur.SelectedPath + "\\world");
                world = false;
            }

            // Test si le plugin LoupGarou est présent
            if (!File.Exists(dossierServeur.SelectedPath + dossierPlugins + "LoupGarou.jar"))
            {
                // Téléchargement du fichier
                download(lienLoupGarou, dossierServeur.SelectedPath + dossierPlugins + "LoupGarou.jar");
            }

            // Test si le plugin LoupGarou est présent
            if (!File.Exists(dossierServeur.SelectedPath + dossierPlugins + "ProtocolLib.jar"))
            {
                // Téléchargement du fichier
                download(lienProtocolLib, dossierServeur.SelectedPath + dossierPlugins + "ProtocolLib.jar");
            }

            // Test si le fichier server.properties est présent
            if (!File.Exists(dossierServeur.SelectedPath + "\\server.properties"))
            {
                // Téléchargement du fichier
                download(lienServerProperties, dossierServeur.SelectedPath + "\\server.properties");
            }

            // Test si le fichier server-icon.png est présent
            if (!File.Exists(dossierServeur.SelectedPath + "\\server-icon.png"))
            {
                // Création du fichier
                download(lienIcone, dossierServeur.SelectedPath + "\\server-icon.png");
            }

            // Test si le fichier eula.txt est présent
            if (!File.Exists(dossierServeur.SelectedPath + "\\eula.txt"))
            {
                // Création du fichier
                File.WriteAllText(dossierServeur.SelectedPath + "\\eula.txt", "eula=true");
            }

            if (!world)
            {
                var selectedOption1 = MessageBox.Show("Voulez-vous la map village ?", titre, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (selectedOption1 == DialogResult.Yes) { mapVillage(dossierServeur); }
                else if (selectedOption1 == DialogResult.No)
                {
                    var selectedOption2 = MessageBox.Show("Voulez-vous la map medieval ?", titre, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (selectedOption2 == DialogResult.Yes) { mapMedieval(dossierServeur); }
                    else if (selectedOption2 == DialogResult.No) { MessageBox.Show("Vous devez copier votre map dans le répertoire du serveur 'world'", titre, MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
            }
        }

        public static void MAJ()
        {
            if (File.Exists(Path.GetTempPath() + "VERSION")) File.Delete(Path.GetTempPath() + "VERSION");
            download(lienVersion, Path.GetTempPath() + "VERSION");
            using (StreamReader sr = new StreamReader(Path.GetTempPath() + "VERSION"))
            {
               if(version != sr.ReadLine())
               {
                    MessageBox.Show("Une nouvelle version de Assistant est disponible !", titre, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process.Start("https://github.com/jvin042/minecraft-loup-garou-assistant/releases");
               }
            }
        }
    }
}