using Binder.Core.Models;
using Binder.Core.Models.Enum;
using Binder.Core.Pages;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Binder.Core.ViewModels
{
    public class MainWindowVM : ViewModelBase, INotifyPropertyChanged
    {
        new public event PropertyChangedEventHandler PropertyChanged;
        #region Variables
        private readonly MainWindow _window;
        private readonly RuleManager _ruleManager;
        private readonly ProxyManager _proxy;
        private ObservableCollection<Rule> _rules;
        private bool _showCreator;
        private RulePage _page;
        #endregion
        #region Properties
        public ObservableCollection<Rule> Rules { get=> _rules; set { _rules = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Rules")); } }
        public bool ShowCreator { get => _showCreator; set { _showCreator = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ShowCreator")); } }
        public RulePage CreatorPage { get=> _page; set { _page = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CreatorPage")); } }
        #endregion
        public MainWindowVM(MainWindow window)
        {
            this._window = window;
            CreatorPage = new RulePage();
            this._ruleManager = new RuleManager();
            Rules = new ObservableCollection<Rule>(_ruleManager.GetRules());
            _proxy = new ProxyManager();
        }

        #region Creator
        private string _name, _site, _content, _type = "Replace";
        private bool _contains, _isRunning;
        public string Name { get => _name; set { _name = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name")); } }
        public string Site { get => _site; set { _site = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Site")); } }
        public string Content { get => _content; set { _content = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Content")); } }
        public string Type { get => _type; set { _type = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Type")); } }
        public bool Contains { get => _contains; set { _contains = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Contains")); } }
        public bool IsRunning { get => _isRunning; set { _isRunning = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRunning")); } }

        private void Reset()
        {
            Name = string.Empty;
            Site = string.Empty;
            Content = string.Empty;
            Type = "Replace";
            Contains = false;
        }
        public ICommand Save => new DelegateCommand(() =>
        {
            ContentType type = ContentType.Replace;
            switch (Type)
            {
                case "Redirect":
                    type = ContentType.Redirect;
                    break;
                case "Download":
                    type = ContentType.Download;
                    break;
                case "Replace":
                    type = ContentType.Replace;
                    break;


            }
            _ruleManager.Save(new Rule() { Name = Name, Contains = Contains, Content = Content, Site = Site, Type = type });
            Rules.Clear();
            _ruleManager.GetRules().ForEach(x => Rules.Add(x));
            Reset();
            ShowCreator = false;
        });
        public ICommand Close => new DelegateCommand(() =>
        {
            Reset();
            ShowCreator = false;
        });
        #endregion
        public ICommand Start => new DelegateCommand(() =>
        {
            _proxy.Rules = Rules.Where(x=>x.Active = true).ToList();
            if (IsRunning == true)
                return;
            IsRunning = true;
            _proxy.Start();
        }); 
        public ICommand Stop => new DelegateCommand(() =>
        {
            IsRunning = false;
            _proxy.Stop();
        });
        public ICommand Creator => new DelegateCommand(() =>
        {
            if (ShowCreator)
                ShowCreator = false;
            else
                ShowCreator = true;
        });
        public ICommand ShowFolder => new DelegateCommand(() =>
        {
            if (!System.IO.Directory.Exists("Rules"))
                System.IO.Directory.CreateDirectory("Rules");
            Process.Start("Rules");
        });
        public ICommand Reload => new DelegateCommand(() =>
        {
            Rules.Clear();
            _ruleManager.GetRules().ForEach(x => Rules.Add(x));
        });
        public ICommand SelectAll => new DelegateCommand(() =>
        {
            var rules = Rules.ToList();
            Rules.Clear();
            for (int i = 0; i < rules.Count(); i++)
            {
                rules[i].Active = true;
                Rules.Add(rules[i]);
            }
        });
        public ICommand DeselectAll => new DelegateCommand(() =>
        {
            var rules = Rules.ToList();
            Rules.Clear();
            for (int i = 0; i < rules.Count(); i++)
            {
                rules[i].Active = false;
                Rules.Add(rules[i]);
            }
        });

        public ICommand ImportRule => new DelegateCommand(() =>
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                FileName = "Rule.json",
                Title = "Import Rule",
                Filter = "Rule JSON|*.json"
            };
            if (dialog.ShowDialog()== DialogResult.OK)
            {
                if (_ruleManager.TryDeserializeRule(System.IO.File.ReadAllText(dialog.FileName), out Rule rule))
                    _ruleManager.Save(rule);
                _rules.Clear();
                _ruleManager.GetRules().ForEach(x => Rules.Add(x));
            }
        });

    }
}
