﻿/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017-2021 Matt McManis
https://github.com/MattMcManis/Axiom
https://axiomui.github.io
mattmcmanis@outlook.com

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.If not, see <http://www.gnu.org/licenses/>. 
---------------------------------------------------------------------- */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Controls.Video.Codec
{
    public class AV1 : Controls.IVideoCodec
    {
        // ---------------------------------------------------------------------------
        // Codec
        // ---------------------------------------------------------------------------
        public ObservableCollection<ViewModel.Video.VideoCodec> codec { get; set; } = new ObservableCollection<ViewModel.Video.VideoCodec>()
        {
            new ViewModel.Video.VideoCodec() { Codec = "libaom-av1", Parameters = "-strict -2" }
        };


        // ---------------------------------------------------------------------------
        // Items Source
        // ---------------------------------------------------------------------------

        // -------------------------
        // Encode Speed
        // -------------------------
        public ObservableCollection<ViewModel.Video.VideoEncodeSpeed> encodeSpeed { get; set; } = new ObservableCollection<ViewModel.Video.VideoEncodeSpeed>()
        {
            new ViewModel.Video.VideoEncodeSpeed() { Name = "none",       Command = ""},
            new ViewModel.Video.VideoEncodeSpeed() { Name = "Placebo",    Command = "-cpu-used 0" },
            new ViewModel.Video.VideoEncodeSpeed() { Name = "Very Slow",  Command = "-cpu-used 0" },
            new ViewModel.Video.VideoEncodeSpeed() { Name = "Slower",     Command = "-cpu-used 0" },
            new ViewModel.Video.VideoEncodeSpeed() { Name = "Slow",       Command = "-cpu-used 0" },
            new ViewModel.Video.VideoEncodeSpeed() { Name = "Medium",     Command = "-cpu-used 1" },
            new ViewModel.Video.VideoEncodeSpeed() { Name = "Fast",       Command = "-cpu-used 2" },
            new ViewModel.Video.VideoEncodeSpeed() { Name = "Faster",     Command = "-cpu-used 3" },
            new ViewModel.Video.VideoEncodeSpeed() { Name = "Very Fast",  Command = "-cpu-used 4" },
            new ViewModel.Video.VideoEncodeSpeed() { Name = "Super Fast", Command = "-cpu-used 5" },
            new ViewModel.Video.VideoEncodeSpeed() { Name = "Ultra Fast", Command = "-cpu-used 6" }
        };

        // -------------------------
        // Pixel Format
        // -------------------------
        public ObservableCollection<string> pixelFormat { get; set; } = new ObservableCollection<string>()
        {
            "auto",
            "yuv420p",
            "yuv420p10le",
            "yuv420p12le",
            "yuv422p",
            "yuv422p10le",
            "yuv422p12le",
            "yuv444p",
            "yuv444p10le",
            "yuv444p12le"
        };

        // -------------------------
        // Quality
        // -------------------------
        public ObservableCollection<ViewModel.Video.VideoQuality> quality { get; set; } = new ObservableCollection<ViewModel.Video.VideoQuality>()
        {
            new ViewModel.Video.VideoQuality() { Name = "Auto",      CRF = "",   CBR_BitMode = "-b:v", CBR = "",      VBR_BitMode = "", VBR = "", MinRate = "", MaxRate = "", BufSize ="", NA = "3000K" },
            new ViewModel.Video.VideoQuality() { Name = "Ultra",     CRF = "16", CBR_BitMode = "-b:v", CBR = "5000K", VBR_BitMode = "", VBR = "", MinRate = "", MaxRate = "", BufSize ="" },
            new ViewModel.Video.VideoQuality() { Name = "High",      CRF = "20", CBR_BitMode = "-b:v", CBR = "2500K", VBR_BitMode = "", VBR = "", MinRate = "", MaxRate = "", BufSize ="" },
            new ViewModel.Video.VideoQuality() { Name = "Medium",    CRF = "25", CBR_BitMode = "-b:v", CBR = "1300K", VBR_BitMode = "", VBR = "", MinRate = "", MaxRate = "", BufSize ="" },
            new ViewModel.Video.VideoQuality() { Name = "Low",       CRF = "35", CBR_BitMode = "-b:v", CBR = "600K",  VBR_BitMode = "", VBR = "", MinRate = "", MaxRate = "", BufSize ="" },
            new ViewModel.Video.VideoQuality() { Name = "Sub",       CRF = "45", CBR_BitMode = "-b:v", CBR = "250K",  VBR_BitMode = "", VBR = "", MinRate = "", MaxRate = "", BufSize ="" },
            new ViewModel.Video.VideoQuality() { Name = "Custom",    CRF = "",   CBR_BitMode = "-b:v", CBR = "",      VBR_BitMode = "", VBR = "", MinRate = "", MaxRate = "", BufSize ="" }
        };

        // -------------------------
        // Pass
        // -------------------------
        public void EncodingPass()
        {
            // -------------------------
            // Quality
            // -------------------------
            switch (VM.VideoView.Video_Quality_SelectedItem)
            {
                // Auto
                case "Auto":
                    VM.VideoView.Video_Pass_Items = new ObservableCollection<string>()
                    {
                        "2 Pass"
                    };

                    VM.VideoView.Video_Pass_SelectedItem = "2 Pass";
                    VM.VideoView.Video_Pass_IsEnabled = false;
                    Controls.passUserSelected = false;
                    VM.VideoView.Video_CRF_IsEnabled = false;
                    break;

                // Custom
                case "Custom":
                    VM.VideoView.Video_Pass_Items = new ObservableCollection<string>()
                    {
                        "CRF",
                        "1 Pass",
                        "2 Pass"
                    };

                    VM.VideoView.Video_Pass_IsEnabled = true;
                    VM.VideoView.Video_CRF_IsEnabled = true;
                    break;

                // None
                case "None":
                    VM.VideoView.Video_Pass_Items = new ObservableCollection<string>()
                    {
                        "auto"
                    };

                    VM.VideoView.Video_Pass_IsEnabled = false;
                    VM.VideoView.Video_CRF_IsEnabled = false;
                    break;

                // Presets: Ultra, High, Medium, Low, Sub
                default:
                    VM.VideoView.Video_Pass_Items = new ObservableCollection<string>()
                    {
                        "CRF",
                        "1 Pass",
                        "2 Pass"
                    };

                    VM.VideoView.Video_Pass_IsEnabled = true;
                    VM.VideoView.Video_CRF_IsEnabled = false;

                    // Default to CRF
                    if (Controls.passUserSelected == false)
                    {
                        VM.VideoView.Video_Pass_SelectedItem = "CRF";
                        Controls.passUserSelected = true;
                    }
                    break;
            }

            // Clear TextBoxes
            if (VM.VideoView.Video_Quality_SelectedItem == "Auto" ||
                VM.VideoView.Video_Quality_SelectedItem == "Lossless" ||
                VM.VideoView.Video_Quality_SelectedItem == "Custom" ||
                VM.VideoView.Video_Quality_SelectedItem == "None"
                )
            {
                VM.VideoView.Video_CRF_Text = string.Empty;
                VM.VideoView.Video_BitRate_Text = string.Empty;
                VM.VideoView.Video_MinRate_Text = string.Empty;
                VM.VideoView.Video_MaxRate_Text = string.Empty;
                VM.VideoView.Video_BufSize_Text = string.Empty;
            }

        }

        // -------------------------
        // Optimize
        // -------------------------
        public ObservableCollection<ViewModel.Video.VideoOptimize> optimize { get; set; } = new ObservableCollection<ViewModel.Video.VideoOptimize>()
        {
            new ViewModel.Video.VideoOptimize() { Name = "None", Tune = "none", Profile = "none", Level = "none", Command = "" },
        };

        // -------------------------
        // Tune
        // -------------------------
        public ObservableCollection<string> tune { get; set; } = new ObservableCollection<string>()
        {
            "none"
        };

        // -------------------------
        // Profile
        // -------------------------
        public ObservableCollection<string> profile { get; set; } = new ObservableCollection<string>()
        {
            "none"
        };

        // -------------------------
        // Level
        // -------------------------
        public ObservableCollection<string> level { get; set; } = new ObservableCollection<string>()
        {
            "none"
        };


        // ---------------------------------------------------------------------------
        // Controls Behavior
        // ---------------------------------------------------------------------------

        // -------------------------
        // Selected Items
        // -------------------------
        public List<ViewModel.Video.Selected> controls_Selected { get; set; } = new List<ViewModel.Video.Selected>()
        {
            new ViewModel.Video.Selected()
            {
                PixelFormat_Lossless = "yuv444p10le",
                PixelFormat = "yuv420p"
            }
        };

        // -------------------------
        // Expanded
        // -------------------------
        public List<ViewModel.Video.Expanded> controls_Expanded { get; set; } = new List<ViewModel.Video.Expanded>()
        {
            new ViewModel.Video.Expanded() {  Optimize = false },
        };

        // -------------------------
        // Checked
        // -------------------------
        public List<ViewModel.Video.Checked> controls_Checked { get; set; } = new List<ViewModel.Video.Checked>()
        {
            new ViewModel.Video.Checked() {  VBR = false },
        };

        // -------------------------
        // Enabled
        // -------------------------
        public List<ViewModel.Video.Enabled> controls_Enabled { get; set; } = new List<ViewModel.Video.Enabled>()
        {
            new ViewModel.Video.Enabled()
            {
                Quality =   true,
                VBR =       false,
                Optimize =  false
            },
            
            // All other controls are set through Format.Controls.MediaTypeControls()
        };

    }
}
