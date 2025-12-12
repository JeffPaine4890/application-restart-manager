
using System.Diagnostics;

namespace ApplicationRestartManager
{
    internal sealed partial class HiddenForm : Form
    {
        List<string> workingProcessList = [];
        List<string> processesWithMainWindowHandles = [];

        public HiddenForm()
        {
            InitializeComponent();
        }

        private void HiddenForm_Load(object sender, EventArgs e)
        {
            Hide();
        }
        private void MainLoopTimer_Tick(object sender, EventArgs e)
        {
            processesWithMainWindowHandles.Clear();
            processesWithMainWindowHandles = [.. workingProcessList];
            workingProcessList.Clear();
            foreach (Process process in Process.GetProcesses())
            {
                try
                {
                    if (process.MainWindowHandle != IntPtr.Zero)
                    {
                        workingProcessList.Add(process.ProcessName);
                    }
                }
                catch
                {
                }
                Thread.Sleep(1);
            }
        }

        private void HiddenForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                List<string> uniqueProcessNames = [];
                List<string> processesToKeep = [.. processesWithMainWindowHandles];
                foreach (Process process in Process.GetProcesses())
                {
                    if (!uniqueProcessNames.Contains(process.ProcessName))
                    {
                        uniqueProcessNames.Add(process.ProcessName);
                    }
                }
                using FileStream processesWithMainWindowHandlesFile = new(Path.GetDirectoryName(Application.ExecutablePath) + "\\working.txt", FileMode.Create, FileAccess.Write, FileShare.None);
                using StreamWriter processesWithMainWindowHandlesWriter = new(processesWithMainWindowHandlesFile);
                foreach (string processName in processesWithMainWindowHandles)
                {
                    processesWithMainWindowHandlesWriter.WriteLine(processName);
                }
                processesWithMainWindowHandlesWriter.Close();
                processesWithMainWindowHandlesFile.Close();
                using FileStream removalListFile = new(Path.GetDirectoryName(Application.ExecutablePath) + "\\remove.txt", FileMode.Create, FileAccess.Write, FileShare.None);
                using StreamWriter removalListWriter = new(removalListFile);
                foreach (string processName in uniqueProcessNames)
                {
                    if (!processesToKeep.Contains(processName))
                    {
                        removalListWriter.WriteLine(processName);
                    }
                }
                removalListWriter.Close();
                removalListFile.Close();
                processesToKeep.Clear();
                uniqueProcessNames.Clear();
            }
            catch
            {
            }
        }
    }
}
