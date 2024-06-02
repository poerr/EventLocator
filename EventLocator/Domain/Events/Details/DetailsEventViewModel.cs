using EventLocator.Common;
using EventLocator.Data;
using EventLocator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLocator.Domain.Events.Details
{
    public class DetailsEventViewModel : BasePageViewModel<Event>
    {
        public DetailsEventViewModel(Event selectedEvent)
        {
            SelectedEntity = selectedEvent;
        }

        public override void DeleteCommandExecute()
        {
            base.DeleteCommandExecute();
            NavigateToPage("Index", "Event", null);
        }
        public override void ListCommandExecute()
        {
            base.ListCommandExecute();
            NavigateToPage("Index", "Event", null);
        }
        public override void AddCommandExecute()
        {
            base.AddCommandExecute();
            NavigateToPage("Add", "Event", null);
        }
        public override void EditCommandExecute()
        {
            base.EditCommandExecute();
            NavigateToPage("Edit", "Event", SelectedEntity);
        }
        public override void DeleteAfterOk(Event item)
        {
            base.DeleteAfterOk(item);
            Repository.Instance.DeleteEvent(item.Id);
            LoadTableData();
        }
    }
}
