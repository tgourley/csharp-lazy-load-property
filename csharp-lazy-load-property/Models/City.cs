using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace csharp_lazy_load_property.Models
{
    public class City
    {
        public int Id { get; set; }
        
        private string _JsonData;
        private bool _JsonDataChanged = true;

        [Column(TypeName = "jsonb")]
        public string JsonData
        {
            get
            {
                return _JsonData;
            }
            set
            {
                _JsonData = value;
                _JsonDataChanged = true;
            }
        }

        public Dictionary<string, string> _JsonDataDictionary;

        [NotMapped]
        public Dictionary<string, string> JsonDataDictionary
        {
            get
            {
                if (_JsonDataDictionary == null || _JsonDataChanged)
                {
                    _JsonDataDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(_JsonData);
                    _JsonDataChanged = false;
                }

                return _JsonDataDictionary;
            }
        }
    }
}
