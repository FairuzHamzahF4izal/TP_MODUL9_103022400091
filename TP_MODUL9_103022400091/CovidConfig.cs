using System;
using System.IO;
using System.Text.Json;

namespace TP_MODUL9_103022400091
{
    class CovidConfig
    {
        public string satuan_suhu { get; set; }
        public int batas_hari_deman { get; set; }
        public string pesan_ditolak { get; set; }
        public string pesan_diterima { get; set; }

        public CovidConfig()
        {
            LoadConfig();
        }

        public void LoadConfig()
        {
            string path = "covid_config.json";

            if (File.Exists(path))
            {
                string jsonString = File.ReadAllText(path);

                var data = JsonSerializer.Deserialize<ConfigData>(jsonString);

                satuan_suhu = data.satuan_suhu;
                batas_hari_deman = data.batas_hari_deman;
                pesan_ditolak = data.pesan_ditolak;
                pesan_diterima = data.pesan_diterima;
            }
            else
            {
                setDefault();
                SaveConfig();
            }
        }

        public void setDefault()
        {
            satuan_suhu = "celcius";
            batas_hari_deman = 14;
            pesan_ditolak = "Anda tidak diperbolehkan masuk ke dalam gedung ini";
            pesan_diterima = "Anda dipersilahkan untuk masuk ke dalam gedung ini";
        }

        public void SaveConfig()
        {
            var data = new ConfigData
            {
                satuan_suhu = satuan_suhu,
                batas_hari_deman = batas_hari_deman,
                pesan_ditolak = pesan_ditolak,
                pesan_diterima = pesan_diterima
            };

            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("covid_config.json", json);
        }

        public void UbahSatuan()
        {
            if (satuan_suhu == "celcius")
                satuan_suhu = "fahrenheit";
            else
                satuan_suhu = "celcius";

            SaveConfig();
        }
    }

    class ConfigData
    {
        public string satuan_suhu { get; set; }
        public int batas_hari_deman { get; set; }
        public string pesan_ditolak { get; set; }
        public string pesan_diterima { get; set; }
    }
}