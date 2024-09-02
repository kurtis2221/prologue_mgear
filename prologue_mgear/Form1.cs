using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace prologue_mgear
{
    public partial class Form1 : Form
    {
        const string config_file = "prologue_mgear.ini";
        const string game_exe = "Prologue";

        const uint asm_start = 0x02000000;
        const uint asm_end = 0x03000000;

        const uint THDSTACK0 = 0x0019FF7C;
        const uint GEAR_PTR = THDSTACK0 - 0x0000016C;
        const uint GEAR_PTR_OFFS = 0x50;

        static byte[] gear_asm_instr = {
            0x81, 0xC3, 0x3C, 0x00, 0x00, 0x00, //add ebx,0000003C
            0x89, 0x03, //mov [ebx],eax
            0x81, 0xEC, 0x0C, 0x00, 0x00, 0x00, //sub esp,0000000C
            0xC7, 0x44, 0x24, 0x04, 0x00, 0x00, 0x00, 0x00, //mov [esp+04],00000000
            0xC7, 0x44, 0x24, 0x08, 0x00, 0x00, 0x80, 0x3F //mov [esp+08],3F800000 (1.00)
        };

        static byte[][] gear_orig_instr = {
            new byte[] { 0x89, 0x03 }, //mov [ebx],eax
            new byte[] { 0x89, 0x1E }, //mov [esi],ebx
            new byte[] { 0x89, 0x1E }, //mov [esi],ebx
            new byte[] { 0x89, 0x1E }, //mov [esi],ebx
        };
        static byte[][] gear_mod_instr = {
            new byte[] { 0x90, 0x90 }, //nop
            new byte[] { 0x90, 0x90 }, //nop
            new byte[] { 0x90, 0x90 }, //nop
            new byte[] { 0x90, 0x90 }, //nop
        };
        static uint[] gear_instr_offsets = { 0x0, 0x29A, 0x2F7, 0x283 };

        uint gear_asm_addr;

        MemoryEdit.Memory mem;
        KeyHook.GlobalKeyboardHook gkh;

        Process game;

        Button key_tmp;
        bool key_set = false;
        bool keys_modified = false;

        Keys shift_up = Keys.A;
        Keys shift_dn = Keys.Y;

        public Form1()
        {
            InitializeComponent();
            mem = new MemoryEdit.Memory();
            gkh = new KeyHook.GlobalKeyboardHook();
            gkh.KeyDown += gkh_KeyDown;
            gkh.Hook();
            try
            {
                string[] tmp = File.ReadAllLines(config_file);
                shift_up = (Keys)Enum.Parse(typeof(Keys), tmp[0]);
                shift_dn = (Keys)Enum.Parse(typeof(Keys), tmp[1]);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while loading {config_file}:\n{ex.Message}", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                keys_modified = true;
            }
            bt_shift_up.Text = shift_up.ToString();
            bt_shift_down.Text = shift_dn.ToString();
        }

        private void gkh_KeyDown(object sender, KeyEventArgs e)
        {
            if (!mem.IsFocused()) return;
            if (e.KeyCode == shift_up)
            {
                uint addr = (uint)mem.Read(GEAR_PTR) + GEAR_PTR_OFFS;
                int tmp = mem.Read(addr);
                if (tmp < 6)
                    mem.WriteBytes(addr, BitConverter.GetBytes(tmp + 1), 4);
            }
            else if (e.KeyCode == shift_dn)
            {
                uint addr = (uint)mem.Read(GEAR_PTR) + GEAR_PTR_OFFS;
                int tmp = mem.Read(addr);
                if (tmp > 0)
                    mem.WriteBytes(addr, BitConverter.GetBytes(tmp - 1), 4);
            }
        }

        private void bt_shift_up_Click(object sender, EventArgs e)
        {
            key_tmp = (Button)sender;
            key_tmp.Text = "Press a key";
            key_set = true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (key_set)
            {
                key_set = false;
                if (keyData == Keys.Escape)
                {
                    bt_shift_up.Text = shift_up.ToString();
                    bt_shift_down.Text = shift_dn.ToString();
                }
                else
                {
                    if (key_tmp == bt_shift_up) shift_up = keyData;
                    else shift_dn = keyData;
                    key_tmp.Text = keyData.ToString();
                    keys_modified = true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (keys_modified)
                    File.WriteAllText(config_file, shift_up.ToString() + "\n" + shift_dn.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while saving {config_file}:\n{ex.Message}", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                EnableGears();
            }
            catch { }
        }

        private void EnableGears()
        {
            if (game == null || game.HasExited) return;
            uint addr = gear_asm_addr;
            for (int i = 0; i < gear_instr_offsets.Length; i++)
            {
                mem.WriteBytes(addr + gear_instr_offsets[i], gear_orig_instr[i], gear_orig_instr[i].Length);
            }
        }

        private void DisableGears()
        {
            for (uint addr = asm_start; addr < asm_end; addr++)
            {
                byte[] tmp = mem.ReadBytes(addr, gear_asm_instr.Length);
                if (Enumerable.SequenceEqual(tmp, gear_asm_instr))
                {
                    addr += 6;
                    gear_asm_addr = addr;
                    for (int i = 0; i < gear_instr_offsets.Length; i++)
                    {
                        mem.WriteBytes(addr + gear_instr_offsets[i], gear_mod_instr[i], gear_mod_instr[i].Length);
                    }
                    lb_status.Text = "Hooked";
                    return;
                }
            }
        }

        private void ScanForGame()
        {
            try
            {
                Process[] procs = Process.GetProcessesByName(game_exe);
                if (procs.Length > 0)
                {
                    game = procs[0];
                    mem.Attach((uint)game.Id, MemoryEdit.Memory.ProcessAccessFlags.All);
                    mem.ReadBytes(0, gear_asm_instr.Length);
                    lb_status.Text = "Loaded";
                    Application.DoEvents();
                    DisableGears();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while hooking game:\n{ex.Message}", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tmr_scan_Tick(object sender, EventArgs e)
        {
            if (game == null || game.HasExited)
            {
                lb_status.Text = "Waiting";
                ScanForGame();
            }
        }
    }
}
