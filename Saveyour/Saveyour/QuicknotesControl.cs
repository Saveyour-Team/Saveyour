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
        public QuicknotesControl()
        {
            qnList = new List<Quicknotes>();
        }


	protected void addQuickNote(){
		Quicknotes qn = new Quicknotes();
		qnList.Add(qn);
		qn.Show();
	}


	protected void removeQuickNote(Quicknote note){
		qnList.Remove(note);
		note.Dispose();
	

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
		String saveString "";
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
			Quicknotes qn = new Quicknotes();
			qn.load(qnStrings[i]);
			qnList.Add(qn);
		}

        }

        public Boolean Equals(Module other)
        {
            return moduleID().Equals(other.moduleID());
        }


        

    }
}
