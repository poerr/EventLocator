using EventLocator.Common;
using EventLocator.Data;
using EventLocator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLocator.Domain.Tags.Details
{
    public class DetailsTagViewModel : BasePageViewModel<Tag>
    {
        public DetailsTagViewModel(Tag tag)
        {
            SelectedEntity = tag;
        }

        public override void DeleteCommandExecute()
        {
            base.DeleteCommandExecute();
            NavigateToPage("Index", "Tag", null);
        }
        public override void ListCommandExecute()
        {
            base.ListCommandExecute();
            NavigateToPage("Index", "Tag", null);
        }
        public override void AddCommandExecute()
        {
            base.AddCommandExecute();
            NavigateToPage("Add", "Tag", null);
        }
        public override void EditCommandExecute()
        {
            base.EditCommandExecute();
            NavigateToPage("Edit", "Tag", SelectedEntity);
        }
        public override void DeleteAfterOk(Tag item)
        {
            base.DeleteAfterOk(item);
            Repository.Instance.DeleteTag(item.Id);
            LoadTableData();
        }
    }
}
