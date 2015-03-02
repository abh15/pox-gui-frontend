//A GUI frontend for commandline POX ,all components not supported yet
// https://github.com/abh15
using System;
using System.Windows.Forms;
using System.Globalization;
using System.Diagnostics;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;

public class guifrontend: Form

{
	public TextBox t,t2,t3,t4,t5;
	public RadioButton r1,r2,r3,r4,r5,r6,r7,r8,r9;
	public ComboBox cb;


	Process p=new Process();//instantiate new process p
	string sel="";	//file to run
	int s_case=0;	//select case
		

	void reset()  
	{						//func to clear all fields & visibility
		t2.Visible=false;
		t3.Visible=false;
		t4.Visible=false;
		t5.Visible=false;
		s_case=0;
		t2.Text="";
	    t3.Text="";
	  	t4.Text="";
	    t5.Text="";
	}
	

	public guifrontend()
	{
		Text="POXGUI";
		Size=new Size(700,630);
		

  		Label L1=new Label();
	    L1.Parent=this;
	    L1.Text="Path to directory containing pox.py:";
	    L1.Size=new Size(50,10);
		L1.Location=new Point(10,14);
	    L1.AutoSize=true;

	    Label L2=new Label();
	    L2.Parent=this;
	    L2.Text="Select log level:";
	    L2.Size=new Size(50,10);
		L2.Location=new Point(10,53);
	    L2.AutoSize=true;

		t=new TextBox();
	    t.Parent=this;
	    t.Location=new Point(210,10); 
	    t.Size=new Size(250,80);
	    t.Text=""; //
	    t.SelectAll();

		Button b=new Button();
	    b.Text="Run script";
	    b.Location=new Point(210,560);  
	    b.Parent=this;
	    b.Click+=new EventHandler(Onsubmit);

	    Button b2=new Button();
	    b2.Text="Terminate";
	    b2.Location=new Point(310,560);  
	    b2.Parent=this;
	    b2.Click+=new EventHandler(Onterm);

	    Button b4=new Button();
	    b4.Text="Exit";
	    b4.Location=new Point(410,560);  
	    b4.Parent=this;
	    b4.Click+=new EventHandler(Onexit);

		Button b3=new Button();
	    b3.Text="Browse";
	    b3.Location=new Point(470,10);  
	    b3.Parent=this;
	    b3.Click+=new EventHandler(Onbrowse);
	    b3.Visible=true;

	    RadioButton r1=new RadioButton();
	    r1.Text="Hub";
	    r1.Location=new Point(10,100);  
	    r1.Parent=this;
	    r1.CheckedChanged+=new EventHandler(Onr1);

	    RadioButton r2=new RadioButton();
	    r2.Text="L2 learning";
	    r2.Location=new Point(10,150);  
	    r2.Parent=this;
	    r2.CheckedChanged+=new EventHandler(Onr2);

	    RadioButton r3=new RadioButton();
	    r3.Text="L3 learning";
	    r3.Location=new Point(10,300);  
	    r3.Parent=this;
	    r3.CheckedChanged+=new EventHandler(Onr3);

	    RadioButton r7=new RadioButton();
	    r7.Text="L2 pairs";
	    r7.Location=new Point(10,250);  
	    r7.Parent=this;
	    r7.CheckedChanged+=new EventHandler(Onr7);

	    RadioButton r8=new RadioButton();
	    r8.Text="L2 multi";
	    r8.Location=new Point(10,200);  
	    r8.Parent=this;
	    r8.CheckedChanged+=new EventHandler(Onr8);

	    RadioButton r5=new RadioButton();
	    r5.Text="DHCP";
	    r5.Location=new Point(10,350);  
	    r5.Parent=this;
	    r5.CheckedChanged+=new EventHandler(Onr5);

	    RadioButton r6=new RadioButton();
	    r6.Text="IP loadbalancer";
	    r6.Location=new Point(10,400);  
	    r6.Parent=this;
	    r6.CheckedChanged+=new EventHandler(Onr6);

	    RadioButton r9=new RadioButton();
	    r9.Text="NAT";
	    r9.Location=new Point(10,450);  
	    r9.Parent=this;
	    r9.CheckedChanged+=new EventHandler(Onr9);


	    RadioButton r4=new RadioButton();
	    r4.Text="Custom script";
	    r4.Location=new Point(10,500);  
	    r4.Parent=this;
	    r4.CheckedChanged+=new EventHandler(Onr4);

		t2=new TextBox();
	    t2.Parent=this;
	    t2.Location=new Point(300,100); 
	    t2.Size=new Size(300,80);
	    t2.Text="";
	    t2.Visible=false;
	    t2.MouseDown+=new MouseEventHandler(clear2);
	   
	    t3=new TextBox();
	    t3.Parent=this;
	    t3.Location=new Point(300,180); 
	    t3.Size=new Size(300,80);
	    t3.Text="";
	    t3.Visible=false;
	    t3.MouseDown+=new MouseEventHandler(clear3);
	   

	    t4=new TextBox();
	    t4.Parent=this;
	    t4.Location=new Point(300,260); 
	    t4.Size=new Size(300,80);
	    t4.Text="";
	    t4.Visible=false;
	    t4.MouseDown+=new MouseEventHandler(clear4);
	   

	    t5=new TextBox();
	    t5.Parent=this;
	    t5.Location=new Point(300,340); 
	    t5.Size=new Size(300,80);
	    t5.Text="";
	    t5.Visible=false;
		t5.MouseDown+=new MouseEventHandler(clear5);
	   
	 	cb=new ComboBox();
	    cb.Parent=this;
	    cb.Text="DEBUG";
	    cb.Location=new Point (150,50);
	    cb.Items.AddRange(new object[]{"DEBUG ","INFO ","WARNING ","CRITICAL ","ERROR "});

	    Controls.Add(r1);
	    Controls.Add(r2);
	    Controls.Add(r3);
	    Controls.Add(r4);
	    Controls.Add(r5);
	    Controls.Add(r6);
	    Controls.Add(r7);
	    Controls.Add(r8);
	    Controls.Add(r9);
	    Controls.Add(cb);
	    
	    
	    if(Convert.ToString(Environment.OSVersion.Platform)!="Unix") //browse button only if non-windows OS
	    {
	    	b3.Visible=false;
	    	

	    }
	   
	}
	//=======GUI ends=======

void Onsubmit(object sender,EventArgs e)
	{                       
		
		sel=sel+this.t2.Text; //for scripts with no param
		
		switch (s_case) //case for scripts with params
   		{
	    case 1:
	        sel=" proto.dhcpd --network="+t2.Text+" --ip="+t3.Text+" --first="+t4.Text+" --last="+t5.Text+" --router=None --dns=None";
			break;
	    case 2:
	        sel=" misc.ip_loadbalancer --ip="+t4.Text+" --servers="+t5.Text;
			break;
	    case 3:
	        sel=t2.Text+" "+t3.Text;
	        break;
	    case 4:
	        sel=" misc.nat --dpid="+t2.Text+" --outside-port="+t3.Text;
	        break;    

	    default:
        	Console.WriteLine("");
        	break;

        }	


		  if(Convert.ToString(Environment.OSVersion.Platform)!="Unix")//to check os 
	    {
	    	
	    	this.t.Text=this.t.Text+"\\";	//in windows path is given as dir\file & default in UNIX is dir/file

	    }

		string par=this.t.Text+"pox.py log.level --"+this.cb.Text+sel+" openflow.keepalive";//store parameters common to all scripts along with log.level selection to a string
		

		string name= "python";	//pass filename to process p
		p.StartInfo.FileName=name;
		p.StartInfo.Arguments=par;	//pass the args to process p
        Console.WriteLine(name+par); 
        p.Start();					//start process p
        reset();
         

	}


//OnClicklisteners to clear textfields as soon as they're clicked

void clear2(object sender,EventArgs e){t2.Text="";} 
void clear3(object sender,EventArgs e){t3.Text="";}
void clear4(object sender,EventArgs e){t4.Text="";}
void clear5(object sender,EventArgs e){t5.Text="";}


void Onr1(object sender,EventArgs e)
{     										//hub
reset();                  
sel=" forwarding.hub";	

}


void Onr2(object sender,EventArgs e)
{     										//l2_learn
reset();                  
sel=" forwarding.l2_learning";

}


void Onr3(object sender,EventArgs e)
{               
reset();										//l3_learn
sel=" forwarding.l3_learning --fakeways=";
this.t2.Text="Enter fakeways for hosts (fakeway1,fakeway2,..)"; //set textfield to get params for l3 learning
this.t2.Visible=true;
}

void Onr4(object sender,EventArgs e) 
{       								//custom scripts
reset();	
this.t2.Text="Name of custom script without extension (in /ext folder)" ;               
this.t2.Visible=true;
sel="";
this.t3.Text="Parameters (optional)" ;               
this.t3.Visible=true;
s_case=3;
}


void Onr5(object sender,EventArgs e)
{       								//dhcp
reset();										

s_case=1;

this.t2.Text="Subnet to allocate addresses from (e.g 192.168.0.0/24)" ;               
this.t2.Visible=true;

this.t3.Text="IP to use for DHCP server";               
this.t3.Visible=true;


this.t4.Text="Start of range (e.g 10)";               
this.t4.Visible=true;

this.t5.Text="End of range (e.g 250)" ;               
this.t5.Visible=true;

}

void Onr6(object sender,EventArgs e)
{       								//iploadbalancer
reset();	

s_case=2;

this.t4.Text="IP from which traffic is to be redirected";               
this.t4.Visible=true;

this.t5.Text="IP of servers where the traffic will be redir (IP1,IP2)" ;               
this.t5.Visible=true;

}


void Onr7(object sender,EventArgs e) 
{												//l2 pairs     
reset();                  
sel=" forwarding.l2_pairs";	

}


void Onr8(object sender,EventArgs e)
{     												//l2 multi
reset();                  
sel=" openflow.discovery forwarding.l2_multi";	

}


void Onr9(object sender,EventArgs e)
{     
reset();             								//NAT     
s_case=4;
this.t2.Text="DPID to NAT-ize" ;               
this.t2.Visible=true;

this.t3.Text="port on which DPID connects upstream(e.g, eth0)" ;               
this.t3.Visible=true;
}


void Onbrowse (object sender,EventArgs e)
{  												//get path to pox folder only for UNIX systems
var openFileDialog1=new FolderBrowserDialog();
DialogResult result=openFileDialog1.ShowDialog();

if(result==DialogResult.OK)
{
	this.t.Text=openFileDialog1.SelectedPath+"/";
}


}


void Onterm(object sender,EventArgs e)
{  
this.p.Kill();					//terminate current script
reset();						//reset all fields
sel="" ;

Console.WriteLine("Terminated");

}



void Onexit(object sender,EventArgs e)
{  
Close();							//exit app
}

static public void Main()
	{
		Application.Run(new guifrontend());	//run app

	}


}