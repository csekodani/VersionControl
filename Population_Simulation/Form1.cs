using Population_Simulation.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Population_Simulation
{
    public partial class Form1 : Form
    {
        List<Person> Population = new List<Person>();
        List<BirthProbability> BirthProbabilities = new List<BirthProbability>();
        List<DeathProbability> DeathProbabilities = new List<DeathProbability>();
        List<int> zaroevek = new List<int>();
        Random rng = new Random(1234);

        public List<Person> GetPopulation(string csvpath)
        {
            List<Person> population = new List<Person>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    population.Add(new Person()
                    {
                        BirthYear = int.Parse(line[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        NbrOfChildren = int.Parse(line[2])
                    });
                }
            }

            return population;
        }
        public List<DeathProbability> GetDeath(string csvpath)
        {
            List<DeathProbability> deaths = new List<DeathProbability>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    deaths.Add(new DeathProbability()
                    {
                        Age = int.Parse(line[1]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[0]),
                        Death_Probability = double.Parse(line[2])
                    });
                }
            }

            return deaths;
        }
        public List<BirthProbability> GetBirth(string csvpath)
        {
            List<BirthProbability> births = new List<BirthProbability>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    births.Add(new BirthProbability()
                    {
                        Age = int.Parse(line[0]),
                        NumOfChildren = int.Parse(line[1]),
                        Birth_Probability = double.Parse(line[2])
                    });
                }
            }

            return births;
        }
        public Form1()
        {
            InitializeComponent();
            startBtn.Text = "Start";
            zarEvLbl.Text = "Zaro Ev";
            
            for (int i = 2005; i <2025; i++)
            {
                zaroevek.Add(i);
            }
            comboBox1.DataSource = zaroevek;


        }
        public void Simulation()
        {
            Population = GetPopulation(@"C:\Windows\Temp\nép.csv");
            BirthProbabilities = GetBirth(@"C:\Windows\Temp\születés.csv");
            DeathProbabilities = GetDeath(@"C:\Windows\Temp\halál.csv");
            for (int year = 2005; year <= int.Parse(comboBox1.SelectedItem.ToString()); year++)
            {

                foreach (Person person in Population)
                {
                    SimStep(year, person); // de emiatt miert ad 0-t?
                }



                int nbrOfMales = (from x in Population
                                  where x.Gender == Gender.Male && x.IsAlive
                                  select x).Count();
                int nbrOfFemales = (from x in Population
                                    where x.Gender == Gender.Female && x.IsAlive
                                    select x).Count();
                Console.WriteLine(
                    string.Format("Év: {0} Fiúk: {1} Lányok: {2}", year, nbrOfMales, nbrOfFemales));
            }

        }

        private void SimStep(int year, Person person)
        {
            
            if (!person.IsAlive) return;

            byte age = (byte)(year - person.BirthYear);

            double pDeath = (from x in DeathProbabilities
                             where x.Gender == person.Gender && x.Age == age
                             select x.Death_Probability).FirstOrDefault();

            if (rng.NextDouble() <= pDeath)
                person.IsAlive = false;


            if (person.IsAlive && person.Gender == Gender.Female)
            {

                double pBirth = (from x in BirthProbabilities
                                 where x.Age == age
                                 select x.Birth_Probability).FirstOrDefault();

                if (rng.NextDouble() <= pBirth)
                {
                    Person újszülött = new Person();
                    újszülött.BirthYear = year;
                    újszülött.NbrOfChildren = 0;
                    újszülött.Gender = (Gender)(rng.Next(1, 3));
                    Population.Add(újszülött);
                }
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            Simulation();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
