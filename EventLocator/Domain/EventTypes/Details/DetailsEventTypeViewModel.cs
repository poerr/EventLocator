using EventLocator.Common;
using EventLocator.Data;
using EventLocator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLocator.Domain.EventTypes.Details
{
    public class DetailsEventTypeViewModel : BasePageViewModel<EventType>
    {
        public DetailsEventTypeViewModel(EventType eventType)
        {
            SelectedEntity = eventType;
        }
        public override void DeleteCommandExecute()
        {
            base.DeleteCommandExecute();
            NavigateToPage("Index", "EventType", null);
        }
        public override void ListCommandExecute()
        {
            base.ListCommandExecute();
            NavigateToPage("Index", "EventType", null);
        }
        public override void AddCommandExecute()
        {
            base.AddCommandExecute();
            NavigateToPage("Add", "EventType", null);
        }
        public override void EditCommandExecute()
        {
            base.EditCommandExecute();
            NavigateToPage("Edit", "EventType", SelectedEntity);
        }
        public override void DeleteAfterOk(EventType item)
        {
            base.DeleteAfterOk(item);
            Repository.Instance.DeleteEventType(item.Id);
            LoadTableData();
        }
    }
}
