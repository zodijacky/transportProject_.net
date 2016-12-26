using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoovTn.Web.Models
{
    public class ViewModels
    {
        private  List<DropDownListModel> _states;

        
        public int SelectedStateId { get; set; }

        public IEnumerable<SelectListItem> getStatesItems()
        {
            DropDownListModel s1 = new DropDownListModel { Id = 0, Name = "Working" };
            DropDownListModel s2 = new DropDownListModel { Id = 1, Name = "Out of service" };
            _states.Add(s1);
            _states.Add(s2);   
            return new SelectList(_states, "Id", "Name"); 
        }
    }
}