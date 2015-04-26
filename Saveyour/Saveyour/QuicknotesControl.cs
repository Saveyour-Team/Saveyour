using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Saveyour
{
    
    public partial class QuicknotesControl : Module
    {

	private List<Quicknotes> qnList;
	private bool isVisible = true;

        public QuicknotesControl()
        {
            qnList = new List<Quicknotes>();
        }


	public void addQuickNote(){
		Quicknotes qn = new Quicknotes(this);
		qnList.Add(qn);
		qn.Show();
	}


	public void removeQuickNote(Quicknotes note){
		qnList.Remove(note);
        ((Window)note).Close();
	

	}


 	public void ToggleVisibility(){
		if (isVisible){
			Hide();
		}
		else{
			Show();
		}

		isVisible = !isVisible;

	}
	
	public void Show(){
		foreach (Quicknotes qn in qnList){
			qn.Show();
		
		}

	}

	public void Hide(){
		foreach (Quicknotes qn in qnList){
			qn.Hide();
		
		}

	}
	
        public String moduleID()
        {
            return "QuicknotesControl";
        }

        public Boolean update()
        {
            return false;
        }

        public String save()
        {
		String saveString = "";
		bool firstNote = true;
		foreach (Quicknotes qn in qnList){
			if (firstNote){
				saveString+=qn.save();
				firstNote = false;
			}
			else{

				saveString+="\r\t\t"+qn.save();
			}
		
		}
            return saveString;
        }

        public Boolean load(String data)
        {
            
		    String[] splitAt = { "\r\t\r" };
            	    //This array contains one quicknotes text contents at each index.
            	    String[] qnStrings = data.Split(splitAt, StringSplitOptions.None);
		
		    //For each saved quicknotes string create a new instance of quicknotes initialized with that string.
		    for(int i = 0; i < qnStrings.Length; i++){
			    Quicknotes qn = new Quicknotes(this);
			    qn.load(qnStrings[i]);
			    qnList.Add(qn);
		    }

            return true;
        }

        public Boolean Equals(Module other)
        {
            return moduleID().Equals(other.moduleID());
        }


        

    }
}
