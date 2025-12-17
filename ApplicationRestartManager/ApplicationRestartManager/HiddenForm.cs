
using System.Diagnostics;

namespace ApplicationRestartManager
{
    internal sealed partial class HiddenForm : Form
    {
        readonly List<string> workingProcessNameList = [];
        List<string> processesWithMainWindowHandles = [];
        List<Process> workingProcessList = [];
        List<Process> processList = [];

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
            processesWithMainWindowHandles = [.. workingProcessNameList];
            workingProcessNameList.Clear();
            processList.Clear();
            processList = [.. workingProcessList];
            workingProcessList.Clear();
            workingProcessList = [.. Process.GetProcesses()];
            foreach (Process process in workingProcessList)
            {
                try
                {
                    if (process.MainWindowHandle != IntPtr.Zero)
                    {
                        workingProcessNameList.Add(process.ProcessName);
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
                foreach (Process process in Process.GetProcesses())
                {
                    if (!uniqueProcessNames.Contains(process.ProcessName))
                    {
                        uniqueProcessNames.Add(process.ProcessName);
                    }
                }
                using FileStream removalListFile = new(Path.GetDirectoryName(Application.ExecutablePath) + "\\remove.txt", FileMode.Create, FileAccess.Write, FileShare.None);
                using StreamWriter removalListWriter = new(removalListFile);
                foreach (string processName in uniqueProcessNames)
                {
                    if (!processesWithMainWindowHandles.Contains(processName))
                    {
                        removalListWriter.WriteLine(processName);
                    }
                }
                removalListWriter.Close();
                removalListFile.Close();
                processesWithMainWindowHandles.Clear();
                uniqueProcessNames.Clear();
                workingProcessList.Clear();
                processList.Clear();
                workingProcessNameList.Clear();
                processesWithMainWindowHandles.Clear();
            }
            catch
            {
            }
        }
    }
}


