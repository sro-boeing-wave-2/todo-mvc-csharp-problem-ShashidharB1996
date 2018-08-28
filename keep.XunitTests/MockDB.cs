using keep.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace keep.XunitTests
{
    class MockDB
    {
        public async Task<Note> GetTestResultData()
        {
            var NewNote = new Note
            
            {
                ID = 1,
                Title = "First Note",
                PlainText = "Text in the first Note",
                PinnedStatus = true,

                Labels = new List<Label>
                {
                    new Label{LabelText="label 1 in second NOte"},
                    new Label{LabelText="label 2 in first Note"}
                },
                CheckList=new List<CheckListItem>
                {
                    new CheckListItem{CheckListText="checklist 1 in first Note"},
                    new CheckListItem {CheckListText="checklist 2 in first Note"}

                }
            
            };

            return await Task.FromResult(NewNote);
        }

        public async Task<List<Note>> GetTestResultListAsync()
        {
            var Notes = new List<Note>
            {new Note()
            {
                ID = 1,
                Title = "First Note",
                PlainText = "Text in the first Note",
                PinnedStatus = true,

                Labels = new List<Label>
                {
                    new Label{LabelText="label 1 in second NOte"},
                    new Label{LabelText="label 2 in first Note"}
                },
                CheckList=new List<CheckListItem>
                {
                    new CheckListItem{CheckListText="checklist 1 in first Note"},
                    new CheckListItem {CheckListText="checklist 2 in first Note"}

                }
            },
            new Note()
            {
                ID = 2,
                Title = "Second Note",
                PlainText = "Text in the second Note",
                PinnedStatus = true,

                Labels = new List<Label>
                {
                    new Label{LabelText="label 1 in second NOte"},
                    new Label{LabelText="label 2 in second NOte"}
                },
                CheckList=new List<CheckListItem>
                {
                    new CheckListItem{CheckListText="checklist 1 in second NOte"},
                    new CheckListItem {CheckListText="checklist 2 in second NOte"}

                }
            }
            };
            return await Task.FromResult(Notes);
        }
    }
}

