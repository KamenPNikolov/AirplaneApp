using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Console;
using Newtonsoft.Json;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        StreamWriter streamWrite;
        StreamReader streamRead;
        private List<Passengers> passList = new List<Passengers>();
      
        private List<Flight> fliList = new List<Flight>();
        private  Passengers paObj = new Passengers();
        private Flight FliObj = new Flight();
        
       

        public Form1()
        {
            InitializeComponent();
            dateTimePickerFlight.MinDate = DateTime.Now;
           
            DateTakeoff.MinDate = DateTime.Now;
            DateLanding.MinDate = DateTime.Now;
            

            //Loading from files
            //Flights
            using (streamRead = new StreamReader("Flights.txt" ))
            { 
               while(!streamRead.EndOfStream)
                {
                   FliObj.FlightID = streamRead.ReadLine();
                    FliObj.takeOffDestination = streamRead.ReadLine();
                    FliObj.landingDestination = streamRead.ReadLine();
                    FliObj.DateTakeoff =Convert.ToDateTime(streamRead.ReadLine());
                    FliObj.DateLanding = Convert.ToDateTime(streamRead.ReadLine());
                    FliObj.stopOver = streamRead.ReadLine();
                    FliObj.planeType = streamRead.ReadLine();
                    fliList.Add(FliObj);
                    FliObj = new Flight();
                }
            }
            //Passengers
            using (streamRead = new StreamReader("Passengers.txt"))
            {
                string probe;
                while(!streamRead.EndOfStream)
                {
                    paObj.firstName = streamRead.ReadLine();
                    paObj.lastName = streamRead.ReadLine();
                    paObj.way = streamRead.ReadLine();
                    paObj.dateOfFlight = Convert.ToDateTime(streamRead.ReadLine());
                    paObj.row = streamRead.ReadLine();
                    paObj.seatAl = streamRead.ReadLine();
                    probe = streamRead.ReadLine();
                    foreach(Flight a in fliList)
                    {  
                        if(probe==a.FlightID)
                        {
                            paObj.flightOfPassanger = a;
                        }
                    }
                    passList.Add(paObj);
                    paObj = new Passengers();

                }
            }
            paObj.way = "One-way";


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

      

        private void label1_Click(object sender, EventArgs e)
        {

        }
//
// Implementation of Passengers
//
        private void textBoxFIrstName_TextChanged(object sender, EventArgs e)
        {
            paObj.firstName = textBoxFIrstName.Text;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
           
           
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonOneWay.Checked == false)
            {
                paObj.way = "Two-way";
            }
        }


        private void textBoxLastName_TextChanged(object sender, EventArgs e)
        {
            paObj.lastName = textBoxLastName.Text;

        }

        private void comboBoxRow_SelectedIndexChanged(object sender, EventArgs e)
        {
            paObj.row = comboBoxRow.Text;

        }

        private void comboBoxSeats_SelectedIndexChanged(object sender, EventArgs e)
        {
            paObj.seatAl = comboBoxSeats.Text;
        }

        private void dateTimePickerFlight_ValueChanged(object sender, EventArgs e)
        {
            comboBoxFLight.Items.Clear();    
             foreach(Flight b in fliList)
             {
                 if(dateTimePickerFlight.Value.ToShortDateString() == b.DateTakeoff.ToShortDateString())
                 {
                    
                     comboBoxFLight.Items.Add( b.takeOffDestination + "-" + b.landingDestination);
                     
                 }
             }
             
            
            paObj.dateOfFlight = dateTimePickerFlight.Value;
            

        }

        private void comboBoxFLight_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach(Flight b in fliList)
            {
                if (( b.takeOffDestination + "-" + b.landingDestination )== comboBoxFLight.Text)
                {
                    paObj.flightOfPassanger = b;
                    break;
                }
            }

        }

        private void buttonCheck_Click(object sender, EventArgs e)
        {
            string a="";
            if (paObj.firstName =="" || paObj.lastName =="" || paObj.row==null || paObj.seatAl==null || paObj.flightOfPassanger==null  )
            {
                a += "There are fundamental information fields about the ticket that are missing" + "\n";
                label10.Visible = true;
            }
            else
            {
                label10.Visible = false;
            }
            foreach(Passengers b in passList)
            {
                if((b.flightOfPassanger.FlightID == paObj.flightOfPassanger.FlightID) && (b.row == paObj.row) && (b.seatAl==paObj.seatAl) )

                {
                    a += "This seat in this plane is taken, please change the desired seat and check again\n";
                }
            }
           
            if(a=="")
            {
                a = " No Problems encountered!";
            }
            MessageBox.Show(a, "Check", MessageBoxButtons.OK);
        }

        private void buttonBooking_Click(object sender, EventArgs e)
        {
            passList.Add(paObj);
            //to file
            using (streamWrite = new StreamWriter("Passengers.txt",true))
            {
                
                streamWrite.WriteLine(paObj.firstName);
                streamWrite.WriteLine(paObj.lastName);
                streamWrite.WriteLine(paObj.way);
                streamWrite.WriteLine(paObj.dateOfFlight.ToString());
                streamWrite.WriteLine(paObj.row);
                streamWrite.WriteLine(paObj.seatAl);
                streamWrite.WriteLine(paObj.flightOfPassanger.FlightID);
            }

                MessageBox.Show("Passenger Ticket booked! \n" + paObj.firstName + "\n" + paObj.lastName + "\n" + paObj.row + paObj.seatAl + "\n" + paObj.dateOfFlight.ToShortDateString() + "\n" + paObj.way + "\n" + paObj.flightOfPassanger.takeOffDestination + "-" + paObj.flightOfPassanger.landingDestination, "Booking", MessageBoxButtons.OK);
            textBoxFIrstName.Text = null;
            textBoxLastName.Text = null;
            comboBoxFLight.Items.Clear();
            comboBoxFLight.Text = null;
            dateTimePickerFlight.Value = DateTime.Now;
            radioButtonOneWay.Checked = true;
            radioButtonTwoWay.Checked = false;
            comboBoxRow.Text = null;
            comboBoxSeats.Text = null;
           paObj = new Passengers();
            paObj.way = "One-way";

        }
//
//Flight implementation
//
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            FliObj.FlightID = textBox3.Text;

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

            FliObj.takeOffDestination = textBox4.Text;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            FliObj.landingDestination = textBox5.Text;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            FliObj.stopOver = textBox6.Text;

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            FliObj.planeType = textBox7.Text;
        }



        private void button1_Click(object sender, EventArgs e)
        { if (FliObj.FlightID == "" || FliObj.takeOffDestination == "" || FliObj.landingDestination == "" || FliObj.planeType == "" || FliObj.DateLanding == null || FliObj.DateTakeoff == null || FliObj.stopOver == "")
            {
                label21.Visible = true;
            }
            else
            {

                fliList.Add(FliObj);
                //comboBoxFLight.Items.Add(FliObj.takeOffDestination + "-" + FliObj.landingDestination);

                // file
                using (streamWrite = new StreamWriter("Flights.txt", true))
                {
                   
                    streamWrite.WriteLine(FliObj.FlightID);
                    streamWrite.WriteLine(FliObj.takeOffDestination);
                    streamWrite.WriteLine(FliObj.landingDestination);
                    streamWrite.WriteLine(FliObj.DateTakeoff.ToString());
                    streamWrite.WriteLine(FliObj.DateLanding.ToString());
                    streamWrite.WriteLine(FliObj.stopOver);
                    streamWrite.WriteLine(FliObj.planeType);

                }
                MessageBox.Show("Flight Noted! \n" + FliObj.FlightID + "\n" + FliObj.takeOffDestination + "\n" + FliObj.landingDestination + "\n" + FliObj.DateTakeoff.ToShortDateString() + "\n" + FliObj.DateLanding.ToShortDateString() + "\n" + FliObj.stopOver + "\n" + FliObj.planeType, "Flight", MessageBoxButtons.OK);
                FliObj = new Flight();
                textBox3.Text = null;
                textBox4.Text = null;
                textBox5.Text = null;
                textBox6.Text = null;
                textBox7.Text = null;
                DateTakeoff.Value = DateTime.Now;
                DateLanding.Value = DateTime.Now;
                label21.Visible = false;
            }

        }

        private void DateTakeoff_ValueChanged(object sender, EventArgs e)
        {
           
            FliObj.DateTakeoff = DateTakeoff.Value;
            DateLanding.MinDate = DateTakeoff.Value;
        }

        private void DateLanding_ValueChanged(object sender, EventArgs e)
        {
           
           
            FliObj.DateLanding = DateLanding.Value;
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage2)
            {
                comboBox3.Items.Clear();
                foreach (Passengers a in passList)
                {
                    comboBox3.Items.Add(a.firstName + " " + a.lastName);
                }
            }
            if (tabControl1.SelectedTab == tabPage4)
            {
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                listBox3.Items.Clear();
                listBox4.Items.Clear();
                listBox5.Items.Clear();
                listBox6.Items.Clear();
               
                foreach (Flight a in fliList)
                {
                    listBox1.Items.Add(a.FlightID);
                    listBox2.Items.Add(a.DateTakeoff.ToShortDateString());
                    listBox3.Items.Add(a.DateLanding.ToShortDateString());
                    listBox4.Items.Add(a.takeOffDestination);
                    listBox5.Items.Add(a.landingDestination);
                    listBox6.Items.Add(a.planeType);
                    
                 }
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Passengers a in passList)
            {
                if (comboBox3.Text == a.firstName+ " "+ a.lastName)
                {
                    MessageBox.Show("Name: " + a.firstName + " " + a.lastName + "\n" + "Flight: " + a.flightOfPassanger.FlightID + " " + a.flightOfPassanger.takeOffDestination + "-" + a.flightOfPassanger.landingDestination + "\n" + "Date:  " + a.dateOfFlight.ToShortDateString() + "\n" + "Seat: " + a.row + " " + a.seatAl,"Passenger "+ a.firstName + " "+ a.lastName,MessageBoxButtons.OK);
                }

            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
