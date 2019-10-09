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
        public string Stage {get; set; }
        public string Discard { get; set; }
        private int _status { get; set; }
        private bool _staged { get; set; }

        private string _workdir { get; set; }
        public GStatus(string workdir, string path, int status, bool staged)
        {
            _workdir = workdir;
            Path = path;
            _status = status;
            _staged = staged;
            Status = _staged ? "Staged" : "Change";
            SetBtn();
        }

        private void SetBtn()
        {
            Stage = _staged ? "-" : "+";
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
                _staged = true;
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
                _staged = false;
                SetBtn();
            }
            catch (Exception err)
            {
                MessageBox.Show(string.Format(err.Message));
            }
        }
    }
}
