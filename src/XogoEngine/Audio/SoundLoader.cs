using OpenTK.Audio.OpenAL;
using System;
using System.IO;
using XogoEngine.OpenAL.Adapters;

namespace XogoEngine.Audio
{
    public sealed class SoundLoader
    {
        private readonly IOpenAlAdapter adapter;

        public SoundLoader() : this(new OpenAlAdapter()) { }

        internal SoundLoader(IOpenAlAdapter adapter)
        {
            this.adapter = adapter;
        }

        public Sound Load(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }
            int bufferHandle = adapter.GenBuffer();
            int sourceHandle = adapter.GenSource();

            int channels, bits_per_sample, sample_rate;
            var sound_data = LoadWave(
                File.Open(filePath, FileMode.Open),
                out channels,
                out bits_per_sample,
                out sample_rate);
            var format = GetSoundFormat(channels, bits_per_sample);

            AL.BufferData(bufferHandle, format, sound_data, sound_data.Length, sample_rate);
            AL.Source(sourceHandle, ALSourcei.Buffer, bufferHandle);

            return new Sound(adapter, sourceHandle, bufferHandle);
        }

        /* Lifted from example code in OpenTK
         * See: https://github.com/opentk/opentk/blob/develop/Source/Examples/OpenAL/1.1/Playback.cs */
        private static byte[] LoadWave(Stream stream, out int channels, out int bits, out int rate)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            using (BinaryReader reader = new BinaryReader(stream))
            {
                // RIFF header
                string signature = new string(reader.ReadChars(4));
                if (signature != "RIFF")
                    throw new NotSupportedException("Specified stream is not a wave file.");

                /*int riff_chunck_size = */
                reader.ReadInt32();

                string format = new string(reader.ReadChars(4));
                if (format != "WAVE")
                    throw new NotSupportedException("Specified stream is not a wave file.");

                // WAVE header
                string format_signature = new string(reader.ReadChars(4));
                if (format_signature != "fmt ")
                    throw new NotSupportedException("Specified wave file is not supported.");

                /* int format_chunk_size = */
                reader.ReadInt32();
                /* int audio_format = */
                reader.ReadInt16();
                int num_channels = reader.ReadInt16();
                int sample_rate = reader.ReadInt32();
                /* int byte_rate = */
                reader.ReadInt32();
                /* int block_align = */
                reader.ReadInt16();
                int bits_per_sample = reader.ReadInt16();

                string data_signature = new string(reader.ReadChars(4));
                if (data_signature != "data")
                    throw new NotSupportedException("Specified wave file is not supported.");

                /* int data_chunk_size = */
                reader.ReadInt32();

                channels = num_channels;
                bits = bits_per_sample;
                rate = sample_rate;

                return reader.ReadBytes((int)reader.BaseStream.Length);
            }
        }

        private static ALFormat GetSoundFormat(int channels, int bits)
        {
            switch (channels)
            {
                case 1:
                    return bits == 8 ? ALFormat.Mono8 : ALFormat.Mono16;
                case 2:
                    return bits == 8 ? ALFormat.Stereo8 : ALFormat.Stereo16;
                default:
                    throw new NotSupportedException("The specified sound format is not supported.");
            }
        }
    }
}
