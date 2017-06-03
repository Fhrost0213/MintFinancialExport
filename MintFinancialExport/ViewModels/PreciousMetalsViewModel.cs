using MintFinancialExport.Core;
using MintFinancialExport.Core.Entities;
using MintFinancialExport.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MintFinancialExport.ViewModels
{
    public class PreciousMetalsViewModel : BaseViewModel
    {
        private decimal? _goldSpotPrice { get; set; }
        private decimal? _silverSpotPrice { get; set; }
        private decimal? _platinumSpotPrice { get; set; }
        private decimal? _palladiumSpotPrice { get; set; }
        private int? _goldOunces { get; set; }
        private int? _silverOunces { get; set; }
        private int? _platinumOunces { get; set; }
        private int? _palladiumOunces { get; set; }
        private decimal? _goldTotal { get; set; }
        private decimal? _silverTotal { get; set; }
        private decimal? _platinumTotal { get; set; }
        private decimal? _palladiumTotal { get; set; }
        private int? _runId { get; set; }


        public decimal? GoldSpotPrice
        {
            get
            {
                return _goldSpotPrice;
            }
            set
            {
                _goldSpotPrice = value;
                OnPropertyChanged("GoldSpotPrice");
            }
        }

        public decimal? SilverSpotPrice
        {
            get
            {
                return _silverSpotPrice;
            }
            set
            {
                _silverSpotPrice = value;
                OnPropertyChanged("SilverSpotPrice");
            }
        }

        public decimal? PlatinumSpotPrice
        {
            get
            {
                return _platinumSpotPrice;
            }
            set
            {
                _platinumSpotPrice = value;
                OnPropertyChanged("PlatinumSpotPrice");
            }
        }

        public decimal? PalladiumSpotPrice
        {
            get
            {
                return _palladiumSpotPrice;
            }
            set
            {
                _palladiumSpotPrice = value;
                OnPropertyChanged("PalladiumSpotPrice");
            }
        }

        public int? GoldOunces
        {
            get
            {
                return _goldOunces;
            }
            set
            {
                _goldOunces = value;
                OnPropertyChanged("GoldOunces");
                RecalculateTotals();
            }
        }

        public int? SilverOunces
        {
            get
            {
                return _silverOunces;
            }
            set
            {
                _silverOunces = value;
                OnPropertyChanged("SilverOunces");
                RecalculateTotals();
            }
        }

        public int? PlatinumOunces
        {
            get
            {
                return _platinumOunces;
            }
            set
            {
                _platinumOunces = value;
                OnPropertyChanged("PlatinumOunces");
                RecalculateTotals();
            }
        }

        public int? PalladiumOunces
        {
            get
            {
                return _palladiumOunces;
            }
            set
            {
                _palladiumOunces = value;
                OnPropertyChanged("PalladiumOunces");
                RecalculateTotals();
            }
        }

        public decimal? GoldTotal
        {
            get
            {
                return _goldTotal;
            }
            set
            {
                _goldTotal = value;
                OnPropertyChanged("GoldTotal");
            }
        }

        public decimal? SilverTotal
        {
            get
            {
                return _silverTotal;
            }
            set
            {
                _silverTotal = value;
                OnPropertyChanged("SilverTotal");
            }
        }

        public decimal? PlatinumTotal
        {
            get
            {
                return _platinumTotal;
            }
            set
            {
                _platinumTotal = value;
                OnPropertyChanged("PlatinumTotal");
            }
        }

        public decimal? PalladiumTotal
        {
            get
            {
                return _palladiumTotal;
            }
            set
            {
                _palladiumTotal = value;
                OnPropertyChanged("PalladiumTotal");
            }
        }

        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return _saveCommand;
            }
            set
            {
                _saveCommand = value;
            }
        }

        private void SaveCommandExecuted(object obj)
        {
            PreciousMetalsHistory history = new PreciousMetalsHistory();

            history.AsOfDate = DateTime.Now;
            history.GoldOunces = GoldOunces;
            history.GoldSpotPrice = GoldSpotPrice;
            history.SilverOunces = SilverOunces;
            history.SilverSpotPrice = SilverSpotPrice;
            history.PlatinumOunces = PlatinumOunces;
            history.PlatinumSpotPrice = PlatinumSpotPrice;
            history.PalladiumOunces = PalladiumOunces;
            history.PalladiumSpotPrice = PalladiumSpotPrice;
            history.RunId = _runId;

            DataAccess.SaveItem(history);
        }

        public PreciousMetalsViewModel(int? runId)
        {
            Initialize();
            RefreshProperties(runId);
        }

        public PreciousMetalsViewModel()
        {
            Initialize();
            RefreshProperties(DataAccess.GetNextRunId());
        }

        private void Initialize()
        {
            SaveCommand = new RelayCommand(SaveCommandExecuted);
        }

        private void RefreshProperties(int? runId)
        {
            _runId = runId;

            PreciousMetalsPriceApi prices = new PreciousMetalsPriceApi();
            GoldSpotPrice = prices.GetPreciousMetalsPrice(Enums.PreciousMetalsTypes.Gold);
            SilverSpotPrice = prices.GetPreciousMetalsPrice(Enums.PreciousMetalsTypes.Silver);
            PlatinumSpotPrice = prices.GetPreciousMetalsPrice(Enums.PreciousMetalsTypes.Platinum);
            PalladiumSpotPrice = prices.GetPreciousMetalsPrice(Enums.PreciousMetalsTypes.Palladium);

            var history = DataAccess.GetList<PreciousMetalsHistory>().OrderByDescending(r => r.RunId).FirstOrDefault();
            if (history != null)
            {
                GoldOunces = history.GoldOunces;
                SilverOunces = history.SilverOunces;
                PlatinumOunces = history.PlatinumOunces;
                PalladiumOunces = history.PalladiumOunces;
            }
        }

        private void RecalculateTotals()
        {
            GoldTotal = GoldSpotPrice * GoldOunces;
            SilverTotal = SilverSpotPrice * SilverOunces;
            PlatinumTotal = PlatinumSpotPrice * PlatinumOunces;
            PalladiumTotal = PalladiumSpotPrice * PalladiumOunces;
        }

        public decimal? GetTotals()
        {
            RecalculateTotals();

            return GoldTotal + SilverTotal + PlatinumTotal + PalladiumTotal;
        }

    }
}
