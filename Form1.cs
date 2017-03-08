using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace marketshare_geography
{
    public partial class Form1 : Form
    {

        public class universityclass
        {
            public int number = -1;
            public List<string> kommun = new List<string>(); //Kommun(er) där lärosätet ligger
            public List<string> lan = new List<string>();    //Län där lärosätet ligger
            public string merged_with = ""; //Namn på lärosäte som detta har uppgått i eller bytt namn till
            public double lat = 0.0; //latitud
            public double lon = 0.0; //longitud
            public string shortform = "";
            public timestudclass beginners = new timestudclass(); //Nybörjare totalt vid detta lärosäte
            public timestudclass registered = new timestudclass(); //Registrerade totalt vid detta lärosäte

            public void addstudsetbeg(int year, bool ht, studsetclass sc)
            {
                if (!String.IsNullOrEmpty(merged_with))
                    unidict[merged_with].addstudsetbeg(year, ht, sc);
                else
                {
                    beginners.addstudset(year, ht, sc);
                }
            }
            public void addstudsetreg(int year, bool ht, studsetclass sc)
            {
                if (!String.IsNullOrEmpty(merged_with))
                {
                    unidict[merged_with].addstudsetreg(year, ht, sc);
                }
                else
                {
                    registered.addstudset(year, ht, sc);
                }
            }

            public int getnumber()
            {
                if (String.IsNullOrEmpty(merged_with))
                    return number;
                else
                    return unidict[merged_with].getnumber();
            }
        }

        public class subjectclass
        {
            public int number = -1;
            public List<string> uni = new List<string>();    //Lärosäten där ämnet finns
            public timestudclass registered = new timestudclass(); //Registrerade totalt i detta ämne
            public string sector;
            public string subjectgroup;

            public void addstudset(int year, bool ht, studsetclass sc)
            {
                registered.addstudset(year, ht, sc);
            }
        }

        public class subjectgroupclass
        {
            public int number = -1;
            public List<string> uni = new List<string>();    //Lärosäten där ämnesgruppen finns
            public timestudclass registered = new timestudclass(); //Registrerade totalt i denna ämnesgrupp
            public string sector;
            public List<string> subjects = new List<string>();

            public void addstudset(int year, bool ht, studsetclass sc)
            {
                registered.addstudset(year, ht, sc);
            }
        }

        public class sectorclass
        {
            public int number = -1;
            public List<string> uni = new List<string>();    //Lärosäten där sektorn finns
            public timestudclass registered = new timestudclass(); //Registrerade totalt i denna sektor
            public List<string> subjects = new List<string>();
            public List<string> subjectgroups = new List<string>();

            public void addstudset(int year, bool ht, studsetclass sc)
            {
                registered.addstudset(year, ht, sc);
            }
        }

        public class kommunclass
        {
            public int number = -1;
            public List<string> university = new List<string>(); //Lärosäte(n) i kommunen
            public string lan = "";    //Län där kommunen ligger
            public string merged_with = ""; //Namn på kommun som denna har uppgått i eller bytt namn till
            public double lat = 0.0; //latitud
            public double lon = 0.0; //longitud
            public int pop = 0; //befolkning
            public timestudclass beginners = new timestudclass(); //Nybörjare totalt från denna kommun

            public void addstudset(int year, bool ht, studsetclass sc)
            {
                if (!String.IsNullOrEmpty(merged_with))
                    kommundict[merged_with].addstudset(year, ht, sc);
                else
                {
                    beginners.addstudset(year, ht, sc);
                }
            }

            public int getnumber()
            {
                if (String.IsNullOrEmpty(merged_with))
                    return number;
                else
                    return kommundict[merged_with].getnumber();
            }
        }

        public class lanclass
        {
            public int number = -1;
            public List<string> university = new List<string>(); //Lärosäte(n) i länet
            public List<string> kommun = new List<string>(); //kommuner i länet
            public string merged_with = ""; //Namn på län som detta har uppgått i eller bytt namn till
            public double lat = 0.0; //latitud
            public double lon = 0.0; //longitud
            public int pop = 0; //befolkning
            public timestudclass beginners = new timestudclass(); //Nybörjare totalt från detta län

            public void addstudset(int year, bool ht, studsetclass sc)
            {
                if (!String.IsNullOrEmpty(merged_with))
                    landict[merged_with].addstudset(year, ht, sc);
                else
                {
                    beginners.addstudset(year, ht, sc);
                }
            }

            public int getnumber()
            {
                if (String.IsNullOrEmpty(merged_with))
                    return number;
                else
                    return landict[merged_with].getnumber();
            }

        }

        public class studsetclass
        {
            //first index gender (0=female, 1=male)
            //second index age (0= -24, 1= 25-34, 2=35+)
            public int[,] stud = new int[2, 3] { { 0, 0, 0 }, { 0, 0, 0 } };
            public double[,] hst = new double[2, 3] { { 0, 0, 0 }, { 0, 0, 0 } };
            public double[,] hpr = new double[2, 3] { { 0, 0, 0 }, { 0, 0, 0 } };

            public string print()
            {
                return "studsetclass " + stud[0, 0] + "," + stud[0, 1] + "," + stud[0, 2] + "/" + stud[1, 0] + "," + stud[1, 1] + "," + stud[1, 2];
            }

            public string print_hst()
            {
                return "studsetclass HST " + hst[0, 0] + "," + hst[0, 1] + "," + hst[0, 2] + "/" + hst[1, 0] + "," + hst[1, 1] + "," + hst[1, 2];
            }

            public int bygender(int gender)
            {
                int sum = 0;

                if ((gender == 0) || (gender == 1))
                {
                    sum += stud[gender, 0];
                    sum += stud[gender, 1];
                    sum += stud[gender, 2];
                }

                return sum;
            }

            public double bygender_hst(int gender)
            {
                double sum = 0;

                if ((gender == 0) || (gender == 1))
                {
                    sum += hst[gender, 0];
                    sum += hst[gender, 1];
                    sum += hst[gender, 2];
                }

                return sum;
            }

            public double bygender_hpr(int gender)
            {
                double sum = 0;

                if ((gender == 0) || (gender == 1))
                {
                    sum += hpr[gender, 0];
                    sum += hpr[gender, 1];
                    sum += hpr[gender, 2];
                }

                return sum;
            }

            public int byage(int age)
            {
                int sum = 0;

                if ((age == 0) || (age == 1) || (age == 2))
                {
                    sum += stud[0, age];
                    sum += stud[1, age];
                }

                return sum;
            }

            public double byage_hst(int age)
            {
                double sum = 0;

                if ((age == 0) || (age == 1) || (age == 2))
                {
                    sum += hst[0, age];
                    sum += hst[1, age];
                }

                return sum;
            }

            public double byage_hpr(int age)
            {
                double sum = 0;

                if ((age == 0) || (age == 1) || (age == 2))
                {
                    sum += hpr[0, age];
                    sum += hpr[1, age];
                }

                return sum;
            }

            public int total()
            {
                int sum = bygender(0) + bygender(1);
                return sum;
            }

            public double total_hst()
            {
                double sum = bygender_hst(0) + bygender_hst(1);
                return sum;
            }

            public double total_hpr()
            {
                double sum = bygender_hpr(0) + bygender_hpr(1);
                return sum;
            }

            public void addtostudset(studsetclass sc)
            {
                stud[0, 0] += sc.stud[0, 0];
                stud[0, 1] += sc.stud[0, 1];
                stud[0, 2] += sc.stud[0, 2];
                stud[1, 0] += sc.stud[1, 0];
                stud[1, 1] += sc.stud[1, 1];
                stud[1, 2] += sc.stud[1, 2];

                hst[0, 0] += sc.hst[0, 0];
                hst[0, 1] += sc.hst[0, 1];
                hst[0, 2] += sc.hst[0, 2];
                hst[1, 0] += sc.hst[1, 0];
                hst[1, 1] += sc.hst[1, 1];
                hst[1, 2] += sc.hst[1, 2];

                hpr[0, 0] += sc.hpr[0, 0];
                hpr[0, 1] += sc.hpr[0, 1];
                hpr[0, 2] += sc.hpr[0, 2];
                hpr[1, 0] += sc.hpr[1, 0];
                hpr[1, 1] += sc.hpr[1, 1];
                hpr[1, 2] += sc.hpr[1, 2];

            }

            public void write(BinaryWriter bw)
            {
                bw.Write(stud[0, 0]);
                bw.Write(stud[0, 1]);
                bw.Write(stud[0, 2]);
                bw.Write(stud[1, 0]);
                bw.Write(stud[1, 1]);
                bw.Write(stud[1, 2]);
            }

            public void read(BinaryReader br)
            {
                stud[0, 0] = br.ReadInt32();
                stud[0, 1] = br.ReadInt32();
                stud[0, 2] = br.ReadInt32();
                stud[1, 0] = br.ReadInt32();
                stud[1, 1] = br.ReadInt32();
                stud[1, 2] = br.ReadInt32();
            }
        }

        public class timestudclass
        {
            public static int maxyear = 30;
            private studsetclass[] studht = new studsetclass[maxyear];
            private studsetclass[] studvt = new studsetclass[maxyear];
            public static int baseyear = 1993;

            public timestudclass()
            {
                for (int i = 0; i < maxyear; i++)
                {
                    studht[i] = new studsetclass();
                    studvt[i] = new studsetclass();
                }
            }

            public static bool validyear(int year)
            {
                if (year < baseyear)
                    return false;
                if (year > baseyear + maxyear)
                    return false;

                return true;
            }

            public int total()
            {
                int sum = 0;
                for (int i = 0; i < maxyear; i++)
                {
                    sum += studht[i].total();
                    sum += studvt[i].total();
                }

                //memo("tsc.total = " + sum);
                return sum;
            }

            public void write(BinaryWriter bw)
            {
                for (int i = 0; i < maxyear; i++)
                {
                    studht[i].write(bw);
                    studvt[i].write(bw);
                }

            }

            public void read(BinaryReader br)
            {
                for (int i = 0; i < maxyear; i++)
                {
                    studht[i].read(br);
                    studvt[i].read(br);
                }
            }


            public void addstudset(int year, bool ht, studsetclass sc)
            {
                int iyear = year - baseyear;
                if (ht)
                {
                    studht[iyear].stud[0, 0] += sc.stud[0, 0];
                    studht[iyear].stud[0, 1] += sc.stud[0, 1];
                    studht[iyear].stud[0, 2] += sc.stud[0, 2];
                    studht[iyear].stud[1, 0] += sc.stud[1, 0];
                    studht[iyear].stud[1, 1] += sc.stud[1, 1];
                    studht[iyear].stud[1, 2] += sc.stud[1, 2];
                    studht[iyear].hst[0, 0] += sc.hst[0, 0];
                    studht[iyear].hst[0, 1] += sc.hst[0, 1];
                    studht[iyear].hst[0, 2] += sc.hst[0, 2];
                    studht[iyear].hst[1, 0] += sc.hst[1, 0];
                    studht[iyear].hst[1, 1] += sc.hst[1, 1];
                    studht[iyear].hst[1, 2] += sc.hst[1, 2];
                    studht[iyear].hpr[0, 0] += sc.hpr[0, 0];
                    studht[iyear].hpr[0, 1] += sc.hpr[0, 1];
                    studht[iyear].hpr[0, 2] += sc.hpr[0, 2];
                    studht[iyear].hpr[1, 0] += sc.hpr[1, 0];
                    studht[iyear].hpr[1, 1] += sc.hpr[1, 1];
                    studht[iyear].hpr[1, 2] += sc.hpr[1, 2];
                }
                else
                {
                    studvt[iyear].stud[0, 0] += sc.stud[0, 0];
                    studvt[iyear].stud[0, 1] += sc.stud[0, 1];
                    studvt[iyear].stud[0, 2] += sc.stud[0, 2];
                    studvt[iyear].stud[1, 0] += sc.stud[1, 0];
                    studvt[iyear].stud[1, 1] += sc.stud[1, 1];
                    studvt[iyear].stud[1, 2] += sc.stud[1, 2];
                    studvt[iyear].hst[0, 0] += sc.hst[0, 0];
                    studvt[iyear].hst[0, 1] += sc.hst[0, 1];
                    studvt[iyear].hst[0, 2] += sc.hst[0, 2];
                    studvt[iyear].hst[1, 0] += sc.hst[1, 0];
                    studvt[iyear].hst[1, 1] += sc.hst[1, 1];
                    studvt[iyear].hst[1, 2] += sc.hst[1, 2];
                    studvt[iyear].hpr[0, 0] += sc.hpr[0, 0];
                    studvt[iyear].hpr[0, 1] += sc.hpr[0, 1];
                    studvt[iyear].hpr[0, 2] += sc.hpr[0, 2];
                    studvt[iyear].hpr[1, 0] += sc.hpr[1, 0];
                    studvt[iyear].hpr[1, 1] += sc.hpr[1, 1];
                    studvt[iyear].hpr[1, 2] += sc.hpr[1, 2];
                }
            }

            public studsetclass getstudset(int year, bool ht)
            {
                int iyear = year - baseyear;
                if (ht)
                {
                    return studht[iyear];
                }
                else
                {
                    return studvt[iyear];
                }
            }

            public studsetclass getstudset(int year)
            {
                int iyear = year - baseyear;
                studsetclass sc = new studsetclass();
                sc.addtostudset(studht[iyear]);
                sc.addtostudset(studvt[iyear]);

                return sc;
            }
        }

        public class LBclass
        {
            public bool alluni = false;
            public bool totaluni = false;
            public bool allkommun = false;
            public bool totalkommun = false;
            public bool alllan = false;
            public bool totallan = false;
            public bool allyear = false;
            public bool totalyear = false;
            public bool allsubject = false;
            public bool totalsubject = false;
            public bool allsubjectgroup = false;
            public bool totalsubjectgroup = false;
            public bool allsector = false;
            public bool totalsector = false;
            public List<string> unilist = new List<string>();
            public List<string> lanlist = new List<string>();
            public List<string> kommunlist = new List<string>();
            public List<string> yearlist = new List<string>();
            public List<string> subjectlist = new List<string>();
            public List<string> subjectgrouplist = new List<string>();
            public List<string> sectorlist = new List<string>();

            public bool mk = false;
            public bool age = false;
            
        }

        public static Dictionary<string, kommunclass> kommundict = new Dictionary<string, kommunclass>();
        public static Dictionary<string, lanclass> landict = new Dictionary<string, lanclass>();
        public static Dictionary<string, universityclass> unidict = new Dictionary<string, universityclass>();
        public static Dictionary<string, subjectclass> subjectdict = new Dictionary<string, subjectclass>();
        public static Dictionary<string, subjectgroupclass> subjectgroupdict = new Dictionary<string, subjectgroupclass>();
        public static Dictionary<string, sectorclass> sectordict = new Dictionary<string, sectorclass>();
        public static Dictionary<int, string> kommunindex = new Dictionary<int, string>();
        public static Dictionary<int, string> lanindex = new Dictionary<int, string>();
        public static Dictionary<int, string> uniindex = new Dictionary<int, string>();
        public static Dictionary<int, string> subjectindex = new Dictionary<int, string>();
        //public static Dictionary<int, string> subjectgroupindex = new Dictionary<int, string>();
        //public static Dictionary<int, string> sectorindex = new Dictionary<int, string>();
        public static Dictionary<string, string> subjectgroupsector = new Dictionary<string, string>();
        public static Dictionary<string, string> subjectgroups = new Dictionary<string, string>();
        public static int nkommun = 0;
        public static int nlan = 0;
        public static int nuni = 0;
        public static int nsubject = 0;
        public static int maxkommun = 300;
        public static int maxuni = 100;
        public static int maxsubject = 200;
        public static timestudclass[,] geobeg = new timestudclass[maxkommun, maxuni]; //main data array with beginners, by kommun and uni
        public static timestudclass geototal = new timestudclass(); //total time series, summed over kommun and uni
        public static int geosum = 0; //total-total students, summed over everything
        public static timestudclass[,] subjreg = new timestudclass[maxsubject, maxuni]; //main data array with beginners, by kommun and uni
        public static timestudclass subjtotal = new timestudclass(); //total time series, summed over kommun and uni
        public static timestudclass subjectgrouptotal = new timestudclass(); //total time series, summed over kommun and uni
        public static timestudclass sectortotal = new timestudclass(); //total time series, summed over kommun and uni
        public static int subjsum = 0; //total-total students, summed over everything
        public static double hstsum = 0; //total-total HST, summed over everything
        public static double hprsum = 0; //total-total HPR, summed over everything
        public static string totallabel = ".Total";
        public static string alllabel = ".Alla";

        public Form1()
        {
            InitializeComponent();
            //read_university();
        }

        private void memo(string line)
        {
            richTextBox1.AppendText(line + "\n");
            richTextBox1.ScrollToCaret();
        }

        public LBclass checkLB()
        {
            LBclass LB = new LBclass();
            foreach (string uni in LB_uni.CheckedItems)
            {
                if (uni == totallabel)
                    LB.totaluni = true;
                else if (uni == alllabel)
                    LB.alluni = true;
                else
                    LB.unilist.Add(uni);

            }
            if ( LB.alluni)
            {
                LB.unilist.Clear();
                foreach (string uni in unidict.Keys)
                    if (String.IsNullOrEmpty(unidict[uni].merged_with))
                        LB.unilist.Add(uni);
            }

            foreach (string lan in LB_lan.CheckedItems)
            {
                if (lan == totallabel)
                    LB.totallan = true;
                else if (lan == alllabel)
                    LB.alllan = true;
                else
                    LB.lanlist.Add(lan);
            }
            if (LB.alllan)
            {
                LB.lanlist.Clear();
                foreach (string lan in landict.Keys)
                    if (String.IsNullOrEmpty(landict[lan].merged_with))
                        LB.lanlist.Add(lan);
            }

            foreach (string kommun in LB_kommun.CheckedItems)
            {
                if (kommun == totallabel)
                    LB.totalkommun = true;
                else if (kommun == alllabel)
                    LB.allkommun = true;
                else
                    LB.kommunlist.Add(kommun);
            }
            if (LB.allkommun)
            {
                LB.kommunlist.Clear();
                if (LB.lanlist.Count > 0)
                {
                    foreach (string kommun in kommundict.Keys)
                        if (LB.lanlist.Contains(kommundict[kommun].lan))
                            LB.kommunlist.Add(kommun);
                }
                else
                {
                    foreach (string kommun in kommundict.Keys)
                        if (String.IsNullOrEmpty(kommundict[kommun].merged_with))
                            LB.kommunlist.Add(kommun);
                }
            }


            foreach (string year in LB_year.CheckedItems)
            {
                if (year == totallabel)
                    LB.totalyear = true;
                else if (year == alllabel)
                    LB.allyear = true;
                else
                    LB.yearlist.Add(year);
            }
            if (LB.allyear)
            {
                LB.yearlist.Clear();
                for (int year = 1993; year < 2020; year++)
                    LB.yearlist.Add(year.ToString());
            }

            foreach (string sector in LB_sector.CheckedItems)
            {
                if (sector == totallabel)
                    LB.totalsector = true;
                else if (sector == alllabel)
                    LB.allsector = true;
                else
                    LB.sectorlist.Add(sector);
            }
            if (LB.allsector)
            {
                LB.sectorlist.Clear();
                foreach (string sector in sectordict.Keys)
                    LB.sectorlist.Add(sector);
            }


            foreach (string subjectgroup in LB_subjectgroup.CheckedItems)
            {
                if (subjectgroup == totallabel)
                    LB.totalsubjectgroup = true;
                else if (subjectgroup == alllabel)
                    LB.allsubjectgroup = true;
                else
                    LB.subjectgrouplist.Add(subjectgroup);
            }
            if (LB.allsubjectgroup)
            {
                LB.subjectgrouplist.Clear();
                foreach (string subjectgroup in subjectgroupdict.Keys)
                    LB.subjectgrouplist.Add(subjectgroup);
            }

            foreach (string subject in LB_subject.CheckedItems)
            {
                if (subject == totallabel)
                    LB.totalsubject = true;
                else if (subject == alllabel)
                    LB.allsubject = true;
                else
                    LB.subjectlist.Add(subject);
            }
            if (LB.allsubject)
            {
                LB.subjectlist.Clear();

                if (LB.sectorlist.Count > 0)
                {
                    foreach (string subject in subjectdict.Keys)
                        if (LB.sectorlist.Contains(subjectdict[subject].sector))
                            LB.subjectlist.Add(subject);
                }
                else if (LB.subjectgrouplist.Count > 0)
                {
                    foreach (string subject in subjectdict.Keys)
                        if (LB.subjectgrouplist.Contains(subjectdict[subject].subjectgroup))
                            LB.subjectlist.Add(subject);
                }
                else
                {
                    foreach (string subject in subjectdict.Keys)
                        LB.subjectlist.Add(subject);
                }
            }

            LB.mk = CBk.Checked;
            

            LB.age = CB24.Checked;
            
            memo("kommunlist.Count = " + LB.kommunlist.Count);
            memo("lanlist.Count = " + LB.lanlist.Count);
            memo("subjectlist.Count = " + LB.subjectlist.Count);
            memo("subjectgrouplist.Count = " + LB.subjectgrouplist.Count);
            memo("sectorlist.Count = " + LB.sectorlist.Count);
            memo("unilist.Count = " + LB.unilist.Count);

            return LB;
        }

        public static int tryconvert(string word)
        {
            int i = -1;

            if (word.Length == 0)
                return 0;

            try
            {
                i = Convert.ToInt32(word);
            }
            catch (OverflowException)
            {
                Console.WriteLine("i Outside the range of the Int32 type: " + word);
            }
            catch (FormatException)
            {
                //if ( !String.IsNullOrEmpty(word))
                //    Console.WriteLine("i Not in a recognizable format: " + word);
            }

            return i;

        }

        public static double tryconvertdouble(string word)
        {
            double i = -1;

            if (word.Length == 0)
                return 0;

            try
            {
                i = Convert.ToDouble(word);
            }
            catch (OverflowException)
            {
                Console.WriteLine("i Outside the range of the Double type: " + word);
            }
            catch (FormatException)
            {
                try
                {
                    i = Convert.ToDouble(word.Replace(".", ","));
                    Console.WriteLine("i converted after replace: " + word);
                }
                catch (FormatException)
                {
                    Console.WriteLine("i Not in a recognizable double format: " + word.Replace(".", ","));
                }
                
            }

            return i;

        }


        public bool read_kommun()
        {
            try
            {
                string file = @"C:\dotnwb3\marketshare-geography\kommuner.txt";
                memo(file);
                using (StreamReader sw = new StreamReader(file))
                {
                    int nlines = 0;
                    //string line = sw.ReadLine();  //header
                    while (!sw.EndOfStream)
                    {
                        nlines++;
                        string line = sw.ReadLine();
                        char splitchar = ';';
                        if (!line.Contains(splitchar)) //Handle either ; or tab as column marker
                            splitchar = '\t';
                        //memo("line=" + line);
                        string[] words = line.Split(splitchar);
                        if (words.Length > 3)
                        {
                            string name = words[0];
                            if (name.Contains("s Kommun"))
                                name = name.Replace("s Kommun", "");
                            else if (name.Contains(" Kommun"))
                                name = name.Replace(" Kommun", "");
                            //memo(name);
                            if (!String.IsNullOrEmpty(name) && !kommundict.ContainsKey(name))
                            {
                                kommunclass kk = new kommunclass();
                                kk.lat = tryconvertdouble(words[1]);
                                kk.lon = tryconvertdouble(words[2]);
                                kk.pop = tryconvert(words[3]);
                                kk.number = nkommun;
                                nkommun++;
                                kommundict.Add(name, kk);
                                kommunindex.Add(kk.number, name);
                            }
                        }
                    }
                    memo("# kommuner = " + nlines.ToString());
                }
                return true;
            }
            catch (IOException ee)
            {
                memo(ee.ToString());
                return false;
            }

        }

        public void university_data()
        {
            universityclass uu1 = new universityclass(); uu1.number = nuni; unidict.Add("Blekinge internationella hälsohögskola", uu1); uniindex.Add(nuni, "Blekinge internationella hälsohögskola"); nuni++;
            universityclass uu2 = new universityclass(); uu2.number = nuni; unidict.Add("Ersta högskola", uu2); uniindex.Add(nuni, "Ersta högskola"); nuni++;
            universityclass uu3 = new universityclass(); uu3.number = nuni; unidict.Add("Stiftelsen Stora Sköndal", uu3); uniindex.Add(nuni, "Stiftelsen Stora Sköndal"); nuni++;
            universityclass uu4 = new universityclass(); uu4.number = nuni; unidict.Add("Vårdhögskolan i Göteborg", uu4); uniindex.Add(nuni, "Vårdhögskolan i Göteborg"); nuni++;
            universityclass uu5 = new universityclass(); uu5.number = nuni; unidict.Add("Vårdhögskolan Falun", uu5); uniindex.Add(nuni, "Vårdhögskolan Falun"); nuni++;
            universityclass uu6 = new universityclass(); uu6.number = nuni; unidict.Add("Vårdhögskolan i Borås", uu6); uniindex.Add(nuni, "Vårdhögskolan i Borås"); nuni++;
            universityclass uu7 = new universityclass(); uu7.number = nuni; unidict.Add("Vårdhögskolan Gävle", uu7); uniindex.Add(nuni, "Vårdhögskolan Gävle"); nuni++;
            universityclass uu8 = new universityclass(); uu8.number = nuni; unidict.Add("Vårdhögskolan i Halland", uu8); uniindex.Add(nuni, "Vårdhögskolan i Halland"); nuni++;
            universityclass uu9 = new universityclass(); uu9.number = nuni; unidict.Add("Hälsohögskolan i Jönköping", uu9); uniindex.Add(nuni, "Hälsohögskolan i Jönköping"); nuni++;
            universityclass uu10 = new universityclass(); uu10.number = nuni; unidict.Add("Hälsohögskolan Väst, Skövde", uu10); uniindex.Add(nuni, "Hälsohögskolan Väst, Skövde"); nuni++;
            universityclass uu11 = new universityclass(); uu11.number = nuni; unidict.Add("Vårdhögskolan Kristianstad", uu11); uniindex.Add(nuni, "Vårdhögskolan Kristianstad"); nuni++;
            universityclass uu12 = new universityclass(); uu12.number = nuni; unidict.Add("Bohusläns vårdhögskola", uu12); uniindex.Add(nuni, "Bohusläns vårdhögskola"); nuni++;
            universityclass uu13 = new universityclass(); uu13.number = nuni; unidict.Add("Hälsohögskolan Väst i Vänersborg", uu13); uniindex.Add(nuni, "Hälsohögskolan Väst i Vänersborg"); nuni++;
            universityclass uu14 = new universityclass(); uu14.number = nuni; unidict.Add("Vårdhögskolan i Vänersborg", uu14); uniindex.Add(nuni, "Vårdhögskolan i Vänersborg"); nuni++;
            universityclass uu15 = new universityclass(); uu15.number = nuni; unidict.Add("Hälsohögskolan i Värmland", uu15); uniindex.Add(nuni, "Hälsohögskolan i Värmland"); nuni++;
            universityclass uu16 = new universityclass(); uu16.number = nuni; unidict.Add("Ingesunds Musikhögskola", uu16); uniindex.Add(nuni, "Ingesunds Musikhögskola"); nuni++;
            universityclass uu17 = new universityclass(); uu17.number = nuni; unidict.Add("Hälsohögskolan i Stockholm", uu17); uniindex.Add(nuni, "Hälsohögskolan i Stockholm"); nuni++;
            universityclass uu18 = new universityclass(); uu18.number = nuni; unidict.Add("Hälsouniversitetet i Linköping", uu18); uniindex.Add(nuni, "Hälsouniversitetet i Linköping"); nuni++;
            universityclass uu19 = new universityclass(); uu19.number = nuni; unidict.Add("Högskolan i Kalmar", uu19); uniindex.Add(nuni, "Högskolan i Kalmar"); nuni++;
            universityclass uu20 = new universityclass(); uu20.number = nuni; unidict.Add("Kalmar läns vårdhögskola", uu20); uniindex.Add(nuni, "Kalmar läns vårdhögskola"); nuni++;
            universityclass uu21 = new universityclass(); uu21.number = nuni; unidict.Add("Växjö universitet", uu21); uniindex.Add(nuni, "Växjö universitet"); nuni++;
            universityclass uu22 = new universityclass(); uu22.number = nuni; unidict.Add("Vårdhögskolan i Växjö", uu22); uniindex.Add(nuni, "Vårdhögskolan i Växjö"); nuni++;
            universityclass uu23 = new universityclass(); uu23.number = nuni; unidict.Add("Vårdhögskolan Boden", uu23); uniindex.Add(nuni, "Vårdhögskolan Boden"); nuni++;
            universityclass uu24 = new universityclass(); uu24.number = nuni; unidict.Add("Vårdhögskolan Lund/Helsingborg", uu24); uniindex.Add(nuni, "Vårdhögskolan Lund/Helsingborg"); nuni++;
            universityclass uu25 = new universityclass(); uu25.number = nuni; unidict.Add("Vårdhögskolan i Malmö", uu25); uniindex.Add(nuni, "Vårdhögskolan i Malmö"); nuni++;
            universityclass uu26 = new universityclass(); uu26.number = nuni; unidict.Add("Vårdhögskolan i Sundsvall/Ö-vik", uu26); uniindex.Add(nuni, "Vårdhögskolan i Sundsvall/Ö-vik"); nuni++;
            universityclass uu27 = new universityclass(); uu27.number = nuni; unidict.Add("Vårdhögskolan i Östersund", uu27); uniindex.Add(nuni, "Vårdhögskolan i Östersund"); nuni++;
            universityclass uu28 = new universityclass(); uu28.number = nuni; unidict.Add("Vårdhögskolan i Eskilstuna", uu28); uniindex.Add(nuni, "Vårdhögskolan i Eskilstuna"); nuni++;
            universityclass uu29 = new universityclass(); uu29.number = nuni; unidict.Add("Vårdhögskolan i Västerås", uu29); uniindex.Add(nuni, "Vårdhögskolan i Västerås"); nuni++;
            universityclass uu30 = new universityclass(); uu30.number = nuni; unidict.Add("Dans- och cirkushögskolan", uu30); uniindex.Add(nuni, "Dans- och cirkushögskolan"); nuni++;
            universityclass uu31 = new universityclass(); uu31.number = nuni; unidict.Add("Dramatiska institutet", uu31); uniindex.Add(nuni, "Dramatiska institutet"); nuni++;
            universityclass uu32 = new universityclass(); uu32.number = nuni; unidict.Add("Lärarhögskolan i Stockholm", uu32); uniindex.Add(nuni, "Lärarhögskolan i Stockholm"); nuni++;
            universityclass uu33 = new universityclass(); uu33.number = nuni; unidict.Add("Operahögskolan i Stockholm", uu33); uniindex.Add(nuni, "Operahögskolan i Stockholm"); nuni++;
            universityclass uu34 = new universityclass(); uu34.number = nuni; unidict.Add("Stockholms dramatiska högskola", uu34); uniindex.Add(nuni, "Stockholms dramatiska högskola"); nuni++;
            universityclass uu35 = new universityclass(); uu35.number = nuni; unidict.Add("Teaterhögskolan i Stockholm", uu35); uniindex.Add(nuni, "Teaterhögskolan i Stockholm"); nuni++;
            universityclass uu36 = new universityclass(); uu36.number = nuni; unidict.Add("Grafiska institutet/IHR", uu36); uniindex.Add(nuni, "Grafiska institutet/IHR"); nuni++;
            universityclass uu37 = new universityclass(); uu37.number = nuni; unidict.Add("Hälsohögskolan i Umeå", uu37); uniindex.Add(nuni, "Hälsohögskolan i Umeå"); nuni++;
            universityclass uu38 = new universityclass(); uu38.number = nuni; unidict.Add("Högskolan på Gotland", uu38); uniindex.Add(nuni, "Högskolan på Gotland"); nuni++;
            universityclass uu39 = new universityclass(); uu39.number = nuni; unidict.Add("Vårdhögskolan i Uppsala", uu39); uniindex.Add(nuni, "Vårdhögskolan i Uppsala"); nuni++;
            universityclass uu40 = new universityclass(); uu40.number = nuni; unidict.Add("Vårdhögskolan i Örebro", uu40); uniindex.Add(nuni, "Vårdhögskolan i Örebro"); nuni++;
            universityclass uu41 = new universityclass(); uu41.number = nuni; unidict.Add("Beckmans designhögskola", uu41); uniindex.Add(nuni, "Beckmans designhögskola"); nuni++;
            universityclass uu42 = new universityclass(); uu42.number = nuni; unidict.Add("Blekinge tekniska högskola", uu42); uniindex.Add(nuni, "Blekinge tekniska högskola"); nuni++;
            universityclass uu43 = new universityclass(); uu43.number = nuni; unidict.Add("Chalmers tekniska högskola", uu43); uniindex.Add(nuni, "Chalmers tekniska högskola"); nuni++;
            universityclass uu44 = new universityclass(); uu44.number = nuni; unidict.Add("Ericastiftelsen", uu44); uniindex.Add(nuni, "Ericastiftelsen"); nuni++;
            universityclass uu45 = new universityclass(); uu45.number = nuni; unidict.Add("Ersta Sköndal högskola", uu45); uniindex.Add(nuni, "Ersta Sköndal högskola"); nuni++;
            universityclass uu46 = new universityclass(); uu46.number = nuni; unidict.Add("Försvarshögskolan", uu46); uniindex.Add(nuni, "Försvarshögskolan"); nuni++;
            universityclass uu47 = new universityclass(); uu47.number = nuni; unidict.Add("Gammelkroppa skogsskola", uu47); uniindex.Add(nuni, "Gammelkroppa skogsskola"); nuni++;
            universityclass uu48 = new universityclass(); uu48.number = nuni; unidict.Add("Gymnastik- och idrottshögskolan", uu48); uniindex.Add(nuni, "Gymnastik- och idrottshögskolan"); nuni++;
            universityclass uu49 = new universityclass(); uu49.number = nuni; unidict.Add("Göteborgs universitet", uu49); uniindex.Add(nuni, "Göteborgs universitet"); nuni++;
            universityclass uu50 = new universityclass(); uu50.number = nuni; unidict.Add("Handelshögskolan i Stockholm", uu50); uniindex.Add(nuni, "Handelshögskolan i Stockholm"); nuni++;
            universityclass uu51 = new universityclass(); uu51.number = nuni; unidict.Add("Högskolan Dalarna", uu51); uniindex.Add(nuni, "Högskolan Dalarna"); nuni++;
            universityclass uu52 = new universityclass(); uu52.number = nuni; unidict.Add("Högskolan Kristianstad", uu52); uniindex.Add(nuni, "Högskolan Kristianstad"); nuni++;
            universityclass uu53 = new universityclass(); uu53.number = nuni; unidict.Add("Högskolan Väst", uu53); uniindex.Add(nuni, "Högskolan Väst"); nuni++;
            universityclass uu54 = new universityclass(); uu54.number = nuni; unidict.Add("Högskolan i Borås", uu54); uniindex.Add(nuni, "Högskolan i Borås"); nuni++;
            universityclass uu55 = new universityclass(); uu55.number = nuni; unidict.Add("Högskolan i Gävle", uu55); uniindex.Add(nuni, "Högskolan i Gävle"); nuni++;
            universityclass uu56 = new universityclass(); uu56.number = nuni; unidict.Add("Högskolan i Halmstad", uu56); uniindex.Add(nuni, "Högskolan i Halmstad"); nuni++;
            universityclass uu57 = new universityclass(); uu57.number = nuni; unidict.Add("Högskolan i Jönköping", uu57); uniindex.Add(nuni, "Högskolan i Jönköping"); nuni++;
            universityclass uu58 = new universityclass(); uu58.number = nuni; unidict.Add("Högskolan i Skövde", uu58); uniindex.Add(nuni, "Högskolan i Skövde"); nuni++;
            universityclass uu59 = new universityclass(); uu59.number = nuni; unidict.Add("Johannelunds teologiska högskola", uu59); uniindex.Add(nuni, "Johannelunds teologiska högskola"); nuni++;
            universityclass uu60 = new universityclass(); uu60.number = nuni; unidict.Add("Karlstads universitet", uu60); uniindex.Add(nuni, "Karlstads universitet"); nuni++;
            universityclass uu61 = new universityclass(); uu61.number = nuni; unidict.Add("Karolinska institutet", uu61); uniindex.Add(nuni, "Karolinska institutet"); nuni++;
            universityclass uu62 = new universityclass(); uu62.number = nuni; unidict.Add("Konstfack", uu62); uniindex.Add(nuni, "Konstfack"); nuni++;
            universityclass uu63 = new universityclass(); uu63.number = nuni; unidict.Add("Kungl. Konsthögskolan", uu63); uniindex.Add(nuni, "Kungl. Konsthögskolan"); nuni++;
            universityclass uu64 = new universityclass(); uu64.number = nuni; unidict.Add("Kungl. Musikhögskolan i Stockholm", uu64); uniindex.Add(nuni, "Kungl. Musikhögskolan i Stockholm"); nuni++;
            universityclass uu65 = new universityclass(); uu65.number = nuni; unidict.Add("Kungl. Tekniska högskolan", uu65); uniindex.Add(nuni, "Kungl. Tekniska högskolan"); nuni++;
            universityclass uu66 = new universityclass(); uu66.number = nuni; unidict.Add("Linköpings universitet", uu66); uniindex.Add(nuni, "Linköpings universitet"); nuni++;
            universityclass uu67 = new universityclass(); uu67.number = nuni; unidict.Add("Linnéuniversitetet", uu67); uniindex.Add(nuni, "Linnéuniversitetet"); nuni++;
            universityclass uu69 = new universityclass(); uu69.number = nuni; unidict.Add("Luleå tekniska universitet", uu69); uniindex.Add(nuni, "Luleå tekniska universitet"); nuni++;
            universityclass uu70 = new universityclass(); uu70.number = nuni; unidict.Add("Lunds universitet", uu70); uniindex.Add(nuni, "Lunds universitet"); nuni++;
            universityclass uu71 = new universityclass(); uu71.number = nuni; unidict.Add("Malmö högskola", uu71); uniindex.Add(nuni, "Malmö högskola"); nuni++;
            universityclass uu72 = new universityclass(); uu72.number = nuni; unidict.Add("Mittuniversitetet", uu72); uniindex.Add(nuni, "Mittuniversitetet"); nuni++;
            universityclass uu73 = new universityclass(); uu73.number = nuni; unidict.Add("Mälardalens högskola", uu73); uniindex.Add(nuni, "Mälardalens högskola"); nuni++;
            universityclass uu76 = new universityclass(); uu76.number = nuni; unidict.Add("Newmaninstitutet", uu76); uniindex.Add(nuni, "Newmaninstitutet"); nuni++;
            universityclass uu77 = new universityclass(); uu77.number = nuni; unidict.Add("Röda Korsets högskola", uu77); uniindex.Add(nuni, "Röda Korsets högskola"); nuni++;
            universityclass uu78 = new universityclass(); uu78.number = nuni; unidict.Add("Sophiahemmet högskola", uu78); uniindex.Add(nuni, "Sophiahemmet högskola"); nuni++;
            universityclass uu79 = new universityclass(); uu79.number = nuni; unidict.Add("Stockholms Musikpedagogiska Institut", uu79); uniindex.Add(nuni, "Stockholms Musikpedagogiska Institut"); nuni++;
            universityclass uu80 = new universityclass(); uu80.number = nuni; unidict.Add("Stockholms konstnärliga högskola", uu80); uniindex.Add(nuni, "Stockholms konstnärliga högskola"); nuni++;
            universityclass uu81 = new universityclass(); uu81.number = nuni; unidict.Add("Stockholms universitet", uu81); uniindex.Add(nuni, "Stockholms universitet"); nuni++;
            universityclass uu82 = new universityclass(); uu82.number = nuni; unidict.Add("Sveriges lantbruksuniversitet", uu82); uniindex.Add(nuni, "Sveriges lantbruksuniversitet"); nuni++;
            universityclass uu83 = new universityclass(); uu83.number = nuni; unidict.Add("Södertörns högskola", uu83); uniindex.Add(nuni, "Södertörns högskola"); nuni++;
            universityclass uu84 = new universityclass(); uu84.number = nuni; unidict.Add("Teologiska Högskolan Stockholm", uu84); uniindex.Add(nuni, "Teologiska Högskolan Stockholm"); nuni++;
            universityclass uu85 = new universityclass(); uu85.number = nuni; unidict.Add("Umeå universitet", uu85); uniindex.Add(nuni, "Umeå universitet"); nuni++;
            universityclass uu86 = new universityclass(); uu86.number = nuni; unidict.Add("Uppsala universitet", uu86); uniindex.Add(nuni, "Uppsala universitet"); nuni++;
            universityclass uu87 = new universityclass(); uu87.number = nuni; unidict.Add("Örebro teologiska högskola", uu87); uniindex.Add(nuni, "Örebro teologiska högskola"); nuni++;
            universityclass uu88 = new universityclass(); uu88.number = nuni; unidict.Add("Örebro universitet", uu88); uniindex.Add(nuni, "Örebro universitet"); nuni++;
            universityclass uu89 = new universityclass(); uu89.number = nuni; unidict.Add("Övr. enskilda anordn. psykoterapeututb.", uu89); uniindex.Add(nuni, "Övr. enskilda anordn. psykoterapeututb."); nuni++;

            unidict["Blekinge internationella hälsohögskola"].merged_with = "Blekinge tekniska högskola";
            unidict["Ersta högskola"].merged_with = "Ersta Sköndal högskola";
            unidict["Stiftelsen Stora Sköndal"].merged_with = "Ersta Sköndal högskola";
            unidict["Vårdhögskolan i Göteborg"].merged_with = "Göteborgs universitet";
            unidict["Vårdhögskolan Falun"].merged_with = "Högskolan Dalarna";
            unidict["Vårdhögskolan i Borås"].merged_with = "Högskolan i Borås";
            unidict["Vårdhögskolan Gävle"].merged_with = "Högskolan i Gävle";
            unidict["Vårdhögskolan i Halland"].merged_with = "Högskolan i Halmstad";
            unidict["Hälsohögskolan i Jönköping"].merged_with = "Högskolan i Jönköping";
            unidict["Hälsohögskolan Väst, Skövde"].merged_with = "Högskolan i Skövde";
            unidict["Vårdhögskolan Kristianstad"].merged_with = "Högskolan Kristianstad";
            unidict["Bohusläns vårdhögskola"].merged_with = "Högskolan Väst";
            unidict["Hälsohögskolan Väst i Vänersborg"].merged_with = "Högskolan Väst";
            unidict["Vårdhögskolan i Vänersborg"].merged_with = "Högskolan Väst";
            unidict["Hälsohögskolan i Värmland"].merged_with = "Karlstads universitet";
            unidict["Ingesunds Musikhögskola"].merged_with = "Karlstads universitet";
            unidict["Hälsohögskolan i Stockholm"].merged_with = "Karolinska institutet";
            unidict["Hälsouniversitetet i Linköping"].merged_with = "Linköpings universitet";
            unidict["Högskolan i Kalmar"].merged_with = "Linnéuniversitetet";
            unidict["Kalmar läns vårdhögskola"].merged_with = "Linnéuniversitetet";
            unidict["Växjö universitet"].merged_with = "Linnéuniversitetet";
            unidict["Vårdhögskolan i Växjö"].merged_with = "Linnéuniversitetet";
            unidict["Vårdhögskolan Boden"].merged_with = "Luleå tekniska universitet";
            unidict["Vårdhögskolan Lund/Helsingborg"].merged_with = "Lunds universitet";
            unidict["Vårdhögskolan i Malmö"].merged_with = "Malmö högskola";
            unidict["Vårdhögskolan i Sundsvall/Ö-vik"].merged_with = "Mittuniversitetet";
            unidict["Vårdhögskolan i Östersund"].merged_with = "Mittuniversitetet";
            unidict["Vårdhögskolan i Eskilstuna"].merged_with = "Mälardalens högskola";
            unidict["Vårdhögskolan i Västerås"].merged_with = "Mälardalens högskola";
            unidict["Dans- och cirkushögskolan"].merged_with = "Stockholms konstnärliga högskola";
            unidict["Dramatiska institutet"].merged_with = "Stockholms konstnärliga högskola";
            unidict["Lärarhögskolan i Stockholm"].merged_with = "Stockholms konstnärliga högskola";
            unidict["Operahögskolan i Stockholm"].merged_with = "Stockholms konstnärliga högskola";
            unidict["Stockholms dramatiska högskola"].merged_with = "Stockholms konstnärliga högskola";
            unidict["Teaterhögskolan i Stockholm"].merged_with = "Stockholms konstnärliga högskola";
            unidict["Grafiska institutet/IHR"].merged_with = "Stockholms universitet";
            unidict["Hälsohögskolan i Umeå"].merged_with = "Umeå universitet";
            unidict["Högskolan på Gotland"].merged_with = "Uppsala universitet";
            unidict["Vårdhögskolan i Uppsala"].merged_with = "Uppsala universitet";
            unidict["Vårdhögskolan i Örebro"].merged_with = "Örebro universitet";

            unidict["Beckmans designhögskola"].lan.Add("Stockholms län");
            unidict["Blekinge tekniska högskola"].lan.Add("Blekinge län");
            unidict["Chalmers tekniska högskola"].lan.Add("Västra Götalands län");
            unidict["Ericastiftelsen"].lan.Add("Stockholms län");
            unidict["Ersta Sköndal högskola"].lan.Add("Stockholms län");
            unidict["Försvarshögskolan"].lan.Add("Stockholms län");
            unidict["Gammelkroppa skogsskola"].lan.Add("Värmlands län");
            unidict["Gymnastik- och idrottshögskolan"].lan.Add("Stockholms län");
            unidict["Göteborgs universitet"].lan.Add("Västra Götalands län");
            unidict["Handelshögskolan i Stockholm"].lan.Add("Stockholms län");
            unidict["Högskolan Dalarna"].lan.Add("Dalarnas län");
            unidict["Högskolan Kristianstad"].lan.Add("Skåne län");
            unidict["Högskolan Väst"].lan.Add("Västra Götalands län");
            unidict["Högskolan i Borås"].lan.Add("Västra Götalands län");
            unidict["Högskolan i Gävle"].lan.Add("Gävleborgs län");
            unidict["Högskolan i Halmstad"].lan.Add("Hallands län");
            unidict["Högskolan i Jönköping"].lan.Add("Jönköpings län");
            unidict["Högskolan i Skövde"].lan.Add("Västra Götalands län");
            unidict["Johannelunds teologiska högskola"].lan.Add("Stockholms län");
            unidict["Karlstads universitet"].lan.Add("Värmlands län");
            unidict["Karolinska institutet"].lan.Add("Stockholms län");
            unidict["Konstfack"].lan.Add("Stockholms län");
            unidict["Kungl. Konsthögskolan"].lan.Add("Stockholms län");
            unidict["Kungl. Musikhögskolan i Stockholm"].lan.Add("Stockholms län");
            unidict["Kungl. Tekniska högskolan"].lan.Add("Stockholms län");
            unidict["Linköpings universitet"].lan.Add("Östergötlands län");
            unidict["Linnéuniversitetet"].lan.Add("Kronobergs län");
            unidict["Linnéuniversitetet"].lan.Add("Kalmar län");
            unidict["Luleå tekniska universitet"].lan.Add("Norrbottens län");
            unidict["Lunds universitet"].lan.Add("Skåne län");
            unidict["Malmö högskola"].lan.Add("Skåne län");
            unidict["Mittuniversitetet"].lan.Add("Västernorrlands län");
            unidict["Mälardalens högskola"].lan.Add("Södermanlands län");
            unidict["Mittuniversitetet"].lan.Add("Jämtlands län");
            unidict["Mälardalens högskola"].lan.Add("Västmanlands län");
            unidict["Newmaninstitutet"].lan.Add("Uppsala län");
            unidict["Röda Korsets högskola"].lan.Add("Stockholms län");
            unidict["Sophiahemmet högskola"].lan.Add("Stockholms län");
            unidict["Stockholms Musikpedagogiska Institut"].lan.Add("Stockholms län");
            unidict["Stockholms konstnärliga högskola"].lan.Add("Stockholms län");
            unidict["Stockholms universitet"].lan.Add("Stockholms län");
            unidict["Sveriges lantbruksuniversitet"].lan.Add("Uppsala län");
            unidict["Södertörns högskola"].lan.Add("Stockholms län");
            unidict["Teologiska Högskolan Stockholm"].lan.Add("Stockholms län");
            unidict["Umeå universitet"].lan.Add("Västerbottens län");
            unidict["Uppsala universitet"].lan.Add("Uppsala län");
            unidict["Örebro teologiska högskola"].lan.Add("Örebro län");
            unidict["Örebro universitet"].lan.Add("Örebro län");
            unidict["Övr. enskilda anordn. psykoterapeututb."].lan.Add("Stockholms län");

            unidict["Beckmans designhögskola"].kommun.Add("Stockholm");
            unidict["Blekinge tekniska högskola"].kommun.Add("Karlskrona");
            unidict["Blekinge tekniska högskola"].kommun.Add("Karlshamn");
            unidict["Chalmers tekniska högskola"].kommun.Add("Göteborg");
            unidict["Ericastiftelsen"].kommun.Add("Stockholm");
            unidict["Ersta Sköndal högskola"].kommun.Add("Stockholm");
            unidict["Ersta Sköndal högskola"].kommun.Add("Bräcke");
            unidict["Försvarshögskolan"].kommun.Add("Stockholm");
            unidict["Gammelkroppa skogsskola"].kommun.Add("Filipstad");
            unidict["Gymnastik- och idrottshögskolan"].kommun.Add("Stockholm");
            unidict["Göteborgs universitet"].kommun.Add("Göteborg");
            unidict["Handelshögskolan i Stockholm"].kommun.Add("Stockholm");
            unidict["Högskolan Dalarna"].kommun.Add("Falun");
            unidict["Högskolan Dalarna"].kommun.Add("Borlänge");
            unidict["Högskolan Kristianstad"].kommun.Add("Kristianstad");
            unidict["Högskolan Väst"].kommun.Add("Trollhättan");
            unidict["Högskolan i Borås"].kommun.Add("Borås");
            unidict["Högskolan i Gävle"].kommun.Add("Gävle");
            unidict["Högskolan i Halmstad"].kommun.Add("Halmstad");
            unidict["Högskolan i Jönköping"].kommun.Add("Jönköping");
            unidict["Högskolan i Skövde"].kommun.Add("Skövde");
            unidict["Johannelunds teologiska högskola"].kommun.Add("Stockholm");
            unidict["Karlstads universitet"].kommun.Add("Karlstad");
            unidict["Karolinska institutet"].kommun.Add("Stockholm");
            unidict["Karolinska institutet"].kommun.Add("Huddinge");
            unidict["Konstfack"].kommun.Add("Stockholm");
            unidict["Kungl. Konsthögskolan"].kommun.Add("Stockholm");
            unidict["Kungl. Musikhögskolan i Stockholm"].kommun.Add("Stockholm");
            unidict["Kungl. Tekniska högskolan"].kommun.Add("Stockholm");
            unidict["Linköpings universitet"].kommun.Add("Linköping");
            unidict["Linköpings universitet"].kommun.Add("Norrköping");
            unidict["Linnéuniversitetet"].kommun.Add("Kalmar");
            unidict["Linnéuniversitetet"].kommun.Add("Växjö");
            unidict["Luleå tekniska universitet"].kommun.Add("Luleå");
            unidict["Luleå tekniska universitet"].kommun.Add("Piteå");
            unidict["Lunds universitet"].kommun.Add("Lund");
            unidict["Lunds universitet"].kommun.Add("Helsingborg");
            unidict["Malmö högskola"].kommun.Add("Malmö");
            unidict["Mittuniversitetet"].kommun.Add("Sundsvall");
            unidict["Mälardalens högskola"].kommun.Add("Västerås");
            unidict["Mittuniversitetet"].kommun.Add("Östersund");
            unidict["Mittuniversitetet"].kommun.Add("Härnösand");
            unidict["Mälardalens högskola"].kommun.Add("Eskilstuna");
            unidict["Newmaninstitutet"].kommun.Add("Uppsala");
            unidict["Röda Korsets högskola"].kommun.Add("Stockholm");
            unidict["Sophiahemmet högskola"].kommun.Add("Stockholm");
            unidict["Stockholms Musikpedagogiska Institut"].kommun.Add("Stockholm");
            unidict["Stockholms konstnärliga högskola"].kommun.Add("Stockholm");
            unidict["Stockholms universitet"].kommun.Add("Stockholm");
            unidict["Sveriges lantbruksuniversitet"].kommun.Add("Uppsala");
            unidict["Södertörns högskola"].kommun.Add("Huddinge");
            unidict["Teologiska Högskolan Stockholm"].kommun.Add("Stockholm");
            unidict["Umeå universitet"].kommun.Add("Umeå");
            unidict["Uppsala universitet"].kommun.Add("Uppsala");
            unidict["Uppsala universitet"].kommun.Add("Gotland");
            unidict["Örebro teologiska högskola"].kommun.Add("Örebro");
            unidict["Örebro universitet"].kommun.Add("Örebro");
            unidict["Övr. enskilda anordn. psykoterapeututb."].kommun.Add("Stockholm");

            unidict["Beckmans designhögskola"].shortform = "Beckman";
            unidict["Blekinge tekniska högskola"].shortform = "BTH";
            unidict["Chalmers tekniska högskola"].shortform = "CTH";
            unidict["Ericastiftelsen"].shortform = "Erica";
            unidict["Ersta Sköndal högskola"].shortform = "ESH";
            unidict["Försvarshögskolan"].shortform = "FHS";
            unidict["Gammelkroppa skogsskola"].shortform = "Gammelkroppa";
            unidict["Gymnastik- och idrottshögskolan"].shortform = "GIH";
            unidict["Göteborgs universitet"].shortform = "GU";
            unidict["Handelshögskolan i Stockholm"].shortform = "HHS";
            unidict["Högskolan Dalarna"].shortform = "HDa";
            unidict["Högskolan Kristianstad"].shortform = "HKr";
            unidict["Högskolan Väst"].shortform = "HV";
            unidict["Högskolan i Borås"].shortform = "HB";
            unidict["Högskolan i Gävle"].shortform = "HiG";
            unidict["Högskolan i Halmstad"].shortform = "HH";
            unidict["Högskolan i Jönköping"].shortform = "JU";
            unidict["Högskolan i Skövde"].shortform = "HiS";
            unidict["Johannelunds teologiska högskola"].shortform = "JTH";
            unidict["Karlstads universitet"].shortform = "KaU";
            unidict["Karolinska institutet"].shortform = "KI";
            unidict["Konstfack"].shortform = "Konstfack";
            unidict["Kungl. Konsthögskolan"].shortform = "KKH";
            unidict["Kungl. Musikhögskolan i Stockholm"].shortform = "KMH";
            unidict["Kungl. Tekniska högskolan"].shortform = "KTH";
            unidict["Linköpings universitet"].shortform = "LiU";
            unidict["Linnéuniversitetet"].shortform = "LnU";
            unidict["Luleå tekniska universitet"].shortform = "LTU";
            unidict["Lunds universitet"].shortform = "LU";
            unidict["Malmö högskola"].shortform = "MaH";
            unidict["Mittuniversitetet"].shortform = "MiU";
            unidict["Mälardalens högskola"].shortform = "MdH";
            unidict["Newmaninstitutet"].shortform = "Newman";
            unidict["Röda Korsets högskola"].shortform = "RKH";
            unidict["Sophiahemmet högskola"].shortform = "Sophia";
            unidict["Stockholms Musikpedagogiska Institut"].shortform = "SMI";
            unidict["Stockholms konstnärliga högskola"].shortform = "SKH";
            unidict["Stockholms universitet"].shortform = "SU";
            unidict["Sveriges lantbruksuniversitet"].shortform = "SLU";
            unidict["Södertörns högskola"].shortform = "SH";
            unidict["Teologiska Högskolan Stockholm"].shortform = "THS";
            unidict["Umeå universitet"].shortform = "UmU";
            unidict["Uppsala universitet"].shortform = "UU";
            unidict["Örebro teologiska högskola"].shortform = "ÖTH";
            unidict["Örebro universitet"].shortform = "ÖrU";
            unidict["Övr. enskilda anordn. psykoterapeututb."].shortform = "Psyko";

            foreach (string uni in unidict.Keys)
            {
                foreach (string ll in unidict[uni].lan)
                    if (landict.ContainsKey(ll))
                        if (!landict[ll].university.Contains(uni))
                            landict[ll].university.Add(uni);

                foreach (string ll in unidict[uni].kommun)
                    if (kommundict.ContainsKey(ll))
                        if (!kommundict[ll].university.Contains(uni))
                            kommundict[ll].university.Add(uni);
            }
        }

        private void new_lan(string lan)
        {
            if (!landict.ContainsKey(lan))
            {
                lanclass lc = new lanclass();
                lc.number = nlan;
                landict.Add(lan, lc);
                lanindex.Add(nlan, lan);
                nlan++;
            }
        }

        private void lan_data()
        {
            new_lan("Göteborgs o Bohuslän");
            new_lan("Kristianstads län");
            new_lan("Malmöhus län");
            new_lan("Skaraborgs län");
            new_lan("Älvsborgs län");
            landict["Göteborgs o Bohuslän"].merged_with = "Västra Götalands län";
            landict["Kristianstads län"].merged_with = "Skåne län";
            landict["Malmöhus län"].merged_with = "Skåne län";
            landict["Skaraborgs län"].merged_with = "Västra Götalands län";
            landict["Älvsborgs län"].merged_with = "Västra Götalands län";

        }

        private void kommun_data()
        {
            read_kommun();

            string name = "LillaEdet";
            kommunclass kk = new kommunclass();
            kk.number = nkommun;
            nkommun++;
            kommundict.Add(name, kk);
            kommunindex.Add(kk.number, name);
            kommundict[name].merged_with = "Lilla Edet";
        }

        private void sector_data()
        {
            subjectgroupsector.Add("Historisk-filosofiska ämnen", "Humaniora och teologi");
            subjectgroupsector.Add("Journalistik, kommunikation och info.", "Humaniora och teologi");
            subjectgroupsector.Add("Religionsvetenskap", "Humaniora och teologi");
            subjectgroupsector.Add("Språkvetenskapliga ämnen", "Humaniora och teologi");
            subjectgroupsector.Add("Beteendevetenskap", "Juridik och samhällsvetenskap");
            subjectgroupsector.Add("Ekonomi/administration", "Juridik och samhällsvetenskap");
            subjectgroupsector.Add("Informatik/Data- och systemvetenskap", "Juridik och samhällsvetenskap");
            subjectgroupsector.Add("Juridik", "Juridik och samhällsvetenskap");
            subjectgroupsector.Add("Övrigt samhällsvetenskap", "Juridik och samhällsvetenskap");
            subjectgroupsector.Add("Konst", "Konstnärligt område");
            subjectgroupsector.Add("Musik", "Konstnärligt område");
            subjectgroupsector.Add("Teater, film och dans", "Konstnärligt område");
            subjectgroupsector.Add("Medicin", "Medicin och odontologi");
            subjectgroupsector.Add("Odontologi", "Medicin och odontologi");
            subjectgroupsector.Add("Veterinärmedicin", "Medicin och odontologi");
            subjectgroupsector.Add("Övrigt medicin och odontologi", "Medicin och odontologi");
            subjectgroupsector.Add("Biologi", "Naturvetenskap");
            subjectgroupsector.Add("Farmaci", "Naturvetenskap");
            subjectgroupsector.Add("Fysik", "Naturvetenskap");
            subjectgroupsector.Add("Geovetenskap", "Naturvetenskap");
            subjectgroupsector.Add("Kemi", "Naturvetenskap");
            subjectgroupsector.Add("Lant- och skogsbruk", "Naturvetenskap");
            subjectgroupsector.Add("Matematik", "Naturvetenskap");
            subjectgroupsector.Add("Övrigt naturvetenskap", "Naturvetenskap");
            subjectgroupsector.Add("Okänt", "Okänt");
            subjectgroupsector.Add("Arkitektur", "Teknik");
            subjectgroupsector.Add("Byggnadsteknik/Väg- och vatten", "Teknik");
            subjectgroupsector.Add("Datateknik", "Teknik");
            subjectgroupsector.Add("Elektroteknik", "Teknik");
            subjectgroupsector.Add("Industriell ekonomi och organisation", "Teknik");
            subjectgroupsector.Add("Kemiteknik", "Teknik");
            subjectgroupsector.Add("Lantmäteri", "Teknik");
            subjectgroupsector.Add("Maskinteknik", "Teknik");
            subjectgroupsector.Add("Samhällsbyggnadsteknik", "Teknik");
            subjectgroupsector.Add("Teknisk fysik", "Teknik");
            subjectgroupsector.Add("Övrigt teknik", "Teknik");
            subjectgroupsector.Add("Omvårdnad", "Vård och omsorg");
            subjectgroupsector.Add("Rehabilitering", "Vård och omsorg");
            subjectgroupsector.Add("Idrott och friskvård", "Övrigt område");
            subjectgroupsector.Add("Militär utbildning", "Övrigt område");
            subjectgroupsector.Add("Transport", "Övrigt område");
            subjectgroupsector.Add("Tvärvetenskap", "Övrigt område");

            subjectgroups.Add("Övriga tvärvetenskapliga studier", "Tvärvetenskap");
            subjectgroups.Add("Övriga tekniska ämnen", "Övrigt teknik");
            subjectgroups.Add("Övriga språk", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Övr. journalistik, kommunikation, info", "Journalistik, kommunikation och info.");
            subjectgroups.Add("Övr. inom transportsektorn", "Transport");
            subjectgroups.Add("Övr. inom teater, film och dans", "Teater, film och dans");
            subjectgroups.Add("Övr. inom samhällsvetenskap", "Övrigt samhällsvetenskap");
            subjectgroups.Add("Övr. inom omvårdnad", "Omvårdnad");
            subjectgroups.Add("Övr. inom naturvetenskap", "Övrigt naturvetenskap");
            subjectgroups.Add("Övr. inom medicin", "Övrigt medicin och odontologi");
            subjectgroups.Add("Övr. inom konst", "Konst");
            subjectgroups.Add("Övr. inom historisk-filosofiska ämnen", "Historisk-filosofiska ämnen");
            subjectgroups.Add("Övr. inom ekonomi och administration", "Ekonomi/administration");
            subjectgroups.Add("Övr. inom beteendevetenskap", "Beteendevetenskap");
            subjectgroups.Add("Översättning och tolkning", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Väg- och vattenbyggnad", "Byggnadsteknik/Väg- och vatten");
            subjectgroups.Add("Veterinärmedicin", "Veterinärmedicin");
            subjectgroups.Add("Utbytesstudier, okänt ämne", "Okänt");
            subjectgroups.Add("Utbildningsvetenskap/didaktik allmänt", "Beteendevetenskap");
            subjectgroups.Add("Utbildningsvetenskap teoretiska ämnen", "Beteendevetenskap");
            subjectgroups.Add("Utbildningsvet. praktisk-estetiska ämnen", "Beteendevetenskap");
            subjectgroups.Add("Ungerska", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Tyska", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Turism- och fritidsvetenskap", "Övrigt samhällsvetenskap");
            subjectgroups.Add("Träfysik och träteknologi", "Övrigt teknik");
            subjectgroups.Add("Trädgårdsvetenskap", "Lant- och skogsbruk");
            subjectgroups.Add("Tjeckiska", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Tibetanska", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Thai", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Textilteknologi", "Övrigt teknik");
            subjectgroups.Add("Terapi, rehab. och kostbehandling", "Rehabilitering");
            subjectgroups.Add("Teologi", "Religionsvetenskap");
            subjectgroups.Add("Teknisk fysik", "Teknisk fysik");
            subjectgroups.Add("Teknik i samhällsperspektiv", "Tvärvetenskap");
            subjectgroups.Add("Teckenspråk", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Tandteknik och oral hälsa", "Odontologi");
            subjectgroups.Add("Svenska/Nordiska Språk", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Svenska som andraspråk", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Swahili", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Studie- och yrkesvägledning", "Övrigt samhällsvetenskap");
            subjectgroups.Add("Statsvetenskap", "Övrigt samhällsvetenskap");
            subjectgroups.Add("Statistik", "Övrigt samhällsvetenskap");
            subjectgroups.Add("Spanska", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Sociologi", "Beteendevetenskap");
            subjectgroups.Add("Socialt arbete och social omsorg", "Beteendevetenskap");
            subjectgroups.Add("Socialantropologi", "Beteendevetenskap");
            subjectgroups.Add("Skogsvetenskap", "Lant- och skogsbruk");
            subjectgroups.Add("Sjöfart", "Transport");
            subjectgroups.Add("Scen och medier", "Teater, film och dans");
            subjectgroups.Add("Samiska", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Samhällskunskap", "Övrigt samhällsvetenskap");
            subjectgroups.Add("Samhällsbyggnadsteknik", "Samhällsbyggnadsteknik");
            subjectgroups.Add("Ryska", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Rymdteknik", "Övrigt teknik");
            subjectgroups.Add("Rumänska", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Retorik", "Historisk-filosofiska ämnen");
            subjectgroups.Add("Religionsvetenskap", "Religionsvetenskap");
            subjectgroups.Add("Regi", "Teater, film och dans");
            subjectgroups.Add("Psykoterapi", "Beteendevetenskap");
            subjectgroups.Add("Psykologi", "Beteendevetenskap");
            subjectgroups.Add("Portugisiska", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Polska", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Persiska", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Pedagogik", "Beteendevetenskap");
            subjectgroups.Add("Omvårdnad/omvårdnadsvetenskap", "Omvårdnad");
            subjectgroups.Add("Odontologi", "Odontologi");
            subjectgroups.Add("Nygrekiska", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Nutrition", "Biologi");
            subjectgroups.Add("Nederländska", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Nationalekonomi", "Ekonomi/administration");
            subjectgroups.Add("Mänskliga rättigheter", "Övrigt samhällsvetenskap");
            subjectgroups.Add("Måltids- och hushållskunskap", "Övrigt samhällsvetenskap");
            subjectgroups.Add("Musikvetenskap", "Historisk-filosofiska ämnen");
            subjectgroups.Add("Musikdramatisk scenframst. o gestaltning", "Teater, film och dans");
            subjectgroups.Add("Musik", "Musik");
            subjectgroups.Add("Miljövård och miljöskydd", "Tvärvetenskap");
            subjectgroups.Add("Miljövetenskap", "Biologi");
            subjectgroups.Add("Medieproduktion", "Journalistik, kommunikation och info.");
            subjectgroups.Add("Medie- o kommunikationsvetenskap", "Journalistik, kommunikation och info.");
            subjectgroups.Add("Medicinska tekniker", "Övrigt medicin och odontologi");
            subjectgroups.Add("Medicinsk biologi", "Biologi");
            subjectgroups.Add("Medicin", "Medicin");
            subjectgroups.Add("Materialteknik", "Övrigt teknik");
            subjectgroups.Add("Matematisk statistik", "Matematik");
            subjectgroups.Add("Matematik", "Matematik");
            subjectgroups.Add("Maskinteknik", "Maskinteknik");
            subjectgroups.Add("Länderkunskap/länderstudier", "Övrigt samhällsvetenskap");
            subjectgroups.Add("Luftfart", "Transport");
            subjectgroups.Add("Livsmedelsvetenskap", "Övrigt naturvetenskap");
            subjectgroups.Add("Litteraturvetenskap", "Historisk-filosofiska ämnen");
            subjectgroups.Add("Litauiska", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Lettiska", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Ledarskap, organisation och styrning", "Ekonomi/administration");
            subjectgroups.Add("Latin", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Lantbruksvetenskap", "Lant- och skogsbruk");
            subjectgroups.Add("Landskapsarkitektur", "Arkitektur");
            subjectgroups.Add("Kurdiska", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Kulturvård", "Historisk-filosofiska ämnen");
            subjectgroups.Add("Kulturvetenskap", "Historisk-filosofiska ämnen");
            subjectgroups.Add("Kultur- och samhällsgeografi", "Övrigt samhällsvetenskap");
            subjectgroups.Add("Kriminologi", "Beteendevetenskap");
            subjectgroups.Add("Krigsvetenskap", "Militär utbildning");
            subjectgroups.Add("Koreografi", "Teater, film och dans");
            subjectgroups.Add("Koreanska", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Konstvetenskap", "Historisk-filosofiska ämnen");
            subjectgroups.Add("Konsthantverk", "Konst");
            subjectgroups.Add("Kinesiska", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Kemiteknik", "Kemiteknik");
            subjectgroups.Add("Kemi", "Kemi");
            subjectgroups.Add("Juridik och rättsvetenskap", "Juridik");
            subjectgroups.Add("Journalistik", "Journalistik, kommunikation och info.");
            subjectgroups.Add("Japanska", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Italienska", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Informatik/Data- och systemvetenskap", "Informatik/Data- och systemvetenskap");
            subjectgroups.Add("Industriell ekonomi och organisation", "Industriell ekonomi och organisation");
            subjectgroups.Add("Indonesiska", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Indologi och sanskrit", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Idrott/idrottsvetenskap", "Idrott och friskvård");
            subjectgroups.Add("Idé- och lärdomshistoria/Idéhistoria", "Historisk-filosofiska ämnen");
            subjectgroups.Add("Hälso- och sjukvårdsutveckling", "Omvårdnad");
            subjectgroups.Add("Husdjursvetenskap", "Övrigt naturvetenskap");
            subjectgroups.Add("Historia", "Historisk-filosofiska ämnen");
            subjectgroups.Add("Hindi", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Hebreiska", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Handikappvetenskap", "Beteendevetenskap");
            subjectgroups.Add("Grekiska", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Geovetenskap och naturgeografi", "Geovetenskap");
            subjectgroups.Add("Geografisk info.teknik och lantmäteri", "Lantmäteri");
            subjectgroups.Add("Genusstudier", "Tvärvetenskap");
            subjectgroups.Add("Författande", "Konst");
            subjectgroups.Add("Företagsekonomi", "Ekonomi/administration");
            subjectgroups.Add("Fysisk planering", "Samhällsbyggnadsteknik");
            subjectgroups.Add("Fysik", "Fysik");
            subjectgroups.Add("Friskvård", "Idrott och friskvård");
            subjectgroups.Add("Fri konst", "Konst");
            subjectgroups.Add("Freds- och utvecklingsstudier", "Övrigt samhällsvetenskap");
            subjectgroups.Add("Franska", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Folkhälsovetenskap", "Omvårdnad");
            subjectgroups.Add("Flerspråkigt inriktade ämnen", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Finska", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Filosofi", "Historisk-filosofiska ämnen");
            subjectgroups.Add("Filmvetenskap", "Historisk-filosofiska ämnen");
            subjectgroups.Add("Film", "Teater, film och dans");
            subjectgroups.Add("Farmakologi", "Farmaci");
            subjectgroups.Add("Farmaci", "Farmaci");
            subjectgroups.Add("Farkostteknik", "Maskinteknik");
            subjectgroups.Add("Etnologi", "Historisk-filosofiska ämnen");
            subjectgroups.Add("Estniska", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Estetik", "Historisk-filosofiska ämnen");
            subjectgroups.Add("Engelska", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Energiteknik", "Elektroteknik");
            subjectgroups.Add("Elektroteknik", "Elektroteknik");
            subjectgroups.Add("Elektronik", "Elektroteknik");
            subjectgroups.Add("Ekonomisk historia", "Ekonomi/administration");
            subjectgroups.Add("Djuromvårdnad", "Veterinärmedicin");
            subjectgroups.Add("Design", "Konst");
            subjectgroups.Add("Datateknik", "Datateknik");
            subjectgroups.Add("Danska", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Dans- och teatervetenskap", "Historisk-filosofiska ämnen");
            subjectgroups.Add("Dans", "Teater, film och dans");
            subjectgroups.Add("Cirkus", "Teater, film och dans");
            subjectgroups.Add("Byggteknik", "Byggnadsteknik/Väg- och vatten");
            subjectgroups.Add("Bulgariska", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Bosniska/kroatiska/serbiska", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Bioteknik", "Biologi");
            subjectgroups.Add("Biomedicinsk laboratorievetenskap", "Övrigt medicin och odontologi");
            subjectgroups.Add("Biologi", "Biologi");
            subjectgroups.Add("Biblioteks- och informationsvetenskap", "Journalistik, kommunikation och info.");
            subjectgroups.Add("Berg- och mineralteknik", "Övrigt teknik");
            subjectgroups.Add("Barn- och ungdomsstudier", "Tvärvetenskap");
            subjectgroups.Add("Automatiseringsteknik", "Övrigt teknik");
            subjectgroups.Add("Arkivvetenskap", "Historisk-filosofiska ämnen");
            subjectgroups.Add("Arkitektur", "Arkitektur");
            subjectgroups.Add("Arkeologi", "Historisk-filosofiska ämnen");
            subjectgroups.Add("Arbetsvetenskap och ergonomi", "Tvärvetenskap");
            subjectgroups.Add("Arameiska/syriska", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Arabiska", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Antikens kultur", "Historisk-filosofiska ämnen");
            subjectgroups.Add("Allmän språkvetenskap/lingvistik", "Språkvetenskapliga ämnen");
            subjectgroups.Add("Administration och förvaltning", "Ekonomi/administration");

            foreach (string subject in subjectgroups.Keys)
            {
                if (!subjectdict.ContainsKey(subject))
                {
                    subjectclass lc = new subjectclass();
                    lc.number = nsubject;
                    subjectdict.Add(subject, lc);
                    subjectindex.Add(nsubject, subject);
                    nsubject++;
                    //memo("nsujbect = " + nsubject);
                }

                string subjectgroup = subjectgroups[subject];
                string sector = subjectgroupsector[subjectgroup];
                memo(subject + "\t" + subjectgroup + "\t" + sector);
                if (!subjectgroupdict.ContainsKey(subjectgroup))
                {
                    subjectgroupclass lc = new subjectgroupclass();
                    subjectgroupdict.Add(subjectgroup, lc);

                    if ( !sectordict.ContainsKey(sector))
                    {
                        sectorclass sc = new sectorclass();
                        sectordict.Add(sector, sc);
                    }
                    subjectgroupdict[subjectgroup].sector = sector;
                    sectordict[sector].subjectgroups.Add(subjectgroup);
                }

                subjectdict[subject].subjectgroup = subjectgroup;
                subjectdict[subject].sector = sector;
                subjectgroupdict[subjectgroup].subjects.Add(subject);
                sectordict[sector].subjects.Add(subject);

            }

        }

        private void geographic_data()
        {
            //Read main data file with geographic data
            try
            {
                string file = @"C:\dotnwb3\marketshare-geography\Högskolenybörjare per kommun.txt";
                memo(file);
                using (StreamReader sw = new StreamReader(file))
                {
                    int nlines = 0;
                    int nvalid = 0;
                    //string line = sw.ReadLine();  //header
                    while (!sw.EndOfStream)
                    {
                        nlines++;
                        string line = sw.ReadLine();
                        char splitchar = '\t';
                        if (!line.Contains(splitchar)) //Handle either ; or tab as column marker
                            splitchar = ';';
                        //memo("line=" + line);
                        string[] words = line.Split(splitchar);
                        if (words.Length > 3)
                        {
                            string uni = words[0];
                            if (String.IsNullOrEmpty(uni))
                                continue;
                            if (!unidict.ContainsKey(uni))
                            {
                                //universityclass uu = new universityclass(); uu.number = nuni; unidict.Add(uni, uu); uniindex.Add(nuni, uni); nuni++;
                                memo("Missing uni " + uni);
                                continue;
                            }

                            int iuni = unidict[uni].getnumber();

                            string lan = words[1];
                            if (String.IsNullOrEmpty(lan))
                                continue;
                            if (!String.IsNullOrEmpty(lan) && !landict.ContainsKey(lan))
                            {
                                lanclass lc = new lanclass();
                                lc.number = nlan;
                                landict.Add(lan, lc);
                                lanindex.Add(nlan, lan);
                                nlan++;
                            }

                            int ilan = landict[lan].getnumber();

                            string kommun = words[2];
                            if (String.IsNullOrEmpty(kommun))
                                continue;

                            if (!String.IsNullOrEmpty(kommun) && !kommundict.ContainsKey(kommun))
                            {
                                memo("Missing " + kommun);
                                kommunclass kk = new kommunclass();
                                kk.number = nkommun;
                                kommundict.Add(kommun, kk);
                                kommunindex.Add(nkommun, kommun);
                                nkommun++;
                            }
                            else
                            {
                                if (String.IsNullOrEmpty(kommundict[kommun].lan))
                                    kommundict[kommun].lan = lan;
                                if (!landict[lan].kommun.Contains(kommun))
                                    landict[lan].kommun.Add(kommun);
                            }
                            int ikommun = kommundict[kommun].getnumber();

                            studsetclass sc = new studsetclass();
                            if (words.Length > 4)
                            {
                                sc.stud[0, 0] = tryconvert(words[4]);
                                if (words.Length > 5)
                                {
                                    sc.stud[0, 1] = tryconvert(words[5]);
                                    if (words.Length > 6)
                                    {
                                        sc.stud[0, 2] = tryconvert(words[6]);
                                        if (words.Length > 7)
                                        {
                                            sc.stud[1, 0] = tryconvert(words[7]);
                                            if (words.Length > 8)
                                            {
                                                sc.stud[1, 1] = tryconvert(words[8]);
                                                if (words.Length > 9)
                                                {
                                                    sc.stud[1, 2] = tryconvert(words[9]);
                                                }
                                            }
                                        }
                                    }
                                }

                                //memo(sc.print());
                                int year = tryconvert(words[3].Substring(2));
                                if (year < 0)
                                    continue;
                                else if (year < 50)
                                    year += 2000;
                                else
                                    year += 1900;
                                nvalid++;

                                bool ht = words[3].Contains("HT");
                                if (geobeg[ikommun, iuni] == null)
                                    geobeg[ikommun, iuni] = new timestudclass();
                                geobeg[ikommun, iuni].addstudset(year, ht, sc);
                                unidict[uni].addstudsetbeg(year, ht, sc);
                                kommundict[kommun].addstudset(year, ht, sc);
                                landict[lan].addstudset(year, ht, sc);
                                geototal.addstudset(year, ht, sc);
                                //memo(sc.print());
                                //memo("sc.total = " + sc.total());
                                geosum += sc.total();

                            }


                        }
                        if ((nlines % 10000) == 0)
                        {
                            memo("# lines = " + nlines.ToString());
                            memo("Total # students = " + geosum);
                            //break;
                        }
                    }
                    memo("# lines = " + nlines.ToString());
                    memo("# valid lines = " + nvalid.ToString());
                    memo("Total # students = " + geosum);

                    //memo("Writing binary");

                    //string filename = @"testdata.bin";
                    //using (BinaryWriter bw = new BinaryWriter(File.Open(filename, FileMode.Create)))
                    //{
                    //    for (int ikommun = 0; ikommun < maxkommun; ikommun++)
                    //        for (int iuni = 0; iuni < maxuni; iuni++)
                    //        {
                    //            if (geobeg[ikommun, iuni] != null)
                    //            {
                    //                bw.Write(ikommun);
                    //                bw.Write(iuni);
                    //                geobeg[ikommun, iuni].write(bw);
                    //            }
                    //        }
                    //}
                    //memo("Done writing binary");

                    //foreach (string uni in unidict.Keys)
                    //    memo(uni);
                    //foreach (string lan in landict.Keys)
                    //    memo(lan);
                    //lan_data();

                    memo("nkommun = " + nkommun);
                    memo("nlan = " + nlan);
                    memo("nuni = " + nuni);

                }
                return;
            }
            catch (IOException ee)
            {
                memo(ee.ToString());
                return;
            }

        }

        private void subject_data()
        {
            //Read main data file with data on students per subject
            try
            {
                string file = @"C:\dotnwb3\marketshare-geography\Registrerade studenter per ämne.txt";
                memo(file);
                using (StreamReader sw = new StreamReader(file))
                {
                    int nlines = 0;
                    int nvalid = 0;
                    //string line = sw.ReadLine();  //header
                    while (!sw.EndOfStream)
                    {
                        nlines++;
                        string line = sw.ReadLine();
                        char splitchar = '\t';
                        if (!line.Contains(splitchar)) //Handle either ; or tab as column marker
                            splitchar = ';';
                        //memo("line=" + line);
                        string[] words = line.Split(splitchar);
                        if (words.Length > 3)
                        {
                            string uni = words[0];
                            if (String.IsNullOrEmpty(uni))
                                continue;
                            if (!unidict.ContainsKey(uni))
                            {
                                //universityclass uu = new universityclass(); uu.number = nuni; unidict.Add(uni, uu); uniindex.Add(nuni, uni); nuni++;
                                memo("Missing uni " + uni);
                                continue;
                            }

                            if (!String.IsNullOrEmpty(unidict[uni].merged_with))
                            {
                                uni = unidict[uni].merged_with;
                            }

                            int iuni = unidict[uni].number;

                            string subject = words[1];
                            if (String.IsNullOrEmpty(subject))
                                continue;
                            if (!String.IsNullOrEmpty(subject) && !subjectdict.ContainsKey(subject))
                            {
                                subjectclass lc = new subjectclass();
                                lc.number = nsubject;
                                subjectdict.Add(subject, lc);
                                subjectindex.Add(nsubject, subject);
                                nsubject++;
                                //memo("nsujbect = " + nsubject);
                            }

                            int isubject = subjectdict[subject].number;



                            studsetclass sc = new studsetclass();
                            if (words.Length > 3)
                            {
                                sc.stud[0, 0] = tryconvert(words[3]);
                                if (words.Length > 4)
                                {
                                    sc.stud[0, 1] = tryconvert(words[4]);
                                    if (words.Length > 5)
                                    {
                                        sc.stud[0, 2] = tryconvert(words[5]);
                                        if (words.Length > 6)
                                        {
                                            sc.stud[1, 0] = tryconvert(words[6]);
                                            if (words.Length > 7)
                                            {
                                                sc.stud[1, 1] = tryconvert(words[7]);
                                                if (words.Length > 8)
                                                {
                                                    sc.stud[1, 2] = tryconvert(words[8]);
                                                }
                                            }
                                        }
                                    }
                                }

                                //memo(sc.print());
                                int year = tryconvert(words[2].Substring(2));
                                if (year < 0)
                                    continue;
                                else if (year < 50)
                                    year += 2000;
                                else
                                    year += 1900;
                                nvalid++;

                                bool ht = words[2].Contains("HT");

                                addstudseteverywhere(sc, subject, uni, year, ht);

                                subjsum += sc.total();

                            }


                        }
                        if ((nlines % 10000) == 0)
                        {
                            memo("# lines = " + nlines.ToString());
                            memo("Total # students = " + subjsum);
                            //break;
                        }
                    }
                    memo("# lines = " + nlines.ToString());
                    memo("# valid lines = " + nvalid.ToString());
                    memo("Total # students = " + subjsum);

                    //memo("Writing binary");

                    //string filename = @"testdata.bin";
                    //using (BinaryWriter bw = new BinaryWriter(File.Open(filename, FileMode.Create)))
                    //{
                    //    for (int ikommun = 0; ikommun < maxkommun; ikommun++)
                    //        for (int iuni = 0; iuni < maxuni; iuni++)
                    //        {
                    //            if (geobeg[ikommun, iuni] != null)
                    //            {
                    //                bw.Write(ikommun);
                    //                bw.Write(iuni);
                    //                geobeg[ikommun, iuni].write(bw);
                    //            }
                    //        }
                    //}
                    //memo("Done writing binary");

                    //foreach (string uni in unidict.Keys)
                    //    memo(uni);
                    //foreach (string lan in landict.Keys)
                    //    memo(lan);
                    //lan_data();

                    memo("nsubject = " + nsubject);
                    memo("nuni = " + nuni);

                }
                return;
            }
            catch (IOException ee)
            {
                memo(ee.ToString());
                return;
            }

        }

        private void hst_data()
        {
            //Read main data file with data on students per subject
            try
            {
                string file = @"C:\dotnwb3\marketshare-geography\Hst per ämne.txt";
                memo(file);
                using (StreamReader sw = new StreamReader(file))
                {
                    int nlines = 0;
                    int nvalid = 0;
                    //string line = sw.ReadLine();  //header
                    while (!sw.EndOfStream)
                    {
                        nlines++;
                        string line = sw.ReadLine();
                        char splitchar = '\t';
                        if (!line.Contains(splitchar)) //Handle either ; or tab as column marker
                            splitchar = ';';
                        //memo("line=" + line);
                        string[] words = line.Split(splitchar);
                        if (words.Length > 3)
                        {
                            string uni = words[0];
                            if (String.IsNullOrEmpty(uni))
                                continue;
                            if (!unidict.ContainsKey(uni))
                            {
                                //universityclass uu = new universityclass(); uu.number = nuni; unidict.Add(uni, uu); uniindex.Add(nuni, uni); nuni++;
                                memo("Missing uni " + uni);
                                continue;
                            }

                            if (!String.IsNullOrEmpty(unidict[uni].merged_with))
                            {
                                uni = unidict[uni].merged_with;
                            }


                            string subject = words[1];
                            if (String.IsNullOrEmpty(subject))
                                continue;
                            if (!String.IsNullOrEmpty(subject) && !subjectdict.ContainsKey(subject))
                            {
                                subjectclass lc = new subjectclass();
                                lc.number = nsubject;
                                subjectdict.Add(subject, lc);
                                subjectindex.Add(nsubject, subject);
                                nsubject++;
                                //memo("nsujbect = " + nsubject);
                            }




                            studsetclass sc = new studsetclass();
                            if (words.Length > 4)
                            {
                                sc.hst[0, 0] = 0.5*tryconvertdouble(words[4]); //0.5 to count half per semester
                                if (words.Length > 5)
                                {
                                    sc.hst[0, 1] = 0.5 * tryconvertdouble(words[5]);
                                    if (words.Length > 6)
                                    {
                                        sc.hst[0, 2] = 0.5 * tryconvertdouble(words[6]);
                                        if (words.Length > 7)
                                        {
                                            sc.hst[1, 0] = 0.5 * tryconvertdouble(words[7]);
                                            if (words.Length > 8)
                                            {
                                                sc.hst[1, 1] = 0.5 * tryconvertdouble(words[8]);
                                                if (words.Length > 9)
                                                {
                                                    sc.hst[1, 2] = 0.5 * tryconvertdouble(words[9]);
                                                }
                                            }
                                        }
                                    }
                                }

                                if ( nlines < 10 )
                                    memo(sc.print_hst());
                                int year = tryconvert(words[3].Split('/')[0]);
                                if (year < 0)
                                    continue;
                                nvalid++;

                                //year is first year of academic year pair


                                addstudseteverywhere(sc, subject, uni, year, true);
                                addstudseteverywhere(sc, subject, uni, year+1, false);
                                hstsum += 2*sc.total_hst();
                            }


                        }
                        if ((nlines % 10000) == 0)
                        {
                            memo("# lines = " + nlines.ToString());
                            memo("Total # HST = " + hstsum);
                            //break;
                        }
                    }
                    memo("# lines = " + nlines.ToString());
                    memo("# valid lines = " + nvalid.ToString());
                    memo("Total # HST = " + hstsum);

                    memo("nsubject = " + nsubject);
                    memo("nuni = " + nuni);

                }
                return;
            }
            catch (IOException ee)
            {
                memo(ee.ToString());
                return;
            }

        }

        private void hpr_data()
        {
            //Read main data file with data on students per subject
            try
            {
                string file = @"C:\dotnwb3\marketshare-geography\Hpr per ämne.txt";
                memo(file);
                using (StreamReader sw = new StreamReader(file))
                {
                    int nlines = 0;
                    int nvalid = 0;
                    //string line = sw.ReadLine();  //header
                    while (!sw.EndOfStream)
                    {
                        nlines++;
                        string line = sw.ReadLine();
                        char splitchar = '\t';
                        if (!line.Contains(splitchar)) //Handle either ; or tab as column marker
                            splitchar = ';';
                        //memo("line=" + line);
                        string[] words = line.Split(splitchar);
                        if (words.Length > 3)
                        {
                            string uni = words[0];
                            if (String.IsNullOrEmpty(uni))
                                continue;
                            if (!unidict.ContainsKey(uni))
                            {
                                //universityclass uu = new universityclass(); uu.number = nuni; unidict.Add(uni, uu); uniindex.Add(nuni, uni); nuni++;
                                memo("Missing uni " + uni);
                                continue;
                            }

                            if (!String.IsNullOrEmpty(unidict[uni].merged_with))
                            {
                                uni = unidict[uni].merged_with;
                            }


                            string subject = words[1];
                            if (String.IsNullOrEmpty(subject))
                                continue;
                            if (!String.IsNullOrEmpty(subject) && !subjectdict.ContainsKey(subject))
                            {
                                subjectclass lc = new subjectclass();
                                lc.number = nsubject;
                                subjectdict.Add(subject, lc);
                                subjectindex.Add(nsubject, subject);
                                nsubject++;
                                //memo("nsujbect = " + nsubject);
                            }




                            studsetclass sc = new studsetclass();
                            if (words.Length > 4)
                            {
                                sc.hpr[0, 0] = 0.5 * tryconvertdouble(words[4]); //0.5 to count half per semester
                                if (words.Length > 5)
                                {
                                    sc.hpr[0, 1] = 0.5 * tryconvertdouble(words[5]);
                                    if (words.Length > 6)
                                    {
                                        sc.hpr[0, 2] = 0.5 * tryconvertdouble(words[6]);
                                        if (words.Length > 7)
                                        {
                                            sc.hpr[1, 0] = 0.5 * tryconvertdouble(words[7]);
                                            if (words.Length > 8)
                                            {
                                                sc.hpr[1, 1] = 0.5 * tryconvertdouble(words[8]);
                                                if (words.Length > 9)
                                                {
                                                    sc.hpr[1, 2] = 0.5 * tryconvertdouble(words[9]);
                                                }
                                            }
                                        }
                                    }
                                }

                                //memo(sc.print());
                                int year = tryconvert(words[3].Split('/')[0]);
                                if (year < 0)
                                    continue;
                                nvalid++;

                                //year is first year of academic year pair


                                addstudseteverywhere(sc, subject, uni, year, true);
                                addstudseteverywhere(sc, subject, uni, year + 1, false);
                                hprsum += 2 * sc.total_hpr();

                            }


                        }
                        if ((nlines % 10000) == 0)
                        {
                            memo("# lines = " + nlines.ToString());
                            memo("Total # hpr = " + hprsum);
                            //break;
                        }
                    }
                    memo("# lines = " + nlines.ToString());
                    memo("# valid lines = " + nvalid.ToString());
                    memo("Total # hpr = " + hprsum);

                    memo("nsubject = " + nsubject);
                    memo("nuni = " + nuni);

                }
                return;
            }
            catch (IOException ee)
            {
                memo(ee.ToString());
                return;
            }

        }

        private void addstudseteverywhere(studsetclass sc, string subject, string uni, int year, bool ht)
        {
            int isubject = subjectdict[subject].number;
            int iuni = unidict[uni].number;

            if (subjreg[isubject, iuni] == null)
                subjreg[isubject, iuni] = new timestudclass();
            subjreg[isubject, iuni].addstudset(year, ht, sc);
            unidict[uni].addstudsetreg(year, ht, sc);
            subjectdict[subject].addstudset(year, ht, sc);
            subjtotal.addstudset(year, ht, sc);
            subjectgroupdict[subjectdict[subject].subjectgroup].addstudset(year, ht, sc);
            subjectgrouptotal.addstudset(year, ht, sc);
            sectordict[subjectdict[subject].sector].addstudset(year, ht, sc);
            sectortotal.addstudset(year, ht, sc);
            //memo(sc.print());
            //memo("sc.total = " + sc.total());
        }

        private void read_data()
        {
            memo("Reading data...");
            kommun_data();
            university_data();
            lan_data();
            geographic_data();
            sector_data();
            subject_data();
            hst_data();
            hpr_data();

            
            fill_listboxes();

            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button1.Enabled = false;
            
        }

        public void fill_listboxes()
        {
            LB_uni.Items.Add(totallabel);
            LB_uni.Items.Add(alllabel);
            foreach (string uni in unidict.Keys)
                if ( String.IsNullOrEmpty(unidict[uni].merged_with))
                    LB_uni.Items.Add(uni);

            LB_kommun.Items.Add(totallabel);
            LB_kommun.Items.Add(alllabel);
            foreach (string kommun in kommundict.Keys)
                if (String.IsNullOrEmpty(kommundict[kommun].merged_with))
                    LB_kommun.Items.Add(kommun);

            LB_lan.Items.Add(totallabel);
            LB_lan.Items.Add(alllabel);
            foreach (string lan in landict.Keys)
                if (String.IsNullOrEmpty(landict[lan].merged_with))
                    LB_lan.Items.Add(lan);

            LB_year.Items.Add(totallabel);
            LB_year.Items.Add(alllabel);
            for (int year=1993; year<2020;year++)
                LB_year.Items.Add(year.ToString());

            LB_subject.Items.Add(totallabel);
            LB_subject.Items.Add(alllabel);
            foreach (string subject in subjectdict.Keys)
                LB_subject.Items.Add(subject);

            LB_subjectgroup.Items.Add(totallabel);
            LB_subjectgroup.Items.Add(alllabel);
            foreach (string subjectgroup in subjectgroupdict.Keys)
                LB_subjectgroup.Items.Add(subjectgroup);

            LB_sector.Items.Add(totallabel);
            LB_sector.Items.Add(alllabel);
            foreach (string sector in sectordict.Keys)
                LB_sector.Items.Add(sector);
        }

        public double marketshare_in_kommun(int year, string uni, string kommun)
        {
            return marketshare_in_kommun(year, uni, kommun, -1, -1);
        }

        public double marketshare_in_kommun(int year, string uni, string kommun, int mk, int age)
        {
            double share = 0.0;

            if (!timestudclass.validyear(year))
                return share;
            if (!unidict.ContainsKey(uni))
                return share;
            if (!kommundict.ContainsKey(kommun))
                return share;
            if (!String.IsNullOrEmpty(unidict[uni].merged_with))
                return marketshare_in_kommun(year, unidict[uni].merged_with, kommun,mk,age);

            int total = 0;
            if (mk >= 0)
            {
                if (age >= 0)
                    total = kommundict[kommun].beginners.getstudset(year).stud[mk, age];
                else
                    total = kommundict[kommun].beginners.getstudset(year).bygender(mk);
            }
            else if (age >= 0)
            {
                total = kommundict[kommun].beginners.getstudset(year).byage(age);
            }
            else
                total = kommundict[kommun].beginners.getstudset(year).total();
            //memo("total = " + total);
            double touni = getgeobeg(kommun,uni,year,mk,age);
            share = touni / total;

            return share;
        }

        public double marketshare_in_lan(int year, string uni, string lan)
        {
            return marketshare_in_lan(year, uni, lan, -1, -1);
        }

        public double marketshare_in_lan(int year, string uni, string lan, int mk, int age)
        {
            double share = 0.0;

            if (!timestudclass.validyear(year))
                return share;
            if (!unidict.ContainsKey(uni))
                return share;
            if (!landict.ContainsKey(lan))
                return share;
            if (!String.IsNullOrEmpty(unidict[uni].merged_with))
                return marketshare_in_lan(year, unidict[uni].merged_with, lan,mk,age);

            int total = 0;
            if (mk >= 0)
            {
                if (age >= 0)
                    total = landict[lan].beginners.getstudset(year).stud[mk, age];
                else
                    total = landict[lan].beginners.getstudset(year).bygender(mk);
            }
            else if (age >= 0)
            {
                total = landict[lan].beginners.getstudset(year).byage(age);
            }
            else
                total = landict[lan].beginners.getstudset(year).total();
            double touni = 0.0;
            foreach (string kommun in landict[lan].kommun)
                touni += getgeobeg(kommun, uni, year,mk,age);
            share = touni / total;

            return share;
        }

        public double marketshare_in_sector(int year, string uni, string sector)
        {
            return marketshare_in_sector(year, uni, sector, -1,-1);
        }


        public double marketshare_in_sector(int year, string uni, string sector, int mk, int age)
        {
            double share = 0.0;

            if (!timestudclass.validyear(year))
                return share;
            if (!unidict.ContainsKey(uni))
                return share;
            if (!sectordict.ContainsKey(sector))
                return share;

            //int total = sectordict[sector].registered.getstudset(year).total();
            int total = 0;
            if (mk >= 0)
            {
                if (age >= 0)
                    total = sectordict[sector].registered.getstudset(year).stud[mk, age];
                else
                    total = sectordict[sector].registered.getstudset(year).bygender(mk);
            }
            else if (age >= 0)
            {
                total = sectordict[sector].registered.getstudset(year).byage(age);
            }
            else
                total = sectordict[sector].registered.getstudset(year).total();

            double touni = 0.0;
            foreach (string subject in sectordict[sector].subjects)
                touni += getsubjreg(subject, uni, year,mk,age);
            share = touni / total;

            return share;
        }

        public double marketshare_in_subjectgroup(int year, string uni, string subjectgroup)
        {
            return marketshare_in_subjectgroup(year, uni, subjectgroup, -1,-1);
        }

        public double marketshare_in_subjectgroup(int year, string uni, string subjectgroup, int mk, int age)
        {
            double share = 0.0;

            if (!timestudclass.validyear(year))
                return share;
            if (!unidict.ContainsKey(uni))
                return share;
            if (!subjectgroupdict.ContainsKey(subjectgroup))
                return share;

            int total = 0;
            if (mk >= 0)
            {
                if (age >= 0)
                    total = subjectgroupdict[subjectgroup].registered.getstudset(year).stud[mk, age];
                else
                    total = subjectgroupdict[subjectgroup].registered.getstudset(year).bygender(mk);
            }
            else if (age >= 0)
            {
                total = subjectgroupdict[subjectgroup].registered.getstudset(year).byage(age);
            }
            else
                total = subjectgroupdict[subjectgroup].registered.getstudset(year).total();
            double touni = 0.0;
            foreach (string subject in subjectgroupdict[subjectgroup].subjects)
                touni += getsubjreg(subject, uni, year,mk,age);
            share = touni / total;

            return share;
        }

        public double marketshare_in_subject(int year, string uni, string subject)
        {
            return marketshare_in_subject(year, uni, subject, -1,-1);
        }

        public double marketshare_in_subject(int year, string uni, string subject, int mk, int age)
        {
            double share = 0.0;

            if (!timestudclass.validyear(year))
                return share;
            if (!unidict.ContainsKey(uni))
                return share;
            if (!subjectdict.ContainsKey(subject))
                return share;
            if (!String.IsNullOrEmpty(unidict[uni].merged_with))
                return marketshare_in_subject(year, unidict[uni].merged_with, subject);

            int total = 0;
            if (mk >= 0)
            {
                if (age >= 0)
                    total = subjectdict[subject].registered.getstudset(year).stud[mk, age];
                else
                    total = subjectdict[subject].registered.getstudset(year).bygender(mk);
            }
            else if (age >= 0)
            {
                total = subjectdict[subject].registered.getstudset(year).byage(age);
            }
            else
                total = subjectdict[subject].registered.getstudset(year).total();
            double touni = getsubjreg(subject, uni, year, mk, age);
            share = touni / total;

            return share;
        }

        public int getgeobeg(string kommun, string uni, int year)
        {
            return getgeobeg(kommun, uni, year, -1, -1);
        }

        public int getgeobeg(string kommun, string uni, int year, int mk, int age)
        {
            int touni = 0;
            if (!timestudclass.validyear(year))
                return touni;
            if (!unidict.ContainsKey(uni))
                return touni;
            if (!kommundict.ContainsKey(kommun))
                return touni;
            if (mk > 1)
                return touni;
            if (age > 2)
                return touni;

            int iuni = unidict[uni].number;
            int ikommun = kommundict[kommun].number;
            if (geobeg[ikommun, iuni] != null)
            {
                if (( mk < 0 ) &&( age < 0))
                    touni = geobeg[ikommun, iuni].getstudset(year).total();
                else
                {
                    if (mk < 0)
                        touni = geobeg[ikommun, iuni].getstudset(year).byage(age);
                    else if (age < 0 )
                        touni = geobeg[ikommun, iuni].getstudset(year).bygender(mk);
                    else
                        touni = geobeg[ikommun, iuni].getstudset(year).stud[mk,age];

                }
            }

            return touni;
        }

        public int getsubjreg(string subject, string uni, int year)
        {
            return getsubjreg(subject, uni, year, -1, -1);
        }

        public int getsubjreg(string subject, string uni, int year, int mk, int age)
        {
            int touni = 0;
            if (!timestudclass.validyear(year))
                return touni;
            if (!unidict.ContainsKey(uni))
                return touni;
            if (!subjectdict.ContainsKey(subject))
                return touni;
            if (mk > 1)
                return touni;
            if (age > 2)
                return touni;

            int iuni = unidict[uni].number;
            int isubject = subjectdict[subject].number;
            if (subjreg[isubject, iuni] != null)
            {
                if ((mk < 0) && (age < 0))
                    touni = subjreg[isubject, iuni].getstudset(year).total();
                else
                {
                    if (mk < 0)
                        touni = subjreg[isubject, iuni].getstudset(year).byage(age);
                    else if (age < 0)
                        touni = subjreg[isubject, iuni].getstudset(year).bygender(mk);
                    else
                        touni = subjreg[isubject, iuni].getstudset(year).stud[mk, age];

                }

            }
            return touni;
        }

        public double getsubjhst(string subject, string uni, int year)
        {
            return getsubjhst(subject, uni, year, -1, -1);
        }

        public double getsubjhst(string subject, string uni, int year, int mk, int age)
        {
            double touni = 0;
            if (!timestudclass.validyear(year))
                return touni;
            if (!unidict.ContainsKey(uni))
                return touni;
            if (!subjectdict.ContainsKey(subject))
                return touni;
            if (mk > 1)
                return touni;
            if (age > 2)
                return touni;

            int iuni = unidict[uni].number;
            int isubject = subjectdict[subject].number;
            if (subjreg[isubject, iuni] != null)
            {
                if ((mk < 0) && (age < 0))
                    touni = subjreg[isubject, iuni].getstudset(year).total_hst();
                else
                {
                    if (mk < 0)
                        touni = subjreg[isubject, iuni].getstudset(year).byage_hst(age);
                    else if (age < 0)
                        touni = subjreg[isubject, iuni].getstudset(year).bygender_hst(mk);
                    else
                        touni = subjreg[isubject, iuni].getstudset(year).hst[mk, age];

                }

            }
            return touni;
        }

        public double getsubjhpr(string subject, string uni, int year)
        {
            return getsubjhpr(subject, uni, year, -1, -1);
        }

        public double getsubjhpr(string subject, string uni, int year, int mk, int age)
        {
            double touni = 0;
            if (!timestudclass.validyear(year))
                return touni;
            if (!unidict.ContainsKey(uni))
                return touni;
            if (!subjectdict.ContainsKey(subject))
                return touni;
            if (mk > 1)
                return touni;
            if (age > 2)
                return touni;

            int iuni = unidict[uni].number;
            int isubject = subjectdict[subject].number;
            if (subjreg[isubject, iuni] != null)
            {
                if ((mk < 0) && (age < 0))
                    touni = subjreg[isubject, iuni].getstudset(year).total_hpr();
                else
                {
                    if (mk < 0)
                        touni = subjreg[isubject, iuni].getstudset(year).byage_hpr(age);
                    else if (age < 0)
                        touni = subjreg[isubject, iuni].getstudset(year).bygender_hpr(mk);
                    else
                        touni = subjreg[isubject, iuni].getstudset(year).hpr[mk, age];

                }

            }
            return touni;
        }

        public string headerline(bool mk, bool age)
        {
            string s = "";
            if (CBmarketshare.Checked)
                s += "andel tot";
            if (CBmarketshare.Checked && CBabsolute.Checked)
                s += "\t";
            if (CBabsolute.Checked)
                s += "antal reg tot";
            if (CB_reg.Checked)
            {
                if (mk)
                {
                    s += "\t";
                    if (CBmarketshare.Checked)
                    {
                        s += "andel bland kvinnor" + "\t";
                        s += "andel bland män";
                    }
                    if (CBmarketshare.Checked && CBabsolute.Checked)
                        s += "\t";
                    if (CBabsolute.Checked)
                    {
                        s += "antal kvinnor" + "\t";
                        s += "antal män";
                    }
                }
                if (age)
                {
                    s += "\t";
                    if (CBmarketshare.Checked)
                    {
                        s += "andel bland -24 år" + "\t";
                        s += "andel bland 25-34" + "\t";
                        s += "andel bland 35+";
                    }
                    if (CBmarketshare.Checked && CBabsolute.Checked)
                        s += "\t";
                    if (CBabsolute.Checked)
                    {
                        s += "antal -24 år" + "\t";
                        s += "antal 25-34" + "\t";
                        s += "antal 35+";
                    }
                }
            }
            if (CB_hst.Checked)
                s += "\tHST totalt";
            if (CB_hpr.Checked)
                s += "\tHPR totalt";
            if (CB_hst.Checked || CB_hpr.Checked)
            {
                if (mk)
                {
                    s += "\t";
                    if (CB_hst.Checked)
                    {
                        s += "HST kvinnor" + "\t";
                        s += "HST män";
                    }
                    if (CB_hst.Checked && CB_hpr.Checked)
                        s += "\t";
                    if (CB_hpr.Checked)
                    {
                        s += "HPR kvinnor" + "\t";
                        s += "HPR män";
                    }
                }
                if (age)
                {
                    s += "\t";
                    if (CB_hst.Checked)
                    {
                        s += "HST -24 år" + "\t";
                        s += "HST 25-34" + "\t";
                        s += "HST 35+";
                    }
                    if (CB_hst.Checked && CB_hpr.Checked)
                        s += "\t";
                    if (CB_hpr.Checked)
                    {
                        s += "HPR -24 år" + "\t";
                        s += "HPR 25-34" + "\t";
                        s += "HPR 35+";
                    }
                }
            }
            return s;
        }

        public void alluni_in_kommun(int year, string kommun)
        {
            alluni_in_kommun(year, kommun, false, false);
        }

        public void alluni_in_kommun(int year, string kommun, bool mk, bool age)
        {
            double sharesum = 0.0;
            double shareresidue = 0.0;
            int numbersum = 0;
            int numberresidue = 0;
            memo(kommun + " " + year.ToString());
            memo("Lärosäte\t" + headerline(mk, age));
            foreach (string uni in unidict.Keys)
            {
                if (!String.IsNullOrEmpty(unidict[uni].merged_with))
                    continue;
                int studnumber = getgeobeg(kommun, uni, year);
                int[] studmk = new int[2] { getgeobeg(kommun, uni, year, 0, -1), getgeobeg(kommun, uni, year, 1, -1) };
                int[] studage = new int[3] { getgeobeg(kommun, uni, year, -1, 0), getgeobeg(kommun, uni, year, -1, 1), getgeobeg(kommun, uni, year, -1, 2) };
                numbersum += studnumber;
                double percent = 100 * marketshare_in_kommun(year, uni, kommun);
                double[] percentmk = new double[2]{100 * marketshare_in_kommun(year, uni, kommun,0,-1),100*marketshare_in_kommun(year, uni, kommun,1,-1)};
                double[] percentage = new double[3] { 100 * marketshare_in_kommun(year, uni, kommun, -1, 0), 100*marketshare_in_kommun(year, uni, kommun, -1, 1), 100*marketshare_in_kommun(year, uni, kommun, -1, 2) };
                sharesum += percent;
                if (percent > 0.1)
                {
                    string s = unidict[uni].shortform + "\t";
                    if (CBmarketshare.Checked)
                        s += percent.ToString("F2");
                    if (CBmarketshare.Checked && CBabsolute.Checked)
                        s += "\t";
                    if (CBabsolute.Checked)
                        s += studnumber.ToString();
                    if ( mk)
                    {
                        s += "\t";
                        if (CBmarketshare.Checked)
                        {
                            s += percentmk[0].ToString("F2") + "\t";
                            s += percentmk[1].ToString("F2");
                        }
                        if (CBmarketshare.Checked && CBabsolute.Checked)
                            s += "\t";
                        if (CBabsolute.Checked)
                        {
                            s += studmk[0].ToString() + "\t";
                            s += studmk[1].ToString();
                        }
                    }
                    if (age)
                    {
                        s += "\t";
                        if (CBmarketshare.Checked)
                        {
                            s += percentage[0].ToString("F2") + "\t";
                            s += percentage[1].ToString("F2") + "\t";
                            s += percentage[2].ToString("F2");
                        }
                        if (CBmarketshare.Checked && CBabsolute.Checked)
                            s += "\t";
                        if (CBabsolute.Checked)
                        {
                            s += studage[0].ToString() + "\t";
                            s += studage[1].ToString() + "\t";
                            s += studage[2].ToString();
                        }
                    }
                    memo(s);
                }
                else
                {
                    shareresidue += percent;
                    numberresidue += studnumber;
                }
            }
        }

        public void alluni_in_lan(int year, string lan)
        {
            alluni_in_lan(year, lan, false, false);
        }

        public void alluni_in_lan(int year, string lan,bool mk,bool age)
        {
            double sharesum = 0.0;
            double shareresidue = 0.0;
            int numbersum = 0;
            int numberresidue = 0;
            memo(lan + " " + year.ToString());
            memo("Lärosäte\t" + headerline(mk, age));
            foreach (string uni in unidict.Keys)
            {
                if (!String.IsNullOrEmpty(unidict[uni].merged_with))
                    continue;
                int studnumber = 0;
                int[] studmk = new int[2] { 0, 0 };
                int[] studage = new int[3] { 0,0,0 };
                foreach (string kommun in landict[lan].kommun)
                {
                    studnumber += getgeobeg(kommun, uni, year);
                    studmk[0] += getgeobeg(kommun, uni, year, 0, -1);
                    studmk[1] += getgeobeg(kommun, uni, year, 1, -1);
                    studage[0] += getgeobeg(kommun, uni, year, -1, 0);
                    studage[1] += getgeobeg(kommun, uni, year, -1, 1);
                    studage[2] += getgeobeg(kommun, uni, year, -1, 2);
                }

                numbersum += studnumber;
                double percent = 100 * marketshare_in_lan(year, uni, lan);
                double[] percentmk = new double[2] { 100 * marketshare_in_lan(year, uni, lan, 0, -1), 100 * marketshare_in_lan(year, uni, lan, 1, -1) };
                double[] percentage = new double[3] { 100 * marketshare_in_lan(year, uni, lan, -1, 0), 100 * marketshare_in_lan(year, uni, lan, -1, 1), 100 * marketshare_in_lan(year, uni, lan, -1, 2) };
                sharesum += percent;
                if (percent > 0.1)
                {
                    string s = unidict[uni].shortform + "\t";
                    if (CBmarketshare.Checked)
                        s += percent.ToString("F2");
                    if (CBmarketshare.Checked && CBabsolute.Checked)
                        s += "\t";
                    if (CBabsolute.Checked)
                        s += studnumber.ToString();
                    if (mk)
                    {
                        s += "\t";
                        if (CBmarketshare.Checked)
                        {
                            s += percentmk[0].ToString("F2") + "\t";
                            s += percentmk[1].ToString("F2");
                        }
                        if (CBmarketshare.Checked && CBabsolute.Checked)
                            s += "\t";
                        if (CBabsolute.Checked)
                        {
                            s += studmk[0].ToString() + "\t";
                            s += studmk[1].ToString();
                        }
                    }
                    if (age)
                    {
                        s += "\t";
                        if (CBmarketshare.Checked)
                        {
                            s += percentage[0].ToString("F2") + "\t";
                            s += percentage[1].ToString("F2") + "\t";
                            s += percentage[2].ToString("F2");
                        }
                        if (CBmarketshare.Checked && CBabsolute.Checked)
                            s += "\t";
                        if (CBabsolute.Checked)
                        {
                            s += studage[0].ToString() + "\t";
                            s += studage[1].ToString() + "\t";
                            s += studage[2].ToString();
                        }
                    }
                    memo(s);
                }
                else
                {
                    shareresidue += percent;
                    numberresidue += studnumber;
                }
            }

            if (CBabsolute.Checked)
            {
                memo("Övriga (antal)\t" + numberresidue.ToString());
                memo("Total (antal)\t" + numbersum.ToString());
            }
            if (CBmarketshare.Checked)
            {
                memo("Övriga (andel)\t" + shareresidue.ToString("F2"));
                memo("Total (andel)\t" + sharesum.ToString("F2"));
            }
        }

        public string makeline(int year, int studnumber, int[] studmk, int[] studage, double percent, double[] percentmk, double[] percentage, bool mk, bool age)
        {
            double[] hstmk = { 0, 0 };
            double[] hstage = { 0, 0, 0 };
            double[] hprmk = { 0, 0 };
            double[] hprage = { 0, 0, 0 };
            return makeline(year, studnumber, studmk, studage, percent, percentmk, percentage, mk, age, 0,0, hstmk, hstage, hprmk, hprage);
        }

        public string makeline(int year, int studnumber, int[] studmk, int[] studage, double percent, double[] percentmk, double[] percentage, bool mk, bool age, double hst, double hpr, double[] hstmk, double[] hstage, double[] hprmk, double[] hprage)
        {
            string s = year.ToString() + "\t";
            if (CBmarketshare.Checked)
                s += percent.ToString("F2");
            if (CBmarketshare.Checked && CBabsolute.Checked)
                s += "\t";
            if (CBabsolute.Checked)
                s += studnumber.ToString();
            if (CB_reg.Checked)
            {
                if (mk)
                {
                    s += "\t";
                    if (CBmarketshare.Checked)
                    {
                        s += percentmk[0].ToString("F2") + "\t";
                        s += percentmk[1].ToString("F2");
                    }
                    if (CBmarketshare.Checked && CBabsolute.Checked)
                        s += "\t";
                    if (CBabsolute.Checked)
                    {
                        s += studmk[0].ToString() + "\t";
                        s += studmk[1].ToString();
                    }
                }
                if (age)
                {
                    s += "\t";
                    if (CBmarketshare.Checked)
                    {
                        s += percentage[0].ToString("F2") + "\t";
                        s += percentage[1].ToString("F2") + "\t";
                        s += percentage[2].ToString("F2");
                    }
                    if (CBmarketshare.Checked && CBabsolute.Checked)
                        s += "\t";
                    if (CBabsolute.Checked)
                    {
                        s += studage[0].ToString() + "\t";
                        s += studage[1].ToString() + "\t";
                        s += studage[2].ToString();
                    }
                }
            }

            if (CB_hst.Checked)
                s += "\t"+hst.ToString("F2");
            if (CB_hpr.Checked)
                s += "\t"+hpr.ToString("F2");
            if (CB_hst.Checked || CB_hpr.Checked)
            {
                if (mk)
                {
                    s += "\t";
                    if (CB_hst.Checked)
                    {
                        s += hstmk[0].ToString("F2") + "\t";
                        s += hstmk[1].ToString("F2");
                    }
                    if (CB_hst.Checked && CB_hpr.Checked)
                        s += "\t";
                    if (CB_hpr.Checked)
                    {
                        s += hprmk[0].ToString("F2") + "\t";
                        s += hprmk[1].ToString("F2");
                    }
                }
                if (age)
                {
                    s += "\t";
                    if (CB_hst.Checked)
                    {
                        s += hstage[0].ToString("F2") + "\t";
                        s += hstage[1].ToString("F2") + "\t";
                        s += hstage[2].ToString("F2");
                    }
                    if (CB_hst.Checked && CB_hpr.Checked)
                        s += "\t";
                    if (CB_hpr.Checked)
                    {
                        s += hprage[0].ToString("F2") + "\t";
                        s += hprage[1].ToString("F2") + "\t";
                        s += hprage[2].ToString("F2");
                    }
                }
            }

            return s;

        }

        public void allyear_in_kommun(string uni, string kommun)
        {
            allyear_in_kommun(uni, kommun, false, false);
        }

        public void allyear_in_kommun(string uni, string kommun,bool mk,bool age)
        {
            bool studfound = false;
            for (int year = 1993; year < 2016; year++)
                if (getgeobeg(kommun, uni, year) > 0)
                    studfound = true;

            if (!studfound)
                return;

            memo(uni + " från " + kommun);
            memo("År\t" + headerline(mk, age));
            for (int year = 1993; year < 2016; year++)
            {
                int studnumber = getgeobeg(kommun, uni, year);
                int[] studmk = new int[2] { getgeobeg(kommun, uni, year, 0, -1), getgeobeg(kommun, uni, year, 1, -1) };
                int[] studage = new int[3] { getgeobeg(kommun, uni, year, -1, 0), getgeobeg(kommun, uni, year, -1, 1), getgeobeg(kommun, uni, year, -1, 2) };
                double percent = 100 * marketshare_in_kommun(year, uni, kommun);
                double[] percentmk = new double[2] { 100 * marketshare_in_kommun(year, uni, kommun, 0, -1), 100 * marketshare_in_kommun(year, uni, kommun, 1, -1) };
                double[] percentage = new double[3] { 100 * marketshare_in_kommun(year, uni, kommun, -1, 0), 100 * marketshare_in_kommun(year, uni, kommun, -1, 1), 100 * marketshare_in_kommun(year, uni, kommun, -1, 2) };


                if (percent > 0)
                {
                    memo(makeline(year, studnumber, studmk, studage, percent, percentmk, percentage,mk,age));
                }
            }
        }

        public void allyear_in_lan(string uni, string lan)
        {
            allyear_in_lan(uni, lan, false, false);
        }

        public void allyear_in_lan(string uni, string lan,bool mk,bool age)
        {
            bool studfound = false;
            for (int year = 1993; year < 2016; year++)
                foreach (string kommun in landict[lan].kommun)
                    if (getgeobeg(kommun, uni, year) > 0)
                        studfound = true;

            if (!studfound)
                return;

            memo(uni + " från " + lan);
            memo("År\t" + headerline(mk, age));
            for (int year = 1993; year < 2016; year++)
            {
                int studnumber = 0;
                int[] studmk = new int[2] { 0, 0 };
                int[] studage = new int[3] { 0, 0, 0 };
                foreach (string kommun in landict[lan].kommun)
                {
                    studnumber += getgeobeg(kommun, uni, year);
                    studmk[0] += getgeobeg(kommun, uni, year, 0, -1);
                    studmk[1] += getgeobeg(kommun, uni, year, 1, -1);
                    studage[0] += getgeobeg(kommun, uni, year, -1, 0);
                    studage[1] += getgeobeg(kommun, uni, year, -1, 1);
                    studage[2] += getgeobeg(kommun, uni, year, -1, 2);
                }

                double percent = 100 * marketshare_in_lan(year, uni, lan);
                double[] percentmk = new double[2] { 100 * marketshare_in_lan(year, uni, lan, 0, -1), 100 * marketshare_in_lan(year, uni, lan, 1, -1) };
                double[] percentage = new double[3] { 100 * marketshare_in_lan(year, uni, lan, -1, 0), 100 * marketshare_in_lan(year, uni, lan, -1, 1), 100 * marketshare_in_lan(year, uni, lan, -1, 2) };

                if (percent > 0)
                {
                    string s = year.ToString() + "\t";
                    if (CBmarketshare.Checked)
                        s += percent.ToString("F2");
                    if (CBmarketshare.Checked && CBabsolute.Checked)
                        s += "\t";
                    if (CBabsolute.Checked)
                        s += studnumber.ToString();
                    if (mk)
                    {
                        s += "\t";
                        if (CBmarketshare.Checked)
                        {
                            s += percentmk[0].ToString("F2") + "\t";
                            s += percentmk[1].ToString("F2");
                        }
                        if (CBmarketshare.Checked && CBabsolute.Checked)
                            s += "\t";
                        if (CBabsolute.Checked)
                        {
                            s += studmk[0].ToString() + "\t";
                            s += studmk[1].ToString();
                        }
                    }
                    if (age)
                    {
                        s += "\t";
                        if (CBmarketshare.Checked)
                        {
                            s += percentage[0].ToString("F2") + "\t";
                            s += percentage[1].ToString("F2") + "\t";
                            s += percentage[2].ToString("F2");
                        }
                        if (CBmarketshare.Checked && CBabsolute.Checked)
                            s += "\t";
                        if (CBabsolute.Checked)
                        {
                            s += studage[0].ToString() + "\t";
                            s += studage[1].ToString() + "\t";
                            s += studage[2].ToString();
                        }
                    }
                    memo(s);
                }
            }
        }

        public void allyear_in_sector(string uni, string sector)
        {
            bool studfound = false;
            for (int year = 2007; year < 2016; year++)
                foreach (string subject in sectordict[sector].subjects)
                    if (getsubjreg(subject, uni, year) > 0)
                        studfound = true;

            if (!studfound)
                return;

            memo(uni + ", andel av studenter inom " + sector + "\t" + unidict[uni].shortform);
            for (int year = 2007; year < 2016; year++)
            {
                double percent = 100 * marketshare_in_sector(year, uni, sector);
                int studnumber = 0;
                foreach (string subject in sectordict[sector].subjects)
                    studnumber += getsubjreg(subject, uni, year);
                //if (percent > 0)
                {
                    string s = year.ToString() + "\t";
                    if (CBmarketshare.Checked)
                        s += percent.ToString("F2");
                    if (CBmarketshare.Checked && CBabsolute.Checked)
                        s += "\t";
                    if (CBabsolute.Checked)
                        s += studnumber.ToString();
                    memo(s);
                }
            }
        }

        public void allyear_in_subjectgroup(string uni, string subjectgroup)
        {
            bool studfound = false;
            for (int year = 2007; year < 2016; year++)
                foreach (string subject in subjectgroupdict[subjectgroup].subjects)
                    if (getsubjreg(subject, uni, year) > 0)
                        studfound = true;

            if (!studfound)
                return;

            memo(uni + ", andel av studenter inom " + subjectgroup + "\t" + unidict[uni].shortform);
            for (int year = 2007; year < 2016; year++)
            {
                double percent = 100 * marketshare_in_subjectgroup(year, uni, subjectgroup);
                int studnumber = 0;
                foreach (string subject in subjectgroupdict[subjectgroup].subjects)
                    studnumber += getsubjreg(subject, uni, year);
                //if (percent > 0)
                {
                    string s = year.ToString() + "\t";
                    if (CBmarketshare.Checked)
                        s += percent.ToString("F2");
                    if (CBmarketshare.Checked && CBabsolute.Checked)
                        s += "\t";
                    if (CBabsolute.Checked)
                        s += studnumber.ToString();
                    memo(s);
                }
            }
        }

        public void allyear_in_subject(string uni, string subject,bool mk,bool age)
        {
            bool studfound = false;
            for (int year = 2007; year < 2016; year++)
                if (getsubjreg(subject, uni, year) > 0)
                    studfound = true;

            if (!studfound)
                return;

            memo(uni + ", andel av studenter inom " + subject+ "\t"+unidict[uni].shortform);
            memo("År\t" + headerline(mk, age));
            for (int year = 2007; year < 2016; year++)
            {
                int studnumber = getsubjreg(subject, uni, year);
                double hsttot = getsubjhst(subject, uni, year);
                double hprtot = getsubjhpr(subject, uni, year);
                int[] studmk = new int[2] { getsubjreg(subject, uni, year, 0, -1), getsubjreg(subject, uni, year, 1, -1) };
                int[] studage = new int[3] { getsubjreg(subject, uni, year, -1, 0), getsubjreg(subject, uni, year, -1, 1), getsubjreg(subject, uni, year, -1, 2) };
                double percent = 100 * marketshare_in_subject(year, uni, subject);
                double[] percentmk = new double[2] { 100 * marketshare_in_subject(year, uni, subject, 0, -1), 100 * marketshare_in_subject(year, uni, subject, 1, -1) };
                double[] percentage = new double[3] { 100 * marketshare_in_subject(year, uni, subject, -1, 0), 100 * marketshare_in_subject(year, uni, subject, -1, 1), 100 * marketshare_in_subject(year, uni, subject, -1, 2) };
                double[] hstmk = new double[2] { getsubjhst(subject, uni, year, 0, -1), getsubjhst(subject, uni, year, 1, -1) };
                double[] hstage = new double[3] { getsubjhst(subject, uni, year, -1, 0), getsubjhst(subject, uni, year, -1, 1), getsubjhst(subject, uni, year, -1, 2) };
                double[] hprmk = new double[2] { getsubjhpr(subject, uni, year, 0, -1), getsubjhpr(subject, uni, year, 1, -1) };
                double[] hprage = new double[3] { getsubjhpr(subject, uni, year, -1, 0), getsubjhpr(subject, uni, year, -1, 1), getsubjhpr(subject, uni, year, -1, 2) };
                //if (percent > 0)
                {
                    memo(makeline(year, studnumber, studmk, studage, percent, percentmk, percentage, mk, age, hsttot,hprtot, hstmk, hstage, hprmk, hprage));
                    //memo(makeline(year, studnumber, studmk, studage, percent, percentmk, percentage, mk, age));
                }
                //int studnumber = getgeobeg(kommun, uni, year);
                //int[] studmk = new int[2] { getgeobeg(kommun, uni, year, 0, -1), getgeobeg(kommun, uni, year, 1, -1) };
                //int[] studage = new int[3] { getgeobeg(kommun, uni, year, -1, 0), getgeobeg(kommun, uni, year, -1, 1), getgeobeg(kommun, uni, year, -1, 2) };
                //double percent = 100 * marketshare_in_kommun(year, uni, kommun);
                //double[] percentmk = new double[2] { 100 * marketshare_in_kommun(year, uni, kommun, 0, -1), 100 * marketshare_in_kommun(year, uni, kommun, 1, -1) };
                //double[] percentage = new double[3] { 100 * marketshare_in_kommun(year, uni, kommun, -1, 0), 100 * marketshare_in_kommun(year, uni, kommun, -1, 1), 100 * marketshare_in_kommun(year, uni, kommun, -1, 2) };


                //if (percent > 0)
                //{
                //    memo(makeline(year, studnumber, studmk, studage, percent, percentmk, percentage, mk, age));
                //}

            }
        }

        public void countuni_in_subject(string subject)
        {
            memo("Antal lärosäten med " + subject);
            
            for (int year = 2007; year < 2016; year++)
            {
                int nsuni = 0;
                foreach (string uni in unidict.Keys)
                {
                    if (getsubjreg(subject, uni, year) > 0)
                        nsuni++;
                }
                memo(year.ToString() + "\t" + nsuni.ToString());

            }

        }

        public void countuni_in_subjectgroup(string subjectgroup)
        {
            memo("Antal lärosäten med " + subjectgroup);

            for (int year = 2007; year < 2016; year++)
            {
                int nsuni = 0;
                foreach (string uni in unidict.Keys)
                {
                    bool found = false;
                    foreach (string subject in subjectgroupdict[subjectgroup].subjects)
                    {
                        if (getsubjreg(subject, uni, year) > 0)
                            found = true;
                    }
                    if (found)
                        nsuni++;
                }
                memo(year.ToString() + "\t" + nsuni.ToString());

            }

        }

        public void countuni_in_sector(string sector)
        {
            memo("Antal lärosäten med " + sector);

            for (int year = 2007; year < 2016; year++)
            {
                int nsuni = 0;
                foreach (string uni in unidict.Keys)
                {
                    bool found = false;
                    foreach (string subject in sectordict[sector].subjects)
                    {
                        if (getsubjreg(subject, uni, year) > 0)
                            found = true;
                    }
                    if (found)
                        nsuni++;
                }
                memo(year.ToString() + "\t" + nsuni.ToString());

            }

        }

        public class hbookclass
        {
            private SortedDictionary<string, int> shist = new SortedDictionary<string, int>();
            private SortedDictionary<int, int> ihist = new SortedDictionary<int, int>();
            private SortedDictionary<double, int> dhist = new SortedDictionary<double, int>();

            private const int MAXBINS = 202;
            private double[] binlimits = new double[MAXBINS];
            private double binmax = 100;
            private double binmin = 0;
            private double binwid = 0;
            private int nbins = MAXBINS - 2;

            public void Add(string key)
            {
                if (!shist.ContainsKey(key))
                    shist.Add(key, 1);
                else
                    shist[key]++;
            }

            public void Add(char key)
            {

                if (!shist.ContainsKey(key.ToString()))
                    shist.Add(key.ToString(), 1);
                else
                    shist[key.ToString()]++;
            }

            public void Add(int key)
            {
                if (!ihist.ContainsKey(key))
                    ihist.Add(key, 1);
                else
                    ihist[key]++;
            }

            private int valuetobin(double key)
            {
                int bin = 0;
                if (key > binmin)
                {
                    if (key > binmax)
                        bin = nbins + 1;
                    else
                    {
                        bin = (int)((key - binmin) / binwid) + 1;
                    }
                }
                return bin;
            }

            private double bintomin(int bin)
            {
                if (bin == 0)
                    return binmin;
                if (bin > nbins)
                    return binmax;
                return binmin + (bin - 1) * binwid;
            }

            private double bintomax(int bin)
            {
                if (bin == 0)
                    return binmin;
                if (bin > nbins)
                    return binmax;
                return binmin + bin * binwid;
            }

            public void Add(double key)
            {
                int bin = valuetobin(key);
                if (!ihist.ContainsKey(bin))
                    ihist.Add(bin, 1);
                else
                    ihist[bin]++;
            }

            public void Add(double key,int weight)
            {
                int bin = valuetobin(key);
                if (!ihist.ContainsKey(bin))
                    ihist.Add(bin, weight);
                else
                    ihist[bin] += weight;
            }

            public void SetBins(double min, double max, int nb)
            {
                if (nbins > MAXBINS - 2)
                {
                    Console.WriteLine("Too many bins. Max " + (MAXBINS - 2).ToString());
                    return;
                }
                else
                {
                    binmax = max;
                    binmin = min;
                    nbins = nb;
                    binwid = (max - min) / nbins;
                    binlimits[0] = binmin;
                    for (int i = 1; i <= nbins; i++)
                    {
                        binlimits[i] = binmin + i * binwid;
                    }

                    for (int i = 0; i <= nbins + 1; i++)
                        if (!ihist.ContainsKey(i))
                            ihist.Add(i, 0);
                }
            }

            public string PrintIHist()
            {
                string s = "";
                int total = 0;
                foreach (int key in ihist.Keys)
                {
                    s += key + ": " + ihist[key].ToString()+"\n";
                    total += ihist[key];
                }
                s += "----Total : " + total.ToString()+"\n";
                return s;
            }

            public string PrintDHist()
            {
                string s = "";
                int total = 0;
                foreach (int key in ihist.Keys)
                {
                    s += bintomin(key).ToString() + " -- " + bintomax(key).ToString() + "\t" + ihist[key].ToString() +"\n";
                    total += ihist[key];
                }
                s += "----Total : " + total.ToString() + "\n";
                return s;
            }

            public string PrintSHist()
            {
                string s = "";
                int total = 0;
                foreach (string key in shist.Keys)
                {
                    s += key + ": " + shist[key].ToString() + "\n";
                    total += shist[key];
                }
                s += "----Total : " + total.ToString() + "\n";
                return s;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            read_data();

        }

        private void button2_Click(object sender, EventArgs e)
        { 
            //Tidsserie stud från kommun
            memo("========================================");

            LBclass LB = checkLB();

            if (LB.unilist.Count == 0)
            {
                memo("Lärosäte ej valt!");
                return;
            }


            if (!( LB.totaluni  | LB.totalkommun | LB.totallan))
            { //grundalternativet, ett visst lärosäte i en viss kommun
                if ((LB.kommunlist.Count == 0) && (LB.lanlist.Count == 0))
                {
                    memo("Kommun eller län ej valt!");
                    return;
                }
                foreach (string uni in LB.unilist)
                {
                    if (LB.kommunlist.Count > 0)
                    {
                        foreach (string kommun in LB.kommunlist)
                        {
                            allyear_in_kommun(uni, kommun,LB.mk,LB.age);
                        }
                    }
                    if ( LB.lanlist.Count > 0)
                    {
                        foreach (string lan in LB.lanlist)
                        {
                            allyear_in_lan(uni, lan, LB.mk, LB.age);
                        }
                    }
                }
            }
            else if (LB.totalkommun || LB.totallan)
            {
                foreach (string uni in LB_uni.CheckedItems)
                {
                    string fromwhere = "";
                    if (LB.alllan || LB.totallan || LB.lanlist.Count == 0)
                        fromwhere = " från hela riket";
                    else
                    {
                        fromwhere = " från";
                        foreach (string s in LB.lanlist)
                            fromwhere += " " + s;
                    }

                    memo(uni + fromwhere);
                    for (int year = 1993; year < 2016; year++)
                    {

                        int sumuni = 0;
                        int sumall = 0;
                        //int[] studmk = new int[2] { 0, 0 };
                        //int[] studage = new int[3] { 0, 0, 0 };
                        //foreach (string kommun in landict[lan].kommun)
                        //{
                        //    studnumber += getgeobeg(kommun, uni, year);
                        //    studmk[0] += getgeobeg(kommun, uni, year, 0, -1);
                        //    studmk[1] += getgeobeg(kommun, uni, year, 1, -1);
                        //    studage[0] += getgeobeg(kommun, uni, year, -1, 0);
                        //    studage[1] += getgeobeg(kommun, uni, year, -1, 1);
                        //    studage[2] += getgeobeg(kommun, uni, year, -1, 2);
                        //}

                        //numbersum += studnumber;
                        //double percent = 100 * marketshare_in_lan(year, uni, lan);
                        //double[] percentmk = new double[2] { 100 * marketshare_in_lan(year, uni, lan, 0, -1), 100 * marketshare_in_lan(year, uni, lan, 1, -1) };
                        //double[] percentage = new double[3] { 100 * marketshare_in_lan(year, uni, lan, -1, 0), 100 * marketshare_in_lan(year, uni, lan, -1, 1), 100 * marketshare_in_lan(year, uni, lan, -1, 2) };
                        //sharesum += percent;

                        foreach (string kommun in kommundict.Keys)
                        {
                            if ((LB.lanlist.Count > 0) && (!LB.lanlist.Contains(kommundict[kommun].lan)))
                                continue;
                            sumuni += getgeobeg(kommun, uni, year);
                            sumall += kommundict[kommun].beginners.getstudset(year).total();

                        }
                        double percent = 100 * sumuni;
                        percent = percent/ sumall;
                        if (percent > 0)
                            memo(year + "\t" + percent.ToString("F2"));
                    }
                }

            }
            



        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Alla lärosäten stud från kommun
            memo("========================================");

            LBclass LB = checkLB();

            if (LB.yearlist.Count == 0)
            {
                memo("År ej valt!");
                return;
            }
            if ((LB.kommunlist.Count == 0) && (LB.lanlist.Count == 0))
            {
                memo("Kommun eller län ej valt!");
                return;
            }


            if (!(LB.totalyear | LB.totalkommun))
            { //grundalternativet, ett visst lärosäte i en viss kommun
                foreach (string year in LB.yearlist)
                {
                    foreach (string kommun in LB.kommunlist)
                    {
                        alluni_in_kommun(tryconvert(year), kommun,LB.mk,LB.age);
                    }
                    foreach (string lan in LB.lanlist)
                    {
                        alluni_in_lan(tryconvert(year), lan,LB.mk,LB.age);
                    }
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Tidsserie stud i ämne
            memo("========================================");

            LBclass LB = checkLB();

            if (LB.unilist.Count == 0)
            {
                memo("Lärosäte ej valt!");
                return;
            }
            if (LB.subjectlist.Count + LB.subjectgrouplist.Count + LB.sectorlist.Count == 0)
            {
                memo("Ämne/område ej valt!");
                return;
            }


            if (!(LB.totaluni | LB.totalsubject))
            { //grundalternativet, ett visst lärosäte i ett visst ämne
                foreach (string uni in LB.unilist)
                {
                    foreach (string sector in LB.sectorlist)
                    {
                        allyear_in_sector(uni, sector);
                    }
                    foreach (string subjectgroup in LB.subjectgrouplist)
                    {
                        allyear_in_subjectgroup(uni, subjectgroup);
                    }
                    foreach (string subject in LB.subjectlist)
                    {
                        allyear_in_subject(uni, subject,LB.mk,LB.age);
                    }
                }
            }
            else if (LB.totalsubject)
            {
                foreach (string uni in LB_uni.CheckedItems)
                {

                    memo(uni + " i alla ämnen");
                    for (int year = 2007; year < 2016; year++)
                    {

                        int sumuni = 0;
                        int sumall = 0;
                        foreach (string subject in subjectdict.Keys)
                        {
                            sumuni += getsubjreg(subject, uni, year);
                            sumall += subjectdict[subject].registered.getstudset(year).total();

                        }
                        double percent = 100 * sumuni / sumall;
                        if (percent > 0)
                            memo(year + "\t" + percent.ToString("F2"));
                    }
                }

            }
            else if (LB.totaluni)
            {
                foreach (string subject in LB.subjectlist)
                {
                    memo(subject + " vid alla lärosäten");
                    memo("År\t" + headerline(LB.mk, LB.age));
                    for (int year = 2007; year < 2016; year++)
                    {
                        int sumuni = 0;
                        double sumunihst = 0;
                        double sumunihpr = 0;
                        int[] studmk = new int[2] { 0,0};
                        int[] studage = new int[3] { 0, 0, 0 };
                        double percent = 100;
                        double[] percentmk = new double[2] { 100 , 100};
                        double[] percentage = new double[3] { 100, 100, 100};
                        double[] hstmk = new double[2] { 0,0};
                        double[] hstage = new double[3] { 0,0,0};
                        double[] hprmk = new double[2] { 0,0};
                        double[] hprage = new double[3] { 0, 0, 0 };
                

                        foreach (string uni in unidict.Keys)
                        {
                            if (!String.IsNullOrEmpty(unidict[uni].merged_with))
                                continue;
                            sumuni += getsubjreg(subject, uni, year);
                            sumunihst += getsubjhst(subject, uni, year);
                            sumunihpr += getsubjhpr(subject, uni, year);
                            studmk[0] += getsubjreg(subject, uni, year, 0, -1);
                            studmk[1] += getsubjreg(subject, uni, year, 1, -1);
                            studage[0] += getsubjreg(subject, uni, year, -1, 0);
                            studage[1] += getsubjreg(subject, uni, year, -1, 1);
                            studage[2] += getsubjreg(subject, uni, year, -1, 2);
                            hstmk[0] += getsubjhst(subject, uni, year, 0, -1);
                            hstmk[1] += getsubjhst(subject, uni, year, 1, -1);
                            hstage[0] += getsubjhst(subject, uni, year, -1, 0);
                            hstage[1] += getsubjhst(subject, uni, year, -1, 1);
                            hstage[2] += getsubjhst(subject, uni, year, -1, 2);
                            hprmk[0] += getsubjhpr(subject, uni, year, 0, -1);
                            hprmk[1] += getsubjhpr(subject, uni, year, 1, -1);
                            hprage[0] += getsubjhpr(subject, uni, year, -1, 0);
                            hprage[1] += getsubjhpr(subject, uni, year, -1, 1);
                            hprage[2] += getsubjhpr(subject, uni, year, -1, 2);

                        }
                        memo(makeline(year, sumuni, studmk, studage, percent, percentmk, percentage, LB.mk, LB.age, sumunihst, sumunihpr, hstmk, hstage, hprmk, hprage));
                    }
                }
            }

        }

        private void clearLB(CheckedListBox clbPrograms)
        {
            foreach (int index in clbPrograms.CheckedIndices)
            {
                clbPrograms.SetItemChecked(index, false);
            }
        }
        private void Clearbutton_Click(object sender, EventArgs e)
        {
            clearLB(LB_uni);
            clearLB(LB_lan);
            clearLB(LB_kommun);
            clearLB(LB_year);
            clearLB(LB_subject);
            clearLB(LB_subjectgroup);
            clearLB(LB_sector);

            LB_uni.ClearSelected();
            LB_lan.ClearSelected();
            LB_kommun.ClearSelected();
            LB_year.ClearSelected();
            LB_subject.ClearSelected();
            LB_subjectgroup.ClearSelected();
            LB_sector.ClearSelected();
        }

        private void button5_Click(object sender, EventArgs e)
        { //Antal lärosäten med ämne
            
            memo("========================================");

            LBclass LB = checkLB();

            if (LB.subjectlist.Count + LB.subjectgrouplist.Count + LB.sectorlist.Count == 0)
            {
                memo("Ämne/område ej valt!");
                return;
            }
                foreach (string sector in LB.sectorlist)
                {
                    countuni_in_sector(sector);
                }
                foreach (string subjectgroup in LB.subjectgrouplist)
                {
                    countuni_in_subjectgroup(subjectgroup);
                }
                foreach (string subject in LB.subjectlist)
                {
                    countuni_in_subject(subject);
                }

        }

        public static double get_distance_latlong(double fromlat, double fromlong, double tolat, double tolong) //returns distance in km
        {
            double kmdeg = 40000 / 360; //km per degree at equator
            double scale = Math.Cos(fromlat * 3.1416 / 180); //latitude-dependent longitude scale
            double dlat = (tolat - fromlat) * kmdeg;
            double dlong = (tolong - fromlong) * kmdeg * scale;

            double dist = Math.Sqrt(dlat * dlat + dlong * dlong);

            if (dist > 1000.0) //use great circle distance (Haversine formula)
            {
                double f1 = fromlat * Math.PI / 180.0; //convert to radians
                double f2 = tolat * Math.PI / 180.0;
                double l1 = fromlong * Math.PI / 180.0;
                double l2 = tolong * Math.PI / 180.0;
                double r = 6371.0; //Earth radius

                double underroot = Math.Pow(Math.Sin((f2 - f1) / 2), 2) + Math.Cos(f1) * Math.Cos(f2) * Math.Pow(Math.Sin((l2 - l1) / 2), 2);
                double root = Math.Sqrt(underroot);
                if (root > 1)
                    root = 1;
                dist = 2 * r * Math.Asin(root);

            }

            return dist;

        }

        private double dist_uni_kommun(string uni,string kommun)
        {
            double dist = 99999;
            foreach (string unikommun in unidict[uni].kommun)
            {
                if (unikommun == kommun)
                    return 0;
                else //find the nearest of the campus sites
                {
                    double dd = get_distance_latlong(kommundict[kommun].lat, kommundict[kommun].lon, kommundict[unikommun].lat, kommundict[unikommun].lon);
                    if (dd < dist)
                        dist = dd;
                }
            }
            return dist;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Avstånd student-lärosäte
            memo("========================================");

            LBclass LB = checkLB();
            hbookclass disthist = new hbookclass();
            disthist.SetBins(0.0, 2000.0, 40);

            if (LB.yearlist.Count == 0)
            {
                memo("År ej valt!");
                return;
            }
            //            double touni = getgeobeg(kommun,uni,year,mk,age);

            foreach (string year in LB.yearlist)
            {
                foreach (string kommun in kommundict.Keys)
                {
                    if (!String.IsNullOrEmpty(kommundict[kommun].merged_with))
                        continue;
                    foreach (string uni in unidict.Keys)
                    {
                        if (!String.IsNullOrEmpty(unidict[uni].merged_with))
                            continue;
                        if (( LB.unilist.Count > 0) && (!LB.unilist.Contains(uni)))
                            continue;

                        double dist = dist_uni_kommun(uni,kommun);
                        int studnumber = getgeobeg(kommun,uni,tryconvert(year));
                        disthist.Add(dist, studnumber);
                    }
                }
            }

            memo(disthist.PrintDHist());

        }

        private void CB24_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CB_reg_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
