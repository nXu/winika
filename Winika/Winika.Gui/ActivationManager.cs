namespace Winika.Gui
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Win32;

    public class ActivationManager
    {
        public string GetMachineGuid()
        {
            const string location = @"SOFTWARE\Microsoft\Cryptography";
            const string name = "MachineGuid";

            using (var localMachineX64View =
                RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            {
                using (var rk = localMachineX64View.OpenSubKey(location))
                {
                    if (rk == null)
                        throw new KeyNotFoundException("0-DOESNT-WORK");

                    var machineGuid = rk.GetValue(name);
                    if (machineGuid == null)
                        throw new IndexOutOfRangeException("1-DOESNT-WORK");

                    return machineGuid.ToString().ToUpper();
                }
            }
        }
    }
}
