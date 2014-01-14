using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ColorSpike;

namespace Intems.Illuminate.HardwareTester
{
    public partial class Form1 : Form
    {
        private const string IPAddrAndPort = @"^((?:2[0-5]{2}|1\d{2}|[1-9]\d|[1-9])\.(?:(?:2[0-5]{2}|1\d{2}|[1-9]\d|\d)\.){2}(?:2[0-5]{2}|1\d{2}|[1-9]\d|\d)):(\d|[1-9]\d|[1-9]\d{2,3}|[1-5]\d{4}|6[0-4]\d{3}|654\d{2}|655[0-2]\d|6553[0-5])$";
        private SerialPort _port;

        private bool _useUdpProtocol;

        private Color _startColor;
        private Color _stopColor;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _startColor = panelColor.BackColor;
            UpdateStartColor();
            _stopColor = panelColor1.BackColor;
            UpdateStopColor();

            string[] ports = SerialPort.GetPortNames();
            if(ports.Length == 0)
                ports = new[] { "COM1", "COM2", "COM3" };
            PopulatePorts(ports);

            var rates = new[]
                            {
                                new Pair<string, int>("9600", 9600), new Pair<string, int>("19200", 19200),
                                new Pair<string, int>("38400", 38400), new Pair<string, int>("57600", 57600),
                                new Pair<string, int>("115200", 115200)
                            };
            PopulateRates(rates);
        }

        private void PopulateRates(Pair<string, int>[] rates)
        {
            cmbRates.DataSource = rates;
            cmbRates.DisplayMember = "First";
            cmbRates.ValueMember = "Second";
            cmbRates.SelectedIndex = 0;
        }

        private void PopulatePorts(IEnumerable<string> ports)
        {
            foreach (var port in ports)
                cmbPortNames.Items.Add(port);
            if(cmbPortNames.Items.Count > 0)
                cmbPortNames.SelectedIndex = 0;
        }

        private void OnBtnOpen_Click(object sender, EventArgs e)
        {
            var selectedRate = (Pair<string, int>) cmbRates.SelectedItem;
            var selectedPort = cmbPortNames.SelectedItem;
            if(selectedRate == null || selectedPort == null) return;

            string portName = selectedPort.ToString();
            int baudRate = selectedRate.Second;
            try
            {
                SetSendingState();
                _port = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
                _port.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void OnPanelColor_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                var result = dlgColor.ShowDialog();
                if(result == DialogResult.OK)
                {
                    _startColor = dlgColor.Color;
                    panelColor.BackColor = _startColor;
                    UpdateStartColor();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void OnStartColor_Changed(object sender, EventArgs e)
        {
            try
            {
                byte R = !String.IsNullOrEmpty(txtRed.Text) ? Byte.Parse(txtRed.Text) : (byte)0x00;
                byte G = !String.IsNullOrEmpty(txtGreen.Text) ? Byte.Parse(txtGreen.Text) : (byte)0x00;
                byte B = !String.IsNullOrEmpty(txtBlue.Text) ?Byte.Parse(txtBlue.Text) : (byte)0x00;
                _startColor = Color.FromArgb(R, G, B);
                panelColor.BackColor = _startColor;
                UpdateStartColor();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void UpdateStartColor()
        {
            txtRed.TextChanged -= OnStartColor_Changed;
            txtBlue.TextChanged -= OnStartColor_Changed;
            txtGreen.TextChanged -= OnStartColor_Changed;

            txtRed.Text = _startColor.R.ToString();
            txtRedHex.Text = _startColor.R.ToString("X");
            txtGreen.Text = _startColor.G.ToString();
            txtGreenHex.Text = _startColor.G.ToString("X");
            txtBlue.Text = _startColor.B.ToString();
            txtBlueHex.Text = _startColor.B.ToString("X");

            txtRed.TextChanged += OnStartColor_Changed;
            txtBlue.TextChanged += OnStartColor_Changed;
            txtGreen.TextChanged += OnStartColor_Changed;
        }

        private void OnPanelColor1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                var result = dlgColor.ShowDialog();
                if (result == DialogResult.OK)
                {
                    _stopColor = dlgColor.Color;
                    panelColor1.BackColor = _stopColor;
                    UpdateStopColor();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void OnStopColor_Changed(object sender, EventArgs e)
        {
            try
            {
                byte R = !String.IsNullOrEmpty(txtRed1.Text) ? Byte.Parse(txtRed1.Text) : (byte)0x00;
                byte G = !String.IsNullOrEmpty(txtGreen1.Text) ? Byte.Parse(txtGreen1.Text) : (byte)0x00;
                byte B = !String.IsNullOrEmpty(txtBlue1.Text) ? Byte.Parse(txtBlue1.Text) : (byte)0x00;
                _stopColor = Color.FromArgb(R, G, B);
                panelColor1.BackColor = _stopColor;
                UpdateStopColor();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void UpdateStopColor()
        {
            txtRed1.TextChanged -= OnStopColor_Changed;
            txtGreen1.TextChanged -= OnStopColor_Changed;
            txtBlue1.TextChanged -= OnStopColor_Changed;

            txtRed1.Text = _stopColor.R.ToString();
            txtRed1Hex.Text = _stopColor.R.ToString("X");
            txtGreen1.Text = _stopColor.G.ToString();
            txtGreen1Hex.Text = _stopColor.G.ToString("X");
            txtBlue1.Text = _stopColor.B.ToString();
            txtBlue1Hex.Text = _stopColor.B.ToString("X");

            txtRed1.TextChanged += OnStopColor_Changed;
            txtGreen1.TextChanged += OnStopColor_Changed;
            txtBlue1.TextChanged += OnStopColor_Changed;

        }

        private void OnBtnClose_Click(object sender, EventArgs e)
        {
            if(_port != null && _port.IsOpen)
            {
                _port.Close();
                _port = null;
            }
            SetConfigState();
        }

        private void OnBtnSend_Click(object sender, EventArgs e)
        {
            var builder = new CommandBuilder(_startColor, _stopColor, txtTime.Text, txtFreq.Text);
            var param = builder.CreateCommonParams(_cmdEnum);
            var chnNum = Int32.Parse(txtChannel.Text);
            var pkg = new Package((byte)chnNum, (byte)_cmdEnum, param);
            Console.WriteLine(pkg);

            if (!_useUdpProtocol)
            {

                if (_port != null && _port.IsOpen)
                    _port.Write(pkg.ToArray(), 0, pkg.Length);
            }
            else
            {
                if (!String.IsNullOrEmpty(txtAddrAndPort.Text))
                {
                    var regex = new Regex(IPAddrAndPort);
                    if (regex.IsMatch(_addrAndPort))
                    {
                        var ep = CreateIPEndPoint();
                        var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                        socket.SendTo(pkg.ToArray(), ep);
                        return;
                    }
                }
                MessageBox.Show("Enter IPv4 addr and port in format: XXX.XXX.XXX.XXX:PORTNUM");
            }
        }

        private IPEndPoint CreateIPEndPoint()
        {
            var fullAddr = _addrAndPort.Split(':');
            var address = IPAddress.Parse(fullAddr[0]);
            var port = Int32.Parse(fullAddr[1]);
            var ep = new IPEndPoint(address, port);
            return ep;
        }

        private void SetSendingState()
        {
            //disable UDP configuration UI
            txtAddrAndPort.Enabled = false;

            //disable comport configuration UI
            cmbPortNames.Enabled = false;
            cmbRates.Enabled = false;
            btnOpen.Enabled = false;
            //enable package sending UI
            btnClose.Enabled = true;
            btnSend.Enabled = true;
        }

        private void SetConfigState()
        {
            //disable UDP configuration UI
            txtAddrAndPort.Enabled = true;

            //disable comport configuration UI
            cmbPortNames.Enabled = true;
            cmbRates.Enabled = true;
            btnOpen.Enabled = true;
            //disable package sending UI
            btnClose.Enabled = false;
            btnSend.Enabled = false;
        }

        private CmdEnum _cmdEnum = CmdEnum.SetColor;
        private void OnRadio_CheckedChanged(object sender, EventArgs e)
        {
            var radioBtn = sender as RadioButton;
            if(radioBtn != null && radioBtn.Checked)
            {
                int en = Int32.Parse(radioBtn.Tag.ToString());
                _cmdEnum = (CmdEnum) en;
            }
        }

        private string _addrAndPort;
        private void OnUdpCheckedChanged(object sender, EventArgs e)
        {
            _useUdpProtocol = chkUseUdp.Checked;
            if (_useUdpProtocol)
            {
                SetSendingState();
                _addrAndPort = txtAddrAndPort.Text;
            }
            else
            {
                SetConfigState();
                _addrAndPort = String.Empty;
            }
        }
    }
}
