
using System;
using Impinj.OctaneSdk;

namespace ImpinjConfigurator
{
	class Program
	{
		// Create an instance of the ImpinjReader class.
		static ImpinjReader reader = new ImpinjReader();
		static void Main(string[] args)
		{
			try
			{
				// Connect to the reader.
				// Change the ReaderHostname constant in SolutionConstants.cs 
				// to the IP address or hostname of your reader.
                Console.WriteLine ("*** Connecting to a reader at: {0} ...", SolutionConstants.ReaderHostname);
				reader.Connect(SolutionConstants.ReaderHostname);
                Console.WriteLine ("*** Connected to the reader.");

				// Get the default settings
				// and then modify the settings
				Settings settings = reader.QueryDefaultSettings();
				settings.Report.IncludeAntennaPortNumber = true;
				settings.Report.IncludePcBits = true;
				settings.Report.IncludePeakRssi = true;

				// Set the reader mode, search mode and session
				settings.ReaderMode = ReaderMode.AutoSetDenseReader;
				settings.SearchMode = SearchMode.DualTarget;
				settings.Session = 2;

				// Enable antenna #1. Disable all others.
				settings.Antennas.DisableAll();
				settings.Antennas.GetAntenna(1).IsEnabled = true;

				// Set the Transmit Power and 
				// Receive Sensitivity to the maximum.
				settings.Antennas.GetAntenna(1).MaxTransmitPower = true;
				settings.Antennas.GetAntenna(1).MaxRxSensitivity = true;

				// Apply the newly modified settings.
				reader.ApplySettings(settings);
                Console.Write("*** Settings applied to the reader.\n");

				// Create a tag operation sequence.
				// You can add multiple read, write, lock, kill and QT
				// operations to this sequence.
				TagOpSequence seq = new TagOpSequence();

				// Create a tag read operation.
				TagReadOp readOp = new TagReadOp();
				// Read from user memory
				readOp.MemoryBank = MemoryBank.User;
				// Read two (16-bit) words
				readOp.WordCount = 2;
				// Starting at word 0
				readOp.WordPointer = 0;

				// Add this tag read op to the tag operation sequence.
				seq.Ops.Add(readOp);

				// Specify a target tag based on the EPC.
				seq.TargetTag.MemoryBank = MemoryBank.Epc;
				seq.TargetTag.BitPointer = BitPointers.Epc;

				// Setting this to null will specify any tag.
				seq.TargetTag.Data = null;

				// Add the tag operation sequence to the reader.
				// The reader supports multiple sequences.
				reader.AddOpSequence(seq);
                Console.Write("*** OpSequence added to the reader.\n");

				// Disconnect from the reader.
				reader.Disconnect();
                Console.Write("*** Disconnected from the reader.\n");

			}
			catch (OctaneSdkException e)
			{
				// Handle Octane SDK errors.
				Console.WriteLine("Octane SDK exception: {0}", e.Message);
			}
			catch (Exception e)
			{
				// Handle other .NET errors.
				Console.WriteLine("Exception : {0}", e.Message);
			}
		}
	}
}
