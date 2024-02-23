using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using Whisper.net.Ggml;

namespace MauiAppUnir
{
    public class Options
    {
        [Option('t', "command", Required = false, HelpText = "Command to run (lang-detect, transcribe or translate)", Default = "transcribe")]
        public string Command { get; set; }

        [Option('f', "file", Required = false, HelpText = "File to process", Default = "kennedy.wav")]
        public string FileName { get; set; }

        [Option('l', "lang", Required = false, HelpText = "Language", Default = "auto")]
        public string Language { get; set; }

        [Option('m', "modelFile", Required = false, HelpText = "Model to use (filename", Default = "ggml-base.bin")]
        public string ModelName { get; set; }

        [Option('g', "ggml", Required = false, HelpText = "Ggml Model type to download (if not exists)", Default = GgmlType.Base)]
        public GgmlType ModelType { get; set; }
    }

}
