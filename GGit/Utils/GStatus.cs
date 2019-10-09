using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibGit2Sharp;

namespace GGit.Utils
{
    public class GStatus
    {
        public string Path { get; set; }
        public string Status { get; set; }
        private int _status { get; set; }
        public string Stage {get; set; }
        public string Discard { get; set; }
        
        private string _workdir { get; set; }
        public GStatus(string workdir, string path, int status)
        {
            _workdir = workdir;
            Path = path;
            _status = status;
            Status = "Change";
            SetBtn();
        }

        private void SetBtn()
        {
            Stage = "Staged" == Status ? "-" : "+";
            Discard = "×";
        }

        public void StageFile()
        {
            try
            {
                using (var repo = new Repository(_workdir))
                {
                    Commands.Stage(repo, Path);
                }
                Status = "Staged";
                SetBtn();
            } catch (Exception err)
            {
                MessageBox.Show(string.Format(err.Message));
            }
        }

        public void UnStageFile()
        {
            try
            {
                // TO DO Unstage a single file. git cli does this like command below
                // git reset HEAD <file>
                using (var repo = new Repository(_workdir))
                {
                    Commands.Unstage(repo, Path);
                }
                Status = "Change";
                SetBtn();
            }
            catch (Exception err)
            {
                MessageBox.Show(string.Format(err.Message));
            }
        }
    }
}
