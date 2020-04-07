using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows.Forms;

namespace Minecraft_Loup_Garou_Assistant
{
    public partial class Form1 : Form
    {
        #region Variables
        // Dossiers
        static string dossierBuildSpigot = "\\build-spigot\\";
        static string dossierLoupGarou = "\\plugins\\LoupGarou\\";
        static string dossierPlugins = "\\plugins\\";

        // Map Village
        static string lienConfigVillage = "https://raw.githubusercontent.com/jvin042/minecraft-loup-garou-assistant/master/ressources/maps/config-village.yml";
        static string lienMapVillage = "https://github.com/jvin042/minecraft-loup-garou-assistant/raw/master/ressources/maps/lg_village.zip";

        // Map Medieval
        static string lienConfigMedieval = "https://raw.githubusercontent.com/jvin042/minecraft-loup-garou-assistant/master/ressources/maps/config-medieval.yml";
        static string lienMapMedieval = "https://github.com/jvin042/minecraft-loup-garou-assistant/raw/master/ressources/maps/lg_medieval.zip";

        // Plugin Loup Garou
        static string versionLoupGarou;
        static string lienLoupGarou;
        static string lienLoupGarouRessources = "https://github.com/jvin042/minecraft-loup-garou-assistant/raw/master/ressources/LoupGarou";

        // Plugin ProtocolLib
        static string versionProtocolLib;
        static string lienProtocolLib;
        static string lienProtocolLibRessources = "https://github.com/jvin042/minecraft-loup-garou-assistant/raw/master/ressources/ProtocolLib";

        // Spigot
        static string versionMinecraft = "1.15.1";
        static string spigot = "spigot-" + versionMinecraft + ".jar";
        static string lienSpigot = "https://cdn.getbukkit.org/spigot/" + spigot;
        static string lienSpigotBuild = "https://hub.spigotmc.org/jenkins/job/BuildTools/lastSuccessfulBuild/artifact/target/BuildTools.jar";

        // Ressources
        static string lienIcone = "https://github.com/jvin042/minecraft-loup-garou-assistant/raw/master/ressources/server-icon.png";
        static string lienServerProperties = "https://github.com/jvin042/minecraft-loup-garou-assistant/raw/master/ressources/server.properties";
        static string lienVersion = "https://github.com/jvin042/minecraft-loup-garou-assistant/raw/master/ressources/VERSION";

        // Autres
        static bool custom = false;
        static bool maj = false;
        static bool spigotBuild;
        static bool world;
        static string version = "1.2";
        static string titre = "Minecraft Loup Garou Assistant";
        #endregion

        #region Form
        public Form1()
        {
            InitializeComponent();

            // Vérification qu'il y à une MAJ du logiciel
            MajAssistant();
            LoupGarou();
            ProtocolLib();
        }
        #endregion

        #region Boutons
        private void buttonInstallationPC_Click(object sender, EventArgs e)
        {
            maj = false;
            PC();
        }

        private void buttonMajPC_Click(object sender, EventArgs e)
        {
            maj = true;
            PC();
        }

        private void buttonInstallationServeur_Click(object sender, EventArgs e)
        {
            maj = false;
            Serveur();
        }

        private void buttonAPropos_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Minecraft Loup Garou Assistant " + version + " by jvin042 (TR1NITY)" +
                            "\n\nVersion Loup Garou : " + versionLoupGarou +
                            "\nVersion ProtocolLib : " + versionProtocolLib, titre, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region Fonctions Boutons
        public static void PC()
        {
            FolderBrowserDialog dossierServeur = new FolderBrowserDialog();

            if (dossierServeur.ShowDialog() == DialogResult.OK)
            {
                if(!maj)
                {
                    if (Directory.GetDirectories(dossierServeur.SelectedPath).Length == 0 && Directory.GetFiles(dossierServeur.SelectedPath).Length == 0) { }
                    else { MessageBox.Show("Le répertoire d'installation doit être vide !", titre, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                }

                Spigot(dossierServeur);
                GenerationServeur(dossierServeur);
                launchServeur(dossierServeur);
            } 
        }

        public static void Serveur()
        {
            FolderBrowserDialog dossierServeur = new FolderBrowserDialog();

            if (dossierServeur.ShowDialog() == DialogResult.OK)
            {
                if (!maj)
                {
                    if (Directory.GetDirectories(dossierServeur.SelectedPath).Length == 0 && Directory.GetFiles(dossierServeur.SelectedPath).Length == 0) { }
                    else { MessageBox.Show("Le répertoire d'installation doit être vide !", titre, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                }

                GenerationServeur(dossierServeur);
                MessageBox.Show("Les fichiers du serveur sont OK ! Il ne reste plus que à les copier sur le serveur !", titre, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        #endregion

        #region Fonctions
        public static void launchServeur(FolderBrowserDialog dossierServeur)
        {
            if(custom)
            {
                MessageBox.Show("Le serveur est OK !", titre, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                Process.Start("explorer.exe", dossierServeur.SelectedPath);
            }
            else
            {
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

            // Si MAJ du serveur alors suppresion des plugins  existant
            if(maj)
            {
                if (File.Exists(dossierServeur.SelectedPath + dossierPlugins + "LoupGarou.jar"))
                {
                    // Suppresion du fichier Loup Garou
                    File.Delete(dossierServeur.SelectedPath + dossierPlugins + "LoupGarou.jar");
                }

                if (File.Exists(dossierServeur.SelectedPath + dossierPlugins + "ProtocolLib.jar"))
                {
                    // Suppresion du fichier Loup Garou
                    File.Delete(dossierServeur.SelectedPath + dossierPlugins + "ProtocolLib.jar");
                }
            }

            // Test si le plugin LoupGarou est présent
            if (!File.Exists(dossierServeur.SelectedPath + dossierPlugins + "LoupGarou.jar"))
            {
                // Téléchargement du fichier
                Download(lienLoupGarou, dossierServeur.SelectedPath + dossierPlugins + "LoupGarou.jar");
            }

            // Test si le plugin LoupGarou est présent
            if (!File.Exists(dossierServeur.SelectedPath + dossierPlugins + "ProtocolLib.jar"))
            {
                // Téléchargement du fichier
                Download(lienProtocolLib, dossierServeur.SelectedPath + dossierPlugins + "ProtocolLib.jar");
            }

            // Test si le fichier server.properties est présent
            if (!File.Exists(dossierServeur.SelectedPath + "\\server.properties"))
            {
                // Téléchargement du fichier
                Download(lienServerProperties, dossierServeur.SelectedPath + "\\server.properties");
            }

            // Test si le fichier server-icon.png est présent
            if (!File.Exists(dossierServeur.SelectedPath + "\\server-icon.png"))
            {
                // Création du fichier
                Download(lienIcone, dossierServeur.SelectedPath + "\\server-icon.png");
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
                    else if (selectedOption2 == DialogResult.No) { custom = true;  MessageBox.Show("Vous devez copier votre map dans le répertoire du serveur 'world'", titre, MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
            }
            else if (world)
            {
                if(maj)
                {
                    // Test maj du fichier de config loup garou
                    if (!File.Exists(dossierServeur.SelectedPath + "\\world\\village"))
                    {
                        var selectedOption1 = MessageBox.Show("Il semblerait que vous ayez la map village ! Souhaitez-vous mettre à jour le fichier de config du loup garou pour les nouveaux rôles ?", titre, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (selectedOption1 == DialogResult.Yes) 
                        {
                            File.Delete(dossierServeur.SelectedPath + dossierLoupGarou + "config.yml");
                            Download(lienConfigVillage, dossierServeur.SelectedPath + dossierLoupGarou + "config.yml"); 
                        }
                    }
                    else if (!File.Exists(dossierServeur.SelectedPath + "\\world\\medieval"))
                    {
                        var selectedOption1 = MessageBox.Show("Il semblerait que vous ayez la map medieval ! Souhaitez-vous mettre à jour le fichier de config du loup garou pour les nouveaux rôles ?", titre, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (selectedOption1 == DialogResult.Yes)
                        {
                            File.Delete(dossierServeur.SelectedPath + dossierLoupGarou + "config.yml");
                            Download(lienConfigMedieval, dossierServeur.SelectedPath + dossierLoupGarou + "config.yml");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Le logiciel n'a pas pus déterminé votre map actuelle ! Il faudra mettre à jour votre fichier de config Loup Garou à la main pour avoir les nouveaux rôles ou réinstaller votre serveur !", titre, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    }
                }
            }
        }

        public static void Spigot(FolderBrowserDialog dossierServeur)
        {
            // Si MAJ du serveur alors suppresion des plugins  existant
            if (maj)
            {
                if (File.Exists(dossierServeur.SelectedPath + "\\" + spigot))
                {
                    var selectedOption1 = MessageBox.Show("Voulez-vous réutiliser la version de Spigot existante ou faire une nouvelle installation de Spigot ?", titre, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (selectedOption1 == DialogResult.No) 
                    {
                        // Suppresion du fichier Loup Garou
                        File.Delete(dossierServeur.SelectedPath + "\\" + spigot);
                    }
                }
            }

            // Test si le fichier spigot existe
            if (!File.Exists(dossierServeur.SelectedPath + "\\" + spigot))
            {
                var selectedOption1 = MessageBox.Show("Voulez-vous build une version de Spigot ou utilisé une version déja compilé ?\n\n ATTENTION ! Si vous utilisez une version déja compilé, il y aura un message erreur au lancement du serveur !", titre, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (selectedOption1 == DialogResult.Yes) { spigotBuild = true; }
                else if (selectedOption1 == DialogResult.No) { spigotBuild = false; }

                if (spigotBuild)
                {
                    // Supprime le dossier de build existant si présent
                    if (Directory.Exists(dossierServeur.SelectedPath + dossierBuildSpigot))
                    {
                        Directory.Delete(dossierServeur.SelectedPath + dossierBuildSpigot, true);
                    }

                    // Création du dossier de build Spigot
                    Directory.CreateDirectory(dossierServeur.SelectedPath + dossierBuildSpigot);

                    // Téléchargement du buildtools de Spigot
                    Download(lienSpigotBuild, dossierServeur.SelectedPath + dossierBuildSpigot + "buildtools.jar");

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
                    File.Move(dossierServeur.SelectedPath + dossierBuildSpigot + spigot, dossierServeur.SelectedPath + "\\" + spigot);
                } 
                else if(!spigotBuild)
                {
                    // Téléchargement du fichier de Spigot
                    Download(lienSpigot, dossierServeur.SelectedPath + "\\" + spigot);
                }

                // Test si le fichier launch.bat est présent
                if (!File.Exists(dossierServeur.SelectedPath + "\\launch.bat"))
                {
                    // Création du fichier
                    File.WriteAllText(dossierServeur.SelectedPath + "\\launch.bat", "echo off\ncls\ntitle Minecraft Loup Garou Assistant\njava -Xmx1G -jar " + spigot + " nogui");
                }
            }
        }
        #endregion

        #region Maps
        public static void mapVillage(FolderBrowserDialog dossierServeur)
        {
            // Suppresion du fichier de config si il exitste
            if (File.Exists(dossierServeur.SelectedPath + dossierPlugins + "LoupGarou\\config.yml")) File.Delete(dossierServeur.SelectedPath + dossierLoupGarou + "config.yml");

            // Téléchargement de la map et du fichier de config
            Download(lienMapVillage, dossierServeur.SelectedPath + "\\lg_village.zip");
            Download(lienConfigVillage, dossierServeur.SelectedPath + dossierLoupGarou + "config.yml");

            // Extration du fichier de map
            ZipFile.ExtractToDirectory(dossierServeur.SelectedPath + "\\lg_village.zip", dossierServeur.SelectedPath + "\\world\\");

            // Suppresion du fichier zip
            File.Delete(dossierServeur.SelectedPath + "\\lg_village.zip");
        }

        public static void mapMedieval(FolderBrowserDialog dossierServeur)
        {
            // Suppresion du fichier de config si il exitste
            if (File.Exists(dossierServeur.SelectedPath + dossierPlugins + "LoupGarou\\config.yml")) File.Delete(dossierServeur.SelectedPath + dossierLoupGarou + "config.yml");

            // Téléchargement de la map et du fichier de config
            Download(lienMapMedieval, dossierServeur.SelectedPath + "\\lg_medieval.zip");
            Download(lienConfigMedieval, dossierServeur.SelectedPath + dossierLoupGarou + "config.yml");

            // Extration du fichier de map
            ZipFile.ExtractToDirectory(dossierServeur.SelectedPath + "\\lg_medieval.zip", dossierServeur.SelectedPath + "\\world\\");

            // Suppresion du fichier zip
            File.Delete(dossierServeur.SelectedPath + "\\lg_medieval.zip");
        }
        #endregion

        #region Autres
        public static void Download(string URL, string path)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            WebClient wc = new WebClient();
            wc.DownloadFile(URL, path);
        }

        public static void MajAssistant()
        {
            if (File.Exists(Path.GetTempPath() + "version")) File.Delete(Path.GetTempPath() + "version");
            Download(lienVersion, Path.GetTempPath() + "version");
            using (StreamReader sr = new StreamReader(Path.GetTempPath() + "version"))
            {
                if (version != sr.ReadLine())
                {
                    File.Delete(Path.GetTempPath() + "version");
                    MessageBox.Show("Une nouvelle version de Assistant est disponible !", titre, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process.Start("https://github.com/jvin042/minecraft-loup-garou-assistant/releases");
                }
            }
        }

        public static void LoupGarou()
        {
            List<string> list = new List<string>();
            if (File.Exists(Path.GetTempPath() + "lienLoupGarouRessources")) File.Delete(Path.GetTempPath() + "lienLoupGarouRessources");
            Download(lienLoupGarouRessources, Path.GetTempPath() + "lienLoupGarouRessources");
            using (StreamReader sr = new StreamReader(Path.GetTempPath() + "lienLoupGarouRessources"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    list.Add(line); // Add to list.
                }

                versionLoupGarou = list[0];
                lienLoupGarou = list[1];
                File.Delete(Path.GetTempPath() + "lienLoupGarouRessources");
            }
        }

        public static void ProtocolLib()
        {
            List<string> list = new List<string>();
            if (File.Exists(Path.GetTempPath() + "lienProtocolLibRessources")) File.Delete(Path.GetTempPath() + "lienProtocolLibRessources");
            Download(lienProtocolLibRessources, Path.GetTempPath() + "lienProtocolLibRessources");
            using (StreamReader sr = new StreamReader(Path.GetTempPath() + "lienProtocolLibRessources"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    list.Add(line); // Add to list.
                }

                versionProtocolLib = list[0];
                lienProtocolLib = list[1];
                File.Delete(Path.GetTempPath() + "lienProtocolLibRessources");
            }
        }
        #endregion
    }
}