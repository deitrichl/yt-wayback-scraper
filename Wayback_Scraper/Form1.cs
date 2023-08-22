using System;
using System.Drawing.Text;
using System.Runtime.Intrinsics.Arm;
using System.Web;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;
using System.Runtime.InteropServices;


namespace Wayback_Scraper
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Makes window movable
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);

            //Confirms that video select list is empty & download list files are deleted on start up
            listBox1.Items.Clear();
            if (File.Exists(@"..\files\selectF.txt"))
            {
                File.Delete(@"..\files\selectF.txt");
            };
            if (File.Exists(@"..\files\select2.txt"))
            {
                File.Delete(@"..\files\select2.txt");
            };

            //Checks if save directory file has been made. If not, use default .\videos
            if (File.Exists(@"..\files\save_directory.txt"))
            {
                textBox3.Text = File.ReadAllText(@"..\files\save_directory.txt");
            }
            else
            {
                string defaultDirectory = @".\videos";
                textBox3.Text = defaultDirectory.ToString();

            }



        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //Allows the Enter press when a url is entered
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(this, new EventArgs());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Resets variables for loops and deletes dummy files used for cmd lines
            int i = 0;
            int j = 0;
            int k = 0;

            if (File.Exists(@"..\files\dummy1.txt"))
            {
                File.Delete(@"..\files\dummy1.txt");
            };

            if (File.Exists(@"..\files\dummy2.txt"))
            {
                File.Delete(@"..\files\dummy2.txt");
            };



            // Write inputted url to file
            using (StreamWriter writer = new(@"..\files\new_url1.txt"))
            {
                writer.WriteLine(textBox1.Text);
            }

            if (checkBox1.Checked == true) //If the checkbox is checked, skip selection
            {
                // Check if video or playlist
                if (textBox1.Text.Contains("playlist"))
                {
                    listBox1.Items.Clear();
                    /* Receive entered url from new_url1.txt.
                     * Receive available video IDs from playlist, save to url_list1.txt.
                     * Then recieve both available and unavailable IDs from playlist, save to url_list2.txt
                     */
                    string strCmdText;
                    strCmdText = @"/C ""cd ..\ & yt-dlp.exe --get-id -a "".\files\new_url1.txt"" -i > "".\files\url_list1.txt"" & yt-dlp.exe --get-id --flat-playlist -a "".\files\new_url1.txt"" -i > "".\files\url_list2.txt"" & yt-dlp.exe --print id https://www.youtube.com/watch?v=jNQXAC9IVRw > "".\files\dummy1.txt""";
                    System.Diagnostics.Process.Start("CMD.exe", strCmdText);
                    Console.Read();

                    while (i < 5)
                    {
                        // Get only deleted IDs
                        if (File.Exists(@"..\files\dummy1.txt"))
                        {
                            var file1Lines = File.ReadAllLines(@"..\files\url_list1.txt");
                            var file2Lines = File.ReadAllLines(@"..\files\url_list2.txt");
                            IEnumerable<String> deleted_ids = file2Lines.Except(file1Lines);

                            // Append IDs to file
                            File.WriteAllLines(@"..\files\del_url1.txt", deleted_ids);
                            i = 6;
                            break;
                        }
                        Thread.Sleep(1000);

                    }

                    while (j < 5)
                    {
                        if (i == 6)
                        {

                            //Convert IDs to Wayback Machine urls
                            List<string> lines = new List<string>();
                            foreach (var line in File.ReadAllLines(@"..\files\del_url1.txt"))
                            {
                                lines.Add("https://web.archive.org/web/20120523122213/http://wayback-fakeurl.archive.org/yt/" + line);
                            }

                            File.WriteAllLines(@"..\files\del_url2.txt", lines.ToArray());

                            //Check if deleted videos are on the Wayback Machine and if so download them
                            strCmdText = @"/C ""cd ..\ & yt-dlp.exe --verbose -ci --batch-file="".\files\del_url2.txt"" -o """ + textBox3.Text + @"""\%(title)s.%(ext)s""";
                            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
                            Console.Read();

                            j = 6;
                            break;
                        }
                        Thread.Sleep(1000);

                    }

                }
                else
                {



                    //If the link is not a playlist (ie. video)

                    //Extract the ID from url
                    var url = textBox1.Text;

                    //Confirms that url is valid
                    if (!string.IsNullOrEmpty(url) && Uri.IsWellFormedUriString(url, UriKind.Absolute))
                    {
                        listBox1.Items.Clear();

                        var uri = new Uri(url);
                        var query = HttpUtility.ParseQueryString(uri.Query);
                        var videoId = string.Empty;

                        if (query.AllKeys.Contains("v"))
                        {
                            videoId = query["v"];
                            File.WriteAllText(@"..\files\del_url1.txt", videoId);

                        }
                        else
                        {
                            videoId = uri.Segments.Last();
                            File.WriteAllText(@"..\files\del_url1.txt", videoId);
                        }


                        //Convert ID to Wayback Machine urls
                        List<string> lines = new List<string>();
                        foreach (var line in File.ReadAllLines(@"..\files\del_url1.txt"))
                        {
                            lines.Add("https://web.archive.org/web/20120523122213/http://wayback-fakeurl.archive.org/yt/" + line);
                        }

                        File.WriteAllLines(@"..\files\del_url2.txt", lines.ToArray());

                        //Check if deleted video is on the Wayback Machine and if so download it
                        string strCmdText;
                        strCmdText = @"/C ""cd ..\ & yt-dlp.exe --verbose -ci --batch-file="".\files\del_url2.txt"" -o """ + textBox3.Text + @"""\%(title)s.%(ext)s""";
                        System.Diagnostics.Process.Start("CMD.exe", strCmdText);
                        Console.Read();



                    }
                    else //if url is invalid
                    {
                        string message = "Please enter a valid Youtube URL and try again.";
                        string title = "Invalid URL";
                        MessageBox.Show(message, title);
                    }
                }

            }
            else
            {

                // Check if video or playlist
                if (textBox1.Text.Contains("playlist"))
                {
                    listBox1.Items.Clear();
                    /* Receive entered url from new_url1.txt.
                     * Receive available video IDs from playlist, save to url_list1.txt.
                     * Then recieve both available and unavailable IDs from playlist, save to url_list2.txt
                     */
                    string strCmdText;
                    strCmdText = @"/C ""cd ..\ & yt-dlp.exe --get-id -a "".\files\new_url1.txt"" -i > "".\files\url_list1.txt"" & yt-dlp.exe --get-id --flat-playlist -a "".\files\new_url1.txt"" -i > "".\files\url_list2.txt"" & yt-dlp.exe --print id https://www.youtube.com/watch?v=jNQXAC9IVRw > "".\files\dummy1.txt""";
                    System.Diagnostics.Process.Start("CMD.exe", strCmdText);
                    Console.Read();

                    while (i < 5)
                    {
                        // Get only deleted IDs
                        if (File.Exists(@"..\files\dummy1.txt"))
                        {
                            var file1Lines = File.ReadAllLines(@"..\files\url_list1.txt");
                            var file2Lines = File.ReadAllLines(@"..\files\url_list2.txt");
                            IEnumerable<String> deleted_ids = file2Lines.Except(file1Lines);

                            // Append IDs to file
                            File.WriteAllLines(@"..\files\del_url1.txt", deleted_ids);
                            i = 6;
                            break;
                        }
                        Thread.Sleep(1000);

                    }

                    while (j < 5)
                    {
                        if (i == 6)
                        {

                            //Convert IDs to Wayback Machine urls
                            List<string> lines = new List<string>();
                            foreach (var line in File.ReadAllLines(@"..\files\del_url1.txt"))
                            {
                                lines.Add("https://web.archive.org/web/20120523122213/http://wayback-fakeurl.archive.org/yt/" + line);
                            }

                            File.WriteAllLines(@"..\files\del_url2.txt", lines.ToArray());

                            //Check if deleted videos are on the Wayback Machine and if so, return name and ID
                            strCmdText = @"/C ""cd ..\ & yt-dlp.exe --verbose -ci --skip-download --print ""%(title)s | %(id)s"" --batch-file="".\files\del_url2.txt"" -i > "".\files\select1.txt"" & yt-dlp.exe --print id https://www.youtube.com/watch?v=jNQXAC9IVRw > "".\files\dummy2.txt""""";
                            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
                            Console.Read();

                            j = 6;
                            break;
                        }
                        Thread.Sleep(1000);

                    }

                    while (k < 5)
                    {
                        //Convert only seletable IDS to wayback urls

                        if (File.Exists(@"..\files\dummy2.txt"))
                        {

                            List<string> lines4 = new List<string>();


                            foreach (var line in File.ReadAllLines(@"..\files\select1.txt"))
                            {
                                listBox1.Items.Add(line);
                                string lines3 = line.Substring(line.Length - 11);
                                lines4.Add("https://web.archive.org/web/20120523122213/http://wayback-fakeurl.archive.org/yt/" + lines3);

                            }
                            File.WriteAllLines(@"..\files\select2.txt", lines4.ToArray());

                            //Return error message if no deleted videos are in playlist or on the WM
                            var info = new FileInfo(@"..\files\select2.txt");
                            if (info.Length == 0)
                            {
                                string message = "Deleted videos from this playlist were not found on the Wayback Machine.";
                                string title = "No archived videos found";
                                MessageBox.Show(message, title);
                            }


                            k = 6;
                            break;
                        }

                        Thread.Sleep(1000);
                    }
                }
                else
                {



                    //If the link is not a playlist (ie. video)

                    //Extract the ID from url
                    var url = textBox1.Text;

                    //Confirms that url is valid
                    if (!string.IsNullOrEmpty(url) && Uri.IsWellFormedUriString(url, UriKind.Absolute))
                    {
                        listBox1.Items.Clear();

                        var uri = new Uri(url);
                        var query = HttpUtility.ParseQueryString(uri.Query);
                        var videoId = string.Empty;

                        if (query.AllKeys.Contains("v"))
                        {
                            videoId = query["v"];
                            File.WriteAllText(@"..\files\del_url1.txt", videoId);

                        }
                        else
                        {
                            videoId = uri.Segments.Last();
                            File.WriteAllText(@"..\files\del_url1.txt", videoId);
                        }


                        //Convert ID to Wayback Machine urls
                        List<string> lines = new List<string>();
                        foreach (var line in File.ReadAllLines(@"..\files\del_url1.txt"))
                        {
                            lines.Add("https://web.archive.org/web/20120523122213/http://wayback-fakeurl.archive.org/yt/" + line);
                        }

                        File.WriteAllLines(@"..\files\del_url2.txt", lines.ToArray());

                        //Check if deleted video is on the Wayback Machine and if so, get name and ID
                        string strCmdText;
                        strCmdText = @"/C ""cd ..\ & yt-dlp.exe --verbose -ci --skip-download --print ""%(title)s | %(id)s"" --batch-file="".\files\del_url2.txt"" -i > "".\files\select1.txt"" & yt-dlp.exe --print id https://www.youtube.com/watch?v=jNQXAC9IVRw > "".\files\dummy2.txt""""";
                        System.Diagnostics.Process.Start("CMD.exe", strCmdText);
                        Console.Read();

                        while (k < 5)
                        {
                            //Convert ID back to Wayback URL

                            if (File.Exists(@"..\files\dummy2.txt"))
                            {

                                List<string> lines4 = new List<string>();


                                foreach (var line in File.ReadAllLines(@"..\files\select1.txt"))
                                {
                                    listBox1.Items.Add(line);
                                    string lines3 = line.Substring(line.Length - 11);
                                    lines4.Add("https://web.archive.org/web/20120523122213/http://wayback-fakeurl.archive.org/yt/" + lines3);

                                }
                                File.WriteAllLines(@"..\files\select2.txt", lines4.ToArray());

                                //If no video is found on WM, return an error message
                                var info = new FileInfo(@"..\files\select2.txt");
                                if (info.Length == 0)
                                {
                                    string message = "Deleted video was not found on the Wayback Machine.";
                                    string title = "No archived video found";
                                    MessageBox.Show(message, title);
                                }


                                k = 6;
                                break;
                            }

                            Thread.Sleep(1000);
                        }

                    }
                    else //if url is invalid
                    {
                        string message = "Please enter a valid Youtube URL and try again.";
                        string title = "Invalid URL";
                        MessageBox.Show(message, title);
                    }



                }



            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            //Select save directory
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Choose a directory to save videos.";

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                //Stores chosen save directory to file 
                string sSelectedPath = fbd.SelectedPath;
                textBox3.Text = sSelectedPath.ToString();
                File.WriteAllText(@"..\files\save_directory.txt", textBox3.Text);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText(TextDataFormat.Text))
            {
                textBox1.Text = Clipboard.GetText(TextDataFormat.Text);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Download the videos selected in the listBox
            List<string> download_all = new List<string>();


            foreach (string str in listBox1.SelectedItems)
            {
                var index = listBox1.Items.IndexOf(str);

                download_all.Add(File.ReadLines(@"..\files\select2.txt").Skip(index).Take(1).First());

            }
            File.WriteAllLines(@"..\files\selectF.txt", download_all.ToArray());

            string strCmdText;
            strCmdText = @"/C ""cd ..\ & yt-dlp.exe --verbose -ci --batch-file="".\files\selectF.txt"" -o """ + textBox3.Text + @"""\%(title)s.%(ext)s""";
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
            Console.Read();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //When the button is pressed, download all videos in listBox
            string strCmdText;
            strCmdText = @"/C ""cd ..\ & yt-dlp.exe --verbose -ci --batch-file="".\files\select2.txt"" -o """ + textBox3.Text + @"""\%(title)s.%(ext)s""";
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
            Console.Read();

        }

        //Allows the x to close the window
        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Makes window movable
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //Update yt-dlp
            string strCmdText;
            strCmdText = @"/K ""cd ..\ & yt-dlp.exe -U""";
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
            Console.Read();
        }

        //For opening video directory
        private void button8_Click(object sender, EventArgs e)
        {
            if (File.Exists(@"..\files\save_directory.txt"))
            {
                System.Diagnostics.Process.Start("explorer.exe", @"" + textBox3.Text);
            }
            else
            {
                string strCmdText;
                strCmdText = @"/C ""cd ..\videos\ & start .""";
                System.Diagnostics.Process.Start("CMD.exe", strCmdText);
                Console.Read();
            }
        }
    }
}