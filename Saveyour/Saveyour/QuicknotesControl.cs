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
using System.Diagnostics;

namespace Saveyour
{
    /***
     * 
     * QuicknotesControl is a data structure to keep track of all the Quicknotes modules created.
     * Most of the functions in this are often wrappers that iterate through a list and call the same function on each Quicknotes module.
     * For example, calling Show() in this class will cause the class to iterate through its list of Quicknotes modules and call Show() on those modules.     
     * 
     * ***/
    
    public partial class QuicknotesControl : Module
    {

        private double topPos;
        private double leftPos;

        //A list of all of the Quicknotes spawned by this module.
	    private List<Quicknotes> qnList;
        //This remembers whether all of the quicknotes children are currently visible or not.
	    private bool isVisible = true;

        //By default we always have at least one Quicknotes window when this is launched.
        public QuicknotesControl()
        {
            qnList = new List<Quicknotes>();
            topPos = 400;
            leftPos = 300;
            addQuickNote();            
        }

        //Adds a new Quicknotes window.
	    public Quicknotes addQuickNote(){
		    Quicknotes qn = new Quicknotes(this);
		    qnList.Add(qn);
            
            qn.Left = leftPos + 40;                            

		    qn.Show();
            return qn;
	    }

        //Removes a specific quicknotes window.
        public void removeQuickNote(Quicknotes note)
        {
            qnList.Remove(note);
            ((Window)note).Close();

        }
	

        /***** LOGIC FOR LIST OF QUICKNOTES INTERACTIONS *****/
        //Toggles the visibility of all the children Quicknotes
 	    public void ToggleVisibility(){
		    if (isVisible){
			    Hide();
		    }
		    else {
			    Show();
		    }
		        isVisible = !isVisible;
	    }                                                                                                                     

        public bool getVisibility()
        {
            return isVisible;
        }

        //Shows all the children Quicknotes
	    public void Show(){
		    foreach (Quicknotes qn in qnList){
			    qn.Show();
                qn.Activate();
                isVisible = true;
	        }
        }


        //Hides all the children Quicknotes
	    public void Hide(){
		    foreach (Quicknotes qn in qnList){
			    qn.Hide();
                isVisible = false;
		    }
        }

        /***** END OF LOGIC FOR LIST OF QUICKNOTES INTERACTIONS *****/


        /***** MODULE INTERFACE METHODS *****/
	
        //Returns the ID for this type of module
        public String moduleID()
        {
            return "QuicknotesControl";
        }

        //Currently unused, later may be used if we abstract the GUI away from WPF forms
        public Boolean update()
        {
            return false;
        }

        /*Aggregates the savedata from all of the children Quicknotes and returns it as one string.
         * Save format is DATA1\r\t\rDATA2\r\t\r etc.
         */
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

				saveString+="\r\t\r"+qn.save(); //Logic for separating saved text within the save files.
			}
		
		}
            return saveString;
        }

        //Splits the given savestring up into savestrings for multiple quicknotes modules and launches a quicknotes copy for each.
        public Boolean load(String data)
        {
            
		    String[] splitAt = { "\r\t\r" };
            	    //This array contains one quicknotes text contents at each index.
            	    String[] qnStrings = data.Split(splitAt, StringSplitOptions.None);
		
		    //For each saved quicknotes string create a new instance of quicknotes initialized with that string.
		    for(int i = 0; i < qnStrings.Length; i++){
                Quicknotes qn;
			    if (i == 0){
                    qn = qnList[0];
                    qn.load(qnStrings[0]);
                    qn.Show();
                }
                else
                {
                    Debug.WriteLine("Added new quicknotes...");
                    qn = new Quicknotes(this);
                    qn.load(qnStrings[i]);
                    qnList.Add(qn);
                    qn.Show();
                }
		
		    }

            if (!qnList.Any())
            {
                addQuickNote();
            }

            return true;
        }

        //Two modules are equal if they are of the same type.
        public Boolean Equals(Module other)
        {
            return moduleID().Equals(other.moduleID());
        }

        /***** END OF MODULE INTERFACE METHODS *****/

    }
}
