using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Interfaces
{
    /// <summary>
    /// Interface to set the basic structure of a Page or Control in Web Forms.
    /// </summary>
    public interface IPageStructure
    {
        /// <summary>
        /// By implementing this method should initialize all variables, that is, instantiate them and load by data.
        /// </summary>
        void InitData();

        /// <summary>
        /// By implementing this method should initialize all ASP.NET controls (Dropdowns, textboxes, repeaters etc.) 
        /// on the page with the necessary data.
        /// </summary>
        void InitUI();

        /// <summary>
        /// By implementing this method should be put the data on the user interface
        /// </summary>
        void LoadUI(params object[] objects);

        /// <summary>
        /// By implementing this method it should get all data from UI
        /// </summary>
        /// <returns></returns>
        void GetFromUI(params object[] objects);
    }
}
