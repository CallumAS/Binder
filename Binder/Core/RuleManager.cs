using Binder.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binder.Core
{
    public class RuleManager
    {
        public void Save(Rule rule)
        {
            if (!System.IO.Directory.Exists("Rules"))
                System.IO.Directory.CreateDirectory("Rules");
            string json = JsonConvert.SerializeObject(rule);
            if (System.IO.File.Exists($"Rules\\{rule.Name}.json"))
                System.IO.File.WriteAllText($"Rules\\{Guid.NewGuid().ToString()}.json", json);
            else
                System.IO.File.WriteAllText($"Rules\\{rule.Name}.json", json);
        }
        public bool TryDeserializeRule(string content, out Rule rule)
        {
            rule = null;
            try {
                rule = JsonConvert.DeserializeObject<Rule>(content);
                return true;
            } catch { }
            return false;
        }
        public List<Rule> GetRules()
        {
            if (!System.IO.Directory.Exists("Rules"))
                System.IO.Directory.CreateDirectory("Rules");
            List<Rule> rules = new List<Rule>();
            System.IO.Directory.GetFiles(Environment.CurrentDirectory + "\\Rules\\", "*.json").ToList().ForEach(x =>
            {
                if (TryDeserializeRule(System.IO.File.ReadAllText(x), out Rule rule))
                    rules.Add(rule);
            });
            return rules;
        }
    }
}
